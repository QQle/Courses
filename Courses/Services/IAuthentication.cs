using Courses.Models;

namespace Courses.Services
{
    public interface IAuthentication
    {
        Task<LoginResponse> Login(Login user);
        Task<LoginResponse> RefreshToken(RefreshTokenModel tokenModel);
        Task<bool> Registration(Login user); 
    }
}
