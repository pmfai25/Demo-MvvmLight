using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight;
using Windows.UI.Xaml.Controls;
using Demo_MvvmLight.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Demo_MvvmLight.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private IData datasource;
        public List<Person> people { get; set; }
        public MainViewModel(IData data)
        {
            this.datasource = data;
            people = new List<Person>();
        }

        private ICommand getPeopleCmmd;

        public ICommand GetPeopleCmmd { get  {
                getPeopleCmmd = new RelayCommand<UIElementCollection>(p=> 
                {
                    people = datasource.Get().ToList();
                    foreach (var item in p)
                    {

                        ListView a = item as ListView;
                        if (a == null)
                        {
                            continue;
                        }
                        if (String.IsNullOrEmpty(a.Name))
                        {
                            continue;
                        }
                        if (a.Name=="Lsv_people")
                        {
                            a.ItemsSource =from c in people
                                           select c.Name ;
                        }
                    }
                    //var lis = p.Where(l => (l as ListView).Name == "Lsv_people").Select(c => c as ListView);


                    RaisePropertyChanged(()=>people);
                });
                return getPeopleCmmd;
            } }
        
    }
}
