namespace CleanCodeProject.C03
{
    public class HtmlUtilRefactory
    {
        // 渲染包含設置和清理頁面的測試頁面
        public static string RenderPageWithSetupsAndTeardowns(PageData pageData, bool isSuite)
        {
            if (IsTestPage(pageData))
                IncludeSetupAndTeardownPages(pageData, isSuite);
            return pageData.Html;
        }

        private static bool IsTestPage(PageData pageData)
        {
            return pageData.HasAttribute("Test");
        }

        private static void IncludeSetupAndTeardownPages(PageData pageData, bool isSuite)
        {
            WikiPage testPage = pageData.WikiPage;
            StringBuffer newPageContent = new StringBuffer();

            IncludeSetupPages(testPage, newPageContent, isSuite);
            newPageContent.Append(pageData.Content);
            IncludeTeardownPages(testPage, newPageContent, isSuite);

            pageData.Content = newPageContent.ToString();
        }

        private static void IncludeSetupPages(WikiPage testPage, StringBuffer newPageContent, bool isSuite)
        {
            if (isSuite)
                IncludeSuiteSetupPage(testPage, newPageContent);
            IncludeSetupPage(testPage, newPageContent);
        }

        private static void IncludeSuiteSetupPage(WikiPage testPage, StringBuffer newPageContent)
        {
            WikiPage suiteSetup = PageCrawlerImpl.GetInheritedPage(SuiteResponder.SUITE_SETUP_NAME, testPage);
            if (suiteSetup != null)
                IncludeSetupPage(suiteSetup, newPageContent);
        }

        private static void IncludeSetupPage(WikiPage testPage, StringBuffer newPageContent)
        {
            WikiPage setup = PageCrawlerImpl.GetInheritedPage("SetUp", testPage);
            if (setup != null)
                IncludePage(setup, "-setup", newPageContent);
        }

        private static void IncludeTeardownPages(WikiPage testPage, StringBuffer newPageContent, bool isSuite)
        {
            IncludeTeardownPage(testPage, newPageContent);
            if (isSuite)
                IncludeSuiteTeardownPage(testPage, newPageContent);
        }

        private static void IncludeTeardownPage(WikiPage testPage, StringBuffer newPageContent)
        {
            WikiPage teardown = PageCrawlerImpl.GetInheritedPage("TearDown", testPage);
            if (teardown != null)
                IncludePage(teardown, "-teardown", newPageContent);
        }

        private static void IncludeSuiteTeardownPage(WikiPage testPage, StringBuffer newPageContent)
        {
            WikiPage suiteTeardown = PageCrawlerImpl.GetInheritedPage(SuiteResponder.SUITE_TEARDOWN_NAME, testPage);
            if (suiteTeardown != null)
                IncludePage(suiteTeardown, "-teardown", newPageContent);
        }

        private static void IncludePage(WikiPage page, string mode, StringBuffer newPageContent)
        {
            WikiPagePath pagePath = page.PageCrawler.GetFullPath(page);
            string pagePathName = PathParser.Render(pagePath);
            newPageContent.Append("!include ")
                .Append(mode)
                .Append(" .")
                .Append(pagePathName)
                .Append("\n");
        }
    }
}