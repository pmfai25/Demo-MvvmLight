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
using Windows.UI.Xaml;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Controls;
using System.Threading.Tasks;

namespace Demo_MvvmLight.ViewModels
{
    public class DetailsViewModel : ViewModelBase
    {

        #region variable
        private string _IdStuffPass;
        private string _Id;
        private IData _DataProvider;
        private string _Salary;
        private string _tempSalary;
        private string _Name;
        private string _tempName;
        private string _Old;
        private string _tempOld;
        private bool _isCheck;
        private ICommand _ShowData;
        private string _sqlString;
        private ICommand _Loaded;
        private ICommand _edit;
        private ICommand _comCancel;
        private ICommand _comAccept;
        private Visibility _canVisibility;
        private Visibility _visibilityForControls;
        #endregion



        public DetailsViewModel(IData dataprovider)
        {
            CanVisibility = Visibility.Visible;
            VisibilityForControls = Visibility.Collapsed;
            this.DataProvider = dataprovider;
        }
        #region Properties
        public string TempSalary { get => _tempSalary; set => _tempSalary = value; }
        public string TempName { get => _tempName; set => _tempName = value; }
        public string TempOld { get => _tempOld; set => _tempOld = value; }

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
                     });
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
        public ICommand Loaded
        {
            get
            {
                _Loaded = new RelayCommand(() =>
                {

                    IdStuffPass = ServiceLocator.Current.GetInstance<string>("IdStuff");
                    SimpleIoc.Default.Unregister<string>("IdStuff");
                    ForLoaded(IdStuffPass);
                    
//                     IdStuffPass = ServiceLocator.Current.GetInstance<string>("IdStuff");
//                     SimpleIoc.Default.Unregister<string>("IdStuff");
//                     DataProvider.GetAllStuff(SqlString).Where(p => p.ID.Equals(IdStuffPass)).Select(p => p).ToList().ForEach(p =>
//                       {
//                           Id = p.ID;
//                           Name = p.Name;
//                           Salary = p.Salary;
//                           Old = p.Old;
//                       });
//                     RaisePropertyChanged("Id");
//                     RaisePropertyChanged("Name");
//                     RaisePropertyChanged("Salary");
//                     RaisePropertyChanged("Old");

                });
                return _Loaded;
            }
        }



        public string IdStuffPass { get => _IdStuffPass; set => _IdStuffPass = value; }
        public Visibility VisibilityForControls { get => _visibilityForControls; set => _visibilityForControls = value; }
        public ICommand Edit
        {
            get
            {
                _edit = new RelayCommand(() =>
                {
                    TempName = Name;
                    TempOld = Old;
                    TempSalary = Salary;
                    CanVisibility = Visibility.Collapsed;
                    VisibilityForControls = Visibility.Visible;
                    RaisePropertyChanged("TempName");
                    RaisePropertyChanged("TempOld");
                    RaisePropertyChanged("TempSalary");
                    RaisePropertyChanged("CanVisibility");
                    RaisePropertyChanged("VisibilityForControls");
                });
                return _edit;
            }
        }

        public ICommand ComCancel
        {
            get
            {
                _comCancel = new RelayCommand(() =>
                  {
                      CanVisibility = Visibility.Visible;
                      VisibilityForControls = Visibility.Collapsed;
                      RaisePropertyChanged("CanVisibility");
                      RaisePropertyChanged("VisibilityForControls");
                  });
                return _comCancel;
            }
        }

        public ICommand ComAccept
        {
            get
            {
                _comAccept = new RelayCommand(async () =>
                  {
                      var a = await ForComAcceptAsync();
                      ForLoaded(IdStuffPass);
                      //                       Stuff user = new Stuff(Id, TempSalary, TempName, TempOld);
                      //                       IsCheck = await DataProvider.UpdateAsync(SqlString, Enum.EChoice.Stuff, Id, stuff: user);
                      // 
                      //                       string content = IsCheck ? "Every is done" : "Something wrong";
                      //                       string tit = IsCheck ? "Done" : "Can't update database";
                      //                       ContentDialog box = new ContentDialog()
                      //                       {
                      //                           Title = tit,
                      //                           Content = content,
                      //                           CloseButtonText = "Ok"
                      //                       };
                      //                       ContentDialogResult result = await box.ShowAsync();
                  });
                return _comAccept;
            }
        }

        public bool IsCheck { get => _isCheck; set => _isCheck = value; }
        public Visibility CanVisibility { get => _canVisibility; set => _canVisibility = value; }

        #endregion

        #region
        protected void ForLoaded(string IdStuffPass)
        {
            
            //IdStuffPass = ServiceLocator.Current.GetInstance<string>("IdStuff");
            //SimpleIoc.Default.Unregister<string>("IdStuff");
            DataProvider.GetAllStuff(SqlString).Where(p => p.ID.Equals(IdStuffPass)).Select(p => p).ToList().ForEach(p =>
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

        }

        protected void ForCancel()
        {

        }

        protected async Task<bool> ForComAcceptAsync()
        {

            Stuff user = new Stuff(Id, TempSalary, TempName, TempOld);
            IsCheck = await DataProvider.UpdateAsync(SqlString, Enum.EChoice.Stuff, Id, stuff: user);

            string content = IsCheck ? "Every is done" : "Something wrong";
            string tit = IsCheck ? "Done" : "Can't update database";
            ContentDialog box = new ContentDialog()
            {
                Title = tit,
                Content = content,
                CloseButtonText = "Ok"
            };
            ContentDialogResult result = await box.ShowAsync();
            /// cần viết lại theo hàm
            CanVisibility = Visibility.Visible;
            VisibilityForControls = Visibility.Collapsed;
            RaisePropertyChanged("CanVisibility");
            RaisePropertyChanged("VisibilityForControls");
            ///
            return true;
        }
        #endregion
    }
}
