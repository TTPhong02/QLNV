using MISA.AMISDemo.Core.Resource;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.Entities
{
    public class Department
    {
        public Guid DepartmentId { get; set; }
        [Required(ErrorMessageResourceName = "DepartmentNameNotEmpty", ErrorMessageResourceType = typeof(MISAResource))]
        public string DepartmentName { get; set; }
        [Required(ErrorMessageResourceName = "DepartmentCodeNotEmpty", ErrorMessageResourceType = typeof(MISAResource))]
        public string DepartmentCode { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
