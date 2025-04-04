
namespace Core.Response
{
    public class NhaCungCapResponse
    {
        public Guid Id { get; set; }
        public required string Ten { get; set; }
        public string? Mota { get; set; }
        public bool TrangThai { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
