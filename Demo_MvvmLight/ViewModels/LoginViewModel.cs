using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight;
using System.Reflection;
using System.IO;

using System.Composition.Hosting;
using System.Composition;
using GalaSoft.MvvmLight.Ioc;
using Demo_MvvmLight.Models;
using GalaSoft.MvvmLight.Messaging;
using System.Windows.Input;
using Microsoft.Practices.ServiceLocation;
using Demo_MvvmLight.Services;

namespace Demo_MvvmLight.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        
        private ICommand _click_Add;
        private ICommand _click_Item;
        private ICommand _click_ShowAllStuff;
        private IData _dataProviders;
        private ObservableCollection< Stuff> _dataStuff;
        private string _addressData;
       
        


        public LoginViewModel(IData data )
        {
            this.DataProviders = data;
        }
       
        
//         public ICommand Click {
//             get {
//                 click = new RelayCommand(() => 
//                 {
//                     Messenger.Default.Register<MEssenger.ButtonMessage>(Cc, (p) =>
//                     {
//                         Method(p);
//                     });
//                 });
//                 return click;
//             }
//             }

        public ICommand Click_Add { get {
                _click_Add = new RelayCommand(() => { NavigationService.Navigate(typeof(AddStuffViewModel).FullName); });
                return _click_Add;
            }
        }

        public ICommand Click_ShowAllStuff {
            get
            {
                _click_ShowAllStuff = new RelayCommand(() =>
                {
                    DataStuff = new ObservableCollection< Stuff>(DataProviders.GetAllStuff(AddressData));
                    RaisePropertyChanged("DataStuff");
                    RaisePropertyChanged("iD");
                });
                return _click_ShowAllStuff;
            }
        }

        public ObservableCollection< Stuff> DataStuff { get => _dataStuff; set => _dataStuff = value; }
        public IData DataProviders { get => _dataProviders; set => _dataProviders = value; }
        public string AddressData {
            get
            {
                _addressData = ServiceLocator.Current.GetInstance<string>("sqlString");
                return _addressData;
            }
            set { _addressData = value; }
        }

        public ICommand Click_Item { get {
                _click_Item = new RelayCommand<object>((p) =>
                {
                    
                    var IdStuff = (p as Stuff).ID.ToString();
                    SimpleIoc.Default.Register(() => IdStuff, "Id");
                    NavigationService.Navigate(typeof(DetailsViewModel).FullName);
                    
                });
                return _click_Item;

            }
        }
        public NavigationServiceEx NavigationService
        {
            get
            {

                return ServiceLocator.Current.GetInstance<NavigationServiceEx>();
            }
        }
        
        //         public void Method(ButtonMessage bt)
        //         {
        //             Cc = bt.ButtonText;
        //         }
        //         private ICommand click;
    }
}