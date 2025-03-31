using AutoMapper;
using Core.Request;
using Core.Services.Interfaces;
using Infrastructure.Models;
using Infrastructure.Repositories.Implements;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Core.Services.Implements
{
    public class CheckoutService : ICheckoutService
    {
        private readonly IHoaDonRepository _hoaDonRepository;
        private readonly ICartService _cartService;
        private readonly ISanPhamRepository _sanPhamRepository;
        private readonly IMapper _mapper;
        public CheckoutService(IHoaDonRepository hoaDonRepository, IMapper mapper, ISanPhamRepository sanPhamRepository, ICartService cartService)
        {
            _hoaDonRepository = hoaDonRepository;
            _mapper = mapper;
            _sanPhamRepository = sanPhamRepository;
            _cartService = cartService;
        }

        public async Task<GioHangDto> GetSanPhamsByIdsAsync(List<CheckOutDetailsDto> requests)
        {
            var gioHang = new GioHangDto();

            foreach (var item in requests)
            {
                var sanPham = await _sanPhamRepository.GetSanPhamWithImagesAsync(item.SanPhamId);
                if (sanPham != null)
                {
                    if (sanPham.SoLuong == 0)
                    {
                        return null;
                    }
                    var chiTiet = new CheckOutDetailsDto
                    {
                        SanPhamId = sanPham.Id,
                        TenSanPham = sanPham.Ten,
                        DonGia = sanPham.GiaBan,
                        SoLuong = item.SoLuong,
                        CartItemId = item.CartItemId,
                        ImageUrl = sanPham.AnhSanPhams.FirstOrDefault()?.ImageUrl ?? "/images/no-image.png" // Lấy ImageUrl
                    };

                    gioHang.ChiTietHoaDons.Add(chiTiet);
                    gioHang.TongTien += chiTiet.DonGia * chiTiet.SoLuong;
                }
            }

            return gioHang;
        }

        public async Task<bool> CreateHoaDonAsync(CheckOutRequest request, Guid? userId)
        {
            var hoaDon = new HoaDon
            {
                MaHD = _hoaDonRepository.GenerateInvoiceCode("HD"),
                TongTien = request.TongTien,
                TenNguoiNhan = request.Ten,
                SDT = request.SDT,
                Email = request.Email,
                DiaChi = request.DiaChi,
                GhiChu = request.GhiChu,
                PhuongThucThanhToan = request.PhuongThucThanhToan,
                UserId = userId,
            };

            hoaDon = await _hoaDonRepository.CreateHoaDon(hoaDon);

            var chiTietHoaDons = request.ChiTietHoaDons.Select(item => new ChiTietHoaDon
            {
                HoaDonId = hoaDon.Id,
                SanPhamId = item.SanPhamId,
                DonGia = item.DonGia,
                SoLuong = item.SoLuong
            }).ToList();

            await _hoaDonRepository.CreateChiTietHoaDon(chiTietHoaDons);

            var cartItems = _cartService.GetCartFromCookie();
            //Xóa các mục giỏ hàng
            foreach (var item in request.ChiTietHoaDons)
            {
                if (userId.HasValue)
                {
                    await _hoaDonRepository.RemoveProductInCart(userId.Value, item.SanPhamId);
                }
                else
                {
                    // Xóa sản phẩm khỏi giỏ hàng cookie sử dụng CartService
                    cartItems.RemoveAll(ci => ci.ProductId == item.SanPhamId);
                }
            }
            _cartService.SetCartToCookie(cartItems);
            return true;
        }

  
    }
}
