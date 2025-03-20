using Infrastructure.Models;

namespace Infrastructure.Repositories.Interfaces
{
    public interface INhaCungCapRepository
    {
        Task<List<NhaCungCap>> GetAllAsync();
        Task<NhaCungCap> GetByIdAsync(Guid id);
        Task<bool> AddAsync(NhaCungCap entity);
        Task<NhaCungCap?> UpdateAsync(NhaCungCap entity);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> UpdateStatusAsync(Guid id);
    }
}
