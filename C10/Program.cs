using CleanCodeProject.C10.Models;
using CleanCodeProject.C10.Services;

namespace CleanCodeProject.C10;

public class Program
{
    public static void Main(string[] args)
    {
        var config = new PrintConfig();
        var generator = new PrimeGenerator();
        var pagePrinter = new ConsolePagePrinter();
        var printer = new ConsolePrimePrinter(config, pagePrinter);

        var primes = generator.Generate(1000);
        printer.Print(primes);
    }
}