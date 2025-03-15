using Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models.User
{
    public class Cart : BaseModel
    {
        [ForeignKey("KhachHang")]
        public Guid IdKhachHang { get; set; }
        [ForeignKey("SanPham")]
        public Guid IdSanPham { get; set; }
        public int SoLuong { get; set; }
        public virtual User? User { get; set; }
        public virtual ICollection<CartItem>? CartItems { get; set; }

    }
}
