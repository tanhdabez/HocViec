using Infrastructure.Models;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IDanhMucLoaiHangRepository
    {
        Task<List<DanhMucLoaiHang>> GetAllAsync();
        Task<DanhMucLoaiHang> GetByIdAsync(Guid id);
        Task<bool> AddAsync(DanhMucLoaiHang entity);
        Task<DanhMucLoaiHang?> UpdateAsync(DanhMucLoaiHang entity);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> UpdateStatusAsync(Guid id);
    }
}
