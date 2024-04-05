using MISA.AMISDemo.Core.Interfaces.Base;
using MISA.AMISDemo.Core.Interfaces.Excels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.Services.Excels
{
    public class BaseExcelService<T> :ExportExcelService<T>,  IBaseExcelService<T> where T : class
    {
        IBaseRepository<T> _baseRepository;
        public BaseExcelService( IBaseRepository<T> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public byte[] ExportAll()
        {
            var data = _baseRepository.GetAll();
            var bytes = ExportExcel(data);
            return bytes;
        }
    }
}
