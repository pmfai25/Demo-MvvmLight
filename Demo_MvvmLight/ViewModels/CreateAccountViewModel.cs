using System;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight;
using System.Windows.Input;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.ServiceLocation;
using Demo_MvvmLight.Models;

namespace Demo_MvvmLight.ViewModels
{
    public class CreateAccountViewModel : ViewModelBase
    {
        private string _name;
        private string _password;
        private string _nameOfUser;
        private ICommand _tapped_Ok;
        private IData _dataProvider;
        private string _addressData;
        private User _userData;

        public CreateAccountViewModel(IData data)
        {
            this.DataProvider = data;
        }

        public string Name { get => _name; set => _name = value; }
        public string Password { get => _password; set => _password = value; }
        public string NameOfUser { get => _nameOfUser; set => _nameOfUser = value; }
        public ICommand Tapped_Ok
        {
            get
            {
                _tapped_Ok = new RelayCommand(() =>
                {
                    DataProvider.Insert(AddressData, Enum.EChoice.User, user: UserData);
                });
                return _tapped_Ok;
            }
        }
        public IData DataProvider { get => _dataProvider; set => _dataProvider = value; }
        public string AddressData
        {
            get
            {
                _addressData = ServiceLocator.Current.GetInstance<string>("sqlString");
                return _addressData;
            }
            set { _addressData = value; }
        }
        public User UserData { get => _userData; set => _userData = value; }
    }
}
