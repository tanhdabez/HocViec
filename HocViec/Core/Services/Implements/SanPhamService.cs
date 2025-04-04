using AutoMapper;
using Core.Request;
using Core.Response;
using Core.Services.Interfaces;
using Infrastructure.Models;
using Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Core.Services.Implements
{
    public class SanPhamService : ISanPhamService
    {
        private readonly IMapper _mapper;
        private readonly ISanPhamRepository _sanPhamRepo;

        public SanPhamService(IMapper mapper, ISanPhamRepository sanPhamRepo)
        {
            _mapper = mapper;
            _sanPhamRepo = sanPhamRepo;
        }

        public async Task<List<SanPhamResponse>> GetAllSanPham_Home()
        {
            var sanPhams = await _sanPhamRepo.GetAllAsync();

            return _mapper.Map<List<SanPhamResponse>>(sanPhams);
        }

        public async Task<List<SanPhamResponse>> GetAllSanPham()
        {
            var sanPhams = await _sanPhamRepo.GetAllSanPhamsWithIncludesAsync();

            return _mapper.Map<List<SanPhamResponse>>(sanPhams);
        }

        public async Task<SanPhamResponse> GetSanPhamById(Guid id)
        {
            var sanPham = await _sanPhamRepo.GetSanPhamWithImagesAsync(id);
            if (sanPham != null)
            {
                return _mapper.Map<SanPhamResponse>(sanPham);

            }
            throw new Exception("Không tìm thấy sản phẩm");
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
                        SanPhamId = sanPham.Id,
                        ImageUrl = imageUrl
                    };
                    await _sanPhamRepo.AddAnhAsync(anhSanPham);
                }
            }
            return true;
        }

        public async Task<SanPhamResponse?> UpdateSanPham(SanPhamResponse request)
        {
            var sanPham = await _sanPhamRepo.GetSanPhamWithImagesAsync(request.Id);
            if (sanPham != null)
            {
                if (request.Images != null && request.Images.Any())
                {
                    foreach (var item in request.Images)
                    {
                        var imageUrl = await SaveImage(item);
                        var anhSanPham = new AnhSanPham
                        {
                            SanPhamId = sanPham.Id,
                            ImageUrl = imageUrl
                        };
                        await _sanPhamRepo.AddAnhAsync(anhSanPham);
                    }
                }
                _mapper.Map(request, sanPham);
                await _sanPhamRepo.UpdateAsync(sanPham);
                return _mapper.Map<SanPhamResponse>(sanPham);
            }
            return null;
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
                DeleteImageFile(imageUrl);
                var image = await _sanPhamRepo.GetImageByUrlAsync(imageUrl);
                if (image != null)
                {
                   await _sanPhamRepo.RemoveImageAsync(image);
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi xoá ảnh: {ex.Message}");
                return false;
            }
        }
    }
}
