using Infrastructure.Entities;
using Infrastructure.Models.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Models
{
    public class ChiTietHoaDon : BaseModel
    {
        public int DonGia { get; set; }
        public int SoLuong { get; set; }
        [ForeignKey("SanPham")]
        public Guid SanPhamId { get; set; }
        [ForeignKey("HoaDon")]
        public Guid HoaDonId { get; set; }
        public virtual SanPham? SanPham { get; set; }
        public virtual HoaDon? HoaDon { get; set; }
    }
}
