using Core.Request;
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
        Task<List<CartItemRequest>> GetCart(Guid? userId = null);
        Task<CartResult> AddToCart(Guid productId, int quantity);
        //int GetTotalItems();
        Task<CartResult> UpdateCartItem(Guid productId, int quantity);
        Task<CartResult> RemoveFromCart(Guid productId);
        Task<CartResult> ClearCart();
    }
}
