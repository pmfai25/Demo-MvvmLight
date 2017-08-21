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
using Windows.UI.Xaml.Controls;

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
        private ICommand _resetData;

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
                _tapped_Ok = new RelayCommand(async () =>
                {
                    UserData.Name = Name;
                    UserData.NameOfUser = NameOfUser;
                    UserData.Pass = Password;
                    Task<bool> Insert = DataProvider.Insert(AddressData, Enum.EChoice.User, user: UserData);
                    var check = await Insert;
                    string content = check ? "OK" : "Error";
                    ContentDialog dialog = new ContentDialog()
                    {
                        Title= "Notifications",
                        Content=content,
                        CloseButtonText="OK"
                    };
                    ContentDialogResult result = await dialog.ShowAsync();
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
        public ICommand ResetData
        {
            get
            {
                _resetData = new RelayCommand(() =>
                {
                    Name = String.Empty;
                    NameOfUser = String.Empty;
                    Password = String.Empty;
                    RaisePropertyChanged("Name");
                    RaisePropertyChanged("NameOfUser");
                    RaisePropertyChanged("Password");
                });
                return _resetData;
            }
        }
    }
}
