using Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HocViec.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }
        public async Task<IActionResult> Index()
        {
            var userIdString = HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out Guid userId))
            {
                var cartItems = await _cartService.GetCart();
                return View(cartItems);
            }
            var userCartItems = await _cartService.GetCart(userId);
            return View(userCartItems);
        }

        public async Task<IActionResult> GetCartData()
        {
            var userIdString = HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out Guid userId))
            {
                var cartItems = await _cartService.GetCart();
                return Ok(cartItems);
            }

            var userCartItems = await _cartService.GetCart(userId);
            return Ok(userCartItems);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(Guid productId, int quantity)
        {
            var result = await _cartService.AddToCart(productId, quantity);

            if (result.Success || result.Warning)
            {
                return Ok(result); // Trả về 200 OK với kết quả thành công
            }
            else
            {
                return BadRequest(result); // Trả về 400 BadRequest với kết quả lỗi
            }

        }
        [HttpPost]
        public async Task<IActionResult> UpdateCartItem(Guid productId, int quantity)
        {
            var result = await _cartService.UpdateCartItem(productId, quantity);

            if (result.Success)
            {
                return Ok(result); // Trả về 200 OK với kết quả thành công
            }
            else
            {
                return BadRequest(result); // Trả về 400 BadRequest với kết quả lỗi
            }
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(Guid productId)
        {
            var result = await _cartService.RemoveFromCart(productId);

            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        public async Task<IActionResult> ClearCart()
        {
            var result = await _cartService.ClearCart();

            if (result.Success)
            {
                return RedirectToAction("Index", "Cart");
            }
            else
            {
                return BadRequest(result);
            }
        }
    }
}
