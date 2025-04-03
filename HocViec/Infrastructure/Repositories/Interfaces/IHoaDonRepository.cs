using Infrastructure.Models;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IHoaDonRepository
    {
        //Order
        Task<int> GetTotalCount();
        Task<HoaDon> CreateHoaDon(HoaDon hoaDon);
        Task<HoaDon?> GetHoaDon(Guid hoaDonId);
        Task<bool> UpdateHoaDon(HoaDon hoaDon);
        public IQueryable<HoaDon> GetAllHoaDons(DateTime? startDate, DateTime? endDate, int? trangThai, string? maHD);
        //Order detail
        Task CreateChiTietHoaDon(List<ChiTietHoaDon> chiTietHoaDon);
        Task<List<ChiTietHoaDon?>> GetChiTietHoaDon(Guid idHoaDon);
        Task<List<ChiTietHoaDon>> GetChiTietHoaDonsByHoaDonId(Guid hoaDonId);
        Task<bool> RemoveProductInCart(Guid? userId, Guid prductId);
        //Ma
        string GenerateInvoiceCode(string prefix);

    }
}
