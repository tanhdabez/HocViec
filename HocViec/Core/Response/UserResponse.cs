using Infrastructure.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace Core.Response
{
    public class UserResponse
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Tên không được để trống")]
        public required string Ten { get; set; }
        [EmailAddress(ErrorMessage = "Không đúng định dạng Email")]
        public required string Email { get; set; }
        public string? Password { get; set; } = null;
        public string? SDT { get; set; }
        [StringLength(200, ErrorMessage = "Địa chỉ không được vượt quá 200 ký tự")]
        public string? DiaChi { get; set; }
        public EnumRole VaiTro { get; set; }
        public bool TrangThai { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
