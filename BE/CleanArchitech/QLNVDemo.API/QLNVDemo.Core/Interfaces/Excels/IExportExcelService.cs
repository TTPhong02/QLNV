using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.Interfaces.Excels
{
    public interface IExportExcelService<T> where T : class
    {
        public byte[] ExportExcel(IEnumerable<T> data);
    }
}
