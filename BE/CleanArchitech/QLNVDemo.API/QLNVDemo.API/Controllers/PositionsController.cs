using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMISDemo.Core.Entities;
using MISA.AMISDemo.Core.Interfaces;
using MISA.AMISDemo.Core.Interfaces.Position;
using System.Net;

namespace MISA.AMISDemo.API.Controllers
{
    [Route("api/v1/Positions")]
    [ApiController]
    public class PositionsController : ControllerBase
    {
        private IPositionRepository _positionRepository;
        private IPositionService _positionService;

        public PositionsController(IPositionRepository positionRepository, IPositionService positionService)
        {
            _positionRepository = positionRepository;
            _positionService = positionService;
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
            var res = _positionRepository.GetAll();
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
            var res = _positionRepository.GetById(id);
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
            var res = _positionRepository.DeleteById(id);
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
            var res = _positionRepository.DeleteAny(ids);
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
        public IActionResult Update(Positions position, Guid id)
        {
            var res = _positionService.UpdateService(position, id);
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
        public IActionResult Insert(Positions position)
        {
            var res = _positionService.InsertService(position);
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
