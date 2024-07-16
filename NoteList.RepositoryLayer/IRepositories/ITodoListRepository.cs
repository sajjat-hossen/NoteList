using NoteList.DomainLayer.Models;
using NoteList.RepositoryLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteList.RepositoryLayer.IRepositories
{
    public interface ITodoListRepository : IRepository<TodoList>
    {
    }
}
