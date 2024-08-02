using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoteList.ServiceLayer.IServices;
using NoteList.ServiceLayer.Models;
using NoteList.ServiceLayer.ValidatorModels;

namespace NoteList.Controllers
{
    [Authorize]
    public class TodoListController : Controller
    {
        #region Fields

        private readonly ITodoListService _todoListService;

        #endregion

        #region Constructor

        public TodoListController(ITodoListService todoListService)
        {
            _todoListService = todoListService;
        }

        #endregion

        #region Index

        [Authorize(Policy = "ViewTodoListPolicy")]

        public async Task<IActionResult> Index()
        {
            var todoLists = await _todoListService.GetAllTodoListAsync();

            return View(todoLists);
        }

        #endregion

        #region Create

        [HttpGet]
        [Authorize(Policy = "CreateTodoListPolicy")]

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Policy = "CreateTodoListPolicy")]

        public async Task<IActionResult> Create(TodoListViewModel model)
        {
            TodoListValidator validator = new TodoListValidator();
            var validationResult = validator.Validate(model);

            if (validationResult.IsValid)
            {
                await _todoListService.CreateTodoListAsync(model);

                return RedirectToAction("Index");
            }

            return View();
        }

        #endregion

        #region Delete

        [HttpGet]
        [Authorize(Policy = "DeleteTodoListPolicy")]

        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var todoList = _todoListService.MapTodoListToTodoListViewModel(await _todoListService.GetTodoListByIdAsync(id));

            if (todoList == null)
            {
                return NotFound();
            }

            return View(todoList);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Policy = "DeleteTodoListPolicy")]

        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var todoList = await _todoListService.GetTodoListByIdAsync(id);

            if (todoList == null)
            {
                return NotFound();
            }

            await _todoListService.RemoveTodoListAsync(todoList);

            return RedirectToAction("Index");

        }

        #endregion

        #region Edit

        [HttpGet]
        [Authorize(Policy = "EditTodoListPolicy")]

        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var todoList = _todoListService.MapTodoListToTodoListViewModel(await _todoListService.GetTodoListByIdAsync(id));

            if (todoList == null)
            {
                return NotFound();
            }

            return View(todoList);
        }

        [HttpPost]
        [Authorize(Policy = "EditTodoListPolicy")]

        public async Task<IActionResult> Edit(TodoListViewModel model)
        {
            TodoListValidator validator = new TodoListValidator();
            var validationResult = validator.Validate(model);

            if (validationResult.IsValid)
            {
                await _todoListService.UpdateTodoListAsync(model);

                return RedirectToAction("Index");
            }

            return View();
        }

        #endregion
    }
}
