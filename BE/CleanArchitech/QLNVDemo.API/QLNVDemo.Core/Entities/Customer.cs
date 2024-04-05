using MISA.AMISDemo.Core.CustomValidation;
using MISA.AMISDemo.Core.Resource;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.Entities
{
    public class Customer
    {
        public Guid CustomerId { get; set; }
        public Guid? CustomerGroupId { get; set; }
        [Required(ErrorMessageResourceName  = "CustomerCodeNotEmpty",ErrorMessageResourceType = typeof(MISAResource))]
        public string CustomerCode { get; set; }
        [Required(ErrorMessageResourceName = "FullNameNotEmpty", ErrorMessageResourceType = typeof(MISAResource))]
        public string FullName { get; set; }
        public string? PhoneNumber { get; set; }
        [EmailAddress(ErrorMessageResourceName = "InvalidEmail", ErrorMessageResourceType = typeof(MISAResource))]
        [Required(ErrorMessageResourceName = "EmailNotEmpty", ErrorMessageResourceType = typeof(MISAResource))]
        public string Email { get; set; }
        public int? Gender { get; set; }
        public string? Address { get; set; }
        [DateGreaterThanToday(ErrorMessageResourceName = "DateGreaterThanToday", ErrorMessageResourceType = typeof(MISAResource))]
        public DateTime? DateOfBirth { get; set; }
        public int? DebitAmount { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

    }
}
