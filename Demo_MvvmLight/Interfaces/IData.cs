using System;
using System.Collections.Generic;
using System.Linq;
using Demo_MvvmLight.Enum;
using System.Text;
using System.Threading.Tasks;
using Demo_MvvmLight.Models;
using System.Collections;

namespace Demo_MvvmLight
{
    public interface IData
    {
        IEnumerable<Stuff> GetAllStuff(string AddressDatabase);
        IEnumerable<User>GetAllUser(string AddressDatabase);
        Task< bool> Insert(string AddressDatabase, EChoice e, User user = null, Stuff stuff = null);
        Task<bool> DeleteWithAsync(string AddressDatabase, EChoice e, object obj, EinStuff? estuff = null, EinUser? euser = null);
        Task<ArrayList> SearchAsync(string AddressDatabase, EChoice e, object obj, EinUser? TargetUser = null, EinStuff? TargetStuff = null);
        
    }
}
