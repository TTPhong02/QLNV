using MISA.AMISDemo.Core.Entities;
using MISA.AMISDemo.Core.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.Interfaces.Employees
{
    public interface IEmployeeRepository : IBaseRepository<Employee>
    {
        /// <summary>
        /// Thực hiện kiểm tra trùng mã nhân viên
        /// </summary>
        /// <param name="employeeCode"></param>
        /// <returns>
        /// trả về true nếu trùng 
        /// trả về false nếu không trùng
        /// </returns>
        /// Created by : TTPhong(25/12/2023)
        bool CheckDuplicateEmployeeCode(string employeeCode);
        Task<PagingEntity<Employee>> Paging(int pageSize, int pageNumber, string? searchString);
        string GetMaxEmployeeCode();
    }
}
