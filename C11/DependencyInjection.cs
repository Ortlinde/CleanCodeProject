namespace CleanCodeProject.C11;

public class DependencyInjection
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var service = new ConcreteService();
            var dependencyInjection = new DependencyInjectionClass(service);

            dependencyInjection.DoWork();
        }
    }

    // 定義介面
    public interface IService
    {
        void Execute();
    }

    // 具體實現類別
    public class ConcreteService : IService
    {
        public void Execute()
        {
            // 實際的服務邏輯
        }
    }

    public class DependencyInjectionClass
    {
        private readonly IService _service;

        // 通過建構函式注入依賴
        public DependencyInjectionClass(IService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public void DoWork()
        {
            _service.Execute();
        }
    }
}
