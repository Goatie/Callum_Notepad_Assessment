using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using System.IO;

namespace Note_Assessment
{
    [Activity(Label = "Note_Assessment")]
    public class MainActivity : Activity
    {

        Button btnSearch;
        string SearchString;
        TextView txtSearch;


        ListView lstNotes;
        List<Notes> myList;

        List<Notes> searchList;

        static string dbName = "Notes.sqlite";
        string dbPath = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.ToString(), dbName);

        DatabaseManager objDB;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);


            SetContentView(Resource.Layout.Main);
            lstNotes = FindViewById<ListView>(Resource.Id.listView1);


            //Copies the file to your mobile phone
            CopyDatabase();

            objDB = new DatabaseManager();
            myList = objDB.ViewAllNotes();
            lstNotes.Adapter = new DataAdapter(this, myList);
            lstNotes.ItemClick += OnLstNotesListClick;

            txtSearch = FindViewById<TextView>(Resource.Id.txtSearch);

            btnSearch = FindViewById<Button>(Resource.Id.btnSearch);
            btnSearch.Click += OnBtnSearchClick;

        }

        private void OnBtnSearchClick(object sender, EventArgs e)
        {
            try
            {
                txtSearch = FindViewById<TextView>(Resource.Id.txtSearch);
                SearchString = txtSearch.Text;
                searchList = objDB.Search(SearchString);

                lstNotes.Adapter = new DataAdapter(this, searchList);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Occurred:" + ex.Message);
            }
        }

        private void OnLstNotesListClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var ListItem = myList[e.Position];
            var edititem = new Intent(this, typeof(EditItem));

            edititem.PutExtra("Title", ListItem.Title);
            edititem.PutExtra("Subtitle", ListItem.Subtitle);
            edititem.PutExtra("NoteID", ListItem.NoteID);

            StartActivity(edititem);
        }

        public void CopyDatabase()
        {
            if (!File.Exists(dbPath))
            {
                using (BinaryReader br = new BinaryReader(Assets.Open(dbName)))
                {
                    using (BinaryWriter bw = new BinaryWriter(new FileStream(dbPath, FileMode.Create)))
                    {
                        byte[] buffer = new byte[2048];
                        int len = 0;
                        while ((len = br.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            bw.Write(buffer, 0, len);
                        }
                    }
                }
            }
        }
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            menu.Add("Add New Item");
            return base.OnPrepareOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            var itemTitle = item.TitleFormatted.ToString();

            switch (itemTitle)
            {
                case "Add New Item":
                    StartActivity(typeof(AddItem));
                    break;
            }
            return base.OnOptionsItemSelected(item);
        }

    }
}