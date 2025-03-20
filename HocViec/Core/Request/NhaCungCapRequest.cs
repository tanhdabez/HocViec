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
    public class NhaCungCapResponse
    {
        public Guid Id { get; set; }
        public required string Ten { get; set; }
        public string? Mota { get; set; }
        public bool TrangThai { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
