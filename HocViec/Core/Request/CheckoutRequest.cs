using Core.Response;

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
        public required List<CheckOutDetailsResponse> ChiTietHoaDons { get; set; }
    }
}
