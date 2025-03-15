using AutoMapper;
using Core.Request;
using Core.Services.Interfaces;
using Infrastructure;
using Infrastructure.Models.DanhMuc;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Core.Services.Implements
{
    public class SanPhamService : ISanPhamService
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IRepository<SanPham> _sanPhamRepo;
        private readonly IRepository<AnhSanPham> _anhSanPhamRepo;

        public SanPhamService(IMapper mapper, AppDbContext appDbContext, IRepository<SanPham> sanPhamRepo, IRepository<AnhSanPham> anhSanPhamRepo)
        {
            _dbContext = appDbContext;
            _mapper = mapper;
            _sanPhamRepo = sanPhamRepo;
            _anhSanPhamRepo = anhSanPhamRepo;
        }
        public async Task<List<SanPham>> GetAllSanPham()
        {
            return await _sanPhamRepo.GetAllWithIncludesAsync("NhaCungCap", "DanhMucLoaiHang", "AnhSanPhams");
        }
        public async Task<List<SanPham>> GetAllWithIncludesAndStatusAsync()
        {
            return await _sanPhamRepo.GetAllWithIncludesAndStatusAsync("NhaCungCap", "DanhMucLoaiHang", "AnhSanPhams");
        }
        public async Task<SanPham> GetSanPhamById(Guid id)
        {
            var sanPham = await _dbContext.SanPhams.FirstOrDefaultAsync(x => x.Id == id);
            if (sanPham == null)
            {
                throw new Exception("Không tìm thấy sản phẩm");
            }
            var anhSanPham = await _dbContext.AnhSanPhams.Where(x => x.IdSanPham == id).ToListAsync();
            var nhaCungCapSanPham = await _dbContext.NhaCungCaps.FirstOrDefaultAsync(x => x.Id == sanPham.IdNhaCungCap);
            var danhMucSanPham = await _dbContext.DanhMucLoaiHangs.FirstOrDefaultAsync(x => x.Id == sanPham.IdDanhMucSanPham);
            sanPham.AnhSanPhams = anhSanPham;
            sanPham.NhaCungCap = nhaCungCapSanPham;
            sanPham.DanhMucLoaiHang = danhMucSanPham;
            return sanPham;
        }

        private async Task<string> SaveImage(IFormFile image)
        {
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
            var filePath = Path.Combine("wwwroot", "images", fileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }
            return "/images/" + fileName;
        }

        public async Task<bool> AddSanPham(AddSanPhamRequest request)
        {
            var sanPham = _mapper.Map<SanPham>(request);
            sanPham.MaSanPham = _sanPhamRepo.GenerateMa("SP");
            await _sanPhamRepo.AddAsync(sanPham);
            if (request.Images != null && request.Images.Any())
            {
                foreach (var image in request.Images)
                {
                    var imageUrl = await SaveImage(image);
                    var anhSanPham = new AnhSanPham
                    {
                        IdSanPham = sanPham.Id,
                        ImageUrl = imageUrl
                    };
                    await _anhSanPhamRepo.AddAsync(anhSanPham);
                }
            }
            return true;
        }

        private void DeleteImageFile(string imageUrl)
        {
            var filePath = Path.Combine("wwwroot", imageUrl.TrimStart('/'));
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        public async Task<SanPham?> UpdateSanPham(SanPhamResponse request)
        {
            var sanPham = await _dbContext.SanPhams.Include(x => x.AnhSanPhams).FirstOrDefaultAsync(x => x.Id == request.Id);
            if (sanPham == null)
            {
                return null;
            }
            if (request.Images != null && request.Images.Any())
            {
                foreach (var item in request.Images)
                {
                    var imageUrl = await SaveImage(item);
                    var anhSanPham = new AnhSanPham
                    {
                        IdSanPham = sanPham.Id,
                        ImageUrl = imageUrl
                    };
                    await _anhSanPhamRepo.AddAsync(anhSanPham);
                }
            }
            var updatesanPham = _mapper.Map(request, sanPham);
            await _sanPhamRepo.UpdateAsync(updatesanPham);
            return sanPham;
        }

        public async Task<bool> DeleteAnhSanPham(Guid imageId, Guid sanPhamId)
        {
            try
            {
                var sanPham = await _dbContext.SanPhams
                    .Include(s => s.AnhSanPhams)
                    .FirstOrDefaultAsync(x => x.Id == sanPhamId);
                if (sanPham == null)
                {
                    return false;
                }

                var anhSanPhamToDelete = sanPham.AnhSanPhams.FirstOrDefault(x => x.Id == imageId);
                if (anhSanPhamToDelete != null)
                {
                    DeleteImageFile(anhSanPhamToDelete.ImageUrl);
                    _dbContext.AnhSanPhams.Remove(anhSanPhamToDelete);
                    await _dbContext.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteSanPham(Guid id)
        {
            await _sanPhamRepo.DeleteAsync(id);
            return true;
        }
        public async Task<bool> UpdateStatus(Guid id)
        {
            await _sanPhamRepo.UpdateStatusAsync(id);
            return true;
        }
    }
}
