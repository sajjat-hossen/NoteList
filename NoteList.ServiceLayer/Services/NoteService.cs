using NoteList.DomainLayer.Models;
using NoteList.RepositoryLayer.IRepositories;
using NoteList.ServiceLayer.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteList.ServiceLayer.Services
{
    public class NoteService : INoteService
    {
        #region Fields

        private readonly INoteRepository _noteRepository;

        #endregion

        #region Constructor

        public NoteService(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        #endregion

        #region GetAllNoteAsync

        public async Task<List<Note>> GetAllNoteAsync()
        {
            var notes = await _noteRepository.GetAllEntityFromDbAsync();

            return notes;
        }

        #endregion

        #region CreateNoteAsync

        public async Task CreateNoteAsync(Note note)
        {
            await _noteRepository.AddEntityAsync(note);
        }

        #endregion

        #region GetNoteByIdAsync

        public async Task<Note> GetNoteByIdAsync(int id)
        {
            var note = await _noteRepository.GetEntityByIdAsync(i => i.Id == id);

            return note;
        }

        #endregion

        public async Task RemoveNoteAsync(Note note)
        {
            await _noteRepository.RemoveEntityAsync(note);
        }

        public async Task UpdateNoteAsync(Note note)
        {
            await _noteRepository.UpdateEntityAsync(note);
        }
    }
}
