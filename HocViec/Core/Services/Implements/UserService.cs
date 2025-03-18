using AutoMapper;
using Core.Helper;
using Core.Request;
using Core.Services.Interfaces;
using Infrastructure;
using Infrastructure.Models.User;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Core.Services.Implements
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IRepository<User> _UserRepo;
        public UserService(AppDbContext dbContext, IMapper mapper, IRepository<User> UserRepo)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _UserRepo = UserRepo;
        }

        public async Task<List<UserResponse>> GetAllUser()
        {
            var response = await _UserRepo.GetAllAsync();
            return _mapper.Map<List<UserResponse>>(response);
        }

        public async Task<UserResponse> GetUserById(Guid id)
        {
            var response = await _UserRepo.GetByIdAsync(id);
            return _mapper.Map<UserResponse>(response);
        }

        public async Task<bool> AddUser(CreateUserRequest request)
        {
            var User = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == request.Email);
            if (User != null)
            {
                return false;
            }
            var newUser = _mapper.Map<User>(request);
            newUser.Password = PasswordHelper.HashPassword(request.Password);
            await _UserRepo.AddAsync(newUser);
            return true;
        }

        public async Task<UserResponse?> UpdateUser(UserResponse request)
        {
            var response = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (response == null) return null;
            var checkUser = await _dbContext.Users.AnyAsync(x => x.Id != request.Id && x.Email == request.Email);
            if (checkUser) return null;
            var updateUser = _mapper.Map(request, response);
            if (!string.IsNullOrEmpty(request.Password))
            {
                response.Password = PasswordHelper.HashPassword(request.Password);
            }
            await _UserRepo.UpdateAsync(response);
            await _dbContext.SaveChangesAsync();
            return _mapper.Map<UserResponse>(response);
        }

        public async Task<bool> UpdateStatusUser(Guid id)
        {
            await _UserRepo.UpdateStatusAsync(id);
            return true;
        }
    }
}
