using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo_MvvmLight.Models;

namespace Demo_MvvmLight.Services
{
    public class Data1
    {
        public Data1()
        { }
        public IEnumerable<User> Get()
        {
            List<User> user = new List<User>() { new User { Name = "Admin", Pass = "123" }, new User { Name = "staff", Pass = "456" } };
            return user;
        }
    }
}
