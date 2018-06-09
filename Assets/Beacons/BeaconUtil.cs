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
using AltBeaconOrg.BoundBeacon;

namespace BLModule.Assets.Beacons
{
    class BeaconUtil : IBeaconConsumer
    {
        BeaconManager BManager;
        Context _context;


        public BeaconUtil(Context context)
        {
            BManager = BeaconManager.GetInstanceForApplication(context);
            _context = context;
        }


        public Context ApplicationContext => _context;

        public IntPtr Handle => throw new NotImplementedException();

        public bool BindService(Intent intent, IServiceConnection serviceConnection, [GeneratedEnum] Bind flags)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void OnBeaconServiceConnect()
        {
            //BManager.SetRangeNotifier(new IRangeNotifier())
        }

        public void UnbindService(IServiceConnection serviceConnection)
        {
            throw new NotImplementedException();
        }
    }
}