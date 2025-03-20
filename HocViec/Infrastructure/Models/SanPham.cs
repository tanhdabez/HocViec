using Infrastructure.Entities;
using Infrastructure.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Models
{
    public class SanPham : BaseModel
    {
        public required string Ten { get; set; }
        public string? MaSanPham { get; set; }
        public string? Mota { get; set; }
        public int SoLuong { get; set; }
        public int GiaBan { get; set; }
        [ForeignKey("NhaCungCap")]
        public Guid NhaCungCapId { get; set; }
        [ForeignKey("DanhMucLoaiHang")]
        public Guid DanhMucSanPhamId { get; set; }
        public virtual NhaCungCap? NhaCungCap { get; set; }
        public virtual DanhMucLoaiHang? DanhMucLoaiHang { get; set; }
        public virtual ICollection<ChiTietHoaDon>? ChiTietHoaDons { get; set; }
        public virtual ICollection<AnhSanPham>? AnhSanPhams { get; set; }
    }
}
