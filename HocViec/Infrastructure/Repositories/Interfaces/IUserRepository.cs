using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync();

        Task<User?> GetUserByEmailAsync(string obj);

        Task<User> GetByIdAsync(Guid id);

        Task<IEnumerable<HoaDon>> GetHoaDonsByUserIdAsync(Guid userId);

        Task<List<ChiTietHoaDon>> GetChiTietByHoaDonIdAsync(Guid hoaDonId);

        Task<bool> AddAsync(User entity);

        Task<User?> UpdateAsync(User entity);

        Task<bool> DeleteAsync(Guid id);

        Task<bool> UpdateStatusAsync(Guid id);

        Task<bool> CheckEmailExistsForOtherUserAsync(Guid userId, string email);
    }
}
