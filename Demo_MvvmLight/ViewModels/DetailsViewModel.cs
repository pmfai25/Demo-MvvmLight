using System;

using GalaSoft.MvvmLight;
using Microsoft.Practices.ServiceLocation;

namespace Demo_MvvmLight.ViewModels
{
    public class DetailsViewModel : ViewModelBase
    {
        private string _ID;

        public DetailsViewModel()
        {
            
        }

        public string ID {
            get {
                _ID= ServiceLocator.Current.GetInstance<string>("IdStuff");
                return _ID;
            }
        }

    }
}
