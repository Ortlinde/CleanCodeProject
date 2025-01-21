namespace CleanCodeProject.C07;

public class UseUncheckedException
{
    // 不好的做法：模擬 Java 的受檢異常模式
    public class AccountService
    {
        public decimal GetBalance(string accountId)
        {
            // 在 Java 中，這些異常都需要在方法簽名中聲明
            // throws AccountNotFoundException, DatabaseException, NetworkException
            ValidateAccount(accountId);
            return FetchBalanceFromDatabase(accountId);
        }

        private void ValidateAccount(string accountId)
        {
            // 可能拋出 AccountNotFoundException
        }

        private decimal FetchBalanceFromDatabase(string accountId)
        {
            // 可能拋出 DatabaseException 或 NetworkException
            return 0;
        }
    }

    // 好的做法：使用非受檢異常
    public class BetterAccountService
    {
        public decimal GetBalance(string accountId)
        {
            // 無需在方法簽名中聲明異常
            // 代碼更簡潔，更容易維護
            if (string.IsNullOrEmpty(accountId))
                throw new ArgumentNullException(nameof(accountId));

            var account = FindAccount(accountId);
            return account.Balance;
        }

        private Account FindAccount(string accountId)
        {
            // 如果需要，直接拋出異常
            // 調用者可以選擇是否捕獲，不會強制要求處理
            throw new AccountNotFoundException($"Account {accountId} not found");
        }
    }

    public class AccountNotFoundException : Exception
    {
        public AccountNotFoundException(string message) : base(message) { }
    }

    private class Account
    {
        public decimal Balance { get; set; }
    }
}