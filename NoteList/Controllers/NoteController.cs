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
            var notes = await _noteService.GetAllNoteAsync();
            return View(notes);
        }

        #endregion

        #region Create

        [HttpPost]
        [Authorize(Policy = "CreateNotePolicy")]
        public async Task<IActionResult> Create(NoteViewModel note)
        {
            NoteValidator validator = new NoteValidator();
            var validationResult = validator.Validate(note);

            if (validationResult.IsValid)
            {
                await _noteService.CreateNoteAsync(note);
                return Ok();
            }

            return BadRequest(ModelState);
        }

        #endregion

        #region GetNote

        [HttpGet]
        public async Task<IActionResult> GetNote(int id)
        {
            var note = await _noteService.GetNoteByIdAsync(id);
            if (note == null)
            {
                return NotFound();
            }

            var noteViewModel = _noteService.MapNoteToNoteViewModel(note);
            return Json(noteViewModel);
        }

        #endregion

        #region Edit

        [HttpPost]
        [Authorize(Policy = "EditNotePolicy")]
        public async Task<IActionResult> Edit(NoteViewModel note)
        {
            NoteValidator validator = new NoteValidator();
            var validationResult = validator.Validate(note);

            if (validationResult.IsValid)
            {
                await _noteService.UpdateNoteAsync(note);
                return Ok();
            }

            return BadRequest(ModelState);
        }

        #endregion

        #region Delete

        [HttpPost]
        [Authorize(Policy = "DeleteNotePolicy")]
        public async Task<IActionResult> Delete(int id)
        {
            var note = await _noteService.GetNoteByIdAsync(id);
            if (note == null)
            {
                return NotFound();
            }

            await _noteService.RemoveNoteAsync(note);
            return Ok();
        }

        #endregion
    }
}
