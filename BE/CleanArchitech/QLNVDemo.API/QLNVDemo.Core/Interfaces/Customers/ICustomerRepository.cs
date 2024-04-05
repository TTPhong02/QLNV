using MISA.AMISDemo.Core.Entities;
using MISA.AMISDemo.Core.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.Interfaces.Customers
{
    public interface ICustomerRepository : IBaseRepository<Customer>
    {
        /// <summary>
        /// Thực hiện kiểm tra trùng mã khách hàng
        /// </summary>
        /// <param name="customerCode"></param>
        /// <returns>
        /// trả về true nếu trùng 
        /// trả về false nếu không trùng
        /// </returns>
        /// Created by : TTPhong(25/12/2023)
        bool CheckDuplicateCustomerCode(string customerCode);
    }
}
