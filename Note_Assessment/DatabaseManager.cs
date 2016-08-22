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
using System.IO;

namespace Note_Assessment
{
   public class DatabaseManager
    {
        static string dbName = "Notes.sqlite";
        string dbPath = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.ToString(), dbName);

        public DatabaseManager()
        {

        }

        public List<Notes> ViewAllNotes()
        {
            try
            {

                using (var conn = new SQLiteConnection(dbPath))
                {
                    var cmd = new SQLiteCommand(conn);
                    cmd.CommandText = "select * from tblNotes";
                    var NoteList = cmd.ExecuteQuery<Notes>();
                    return NoteList;

                    
                }

            }
            catch (Exception e)
            {

                Console.WriteLine("Error : " + e.Message);
                return null;
            }
        }

        public void AddItem(string title, string subtitle)
        {
            try
            {
                using (var conn = new SQLiteConnection(dbPath))
                {
                    var cmd = new SQLiteCommand(conn);
                    cmd.CommandText = "insert into tblNotes(Title,Subtitle) values('" + title + "','" + subtitle + "')";
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {

                Console.WriteLine("Error : " + e.Message);

            }
        }

        public void EditItem(string title, string subtitle, int noteid)
        {
            try
            {
                using (var conn = new SQLiteConnection(dbPath))
                {
                    var cmd = new SQLiteCommand(conn);
                    cmd.CommandText = "update tblNotes set Title='" + title + "', Subtitle='" + subtitle + "' where NoteID=" + noteid;
                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception e)
            {

                Console.WriteLine("Error:" + e.Message);

            }
        }

        public void DeleteItem(int noteid)
        {
            try
            {
                using (var conn = new SQLiteConnection(dbPath))
                {
                    var cmd = new SQLiteCommand(conn);
                    cmd.CommandText = "delete from tblNotes where NoteID = " + noteid;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {

                Console.WriteLine("Error : " + e.Message);
            }
        }

        public List<Notes> Search(string SearchString)
        {
            try
            {
                using (var conn = new SQLiteConnection(dbPath))
                {
                    var cmd = new SQLiteCommand(conn);
                    cmd.CommandText = "Select * from tblNotes where Title like '%" + SearchString + "%' or Subtitle like '%" + SearchString + "%'";
                    var SearchItems = cmd.ExecuteQuery<Notes>();
                    return SearchItems;
                }

            }
            catch (Exception e)
            {

                Console.WriteLine("Error : " + e.Message);
                return null;
            }
        }

    }
}