using Demo_MvvmLight.ViewModels;

using Windows.UI.Xaml.Controls;

namespace Demo_MvvmLight.Views
{
    public sealed partial class ShowAccountPage : Page
    {
        private ShowAccountViewModel ViewModel
        {
            get { return DataContext as ShowAccountViewModel; }
        }

        public ShowAccountPage()
        {
            InitializeComponent();
        }
    }
}
