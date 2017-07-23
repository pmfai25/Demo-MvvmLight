using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_MvvmLight.Models
{
    public class Stuff
    {
        public Stuff()
        {

        }
        private string iD;
        private string salary;
        private string name;
        private string old;
        public string ID { get => iD; set => iD = value; }
        public string Salary { get => salary; set => salary = value; }
        public string Name { get => name; set => name = value; }
        public string Old { get => old; set => old = value; }
    }
}
