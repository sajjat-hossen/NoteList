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

        [HttpPost]
        [Authorize(Policy = "CreateTodoListPolicy")]

        public async Task<IActionResult> Create(TodoListViewModel model)
        {
            TodoListValidator validator = new TodoListValidator();
            var validationResult = validator.Validate(model);

            if (validationResult.IsValid)
            {
                await _todoListService.CreateTodoListAsync(model);

                return Ok();
            }

            return BadRequest(ModelState);
        }

        #endregion

        #region GetTodoList

        [HttpGet]
        public async Task<IActionResult> GetTodoList(int id)
        {
            var todoList = await _todoListService.GetTodoListByIdAsync(id);
            if (todoList == null)
            {
                return NotFound();
            }

            var noteViewModel = _todoListService.MapTodoListToTodoListViewModel(todoList);

            return Json(noteViewModel);
        }

        #endregion

        #region Delete

        [HttpPost]
        [Authorize(Policy = "DeleteTodoListPolicy")]

        public async Task<IActionResult> Delete(int id)
        {
            var todoList = await _todoListService.GetTodoListByIdAsync(id);

            if (todoList == null)
            {
                return NotFound();
            }

            await _todoListService.RemoveTodoListAsync(todoList);

            return Ok();

        }

        #endregion

        #region Edit

        [HttpPost]
        [Authorize(Policy = "EditTodoListPolicy")]

        public async Task<IActionResult> Edit(TodoListViewModel model)
        {
            TodoListValidator validator = new TodoListValidator();
            var validationResult = validator.Validate(model);

            if (validationResult.IsValid)
            {
                await _todoListService.UpdateTodoListAsync(model);

                return Ok();
            }

            return BadRequest(ModelState);
        }

        #endregion
    }
}
