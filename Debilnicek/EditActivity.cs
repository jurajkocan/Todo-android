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

namespace Debilnicek
{
    [Activity(Label = "EditActivity")]
    public class EditActivity : Activity
    {
        private long ItemId;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.EditLayout);

            this.ItemId = this.Intent.Extras.GetLong("itemId");

            SetContent();
            Button button = FindViewById<Button>(Resource.Id.buttonDeleteItem);
            button.Click += delegate { BtnDeleteClick(); };
        }

        private void SetContent()
        {
            DBAccess.DBInterface db = null;
            try
            {
                db = new DBAccess.DBInterface();
                DBModel.Item item = db.GetItem(ItemId);
                List<DBModel.Item> adapterItemList = new List<DBModel.Item>() { item };

                ListView view = FindViewById(Resource.Id.listViewItemEdit) as ListView;
                CustomListItemAdapter adapter = new CustomListItemAdapter(this, adapterItemList);
                view.Adapter = adapter;
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (db != null)
                    db.Disconnect();
            }
        }

        private void BtnDeleteClick()
        {
            DBAccess.DBInterface db = null;
            try
            {
                db = new DBAccess.DBInterface();
                db.DeleteItem(ItemId);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (db != null)
                    db.Disconnect();
            }

            var activityMain = new Intent(this, typeof(MainActivity));
            activityMain.PutExtra("MyData", "Data from Activity1");
            StartActivity(activityMain);
        }
    }
}