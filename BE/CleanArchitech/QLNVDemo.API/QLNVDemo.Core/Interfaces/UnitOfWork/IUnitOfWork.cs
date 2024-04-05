using MISA.AMISDemo.Core.Entities;
using MISA.AMISDemo.Core.Interfaces.Customers;
using MISA.AMISDemo.Core.Interfaces.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {
        
        ICustomerRepository Customers  { get; }
        IEmployeeRepository Employees { get; }
        void BeginTransaction();
        void Commit();
        void Rollback();
    }
}
