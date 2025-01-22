namespace CleanCodeProject.C10.Models;

public class PrintConfig
{
    public const int NUMBER_WIDTH = 10;

    public int RowsPerPage { get; }
    public int ColumnsPerPage { get; }

    public PrintConfig(int rowsPerPage = 50, int columnsPerPage = 4)
    {
        RowsPerPage = rowsPerPage;
        ColumnsPerPage = columnsPerPage;
    }
}