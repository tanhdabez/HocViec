//using Core.Request;
//using Core.Services.Interfaces;
//using Microsoft.AspNetCore.Mvc;

//namespace HocViec.Controllers
//{
//    public class VaiTroController : Controller
//    {
//        private readonly ILogger<VaiTroController> _logger;
//        private readonly IVaiTroService _VaiTroService;
//        public VaiTroController(ILogger<VaiTroController> logger, IVaiTroService VaiTroService)
//        {
//            _logger = logger;
//            _VaiTroService = VaiTroService;
//        }
//        [HttpGet("DanhSachVaiTro")]
//        public async Task<IActionResult> Index()
//        {
//            try
//            {
//                var data = await _VaiTroService.GetAllVaiTro();
//                return View(data);
//            }
//            catch (Exception e)
//            {
//                _logger.LogError(e, "An error occurred while processing the request.");
//                return Ok(e.Message);
//            }
//        }
//        [HttpPost("VaiTro/Create")]
//        public async Task<IActionResult> Create(AddVaiTroRequest request)
//        {
//            if (!ModelState.IsValid)
//            {
//                return View(request);
//            }
//            try
//            {
//                bool isAdded = await _VaiTroService.AddVaiTro(request);

//                if (!isAdded)
//                {
//                    //ModelState.AddModelError("Email", "Email đã được sử dụng.");
//                    return View(request);
//                }

//                return RedirectToAction("Index");
//            }
//            catch (Exception e)
//            {
//                _logger.LogError(e, "An error occurred while processing the request.");
//                ModelState.AddModelError("", "Có lỗi xảy ra, vui lòng thử lại.");
//                return View(request);
//            }
//        }
//        [HttpGet("VaiTro/Create")]
//        public IActionResult Create()
//        {

//            return View();
//        }
//        [HttpPost("VaiTro/Update")]
//        public async Task<IActionResult> Update(UpdateVaiTroRequest request)
//        {
//            try
//            {
//                var updatedVaiTro = await _VaiTroService.UpdateVaiTro(request);
//                if (updatedVaiTro == null)
//                {
//                    //ModelState.AddModelError("Email", "Email đã được sử dụng");
//                    return View(request); // Trả lại dữ liệu nhập trước đó
//                }

//                return RedirectToAction("Index");
//            }
//            catch (Exception e)
//            {
//                _logger.LogError(e, "An error occurred while processing the request.");
//                ModelState.AddModelError("", "Có lỗi xảy ra, vui lòng thử lại.");
//                return View(request);
//            }
//        }
//        [HttpGet("VaiTro/Update")]
//        public async Task<IActionResult> Update(Guid id)
//        {
//            try
//            {
//                var data = await _VaiTroService.GetVaiTroById(id);
//                return View(data);
//            }
//            catch (Exception e)
//            {
//                _logger.LogError(e, "An error occurred while processing the request.");
//                return Ok(e.Message);
//            }
//        }
//        [HttpGet("VaiTro/Remove")]
//        public async Task<IActionResult> Remove(Guid id)
//        {
//            try
//            {
//                bool isDeleted = await _VaiTroService.DeleteVaiTro(id);
//                if (!isDeleted)
//                {
//                    ModelState.AddModelError("", "Vai trò không tồn tại hoặc đã bị xóa.");
//                    return View();
//                }

//                return RedirectToAction("Index");
//            }
//            catch (Exception e)
//            {
//                _logger.LogError(e, "An error occurred while processing the request.");
//                ModelState.AddModelError("", "Có lỗi xảy ra, vui lòng thử lại.");
//                return View();
//            }
//        }

//    }
//}
