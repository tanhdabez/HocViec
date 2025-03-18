using Core.Services.Interfaces;
using HocViec.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HocViec.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISanPhamService _sanPhamService;
        public HomeController(ILogger<HomeController> logger, ISanPhamService sanPhamService)
        {
            _logger = logger;
            _sanPhamService = sanPhamService;
        }
        public async Task<IActionResult> Index()
        {
            // Kiểm tra session
            if (HttpContext.Session.GetString("Name") != null)
            {
                // Người dùng đã đăng nhập
                ViewBag.Name = HttpContext.Session.GetString("Name");
                ViewBag.Role = HttpContext.Session.GetString("Role");
                var sanPhams = await _sanPhamService.GetAllSanPham();
                return View(sanPhams);
            }
            else
            {
                var sanPhams = await _sanPhamService.GetAllSanPham();
                return View(sanPhams);
            }
        }
        [HttpGet("/chi-tiet-san-pham")]
        public async Task<IActionResult> ChiTietSanPham_Home(Guid id)
        {
            var sanPhams = await _sanPhamService.GetSanPhamById(id);
            return View(sanPhams);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
