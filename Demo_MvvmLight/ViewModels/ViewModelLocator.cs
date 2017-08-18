
using Demo_MvvmLight.Services;
using Demo_MvvmLight.Views;

using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;

using Microsoft.Practices.ServiceLocation;

using Windows.UI.Xaml.Shapes;

//using System.IO;

namespace Demo_MvvmLight.ViewModels
{
    public class ViewModelLocator
    {
        NavigationServiceEx _navigationService = new NavigationServiceEx();
        string _sqlConnection = Windows.Storage.ApplicationData.Current.LocalFolder.Path + "\\Data.db";
        
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<IData, Data>();
            SimpleIoc.Default.Register(() => _sqlConnection,"sqlString");
            SimpleIoc.Default.Register(() => _navigationService);
            Register<MainViewModel, MainPage>();
            Register<ShowDataViewModel, ShowDataPage>();
            Register<CreateAccountViewModel, CreateAccountPage>();
            Register<DetailsViewModel, DetailsPage>();
            Register<AddStuffViewModel, AddStuffPage>();
        }

        public AddStuffViewModel AddStuffViewModel => ServiceLocator.Current.GetInstance<AddStuffViewModel>();

        public DetailsViewModel DetailsViewModel => ServiceLocator.Current.GetInstance<DetailsViewModel>();
        
        
        public CreateAccountViewModel CreateAccountViewModel => ServiceLocator.Current.GetInstance<CreateAccountViewModel>();

        public ShowDataViewModel ShowDataViewModel => ServiceLocator.Current.GetInstance<ShowDataViewModel>();

        public MainViewModel MainViewModel => ServiceLocator.Current.GetInstance<MainViewModel>();

        public void Register<VM, V>() where VM : class
        {
            SimpleIoc.Default.Register<VM>();
            _navigationService.Configure(typeof(VM).FullName, typeof(V));
        }
    }
}
