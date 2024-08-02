using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoteList.ServiceLayer.IServices;
using NoteList.ServiceLayer.Models;
using NoteList.ServiceLayer.ValidatorModels;

namespace NoteList.Controllers
{
    [Authorize]
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

        #region Index

        [Authorize(Policy = "ViewNotePolicy")]
        public async Task<IActionResult> Index()
        {
            var notes = await  _noteService.GetAllNoteAsync();

            return View(notes);
        }

        #endregion

        #region Create

        [HttpGet]
        [Authorize(Policy = "CreateNotePolicy")]

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Policy = "CreateNotePolicy")]
        public async Task<IActionResult> Create(NoteViewModel note)
        {
            NoteValidator validator = new NoteValidator();
            var validationResult = validator.Validate(note);

            if (validationResult.IsValid)
            {
                await _noteService.CreateNoteAsync(note);

                return RedirectToAction("Index");
            }

            return View();
        }

        #endregion

        #region Delete

        [HttpGet]
        [Authorize(Policy = "DeleteNotePolicy")]

        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var note = _noteService.MapNoteToNoteViewModel(await _noteService.GetNoteByIdAsync(id));

            if (note == null)
            {
                return NotFound();
            }

            return View(note);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Policy = "DeleteNotePolicy")]

        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var note = await _noteService.GetNoteByIdAsync(id);

            if (note == null)
            {
                return NotFound();
            }

            await _noteService.RemoveNoteAsync(note);

            return RedirectToAction("Index");

        }

        #endregion

        #region Edit

        [HttpGet]
        [Authorize(Policy = "EditNotePolicy")]

        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var note = _noteService.MapNoteToNoteViewModel (await _noteService.GetNoteByIdAsync(id));

            if (note == null)
            {
                return NotFound();
            }

            return View(note);
        }

        [HttpPost]
        [Authorize(Policy = "EditNotePolicy")]

        public async Task<IActionResult> Edit(NoteViewModel note)
        {
            NoteValidator validator = new NoteValidator();
            var validationResult = validator.Validate(note);

            if (validationResult.IsValid)
            {
                await _noteService.UpdateNoteAsync(note);

                return RedirectToAction("Index");
            }

            return View();
        }

        #endregion
    }
}
