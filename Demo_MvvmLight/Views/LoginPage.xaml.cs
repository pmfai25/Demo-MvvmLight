using Demo_MvvmLight.ViewModels;

using Windows.UI.Xaml.Controls;

namespace Demo_MvvmLight.Views
{
    public sealed partial class LoginPage : Page
    {
        private LoginViewModel ViewModel
        {
            get { return DataContext as LoginViewModel; }
        }

        public LoginPage()
        {
            InitializeComponent();
        }
        
    }
}
