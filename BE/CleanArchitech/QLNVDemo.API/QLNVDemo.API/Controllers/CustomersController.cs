using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMISDemo.Core.Entities;
using MISA.AMISDemo.Core.Interfaces.Customers;
using MISA.AMISDemo.Infractructure.Repository;
using System.Net;

namespace MISA.AMISDemo.API.Controllers
{
    [Route("api/v1/Customers")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private ICustomerRepository _customerRepository;
        private ICustomerService _customerService;

        public CustomersController(ICustomerRepository repository, ICustomerService service)
        {
            _customerRepository = repository;
            _customerService = service;
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
            var res = _customerRepository.GetAll();
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
            var res = _customerRepository.GetById(id);
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
            var res = _customerRepository.DeleteById(id);
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
        public IActionResult DeleteAny([FromBody]Guid[] ids)
        {
            var res = _customerRepository.DeleteAny(ids);
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
        public IActionResult Update(Customer customer, Guid id) {
            var res = _customerService.UpdateService(customer,id);
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
        public IActionResult Insert(Customer customer)
        {
                var res = _customerService.InsertService(customer);
                if (res.Success == true)
                {
                    return StatusCode(201, res);
                }
                else
                {

                    return StatusCode((int)HttpStatusCode.BadRequest, res);
                }
        }
        /// <summary>
        /// Thực hiện nhập khẩu 
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost("import")]
        public IActionResult Import(IFormFile fileImport)
        {
            var res = _customerService.ImportCustomer(fileImport);
            return StatusCode(200,res);
        }
    }
}
