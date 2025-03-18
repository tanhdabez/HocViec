using AutoMapper;
using Core.Request;
using Core.Services.Implements;
using Core.Services.Interfaces;
using Infrastructure.Models.DanhMuc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HocViec.Controllers
{
    [Authorize(Roles = "Admin")]
    public class NhaCungCapController : Controller
    {
        private readonly ILogger<NhaCungCapController> _logger;
        private readonly INhaCungCapService _nhaCungCapService;
        private readonly IMapper _mapper;
        public NhaCungCapController(ILogger<NhaCungCapController> logger, INhaCungCapService NhaCungCapService, IMapper mapper)
        {
            _logger = logger;
            _nhaCungCapService = NhaCungCapService;
            _mapper = mapper;
        }
        [HttpGet("NhaCungCap")]
        public async Task<IActionResult> Index()
        {
            try
            {
                var data = await _nhaCungCapService.GetAllNhaCungCap();
                return View(data);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while processing the request.");
                return Ok(e.Message);
            }
        }
        [HttpGet("/NhaCungCap/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var data = await _nhaCungCapService.GetNhaCungCapById(id);
                return View(data);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while processing the request.");
                return Ok(e.Message);
            }
        }
        [HttpGet("NhaCungCap/Create")]
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost("NhaCungCap/Create")]
        public async Task<IActionResult> Create(CreateNhaCungCapRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            try
            {
                bool isAdded = await _nhaCungCapService.AddNhaCungCap(request);

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
        [HttpGet("NhaCungCap/Update")]
        public async Task<IActionResult> Update(Guid id)
        {
            try
            {
                var data = await _nhaCungCapService.GetNhaCungCapById(id);
                return View(data);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while processing the request.");
                return Ok(e.Message);
            }
        }
        [HttpPost("NhaCungCap/Update")]
        public async Task<IActionResult> Update(NhaCungCapResponse request)
        {
            try
            {
                var updatedNhaCungCap = await _nhaCungCapService.UpdateNhaCungCap(request);
                if (updatedNhaCungCap == null)
                {
                    return NotFound();
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
        [HttpGet("NhaCungCap/Remove")]
        public async Task<IActionResult> UpdateStatus(Guid id)
        {
            try
            {
                bool isDeleted = await _nhaCungCapService.UpdateStatusNhaCungCap(id);
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
