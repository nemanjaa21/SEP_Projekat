
using AgencyService.Models;
using System.Linq.Expressions;

namespace AgencyService.Interfaces
{
    public interface IGenericRepository<T> where T : EntityBase
    {
        Task<IQueryable<T>> GetAll();
        Task<IList<T>> GetAll(Expression<Func<T, bool>>? expression = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, List<string>? includes = null);
        Task<T?> Get(Expression<Func<T, bool>> expression, List<string>? includes = null);
        Task Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
