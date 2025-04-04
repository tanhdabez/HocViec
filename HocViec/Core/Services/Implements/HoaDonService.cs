using AutoMapper;
using Core.Request;
using Core.Response;
using Core.Services.Interfaces;
using Infrastructure.Models;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Core.Services.Implements
{
    public class HoaDonService : IHoaDonService
    {
        private readonly IHoaDonRepository _hoaDonRepository;
        private readonly ISanPhamRepository _sanPhamRepository;
        private readonly IMapper _mapper;
        public HoaDonService(IHoaDonRepository hoaDonRepository, IMapper mapper, ISanPhamRepository sanPhamRepository)
        {
            _hoaDonRepository = hoaDonRepository;
            _mapper = mapper;
            _sanPhamRepository = sanPhamRepository;
        }
        public async Task<PaginationRequest<HoaDonResponse>> GetAllHoaDonAsync(FilterRequest filter)
        {
            if (filter.StartDate == null || filter.EndDate == null)
            {
                filter.StartDate = DateTime.Today.AddDays(-7);
                filter.EndDate = DateTime.Today;
            }
            var hoaDons = _hoaDonRepository.GetAllHoaDons(filter.StartDate, filter.EndDate, filter.TrangThai, filter.MaHD);
            var totalItems = await hoaDons.CountAsync();
            var totalPages = (int)Math.Ceiling(totalItems / (double)filter.PageSize);
            var data = await hoaDons.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize).ToListAsync();
            var mappedData = _mapper.Map<List<HoaDonResponse>>(data);

            return new PaginationRequest<HoaDonResponse>
            {
                Data = mappedData,
                TotalItems = totalItems,
                TotalPages = totalPages
            };
        }

        public async Task<HoaDonResponse> GetHoaDonChiTietAsync(Guid id)
        {
            var hoaDon = await _hoaDonRepository.GetHoaDon(id);
            if (hoaDon == null) return null;

            var chiTietHoaDons = await _hoaDonRepository.GetChiTietHoaDon(id);
            if (chiTietHoaDons == null) return null;

            var chiTietSanPhamDTOs = chiTietHoaDons.Select(ct => new ChiTietSanPhamResponse
            {
                MaSanPham = ct.SanPham.MaSanPham,
                TenSanPham = ct.SanPham.Ten,
                SoLuong = ct.SoLuong,
                DonGia = ct.DonGia,
                ThanhTien = ct.DonGia * ct.SoLuong
            }).ToList();

            var hoaDonChiTietDTO = new HoaDonResponse
            {
                Id = hoaDon.Id,
                MaHD = hoaDon.MaHD,
                TenNguoiNhan = hoaDon.TenNguoiNhan,
                SDT = hoaDon.SDT,
                Email = hoaDon.Email,
                DiaChi = hoaDon.DiaChi,
                TongTien = hoaDon.TongTien,
                PhuongThucThanhToan = hoaDon.PhuongThucThanhToan,
                GhiChu = hoaDon.GhiChu,
                TrangThai = hoaDon.TrangThai,
                UserId = hoaDon.UserId,
                TenKhachHang = hoaDon.TenNguoiNhan,
                CreatedDate = hoaDon.CreatedDate,
                UpdatedDate = hoaDon.UpdatedDate,
                ChiTietSanPhams = chiTietSanPhamDTOs
            };

            return hoaDonChiTietDTO;
        }

        public async Task<bool> UpdateHoaDonAsync(Guid hoaDonId, int trangThai, string ghiChu)
        {
            var hoaDon = await _hoaDonRepository.GetHoaDon(hoaDonId);

            hoaDon.TrangThai = trangThai;
            hoaDon.GhiChu = ghiChu;
            if (trangThai == 3)
            {
                hoaDon.UpdatedDate = DateTime.Now;

            }
            var result = await _hoaDonRepository.UpdateHoaDon(hoaDon);
            return true;
        }
    }
}
