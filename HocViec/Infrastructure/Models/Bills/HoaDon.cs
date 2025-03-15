using Infrastructure.Entities;
using Infrastructure.Models.Enum;
using Infrastructure.Models.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models.Bills
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
        public Guid IdNhanVien { get; set; }
        public virtual User.User? NhanVien { get; set; }
        public virtual ICollection<ChiTietHoaDon>? ChiTietHoaDons { get; set; }
    }
}
