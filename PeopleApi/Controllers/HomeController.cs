using Core.Auth;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using PeopleApi.Business;

namespace PeopleApi.Controllers
{
    [ApiController]
    //[ApiKeyAuth]
    [Route("home")]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        private readonly PeopleBusiness _peopleBusiness;

        public HomeController(ILogger<HomeController> logger, PeopleBusiness peopleBusiness)
        {
            _logger = logger;
            _peopleBusiness = peopleBusiness;
        }

        [HttpGet("GetByIdPeople")]
        public async Task<IActionResult> GetById([FromQuery] int Id)
        {
            var res = await _peopleBusiness.GetById(Id);
            if (res.Success) return Ok(res);
            else
            {
                _logger.LogError(res?.FirstError?.Description);
                return NotFound();
            }
        }

        [HttpGet("GetListPeople")]
        public async Task<IActionResult> GetList()
        {
            var res = await _peopleBusiness.GetList();
            if (res.Success) return Ok(res);
            else
            {
                _logger.LogError(res?.FirstError?.Description);
                return NotFound();
            }
        }

        [HttpGet("DeletePeople")]
        public async Task<IActionResult> Delete([FromQuery] int Id)
        {
            var res = await _peopleBusiness.Delete(Id);
            if (res.Success) return Ok(res);
            else
            {
                _logger.LogError(res?.FirstError?.Description);
                return NotFound();
            }
        }

        [HttpPost("InsertPeople")]
        public async Task<IActionResult> Insert([FromBody] PeopleDto dto)
        {
            var res = await _peopleBusiness.Insert(dto);
            if (res.Success) return Ok(res);
            else
            {
                _logger.LogError(res?.FirstError?.Description);
                return NotFound();
            }
        }

        [HttpPut("UpdatePeople")]
        public async Task<IActionResult> Update([FromBody] PeopleDto dto)
        {
            var res = await _peopleBusiness.Update(dto);
            if (res.Success) return Ok(res);
            else
            {
                _logger.LogError(res?.FirstError?.Description);
                return NotFound();
            }
        }
    }
}
