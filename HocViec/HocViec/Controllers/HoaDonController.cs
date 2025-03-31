using Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using X.PagedList.Extensions;
namespace HocViec.Controllers
{
    public class HoaDonController : Controller
    {
        private readonly IHoaDonService _hoaDonService;
        public HoaDonController(ILogger<HoaDonController> logger, IHoaDonService HoaDonService)
        {
            _hoaDonService = HoaDonService;
        }

        public async Task<IActionResult> Index(int? page, DateTime? startDate, DateTime? endDate, int? trangThai)
        {
            try
            {
                int pageSize = 6;
                int pageNumber = (page ?? 1);

                var danhSachHoaDon = await _hoaDonService.GetAllHoaDonAsync(startDate, endDate, trangThai);
                // Tính tổng tiền
                decimal? tongTienTatCa = danhSachHoaDon.Sum(hd => hd.TongTien);

                // Truyền tổng tiền vào view
                ViewBag.TongTienTatCa = tongTienTatCa;

                var hoaDonPagedList = danhSachHoaDon.ToPagedList(pageNumber, pageSize);
                return View(hoaDonPagedList);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //public async Task<IActionResult> Details(int id)
        //{
        //    try
        //    {
        //        var hoaDon = await _hoaDonService.GetHoaDonByIdAsync(id);

        //        if (hoaDon == null)
        //        {
        //            return NotFound();
        //        }

        //        return View(hoaDon);
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e.Message);
        //    }
        //}

        public async Task<IActionResult> AjaxPagination(int? page, DateTime? startDate, DateTime? endDate, int? trangThai)
        {
            int pageSize = 6;
            int pageNumber = page ?? 1;

            var danhSachHoaDon = await _hoaDonService.GetAllHoaDonAsync(startDate, endDate, trangThai);
            var pagedList = danhSachHoaDon.ToPagedList(pageNumber, pageSize);
            // Tính tổng tiền
            decimal? tongTienTatCa = danhSachHoaDon.Sum(hd => hd.TongTien);

            // Truyền tổng tiền vào view
            ViewBag.TongTienTatCa = tongTienTatCa;
            return PartialView("_HoaDonPartial", pagedList);
        }

    }
}
