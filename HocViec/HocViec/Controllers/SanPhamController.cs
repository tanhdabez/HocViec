using Core.Request;
using Core.Services.Implements;
using Core.Services.Interfaces;
using Infrastructure.Migrations;
using Infrastructure.Models.DanhMuc;
using Microsoft.AspNetCore.Mvc;

namespace HocViec.Controllers
{
    public class SanPhamController : Controller
    {
        private readonly ILogger<SanPhamController> _logger;
        private readonly ISanPhamService _SanPhamService;
        private readonly INhaCungCapService _nhaCungCapService;
        private readonly IDanhMucLoaiHangService _danhMucLoaiHangService;
        public SanPhamController(ILogger<SanPhamController> logger, ISanPhamService SanPhamService, INhaCungCapService nhaCungCapService, IDanhMucLoaiHangService danhMucLoaiHangService)
        {
            _logger = logger;
            _SanPhamService = SanPhamService;
            _nhaCungCapService = nhaCungCapService;
            _danhMucLoaiHangService = danhMucLoaiHangService;
        }
        [HttpGet("SanPham")]
        public async Task<IActionResult> Index()
        {
            try
            {
                var data = await _SanPhamService.GetAllSanPham();
                return View(data);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while processing the request.");
                return Ok(e.Message);
            }
        }
        [HttpGet("SanPham/ChiTietSanPham")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var data = await _SanPhamService.GetSanPhamById(id);
                return View(data);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while processing the request.");
                return Ok(e.Message);
            }
        }
        [HttpGet("SanPham/Create")]
        public async Task<IActionResult> Create()
        {
            await ViewBagData();
            return View();
        }
        [HttpPost("SanPham/Create")]
        public async Task<IActionResult> Create(AddSanPhamRequest request)
        {
            try
            {
                bool isAdded = await _SanPhamService.AddSanPham(request);

                if (!isAdded)
                {
                    return View(request);
                }

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while processing the request.");
                ModelState.AddModelError("", "Có lỗi xảy ra, vui lòng thử lại.");
                return View(request);
            }
        }
        [HttpGet("SanPham/Update")]
        public async Task<IActionResult> Update(Guid id)
        {
            try
            {
                await ViewBagData();
                var data = await _SanPhamService.GetSanPhamById(id);
                return View(data);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while processing the request.");
                return Ok(e.Message);
            }
        }
        [HttpPost("SanPham/Update")]
        public async Task<IActionResult> Update(SanPhamResponse request)
        {
            try
            {
                var updatedSanPham = await _SanPhamService.UpdateSanPham(request);
                if (updatedSanPham == null)
                {
                    return View(request);
                }

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while processing the request.");
                ModelState.AddModelError("", "Có lỗi xảy ra, vui lòng thử lại.");
                return View(request);
            }
        }
        [HttpPost]
        public async Task<IActionResult> DeleteAnhSanPham(Guid imageId, Guid sanPhamId)
        {
            var result = await _SanPhamService.DeleteAnhSanPham(imageId, sanPhamId);
            if (result)
            {
                return Ok(); 
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpGet("SanPham/Remove")]
        public async Task<IActionResult> UpdateStatus(Guid id)
        {
            try
            {
                bool isDeleted = await _SanPhamService.UpdateStatus(id);
                if (!isDeleted)
                {
                    ModelState.AddModelError("", "Vai trò không tồn tại hoặc đã bị xóa.");
                    return View();
                }

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while processing the request.");
                ModelState.AddModelError("", "Có lỗi xảy ra, vui lòng thử lại.");
                return View();
            }
        }
        private async Task ViewBagData()
        {
            ViewBag.NhaCungCaps = await _nhaCungCapService.GetAllNhaCungCap();
            ViewBag.DanhMucLoaiHangs = await _danhMucLoaiHangService.GetAllDanhMucLoaiHang();
        }
    }
}
