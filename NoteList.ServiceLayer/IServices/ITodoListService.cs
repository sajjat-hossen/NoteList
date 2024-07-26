using NoteList.DomainLayer.Models;
using NoteList.ServiceLayer.Models;

namespace NoteList.ServiceLayer.IServices
{
    public interface ITodoListService
    {
        Task<List<TodoListViewModel>> GetAllTodoListAsync();
        Task CreateTodoListAsync(TodoListViewModel model);
        Task<TodoList> GetTodoListByIdAsync(int id);
        Task RemoveTodoListAsync(TodoList todoList);
        Task UpdateTodoListAsync(TodoListViewModel model);
        TodoListViewModel MapTodoListToTodoListViewModel(TodoList todoList);
        TodoList MapTodoListViewModelToTodoList(TodoListViewModel model);
    }
}
