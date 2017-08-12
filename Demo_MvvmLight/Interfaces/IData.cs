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
        /// <summary>
        /// Get all data from  table Stuff
        /// </summary>
        /// <param name="AddressDatabase">aaa</param>
        /// <returns></returns>
        IEnumerable<Stuff> GetAllStuff(string AddressDatabase);

        /// <summary>
        /// 
        /// Get all data from table User
        /// 
        /// </summary>
        /// <param name="AddressDatabase"></param>
        /// <returns></returns>
        IEnumerable<User>GetAllUser(string AddressDatabase);

        /// <summary>
        /// Insert data to database
        /// And the data is User or Stuff
        /// </summary>
        /// <param name="AddressDatabase"></param>
        /// <param name="e"></param>
        /// <param name="user"></param>
        /// <param name="stuff"></param>
        /// <returns></returns>
        Task< bool> Insert(string AddressDatabase, EChoice e, User user = null, Stuff stuff = null);

        /// <summary>
        /// 
        /// Removes a element that match the conditions defined 
        /// </summary>
        /// <param name="AddressDatabase"></param>
        /// <param name="e"></param>
        /// <param name="obj"></param>
        /// <param name="estuff"></param>
        /// <param name="euser"></param>
        /// <returns></returns>
        Task<bool> DeleteWithAsync(string AddressDatabase, EChoice e, object obj, EinStuff? estuff = null, EinUser? euser = null);

        /// <summary>
        /// Search a elements is in Database
        /// </summary>
        /// <param name="AddressDatabase"></param>
        /// <param name="e"></param>
        /// <param name="obj"></param>
        /// <param name="TargetUser"></param>
        /// <param name="TargetStuff"></param>
        /// <returns></returns>
        Task<ArrayList> SearchAsync(string AddressDatabase, EChoice e, object obj, EinUser? TargetUser = null, EinStuff? TargetStuff = null);
        
    }
}
