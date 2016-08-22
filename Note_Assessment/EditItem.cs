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

namespace Note_Assessment
{
    [Activity(Label = "EditItem")]
    public class EditItem : Activity
    {

        int NoteId;
        string Title;
        string Subtitle;

        TextView txtTitle;
        TextView txtSubtitle;
        Button btnEdit;
        Button btnDelete;
        DatabaseManager objDb;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Create your application here
            SetContentView(Resource.Layout.EditItem);

            txtTitle = FindViewById<TextView>(Resource.Id.txtEditTitle);
            txtSubtitle = FindViewById<TextView>(Resource.Id.txtEditDescription);

            btnEdit = FindViewById<Button>(Resource.Id.btnEdit);
            btnDelete = FindViewById<Button>(Resource.Id.btnDelete);

            btnEdit.Click += OnBtnEditClick;
            btnDelete.Click += OnBtnDeleteClick;

            NoteId = Intent.GetIntExtra("NoteID", 0);
            Subtitle = Intent.GetStringExtra("Subtitle");
            Title = Intent.GetStringExtra("Title");

            txtTitle.Text = Title;
            txtSubtitle.Text = Subtitle;

            objDb = new DatabaseManager();
        }

        public void OnBtnEditClick(object sender, EventArgs e)
        {
            try
            {
                objDb.EditItem(txtTitle.Text, txtSubtitle.Text, NoteId);
                Toast.MakeText(this, "Note Edited", ToastLength.Long).Show();
                this.Finish();
                StartActivity(typeof(MainActivity));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Occurred:" + ex.Message);
            }
        }

        public void OnBtnDeleteClick(object sender, EventArgs e)
        {
            try
            {
                objDb.DeleteItem(NoteId);
                Toast.MakeText(this, "Note Deleted", ToastLength.Long).Show();
                this.Finish();
                StartActivity(typeof(MainActivity));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Occurred:" + ex.Message);
            }
        }
    }
}