using Core.Request;
using Infrastructure.Models;

namespace Core.Services.Interfaces
{
    public interface IHoaDonService
    {
        Task<PaginationResponse<HoaDonResponse>> GetAllHoaDonAsync(FilterRequest filter);
        Task<bool> UpdateHoaDonAsync(Guid hoaDonId, int trangThai, string ghiChu);
        Task<HoaDonResponse> GetHoaDonChiTietAsync(Guid id);
    }
}
