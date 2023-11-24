using POS.Utilities.Static;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Infrastructure.FileExcel
{
    public interface IGenerateExcel
    {
        MemoryStream GenerateToExcel<T>(IEnumerable<T> data, List<TableColumns> columns);
    }
}
