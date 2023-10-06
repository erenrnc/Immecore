using Core.Auth;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using PeopleApi.Business;

namespace PeopleApi.Controllers
{
    [ApiController]
    //[ApiKeyAuth]
    [Route("salary")]
    public class SalaryController : ControllerBase
    {
        private readonly ILogger<SalaryController> _logger;
        private readonly SalaryBusiness _salaryBusiness;

        public SalaryController(ILogger<SalaryController> logger, SalaryBusiness salaryBusiness)
        {
            _logger = logger;
            _salaryBusiness = salaryBusiness;
        }

        [HttpGet("GetByIdSalary")]
        public async Task<IActionResult> GetById([FromQuery] int Id)
        {
            var res = await _salaryBusiness.GetById(Id);
            if (res.Success) return Ok(res);
            else
            {
                _logger.LogError(res?.FirstError?.Description);
                return NotFound();
            }
        }

        [HttpGet("GetListSalary")]
        public async Task<IActionResult> GetList([FromQuery] int peopleId)
        {
            var res = await _salaryBusiness.GetList(peopleId);
            if (res.Success) return Ok(res);
            else
            {
                _logger.LogError(res?.FirstError?.Description);
                return NotFound();
            }
        }

        [HttpGet("DeleteSalary")]
        public async Task<IActionResult> Delete([FromQuery] int Id)
        {
            var res = await _salaryBusiness.Delete(Id);
            if (res.Success) return Ok(res);
            else
            {
                _logger.LogError(res?.FirstError?.Description);
                return NotFound();
            }
        }

        [HttpPost("InsertSalary")]
        public async Task<IActionResult> Insert([FromBody] SalaryDto dto)
        {
            var res = await _salaryBusiness.Insert(dto);
            if (res.Success) return Ok(res);
            else
            {
                _logger.LogError(res?.FirstError?.Description);
                return NotFound();
            }
        }

        [HttpPut("UpdateSalary")]
        public async Task<IActionResult> Update([FromBody] SalaryDto dto)
        {
            var res = await _salaryBusiness.Update(dto);
            if (res.Success) return Ok(res);
            else
            {
                _logger.LogError(res?.FirstError?.Description);
                return NotFound();
            }
        }
    }
}
