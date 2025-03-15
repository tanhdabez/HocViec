using Infrastructure.Entities;
using Infrastructure.Models.DanhMuc;
using Infrastructure.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Request
{
    public class CreateNhaCungCapRequest
    {
        [Required(ErrorMessage = "Yêu cầu tên nhà cung cấp")]
        public required string Ten { get; set; }
        public string? Mota { get; set; }
        public bool TrangThai { get; set; }
    }
    public class NhaCungCapResponse
    {
        public Guid Id { get; set; }
        public string Ten { get; set; }
        public string? Mota { get; set; }
        public bool TrangThai { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
