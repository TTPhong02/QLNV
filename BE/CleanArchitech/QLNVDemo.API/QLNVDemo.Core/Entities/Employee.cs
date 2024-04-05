using MISA.AMISDemo.Core.Resource;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.Entities
{
    public class Employee
    {
        public Guid EmployeeId { get; set; }
        [Required(ErrorMessageResourceName = "EmployeeCodeNotEmpty", ErrorMessageResourceType = typeof(MISAResource))]
        public string EmployeeCode { get; set; }
        [Required(ErrorMessageResourceName = "FullNameNotEmpty", ErrorMessageResourceType = typeof(MISAResource))]
        public string FullName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? Gender { get; set; }
        //[Required(ErrorMessageResourceName = "IdentityNumberNotEmpty", ErrorMessageResourceType = typeof(MISAResource))]
        public string? IdentityNumber { get; set; }
        public DateTime? IdentityDate { get; set; }
        public string?  IdentityPlace { get; set; }
        //[Required(ErrorMessageResourceName = "EmailNotEmpty", ErrorMessageResourceType = typeof(MISAResource))]
        //[EmailAddress(ErrorMessageResourceName = "InvalidEmail", ErrorMessageResourceType = typeof(MISAResource))]
        public string? Email { get; set; }
        public string?  PhoneNumber { get; set; }
        public string? PhoneNumberFixed { get; set; }
        public int?  Salary { get; set; }
        public DateTime?  JoinDate { get; set; }
        public int?  WorkStatus { get; set; }
        public string?  Address { get; set; }   
        public string?  BankAccount { get; set; }
        public string?  BankName { get; set; }
        public string?  BankAddress { get; set; }
        public Guid?  DepartmentId { get; set; }
        public Guid?  PositionsId { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
