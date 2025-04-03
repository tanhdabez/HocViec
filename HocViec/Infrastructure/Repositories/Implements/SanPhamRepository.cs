using Infrastructure.Models;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repositories.Implements
{
    public class SanPhamRepository : ISanPhamRepository
    {
        private readonly AppDbContext _dbContext; private readonly ILogger<SanPhamRepository> _logger;
        public SanPhamRepository(AppDbContext dbContext, ILogger<SanPhamRepository> logger) // Thêm ILogger vào constructor
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        public async Task<List<SanPham>> GetAllAsync()
        {
            return await _dbContext.SanPhams
              .Include(sp => sp.NhaCungCap)
              .Include(sp => sp.DanhMucLoaiHang)
              .Include(sp => sp.AnhSanPhams)
              .Where(sp => sp.TrangThai == true)
              .ToListAsync();
        }

        public async Task<List<SanPham>> GetAllSanPhamsWithIncludesAsync()
        {
            return await _dbContext.SanPhams
                .Include(sp => sp.NhaCungCap)
                .Include(sp => sp.DanhMucLoaiHang)
                .Include(sp => sp.AnhSanPhams)
                .ToListAsync();
        }

        public async Task<SanPham> GetByIdAsync(Guid id)
        {
            return await _dbContext.SanPhams.FirstOrDefaultAsync(x => x.Id == id) ?? throw new Exception("Không tồn tại");
        }

        public async Task<SanPham> GetSanPhamWithImagesAsync(Guid sanPhamId)
        {
            return await _dbContext.SanPhams
                .Include(x => x.AnhSanPhams)
                .FirstOrDefaultAsync(x => x.Id == sanPhamId);
        }

        public async Task<AnhSanPham> GetImageByUrlAsync(string imageUrl)
        {
            return await _dbContext.AnhSanPhams
                .FirstOrDefaultAsync(x => x.ImageUrl == imageUrl);
        }

        public async Task<SanPham> GetSanPhamDetailsAsync(Guid sanPhamId)
        {
            return await _dbContext.SanPhams
                .Include(sp => sp.NhaCungCap)
                .Include(sp => sp.DanhMucLoaiHang)
                .Include(sp => sp.AnhSanPhams)
                .FirstOrDefaultAsync(sp => sp.Id == sanPhamId);
        }

        public async Task<bool> AddAsync(SanPham entity)
        {
            try
            {
                var danhMuc = await _dbContext.DanhMucLoaiHangs.FindAsync(entity.DanhMucSanPhamId);

                if (danhMuc == null)
                {
                    // Xử lý lỗi: Danh mục không tồn tại
                    // Ví dụ: ném ngoại lệ, ghi log, trả về thông báo lỗi cho người dùng
                    throw new Exception($"Danh mục loại hàng với Id {entity.DanhMucSanPhamId} không tồn tại.");
                }
                await _dbContext.AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                // Log lỗi tại đây
                // Ví dụ: sử dụng ILogger<SanPhamRepository>
                _logger.LogError(ex, "Lỗi khi thêm sản phẩm: {SanPhamId}", entity?.Id);

                // Hoặc sử dụng Console.WriteLine (chỉ cho mục đích debug)
                // Console.WriteLine($"Lỗi khi thêm sản phẩm: {ex.Message}");

                return false; // Hoặc ném lại ngoại lệ nếu cần
            }
        }

        public async Task<bool> AddAnhAsync(AnhSanPham entity)
        {
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<SanPham?> UpdateAsync(SanPham entity)
        {
            entity.UpdatedDate = DateTime.Now;
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> UpdateStatusAsync(Guid id)
        {
            var entity = await _dbContext.SanPhams.FirstOrDefaultAsync(x => x.Id == id);
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
            var sanPham = await _dbContext.SanPhams.FirstOrDefaultAsync(sp => sp.Id == id);
            if (sanPham != null)
            {
                var anhSanPham = await _dbContext.AnhSanPhams.Where(a => a.SanPhamId == sanPham.Id).ToListAsync();
                _dbContext.Remove(sanPham);
                _dbContext.RemoveRange(anhSanPham);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task RemoveImageAsync(AnhSanPham image)
        {
            _dbContext.AnhSanPhams.Remove(image);
            await _dbContext.SaveChangesAsync();
        }

        public string GenerateMa(string prefix)
        {
            string id = Guid.NewGuid().ToString("N");
            return prefix + "-" + id.Substring(0, 8).ToUpper();
        }
    }
}
