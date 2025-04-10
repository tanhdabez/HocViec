using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Interfaces
{
    public interface ISanPhamRepository
    {
        Task<List<SanPham>> GetAllAsync();
        Task<List<SanPham>> GetAllWithLoaiHang(Guid? loaiHang);
        Task<List<SanPham>> GetAllSanPhamsWithIncludesAsync();
        Task<SanPham> GetByIdAsync(Guid id);
        Task<SanPham> GetSanPhamWithImagesAsync(Guid sanPhamId);
        Task<AnhSanPham> GetImageByUrlAsync(string imageUrl);
        Task<SanPham> GetSanPhamDetailsAsync(Guid sanPhamId);
        Task<bool> AddAsync(SanPham entity);
        Task<bool> AddAnhAsync(AnhSanPham entity);
        Task<SanPham?> UpdateAsync(SanPham entity);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> UpdateStatusAsync(Guid id);
        Task RemoveImageAsync(AnhSanPham image);
        string GenerateMa(string prefix);
    }
}
