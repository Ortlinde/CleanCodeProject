using System.Text;

namespace CleanCodeProject.C03
{
    public class FunctionArguments
    {
        // 好的做法：分開成兩個明確的方法
        public void RenderAdminView()
        {
            RenderAdminPage();
        }

        public void RenderUserView()
        {
            RenderUserPage();
        }

        private void RenderAdminPage()
        {
            // 渲染管理員頁面的邏輯
        }

        private void RenderUserPage()
        {
            // 渲染一般用戶頁面的邏輯
        }

        Circle MakeCircle(int x, int y, int radius)
        {
            return new Circle(new Point(x, y), radius);
        }

        Circle MakeCircle(Point center, int radius)
        {
            return new Circle(center, radius);
        }

        public void Append(StringBuilder text)
        {
            text.Append("Tail.");
        }
    }

    public class Circle
    {
        public Circle(Point center, int radius)
        {
            // 實現 Circle 的邏輯
        }
    }

    public class Point
    {
        public Point(int x, int y)
        {
            // 實現 Point 的邏輯
        }
    }
}