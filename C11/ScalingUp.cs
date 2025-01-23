namespace CleanCodeProject.C11;

public class ScalingUp
{
    // 一般的業務邏輯（沒有 AOP）
    public class UserService
    {
        public void CreateUser(User user)
        {
            Console.WriteLine("開始記錄日誌..."); // 橫切關注點
            try
            {
                BeginTransaction();              // 橫切關注點
                                                 // 實際的用戶創建邏輯
                Console.WriteLine("創建用戶...");
                CommitTransaction();             // 橫切關注點
            }
            catch (Exception ex)
            {
                RollbackTransaction();           // 橫切關注點
                Console.WriteLine("記錄錯誤..."); // 橫切關注點
                throw;
            }
        }
    }

    // 使用 AOP 後
    public class UserService
    {
        public void CreateUser(User user)
        {
            // 只關注核心業務邏輯
            Console.WriteLine("創建用戶...");
        }
    }

    // 日誌切面
    [Aspect]
    public class LoggingAspect
    {
        [Before("execution(* CreateUser(..))")]
        public void LogBefore()
        {
            Console.WriteLine("開始記錄日誌...");
        }

        [AfterThrowing]
        public void LogError()
        {
            Console.WriteLine("記錄錯誤...");
        }
    }

    // 事務切面
    [Aspect]
    public class TransactionAspect
    {
        [Before]
        public void StartTransaction()
        {
            BeginTransaction();
        }

        [AfterReturning]
        public void CommitTransaction()
        {
            CommitTransaction();
        }
    }
}