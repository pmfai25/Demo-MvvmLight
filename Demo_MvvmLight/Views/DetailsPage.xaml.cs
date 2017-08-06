using Demo_MvvmLight.ViewModels;

using Windows.UI.Xaml.Controls;

namespace Demo_MvvmLight.Views
{
    public sealed partial class DetailsPage : Page
    {
        private DetailsViewModel ViewModel
        {
            get { return DataContext as DetailsViewModel; }
        }

        public DetailsPage()
        {
            InitializeComponent();
        }
    }
}
