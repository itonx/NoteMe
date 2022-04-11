using NoteMe.Properties;
using NoteMe.Services;
using NoteMe.ViewModels;
using NoteMe.Views;
using Prism;
using Prism.Ioc;
using System.Globalization;
using System.Threading;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;

namespace NoteMe
{
    public partial class App
    {
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();            
            LoadLangugage();
            await NavigationService.NavigateAsync("NavigationPage/MainPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<NoteEntryPage, NoteEntryPageViewModel>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();

            containerRegistry.RegisterSingleton<INoteService, LocalNoteService>();
        }

        private void LoadLangugage()
        {
            //CultureInfo language = new CultureInfo("es");
            CultureInfo language = CultureInfo.InstalledUICulture;
            Thread.CurrentThread.CurrentUICulture = language;
            NoteMeeLang.Culture = language;
        }
    }
}
