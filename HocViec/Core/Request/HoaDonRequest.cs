using Infrastructure.Entities;
using Infrastructure.Models;
using System.ComponentModel.DataAnnotations.Schema;
using X.PagedList;

namespace Core.Request
{
    public class HoaDonRequest
    {
        public required string MaHD { get; set; }
        public string? TenNguoiNhan { get; set; }
        public string? SDT { get; set; }
        public string? Email { get; set; }
        public string? DiaChi { get; set; }
        public int? TongTien { get; set; }
        public int? PhuongThucThanhToan { get; set; }
        public string? GhiChu { get; set; }
        public Guid? UserId { get; set; }

    }
    public class HoaDonResponse
    {
        public Guid Id { get; set; }
        public required string MaHD { get; set; }
        public string? TenNguoiNhan { get; set; }
        public string? SDT { get; set; }
        public string? Email { get; set; }
        public string? DiaChi { get; set; }
        public int? TongTien { get; set; }
        public int? PhuongThucThanhToan { get; set; }
        public string? GhiChu { get; set; }
        public Guid? UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int TrangThai { get; set; }
    }
    public class HoaDonPaginationModel
    {
        public IPagedList<HoaDonResponse> PagedHoaDons { get; set; }
        public decimal TongTienTatCa { get; set; }
    }
    public class ChiTietHoaDonRequest
    {
        public int DonGia { get; set; }
        public int SoLuong { get; set; }
        public Guid SanPhamId { get; set; }
        public Guid HoaDonId { get; set; }
    }
    public class ChiTietHoaDonResponse
    {
        public Guid Id { get; set; }
        public int DonGia { get; set; }
        public int SoLuong { get; set; }
        public Guid SanPhamId { get; set; }
        public Guid HoaDonId { get; set; }
    }
}
