using Core.Request;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HocViec.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DanhMucLoaiHangController : Controller
    {
        private readonly ILogger<DanhMucLoaiHangController> _logger;
        private readonly IDanhMucLoaiHangService _DanhMucLoaiHangService;
        public DanhMucLoaiHangController(ILogger<DanhMucLoaiHangController> logger, IDanhMucLoaiHangService DanhMucLoaiHangService)
        {
            _logger = logger;
            _DanhMucLoaiHangService = DanhMucLoaiHangService;
        }
        [HttpGet("DanhMucLoaiHang")]
        public async Task<IActionResult> Index()
        {
            try
            {
                var data = await _DanhMucLoaiHangService.GetAllDanhMucLoaiHang();
                return View(data);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while processing the request.");
                return Ok(e.Message);
            }
        }
        [HttpGet("DanhMucLoaiHang/ChiTietDanhMucLoaiHang")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var data = await _DanhMucLoaiHangService.GetDanhMucLoaiHangById(id);
                return View(data);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while processing the request.");
                return Ok(e.Message);
            }
        }
        [HttpPost("DanhMucLoaiHang/Create")]
        public async Task<IActionResult> Create(CreateDanhMucLoaiHangRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            try
            {
                bool isAdded = await _DanhMucLoaiHangService.AddDanhMucLoaiHang(request);

                if (!isAdded)
                {
                    //ModelState.AddModelError("Email", "Email đã được sử dụng.");
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
        [HttpGet("DanhMucLoaiHang/Create")]
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost("DanhMucLoaiHang/Update")]
        public async Task<IActionResult> Update(DanhMucLoaiHangResponse request)
        {
            try
            {
                var updatedDanhMucLoaiHang = await _DanhMucLoaiHangService.UpdateDanhMucLoaiHang(request);
                if (updatedDanhMucLoaiHang == null)
                {
                    //ModelState.AddModelError("Email", "Email đã được sử dụng");
                    return View(request); // Trả lại dữ liệu nhập trước đó
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
        [HttpGet("DanhMucLoaiHang/Update")]
        public async Task<IActionResult> Update(Guid id)
        {
            try
            {
                var data = await _DanhMucLoaiHangService.GetDanhMucLoaiHangById(id);
                return View(data);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while processing the request.");
                return Ok(e.Message);
            }
        }
        [HttpGet("DanhMucLoaiHang/UpdateStatus")]
        public async Task<IActionResult> UpdateStatus(Guid id)
        {
            try
            {
                bool isDeleted = await _DanhMucLoaiHangService.UpdateStatusDanhMucLoaiHang(id);
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
    }
}
