using Infrastructure.Entities;
using Infrastructure.Models.DanhMuc;
using Infrastructure.Models.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Models.Bills
{
    public class ChiTietHoaDon : BaseModel
    {
        public int DonGia { get; set; }
        public int SoLuong { get; set; }
        [ForeignKey("SanPham")]
        public Guid IdSanPham { get; set; }
        [ForeignKey("HoaDon")]
        public Guid IDHoaDon { get; set; }
        public virtual SanPham? SanPham { get; set; }
        public virtual HoaDon? HoaDon { get; set; }
    }
}
