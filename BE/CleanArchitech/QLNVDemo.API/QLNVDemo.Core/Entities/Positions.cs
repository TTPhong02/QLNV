using MISA.AMISDemo.Core.Resource;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.Entities
{
    public class Positions
    {
        public Guid PositionsId { get; set; }
        [Required(ErrorMessageResourceName = "PositionsNameNotEmpty", ErrorMessageResourceType = typeof(MISAResource))]
        public string PositionsName { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
