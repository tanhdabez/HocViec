using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Response
{
    public class RegisterRequest
    {
        [Required(ErrorMessage = "Yêu cầu nhập tên")]
        public required string Ten { get; set; }
        [Required(ErrorMessage = "Yêu cầu email")]
        [EmailAddress(ErrorMessage = "Sai định dạng email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Yêu cầu mật khẩu")]
        [RegularExpression(@"^(?=.*\d).{8,}$", ErrorMessage = "Mật khẩu phải có ít nhất 8 ký tự và chứa ít nhất một chữ số")]
        public string Password { get; set; }
    }
    public class UserLoginResult
    {
        public bool IsSuccess { get; set; }
        public string UserId { get; set; }
        public string Role { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
