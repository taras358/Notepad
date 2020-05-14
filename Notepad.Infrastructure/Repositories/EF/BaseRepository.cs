using Microsoft.EntityFrameworkCore;
using Notepad.Core.Entities;
using Notepad.Core.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notepad.Infrastructure.Repositories.EF
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected AppDbContext _appDbContext;
        protected DbSet<TEntity> _dbSet;

        public BaseRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _dbSet = _appDbContext.Set<TEntity>();
        }
        public async Task<string> Add(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await _appDbContext.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<int> AddRange(List<TEntity> entities)
        {
            _dbSet.AddRange(entities);
            await _appDbContext.SaveChangesAsync();
            return entities.Count;
        }

        public async Task<string> Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
            await _appDbContext.SaveChangesAsync();
            return entity.Id;
        }

        public async Task DeleteRange(ICollection<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<List<TEntity>> GetAll()
        {
            return await _dbSet
                .ToListAsync();
        }

        public async Task<TEntity> GetById(string id)
        {
            return await _dbSet
                .Where(entity => entity.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task Update(TEntity entity)
        {
            _dbSet.Update(entity);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task UpdateRange(List<TEntity> entity)
        {
            _dbSet.UpdateRange(entity);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
