using MISA.AMISDemo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.DTOs
{
    public class CustomerImport:Customer
    {
        public CustomerImport()
        {
            ImportErrors = new List<string>();

        }
        public List<string> ImportErrors { get; set; }
        public bool IsImported { get; set; }  
    }
}
