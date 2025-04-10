using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Response
{
    public class ThongKeResponse
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int SoLuongHangBan { get; set; }
        public int SoLuongHangConLai { get; set; }
        public int SoLuongHoaDon { get; set; }
        public List<int>? TopSoLuong { get; set; }
        public List<string>? TopNhaCungCap { get; set; }
    }
}
