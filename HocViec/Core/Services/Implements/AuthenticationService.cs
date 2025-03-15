using Azure.Core;
using Core.Helper;
using Core.Request;
using Core.Services.Interfaces;
using Infrastructure;
using Infrastructure.Models.Enum;
using Infrastructure.Models.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Implements
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserService _UserService;
        private readonly AppDbContext _dbContext;
        public AuthenticationService(AppDbContext appDbContext, IUserService UserService)
        {
            _dbContext = appDbContext;
            _UserService = UserService;
        }
        public async Task<UserLoginResult> Login(string email, string password)
        {
            var dataUser = await _dbContext.Users.FirstOrDefaultAsync(x=>x.Email == email /*&& x.TrangThai == true*/);
            if (dataUser != null && Helper.PasswordHelper.VerifyPassword(password, dataUser.Password))
            {
                return new UserLoginResult
                {
                    IsSuccess = true,
                    Role = dataUser.VaiTro.ToString(), // Chuyển đổi enum thành chuỗi
                    Name = dataUser.Ten,
                    Email = dataUser.Email
                };
            }
            //var User = await _dbContext.Users.FirstOrDefaultAsync(x=>x.Email == email);
            //if (User != null && Helper.PasswordHelper.VerifyPassword(password, User.Password))
            //{
            //    return new UserLoginResult
            //    {
            //        IsSuccess = true,
            //        Role = User.TrangThai.ToString(), // Chuyển đổi enum thành chuỗi
            //        Name = User.Ten,
            //        Email = User.Email
            //    };
            //}
            return new UserLoginResult { IsSuccess = false };
        }

        public async Task<bool> Register(string ten, string email, string password)
        {
            var User = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == email);
            if (User != null)
            {
                return false;
            }
            var hashedPassword = PasswordHelper.HashPassword(password);
            var newUser = new User
            {
                Ten = ten,
                Email = email,
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
