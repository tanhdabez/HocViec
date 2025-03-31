namespace Core.Request
{
    public class CheckOutRequest
    {
        public string Ten { get; set; } = string.Empty;
        public string SDT { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string DiaChi { get; set; } = string.Empty;
        public string? GhiChu { get; set; }
        public int? PhuongThucThanhToan { get; set; }
        public int TongTien { get; set; }
        public Guid? UserId { get; set; }
        public required List<CheckOutDetailsDto> ChiTietHoaDons { get; set; }
    }

    public class CheckOutDetailsDto
    {
        public Guid SanPhamId { get; set; }
        public string TenSanPham { get; set; }
        public int DonGia { get; set; }
        public int SoLuong { get; set; }
        public Guid CartItemId { get; set; }
        public string ImageUrl { get; set; } = "/images/no-image.png"; // Thêm ImageUrl
    }

    public class GioHangDto
    {
        public int TongTien { get; set; }
        public List<CheckOutDetailsDto> ChiTietHoaDons { get; set; } = new List<CheckOutDetailsDto>();
    }
}
