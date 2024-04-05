using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.Entities
{
    /// <summary>
    /// Class PagingEntity
    /// </summary>
    /// <typeparam name="T">Class</typeparam>
    /// Created By: TTPhong(24/01/2024)
    public class PagingEntity<T> where T : class
    {
        #region Properties
        /// <summary>
        /// Tổng số trang 
        /// </summary>
        /// Created By: TTPhong(24/01/2024)
        public int TotalPage { get; set; }

        /// <summary>
        /// Tổng số bản ghi
        /// </summary>
        /// Created By: TTPhong(24/01/2024)
        public int TotalRecord { get; set; }

        /// <summary>
        /// Danh sách bản ghi 
        /// </summary>
        /// Created By: TTPhong(24/01/2024)
        public IEnumerable<T> Data { get; set; }
        #endregion
    }
}
