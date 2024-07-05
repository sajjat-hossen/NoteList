using Microsoft.EntityFrameworkCore;
using NoteList.DomainLayer.Data;

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
        }

        #endregion

        #region GetAllEntityFromDbAsync

        public async Task<List<TEntity>> GetAllEntityFromDbAsync()
        {
            return await _dbSet.ToListAsync();
        }

        #endregion

        #region GetEntityByIdAsync

        public async Task<TEntity> GetEntityByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        #endregion

        #region RemoveEntity

        public void RemoveEntity(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        #endregion
    }
}
