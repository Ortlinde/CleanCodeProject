namespace CleanCodeProject.C06
{
    public class DataAbstraction
    {
        public class Point : IAbstractPoint
        {
            private double r;     // 改用極座標儲存
            private double theta;

            public double getX()
            {
                return r * Math.Cos(theta); // 動態計算
            }

            public double getY()
            {
                return r * Math.Sin(theta);
            }

            public void setCartesian(double x, double y)
            {
                r = Math.Sqrt(x * x + y * y);
                theta = Math.Atan2(y, x);
            }

            public double getR()
            {
                return r;
            }

            public double getTheta()
            {
                return theta;
            }

            public void setPolar(double r, double theta)
            {
                this.r = r;
                this.theta = theta;
            }
        }

        public interface IAbstractPoint
        {
            double getX();
            double getY();
            void setCartesian(double x, double y);
            double getR();
            double getTheta();
            void setPolar(double r, double theta);
        }
    }
}