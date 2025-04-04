using Core.Request;
using Core.Response;

namespace Core.Services.Interfaces
{
    public interface ISanPhamService
    {
        Task<List<SanPhamResponse>> GetAllSanPham_Home();
        Task<List<SanPhamResponse>> GetAllSanPham();
        Task<SanPhamResponse> GetSanPhamById(Guid id);

        Task<bool> AddSanPham(AddSanPhamRequest request);

        Task<SanPhamResponse?> UpdateSanPham(SanPhamResponse request);

        Task<bool> DeleteSanPham(Guid id);

        Task<bool> UpdateStatus(Guid id);

        Task<bool> DeleteImageAsync(string imageUrl);
    }
}
