using NUnit.Framework;

namespace CleanCodeProject.C10;

public class IsolatingFromChange
{
    public interface IStockExchange
    {
        public Money GetCurrentPrice(string symbol);
    }

    public class Money
    {
        public float Amount { get; }
        public Money(float amount) => Amount = amount;
    }

    public class StockQuote
    {
        private readonly IStockExchange exchange;
        private readonly Dictionary<string, float> stocks;

        public StockQuote(IStockExchange exchange)
        {
            this.exchange = exchange;
            this.stocks = new Dictionary<string, float>();
        }

        public void Add(string symbol, int quantity)
        {
            if (!CheckSymbolExists(symbol))
                stocks[symbol] = 0;
            stocks[symbol] += quantity;
        }

        private bool CheckSymbolExists(string symbol)
        {
            return stocks.ContainsKey(symbol);
        }

        public float GetTotal()
        {
            float total = 0;
            foreach (var stock in stocks)
            {
                var currentPrice = exchange.GetCurrentPrice(stock.Key);
                total += currentPrice.Amount * stock.Value;
            }
            return total;
        }
    }

    public class StockQuoteTest
    {
        private FixedStockExchangeStub exchange;
        private StockQuote? quote;

        [SetUp]
        public void SetUp()
        {
            exchange = new FixedStockExchangeStub();
            exchange.Fix("MSFT", 100);
            quote = new StockQuote(exchange);
        }

        [Test]
        public void GivenFiveMSFTTotalShouldBe500()
        {
            quote?.Add("MSFT", 5);
            Assert.AreEqual(500, quote?.GetTotal());
        }
    }

    public class FixedStockExchangeStub : IStockExchange
    {
        private string symbol;
        private float price;

        public void Fix(string symbol, float price)
        {
            this.symbol = symbol;
            this.price = price;
        }

        public Money GetCurrentPrice(string symbol) => new Money(price);
    }
}