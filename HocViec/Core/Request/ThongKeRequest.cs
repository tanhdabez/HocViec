namespace Core.Request
{
    public class ThongKeRequest
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int SoLuongHangBan { get; set; }
        public int SoLuongHangConLai { get; set; }
        public int SoLuongHoaDon { get; set; }
    }
}
