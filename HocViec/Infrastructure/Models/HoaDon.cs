using Infrastructure.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Models
{
    public class HoaDon : BaseModel
    {
        public required string MaHD { get; set; }
        public string? TenNguoiNhan { get; set; }
        public string? SDT { get; set; }
        public string? Email { get; set; }
        public string? DiaChi { get; set; }
        public int? TongTien { get; set; }
        public int? PhuongThucThanhToan { get; set; }
        public string? GhiChu { get; set; }
        [ForeignKey("NhanVien")]
        public Guid NhanVienId { get; set; }
        public virtual User? User { get; set; }
        public virtual ICollection<ChiTietHoaDon>? ChiTietHoaDons { get; set; }
    }
}
