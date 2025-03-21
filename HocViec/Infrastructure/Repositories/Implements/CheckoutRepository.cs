using Infrastructure.Models;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Implements
{
    public class CheckoutRepository : ICheckoutRepository
    {
        private readonly AppDbContext _dbContext;
        public CheckoutRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> CheckoutAsync(HoaDon request)
        {
            await _dbContext.AddAsync(request);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<HoaDon?> GetHoaDonById(Guid Id)
        {
            return await _dbContext.HoaDons.Include(hd => hd.ChiTietHoaDons).FirstOrDefaultAsync(hd => hd.Id == Id);
        }

        public string GenerateInvoiceCode(string prefix)
        {
            string invoiceId = Guid.NewGuid().ToString("N");
            // Sử dụng format "N" để loại bỏ dấu gạch ngang, sau đó lấy 8 ký tự đầu
            return prefix + "-" + invoiceId.Substring(0, 8).ToUpper();
        }

        public Task<HoaDon?> CreateHoaDon(HoaDon hoaDon)
        {
            throw new NotImplementedException();
        }

        public Task<HoaDon?> GetHoaDon(Guid hoaDonId)
        {
            throw new NotImplementedException();
        }

        public Task<HoaDon?> UpdateHoaDon(HoaDon hoaDon)
        {
            throw new NotImplementedException();
        }

        public Task<List<HoaDon>> GetAllHoaDons()
        {
            throw new NotImplementedException();
        }

        public Task<ChiTietHoaDon?> CreateChiTietHoaDon(ChiTietHoaDon chiTietHoaDon)
        {
            throw new NotImplementedException();
        }

        public Task<ChiTietHoaDon?> GetChiTietHoaDon(Guid chiTietHoaDonId)
        {
            throw new NotImplementedException();
        }

        public Task<List<ChiTietHoaDon?>> GetChiTietHoaDonsByHoaDonId(Guid hoaDonId)
        {
            throw new NotImplementedException();
        }
    }
}
