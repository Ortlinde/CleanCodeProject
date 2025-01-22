namespace CleanCodeProject.C10;

public class ISP
{
    #region interface
    interface IMultiFunction
    {
        public void Print();

        public void Scan();

        public void Fax();

        public void Copy();
    }

    interface IPrint
    {
        public void Print();
    }

    interface IScan
    {
        public void Scan();
    }

    interface IFax
    {
        public void Fax();
    }

    interface ICopy
    {
        public void Copy();
    }
    #endregion

    #region bad example
    class MultiFunctionPrinter : IMultiFunction
    {
        public void Print()
        {
            Console.WriteLine("Printing");
        }

        public void Scan()
        {
            Console.WriteLine("Scanning");
        }

        public void Fax()
        {
            Console.WriteLine("Faxing");
        }

        public void Copy()
        {
            Console.WriteLine("Copying");
        }
    }

    class OldPrinter : IMultiFunction
    {
        public void Print()
        {
            Console.WriteLine("Printing");
        }

        public void Scan()
        {
            // do nothing 
        }

        public void Fax()
        {
            // do nothing 
        }

        public void Copy()
        {
            Console.WriteLine("Copying");
        }
    }
    #endregion

    #region good example
    class GoodMultiFunctionPrinter : IPrint, IScan, IFax, ICopy
    {
        public void Print()
        {
            Console.WriteLine("Printing");
        }

        public void Scan()
        {
            Console.WriteLine("Scanning");
        }

        public void Fax()
        {
            Console.WriteLine("Faxing");
        }

        public void Copy()
        {
            Console.WriteLine("Copying");
        }
    }

    class GoodOldPrinter : IPrint, ICopy
    {
        public void Print()
        {
            Console.WriteLine("Printing");
        }

        public void Copy()
        {
            Console.WriteLine("Copying");
        }
    }
    #endregion
}