using System.ComponentModel.DataAnnotations;

namespace Core.Request
{
    public class CreateNhaCungCapRequest
    {
        [Required(ErrorMessage = "Yêu cầu tên nhà cung cấp")]
        public required string Ten { get; set; }
        public string? Mota { get; set; }
        public bool TrangThai { get; set; }
    }
   
}
