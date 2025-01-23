namespace CleanCodeProject.C09;

public class First
{
    // 1. 先寫測試
    [Fact]
    public void Calculator_Add_ShouldReturnSum()
    {
        var calculator = new Calculator();
        var result = calculator.Add(2, 3);
        Assert.Equal(5, result);
    }

    // 2. 再實現功能
    public class Calculator
    {
        public int Add(int a, int b)
        {
            return a + b;
        }
    }


    // ❌ 不好的例子：需要人工檢查
    public class Logger
    {
        public void Log(string message)
        {
            File.AppendAllText("log.txt", message);
        }
    }

    [Fact]
    public void TestLogOutput()
    {
        var logger = new Logger();
        logger.Log("test message");
        // 需要手動檢查日誌文件
        Console.WriteLine("請檢查 log.txt 文件是否包含消息");
    }

    // 回傳布林值判斷是否包含測試消息
    public bool CheckLogHasTestMessage()
    {
        var mockLogger = new MockLogger();
        var logger = new Logger(mockLogger);

        logger.Log("test message");

        return mockLogger.LastMessage.Contains("test message");
    }
}