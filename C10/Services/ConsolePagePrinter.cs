using CleanCodeProject.C10.Interfaces;
using CleanCodeProject.C10.Models;

namespace CleanCodeProject.C10.Services;

public class ConsolePagePrinter : IPagePrinter
{
    public void PrintPage(int[] numbers, int pageNumber, int pageOffset, PrintConfig config)
    {
        PrintHeader(numbers.Length - 1, pageNumber);
        PrintContent(numbers, pageOffset, config);
        PrintFooter();
    }

    private void PrintHeader(int total, int pageNumber)
    {
        Console.WriteLine($"The First {total} Prime Numbers --- Page {pageNumber}");
        Console.WriteLine();
    }

    private void PrintContent(int[] numbers, int pageOffset, PrintConfig config)
    {
        for (int row = pageOffset; row < pageOffset + config.RowsPerPage; row++)
        {
            PrintRow(numbers, row, config);
            Console.WriteLine();
        }
    }

    private void PrintRow(int[] numbers, int rowOffset, PrintConfig config)
    {
        for (int col = 0; col < config.ColumnsPerPage; col++)
        {
            int position = rowOffset + col * config.RowsPerPage;
            if (position <= numbers.Length - 1)
            {
                Console.Write($"{numbers[position],PrintConfig.NUMBER_WIDTH}");
            }
        }
    }

    private void PrintFooter()
    {
        Console.WriteLine("\f");
    }
}