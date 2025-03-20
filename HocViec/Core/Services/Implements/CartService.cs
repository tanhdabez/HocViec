using AutoMapper;
using Core.Request;
using Core.Services.Interfaces;
using Infrastructure;
using Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

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
        //public List<CartItemRequest> GetCart()
        //{
        //    var cartCookie = _httpContextAccessor.HttpContext.Request.Cookies[_cartCookieName];
        //    if (string.IsNullOrEmpty(cartCookie))
        //    {
        //        return new List<CartItemRequest>();
        //    }
        //    var cartItems = JsonConvert.DeserializeObject<List<CartItemRequest>>(cartCookie);
        //    if (cartItems == null || cartItems.Count == 0)
        //    {
        //        return new List<CartItemRequest>();
        //    }
        //    var productIds = cartItems.Select(item => item.ProductId).ToList();
        //    var products = _cartRepo.GetSanPhamsByIds(productIds);
        //    foreach (var item in cartItems)
        //    {
        //        var product = products.FirstOrDefault(p => p.Id == item.ProductId);

        //        if (product != null)
        //        {
        //            item.ProductName = product.Ten;
        //            item.ProductCode = product.MaSanPham;
        //            item.Price = product.GiaBan;
        //            item.ImageUrl = product.AnhSanPhams?.FirstOrDefault()?.ImageUrl;
        //            item.StockQuantity = product.SoLuong;
        //        }
        //    }
        //    return cartItems;
        //}


        //public int GetTotalItems()
        //{
        //    var cart = GetCart();
        //    return cart.Count();
        //}

        //public async Task<CartResult> AddToCart(Guid productId, int quantity)
        //{
        //    var cart = GetCart();
        //    var existingItem = cart.FirstOrDefault(item => item.ProductId == productId);
        //    var productInStock = await _dbContext.SanPhams.Where(x => x.Id == productId).Select(x => x.SoLuong).FirstOrDefaultAsync();
        //    // Kiểm tra số lượng sản phẩm trong kho
        //    if (productInStock <= 0)
        //    {
        //        return new CartResult { Success = false, Message = "Sản phẩm đã hết hàng." };
        //    }
        //    if (existingItem != null)
        //    {
        //        int totalQuantity = existingItem.Quantity + quantity;
        //        if (totalQuantity > productInStock)
        //        {
        //            return new CartResult { Success = false, Message = $"Bạn đã có {existingItem.Quantity} sản phẩm trong giỏ hàng. Không thể thêm số lượng đã chọn vào giỏ hàng vì sẽ vượt quá giới hạn mua hàng của bạn." };
        //        }
        //        // Cập nhật số lượng sản phẩm trong giỏ hàng
        //        existingItem.Quantity = totalQuantity;
        //    }
        //    else
        //    {
        //        // Kiểm tra xem số lượng thêm vào có vượt quá số lượng trong kho không
        //        if (quantity > productInStock)
        //        {
        //            return new CartResult { Success = false, Message = $"Số lượng sản phẩm trong kho không đủ. Số lượng hiện còn: {productInStock}" };
        //        }

        //        // Thêm sản phẩm mới vào giỏ hàng
        //        cart.Add(new CartItemRequest { ProductId = productId, Quantity = quantity });
        //    }
        //    SaveCart(cart);
        //    return new CartResult { Success = true, Message = "Thêm sản phẩm vào giỏ hàng thành công." };

        //}
        //public void UpdateCartItem(Guid productId, int quantity)
        //{
        //    var cart = GetCart();
        //    var item = cart.FirstOrDefault(item => item.ProductId == productId);
        //    if (item != null)
        //    {
        //        item.Quantity = quantity;
        //        SaveCart(cart);
        //    }
        //}
        //public void RemoveFromCart(Guid productId)
        //{
        //    var cart = GetCart();
        //    cart.RemoveAll(item => item.ProductId == productId);
        //    SaveCart(cart);
        //}
        //public void ClearCart()
        //{
        //    SaveCart(new List<CartItemRequest>());
        //}
        //private void SaveCart(List<CartItemRequest> cart)
        //{
        //    var cartJson = JsonConvert.SerializeObject(cart);
        //    _httpContextAccessor.HttpContext.Response.Cookies.Append(_cartCookieName, cartJson, new CookieOptions
        //    {
        //        Expires = System.DateTimeOffset.Now.AddDays(30) // Lưu trong 30 ngày
        //    });
        //}
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
                            ProductName = product.Ten,
                            Price = product.GiaBan,
                            ImageUrl = product.AnhSanPhams?.FirstOrDefault()?.ImageUrl
                        });
                    }
                }
                return cartItemsWithProductInfo;
            }
        }
        public async Task<CartResult> AddToCart(Guid productId, int quantity)
        {
            var userIdString = _httpContextAccessor.HttpContext?.Session.GetString("UserId");

            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out Guid userId))
            {
                // Xử lý giỏ hàng cookie cho khách vãng lai
                var cartItems = GetCartFromCookie();
                var existingItem = cartItems.FirstOrDefault(ci => ci.ProductId == productId);

                var product = await _sanPhamRepo.GetByIdAsync(productId);
                if (product == null)
                {
                    return new() { Success = false, Message = "Sản phẩm không tồn tại." };
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
                return new() { Success = false, Message = productForUser == null ? "Sản phẩm không tồn tại." : "Số lượng không đủ." };

            var cart = await _cartRepo.GetCartByUserIdAsync(userId) ?? await _cartRepo.CreateCartAsync(userId);
            var cartItem = cart.CartItems?.FirstOrDefault(ci => ci.SanPhamId == productId);

            if (cartItem != null) cartItem.SoLuong += quantity;
            else await _cartRepo.AddCartItemAsync(new() { CartId = cart.Id, SanPhamId = productId, SoLuong = quantity });

            if (cartItem != null) await _cartRepo.UpdateCartItemAsync(cartItem);

            return new() { Success = true, Message = "Thêm vào giỏ hàng thành công.", CartItemCount = cart.CartItems?.Sum(ci => ci.SoLuong) ?? 0 };
        }

        public async Task<CartResult> UpdateCartItem(Guid productId, int quantity)
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

        public async Task<CartResult> RemoveFromCart(Guid productId)
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

        public async Task<CartResult> ClearCart()
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
                return new CartResult { Success = false, Message = "Giỏ hàng trống hoặc không tồn tại." };

            await _cartRepo.ClearCartAsync(cart.Id);

            return new CartResult { Success = true, Message = "Xóa giỏ hàng thành công.", CartItemCount = 0 };
        }
    }
}
