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
        #region variable
        private ICommand _click_Add;
        private ICommand _click_Item;
        private ICommand _click_ResetDataStuff;
        private ICommand _mouseEnterLayoutRoot;
        private ICommand _loaded_ShowData;
        private ICommand _menuClickDelete;
        private ICommand _holdingTapped_Menu;
        private ICommand _tapped_SelectMode;
        private ICommand _MultiDelete;
        private IData _dataProviders;
        private Stuff _holdingSelect;
        private ObservableCollection<Stuff> _dataStuff;
        private List<Stuff> _stuffBeSelect;
        private bool _isCheckAdmin;
        private bool _isEnabled;
        private ListViewSelectionMode _checkSelectionMode;
        private bool _isCheckMultiChechkBoxSelectMode;
        private string _userName;
        private string _addressData;
        private string _IdStuffPass;
        private Point _mousePoint;
        private Visibility _isVisibility;

        #endregion

        public ShowDataViewModel(IData data)
        {
            this.DataProviders = data;
        }
        #region Properties

        # region Command
        public ICommand Click_Add
        {
            get
            {
                _click_Add = new RelayCommand(() => { NavigationService.Navigate(typeof(AddStuffViewModel).FullName); });
                return _click_Add;
            }
        }

        public ICommand Click_ResetDataStuff
        {
            get
            {
                _click_ResetDataStuff = new RelayCommand(() =>
                {

                    DataStuff = new ObservableCollection<Stuff>(DataProviders.GetAllStuff(AddressData));
                    RaisePropertyChanged("DataStuff");
                    //RaisePropertyChanged("iD");
                });
                return _click_ResetDataStuff;
            }
        }

        public ICommand Click_Item
        {
            get
            {
                _click_Item = new RelayCommand<object>((p) =>
                {

                    if (IsCheckMultiChechkBoxSelectMode == false)
                    {
                        IdStuffPass = (p as Stuff).ID.ToString();
                        bool ischeck = SimpleIoc.Default.IsRegistered<string>("IdStuff");
                        if (ischeck)
                        {
                            SimpleIoc.Default.Unregister<string>("IdStuff");
                        }
                        SimpleIoc.Default.Register(() => IdStuffPass, "IdStuff");
                        NavigationService.Navigate(typeof(DetailsViewModel).FullName);
                    }

                });
                return _click_Item;
            }
        }
        public ICommand HoldingTapped_Menu
        {

            get
            {
                _holdingTapped_Menu = new RelayCommand<Tuple<object, HoldingRoutedEventArgs>>((p) =>
                 {
                     var sender = p.Item1;
                     var e = p.Item2;
                     var MenuContext = (sender as ListView).Resources.Where(a => a.Key.ToString().Equals("A")).Single();
                     (MenuContext.Value as MenuFlyout).ShowAt(sender as ListView, e.GetPosition(sender as ListView));
                     HoldingSelect = e.OriginalSource is TextBlock ? ((e.OriginalSource as TextBlock).DataContext as Stuff) :
                   ((e.OriginalSource as ListViewItemPresenter).DataContext as Stuff);
                 });
                return _holdingTapped_Menu;
            }
        }
        public ICommand Loaded_ShowData
        {
            get
            {
                _loaded_ShowData = new RelayCommand(() =>
                {
                    CheckSelectionMode = ListViewSelectionMode.Single;
                    IsCheckMultiChechkBoxSelectMode = false;
                    IsEnabled = true;
                    IsVisibility = Visibility.Visible;
                    RaisePropertyChanged("IsVisibility");
                    RaisePropertyChanged("IsEnabled");
                    RaisePropertyChanged("IsCheckMultiChechkBoxSelectMode");
                    RaisePropertyChanged("CheckSelectionMode");
                    DataStuff = new ObservableCollection<Stuff>(DataProviders.GetAllStuff(AddressData));
                    RaisePropertyChanged("DataStuff");
                });
                return _loaded_ShowData;
            }
        }

        public ICommand MenuClickDelete
        {
            get
            {
                _menuClickDelete = new RelayCommand(async () =>
                 {
                     bool check = await DataProviders.DeleteWithAsync(AddressData, Enum.EChoice.Stuff, HoldingSelect.ID, estuff: Enum.EinStuff.ID);
                     DataStuff = new ObservableCollection<Stuff>(DataProviders.GetAllStuff(AddressData));
                     RaisePropertyChanged("DataStuff");
                 });
                return _menuClickDelete;
            }
        }

        public ICommand MouseEnterLayoutRoot
        {
            get
            {
                _mouseEnterLayoutRoot = new RelayCommand<Point>((p) =>
                {
                    MousePoint = p;
                });

                return _mouseEnterLayoutRoot;
            }
        }

        public ICommand Tapped_SelectMode
        {
            get
            {
                _tapped_SelectMode = new RelayCommand(() =>
                {
                    IsEnabled = !IsEnabled;
                    IsCheckMultiChechkBoxSelectMode = !IsCheckMultiChechkBoxSelectMode;
                    CheckSelectionMode = IsCheckMultiChechkBoxSelectMode ? ListViewSelectionMode.Multiple : ListViewSelectionMode.Single;
                    IsVisibility = IsCheckMultiChechkBoxSelectMode ? Visibility.Collapsed : Visibility.Visible;
                    RaisePropertyChanged("IsVisibility");
                    RaisePropertyChanged("IsCheckMultiChechkBoxSelectMode");
                    RaisePropertyChanged("CheckSelectionMode");
                    RaisePropertyChanged("IsEnabled");
                });
                return _tapped_SelectMode;
            }
        }

        public ICommand MultiDelete
        {
            get
            {
                _MultiDelete = new RelayCommand<ListView>((p) =>
                {
                    p.SelectedItems.ToList().ForEach((t) =>
                    {
                        DataProviders.DeleteWithAsync(AddressData, Enum.EChoice.Stuff, (t as Stuff).ID, Enum.EinStuff.ID);
                    });
                });
                return _MultiDelete;
            }
        }

        #endregion

        #region Other Properties
        public ObservableCollection<Stuff> DataStuff { get => _dataStuff; set => _dataStuff = value; }
        public IData DataProviders { get => _dataProviders; set => _dataProviders = value; }
        public string AddressData
        {
            get
            {
                _addressData = ServiceLocator.Current.GetInstance<string>("sqlString");
                return _addressData;
            }
            set { _addressData = value; }
        }
        public string IdStuffPass { get => _IdStuffPass; set => _IdStuffPass = value; }

        public NavigationServiceEx NavigationService
        {
            get
            {

                return ServiceLocator.Current.GetInstance<NavigationServiceEx>();
            }
        }

        public Point MousePoint { get => _mousePoint; set => _mousePoint = value; }

        public Stuff HoldingSelect { get => _holdingSelect; set => _holdingSelect = value; }

        public bool IsCheckAdmin
        {
            get
            {
                _isCheckAdmin = UserName.Equals("Admin");
                return _isCheckAdmin;
            }
        }
        public bool IsCheckMultiChechkBoxSelectMode { get => _isCheckMultiChechkBoxSelectMode; set => _isCheckMultiChechkBoxSelectMode = value; }
        public string UserName
        {
            get
            {
                _userName = ServiceLocator.Current.GetInstance<string>("UserName");
                return _userName;
            }
        }

        public List<Stuff> StuffBeSelect { get => _stuffBeSelect; set => _stuffBeSelect = value; }
        public ListViewSelectionMode CheckSelectionMode { get => _checkSelectionMode; set => _checkSelectionMode = value; }
        public bool IsEnabled { get => _isEnabled; set => _isEnabled = value; }
        public Visibility IsVisibility { get => _isVisibility; set => _isVisibility = value; }

        #endregion
        #endregion
    }
}
