using AutoMapper;
using Core.Request;
using Core.Response;
using Core.Services.Interfaces;
using Infrastructure;
using Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Core.Services.Implements
{
    public class CartService : ICartService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISanPhamRepository _sanPhamRepo;
        private readonly ICartRepository _cartRepo;
        private readonly IMapper _mapper;
        private readonly string _cartCookieName = "Cart";

        public CartService(IHttpContextAccessor httpContextAccessor, AppDbContext dbContext, ICartRepository cartRepo, IMapper mapper, ISanPhamRepository sanPhamRepo)
        {
            _httpContextAccessor = httpContextAccessor;
            _cartRepo = cartRepo;
            _mapper = mapper;
            _sanPhamRepo = sanPhamRepo;
        }

        public List<CartItemRequest> GetCartFromCookie()
        {
            var cartCookie = _httpContextAccessor.HttpContext?.Request.Cookies[_cartCookieName];
            if (string.IsNullOrEmpty(cartCookie))
            {
                return new List<CartItemRequest>();
            }
            return JsonSerializer.Deserialize<List<CartItemRequest>>(cartCookie) ?? new List<CartItemRequest>();
        }

        public void SetCartToCookie(List<CartItemRequest> cartItems)
        {
            var cartJson = JsonSerializer.Serialize(cartItems);
            _httpContextAccessor.HttpContext?.Response.Cookies.Append(_cartCookieName, cartJson);
        }

        public async Task<List<CartItemRequest>> GetCart(Guid? userId = null)
        {
            if (userId.HasValue)
            {
                // Xử lý giỏ hàng cho người dùng đã đăng nhập
                var cart = await _cartRepo.GetCartItemsByUserIdAsync(userId.Value);
                if (cart == null || !cart.Any())
                {
                    return new List<CartItemRequest>();
                }
                return _mapper.Map<List<CartItemRequest>>(cart);
            }
            else
            {
                // Xử lý giỏ hàng cookie cho khách vãng lai
                var cartItems = GetCartFromCookie();
                var cartItemsWithProductInfo = new List<CartItemRequest>();

                foreach (var item in cartItems)
                {
                    var product = await _sanPhamRepo.GetSanPhamWithImagesAsync(item.ProductId);
                    if (product != null)
                    {
                        cartItemsWithProductInfo.Add(new CartItemRequest
                        {
                            ProductId = item.ProductId,
                            Quantity = item.Quantity,
                            StockQuantity = product.SoLuong,
                            ProductName = product.Ten,
                            Price = product.GiaBan,
                            ImageUrl = product.AnhSanPhams?.FirstOrDefault()?.ImageUrl
                        });
                    }
                }
                return cartItemsWithProductInfo;
            }
        }

        public async Task<int> GetTotalItems()
        {
            var cart = await GetCart(); 

            if (cart == null || !cart.Any())
            {
                return 0;
            }
            var uniqueProductIds = new HashSet<Guid>();
            foreach (var item in cart)
            {
                uniqueProductIds.Add(item.ProductId);
            }
            return uniqueProductIds.Count;
        }

        public async Task<CartItemResponse> AddToCart(Guid productId, int quantity)
        {
            var userIdString = _httpContextAccessor.HttpContext?.Session.GetString("UserId");

            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out Guid userId))
            {
                // Xử lý giỏ hàng cookie cho khách vãng lai
                var cartItems = GetCartFromCookie();
                var existingItem = cartItems.FirstOrDefault(ci => ci.ProductId == productId);

                var product = await _sanPhamRepo.GetByIdAsync(productId);
                if (product?.SoLuong < quantity)
                    return new() { Warning = true, Message = product == null ? "Sản phẩm không tồn tại." : "Số lượng không đủ." };
                int currentQuantity = existingItem != null ? existingItem.Quantity : 0;
                int totalQuantity = currentQuantity + quantity;

                if (product.SoLuong < totalQuantity)
                {
                    return new() { Warning = true, Message = $"Bạn đã có {currentQuantity} sản phẩm trong giỏ hàng. Không thể thêm {quantity} sản phẩm vì sẽ vượt quá giới hạn mua hàng của bạn." };
                }

                if (existingItem != null)
                {
                    existingItem.Quantity += quantity;
                }
                else
                {
                    cartItems.Add(new CartItemRequest
                    {
                        ProductId = productId,
                        Quantity = quantity,
                        ProductName = product.Ten,
                        Price = product.GiaBan,
                        ImageUrl = product.AnhSanPhams?.FirstOrDefault()?.ImageUrl
                    });
                }
                SetCartToCookie(cartItems);
                return new() { Success = true, Message = "Thêm vào giỏ hàng thành công.", CartItemCount = cartItems.Sum(ci => ci.Quantity) };
            }

            // Xử lý giỏ hàng cho người dùng đã đăng nhập
            var productForUser = await _sanPhamRepo.GetByIdAsync(productId);
            if (productForUser?.SoLuong < quantity)
                return new() { Warning = true, Message = productForUser == null ? "Sản phẩm không tồn tại." : "Số lượng không đủ." };

            var cart = await _cartRepo.GetCartByUserIdAsync(userId) ?? await _cartRepo.CreateCartAsync(userId);
            var cartItem = cart.CartItems?.FirstOrDefault(ci => ci.SanPhamId == productId);
            int currentQuantityForUser = cartItem != null ? cartItem.SoLuong : 0;
            int totalQuantityForUser = currentQuantityForUser + quantity;

            if (productForUser.SoLuong < totalQuantityForUser)
            {
                return new() { Warning = true, Message = $"Bạn đã có {currentQuantityForUser} sản phẩm trong giỏ hàng. Không thể thêm {quantity} sản phẩm vì sẽ vượt quá giới hạn mua hàng của bạn." };
            }
            if (cartItem != null) cartItem.SoLuong += quantity;
            else await _cartRepo.AddCartItemAsync(new() { CartId = cart.Id, SanPhamId = productId, SoLuong = quantity });

            if (cartItem != null) await _cartRepo.UpdateCartItemAsync(cartItem);

            return new() { Success = true, Message = "Thêm vào giỏ hàng thành công.", CartItemCount = cart.CartItems?.Sum(ci => ci.SoLuong) ?? 0 };
        }

        public async Task<CartItemResponse> UpdateCartItem(Guid productId, int quantity)
        {
            var userIdString = _httpContextAccessor.HttpContext?.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out Guid userId))
            {
                // Xử lý giỏ hàng cookie cho khách vãng lai
                var cartItems = GetCartFromCookie();
                var existingItem = cartItems.FirstOrDefault(ci => ci.ProductId == productId);
                if (existingItem != null)
                {
                    existingItem.Quantity = quantity;
                    SetCartToCookie(cartItems);
                    return new() { Success = true, Message = "Cập nhật giỏ hàng thành công.", CartItemCount = cartItems.Sum(ci => ci.Quantity) };
                }
                return new() { Success = false, Message = "Sản phẩm không có trong giỏ hàng." };
            }

            // Xử lý giỏ hàng cho người dùng đã đăng nhập
            var product = await _sanPhamRepo.GetByIdAsync(productId);
            if (product?.SoLuong < quantity)
                return new() { Success = false, Message = product == null ? "Sản phẩm không tồn tại." : "Số lượng không đủ." };

            var cart = await _cartRepo.GetCartByUserIdAsync(userId);
            var cartItem = cart?.CartItems?.FirstOrDefault(ci => ci.SanPhamId == productId);
            if (cartItem == null)
                return new() { Success = false, Message = cart == null ? "Giỏ hàng không tồn tại." : "Sản phẩm không có trong giỏ hàng." };

            cartItem.SoLuong = quantity;
            await _cartRepo.UpdateCartItemAsync(cartItem);

            return new() { Success = true, Message = "Cập nhật giỏ hàng thành công.", CartItemCount = cart.CartItems.Sum(ci => ci.SoLuong) };
        }

        public async Task<CartItemResponse> RemoveFromCart(Guid productId)
        {
            var userIdString = _httpContextAccessor.HttpContext?.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out Guid userId))
            {
                // Xử lý giỏ hàng cookie cho khách vãng lai
                var cartItems = GetCartFromCookie();
                cartItems.RemoveAll(ci => ci.ProductId == productId);
                SetCartToCookie(cartItems);
                return new() { Success = true, Message = "Xóa sản phẩm khỏi giỏ hàng thành công.", CartItemCount = cartItems.Sum(ci => ci.Quantity) };
            }

            // Xử lý giỏ hàng cho người dùng đã đăng nhập
            if (!await _cartRepo.RemoveCartItemAsync(productId))
                return new() { Success = false, Message = "Xóa sản phẩm thất bại." };

            var cart = await _cartRepo.GetCartByUserIdAsync(userId);

            return new()
            {
                Success = true,
                Message = "Xóa sản phẩm khỏi giỏ hàng thành công.",
                CartItemCount = cart?.CartItems?.Sum(ci => ci.SoLuong) ?? 0
            };
        }

        public async Task<CartItemResponse> ClearCart()
        {
            var userIdString = _httpContextAccessor.HttpContext?.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out Guid userId))
            {
                // Xử lý giỏ hàng cookie cho khách vãng lai
                SetCartToCookie(new List<CartItemRequest>());
                return new() { Success = true, Message = "Xóa giỏ hàng thành công.", CartItemCount = 0 };
            }

            // Xử lý giỏ hàng cho người dùng đã đăng nhập
            var cart = await _cartRepo.GetCartByUserIdAsync(userId);
            if (cart == null || cart.CartItems == null || !cart.CartItems.Any())
                return new CartItemResponse { Success = false, Message = "Giỏ hàng trống hoặc không tồn tại." };

            await _cartRepo.ClearCartAsync(cart.Id);

            return new CartItemResponse { Success = true, Message = "Xóa giỏ hàng thành công.", CartItemCount = 0 };
        }
    }
}
