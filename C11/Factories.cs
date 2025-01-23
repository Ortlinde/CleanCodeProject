namespace CleanCodeProject.C11;

public class Factories
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var factory = new LineItemFactoryImplementation();
            var orderProcessor = new OrderProcessing(factory);

            orderProcessor.ProcessOrder();
        }
    }

    public interface ILineItemFactory
    {
        public abstract ILineItem MakeLineItem();
    }

    public class LineItemFactoryImplementation : ILineItemFactory
    {
        public ILineItem MakeLineItem()
        {
            return new LineItem();
        }
    }

    public class OrderProcessing
    {
        private readonly ILineItemFactory _lineItemFactory;

        public OrderProcessing(ILineItemFactory lineItemFactory)
        {
            _lineItemFactory = lineItemFactory;
        }

        public void ProcessOrder()
        {
            var lineItem = _lineItemFactory.MakeLineItem();
            // 處理訂單邏輯...
        }
    }

    public interface ILineItem
    {

    }

    public class LineItem : ILineItem
    {
        // 實現 ILineItem 的屬性和方法
    }
}
