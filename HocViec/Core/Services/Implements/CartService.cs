using Core.Request;
using Core.Services.Interfaces;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Core.Services.Implements
{
    public class CartService : ICartService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _cartCookieName = "Cart";
        private readonly AppDbContext _dbContext;
        public CartService(IHttpContextAccessor httpContextAccessor, AppDbContext dbContext)
        {
            _httpContextAccessor = httpContextAccessor;
            _dbContext = dbContext;
        }
        public List<CartItemRequest> GetCart()
        {
            var cartCookie = _httpContextAccessor.HttpContext.Request.Cookies[_cartCookieName];
            if (string.IsNullOrEmpty(cartCookie))
            {
                return new List<CartItemRequest>();
            }
            var cartItems = JsonConvert.DeserializeObject<List<CartItemRequest>>(cartCookie);
            if (cartItems == null || cartItems.Count == 0)
            {
                return new List<CartItemRequest>();
            }
            var productIds = cartItems.Select(item => item.ProductId).ToList();
            var products = _dbContext.SanPhams
                .Include(p => p.AnhSanPhams)
                .Where(p => productIds.Contains(p.Id))
                .ToList();
            foreach (var item in cartItems)
            {
                // Tìm sản phẩm tương ứng trong danh sách sản phẩm lấy từ database
                var product = products.FirstOrDefault(p => p.Id == item.ProductId);

                // 8. Kiểm tra nếu tìm thấy sản phẩm
                if (product != null)
                {
                    // 9. Cập nhật thông tin sản phẩm vào CartItemRequest
                    item.ProductName = product.Ten;
                    item.ProductCode = product.MaSanPham;
                    item.Price = product.GiaBan;
                    item.ImageUrl = product.AnhSanPhams?.FirstOrDefault()?.ImageUrl;
                    item.StockQuantity = product.SoLuong;
                }
            }
            return cartItems;
        }
        public int GetTotalItems()
        {
            var cart = GetCart();
            return cart.Count();
        }

        public async Task<CartResult> AddToCart(Guid productId, int quantity)
        {
            var cart = GetCart();
            var existingItem = cart.FirstOrDefault(item => item.ProductId == productId);
            var productInStock = await _dbContext.SanPhams.Where(x => x.Id == productId).Select(x => x.SoLuong).FirstOrDefaultAsync();
            // Kiểm tra số lượng sản phẩm trong kho
            if (productInStock <= 0)
            {
                return new CartResult { Success = false, Message = "Sản phẩm đã hết hàng." };
            }
            if (existingItem != null)
            {
                int totalQuantity = existingItem.Quantity + quantity;
                if (totalQuantity > productInStock)
                {
                    return new CartResult { Success = false, Message = $"Bạn đã có {existingItem.Quantity} sản phẩm trong giỏ hàng. Không thể thêm số lượng đã chọn vào giỏ hàng vì sẽ vượt quá giới hạn mua hàng của bạn." };
                }
                // Cập nhật số lượng sản phẩm trong giỏ hàng
                existingItem.Quantity = totalQuantity;
            }
            else
            {
                // Kiểm tra xem số lượng thêm vào có vượt quá số lượng trong kho không
                if (quantity > productInStock)
                {
                    return new CartResult { Success = false, Message = $"Số lượng sản phẩm trong kho không đủ. Số lượng hiện còn: {productInStock}" };
                }

                // Thêm sản phẩm mới vào giỏ hàng
                cart.Add(new CartItemRequest { ProductId = productId, Quantity = quantity });
            }
            SaveCart(cart);
            return new CartResult { Success = true, Message = "Thêm sản phẩm vào giỏ hàng thành công." };

        }
        public void UpdateCartItem(Guid productId, int quantity)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(item => item.ProductId == productId);
            if (item != null)
            {
                item.Quantity = quantity;
                SaveCart(cart);
            }
        }
        public void RemoveFromCart(Guid productId)
        {
            var cart = GetCart();
            cart.RemoveAll(item => item.ProductId == productId);
            SaveCart(cart);
        }
        public void ClearCart()
        {
            SaveCart(new List<CartItemRequest>());
        }
        private void SaveCart(List<CartItemRequest> cart)
        {
            var cartJson = JsonConvert.SerializeObject(cart);
            _httpContextAccessor.HttpContext.Response.Cookies.Append(_cartCookieName, cartJson, new CookieOptions
            {
                Expires = System.DateTimeOffset.Now.AddDays(30) // Lưu trong 30 ngày
            });
        }
    }
}
