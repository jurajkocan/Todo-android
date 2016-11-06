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
using Java.Lang;

namespace Debilnicek
{
    public class CustomListItemAdapter: BaseAdapter
    {
        private Activity _activity;
        private List<DBModel.Item> _Items;

        public CustomListItemAdapter(Activity activity, List<DBModel.Item> listItems)
        {
            this._activity = activity;
            this._Items = listItems;
        }

        public override int Count
        {
            get { return _Items.Count; }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            // could wrap a Contact in a Java.Lang.Object
            // to return it here if needed
            return null;
        }

        public override long GetItemId(int position)
        {
            return _Items[position].Id;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var textView = new TextView(_activity);
            textView.Text = _Items[position].ItemName + ", " + _Items[position].ItemDate.ToString();

            return textView;
        }        
    }
}