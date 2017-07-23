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
        
        public string cc1 { get; set; }
        public string cc { get; set; }
        public LoginViewModel()
        {                        
        }
       
        
        public ICommand Click {
            get {
                click = new RelayCommand(() => 
                {
                    Messenger.Default.Register<MEssenger.ButtonMessage>(cc, (p) =>
                    {
                        Method(p);
                    });
                });
                return click;
            }
            }
        public void Method(ButtonMessage bt)
        {
            cc = bt.ButtonText;
        }
        private ICommand click;
    }
}