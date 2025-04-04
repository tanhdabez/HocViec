using System.ComponentModel.DataAnnotations;

namespace Core.Request
{
    public class CreateDanhMucLoaiHangRequest
    {
        [Required(ErrorMessage = "Tên danh mục không được để trống.")]
        public required string Ten { get; set; }

        public string? GhiChu { get; set; }

        public bool TrangThai { get; set; }
    }
}
