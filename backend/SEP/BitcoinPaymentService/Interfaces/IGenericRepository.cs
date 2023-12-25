using BitcoinPaymentService.Models;
using System.Linq.Expressions;

namespace BitcoinPaymentService.Interfaces
{
    public interface IGenericRepository<T> where T : EntityBase
    {
        Task<IQueryable<T>> GetAll();
        Task<T?> Get(Expression<Func<T, bool>> expression, List<string>? includes = null);
        Task Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
