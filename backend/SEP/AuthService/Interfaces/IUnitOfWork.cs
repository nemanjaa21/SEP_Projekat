using AuthService.Models;

namespace AuthService.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<User> UserRepository { get; }
        Task Save();
    }
}
