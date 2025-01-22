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
    public class Stack<T>
    {
        private T[] items;
        private int _capacity;

        public int Count => items.Length;
        public int Capacity
        {
            get => _capacity;
            private set => _capacity = value > 0 ? value : throw new ArgumentException("容量不能小於零");
        }

        public Stack(int capacity)
        {
            items = new T[capacity];
            Capacity = capacity;
        }
    }
    #endregion

    #region post-condition

    #endregion

    #region invariant

    #endregion
}