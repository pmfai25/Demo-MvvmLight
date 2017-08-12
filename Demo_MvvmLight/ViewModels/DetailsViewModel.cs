using System;
using System.Composition;
using System.Composition.Hosting;
using System.Collections.Generic;
using System.Collections;
using System.Reflection;
using GalaSoft.MvvmLight.Ioc;
using System.Linq;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Practices.ServiceLocation;
using System.Windows.Input;
using Demo_MvvmLight.Models;

namespace Demo_MvvmLight.ViewModels
{
    public class DetailsViewModel : ViewModelBase
    {

        #region variable
        private string _IdStuffPass;
        private string _Id;
        private IData _DataProvider;
        private string _Salary;
        private string _Name;
        private string _Old;
        private ICommand _ShowData;
        private string _sqlString;
        private ICommand _Loaded;

        #endregion

       
       
        public DetailsViewModel(IData dataprovider)
        {
            //Messenger.Default.Register<string>(this,"jj",p=> cc(p));

            //Messenger.Default.Unregister<string>(this, cc);
            this.DataProvider = dataprovider;
        }
        #region Properties
        
        public string Id { get => _Id; set => _Id = value; }
        public IData DataProvider { get => _DataProvider; set => _DataProvider = value; }
        public string Salary { get => _Salary; set => _Salary = value; }
        public string Name { get => _Name; set => _Name = value; }
        public string Old { get => _Old; set => _Old = value; }
        public ICommand ShowData
        {
            get
            {
                _ShowData = new RelayCommand(() =>
                {
                    DataProvider.GetAllStuff(SqlString).Where(p => p.ID.Equals(IdStuffPass)).Select((p) =>
                     new
                     {
                         Id = p.ID,
                         Name = p.Name,
                         Salary = p.Salary,
                         Old = p.Old
                     }
                    );
                    RaisePropertyChanged("Id");
                    RaisePropertyChanged("Name");
                    RaisePropertyChanged("Salary");
                    RaisePropertyChanged("Old");

                });
                return _ShowData;
            }
        }

        public string SqlString
        {
            get
            {
                _sqlString = ServiceLocator.Current.GetInstance<string>("sqlString");
                return _sqlString;
            }
        }
        /// <summary>
        /// Get IdStuff in LoginViewModel .Method will remove it from cache after you use it 
        /// </summary>
        public ICommand Loaded { get
            {
                _Loaded = new RelayCommand(() => 
                {
                    IdStuffPass = ServiceLocator.Current.GetInstance<string>("IdStuff");
                    SimpleIoc.Default.Unregister<string>("IdStuff");
                    DataProvider.GetAllStuff(SqlString).Where(p => p.ID.Equals(IdStuffPass)).Select(p=>p).ToList().ForEach(p=>
                    {
                        Id = p.ID;
                        Name = p.Name;
                        Salary = p.Salary;
                        Old = p.Old;                        
                    });

                   
                    RaisePropertyChanged("Id");
                    RaisePropertyChanged("Name");
                    RaisePropertyChanged("Salary");
                    RaisePropertyChanged("Old");

                });
                return _Loaded;
            }
        }



        public string IdStuffPass { get => _IdStuffPass; set => _IdStuffPass = value; }

        #endregion

    }
}
