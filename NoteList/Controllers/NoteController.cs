using Microsoft.AspNetCore.Mvc;
using NoteList.DomainLayer.Models;
using NoteList.ServiceLayer.IServices;

namespace NoteList.Controllers
{
    public class NoteController : Controller
    {
        #region Fields

        private readonly INoteService _noteService;

        #endregion

        #region Constructor

        public NoteController(INoteService noteService)
        {
            _noteService = noteService;
        }

        #endregion

        public async Task<IActionResult> Index()
        {
            var notes = await  _noteService.GetAllNoteAsync();

            return View(notes);
        }
    }
}
