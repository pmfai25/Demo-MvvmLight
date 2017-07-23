using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo_MvvmLight.Models;
using SQLitePCL;
using System.Diagnostics;

namespace Demo_MvvmLight.Services
{
    public class Data:IData
    {
        public Data()
        { }
        /// <summary>
        /// Get all data from  table Stuff
        /// </summary>
        /// <param name="AddressDatabase"></param>
        /// <returns></returns>
        public IEnumerable<Stuff> GetAllStuff(string AddressDatabase)
        {
            List<Stuff> user = new List<Stuff>();
            using (var db = new SQLiteConnection(AddressDatabase))
            {
                var statement = db.Prepare("select * from User");
                while (!(SQLiteResult.DONE == statement.Step()))
                {
                    user.Add(new Stuff() { ID = statement[0].ToString(),
                                           Name = statement[1].ToString(),
                                           Salary = statement[2].ToString(),
                                           Old = statement[3].ToString() });
                }
            }
            return user;
        }

        /// <summary>
        /// 
        /// Get all data from table User
        /// 
        /// </summary>
        /// <param name="AddressDatabase"></param>
        /// <returns></returns>
        public IEnumerable<User> GetAllUser(string AddressDatabase)
        {
            List<User> user = new List<User>();
            using (var db = new SQLiteConnection(AddressDatabase))
            {                
                var statement = db.Prepare("select * from User");
                while (!(SQLiteResult.DONE == statement.Step()))
                {
                    user.Add(new User() { Name = statement[0].ToString(), Pass = statement[1].ToString(), NameOfUser = statement[2].ToString() });
                }                
            }
            return user;
        }
    }
}
