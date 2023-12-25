using AuthService.Models;

namespace AuthService.Interfaces
{
    public interface IAuthService
    {
        Task<string> Login(User user);
    }
}
