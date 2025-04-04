using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Response
{
    public class CheckOutDetailsResponse
    {
        public Guid SanPhamId { get; set; }
        public string TenSanPham { get; set; }
        public int DonGia { get; set; }
        public int SoLuong { get; set; }
        public Guid CartItemId { get; set; }
        public string ImageUrl { get; set; } = "/images/no-image.png"; // Thêm ImageUrl
    }

    public class GioHangDto
    {
        public int TongTien { get; set; }
        public List<CheckOutDetailsResponse> ChiTietHoaDons { get; set; } = new List<CheckOutDetailsResponse>();
    }
}
