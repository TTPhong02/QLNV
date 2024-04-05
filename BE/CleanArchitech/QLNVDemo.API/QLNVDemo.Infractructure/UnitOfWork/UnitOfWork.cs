using MISA.AMISDemo.Core.Entities;
using MISA.AMISDemo.Core.Interfaces;
using MISA.AMISDemo.Core.Interfaces.Customers;
using MISA.AMISDemo.Core.Interfaces.Employees;
//using MISA.AMISDemo.Core.Interfaces.UnitOfWork;
using MISA.AMISDemo.Infractructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Infractructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        IMISADbContext _dbContext;
        public ICustomerRepository Customers { get; }
        public IEmployeeRepository Employees { get; }

        public UnitOfWork(IMISADbContext dbContext, ICustomerRepository customerRepository, IEmployeeRepository employeeRepository)
        {
            _dbContext = dbContext;
            Customers = customerRepository;
            Employees = employeeRepository;
        }

        public void BeginTransaction()
        {
            _dbContext.Connection.Open();
            _dbContext.Transaction = _dbContext.Connection.BeginTransaction();
        }

        public void Commit()    
        {
            _dbContext.Transaction.Commit();
        }

        public void Dispose()
        {
            if (_dbContext.Connection.State == ConnectionState.Open)
            {
                _dbContext.Connection.Close();
            }
            _dbContext.Connection?.Dispose();
        }

        public void Rollback()
        {
            _dbContext.Transaction.Rollback();
        }
    }
}
