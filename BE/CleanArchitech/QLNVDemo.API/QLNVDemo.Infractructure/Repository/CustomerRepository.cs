using MISA.AMISDemo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MySqlConnector;
using System.Data;
using MISA.AMISDemo.Infractructure.Interfaces;
using MISA.AMISDemo.Core.Interfaces.Customers;

namespace MISA.AMISDemo.Infractructure.Repository
{
    public class CustomerRepository:BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(IMISADbContext dbContext) : base(dbContext)
        {
        }
        public bool CheckDuplicateCustomerCode(string customerCode)
        {
            var sql = "SELECT CustomerCode FROM Customer c WHERE c.CustomerCode = @CustomerCode";
            var parameters = new DynamicParameters();
            parameters.Add("@CustomerCode", customerCode);
            var res = _dbContext.Connection.QueryFirstOrDefault<string>(sql, parameters,transaction:_dbContext.Transaction);
            return res != null;
        }
    }
}
