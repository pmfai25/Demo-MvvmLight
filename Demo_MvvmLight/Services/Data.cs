using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo_MvvmLight.Models;
using SQLitePCL;
using System.Diagnostics;
using Demo_MvvmLight.Enum;

namespace Demo_MvvmLight.Services
{
    public class Data : IData
    {
        public Data()
        {

        }


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
        public async Task<bool> DeleteWithAsync(string AddressDatabase, EChoice e, object obj, EinStuff? estuff = null, EinUser? euser = null)
        {
            return await Task.Run(() =>
            {
                string query = String.Empty;
                using (var db = new SQLiteConnection(AddressDatabase))
                {
                    if (EChoice.Stuff == e && estuff != null)
                    {
                        query = $"delete from {e.ToString()} where {estuff.ToString()} like {obj.ToString()}";
                        db.Prepare(query).Step();
                        return true;
                    }
                    else if (EChoice.User == e && euser != null)
                    {
                        query = $"delete from {e.ToString()} where {euser.ToString()} like {obj.ToString()}";
                        db.Prepare(query).Step();
                        return true;
                    }

                }
                return false;
            });

        }

        /// <summary>
        /// Get all data from  table Stuff
        /// </summary>
        /// <param name="AddressDatabase">aaa</param>
        /// <returns></returns>
        public IEnumerable<Stuff> GetAllStuff(string AddressDatabase)
        {
            List<Stuff> user = new List<Stuff>();
            using (var db = new SQLiteConnection(AddressDatabase))
            {
                var statement = db.Prepare("select * from Stuff");
                while (!(SQLiteResult.DONE == statement.Step()))
                {
                    user.Add(new Stuff()
                    {
                        ID = statement[0].ToString(),
                        Name = statement[1].ToString(),
                        Salary = statement[2].ToString(),
                        Old = statement[3].ToString()
                    });
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

        /// <summary>
        /// Search a elements is in Database
        /// </summary>
        /// <param name="AddressDatabase"></param>
        /// <param name="e"></param>
        /// <param name="obj"></param>
        /// <param name="TargetUser"></param>
        /// <param name="TargetStuff"></param>
        /// <returns></returns>
        public Task<ArrayList> SearchAsync(string AddressDatabase, EChoice e, object obj, EinUser? TargetUser = null, EinStuff? TargetStuff = null)
        {

            return Task.Factory.StartNew(() => Search(AddressDatabase, e, obj, TargetUser, TargetStuff));
        }

        /// <summary>
        /// Method for SearchAsync
        /// </summary>
        /// <param name="AddressDatabase"></param>
        /// <param name="e"></param>
        /// <param name="obj"></param>
        /// <param name="TargetUser"></param>
        /// <param name="TargetStuff"></param>
        /// <returns></returns>
        protected ArrayList Search(string AddressDatabase, EChoice e, object obj, EinUser? TargetUser = null, EinStuff? TargetStuff = null)
        {

            ArrayList result = new ArrayList();

            string query = string.Empty;
            using (var db = new SQLiteConnection(AddressDatabase))
            {
                if (EChoice.User == e && TargetUser != null)
                {

                    query = $"select * from User where {TargetUser.ToString()} like '{obj.ToString()}'";
                    var statement = db.Prepare(query);
                    while (!(SQLiteResult.DONE == statement.Step()))
                    {
                        result.Add(new User() { Name = statement[0].ToString(), Pass = statement[1].ToString(), NameOfUser = statement[2].ToString() });
                    }
                }
                else if (EChoice.Stuff == e)
                {
                    query = $"select * from Stuff where {TargetStuff.ToString()} like '{obj.ToString()}'";
                    var statement = db.Prepare(query);
                    while (!(SQLiteResult.DONE == statement.Step()))
                    {
                        result.Add(new Stuff()
                        {
                            ID = statement[0].ToString(),
                            Name = statement[1].ToString(),
                            Salary = statement[2].ToString(),
                            Old = statement[3].ToString()
                        });
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Insert data to database
        /// And the data is User or Stuff
        /// </summary>
        /// <param name="AddressDatabase"></param>
        /// <param name="e"></param>
        /// <param name="user"></param>
        /// <param name="stuff"></param>
        /// <returns></returns>
        public async Task<bool> Insert(string AddressDatabase, EChoice e, User user = null, Stuff stuff = null)
        {
            Task<IEnumerable<User>> getUser = Task.Factory.StartNew(() => GetAllUser(AddressDatabase));
            Task<IEnumerable<Stuff>> getStuff = Task.Factory.StartNew(() => GetAllStuff(AddressDatabase));

            using (var db = new SQLiteConnection(AddressDatabase))
            {
                var result = SQLiteResult.EMPTY;
                var GetUser = await getUser;
                var GetStuff = await getStuff;

                if (EChoice.Stuff == e)
                {
                    if (GetStuff.ToList().Exists(p => p.ID == stuff.ID))
                    {
                        return false;
                    }
                    else if (String.IsNullOrWhiteSpace(stuff.ID)|| String.IsNullOrWhiteSpace(stuff.Name) || String.IsNullOrWhiteSpace(stuff.Old) || String.IsNullOrWhiteSpace(stuff.Salary) )
                    {
                        return false;
                    }
                    else if (String.IsNullOrEmpty(stuff.ID)|| String.IsNullOrEmpty(stuff.Name)|| String.IsNullOrEmpty(stuff.Old)|| String.IsNullOrEmpty(stuff.Salary))
                    {
                        return false;
                    }
                    else if (stuff != null )
                    {
                        string query = $"INSERT INTO Stuff VALUES ('{stuff.ID}','{stuff.Name.ToString()}','{stuff.Old}','{stuff.Salary}')";
                        result = db.Prepare(query).Step();
                        if (result != SQLiteResult.DONE)
                        {
                            return false;
                            //throw new Exception("Query can't execute");
                        }
                        return true;
                    }
                    else
                    {
                        return false;
                        //throw new ArgumentException("Paramater stuff can't null");
                    }
                }
                else if (EChoice.User == e)
                {
                    if (GetUser.ToList().Exists(p => p.Name == user.Name))
                    {
                        return false;
                    }
                    if (user != null && user.Name != null && user.NameOfUser != null && user.Pass != null)
                    {
                        string query = $"INSERT INTO User VALUES ('{ user.Name.ToString() }','{ user.Pass.ToString() }','{ user.NameOfUser.ToString() }')";
                        result = db.Prepare(query).Step();
                        if (result != SQLiteResult.DONE)
                        {
                            return false;
                            //throw new Exception("Query can't execute");
                        }
                        return true;
                    }
                    else
                    {
                        return false;
                        //throw new ArgumentException("Paramater stuff can't null");
                    }
                }
            }
            return false;
        }
        /// <summary>
        /// Update a element that match the conditions defined 
        /// </summary>
        /// <param name="AddressDatabase"></param>
        /// <param name="e"></param>
        /// <param name="user"></param>
        /// <param name="stuff"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(string AddressDatabase, EChoice e, object Target, User user = null, Stuff stuff = null)
        {
            return await Task.Run(() =>
            {
                using (var db = new SQLiteConnection(AddressDatabase))
                {
                    string query = String.Empty;
                    var result = SQLiteResult.EMPTY;
                    if (EChoice.Stuff.Equals(e))
                    {
                        query = $"UPDATE  Stuff SET Name ='{stuff.Name}' , Salary = '{stuff.Salary}' , Old = '{stuff.Old}' WHERE ID LIKE '{stuff.ID}'";
                        result = db.Prepare(query).Step();
                        if (result == SQLiteResult.DONE)
                        {
                            return true;
                        }
                    }
                    else if (EChoice.User.Equals(e))
                    {

                        query = $"UPDATE  User SET Name ='{stuff.Name}' , Password = '{user.Pass}' , NameOfUser = '{user.NameOfUser}' WHERE Name LIKE '{user.Name}'";
                        result = db.Prepare(query).Step();
                        if (result == SQLiteResult.DONE)
                        {
                            return true;
                        }
                    }

                    return false;
                }

            });


        }
    }
}
