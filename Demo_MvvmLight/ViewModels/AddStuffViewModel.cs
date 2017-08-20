using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight;
using System.Reflection;
using System.IO;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Composition.Hosting;
using System.Composition;
using GalaSoft.MvvmLight.Ioc;
using Demo_MvvmLight.Models;
using GalaSoft.MvvmLight.Messaging;
using System.Windows.Input;
using Microsoft.Practices.ServiceLocation;
using Demo_MvvmLight.Services;
using Windows.UI.Xaml.Media;
using Windows.UI;

namespace Demo_MvvmLight.ViewModels
{
    public class AddStuffViewModel : ViewModelBase
    {
        private Stuff _stuff;
        private Visibility _getVisibility;
        private SolidColorBrush _getColor;
        private IData _dataProvider;
        private string _sqlString;
        private ICommand _Add;
        private string _txb_Name;
        private UInt32 _txb_ID;
        private byte _txb_Old;
        private uint _txb_Salary;
        private ICommand _Cancel;
        private ICommand _resetData;
        private string _getMess;
        private string _getGlyph;

        public AddStuffViewModel(IData dt)
        {
            Txb_Name = String.Empty;

            this._dataProvider = dt;
        }
        public string Txb_Name { get => _txb_Name; set => _txb_Name = value; }
        public uint Txb_ID { get => _txb_ID; set => _txb_ID = value; }
        public byte Txb_Old { get => _txb_Old; set => _txb_Old = value; }
        public uint Txb_Salary { get => _txb_Salary; set => _txb_Salary = value; }
        public Stuff stuff { get => _stuff; set => _stuff = value; }
        public IData DataProviders { get => _dataProvider; set => _dataProvider = value; }
        public string SqlString
        {
            get
            {
                _sqlString = ServiceLocator.Current.GetInstance<string>("sqlString");
                return _sqlString;
            }
        }

        public ICommand Add
        {
            get
            {
                _Add = new RelayCommand(async () =>
                {
                    stuff = new Stuff(Txb_ID.ToString(), Txb_Salary.ToString(), Txb_Name.ToString(), Txb_Old.ToString());
                    bool check=await DataProviders.Insert(SqlString, Enum.EChoice.Stuff, stuff: stuff);
                    GetVisibility = Visibility.Visible;
                    GetColor = check ? new SolidColorBrush(Colors.ForestGreen) : new SolidColorBrush(Colors.Red);
                    GetMess = check ? "Everything Ok" : "Something wrong";
                    GetGlyph = check ? "\uE001" : "\uE10A";
                    RaisePropertyChanged("GetVisibility");
                    RaisePropertyChanged("GetColor");
                    RaisePropertyChanged("GetGlyph");
                    RaisePropertyChanged("GetMess");
                });
                return _Add;
            }
        }

        public ICommand Cancel
        {
            get
            {
                _Cancel = new RelayCommand(() =>
                {
                    ServiceLocator.Current.GetInstance<NavigationServiceEx>().GoBack();
                });
                return _Cancel;
            }
        }

        public ICommand ResetData
        {
            get
            {
                _resetData = new RelayCommand(() =>
                {
                    Txb_Name = string.Empty;
                    Txb_ID = 0;
                    Txb_Old = 0;
                    Txb_Salary = 0;
                    GetVisibility = Visibility.Collapsed;
                    RaisePropertyChanged("GetVisibility");
                    RaisePropertyChanged("Txb_ID");
                    RaisePropertyChanged("Txb_Name");
                    RaisePropertyChanged("Txb_Old");
                    RaisePropertyChanged("Txb_Salary");
                });
                return _resetData;
            }
        }

        public Visibility GetVisibility { get => _getVisibility; set => _getVisibility = value; }
        public SolidColorBrush GetColor { get => _getColor; set => _getColor = value; }
        public string GetMess { get => _getMess; set => _getMess = value; }
        public string GetGlyph { get => _getGlyph; set => _getGlyph = value; }
    }
}
