using Core.Extensions;
using FluentValidation;
using FluentValidation.Results;
using ImmedisWeb.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UserApi.Business;

namespace ImmedisWeb.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;
        private readonly UserBusiness _business;
        private IValidator<LoginModel> _validator;

        public LoginController(ILogger<LoginController> logger, UserBusiness business, IValidator<LoginModel> validator)
        {
            _logger = logger;
            _business = business;
            _validator = validator;
        }

        public IActionResult Index()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            LoginModel model = new LoginModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginModel model)
        {
            ValidationResult result = await _validator.ValidateAsync(model);
            if (!result.IsValid)
            {
                // Copy the validation results into ModelState.
                // ASP.NET uses the ModelState collection to populate 
                // error messages in the View.
                result.AddToModelState(this.ModelState);

                // re-render the view when validation failed.
                return View("Index", model);
            }
            else
            {
                var loginValues = await _business.UserNameDatabaseAuthentication(model.Email, model.Password);
                if (loginValues.Success && loginValues.Data != null)
                {
                    //var user = await _business.SetUserDataToSession(loginValues.Data.Id);
                    var claims = new List<Claim>  {
                     new Claim(ClaimTypes.Name, loginValues.Data.Email),
                     new Claim("user_id", loginValues.Data.Id.ToString()),
                     new Claim(ClaimTypes.Role,loginValues.Data.Type.ToString()) };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {
                        //AllowRefresh = <bool>,
                        // Refreshing the authentication session should be allowed.

                        ExpiresUtc = DateTimeOffset.UtcNow.AddHours(2),
                        //The time at which the authentication ticket expires.A
                        // value set here overrides the ExpireTimeSpan option of
                        // CookieAuthenticationOptions set with AddCookie.

                        IsPersistent = true,
                        // Whether the authentication session is persisted across 
                        // multiple requests. When used with cookies, controls
                        // whether the cookie's lifetime is absolute (matching the
                        // lifetime of the authentication ticket) or session-based.

                        //IssuedUtc = <DateTimeOffset>,
                        // The time at which the authentication ticket was issued.

                        //RedirectUri = <string>
                        // The full path or absolute URI to be used as an http 
                        // redirect response value.
                    };

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity), authProperties);

                    return RedirectToAction("Index", "Home");
                }
                return RedirectToAction("Index", "Login");
            }
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Login");
        }
    }
}
