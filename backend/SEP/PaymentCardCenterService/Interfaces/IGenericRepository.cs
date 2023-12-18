using PaymentCardCenterService.Models;
using System.Linq.Expressions;

namespace PaymentCardCenterService.Interfaces
{
    public interface IGenericRepository <T> where T : Bank
    {
        Task<IQueryable<T>> GetAll();
        Task<T?> Get(Expression<Func<T, bool>> expression, List<string>? includes = null);
        Task Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
