using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight;
using System.Reflection;
using System.IO;
using Demo_MvvmLight.MEssenger;
using System.Composition.Hosting;
using System.Composition;
using GalaSoft.MvvmLight.Messaging;
using System.Windows.Input;
using Microsoft.Practices.ServiceLocation;


namespace Demo_MvvmLight.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        
        private ICommand click_Add;
        private ICommand click_ShowAll;
       
        public string Cc { get; set; }
        public LoginViewModel()
        {
            
        }
       
        
        public ICommand Click {
            get {
                click = new RelayCommand(() => 
                {
                    Messenger.Default.Register<MEssenger.ButtonMessage>(Cc, (p) =>
                    {
                        Method(p);
                    });
                });
                return click;
            }
            }

        public ICommand Click_Add { get {
                click = new RelayCommand(() => { });
                return click_Add;
            }
        }

        public void Method(ButtonMessage bt)
        {
            Cc = bt.ButtonText;
        }
        private ICommand click;
    }
}