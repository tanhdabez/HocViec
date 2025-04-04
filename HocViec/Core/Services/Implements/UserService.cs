using AutoMapper;
using Core.Helper;
using Core.Request;
using Core.Services.Interfaces;
using Infrastructure;
using Infrastructure.Models;
using Infrastructure.Repositories.Implements;
using Infrastructure.Repositories.Interfaces;

namespace Core.Services.Implements
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepo;
        private readonly AppDbContext _dbContext;
        public UserService(IMapper mapper, IUserRepository UserRepo, AppDbContext dbContext)
        {
            _mapper = mapper;
            _userRepo = UserRepo;
            _dbContext = dbContext;
        }

        public async Task<List<UserResponse>> GetAllUser()
        {
            var response = await _userRepo.GetAllAsync();
            return _mapper.Map<List<UserResponse>>(response);
        }

        public async Task<UserResponse> GetUserById(Guid id)
        {
            var response = await _userRepo.GetByIdAsync(id);
            return _mapper.Map<UserResponse>(response);
        }

        public async Task<IEnumerable<HoaDon>> GetHoaDonsByUserAsync(Guid userId)
        {
            return await _userRepo.GetHoaDonsByUserIdAsync(userId);
        }

        public async Task<List<ChiTietHoaDon>> GetChiTietHoaDonsByHoaDonIdAsync(Guid hoaDonId)
        {
            return await _userRepo.GetChiTietByHoaDonIdAsync(hoaDonId);
        }

        public async Task<bool> AddUser(CreateUserRequest request)
        {
            var User = await _userRepo.GetUserByEmailAsync(request.Email);
            if (User != null) return false;
            var newUser = _mapper.Map<User>(request);
            newUser.Password = PasswordHelper.HashPassword(request.Password);
            await _userRepo.AddAsync(newUser);
            return true;
        }

        public async Task<UserResponse?> UpdateUser(UserResponse request)
        {
            var response = await _userRepo.GetByIdAsync(request.Id);
            var checkUser = await _userRepo.CheckEmailExistsForOtherUserAsync(request.Id, request.Email!);
            if (response == null || checkUser) return null;
            var updateUser = _mapper.Map(request, response);
            if (!string.IsNullOrEmpty(request.Password))
            {
                response.Password = PasswordHelper.HashPassword(request.Password);
            }
            await _userRepo.UpdateAsync(response);
            await _dbContext.SaveChangesAsync();
            return _mapper.Map<UserResponse>(response);
        }

        public async Task<bool> UpdateStatusUser(Guid id)
        {
            await _userRepo.UpdateStatusAsync(id);
            return true;
        }
    }
}
