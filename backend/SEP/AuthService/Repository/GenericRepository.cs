﻿using AuthService.Interfaces;
using AuthService.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : User
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

        public async Task<T?> Get(int id)
        {
            return await entities.FirstOrDefaultAsync(x => x.Id == id);
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
