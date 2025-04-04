using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Response
{
    public class DanhMucLoaiHangResponse
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Tên danh mục không được để trống.")]
        public required string Ten { get; set; }
        public string? GhiChu { get; set; }
        public bool TrangThai { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
