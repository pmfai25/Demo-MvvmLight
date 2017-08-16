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
using System.Composition;
using System.Composition.Hosting;
using GalaSoft.MvvmLight.Ioc;
using Demo_MvvmLight.Models;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using System.Windows.Input;
using Microsoft.Practices.ServiceLocation;
using Demo_MvvmLight.Services;
using Windows.UI.Xaml;
using Windows.Foundation;
using Windows.UI.Xaml.Controls.Primitives;

namespace Demo_MvvmLight.ViewModels
{
    public class ShowDataViewModel : ViewModelBase
    {
        
        private ICommand _click_Add;
        private ICommand _click_Item;
        private ICommand _click_ResetDataStuff;
        private IData _dataProviders;
        private ObservableCollection< Stuff> _dataStuff;
        private string _addressData;
        private string _IdStuffPass;
        private ICommand _rightTapped;
        private Point _mousePoint;
        private ICommand _mouseEnterLayoutRoot;
        private ICommand _loaded_ShowData;

        public ShowDataViewModel(IData data )
        {
            this.DataProviders = data;
        }
       

        public ICommand Click_Add { get {
                _click_Add = new RelayCommand(() => { NavigationService.Navigate(typeof(AddStuffViewModel).FullName); });
                return _click_Add;
            }
        }

        public ICommand Click_ResetDataStuff {
            get
            {
                _click_ResetDataStuff = new RelayCommand(() =>
                {
                    
                    DataStuff = new ObservableCollection< Stuff>(DataProviders.GetAllStuff(AddressData));
                    RaisePropertyChanged("DataStuff");
                    //RaisePropertyChanged("iD");
                });
                return _click_ResetDataStuff;
            }
        }
        public ObservableCollection< Stuff> DataStuff { get => _dataStuff; set => _dataStuff = value; }
        public IData DataProviders { get => _dataProviders;set => _dataProviders = value; }
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
                    IdStuffPass = (p as Stuff).ID.ToString();
                    bool ischeck = SimpleIoc.Default.IsRegistered<string>("IdStuff");
                    if (ischeck==false)
                    {
                        SimpleIoc.Default.Register(() => IdStuffPass, "IdStuff");
                    }
                    else
                    {
                        SimpleIoc.Default.Unregister<string>("IdStuff");
                        SimpleIoc.Default.Register(() => IdStuffPass, "IdStuff");
                    }
                    
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

        public string IdStuffPass { get => _IdStuffPass; set => _IdStuffPass = value; }
        public ICommand RightTapped { get {
                _rightTapped = new RelayCommand<Tuple<object,RightTappedRoutedEventArgs>>((p) => 
                {
                     var sender = p.Item1 as Button;
                     var e = p.Item2;
                   
                    // If you need the clicked element:
                    // Item whichOne = senderElement.DataContext as Item;

                    //var b = (ListViewItemPresenter)p.OriginalSource;
                    //ListViewItem l =(ListViewItem) b.Content;
                    //FlyoutBase.ShowAttachedFlyout(l);
                    //a.ShowAt(b);
                    //var UieleClollec = p.ItemsPanelRoot.Children;
                    //UieleClollec.
                    MenuFlyout MeFlyout = new MenuFlyout();
                    MenuFlyoutItem MnFtItem = new MenuFlyoutItem()
                    {
                        Text = "About",
                    };MeFlyout.Placement = FlyoutPlacementMode.Bottom;
                    MeFlyout.Items.Add(MnFtItem);
                    //Flyout a = new Flyout();
                    MeFlyout.ShowAt(sender as UIElement, e.GetPosition(sender as UIElement));
                    //MeFlyout.ShowAt(p as Grid, MousePoint);
                    //int bug = 0;
                    //m.ShowAt((FrameworkElement)p);
                    //                     ContentDialog noWifiDialog = new ContentDialog
                    //                     {
                    //                         Title = "No wifi connection",
                    //                         Content = "Check your connection and try again.",
                    //                         
                    //                         
                    //                         
                    //                     };
                    // 
                    //                     ContentDialogResult result = await noWifiDialog.ShowAsync();
                });
                return _rightTapped;
            }
        }

        public ICommand MouseEnterLayoutRoot { get
            {
                _mouseEnterLayoutRoot = new RelayCommand<Point>((p) => 
                {
                    MousePoint = p;
                });

                return _mouseEnterLayoutRoot;
            }
        }

        public Point MousePoint { get => _mousePoint; set => _mousePoint = value; }
        public ICommand Loaded_ShowData
        {
            get
            {
                _loaded_ShowData = new RelayCommand(() =>
                {
                    DataStuff = new ObservableCollection<Stuff>(DataProviders.GetAllStuff(AddressData));
                    RaisePropertyChanged("DataStuff");
                });
                return _loaded_ShowData;
            }
        }
    }
}
