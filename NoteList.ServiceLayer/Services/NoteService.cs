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

        public async Task<List<Note>> GetAllNoteAsync()
        {
            var notes = await _noteRepository.GetAllEntityFromDbAsync();

            return notes;
        }
    }
}
