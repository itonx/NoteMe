using NoteMe.Models;
using NoteMe.Properties;
using NoteMe.Services;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NoteMe.ViewModels
{
    public class NoteEntryPageViewModel : ViewModelBase, INavigationAware
    {
        private readonly INoteService _noteService;
        private readonly IPageDialogService _dialogService;
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

        public NoteEntryPageViewModel(INavigationService navigationService, INoteService noteService, IPageDialogService dialogService) : base(navigationService)
        {
            NoteToEdit = new Note();
            SaveNoteCommand = new DelegateCommand(SaveNote);
            DeleteNoteCommand = new DelegateCommand(DeleteNoteAsync);
            _noteService = noteService;
            _dialogService = dialogService;
        }

        private async void DeleteNoteAsync()
        {
            bool delete = await _dialogService.DisplayAlertAsync(NoteMeeLang.DialogAlert, NoteMeeLang.DialogDeleteNoteQuestion, NoteMeeLang.DialogButtonContinue, NoteMeeLang.DialogButtonCancel);

            if (delete)
            {
                int result = await _noteService.DeleteNoteAsync(NoteToEdit);

                if(result > 0)
                {
                    await _dialogService.DisplayAlertAsync(NoteMeeLang.DialogAlert, NoteMeeLang.DialogDeleteNoteConfirmation, NoteMeeLang.DialogButtonOK);
                }
                else
                {
                    await _dialogService.DisplayAlertAsync(NoteMeeLang.DialogAlert, NoteMeeLang.DialogDeleteNoteError, NoteMeeLang.DialogButtonOK);
                }
                await NavigationService.GoBackAsync();
            }
        }

        private async void SaveNote()
        {
            int result = await _noteService.SaveNoteAsync(NoteToEdit);
            if (result > 0)
            {
                await _dialogService.DisplayAlertAsync(NoteMeeLang.DialogAlert, NoteMeeLang.DialogSaveNoteConfirmation, NoteMeeLang.DialogButtonOK);
            }
            else
            {
                await _dialogService.DisplayAlertAsync(NoteMeeLang.DialogAlert, NoteMeeLang.DialogSaveNoteError, NoteMeeLang.DialogButtonOK);
            }
            await NavigationService.GoBackAsync();
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            NoteToEdit = parameters.ContainsKey("note") ? (Note)parameters["note"] : new Note();
        }
    }
}
