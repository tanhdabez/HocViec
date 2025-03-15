using Core.Request;
using Infrastructure.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
