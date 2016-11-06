using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using SQLite;

namespace Debilnicek.DBAccess
{
    public class DBInterface
    {
        private SQLiteConnection dbConn;
        public DBInterface()
        {
            string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            this.dbConn = new SQLiteConnection(System.IO.Path.Combine(folder, "debilnice.db"));
            
            createDatabase();
        }

        public void Disconnect()
        {
            this.dbConn.Close();
        }

        private string createDatabase()
        {
            try
            {
                dbConn.CreateTable<DBModel.Item>();
                return "";
            }
            catch (SQLiteException ex)
            {
                return ex.Message;
            }
        }

        public void AddNewItem(string name, DateTime date)
        {
            var item = new DBModel.Item { ItemName = name , ItemDate = date};
            this.dbConn.Insert(item);
        }

        public List<DBModel.Item> GetALlItems()
        {
            List<DBModel.Item> items = new List<DBModel.Item>();

            items = dbConn.Table<DBModel.Item>().ToList<DBModel.Item>();            

            return items;                
        }

        public DBModel.Item GetItem(long itemId)
        {
            DBModel.Item item;
            item = dbConn.Table<DBModel.Item>().FirstOrDefault(i => i.Id == itemId);

            return item;
        }
    }
}