namespace POS.Application.Interfaces
{
    public interface IGenerateExcelApplication
    {
        byte[] GenerateExcel<T>(IEnumerable<T> data, List<(string ColumnName, string PropertyName)> columns);
    }
}
