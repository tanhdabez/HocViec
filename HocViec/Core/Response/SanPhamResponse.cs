using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Response
{
    public class SanPhamResponse
    {
        public Guid Id { get; set; }
        public required string Ten { get; set; }
        public string? MaSanPham { get; set; }
        public string? Mota { get; set; }
        public int SoLuong { get; set; }
        public int GiaBan { get; set; }
        public bool TrangThai { get; set; }
        public Guid NhaCungCapId { get; set; }
        public Guid DanhMucSanPhamId { get; set; }
        public string? TenNhaCungCap { get; set; }
        public string? TenDanhMucSanPham { get; set; }
        public List<string>? AnhSanPhams { get; set; }
        public List<IFormFile>? Images { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
