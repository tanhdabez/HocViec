using Core.Request;
using Infrastructure.Models;

namespace Core.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<UserResponse>> GetAllUser();

        Task<UserResponse> GetUserById(Guid id);

        Task<IEnumerable<HoaDon>> GetHoaDonsByUserAsync(Guid userId);

        Task<List<ChiTietHoaDon>> GetChiTietHoaDonsByHoaDonIdAsync(Guid hoaDonId);

        Task<bool> AddUser(CreateUserRequest request);

        Task<UserResponse?> UpdateUser(UserResponse request);

        Task<bool> UpdateStatusUser(Guid id);
    }
}
