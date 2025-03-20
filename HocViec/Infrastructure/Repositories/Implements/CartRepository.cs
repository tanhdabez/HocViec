using Infrastructure.Models;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Implements
{
    public class CartRepository : ICartRepository
    {
        private readonly AppDbContext _dbContext;
        public CartRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Cart> GetCartByUserIdAsync(Guid userId)
        {
            return await _dbContext.Carts.Include(c => c.CartItems).FirstOrDefaultAsync(c => c.UserId == userId);
        }

        public async Task<List<CartItem>> GetCartItemsByUserIdAsync(Guid userId)
        {
            var cart = await _dbContext.Carts.Include(c => c.CartItems).FirstOrDefaultAsync(c => c.UserId == userId);
            if (cart == null || cart.CartItems == null || !cart.CartItems.Any())
            {
                return new List<CartItem>();
            }
            var productIds = cart.CartItems.Select(ci => ci.SanPhamId).ToList();
            var sanPhams = await _dbContext.SanPhams
                .Include(sp => sp.AnhSanPhams)
                .Where(sp => productIds.Contains(sp.Id))
                .ToListAsync();
            foreach (var cartItem in cart.CartItems)
            {
                cartItem.SanPham = sanPhams.FirstOrDefault(sp => sp.Id == cartItem.SanPhamId);
            }
            return cart.CartItems.ToList();
        }
        public async Task<Cart> CreateCartAsync(Guid userId)
        {
            var cart = new Cart { UserId = userId };
            _dbContext.Carts.Add(cart);
            await _dbContext.SaveChangesAsync();
            return cart;
        }

        public async Task<CartItem> AddCartItemAsync(CartItem cartItem)
        {
            _dbContext.CartItems.Add(cartItem);
            await _dbContext.SaveChangesAsync();
            return cartItem;
        }

        public async Task<CartItem> UpdateCartItemAsync(CartItem cartItem)
        {
            _dbContext.CartItems.Update(cartItem);
            await _dbContext.SaveChangesAsync();
            return cartItem;
        }

        public async Task<bool> RemoveCartItemAsync(Guid cartItemId)
        {
            var cartItem = await _dbContext.CartItems.FirstOrDefaultAsync(x=>x.SanPhamId == cartItemId);
            if (cartItem != null)
            {
                _dbContext.CartItems.Remove(cartItem);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> ClearCartAsync(Guid cartId)
        {
            var cartItems = await _dbContext.CartItems.Where(ci => ci.CartId == cartId).ToListAsync();
            if (cartItems.Any())
            {
                _dbContext.CartItems.RemoveRange(cartItems);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public List<SanPham> GetSanPhamsByIds(List<Guid> productIds)
        {
            return _dbContext.SanPhams
            .Include(p => p.AnhSanPhams)
            .Where(p => productIds.Contains(p.Id))
            .ToList();
        }

    }
}
