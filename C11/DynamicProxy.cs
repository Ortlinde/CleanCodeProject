using System.Reflection;

namespace CleanCodeProject.C11;

// 定義接口
public interface IUserService
{
    void CreateUser(string username);
    string GetUser(int id);
}

// 實際服務實現
public class UserService : IUserService
{
    public void CreateUser(string username)
    {
        Console.WriteLine($"創建用戶：{username}");
    }

    public string GetUser(int id)
    {
        return $"用戶 ID: {id}";
    }
}

public class DynamicProxy<T> where T : class
{
    private readonly T _target;

    public DynamicProxy(T target)
    {
        _target = target;
    }

    public T CreateProxy()
    {
        // 使用 DispatchProxy 創建代理
        return DispatchProxy.Create<T, MethodProxy<T>>();
    }
}

public class MethodProxy<T> : DispatchProxy where T : class
{
    private T _target;

    public void SetTarget(T target)
    {
        _target = target;
    }

    protected override object Invoke(MethodInfo targetMethod, object[] args)
    {
        try
        {
            Console.WriteLine($"方法執行前：{targetMethod.Name}");

            // 執行實際方法
            var result = targetMethod.Invoke(_target, args);

            Console.WriteLine($"方法執行後：{targetMethod.Name}");

            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"方法執行出錯：{ex.Message}");
            throw;
        }
    }
}

public class Program
{
    //輸出:
    //方法執行前：CreateUser
    //創建用戶：張三
    //方法執行後：CreateUser
    public static void Main(string[] args)
    {
        // 使用方式
        IUserService userService = new UserService();
        var proxy = new DynamicProxy<IUserService>(userService);
        var proxyService = proxy.CreateProxy();

        proxyService.CreateUser("張三"); // 將輸出執行前後的日誌
    }
}
