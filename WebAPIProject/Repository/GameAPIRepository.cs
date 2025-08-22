namespace WebAPIProject.Repository
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using WebAPIProject.Interface;
    using WebAPIProject.Models;

    public class GameAPIRepository<T, R> : IGameAPI<T, string> where T : class
    {
        private readonly GameContext _context;
        private readonly DbSet<T> dbSetTables;

        public GameAPIRepository(GameContext context)
        {
            _context = context;
            dbSetTables = _context.Set<T>();
        }

        public async Task<T?> GetByIdAsync(string id)
        {
            return await dbSetTables.FindAsync(id);
        }

        public async Task<T> AddAsync(T entity)
        {

            await dbSetTables.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            dbSetTables.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null) return false;

            dbSetTables.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await dbSetTables.ToListAsync();
        }
    }
}
