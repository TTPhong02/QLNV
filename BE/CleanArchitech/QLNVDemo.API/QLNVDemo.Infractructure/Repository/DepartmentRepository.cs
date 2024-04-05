using Dapper;
using MISA.AMISDemo.Core.Entities;
using MISA.AMISDemo.Core.Interfaces.Departments;
using MISA.AMISDemo.Infractructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Infractructure.Repository
{
    public class DepartmentRepository : BaseRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(IMISADbContext dbContext) : base(dbContext)
        {
        }

        public bool CheckDuplicateDepartmentCode(string departmentCode)
        {
            var sql = "SELECT DepartmentCode FROM Department c WHERE c.DepartmentCode = @DepartmentCode";

            var parameters = new DynamicParameters();
            parameters.Add("@DepartmentCode", departmentCode);
            var res = _dbContext.Connection.QueryFirstOrDefault<string>(sql, parameters);
            return res != null;
        }

        
    }
}
