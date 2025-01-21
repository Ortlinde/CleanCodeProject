namespace CleanCodeProject.C07;

public class DontPassNull
{
    public void RegisterItem(Item item)
    {
        ArgumentNullException.ThrowIfNull(item);

        if (!item.IsValid())
        {
            throw new ArgumentException("Item must be in a valid state", nameof(item));
        }

        ProcessRegistration(item);
    }

    private void ProcessRegistration(Item item)
    {
        // 由於已經在公開方法檢查過null，這裡可以安全地使用item
        // 實作註冊邏輯
    }
}