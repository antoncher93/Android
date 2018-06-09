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
using BLModule.Assets.Beacons;
using Android.Bluetooth;

namespace BLModule.Assets.Adapters
{
    class BeaconInfoAdapter : BaseAdapter<BeaconInfo>
    {
        protected IList<BeaconInfo> _beacons;

        public BeaconInfoAdapter(IList<BeaconInfo> beacons)
        {
            _beacons = beacons;
        }

        public override BeaconInfo this[int position] => throw new NotImplementedException();

        public override int Count => _beacons.Count;
      

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.Custom, parent, false);

            var adress = view.FindViewById<TextView>(Resource.Id.textView_name);
            var rssi = view.FindViewById<TextView>(Resource.Id.textView_Rssi);
            
            adress.Text = _beacons[position].adress;
            rssi.Text = _beacons[position].Rssi.ToString();

            return view;
        }
    }
}