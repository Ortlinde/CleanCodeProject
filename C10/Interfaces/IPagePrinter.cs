using CleanCodeProject.C10.Models;

namespace CleanCodeProject.C10.Interfaces;

public interface IPagePrinter
{
    void PrintPage(int[] numbers, int pageNumber, int pageOffset, PrintConfig config);
}