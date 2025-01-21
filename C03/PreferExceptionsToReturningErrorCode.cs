namespace CleanCodeProject.C03
{
    public class PreferExceptionsToReturningErrorCode
    {
        public void ReturnErrorCodeExample()
        {
            if (deletePage(page) == ErrorCode.E_OK)
            {
                if (registry.deleteReference(page.name) == ErrorCode.E_OK)
                {
                    if (configKeys.deleteKey(page.name.makeKey()) == ErrorCode.E_OK)
                    {
                        logger.log("page deleted");
                    }
                    else
                    {
                        logger.log("configKey not deleted");
                    }
                }
                else
                {
                    logger.log("deleteReference from registry failed");
                }
            }
            else
            {
                logger.log("delete failed");
                return ErrorCode.E_ERROR;
            }
        }

        public void ThrowExceptionExample()
        {
            try
            {
                ProcessPageDeletion(page);
            }
            catch (PageDeletionException e)
            {
                HandlePageDeletionError(e);
            }
            catch (RegistryException e)
            {
                HandleRegistryError(e);
            }
            catch (ConfigurationException e)
            {
                HandleConfigurationError(e);
            }
        }

        private void ProcessPageDeletion(Page page)
        {
            DeletePage(page);
            DeleteRegistryReference(page.name);
            DeleteConfigKey(page.name.makeKey());
            logger.log("page deleted");
        }

        private void HandlePageDeletionError(PageDeletionException e)
        {
            logger.log($"delete failed: {e.Message}");
            throw;
        }

        private void HandleRegistryError(RegistryException e)
        {
            logger.log($"deleteReference from registry failed: {e.Message}");
            throw;
        }

        private void HandleConfigurationError(ConfigurationException e)
        {
            logger.log($"configKey not deleted: {e.Message}");
            throw;
        }

        private void DeletePage(Page page)
        {
            if (!TryDeletePage(page))
                throw new PageDeletionException(
                    $"無法刪除頁面 '{page.name}' - ID: {page.id}，類型: {page.type}");
        }

        private void DeleteRegistryReference(string pageName)
        {
            if (!registry.deleteReference(pageName))
                throw new RegistryException(
                    $"無法從註冊表刪除頁面 '{pageName}' 的參考，註冊表路徑: {registry.path}");
        }

        private void DeleteConfigKey(string key)
        {
            if (!configKeys.deleteKey(key))
                throw new ConfigurationException(
                    $"無法刪除設定鍵值 '{key}'，配置區域: {configKeys.section}");
        }
    }
}