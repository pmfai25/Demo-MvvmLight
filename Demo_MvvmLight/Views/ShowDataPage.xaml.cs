using Demo_MvvmLight.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

namespace Demo_MvvmLight.Views
{
    public sealed partial class ShowDataPage : Page
    {
        private ShowDataViewModel ViewModel
        {
            get { return DataContext as ShowDataViewModel; }
        }

        public ShowDataPage()
        {
            InitializeComponent();
        }

        
    }
}
