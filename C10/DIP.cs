namespace CleanCodeProject.C10;

public class DIP
{
    public class IoC
    {
        public static void Main(string[] args)
        {
            Programmer programmer = new Programmer();
            IProgrammable computer = new Computer();

            programmer.SetProgrammable(computer);
        }
    }

    public class Programmer
    {
        private IProgrammable programmable;

        public void SetProgrammable(IProgrammable programmable)
        {
            this.programmable = programmable;
        }

        public void Work()
        {
            programmable.Program();
        }
    }

    public interface IProgrammable
    {
        void Program();
    }

    public class Computer : IProgrammable
    {
        public void Program()
        {
            Console.WriteLine("Programmer is programming");
        }
    }
}