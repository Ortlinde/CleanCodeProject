using CleanCodeProject.C10.Interfaces;
using CleanCodeProject.C10.Models;

namespace CleanCodeProject.C10.Services;

public class ConsolePrimePrinter : IPrimePrinter
{
    private readonly PrintConfig _config;
    private readonly IPagePrinter _pagePrinter;

    public ConsolePrimePrinter(PrintConfig config, IPagePrinter pagePrinter)
    {
        _config = config;
        _pagePrinter = pagePrinter;
    }

    public void Print(int[] numbers)
    {
        int pageNumber = 1;
        int pageOffset = 1;

        while (pageOffset <= numbers.Length - 1)
        {
            _pagePrinter.PrintPage(numbers, pageNumber, pageOffset, _config);
            pageOffset += _config.RowsPerPage * _config.ColumnsPerPage;
            pageNumber++;
        }
    }
}