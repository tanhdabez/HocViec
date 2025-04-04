using Core.Request;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Core.Response;

namespace HocViec.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly ICustomAuthenticationService _authenticationService;

        public AuthenticationController(ICustomAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }
        [HttpGet("Login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            var result = await _authenticationService.Login(request);
            if (!result.IsSuccess)
            {
                ModelState.AddModelError("", "Đăng nhập thất bại");
                return View(request);
            }
            HttpContext.Session.SetString("UserId", result.UserId.ToString());
            HttpContext.Session.SetString("Name", result.Name);
            HttpContext.Session.SetString("Email", result.Email);
            HttpContext.Session.SetString("Role", result.Role);

            if (result.Role == "Admin" || result.Role == "Employee")
            {
                return RedirectToAction("Index", "ThongKe");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet("Register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            var result = await _authenticationService.Register(request);

            if (!result)
            {
                ModelState.AddModelError("Email", "Email đã tồn tại.");
                return View(request);
            }

            // Tự động đăng nhập
            return await Login(new LoginRequest { Email = request.Email, Password = request.Password });
        }

        [HttpGet("Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        [Route("AccessDenied")]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
