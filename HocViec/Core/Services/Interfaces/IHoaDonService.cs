using Core.Request;
using Infrastructure.Models;

namespace Core.Services.Interfaces
{
    public interface IHoaDonService
    {
        Task<List<HoaDonResponse>> GetAllHoaDonAsync(DateTime? startDate, DateTime? endDate, int? trangThai);
        Task<HoaDon> UpdateHoaDonAsync(HoaDon hoaDon);
        Task<List<ChiTietHoaDon>> GetChiTietHoaDonsByHoaDonIdAsync(Guid hoaDonId);
    }
}
