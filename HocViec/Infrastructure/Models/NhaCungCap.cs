using Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public class NhaCungCap : BaseModel
    {
        [Required(ErrorMessage = "Yêu cầu tên nhà cung cấp")]
        public required string Ten { get; set; }
        public string? Mota { get; set; }
        public bool TrangThai { get; set; }
        public ICollection<SanPham> sanPhams { get; set; }
    }
}
