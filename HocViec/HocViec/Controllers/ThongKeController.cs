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
        var now = DateTime.Now;
        var fromDate = startDate ?? new DateTime(now.Year, now.Month, 1);
        var toDate = endDate ?? fromDate.AddMonths(1).AddDays(-1);

        var thongKe = await _thongKeService.GetThongKeAsync(fromDate, toDate);

        return View(thongKe);
    }
}
