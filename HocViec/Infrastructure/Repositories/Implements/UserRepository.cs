using Infrastructure.Models;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Implements
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _dbContext;
        public UserRepository(AppDbContext appDbContext)
        {
            _dbContext = appDbContext;
        }

        public async Task<User?> GetUserByEmailAsync(string obj)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == obj);
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(x=>x.Id == id) ?? throw new Exception("Không tồn tại");
        }

        public async Task<IEnumerable<HoaDon>> GetHoaDonsByUserIdAsync(Guid userId)
        {
            return await _dbContext.HoaDons
                .Where(h => h.UserId == userId)
                .Include(h => h.ChiTietHoaDons)
                    .ThenInclude(ct => ct.SanPham)
                .ToListAsync();
        }

        public async Task<List<ChiTietHoaDon>> GetChiTietByHoaDonIdAsync(Guid hoaDonId)
        {
            return await _dbContext.ChiTietHoaDons
                .Include(x => x.SanPham)
                .ThenInclude(sp => sp.AnhSanPhams)
                .Where(x => x.HoaDonId == hoaDonId)
                .ToListAsync();
        }

        public async Task<bool> AddAsync(User entity)
        {
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<User?> UpdateAsync(User entity)
        {
            entity.UpdatedDate = DateTime.Now;
            //_dbSet.Update(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> UpdateStatusAsync(Guid id)
        {
            var entity = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (entity != null)
            {
                entity.TrangThai = !entity.TrangThai;
                entity.UpdatedDate = DateTime.Now;

                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _dbContext.Users.FirstOrDefaultAsync(x=>x.Id == id);
            if (entity == null) return false;
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> CheckEmailExistsForOtherUserAsync(Guid userId, string email)
        {
            return await _dbContext.Users.AnyAsync(x => x.Id != userId && x.Email == email);
        }
    }
}
