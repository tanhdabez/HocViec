using Core.Request;
using Core.Services.Implements;
using Core.Services.Interfaces;
using Infrastructure.Models.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HocViec.Controllers
{
    [Authorize(Roles = "Admin, Employee")]
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _UserService;
        public UserController(ILogger<UserController> logger, IUserService UserService)
        {
            _logger = logger;
            _UserService = UserService;
        }
        [HttpGet("User")]
        public async Task<IActionResult> Index()
        {
            try
            {
                var data = await _UserService.GetAllUser();
                return View(data);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while processing the request.");
                return Ok(e.Message);
            }
        }
        [HttpGet("User/ChiTietUser")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var data = await _UserService.GetUserById(id);
                return View(data);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while processing the request.");
                return Ok(e.Message);
            }
        }
        [HttpGet("User/Create")]
        public IActionResult Create()
        {
            ViewBagData();
            return View();
        }
        [HttpPost("User/Create")]
        public async Task<IActionResult> Create(CreateUserRequest request)
        {
            try
            {
                bool isAdded = await _UserService.AddUser(request);
                if (!isAdded)
                {
                    ModelState.AddModelError("Email", "Email đã được sử dụng.");
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
        [HttpGet("User/Update")]
        public async Task<IActionResult> Update(Guid id)
        {
            try
            {
                ViewBagData();
                var data = await _UserService.GetUserById(id);
                return View(data);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while processing the request.");
                return Ok(e.Message);
            }
        }
        [HttpPost("User/Update")]
        public async Task<IActionResult> Update(UserResponse request)
        {
            try
            {
                var updatedUser = await _UserService.UpdateUser(request);
                if (updatedUser == null)
                {
                    ModelState.AddModelError("Email", "Email đã được sử dụng");
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
        [HttpGet("User/Remove")]
        public async Task<IActionResult> UpdateStatus(Guid id)
        {
            try
            {
                bool isDeleted = await _UserService.UpdateStatusUser(id);
                if (!isDeleted)
                {
                    ModelState.AddModelError("", "Nhân viên không tồn tại hoặc đã bị xóa.");
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
        private void ViewBagData()
        {
            ViewBag.VaiTroList = new SelectList(Enum.GetValues(typeof(EnumRole)));
        }
    }
}
