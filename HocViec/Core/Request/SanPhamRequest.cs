using Infrastructure.Entities;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Request
{
    public class AddSanPhamRequest
    {
        public required string Ten { get; set; }
        public string? MaSanPham { get; set; }
        public string? Mota { get; set; }
        public int SoLuong { get; set; }
        public int GiaBan { get; set; }
        public bool TrangThai { get; set; }
        [ForeignKey("NhaCungCap")]
        public Guid IdNhaCungCap { get; set; }
        [ForeignKey("DanhMucLoaiHang")]
        public Guid IdDanhMucSanPham { get; set; }
        public List<IFormFile>? Images { get; set; }
        public DateTime UpdateDate { get; set; }
    }
    public class SanPhamResponse
    {
        public Guid Id { get; set; }
        public required string Ten { get; set; }
        public string? MaSanPham { get; set; }
        public string? Mota { get; set; }
        public int SoLuong { get; set; }
        public int GiaBan { get; set; }
        public bool TrangThai { get; set; }
        [ForeignKey("NhaCungCap")]
        public Guid IdNhaCungCap { get; set; }
        public string? TenNhaCungCap { get; set; }
        [ForeignKey("DanhMucLoaiHang")]
        public Guid IdDanhMucSanPham { get; set; }
        public string? TenDanhMucSanPham { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
