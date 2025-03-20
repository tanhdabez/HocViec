using Core.Request;

namespace Core.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<UserResponse>> GetAllUser();
        Task<UserResponse> GetUserById(Guid id);

        Task<bool> AddUser(CreateUserRequest request);

        Task<UserResponse?> UpdateUser(UserResponse request);

        Task<bool> UpdateStatusUser(Guid id);
    }
}
