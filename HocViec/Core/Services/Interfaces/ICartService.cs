using Core.Request;
using Core.Response;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Interfaces
{
    public interface ICartService
    {
        List<CartItemRequest> GetCartFromCookie();
        public void SetCartToCookie(List<CartItemRequest> cartItems);
        Task<List<CartItemRequest>> GetCart(Guid? userId = null);
        Task<CartItemResponse> AddToCart(Guid productId, int quantity);
        Task<int> GetTotalItems();
        Task<CartItemResponse> UpdateCartItem(Guid productId, int quantity);
        Task<CartItemResponse> RemoveFromCart(Guid productId);
        Task<CartItemResponse> ClearCart();
    }
}
