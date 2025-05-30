﻿using Infrastructure.Entities;
using Infrastructure.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models
{
    public class User : BaseModel
    {
        [Required(ErrorMessage = "Tên không được để trống")]
        public required string Ten { get; set; }
        [EmailAddress(ErrorMessage = "Không đúng định dạng Email")]
        public required string Email { get; set; }
        [Required(ErrorMessage = "Yêu cầu mật khẩu")]
        [RegularExpression(@"^(?=.*\d).{8,}$", ErrorMessage = "Mật khẩu phải có ít nhất 8 ký tự và chứa ít nhất một chữ số")]
        public required string Password { get; set; }
        public string? SDT { get; set; }
        [StringLength(200, ErrorMessage = "Địa chỉ không được vượt quá 200 ký tự")]
        public string? DiaChi { get; set; }
        public bool TrangThai { get; set; }
        public EnumRole VaiTro { get; set; }
    }
}
