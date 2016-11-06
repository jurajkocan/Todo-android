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
    [Activity(Label = "AddActivity")]
    public class AddActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.AddLayout);            

            Button okButton = FindViewById<Button>(Resource.Id.buttonNewItemConfirm);
            okButton.Click += delegate { okClick(); };
        }

        private void okClick()
        {
            TextView textAddName = FindViewById<Android.Widget.TextView>(Resource.Id.editTextAddName);
            TextView textAddDate = FindViewById<Android.Widget.TextView>(Resource.Id.editTextDateItem);
            TimePicker time = FindViewById<Android.Widget.TimePicker>(Resource.Id.timePickerActivitime);
            DateTime date;
            

            bool parsed = DateTime.TryParse(textAddDate.Text, out date);
            date = date.Date.AddHours(Convert.ToDouble(time.CurrentHour.ToString())).AddMinutes(Convert.ToDouble(time.CurrentMinute.ToString()));
            if (parsed)
            {
                DBAccess.DBInterface db = new DBAccess.DBInterface();
                try
                {
                    db.AddNewItem(textAddName.Text, date);
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
            else
            {
                textAddDate.Text = "";
                textAddDate.Hint = "Required Format: 'DD.MM.YYYY'";
            }
        }
    }
}