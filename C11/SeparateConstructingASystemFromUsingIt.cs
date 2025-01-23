namespace CleanCodeProject.C11
{
    public class SeparateConstructingASystemFromUsingIt
    {
        public class Program
        {
            public static void Main(string[] args)
            {
                // 在啟動時進行系統構建和依賴注入
                ServiceFactory.Initialize();

                // 在運行時使用構建好的系統
                var service = ServiceFactory.GetService();
                service.DoWork();
            }
        }

        // 建立一個簡單的工廠類來處理系統構建
        public class ServiceFactory
        {
            private static IService service;

            public static void Initialize()
            {
                // 在啟動時進行系統構建和依賴注入
                var dependency = new Dependency();
                service = new ConcreteService(dependency);
            }

            public static IService GetService()
            {
                if (service == null)
                {
                    throw new InvalidOperationException("系統尚未初始化");
                }
                return service;
            }
        }

        // 定義接口和實現類
        public interface IService
        {
            void DoWork();
        }

        public class Dependency
        {
            public void HelperMethod() { }
        }

        public class ConcreteService : IService
        {
            private readonly Dependency _dependency;

            public ConcreteService(Dependency dependency)
            {
                _dependency = dependency;
            }

            public void DoWork()
            {
                _dependency.HelperMethod();
                // 執行業務邏輯
            }
        }
    }
}