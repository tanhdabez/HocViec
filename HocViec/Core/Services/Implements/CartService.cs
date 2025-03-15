using Core.Request;
using Core.Services.Interfaces;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    item.ImageUrl = product.AnhSanPhams.FirstOrDefault()?.ImageUrl; // Lấy ảnh đầu tiên (nếu có)
                }
            }
            return cartItems;
        }
        public int GetTotalItems()
        {
            var cart = GetCart();
            return cart.Count();
        }

        public void AddToCart(Guid productId, int quantity)
        {
            var cart = GetCart();
            var existingItem = cart.FirstOrDefault(item => item.ProductId == productId);
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                cart.Add(new CartItemRequest { ProductId = productId, Quantity = quantity });
            }
            SaveCart(cart);
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
