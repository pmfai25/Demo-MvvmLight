using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight;
using Windows.UI.Xaml.Controls;

using Demo_MvvmLight.Views;
using Demo_MvvmLight.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Practices.ServiceLocation;
using GalaSoft.MvvmLight.Views;
using Demo_MvvmLight.Services;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI;
using System.Composition;
using GalaSoft.MvvmLight.Ioc;
using Demo_MvvmLight.Enum;
using System.Diagnostics;

namespace Demo_MvvmLight.ViewModels
{
    public class MainViewModel : ViewModelBase
    {

        #region variable

        private string _username;
        private string textTxblWarn;
        private Brush borderWarn;
        private IData datasource;
        public List<User> People { get; set; }
        private string textTxbUser;
        private string textTxbPass;
        private ICommand click_SignIn;
        private ICommand _clickGetAccount;
        private ICommand _loadedView;
        #endregion

        public MainViewModel(IData data)
        {

            this.datasource = data;
            People = new List<User>();
        }
        #region GetInstance from ServiceLocator

        #region Get String sql for connection
        /// <summary>
        /// Get a string for connection
        /// </summary>
        public string _StringSql
        {
            get
            {
                return ServiceLocator.Current.GetInstance<string>("sqlString");
            }
        }
        #endregion

        #region NavigationService
        /// <summary>
        /// Get NavigationService for navigation
        /// </summary>
        public NavigationServiceEx NavigationService
        {
            get
            {
                return Microsoft.Practices.ServiceLocation.ServiceLocator.Current.GetInstance<NavigationServiceEx>();
            }
        }
        #endregion
        #endregion

        #region Properties 
        public string TextTxbUser { get => textTxbUser; set => textTxbUser = value; }
        public string TextTxbPass { get => textTxbPass; set => textTxbPass = value; }

        #region Command
        public ICommand Click_SignIn
        {
            get
            {
                click_SignIn = new RelayCommand(async () =>
                {
                    //User user = new User() { Name = "f", NameOfUser = "f", Pass = "f" };
                    //bool ab= await datasource.Insert(_StringSql, Enum.EChoice.User, user: user);

                    ArrayList a = await datasource.SearchAsync(_StringSql, EChoice.User, "123", TargetUser: EinUser.Password);
                    //bool abc = await datasource.DeleteWithAsync(_StringSql, EChoice.User, "f", euser: EinUser.Name);

                    if (SignIncommand() == false)
                    {

                        SolidColorBrush MyBrush = new SolidColorBrush(Colors.Red);
                        BorderWarn = MyBrush;
                        TextTxblWarn = "Your account or password is incorrect.\n";
                        RaisePropertyChanged("BorderForTxbUser");
                        RaisePropertyChanged("TextTxblWarn");
                    }

                    //SignIncommand();
                });
                return click_SignIn;
            }
        }
        #endregion
        /// <summary>
        /// Boder for warn
        /// </summary>
        public Brush BorderWarn
        {
            get
            {
                if (borderWarn == null)
                {
                    var MyBrush = new SolidColorBrush(Colors.White);
                    borderWarn = MyBrush;
                    return borderWarn;
                }
                else
                {
                    return borderWarn;
                }


            }
            set { borderWarn = value; }
        }

        public string TextTxblWarn { get => textTxblWarn; set => textTxblWarn = value; }
        public string Username
        {
            get => _username;
            set
            {
                if (SimpleIoc.Default.IsRegistered<string>("UserName"))
                {
                    SimpleIoc.Default.Unregister<string>("UserName");
                }
                SimpleIoc.Default.Register(() => value, "UserName");
                _username = value;
            }
        }

        public ICommand ClickGetAccount
        {
            get
            {
                _clickGetAccount = new RelayCommand(() =>
                {
                    NavigationService.Navigate(typeof(CreateAccountViewModel).FullName);
                });
                return _clickGetAccount;
            }
        }
        public ICommand LoadedView
        {
            get
            {
                TextTxbPass = String.Empty;
                TextTxbUser = String.Empty;
                RaisePropertyChanged("TextTxbUser");
                RaisePropertyChanged("TextTxbPass");
                return _loadedView;
            }
        }


        #endregion


        #region Method for command

        /// <summary>
        ///Check Password and User Name not null
        ///and people is exist or not exist
        /// </summary>
        /// <returns></returns>
        bool SignIncommand()
        {
            People = datasource.GetAllUser(_StringSql).ToList();
            if (TextTxbPass == null || TextTxbUser == null)
            {
                return false;
            }
            else if (People.Exists(p => p.Name.Equals(TextTxbUser)) == false)
            {
                return false;
            }
            else if (People.Find(p => p.Name.Equals(TextTxbUser)).Pass.Equals(TextTxbPass))
            {
                Username = TextTxbUser;
                NavigationService.Navigate(typeof(ShowDataViewModel).ToString());
                return true;
            }
            return false;
        }

        #endregion

    }
}
