using Core.Request;
using Core.Response;
using Infrastructure.Models;

namespace Core.Services.Interfaces
{
    public interface IHoaDonService
    {
        Task<PaginationRequest<HoaDonResponse>> GetAllHoaDonAsync(FilterRequest filter);
        Task<bool> UpdateHoaDonAsync(Guid hoaDonId, int trangThai, string ghiChu);
        Task<HoaDonResponse> GetHoaDonChiTietAsync(Guid id);
    }
}
