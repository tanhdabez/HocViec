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
  
}
