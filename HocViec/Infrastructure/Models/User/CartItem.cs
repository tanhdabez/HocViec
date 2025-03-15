using Infrastructure.Entities;
using Infrastructure.Models.DanhMuc;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Models.User
{
    public class CartItem : BaseModel
    {
        [ForeignKey("Cart")]
        public Guid IdCart { get; set; }
        [ForeignKey("SanPham")]
        public Guid IdSanPham { get; set; }
        public int SoLuong { get; set; }
        public virtual Cart? Cart { get; set; }
        public virtual SanPham? SanPham { get; set; }
    }
}
