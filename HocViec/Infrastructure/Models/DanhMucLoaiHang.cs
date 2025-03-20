using Infrastructure.Entities;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models
{
    public class DanhMucLoaiHang: BaseModel
    {
        [Required(ErrorMessage = "Tên danh mục không được để trống.")]
        public required string Ten { get; set; }

        public string? GhiChu { get; set; }

        public virtual ICollection<SanPham> SanPhams { get; set; } = new List<SanPham>();


    }
}
