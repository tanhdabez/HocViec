using Core.Helper;
using Core.Request;
using Core.Services.Interfaces;
using Infrastructure;
using Infrastructure.Models.Enum;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Core.Response;


namespace Core.Services.Implements
{
    public class AuthenticationService : ICustomAuthenticationService
    {
        private readonly IUserService _UserService;
        private readonly AppDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AuthenticationService(AppDbContext appDbContext, IUserService UserService, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = appDbContext;
            _UserService = UserService;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<UserLoginResult> Login(LoginRequest request)
        {
            var dataUser = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == request.Email);
            if (dataUser != null && Helper.PasswordHelper.VerifyPassword(request.Password, dataUser.Password))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, dataUser.Ten),
                    new Claim(ClaimTypes.Email, dataUser.Email),
                    new Claim(ClaimTypes.Role, dataUser.VaiTro.ToString())
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true
                };

                await _httpContextAccessor.HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                return new UserLoginResult
                {
                    IsSuccess = true,
                    UserId = dataUser.Id.ToString(),
                    Role = dataUser.VaiTro.ToString(),
                    Name = dataUser.Ten,
                    Email = dataUser.Email
                };
            }
            return new UserLoginResult { IsSuccess = false };
        }

        public async Task<bool> Register(RegisterRequest request)
        {
            var User = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == request.Email);
            if (User != null)
            {
                return false;
            }
            var hashedPassword = PasswordHelper.HashPassword(request.Password);
            var newUser = new User
            {
                Ten = request.Ten,
                Email = request.Email,
                Password = hashedPassword,
                TrangThai = true,
                VaiTro = EnumRole.Customer
                
            };
            _dbContext.Users.Add(newUser);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
