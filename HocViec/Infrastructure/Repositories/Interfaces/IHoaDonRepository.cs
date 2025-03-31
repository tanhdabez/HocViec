using Infrastructure.Models;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IHoaDonRepository
    {
        //Order
        Task<int> GetTotalCount();
        Task<HoaDon> CreateHoaDon(HoaDon hoaDon);
        Task<HoaDon?> GetHoaDon(Guid hoaDonId);
        Task<HoaDon> UpdateHoaDon(HoaDon hoaDon);
        Task<List<HoaDon>> GetAllHoaDons(DateTime? startDate, DateTime? endDate, int? trangThai);
        //Order detail
        Task CreateChiTietHoaDon(List<ChiTietHoaDon> chiTietHoaDon);
        Task<List<ChiTietHoaDon?>> GetChiTietHoaDon(Guid idHoaDon);
        Task<List<ChiTietHoaDon>> GetChiTietHoaDonsByHoaDonId(Guid hoaDonId);
        Task<bool> RemoveProductInCart(Guid? userId, Guid prductId);
        //Ma
        string GenerateInvoiceCode(string prefix);

    }
}
