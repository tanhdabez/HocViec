using Core.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Interfaces
{
    public interface ICartService
    {
        List<CartItemRequest> GetCart();
        Task<CartResult> AddToCart(Guid productId, int quantity);
        int GetTotalItems();
        void UpdateCartItem(Guid productId, int quantity);
        void RemoveFromCart(Guid productId);
        void ClearCart();
    }
}
