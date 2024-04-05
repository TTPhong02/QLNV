using Microsoft.AspNetCore.Http;
using MISA.AMISDemo.Core.Entities;
using MISA.AMISDemo.Core.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.Interfaces.Customers
{
    public interface ICustomerService : IBaseService<Customer>
    {
        /// <summary>
        /// Thực hiện nhập khẩu dữ liệu
        /// </summary>
        /// <param name="fileImport">File nhập khẩu</param>
        /// <returns>Danh sách khách hàng được nhập thành công</returns>
        /// Created by : TTPhong (15/01/2024)
        IEnumerable<Customer> ImportCustomer(IFormFile fileImport);
    }
}
