using NoteMe.Models;
using NoteMe.Services;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NoteMe.ViewModels
{
    public class NoteEntryPageViewModel : ViewModelBase, INavigationAware
    {
        private readonly INoteService _noteService;
        public Note _note;
        public Note NoteToEdit
        {
            get
            {
                return _note;
            }
            set
            {
                SetProperty(ref _note, value);
            }
        }
        public DelegateCommand SaveNoteCommand { get; set; }
        public DelegateCommand DeleteNoteCommand { get; set; }

        public NoteEntryPageViewModel(INavigationService navigationService, INoteService noteService) : base(navigationService)
        {
            NoteToEdit = new Note();
            SaveNoteCommand = new DelegateCommand(SaveNote);
            DeleteNoteCommand = new DelegateCommand(DeleteNoteAsync);
            _noteService = noteService;
        }

        private async void DeleteNoteAsync()
        {
            await _noteService.DeleteNoteAsync(NoteToEdit);
            await NavigationService.GoBackAsync();
        }

        private async void SaveNote()
        {
            await _noteService.SaveNoteAsync(NoteToEdit);
            await NavigationService.GoBackAsync();
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            NoteToEdit = parameters.ContainsKey("note") ? (Note)parameters["note"] : new Note();
        }
    }
}
