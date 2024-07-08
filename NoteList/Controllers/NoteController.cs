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

        #region Index

        public async Task<IActionResult> Index()
        {
            var notes = await  _noteService.GetAllNoteAsync();

            return View(notes);
        }

        #endregion

        #region Create

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Note note)
        {
            if (ModelState.IsValid)
            {
                await _noteService.CreateNoteAsync(note);

                return RedirectToAction("Index");
            }

            return View();
        }

        #endregion

        #region Delete

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var note = await _noteService.GetNoteByIdAsync(id);

            if (note == null)
            {
                return NotFound();
            }

            return View(note);
        }

        [HttpPost, ActionName("Delete")]
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
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var note =(Note) await _noteService.GetNoteByIdAsync(id);

            if (note == null)
            {
                return NotFound();
            }

            return View(note);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Note note)
        {
            if (ModelState.IsValid)
            {
                await _noteService.UpdateNoteAsync(note);

                return RedirectToAction("Index");
            }

            return View();
        }

        #endregion
    }
}
