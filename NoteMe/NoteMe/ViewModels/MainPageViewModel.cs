using NoteMe.Models;
using NoteMe.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NoteMe.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly INoteService _noteService;
        private List<Note> _tmpNotes;
        private List<Note> _notes;
        public List<Note> Notes
        {
            get
            {
                return _notes;
            }
            set
            {
                SetProperty(ref _notes, value);
            }
        }
        private Note _selectedNote;
        public Note SelectedNote
        {
            get
            {
                return _selectedNote;
            }
            set
            {
                SetProperty(ref _selectedNote, value);
                if(value != null)
                {
                    Search = null;
                    INavigationParameters parameters = new NavigationParameters();
                    parameters.Add("note", value);
                    NavigationService.NavigateAsync("NoteEntryPage", parameters);
                }
            }
        }
        private string _search;
        public string Search
        {
            get 
            { 
                return _search; 
            }
            set 
            {
                SetProperty(ref _search, value);
                if(_search != null)
                {
                    SearchNote(value);
                }
            }
        }
        public DelegateCommand AddCommand { get; set; }

        public MainPageViewModel(INavigationService navigationService, INoteService noteService)
            : base(navigationService)
        {
            Title = "NoteMe";
            AddCommand = new DelegateCommand(Add);
            _noteService = noteService;
            LoadNotes();
        }

        private void SearchNote(string value)
        {
            value = value.ToUpper();
            Notes = _tmpNotes.Where(note => note.Title.ToUpper().Contains(value) || note.Content.ToUpper().Contains(value)).ToList();
        }

        private void Add()
        {
            NavigationService.NavigateAsync("NoteEntryPage");
        }

        async private void LoadNotes()
        {
            Notes = await _noteService.GetNotesAsync();
            _tmpNotes = Notes;
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            LoadNotes();           
        }
    }
}
