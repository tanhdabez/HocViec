using Core.Request;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize(Roles = "Admin")]
public class ThongKeController : Controller
{
    private readonly IThongKeService _thongKeService;

    public ThongKeController(IThongKeService thongKeService)
    {
        _thongKeService = thongKeService;
    }

    [HttpGet("ThongKe")]
    public async Task<IActionResult> Index(DateTime? startDate, DateTime? endDate)
    {
        // Mặc định, nếu không truyền, lấy đầu tháng và cuối tháng hiện tại
        var now = DateTime.Now;
        var defaultStart = new DateTime(now.Year, now.Month, 1);
        var defaultEnd = defaultStart.AddMonths(1).AddDays(-1);

        var fromDate = startDate ?? defaultStart;
        var toDate = endDate ?? defaultEnd;

        // Gọi Service
        var soLuongHangBan = await _thongKeService.GetSoLuongHangHoaBanAsync(fromDate, toDate);
        var soLuongHangConLai = await _thongKeService.GetSoLuongHangHoaConLaiAsync();
        var soLuongHoaDon = await _thongKeService.GetSoLuongHoaDonBanAsync(fromDate, toDate);

        // Có thể tạo ViewModel để đóng gói các số liệu
        var thongKeViewModel = new ThongKeRequest
        {
            StartDate = fromDate,
            EndDate = toDate,
            SoLuongHangBan = soLuongHangBan,
            SoLuongHangConLai = soLuongHangConLai,
            SoLuongHoaDon = soLuongHoaDon
        };

        return View(thongKeViewModel);
    }
}
