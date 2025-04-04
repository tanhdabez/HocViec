using Core.Request;
using Core.Response;

namespace Core.Services.Interfaces
{
    public interface ICustomAuthenticationService
    {
        Task<UserLoginResult> Login(LoginRequest request);
        Task<bool> Register(RegisterRequest request);
    }
}
