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
        public int TrangThai { get; set; }
        public Guid? UserId { get; set; }
        public string? TenKhachHang { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public List<ChiTietSanPhamResponse> ChiTietSanPhams { get; set; }

    }

    public class ChiTietSanPhamResponse
    {
        public string MaSanPham { get; set; }
        public string TenSanPham { get; set; }
        public int SoLuong { get; set; }
        public int DonGia { get; set; }
        public int ThanhTien { get; set; }
    }
}
