using Core.Auth;
using Core.Db;
using Microsoft.AspNetCore.Mvc;
using UserApi.Business;

namespace UserApi.Controllers
{
    [ApiController]
    //[ApiKeyAuth]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserBusiness _userBusiness;

        public HomeController(ILogger<HomeController> logger, UserBusiness userBusiness)
        {
            _logger = logger;
            _userBusiness = userBusiness;
        }

        [HttpGet("UserNameDatabaseAuthentication")]
        public async Task<IActionResult> UserNameDatabaseAuthentication([FromQuery] string userName, string password)
        {
            var res = await _userBusiness.UserNameDatabaseAuthentication(userName, password);
            if (res.Success) return Ok(res);
            else
            {
                _logger.LogError(res?.FirstError?.Description);
                return NotFound();
            }
        }

        [HttpPost("CreateUser")]
        public IActionResult CreateUser([FromBody] User user)
        {
            var res = _userBusiness.CreateUser(user);
            if (res.Success) return Ok(res);
            else
            {
                _logger.LogError(res?.FirstError?.Description);
                return NotFound();
            }
        }
    }
}
