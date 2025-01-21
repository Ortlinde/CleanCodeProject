namespace CleanCodeProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            goto Label;
            while (true) ; // 沒有主體的while/for迴圈
            {
                // 這個區段和上面的while/for是無關的
            }

        Label:
            Console.WriteLine("Hello, World!");
        }
    }
}
