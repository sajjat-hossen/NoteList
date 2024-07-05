﻿using NoteList.DomainLayer.Models;
using NoteList.RepositoryLayer.Repositories;

namespace NoteList.RepositoryLayer.IRepositories
{
    public interface INoteRepository : IRepository<Note>
    {
        void UpdateNote(Note note);
        Task SaveNoteAsync();
    }
}
