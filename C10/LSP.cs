namespace CleanCodeProject.C10;

public class LSP
{
    #region bird
    public void TestFly()
    {
        Bird bird = new Bird();
        bird.Fly(); // Print: Flying

        Bird ostrich = new Ostrich();
        ostrich.Fly(); // Exception: Ostrich cannot fly
    }

    class Bird
    {
        public virtual void Fly()
        {
            Console.WriteLine("Flying");
        }
    }

    class Ostrich : Bird
    {
        public override void Fly()
        {
            throw new Exception("Ostrich cannot fly");
        }
    }
    #endregion

    #region shape

    public void TestShape()
    {
        Rectangle rectangle = new Rectangle();
        rectangle.SetWidth(10);
        rectangle.SetHeight(20);
        Console.WriteLine(rectangle.Area()); // Print: 200

        Rectangle square = new Square();
        square.SetWidth(10);
        square.SetHeight(20);
        Console.WriteLine(square.Area()); // Print: 400
    }

    class Rectangle
    {
        protected int width;
        protected int height;

        // 前置條件：width > 0
        // 後置條件：設置寬度且保持高度不變
        public virtual void SetWidth(int width)
        {
            if (width <= 0)
                throw new ArgumentException("寬度必須大於零");

            this.width = width;
        }

        public virtual void SetHeight(int value)
        {
            if (value < 0)
            {
                throw new Exception("Height cannot be negative");
            }
            height = value;
        }

        public virtual int GetWidth()
        {
            return width;
        }

        // 後置條件：返回面積 = width * height
        public virtual int GetArea()
        {
            return width * height;
        }
    }

    class Square : Rectangle
    {
        // 違反 LSP：改變了後置條件，
        public override int GetArea()
        {
            return width * width;
        }
    }
    #endregion

    #region pre-condition
    public class BankAccount
    {
        protected decimal balance;

        public virtual ICollection<int> PostConditionExampleMethod(int value)
        {
            if (value < 0)
                throw new Exception("value must be greater than 0");

            return new List<int>();
        }

        // 前置條件：amount > 0
        public virtual void Deposit(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("存款金額必須大於零");

            AddToBalance(amount);
        }

        // 提供給子類覆寫的方法
        protected virtual void AddToBalance(decimal amount)
        {
            balance += amount;
        }

        public virtual decimal GetBalance()
        {
            return balance;
        }
    }

    // 違反 LSP 的子類 - 前置條件更嚴格
    public class SavingsAccount : BankAccount
    {
        public override void Deposit(decimal amount)
        {
            // 違反 LSP：加入更嚴格的前置條件
            if (amount < 100)
                throw new ArgumentException("最低存款金額為 100");

            base.Deposit(amount);
        }
    }
    #endregion

    #region post-condition
    // 符合 LSP 的子類 - 前置條件更寬鬆
    public class FlexibleSavingsAccount : BankAccount
    {
        private const decimal MinimumPreferredDeposit = 100;
        private decimal bonusRate = 0.01m;

        public override IList<int> PostConditionExampleMethod(int value)
        {
            if (value < 0)
                throw new ArgumentException("value must be greater than 0");

            return new List<int>();
        }

        // 不覆寫 Deposit，保持原有的前置和後置條件
        protected override void AddToBalance(decimal amount)
        {
            if (amount >= MinimumPreferredDeposit)
            {
                decimal bonus = amount * bonusRate;
                base.AddToBalance(amount + bonus);
            }
            else
            {
                base.AddToBalance(amount);
            }
        }

        // 新增方法來說明額外功能
        public decimal CalculateBonus(decimal amount)
        {
            return amount >= MinimumPreferredDeposit ? amount * bonusRate : 0;
        }
    }

    public class BaseClass
    {
        // 後置條件：返回排序後的集合
        public virtual ICollection<int> GetNumbers(List<int> numbers)
        {
            return numbers.OrderBy(x => x).ToList();
        }
    }

    public class DerivedClass : BaseClass
    {
        // 違反 LSP：削弱了後置條件（不保證排序）
        public override ICollection<int> GetNumbers(List<int> numbers)
        {
            return numbers;
        }
    }

    public class TestDerived
    {
        public void TestDerivedMethod()
        {
            List<int> numbers = new List<int> { 3, 1, 4, 1, 5 };

            BaseClass baseClass = new BaseClass();
            baseClass.GetNumbers(numbers);

            DerivedClass derivedClass = new DerivedClass();
            derivedClass.GetNumbers(numbers);
        }
    }
    #endregion

    #region invariant
    public class InvariantClass
    {
        public class BankAccount
        {
            private decimal balance;
            private readonly decimal minimumBalance;
            private bool isFrozen;

            // 不變條件：
            // 1. balance >= minimumBalance
            // 2. 如果 isFrozen 為 true，則不允許任何交易

            public BankAccount(decimal initialBalance, decimal minimumBalance)
            {
                this.minimumBalance = minimumBalance;
                this.balance = initialBalance;
                ValidateInvariants();
            }

            public virtual void Withdraw(decimal amount)
            {
                ValidateInvariants();

                if (isFrozen)
                    throw new InvalidOperationException("帳戶已凍結");

                if (balance - amount < minimumBalance)
                    throw new InvalidOperationException("餘額不足");

                balance -= amount;

                ValidateInvariants();
            }

            protected void ValidateInvariants()
            {
                Debug.Assert(balance >= minimumBalance,
                    $"餘額 ({balance}) 不能低於最低要求 ({minimumBalance})");
            }
        }

        // 子類必須維護父類的不變條件
        public class PremiumAccount(decimal initialBalance, decimal minimumBalance) : BankAccount(initialBalance, minimumBalance)
        {
            private decimal overdraftLimit;

            // 新增的不變條件：
            // 3. overdraftLimit >= 0

            public override void Withdraw(decimal amount)
            {
                ValidateInvariants();
                ValidateOverdraftLimit();

                // ... 提款邏輯

                ValidateInvariants();
                ValidateOverdraftLimit();
            }

            private void ValidateOverdraftLimit()
            {
                Debug.Assert(overdraftLimit >= 0, "透支限額不能為負");
            }
        }
    }
    #endregion
}