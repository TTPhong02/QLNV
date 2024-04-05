using MISA.AMISDemo.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.Interfaces.Base
{
    public interface IBaseService<T> where T : class
    {
        /// <summary>
        /// Service thực hiện việc thêm mới 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// Created by : TTPhong (25/12/2023)
        public MISAServiceResult InsertService(T entity);
        /// <summary>
        /// Service thực hiện việc update dư liệu
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// Created by : TTPhong (25/12/2023)
        public MISAServiceResult UpdateService(T entity, Guid id);

    }
}
