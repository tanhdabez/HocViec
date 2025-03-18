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
        public IActionResult Index()
        {
            var cart = _cartService.GetCart();
            return View(cart);
        }
        public IActionResult GetCartData()
        {
            var cart = _cartService.GetCart();
            return Json(cart);
        }
        public JsonResult GetCartItemCount()
        {
            var count = _cartService.GetTotalItems();
            return Json(new { count });
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(Guid productId, int quantity)
        {
            if (quantity <= 0)
            {
                return BadRequest(new { success = false, message = "Số lượng không hợp lệ." });
            }
            var result = await _cartService.AddToCart(productId, quantity);
            if (result.Success)
            {
                return Ok(new { success = true, message = result.Message });
            }
            else
            {
                return Ok(new { error = false, message = result.Message });
            }

        }
        [HttpPost]
        public IActionResult UpdateCartItem(Guid productId, int quantity)
        {
            _cartService.UpdateCartItem(productId, quantity);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult RemoveFromCart(Guid productId)
        {
            _cartService.RemoveFromCart(productId);
            return RedirectToAction("Index");
        }

        public IActionResult ClearCart()
        {
            _cartService.ClearCart();
            return RedirectToAction("Index");
        }
    }
}
