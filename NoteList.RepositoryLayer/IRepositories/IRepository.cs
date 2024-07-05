namespace NoteList.RepositoryLayer.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<List<TEntity>> GetAllEntityFromDbAsync();
        Task<TEntity> GetEntityByIdAsync(int id);
        Task AddEntityAsync(TEntity entity);
        void RemoveEntity(TEntity entity);
    }
}
