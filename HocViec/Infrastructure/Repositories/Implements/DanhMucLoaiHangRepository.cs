using Infrastructure.Models;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Implements
{
    public class DanhMucLoaiHangRepository : IDanhMucLoaiHangRepository
    {
        private readonly AppDbContext _dbContext;
        public DanhMucLoaiHangRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<DanhMucLoaiHang>> GetAllAsync()
        {
            return await _dbContext.DanhMucLoaiHangs.ToListAsync();
        }

        public async Task<DanhMucLoaiHang> GetByIdAsync(Guid id)
        {
            return await _dbContext.DanhMucLoaiHangs.FirstOrDefaultAsync(x => x.Id == id) ?? throw new Exception("Không tồn tại");
        }

        public async Task<bool> AddAsync(DanhMucLoaiHang entity)
        {
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<DanhMucLoaiHang?> UpdateAsync(DanhMucLoaiHang entity)
        {
            entity.UpdatedDate = DateTime.Now;
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> UpdateStatusAsync(Guid id)
        {
            var DanhMucLoaiHang = await _dbContext.DanhMucLoaiHangs
                   .FirstOrDefaultAsync(x => x.Id == id);
            if (DanhMucLoaiHang != null)
            {
                DanhMucLoaiHang.TrangThai = !DanhMucLoaiHang.TrangThai;
                DanhMucLoaiHang.UpdatedDate = DateTime.Now;

                var sanPhams = await _dbContext.SanPhams
                    .Where(sp => sp.DanhMucSanPhamId == id)
                    .ToListAsync();

                foreach (var sanPham in sanPhams)
                {
                    sanPham.TrangThai = DanhMucLoaiHang.TrangThai;
                    sanPham.UpdatedDate = DateTime.Now;
                }
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _dbContext.DanhMucLoaiHangs.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null) return false;
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
