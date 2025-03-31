using Core.Request;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace YourNamespace.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly ICheckoutService _checkoutService;

        public CheckoutController(ICheckoutService checkoutService)
        {
            _checkoutService = checkoutService;
        }

        // Hiển thị form nhập thông tin thanh toán
        public async Task<IActionResult> CheckOut(string requests)
        {
            List<CheckOutDetailsDto> requestList;
            try
            {
                requestList = JsonSerializer.Deserialize<List<CheckOutDetailsDto>>(requests);
            }
            catch (JsonException)
            {
                return BadRequest("Invalid JSON data.");
            }

            var viewModel = await _checkoutService.GetSanPhamsByIdsAsync(requestList);
            return View(viewModel);
        }

        // Xử lý thông tin thanh toán và tạo hóa đơn
        [HttpPost]
        public async Task<IActionResult> ProcessCheckout(CheckOutRequest request)
        {
            Guid? userId = null; // Khởi tạo userId có thể null

            var userIdString = HttpContext.Session.GetString("UserId");

            if (!string.IsNullOrEmpty(userIdString) && Guid.TryParse(userIdString, out Guid parsedUserId))
            {
                userId = parsedUserId; // Nếu có userId hợp lệ, gán giá trị
            }

            try
            {
                var hoaDon = await _checkoutService.CreateHoaDonAsync(request, userId);
                return RedirectToAction("Success", "Authentication"); // Hoặc trang thành công khác
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        // Trang thông báo đặt hàng thành công
        public IActionResult OrderSuccess(Guid id)
        {
            ViewBag.HoaDonId = id;
            return View();
        }
    }
}