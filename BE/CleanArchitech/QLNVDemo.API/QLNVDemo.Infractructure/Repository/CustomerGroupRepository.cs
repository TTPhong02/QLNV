using Dapper;
using MISA.AMISDemo.Core.Entities;
using MISA.AMISDemo.Core.Interfaces.Customers;
using MISA.AMISDemo.Infractructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Infractructure.Repository
{
    public class CustomerGroupRepository : BaseRepository<CustomerGroup>, ICustomerGroupRepository
    {
        public CustomerGroupRepository(IMISADbContext dbContext) : base(dbContext)
        {
        }

        public bool CheckDuplicateCustomerGroupName(string CustomerGroupName)
        {
            var sql = "SELECT CustomerGroupName FROM CustomerGroup c WHERE c.CustomerGroupName = @CustomerGroupName";

            var parameters = new DynamicParameters();
            parameters.Add("@CustomerGroupName", CustomerGroupName);
            var res = _dbContext.Connection.QueryFirstOrDefault<string>(sql, parameters);
            return res != null;
        }
    }
}
