using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMISDemo.Core.Entities;
using MISA.AMISDemo.Core.Interfaces.Customers;
using MISA.AMISDemo.Core.Services;
using MISA.AMISDemo.Infractructure.Repository;
using System.Net;

namespace MISA.AMISDemo.API.Controllers
{
    [Route("api/v1/CustomerGroups")]
    [ApiController]
    public class CustomerGroupsController : ControllerBase
    {
        private ICustomerGroupRepository _customerGroupRepository;
        private ICustomerGroupService _customerGroupService;

        public CustomerGroupsController(ICustomerGroupRepository customerGroupRepository, ICustomerGroupService customerGroupService)
        {
            _customerGroupRepository = customerGroupRepository;
            _customerGroupService = customerGroupService;
        }

        /// <summary>
        /// Hàm lấy tất cả thông tin
        /// </summary>
        /// <returns>
        /// trả về mã 200 và tất cả thông tin
        /// </returns>
        /// Created By : TTPhong(25/12/2023)
        [HttpGet]
        public IActionResult Get()
        {
            var res = _customerGroupRepository.GetAll();
            return StatusCode(200, res);
        }
        /// <summary>
        /// Hàm lấy tất cả thông tin theo Id
        /// </summary>
        /// <returns>
        /// trả về mã 200 và tất cả thông tin Id
        /// </returns>
        /// Created By : TTPhong(25/12/2023)
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var res = _customerGroupRepository.GetById(id);
            return StatusCode(200, res);
        }
        /// <summary>
        /// Hàm lấy xóa thông tin theo id
        /// </summary>
        /// <returns>
        /// trả về mã 201 
        /// trả về số lượng bản ghi được xóa
        /// </returns>
        /// Created By : TTPhong(25/12/2023)
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var res = _customerGroupRepository.DeleteById(id);
            return StatusCode(201, res);
        }
        /// <summary>
        /// Hàm lấy xóa nhiều thông tin theo danh sách id
        /// </summary>
        /// <returns>
        /// trả về mã 201 
        /// trả về số lượng bản ghi được xóa
        /// </returns>
        /// Created By : TTPhong(25/12/2023)
        [HttpDelete("{ids}")]
        public IActionResult DeleteAny([FromBody] Guid[] ids)
        {
            var res = _customerGroupRepository.DeleteAny(ids);
            return StatusCode(201, res);
        }
        /// <summary>
        /// Hàm sửa thông tin theo Id
        /// </summary>
        /// <returns>
        /// trả về mã 200
        /// trả về số lượng bản ghi được sửa
        /// </returns>
        /// Created By : TTPhong(25/12/2023)
        [HttpPut("{id}")]
        public IActionResult Update(CustomerGroup customerGroup, Guid id)
        {
            var res = _customerGroupService.UpdateService(customerGroup, id);
            return StatusCode(200, res);
        }
        /// <summary>
        /// Hàm thêm mới thông tin
        /// </summary>
        /// <returns>
        /// trả về mã 201 
        /// trả về số lượng bản ghi được thêm vào
        /// </returns>
        /// Created By : TTPhong(25/12/2023)
        [HttpPost]
        public IActionResult Insert(CustomerGroup customerGroup)
        {
            var res = _customerGroupService.InsertService(customerGroup);
            if (res.Success == true)
            {
                return StatusCode(201, res);
            }
            else
            {

                return StatusCode((int)HttpStatusCode.BadRequest, res);
            }
        }
    }
}
