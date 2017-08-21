using System;
using System.Collections;
using System.Linq;
using Windows.UI.Xaml.Controls;
using Microsoft.Practices.ServiceLocation;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight;
using Demo_MvvmLight.Models;
using System.Collections.Generic;
using System.Windows.Input;

namespace Demo_MvvmLight.ViewModels
{
    public class ShowAccountViewModel : ViewModelBase
    {
        private List<User> _accountUser;
        private ICommand _showData;
        private ICommand _deleteSpecificData;
        private ICommand _showSelectionMode;
        private IData _dataProvider;
        private ListViewSelectionMode _listViewShowSelectionMode;
        private string _addressData;
        private bool _isCheckMultiChechkBoxSelectMode;


        public ShowAccountViewModel(IData data)
        {
            ListViewShowSelectionMode = ListViewSelectionMode.Single;
            IsCheckMultiChechkBoxSelectMode = false;
            RaisePropertyChanged("ListViewShowSelectionMode");
            RaisePropertyChanged("IsCheckMultiChechkBoxSelectMode");
            this._dataProvider = data;
        }

        public List<User> AccountUser { get => _accountUser; set => _accountUser = value; }
        public ICommand ShowData
        {
            get
            {
                _showData = new RelayCommand(() =>
                {
                    AccountUser = DataProvider.GetAllUser(AddressData).ToList();
                    RaisePropertyChanged("AccountUser");
                });
                return _showData;
            }
        }
        public IData DataProvider { get => _dataProvider; set => _dataProvider = value; }
        public ICommand DeleteSpecificData
        {
            get
            {
                _deleteSpecificData = new RelayCommand<object>( (p) =>
                {
                    var listview = p as ListView;
                    listview.SelectedItems.ToList().ForEach(async(t) =>
                    {
                        bool a =await DataProvider.DeleteWithAsync(AddressData, Enum.EChoice.User, (t as User).Name, euser: Enum.EinUser.Name);
                    });
                });
                return _deleteSpecificData;
            }
        }
        public string AddressData
        {
            get
            {
                _addressData = ServiceLocator.Current.GetInstance<string>("sqlString");
                return _addressData;
            }
        }

        public ListViewSelectionMode ListViewShowSelectionMode { get => _listViewShowSelectionMode; set => _listViewShowSelectionMode = value; }
        public bool IsCheckMultiChechkBoxSelectMode { get => _isCheckMultiChechkBoxSelectMode; set => _isCheckMultiChechkBoxSelectMode = value; }
        public ICommand ShowSelectionMode
        {
            get
            {
                _showSelectionMode = new RelayCommand(() =>
                {
                    IsCheckMultiChechkBoxSelectMode = !IsCheckMultiChechkBoxSelectMode;
                    ListViewShowSelectionMode = IsCheckMultiChechkBoxSelectMode ? ListViewSelectionMode.Multiple : ListViewSelectionMode.Single;
                    RaisePropertyChanged("ListViewShowSelectionMode");
                    RaisePropertyChanged("IsCheckMultiChechkBoxSelectMode");
                });
                return _showSelectionMode;
            }
        }
    }
}
