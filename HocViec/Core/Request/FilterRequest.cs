namespace Core.Request
{
    public class FilterRequest : GenericPage
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? TrangThai { get; set; }
        public string? MaHD { get; set; }
    }
}
