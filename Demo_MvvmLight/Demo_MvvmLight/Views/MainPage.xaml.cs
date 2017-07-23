using Demo_MvvmLight.ViewModels;

using Windows.UI.Xaml.Controls;

namespace Demo_MvvmLight.Views
{
    public sealed partial class MainPage : Page
    {
        private MainViewModel ViewModel
        {
            get { return DataContext as MainViewModel; }
        }

        public MainPage()
        {
            InitializeComponent();
        }
    }
}
