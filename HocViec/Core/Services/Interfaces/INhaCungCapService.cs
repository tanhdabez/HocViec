using Core.Request;

namespace Core.Services.Interfaces
{
    public interface INhaCungCapService
    {
        Task<List<NhaCungCapResponse>> GetAllNhaCungCap();
        Task<NhaCungCapResponse> GetNhaCungCapById(Guid id);

        Task<bool> AddNhaCungCap(CreateNhaCungCapRequest request);

        Task<NhaCungCapResponse?> UpdateNhaCungCap(NhaCungCapResponse request);

        Task<bool> UpdateStatusNhaCungCap(Guid id);
    }
}
