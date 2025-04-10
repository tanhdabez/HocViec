using Core.Request;
using Core.Response;

namespace Core.Services.Interfaces
{
    public interface INhaCungCapService
    {
        Task<List<NhaCungCapResponse>> GetAllNhaCungCap();

        Task<Dictionary<int, int>> GetMonthlySalesBySupplierId(Guid id);

        Task<NhaCungCapResponse> GetNhaCungCapById(Guid id);

        Task<bool> AddNhaCungCap(CreateNhaCungCapRequest request);

        Task<NhaCungCapResponse?> UpdateNhaCungCap(NhaCungCapResponse request);

        Task<bool> UpdateStatusNhaCungCap(Guid id);
    }
}
