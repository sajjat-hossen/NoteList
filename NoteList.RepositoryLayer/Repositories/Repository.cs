using Microsoft.EntityFrameworkCore;
using NoteList.DomainLayer.Data;
using System.Linq.Expressions;

namespace NoteList.RepositoryLayer.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        #region Fields

        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;

        #endregion

        #region Constructor

        public Repository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        #endregion

        #region AddEntityAsync

        public async Task AddEntityAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        #endregion

        #region GetAllEntityFromDbAsync

        public async Task<List<TEntity>> GetAllEntityFromDbAsync()
        {
            return await _dbSet.ToListAsync();
        }

        #endregion

        #region GetEntityByIdAsync

        public async Task<TEntity> GetEntityByIdAsync(Expression<Func<TEntity, bool>> filters)
        {
            var query = _dbSet.Where(filters);

            return await query.FirstOrDefaultAsync();
        }

        #endregion

        #region RemoveEntity

        public async Task RemoveEntityAsync(TEntity entity)
        {
            _dbSet.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        #endregion

        #region UpdateEntityAsync

        public async Task UpdateEntityAsync(TEntity entity)
        {
            _dbSet.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        #endregion
    }
}
