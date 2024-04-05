using Microsoft.AspNetCore.Http;
using MISA.AMISDemo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.Interfaces.Base
{
    public interface IBaseRepository<T> where T : class
    {
        /// <summary>
        /// Thực hiện lấy tất cả thông tin 
        /// </summary>
        /// <returns>
        /// trả về một List danh sách thông tin
        /// </returns>
        /// Created by : TTPhong (25/12/2023)
        IEnumerable<T> GetAll();
        /// <summary>
        /// Thực hiện lấy thông tin bằng id 
        /// </summary>
        /// <returns>
        /// trả về thông tin theo id
        /// </returns>
        /// Created by : TTPhong (25/12/2023)
        T GetById(Guid id);
        /// <summary>
        /// Thực hiện thêm mới dữ liệu 
        /// </summary>
        /// <returns>
        /// trả về số lượng bản ghi được thêm
        /// </returns>
        /// Created by : TTPhong (25/12/2023)
        int Insert(T entity);
        /// <summary>
        /// Thực hiện cập nhập dữ liệu
        /// </summary>
        /// <returns>
        /// trả về số lượng bản ghi được cập nhật
        /// </returns>
        /// Created by : TTPhong (25/12/2023)
        int Update(T entity, Guid id);
        /// <summary>
        /// Thực hiện xóa thông tin bằng id 
        /// </summary>
        /// <returns>
        /// trả về số lượng bản ghi được xóa
        /// </returns>
        /// Created by : TTPhong (25/12/2023)
        int DeleteById(Guid id);
        /// <summary>
        /// Thực hiện xóa một vài chỉ định thông tin bằng id
        /// </summary>
        /// <returns>
        /// trả về số lượng bản ghi được xóa
        /// </returns>
        /// Created by : TTPhong (25/12/2023) 
        int DeleteAny(Guid[] ids);
    }
}
