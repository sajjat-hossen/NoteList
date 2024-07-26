using NoteList.DomainLayer.Data;
using NoteList.DomainLayer.Models;
using NoteList.RepositoryLayer.IRepositories;

namespace NoteList.RepositoryLayer.Repositories
{
    public class TodoListRepository : Repository<TodoList>, ITodoListRepository
    {
        #region Fields

        private readonly ApplicationDbContext _dbContext;

        #endregion

        #region Constructor

        public TodoListRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        #endregion
    }
}
