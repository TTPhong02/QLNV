using MISA.AMISDemo.Core.DTOs;
using MISA.AMISDemo.Core.Interfaces.Base;
using MISA.AMISDemo.Core.Interfaces.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.Services
{
    public class BaseService<T> : IBaseService<T> where T : class
    {
        //BaseRepository<T> repository;
        protected IBaseRepository<T> repository;
        public BaseService(IBaseRepository<T> repository)
        {
            this.repository = repository;
        }

        public BaseService(ICustomerRepository repository1)
        {
        }

        public virtual MISAServiceResult InsertService(T entity)
        {
            ValidateObject(entity);
            var res = repository.Insert(entity);
            //ProcessAfterInsert(entity);
            return new MISAServiceResult{ 
                Success = true,
                Data = res,
                StatusCode = System.Net.HttpStatusCode.Created,
                Errors= null
            };

        }

        public virtual MISAServiceResult UpdateService(T entity, Guid id)
        {
            ValidateObject(entity);
            var res = repository.Update(entity,id);
            //ProcessAfterInsert(entity);
            return new MISAServiceResult
            {
                Success = true,
                Data = res,
                StatusCode = System.Net.HttpStatusCode.OK,
                Errors = null
            };
        }
        protected virtual void ValidateObject(T entity)
        {

        }
    }
}
