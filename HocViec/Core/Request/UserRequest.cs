using Infrastructure.Entities;
using Infrastructure.Models.Enum;
using Infrastructure.Models.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Request
{
    public class CreateUserRequest
    {
        [Required(ErrorMessage = "Tên không được để trống")]
        public required string Ten { get; set; }
        [EmailAddress(ErrorMessage = "Không đúng định dạng Email")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Yêu cầu mật khẩu")]
        [RegularExpression(@"^(?=.*\d).{8,}$", ErrorMessage = "Mật khẩu phải có ít nhất 8 ký tự và chứa ít nhất một chữ số")]
        public required string Password { get; set; }
        public string? SDT { get; set; }
        [StringLength(200, ErrorMessage = "Địa chỉ không được vượt quá 200 ký tự")]
        public string? DiaChi { get; set; }
        public EnumRole VaiTro { get; set; }
        public bool TrangThai { get; set; }
    }
    public class UserResponse
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Tên không được để trống")]
        public required string Ten { get; set; }
        [EmailAddress(ErrorMessage = "Không đúng định dạng Email")]
        public required string? Email { get; set; }
        public string? Password { get; set; } = null;
        public string? SDT { get; set; }
        [StringLength(200, ErrorMessage = "Địa chỉ không được vượt quá 200 ký tự")]
        public string? DiaChi { get; set; }
        public EnumRole VaiTro { get; set; }
        public bool TrangThai { get; set; }
        public DateTime UpdatedDate { get; set; }

    }
}
