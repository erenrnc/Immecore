using Core.Auth;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using PeopleApi.Business;

namespace PeopleApi.Controllers
{
    [ApiController]
    //[ApiKeyAuth]
    [Route("department")]
    public class DepartmentController : ControllerBase
    {
        private readonly ILogger<DepartmentController> _logger;
        private readonly DepartmentBusiness _departmentBusiness;

        public DepartmentController(ILogger<DepartmentController> logger, DepartmentBusiness departmentBusiness)
        {
            _logger = logger;
            _departmentBusiness = departmentBusiness;
        }

        [HttpGet("GetByIdDepartment")]
        public async Task<IActionResult> GetById([FromQuery] int Id)
        {
            var res = await _departmentBusiness.GetById(Id);
            if (res.Success) return Ok(res);
            else
            {
                _logger.LogError(res?.FirstError?.Description);
                return NotFound();
            }
        }

        [HttpGet("GetListDepartment")]
        public async Task<IActionResult> GetList()
        {
            var res = await _departmentBusiness.GetList();
            if (res.Success) return Ok(res);
            else
            {
                _logger.LogError(res?.FirstError?.Description);
                return NotFound();
            }
        }

        [HttpGet("DeleteDepartment")]
        public async Task<IActionResult> Delete([FromQuery] int Id)
        {
            var res = await _departmentBusiness.Delete(Id);
            if (res.Success) return Ok(res);
            else
            {
                _logger.LogError(res?.FirstError?.Description);
                return NotFound();
            }
        }

        [HttpPost("InsertDepartment")]
        public async Task<IActionResult> Insert([FromBody] DepartmentDto dto)
        {
            var res = await _departmentBusiness.Insert(dto);
            if (res.Success) return Ok(res);
            else
            {
                _logger.LogError(res?.FirstError?.Description);
                return NotFound();
            }
        }

        [HttpPut("UpdateDepartment")]
        public async Task<IActionResult> Update([FromBody] DepartmentDto dto)
        {
            var res = await _departmentBusiness.Update(dto);
            if (res.Success) return Ok(res);
            else
            {
                _logger.LogError(res?.FirstError?.Description);
                return NotFound();
            }
        }
    }
}
