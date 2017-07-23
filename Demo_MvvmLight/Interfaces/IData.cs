using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo_MvvmLight.Models;

namespace Demo_MvvmLight
{
    public interface IData
    {
        IEnumerable<Stuff> GetAllStuff(string AddressDatabase);
        IEnumerable<User>GetAllUser(string AddressDatabase);
    }
}
