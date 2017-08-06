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

using System.Composition.Hosting;
using System.Composition;
using GalaSoft.MvvmLight.Ioc;
using Demo_MvvmLight.Models;
using GalaSoft.MvvmLight.Messaging;
using System.Windows.Input;
using Microsoft.Practices.ServiceLocation;
using Demo_MvvmLight.Services;


namespace Demo_MvvmLight.ViewModels
{
    public class AddStuffViewModel : ViewModelBase
    {
        private Stuff _stuff;
        private IData _dataProvider;
        private string _sqlString;
        private ICommand _Add;
        private string _txb_Name;
        private UInt32 _txb_ID;
        private byte _txb_Old;
        private uint _txb_Salary;

        public AddStuffViewModel(IData dt)
        {
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
                _Add = new RelayCommand(() =>
                {
                    stuff = new Stuff(Txb_ID.ToString(), Txb_Salary.ToString(), Txb_Name.ToString(), Txb_Old.ToString());
                    DataProviders.Insert(SqlString, Enum.EChoice.Stuff, stuff: stuff);
                });
                return _Add;
            }
        }


    }
}
