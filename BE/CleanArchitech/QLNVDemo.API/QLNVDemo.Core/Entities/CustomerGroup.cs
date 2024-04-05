using MISA.AMISDemo.Core.Resource;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.Entities
{
    public class CustomerGroup
    {

        public Guid CustomerGroupId { get; set; }
        [Required(ErrorMessageResourceName = "CustomerGroupNameNotEmpty", ErrorMessageResourceType = typeof(MISAResource))]
        public string CustomerGroupName { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
