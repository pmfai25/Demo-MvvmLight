using Demo_MvvmLight.ViewModels;

using Windows.UI.Xaml.Controls;

namespace Demo_MvvmLight.Views
{
    public sealed partial class AddStuffPage : Page
    {
        private AddStuffViewModel ViewModel
        {
            get { return DataContext as AddStuffViewModel; }
        }

        public AddStuffPage()
        {
            InitializeComponent();
        }
    }
}
