using Core.Request;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HocViec.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SanPhamController : Controller
    {
        private readonly ILogger<SanPhamController> _logger;
        private readonly ISanPhamService _sanPhamService;
        private readonly INhaCungCapService _nhaCungCapService;
        private readonly IDanhMucLoaiHangService _danhMucLoaiHangService;
        public SanPhamController(ILogger<SanPhamController> logger, ISanPhamService SanPhamService, INhaCungCapService nhaCungCapService, IDanhMucLoaiHangService danhMucLoaiHangService)
        {
            _logger = logger;
            _sanPhamService = SanPhamService;
            _nhaCungCapService = nhaCungCapService;
            _danhMucLoaiHangService = danhMucLoaiHangService;
        }

        [HttpGet("SanPham")]
        public async Task<IActionResult> Index()
        {
            try
            {
                var data = await _sanPhamService.GetAllSanPham();
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
                var data = await _sanPhamService.GetSanPhamById(id);
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
                await ViewBagData();

                bool isAdded = await _sanPhamService.AddSanPham(request);

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
                var data = await _sanPhamService.GetSanPhamById(id);
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
                var updatedSanPham = await _sanPhamService.UpdateSanPham(request);
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

      
        [HttpGet("SanPham/Remove")]
        public async Task<IActionResult> UpdateStatus(Guid id)
        {
            try
            {
                bool isDeleted = await _sanPhamService.UpdateStatus(id);
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

        [HttpPost("SanPham/DeleteImage")]
        public async Task<IActionResult> DeleteImage(string imageUrl)
        {
            try
            {
                var result = await _sanPhamService.DeleteImageAsync(imageUrl);
                if (!result)
                {
                    return NotFound("Không tìm thấy ảnh hoặc xoá thất bại.");
                }

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the image.");
                return StatusCode(500, "Đã xảy ra lỗi khi xoá ảnh.");
            }
        }

        private async Task ViewBagData()
        {
            ViewBag.NhaCungCaps = await _nhaCungCapService.GetAllNhaCungCap();
            ViewBag.DanhMucLoaiHangs = await _danhMucLoaiHangService.GetAllDanhMucLoaiHang();
        }

    }
}
