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
}
