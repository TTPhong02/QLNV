using MISA.AMISDemo.Core.Entities;
using MISA.AMISDemo.Core.Interfaces.Base;
using MISA.AMISDemo.Core.Interfaces.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.Services
{

    public class CustomerGroupService : BaseService<CustomerGroup>, ICustomerGroupService
    {

        public CustomerGroupService(IBaseRepository<CustomerGroup> repository) : base(repository)
        {

        }
    }
}
