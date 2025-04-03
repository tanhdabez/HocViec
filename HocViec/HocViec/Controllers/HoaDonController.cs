using Core.Request;
using Core.Services.Interfaces;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
namespace HocViec.Controllers
{
    public class HoaDonController : Controller
    {
        private readonly IHoaDonService _hoaDonService;
        public HoaDonController(ILogger<HoaDonController> logger, IHoaDonService HoaDonService)
        {
            _hoaDonService = HoaDonService;
        }

        public async Task<IActionResult> Index([FromQuery] FilterRequest filter)
        {
            try
            {
               
                var result = await _hoaDonService.GetAllHoaDonAsync(filter);

                ViewBag.PageNumber = filter.PageNumber;
                ViewBag.PageSize = filter.PageSize;
                ViewBag.TotalItems = result.TotalItems;
                ViewBag.TotalPages = result.TotalPages;

                return View(result.Data);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        public async Task<IActionResult> GetHoaDonPage([FromQuery] FilterRequest filter)
        {
            try
            {
                var result = await _hoaDonService.GetAllHoaDonAsync(filter);

                ViewBag.PageNumber = filter.PageNumber;
                ViewBag.PageSize = filter.PageSize;
                ViewBag.TotalItems = result.TotalItems;
                ViewBag.TotalPages = result.TotalPages;

                return PartialView("_HoaDonTable", result.Data);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        public async Task<IActionResult> GetHoaDonDetails(Guid id)
        {
            var hoaDonChiTietDTO = await _hoaDonService.GetHoaDonChiTietAsync(id);

            if (hoaDonChiTietDTO == null)
            {
                return NotFound();
            }

            return PartialView("GetHoaDonDetails", hoaDonChiTietDTO);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateHoaDon(Guid hoaDonId, int trangThai, string ghiChu)
        {
            if (hoaDonId == Guid.Empty)
            {
                return BadRequest("Invalid HoaDonId");
            }
            if (trangThai < 0)
            {
                return BadRequest("Invalid TrangThai");
            }
            var result = await _hoaDonService.UpdateHoaDonAsync(hoaDonId, trangThai, ghiChu);
            if (result)
            {
                return Ok("HoaDon updated successfully");
            }
            else
            {
                return BadRequest("HoaDon update failed");
            }
        }
    }
}
