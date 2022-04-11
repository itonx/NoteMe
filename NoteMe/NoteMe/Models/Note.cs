using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace NoteMe.Models
{
    public class Note
    {
        [PrimaryKey, AutoIncrement]
        public long NoteUUID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }

        public Note()
        {
            Title = null;
            NoteUUID = 0;
            Created = Modified = DateTime.Now;
        }
    }

    public class NoteStaticService
    {
        public static NoteDatabase DB;

        static NoteStaticService()
        {
            DB = new NoteDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "NoteMee.db3"));
        }
    }

    public class NoteDatabase
    {
        readonly SQLiteAsyncConnection database;

        public NoteDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Note>().Wait();
        }

        public Task<List<Note>> GetNotesAsync()
        {
            //Get all notes.
            return database.Table<Note>().ToListAsync();
        }

        public Task<Note> GetNoteAsync(long id)
        {
            // Get a specific note.
            return database.Table<Note>()
                            .Where(i => i.NoteUUID == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveNoteAsync(Note note)
        {
            if (note.NoteUUID != 0)
            {
                // Update an existing note.
                return database.UpdateAsync(note);
            }
            else
            {
                // Save a new note.
                return database.InsertAsync(note);
            }
        }

        public Task<int> DeleteNoteAsync(Note note)
        {
            // Delete a note.
            return database.DeleteAsync(note);
        }
    }
}
