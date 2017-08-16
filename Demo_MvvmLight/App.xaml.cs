using System;

using Demo_MvvmLight.Services;

using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.Storage;

namespace Demo_MvvmLight
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        public string a;
        private Lazy<ActivationService> _activationService;
        private ActivationService ActivationService { get { return _activationService.Value; } }

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            InitializeComponent();

            //Deferred execution until used. Check https://msdn.microsoft.com/library/dd642331(v=vs.110).aspx for further info on Lazy<T> class.
            _activationService = new Lazy<ActivationService>(CreateActivationService);
        }

        private async void CreateDataBase()
        {
            StorageFile _file = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Database/Data.db"));
            StorageFolder _folder = ApplicationData.Current.LocalFolder;
            try
            {
                StorageFile _fileCheck = await _folder.GetFileAsync("Data.db");
            }
            catch
            {
                await _file.CopyAsync(_folder, "Data.db", NameCollisionOption.ReplaceExisting);
            }
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override async void OnLaunched(LaunchActivatedEventArgs e)
        {
            CreateDataBase();
            if (!e.PrelaunchActivated)
            {
                await ActivationService.ActivateAsync(e); 
            }
        }

        /// <summary>
        /// Invoked when the application is activated by some means other than normal launching.
        /// </summary>
        /// <param name="args">Event data for the event.</param>
        protected override async void OnActivated(IActivatedEventArgs args)
        {
            await ActivationService.ActivateAsync(args);
        }
    
        private ActivationService CreateActivationService()
        {
            return new ActivationService(this, typeof(ViewModels.ShowDataViewModel));
            //return new ActivationService(this, typeof(ViewModels.MainViewModel));
        }
    }
}
