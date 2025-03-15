using Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models.DanhMuc
{
    public class DanhMucLoaiHang: BaseModel
    {
        [Required(ErrorMessage = "Tên danh mục không được để trống.")]
        public required string Ten { get; set; }

        public string? GhiChu { get; set; }

        public virtual ICollection<SanPham> SanPhams { get; set; }

    }
}
