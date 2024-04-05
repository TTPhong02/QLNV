using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Infractructure.Interfaces
{
    public interface IMISADbContext
    {
        IDbConnection Connection { get; }
        IDbTransaction Transaction { get; set; }
        /// <summary>
        /// /// Thực hiện lấy tất cả thông tin 
        /// </summary>
        /// <returns>
        /// trả về tất cả thông tin 
        /// </summary>
        /// <typeparam name="Y"></typeparam>
        /// <returns></returns>
        /// Created by : TTPhong (25/12/2023)
        IEnumerable<Y> GetAll<Y>();
        /// <summary>
        /// Thực hiện lấy thông tin bằng id 
        /// </summary>
        /// <returns>
        /// trả về thông tin theo id
        /// </returns>
        /// Created by : TTPhong (25/12/2023)
        Y? GetById<Y>(Guid id);
        /// <summary>
        /// Thực hiện thêm mới dữ liệu 
        /// </summary>
        /// <returns>
        /// trả về số lượng bản ghi được thêm
        /// </returns>
        /// Created by : TTPhong (25/12/2023)
        int Insert<Y>(Y entity);
        /// <summary>
        /// Thực hiện cập nhập dữ liệu
        /// </summary>
        /// <returns>
        /// trả về số lượng bản ghi được cập nhật
        /// </returns>
        /// Created by : TTPhong (25/12/2023)
        int Update<Y>(Y entity, Guid id);
        /// <summary>
        /// Thực hiện xóa thông tin bằng id 
        /// </summary>
        /// <returns>
        /// trả về số lượng bản ghi được xóa
        /// </returns>
        /// Created by : TTPhong (25/12/2023)
        int DeleteById<Y>(Guid id);
        /// <summary>
        /// Thực hiện xóa một vài chỉ định
        /// </summary>
        /// <returns>
        /// trả về số lượng bản ghi được xóa
        /// </returns>
        /// Created by : TTPhong (25/12/2023)
        int DeleteAny<Y>(Guid[] ids);
    }
}
