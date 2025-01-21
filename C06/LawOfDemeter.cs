namespace CleanCodeProject.C06
{
    public class LawOfDemeter
    {
        // 違反迪米特定律的例子
        public class BadExample
        {
            public void ProcessOrder(Customer customer)
            {
                customer.GetWallet().GetMoney().Reduce(50);  // 違反定律：方法鏈造成了深層次的依賴
            }
        }

        // 遵循迪米特定律的例子
        public class GoodExample
        {
            public void ProcessOrder(Customer customer)
            {
                customer.MakePayment(50);  // 好的做法：只與直接的朋友交談
            }
        }

        public class Customer
        {
            private Wallet wallet;

            public void MakePayment(decimal amount)
            {
                wallet.DeductMoney(amount);
            }
        }

        public class Wallet
        {
            private Money money;

            public void DeductMoney(decimal amount)
            {
                money.Reduce(amount);
            }
        }

        public class Money
        {
            private decimal amount;

            public void Reduce(decimal value)
            {
                amount -= value;
            }
        }

        // 流式接口的正確使用方式
        public class FluentCustomer
        {
            private int points;
            private int level;
            private decimal discount;

            public FluentCustomer AddPoints(int points)
            {
                this.points += points;
                return this;
            }

            public FluentCustomer UpdateLevel()
            {
                this.level = points / 100;
                return this;
            }

            public FluentCustomer AddDiscount(decimal discount)
            {
                this.discount = discount;
                return this;
            }
        }

        // 實際使用的例子
        public class OrderProcessor
        {
            public void ProcessCustomerOrder()
            {
                // 使用流式接口的正確方式
                var customer = new FluentCustomer()
                    .AddPoints(100)
                    .UpdateLevel()
                    .AddDiscount(10);

                // LINQ的使用是可以接受的
                var numbers = new[] { 1, 2, 3, 4, 5 };
                var result = numbers
                    .Where(n => n > 2)
                    .Select(n => n * 2)
                    .ToList();
            }
        }
    }
}
