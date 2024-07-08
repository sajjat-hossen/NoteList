using NoteList.DomainLayer.Data;
using NoteList.DomainLayer.Models;
using NoteList.RepositoryLayer.IRepositories;

namespace NoteList.RepositoryLayer.Repositories
{
    public class NoteRepository : Repository<Note>, INoteRepository
    {
        #region Fields

        private readonly ApplicationDbContext _dbContext;

        #endregion

        #region Constructor

        public NoteRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        #endregion

    }
}
