﻿using Infrastructure.Entities;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Request
{
    public class AddSanPhamRequest
    {
        public required string Ten { get; set; }
        public string? MaSanPham { get; set; }
        public string? Mota { get; set; }
        public int SoLuong { get; set; }
        public int GiaBan { get; set; }
        public bool TrangThai { get; set; }
        public Guid NhaCungCapId { get; set; }
        public Guid DanhMucSanPhamId { get; set; }
        public List<IFormFile>? Images { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
    
}
