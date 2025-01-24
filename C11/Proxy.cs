namespace CleanCodeProject.C11;

// 定義抽象介面
public interface ISubject
{
    void Request();
}

// 真實主題類別
public class RealSubject : ISubject
{
    public void Request()
    {
        Console.WriteLine("RealSubject 處理請求");
    }
}

// 代理類別
public class Proxy : ISubject
{
    private RealSubject _realSubject;

    public void Request()
    {
        // 延遲初始化
        if (_realSubject == null)
        {
            _realSubject = new RealSubject();
        }

        // 在執行實際請求前可以加入額外的控制邏輯
        PreRequest();

        // 呼叫真實主題的方法
        _realSubject.Request();

        // 在執行實際請求後可以加入額外的控制邏輯
        PostRequest();
    }

    private void PreRequest()
    {
        Console.WriteLine("Proxy: 執行請求前的處理");
    }

    private void PostRequest()
    {
        Console.WriteLine("Proxy: 執行請求後的處理");
    }
}