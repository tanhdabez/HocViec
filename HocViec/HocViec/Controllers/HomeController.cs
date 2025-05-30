﻿using Core.Services.Implements;
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
        private readonly IUserService _userService;
        private readonly IDanhMucLoaiHangService _loaiSanPham;

        public HomeController(ILogger<HomeController> logger, ISanPhamService sanPhamService, IUserService userService, IDanhMucLoaiHangService loaiSanPham)
        {
            _logger = logger;
            _sanPhamService = sanPhamService;
            _userService = userService;
            _loaiSanPham = loaiSanPham;
        }
        public async Task<IActionResult> Index(Guid? loaiHang)
        {
            // Kiểm tra session
            if (HttpContext.Session.GetString("Name") != null)
            {
                // Người dùng đã đăng nhập
                ViewBag.Name = HttpContext.Session.GetString("Name");
                ViewBag.Role = HttpContext.Session.GetString("Role");
               
            }
            var loaiHangs = await _loaiSanPham.GetAllDanhMucLoaiHang();
            ViewBag.LoaiHangs = loaiHangs;
            var sanPhams = await _sanPhamService.GetAllSanPham_Home(loaiHang);
            return View(sanPhams);
         
        }

        [HttpGet("/chi-tiet-san-pham")]
        public async Task<IActionResult> ChiTietSanPham_Home(Guid id)
        {
            var sanPhams = await _sanPhamService.GetSanPhamById(id);
            return View(sanPhams);
        }

        [HttpGet("/DonHang")]
        public async Task<IActionResult> HoaDonByUser()
        {
            var userIdString = HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out var userId))
            {
                return RedirectToAction("Login", "Authentication");
            }

            var hoaDons = await _userService.GetHoaDonsByUserAsync(userId);
            return View(hoaDons);
        }

        [HttpGet("DonHang/ChiTiet/{id}")]
        public async Task<IActionResult> ChiTietHoaDonByUser(Guid id)
        {
            var chiTietHoaDons = await _userService.GetChiTietHoaDonsByHoaDonIdAsync(id);
            return View(chiTietHoaDons);
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
