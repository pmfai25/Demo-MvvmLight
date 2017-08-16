using Demo_MvvmLight.ViewModels;

using Windows.UI.Xaml.Controls;

namespace Demo_MvvmLight.Views
{
    public sealed partial class CreateAccountPage : Page
    {
        private CreateAccountViewModel ViewModel
        {
            get { return DataContext as CreateAccountViewModel; }
        }

        public CreateAccountPage()
        {
            InitializeComponent();
        }
    }
}
