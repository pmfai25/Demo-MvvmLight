using System;
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

namespace Demo_MvvmLight.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        #region variable
        
        private Brush borderForTxbUser;
        private IData datasource;
        public List<Person> People { get; set; }
        private string textTxbUser;
        private string textTxbPass;
        private ICommand click_SignIn;
        #endregion
        public MainViewModel(IData data)
        {
            this.datasource = data;
            People = new List<Person>();
        }
        #region NavigationService
        public NavigationServiceEx NavigationService
        {
            get
            {
                return Microsoft.Practices.ServiceLocation.ServiceLocator.Current.GetInstance<NavigationServiceEx>();
            }
        }
        #endregion

        #region Properties 
        public string TextTxbUser { get => textTxbUser; set => textTxbUser = value; }
        public string TextTxbPass { get => textTxbPass; set => textTxbPass = value; }
        
        #region Command
        public ICommand Click_SignIn { get
            {
                click_SignIn = new RelayCommand(() => {
                    if (SignIncommand()!=true)
                    {
                        BorderForTxbUser = new SolidColorBrush(Colors.Red);
                    }
                    //SignIncommand();
                });
                return click_SignIn;
            } }
        #endregion

        public Brush BorderForTxbUser { get => borderForTxbUser; set => borderForTxbUser = value; }
        

        #endregion


        #region Method for command

        bool SignIncommand()
        {
            if (TextTxbPass==null||TextTxbUser==null)
            {
                return false;
            }
            People = datasource.Get().ToList();
            if (People.Exists(p=>p.Name.Equals(TextTxbUser))==false)
            {
                return false;
            }
            if (People.Find(p => p.Name.Equals(TextTxbUser)).Pass.Equals(TextTxbPass))
            {
                NavigationService.Navigate(typeof(LoginViewModel).ToString());
                return true;
            }
            return false;
        }


        #endregion




    }
}
