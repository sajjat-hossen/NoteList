using NoteList.DomainLayer.Models;

namespace NoteList.ServiceLayer.IServices
{
    public interface INoteService
    {
        Task<List<Note>> GetAllNoteAsync();
        Task CreateNoteAsync(Note note);
        Task<Note> GetNoteByIdAsync(int id);
        Task RemoveNoteAsync(Note note);
        Task UpdateNoteAsync(Note note);
    }
}
