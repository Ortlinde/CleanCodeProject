namespace CleanCodeProject.C06
{
    public class Point
    {
        public double x;
        public double y;
    }

    public class Square
    {
        public Point topLeft;
        public double side;
    }
    public class Rectangle
    {
        public Point topLeft;
        public double height;
        public double width;
    }
    public class Circle
    {
        public Point center;
        public double radius;
    }
    public class Geometry
    {
        public const double PI = 3.141592653589793;

        public double Area(object shape)
        {
            if (shape is Square s)
                return s.side * s.side;
            else if (shape is Rectangle r)
                return r.height * r.width;
            else if (shape is Circle c)
                return PI * c.radius * c.radius;

            throw new InvalidOperationException("No such shape");
        }
    }












    public interface IShape
    {
        double Area();
    }

    public class Square2 : IShape
    {
        private Point TopLeft { get; set; }
        private double Side { get; set; }

        public double Area()
        {
            return Side * Side;
        }
    }

    public class Rectangle2 : IShape
    {
        private Point TopLeft { get; set; }
        private double Height { get; set; }
        private double Width { get; set; }

        public double Area()
        {
            return Height * Width;
        }
    }

    public class Circle2 : IShape
    {
        private Point Center { get; set; }
        private double Radius { get; set; }
        private const double PI = 3.141592653589793;

        public double Area()
        {
            return PI * Radius * Radius;
        }
    }
}
