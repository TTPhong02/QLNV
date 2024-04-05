using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MISA.AMISDemo.Core.Entities;
using MISA.AMISDemo.Infractructure.Interfaces;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Infractructure.MISADataBaseContext
{
    public class MySqlDbContext : IMISADbContext
    {
        public IDbConnection Connection { get; }
        public IDbTransaction Transaction { get; set; }

        public MySqlDbContext(IConfiguration config)
        {
            Connection = new MySqlConnection(config.GetConnectionString("DatabaseMySql"));
        }

        public IEnumerable<Y> GetAll<Y>()
        {
            var className = typeof(Y).Name;
            var sql = $"SELECT * FROM {className}";
            var res = Connection.Query<Y>(sql);
            return res;
        }

        public Y? GetById<Y>(Guid id)
        {
            var className = typeof(Y).Name;
            //câu lẹnh sql
            var sql = $"SELECT * FROM {className} WHERE {className}Id = @id";
            var parameters = new DynamicParameters(id);
            parameters.Add("@id", id);
            var data = Connection.QueryFirstOrDefault<Y>(sql, parameters);
            //trả về kết quả
            return data;
        }

        public int Insert<Y>(Y entity)
        {
            var className = typeof(Y).Name;
            var sql = $"Proc_Insert_{className}";
            var data = Connection.Execute(sql: sql,param:entity,commandType: System.Data.CommandType.StoredProcedure,transaction:Transaction);
            //trả về kết quả
            return data;
        }

        public int Update<Y>(Y entity, Guid id)
        {
            var className = typeof(Y).Name;
            entity?.GetType()?.GetProperty($"{className}Id")?.SetValue(entity, id);
            var sql = $"Proc_Update_{className}";
            var data = Connection.Execute(sql:sql,param: entity, commandType: System.Data.CommandType.StoredProcedure);
            //trả về kết quả
            return data;
        }

        public int DeleteById<Y>(Guid id)
        {
            var className = typeof(Y).Name;
            var sql = $"DELETE FROM {className} WHERE {className}Id = @id";
            var parameters = new DynamicParameters(id);
            parameters.Add("@id", id);
            var data = Connection.Execute(sql, parameters);
            return data;
        }

        public int DeleteAny<Y>(Guid[] ids)
        {
            var className = typeof(Y).Name;
            var idsArray = "";
            var sql = $"DELETE FROM {className} WHERE {className}Id IN @ids";
            var parameters = new DynamicParameters();
            parameters.Add("@ids", ids);
            var res = Connection.Execute(sql, parameters);
            return res;
        }
    }
}
