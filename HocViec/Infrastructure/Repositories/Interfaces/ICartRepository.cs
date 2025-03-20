using Infrastructure.Models;

namespace Infrastructure.Repositories.Interfaces
{
    public interface ICartRepository
    {
        // Các hàm liên quan đến sản phẩm
        Task<Cart?> GetCartByUserIdAsync(Guid userId);
        Task<List<CartItem>> GetCartItemsByUserIdAsync(Guid userId);
        List<SanPham> GetSanPhamsByIds(List<Guid> productIds);
        Task<Cart> CreateCartAsync(Guid userId);
        Task<CartItem> AddCartItemAsync(CartItem cartItem);
        Task<CartItem> UpdateCartItemAsync(CartItem cartItem);
        Task<bool> RemoveCartItemAsync(Guid cartItemId);
        Task<bool> ClearCartAsync(Guid cartId);
    }
}
