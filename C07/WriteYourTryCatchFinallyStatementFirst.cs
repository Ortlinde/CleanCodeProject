namespace CleanCodeProject.C07;
using System.IO;

public class WriteYourTryCatchFinallyStatementFirst
{
    public void ProcessFile(string filePath)
    {
        // 先定義 try-catch-finally 框架
        try
        {
            // 在這裡放置可能拋出異常的文件處理邏輯
            var fileStream = File.OpenRead(filePath);
            // 處理文件...
        }
        catch (FileNotFoundException ex)
        {
            // 確保在文件不存在時，程序仍然處於一致的狀態
            Logger.LogError($"文件不存在: {ex.Message}");
            throw new BusinessException("無法找到指定的文件", ex);
        }
        finally
        {
            // 清理資源，確保無論是否發生異常，資源都能被正確釋放
            // 例如關閉文件流
        }
    }
}