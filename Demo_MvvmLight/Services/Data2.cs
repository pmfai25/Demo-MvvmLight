using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Demo_MvvmLight.Models;
using System.Threading.Tasks;

namespace Demo_MvvmLight.Services
{
    public class Data2:IData
    {
        public Data2()
        {
                
        }
        public IEnumerable<Person> Get()
        {
            List<Person> people = new List<Person>() { new Person {Name="A",Pass="123" },new Person { Name = "B", Pass = "456" } };
            return people;
        }
        

    }
}
