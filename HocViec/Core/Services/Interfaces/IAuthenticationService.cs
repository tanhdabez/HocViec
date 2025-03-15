using Core.Request;

namespace Core.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<UserLoginResult> Login(string email, string password);
        Task<bool> Register(string ten, string email, string password);
    }
}
