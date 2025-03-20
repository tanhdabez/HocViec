using Infrastructure.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace Core.Request
{
    public class CreateUserRequest
    {
        [Required(ErrorMessage = "Tên không được để trống")]
        public required string Ten { get; set; }
        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Không đúng định dạng Email")]
        public required string Email { get; set; }
        [Required(ErrorMessage = "Yêu cầu mật khẩu")]
        [RegularExpression(@"^(?=.*\d).{8,}$", ErrorMessage = "Mật khẩu phải có ít nhất 8 ký tự và chứa ít nhất một chữ số")]
        public required string Password { get; set; }
        public string? SDT { get; set; }
        [StringLength(200, ErrorMessage = "Địa chỉ không được vượt quá 200 ký tự")]
        public string? DiaChi { get; set; }
        [Required(ErrorMessage = "Không để trống vai trò")]
        public required EnumRole VaiTro { get; set; }
        public bool TrangThai { get; set; }
    }
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
