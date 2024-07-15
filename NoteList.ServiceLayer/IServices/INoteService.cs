using NoteList.DomainLayer.Models;
using NoteList.ServiceLayer.Models;

namespace NoteList.ServiceLayer.IServices
{
    public interface INoteService
    {
        Task<List<NoteViewModel>> GetAllNoteAsync();
        Task CreateNoteAsync(NoteViewModel note);
        Task<Note> GetNoteByIdAsync(int id);
        Task RemoveNoteAsync(Note note);
        Task UpdateNoteAsync(NoteViewModel note);
        NoteViewModel MapNoteToNoteViewModel(Note note);
        Note MapNoteViewModelToNote(NoteViewModel model);
    }
}
