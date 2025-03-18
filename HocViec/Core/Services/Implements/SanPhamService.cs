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
        public async Task<List<SanPhamResponse>> GetAllSanPham()
        {
            var sanPhams = await _dbContext.SanPhams.Include(sp => sp.NhaCungCap).Include(sp => sp.DanhMucLoaiHang).Include(sp => sp.AnhSanPhams).ToListAsync();

            return _mapper.Map<List<SanPhamResponse>>(sanPhams);
        }
        public async Task<SanPhamResponse> GetSanPhamById(Guid id)
        {
            var sanPham = await _dbContext.SanPhams.Include(sp => sp.NhaCungCap).Include(sp => sp.DanhMucLoaiHang).Include(sp => sp.AnhSanPhams).FirstOrDefaultAsync(sp => sp.Id == id);

            if (sanPham == null)
            {
                throw new Exception("Không tìm thấy sản phẩm");
            }

            return _mapper.Map<SanPhamResponse>(sanPham);
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

        public async Task<SanPhamResponse?> UpdateSanPham(SanPhamResponse request)
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
            _mapper.Map(request, sanPham); // Ánh xạ dữ liệu từ request sang sanPham
            await _sanPhamRepo.UpdateAsync(sanPham); // Cập nhật sản phẩm trong database

            // Trả về thông tin sản phẩm đã cập nhật dưới dạng SanPhamResponse
            return _mapper.Map<SanPhamResponse>(sanPham);
        }

        private void DeleteImageFile(string imageUrl)
        {
            var filePath = Path.Combine("wwwroot", imageUrl.TrimStart('/'));
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
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

        public async Task<bool> DeleteImageAsync(string imageUrl)
        {
            try
            {
                // Xoá file ảnh từ thư mục
                DeleteImageFile(imageUrl);

                // Xoá ảnh từ database (nếu cần)
                var image = await _dbContext.AnhSanPhams.FirstOrDefaultAsync(x => x.ImageUrl == imageUrl);
                if (image != null)
                {
                    _dbContext.AnhSanPhams.Remove(image);
                    await _dbContext.SaveChangesAsync();
                }

                return true;
            }
            catch (Exception ex)
            {
                // Log lỗi nếu cần
                Console.WriteLine($"Lỗi khi xoá ảnh: {ex.Message}");
                return false;
            }
        }
    }
}
