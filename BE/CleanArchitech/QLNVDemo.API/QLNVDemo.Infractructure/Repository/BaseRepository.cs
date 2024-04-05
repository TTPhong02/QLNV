using Dapper;
using Microsoft.AspNetCore.Http;
using MISA.AMISDemo.Core.Interfaces.Base;
using MISA.AMISDemo.Infractructure.Interfaces;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace MISA.AMISDemo.Infractructure.Repository
{
    public class BaseRepository<T>:IBaseRepository<T> where T : class
    {
       
        protected IMISADbContext _dbContext;

        public BaseRepository(IMISADbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<T> GetAll( )
        {
            var res = _dbContext.GetAll<T>();
            return res;
        }

        public T GetById(Guid id)
        {
            var res = _dbContext.GetById<T>(id);
            return res;
        }

        public int Insert(T entity)
        {
            var res = _dbContext.Insert<T>(entity);
            return res;
        }

        public int Update(T entity, Guid id)
        {
            var res = _dbContext.Update<T>(entity,id);
            return res;
        }

        public int DeleteById(Guid id)
        {
            var res = _dbContext.DeleteById<T>( id);
            return res;
        }

        public int DeleteAny(Guid[] ids)
        {
            var res = _dbContext.DeleteAny<T>(ids);
            return res;
        }
    }
}
