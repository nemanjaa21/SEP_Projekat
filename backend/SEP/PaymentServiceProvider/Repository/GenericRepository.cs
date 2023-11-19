using PaymentServiceProvider.Interfaces;
using PaymentServiceProvider.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace PaymentServiceProvider.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : PaymentService
    {
        private readonly DbContext _dbContext;
        private DbSet<T> entities;
        public GenericRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
            entities = dbContext.Set<T>();
        }
        public async Task<IQueryable<T>> GetAll()
        {
            return await Task.FromResult(entities);
        }

        public async Task<T?> Get(Expression<Func<T, bool>> expression, List<string>? includes = null)
        {
            IQueryable<T> query = entities;
            if (includes != null)
            {
                foreach (var includeProperty in includes)
                {
                    query = query.Include(includeProperty);
                }
            }

            return (await query.FirstOrDefaultAsync(expression))!;
        }

        public async Task Insert(T entity)
        {
            await entities.AddAsync(entity);
        }

        public void Update(T entity)
        {
            entities.Update(entity);
        }
        public void Delete(T entity)
        {
            entities.Remove(entity);
        }
    }
}
