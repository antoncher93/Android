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
using Android.Bluetooth;
using Robotics.Mobile.Core.Bluetooth.LE;

namespace BLModule.Assets.Adapters
{
    class BeaconAdapter : BaseAdapter
    {
        protected IList<IDevice> _devices;

        public BeaconAdapter(IList<IDevice> devices)
        {
            _devices = devices;
        }

        public override int Count => _devices.Count;

        public override Java.Lang.Object GetItem(int position)
        {
            return (BluetoothDevice)_devices[position].NativeDevice;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.Custom, parent, false);

            var adress = view.FindViewById<TextView>(Resource.Id.textView_name);
            var rssi = view.FindViewById<TextView>(Resource.Id.textView_Rssi);
            BluetoothDevice dev = (BluetoothDevice)_devices[position].NativeDevice;
            adress.Text = dev.Address;
            rssi.Text = _devices[position].Rssi.ToString();

            return view;  
        }
    }
}