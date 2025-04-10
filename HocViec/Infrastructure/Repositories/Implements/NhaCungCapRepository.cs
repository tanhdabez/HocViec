using Infrastructure.Models;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Implements
{
    public class NhaCungCapRepository : INhaCungCapRepository
    {
        private readonly AppDbContext _dbContext;
        public NhaCungCapRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<NhaCungCap>> GetAllAsync()
        {
            return await _dbContext.NhaCungCaps.ToListAsync();
        }
        public async Task<Dictionary<int, int>> GetMonthlySalesBySupplierIdAsync(Guid id)
        {
            var salesData = await _dbContext.ChiTietHoaDons
                .Where(cthd => cthd.SanPham.NhaCungCapId == id && cthd.HoaDon.TrangThai == 3)
                .Select(cthd => new
                {
                    Month = cthd.HoaDon.CreatedDate.Month,
                    Quantity = cthd.SoLuong
                })
                .ToListAsync();

            var monthlySales = salesData
                .GroupBy(sale => sale.Month)
                .ToDictionary(group => group.Key, group => group.Sum(s => s.Quantity));

            return monthlySales;
        }

        public async Task<NhaCungCap> GetByIdAsync(Guid id)
        {
            return await _dbContext.NhaCungCaps.FirstOrDefaultAsync(x => x.Id == id) ?? throw new Exception("Không tồn tại");
        }

        public async Task<bool> AddAsync(NhaCungCap entity)
        {
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<NhaCungCap?> UpdateAsync(NhaCungCap entity)
        {
            entity.UpdatedDate = DateTime.Now;
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> UpdateStatusAsync(Guid id)
        {
            var nhaCungCap = await _dbContext.NhaCungCaps
                   .FirstOrDefaultAsync(x => x.Id == id);
            if (nhaCungCap != null)
            {
                nhaCungCap.TrangThai = !nhaCungCap.TrangThai;
                nhaCungCap.UpdatedDate = DateTime.Now;

                var sanPhams = await _dbContext.SanPhams
                    .Where(sp => sp.NhaCungCapId == id)
                    .ToListAsync();

                foreach (var sanPham in sanPhams)
                {
                    sanPham.TrangThai = nhaCungCap.TrangThai;
                    sanPham.UpdatedDate = DateTime.Now;
                }
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _dbContext.NhaCungCaps.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null) return false;
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
