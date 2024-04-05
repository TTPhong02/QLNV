using MISA.AMISDemo.Core.Entities;
using MISA.AMISDemo.Core.Interfaces.Employees;
using MISA.AMISDemo.Core.Interfaces.Excels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.Services.Excels
{
    public class EmployeeExcelService : BaseExcelService<Employee> , IEmployeeExcelService
    {
        public EmployeeExcelService(IEmployeeRepository employeeRepository) : base(employeeRepository)
        {
        }
    }
}
