using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.Entities
{
    public class LoginModel
    {
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string? Email { get; set; }
    }
}
