using Dapper;
using MISA.AMISDemo.Core.Entities;
using MISA.AMISDemo.Core.Interfaces.Employees;
using MISA.AMISDemo.Infractructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Infractructure.Repository
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(IMISADbContext dbContext) : base(dbContext)
        {
        }

        public bool CheckDuplicateEmployeeCode(string employeeCode)
        {
            var sql = "SELECT EmployeeCode FROM Employee c WHERE c.EmployeeCode = @EmployeeCode";

            var parameters = new DynamicParameters();
            parameters.Add("@EmployeeCode", employeeCode);
            var res = _dbContext.Connection.QueryFirstOrDefault<string>(sql, parameters,transaction: _dbContext.Transaction);
            return res != null;
        }
        public string GetMaxEmployeeCode()
        {
            var sql = "Proc_Employee_GetMaxEmployeeCode";
            var parameters = new DynamicParameters();
            parameters.Add("@MaxEmployeeCode", dbType: DbType.String, direction: ParameterDirection.Output);

            _dbContext.Connection.Execute(sql:sql, parameters, commandType: CommandType.StoredProcedure);

            var MaxEmployeeCode = "NV-" + parameters.Get<string>("@MaxEmployeeCode");
            return MaxEmployeeCode;
            
            
        }

        public async Task<PagingEntity<Employee>> Paging(int pageSize, int pageNumber, string? searchString)
        {
            var sql = $"Proc_Employee_Filter";

            PagingEntity<Employee> pagingEntity = new PagingEntity<Employee>();
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@pageSize", pageSize);
            parameters.Add("@pageNumber", pageNumber);
            parameters.Add("@searchString", searchString);

            parameters.Add("@totalRecord", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);
            parameters.Add("@totalPage", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);

            var res = await _dbContext.Connection.QueryAsync<Employee>(sql, parameters, commandType: System.Data.CommandType.StoredProcedure);

            pagingEntity.Data = res;
            pagingEntity.TotalRecord = parameters.Get<int>("@totalRecord");
            pagingEntity.TotalPage = parameters.Get<int>("@totalPage");

            return pagingEntity;
        }
    }
}
