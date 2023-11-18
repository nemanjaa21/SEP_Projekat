using AuthService.Models;

namespace AuthService.Interfaces
{
    public interface IGenericRepository<T> where T : User
    {
        Task<IQueryable<T>> GetAll();
        Task<T?> Get(int id);
        Task Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
