using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using System.Linq;

namespace Debilnicek
{
    [Activity(Label = "Debilnicek", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {        
        protected override void OnCreate(Bundle bundle)
        {
            
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            SetContent();

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.btnAddItem);            
            button.Click += delegate { addClick(); };
        }

        private void addClick()
        {
            var activityAdd = new Intent(this, typeof(AddActivity));
            activityAdd.PutExtra("MyData", "Data from Activity1");
            StartActivity(activityAdd);
        }

        private void SetContent()
        {
            List<DBModel.Item> mItems = new List<DBModel.Item>();
            DBAccess.DBInterface db = new DBAccess.DBInterface();
            try
            {
                mItems = db.GetALlItems();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (db != null)
                    db.Disconnect();
            }
            
            ListView listView = FindViewById<ListView>(Resource.Id.listViewItems);            
            CustomListItemAdapter adapter = new CustomListItemAdapter(this, mItems);                        
            listView.Adapter = adapter;
            listView.ItemClick += listView_ItemClick;
        }

        void listView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            ListView listView = sender as ListView;
            var item = listView.Adapter.GetItemId(e.Position);

            var activityEdit = new Intent(this, typeof(EditActivity));
            activityEdit.PutExtra("itemId", item);

            StartActivity(activityEdit);
        }
    }
}

