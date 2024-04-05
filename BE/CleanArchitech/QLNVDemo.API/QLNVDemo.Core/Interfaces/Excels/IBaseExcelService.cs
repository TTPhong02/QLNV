using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.Interfaces.Excels
{
    public interface IBaseExcelService<T> where T : class
    {
        /// <summary>
        /// Xuất file excel cho tất cả danh sách
        /// </summary>
        /// <returns>trả về mảng byte file excel</returns>
        /// Created By : TTPhong (29/01/2024)
        byte[] ExportAll();
    }
}
