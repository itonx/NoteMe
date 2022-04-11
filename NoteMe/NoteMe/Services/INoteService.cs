using NoteMe.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NoteMe.Services
{
    public interface INoteService
    {
        Task<List<Note>> GetNotesAsync();
        Task<Note> GetNoteAsync(long id);
        Task<int> SaveNoteAsync(Note note);
        Task<int> DeleteNoteAsync(Note note);
    }
}
