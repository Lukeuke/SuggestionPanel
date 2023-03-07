using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SuggestionPanel.Application.Services.Authentication;
using SuggestionPanel.Domain.Enums;
using System.Security.Claims;
using SuggestionPanel.Domain.DTOs;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SuggestionPanel.UI.Controllers
{
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IConfiguration _config;

        public AuthController(IAuthService authService, IConfiguration config)
        {
            _authService = authService;
            _config = config;
        }

        public IActionResult Index()
        {
            return NotFound();
        }

        [Route("Login")]
        public ActionResult Login()
        {
            return View();
        }

        [Route("Login")]
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<ActionResult> Login(LoginRequest request)
        {
            var success = _authService.Login(request.Number, request.Password);

            if (!success)
            {
                ViewData["Error"] = "Wrong password.";
                return View();
            }

            var claims = new List<Claim>
            {
                new (ClaimTypes.Name, request.Number),
                new (ClaimTypes.Role, ERoles.ValueStreamResponsibility.ToString()),
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            var props = new AuthenticationProperties();

            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, props).Wait();

            return RedirectToAction("Index", "Suggestion", new { Number = request.Number });
        }

        [Route("Logout")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        [Route("Admin")]
        public ActionResult Admin()
        {
            ViewData["Roles"] = new SelectList(new List<string> { ERoles.Committee.ToString(), ERoles.Admin.ToString() });

            return View();
        }

        [Route("Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Admin(AdminRequest request)
        {
            var validPass = _config.GetValue<string>(request.Role + "Pass");

            if (validPass != request.Password)
            {
                ViewData["Roles"] = new SelectList(new List<string> { ERoles.Committee.ToString(), ERoles.Admin.ToString() });
                ViewData["Error"] = "Wrong password.";
                return View();
            }

            var claims = new List<Claim>
            {
                new (ClaimTypes.Role, request.Role),
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            var props = new AuthenticationProperties();

            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, props).Wait();

            return RedirectToAction("Index", "Home");
        }
    }
}
