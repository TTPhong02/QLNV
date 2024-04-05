
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMISDemo.Core.Entities;
using MISA.AMISDemo.Core.Interfaces.Employees;
using MISA.AMISDemo.Core.Interfaces.Excels;
using MISA.AMISDemo.Core.Services;
using MISA.AMISDemo.Infractructure.Repository;
using System.Net;

namespace MISA.AMISDemo.API.Controllers
{
    [Route("api/v1/Employees")]
    [ApiController]

    public class EmployeesController : ControllerBase
    {
        private IEmployeeRepository _employeeRepository;
        private IEmployeeService _employeeService;
        private IEmployeeExcelService _employeeExcelService;

        public EmployeesController(IEmployeeRepository repository, IEmployeeService service, IEmployeeExcelService employeeExcelService)
        {
            _employeeRepository = repository;
            _employeeService = service;
            _employeeExcelService = employeeExcelService;   
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
            var res = _employeeRepository.GetAll();
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
            var res = _employeeRepository.GetById(id);
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
            var res = _employeeRepository.DeleteById(id);
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
        [HttpDelete("ids")]
        public IActionResult DeleteAny([FromBody] Guid[] ids)
        {
            var res = _employeeRepository.DeleteAny(ids);
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
        public IActionResult Update(Employee employee, Guid id)
        {
            var res = _employeeService.UpdateService(employee, id);
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
        public IActionResult Insert(Employee employee)
        {
            var res = _employeeService.InsertService(employee);
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
        /// Tìm kiếm theo Họ tên và mã nhân viên và số điện thoại và phân trang 
        /// </summary>
        /// <param name="pageSize">số bản ghi trong 1 trang</param>
        /// <param name="pageNumber">trang hiện tại</param>
        /// <param name="searchString">chuỗi tìm kiếm</param>
        /// <returns>
        /// - StatusCode: 200 và danh sách các bản ghi, tổng số bản ghi, tổng số trang thỏa mãn điều kiện tìm kiếm
        /// </returns>
        /// Created by: TTPhong(24/01/2024)
        [HttpGet("Filter")]
        [Authorize]
        public async Task<IActionResult> Filter(int pageSize, int pageNumber, string? searchString)
        {
             var res = await _employeeRepository.Paging(pageSize, pageNumber, searchString);
            return Ok(res);
        }
        /// <summary>
        /// Lấy ra mã nhân viên lớn nhất 
        /// </summary
        /// <returns>
        /// trả về mã nhân viên lớn nhất
        /// </returns>
        /// Created by: TTPhong(24/01/2024)
        [HttpGet("GetMaxEmployeeCode")]
        public IActionResult GetMaxEmployeeCode()
        {
            var res = _employeeRepository.GetMaxEmployeeCode();
            return StatusCode(200, res);
        }
        /// <summary>
        /// Xuất file Excel nhân viên
        /// </summary
        /// <returns>
        /// trả về file excel 
        /// </returns>
        /// Created by: TTPhong(29/01/2024)
        [HttpGet("Export/Excel")]

        public IActionResult ExportEmployeeToExcel()
        {
            var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            var fileName = $"Danh sách nhân viên.xlsx";

            var res = _employeeExcelService.ExportAll();

            return File(res, contentType, fileName);
        }
        /// <summary>
        /// Thực hiện nhập khẩu nhân viên 
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost("Import")]

        public IActionResult Import(IFormFile fileImport)
        {
            var res = _employeeService.ImportEmployee(fileImport);
            return StatusCode(200, res);
        }
    }
}
