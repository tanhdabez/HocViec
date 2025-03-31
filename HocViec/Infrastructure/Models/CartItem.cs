using Infrastructure.Entities;

namespace Infrastructure.Models
{
    public class CartItem : BaseModel
    {
        public Guid CartId { get; set; }
        public Guid SanPhamId { get; set; }
        public int SoLuong { get; set; }
        public bool TrangThai { get; set; }
        public virtual Cart? Cart { get; set; }
        public virtual SanPham? SanPham { get; set; }
    }
}
