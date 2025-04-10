using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Implements
{
    public class ThongKeRepository : IThongKeRepository
    {
        private readonly AppDbContext _context;

        public ThongKeRepository(AppDbContext dbContext)
        {
            _context = dbContext;
        }
        public async Task<int> GetSoLuongHangHoaBanAsync(DateTime startDate, DateTime endDate)
        {
            var soldQuantity = await _context.HoaDons
                .Where(h => h.CreatedDate >= startDate && h.CreatedDate <= endDate && h.TrangThai == 3)
                .SelectMany(h => h.ChiTietHoaDons)
                .SumAsync(ct => ct.SoLuong);

            return soldQuantity;
        }
        public async Task<int> GetSoLuongHangHoaConLaiAsync()
        {
            var remainingQuantity = await _context.SanPhams.SumAsync(s => s.SoLuong);
            return remainingQuantity;
        }
        public async Task<int> GetSoLuongHoaDonBanAsync(DateTime startDate, DateTime endDate)
        {
            var invoiceCount = await _context.HoaDons
                .CountAsync(h => h.CreatedDate >= startDate && h.CreatedDate <= endDate);
            return invoiceCount;
        }
        public async Task<int> GetTopSoLuongBanRaAsync(Guid idNCC, DateTime startDate, DateTime endDate)
        {
            var productIds = await _context.SanPhams
                .Where(x=>x.NhaCungCapId == idNCC)
                .Select(x=>x.Id)
                .ToListAsync();

            var totalQuantitySold = await _context.ChiTietHoaDons
            .Where(cthd => productIds.Contains(cthd.SanPhamId) &&
                     cthd.HoaDon.CreatedDate >= startDate &&
                     cthd.HoaDon.CreatedDate <= endDate && cthd.HoaDon.TrangThai == 3)
            .SumAsync(cthd => cthd.SoLuong);
            return totalQuantitySold;
        }
    }
}
