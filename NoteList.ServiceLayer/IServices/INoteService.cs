using NoteList.DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteList.ServiceLayer.IServices
{
    public interface INoteService
    {
        Task<List<Note>> GetAllNoteAsync();
        Task CreateNoteAsync(Note note);
        Task<Note> GetNoteByIdAsync(int id);
    }
}
