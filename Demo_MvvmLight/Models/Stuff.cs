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
        public Stuff(string id,string sala,string fullname,string oLd)
        {
            this.ID = id;
            this.Salary = sala;
            this.Name = fullname;
            this.Old = oLd;
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
