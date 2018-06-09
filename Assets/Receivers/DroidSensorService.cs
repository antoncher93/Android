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
using Android.Nfc;
using Android.Util;

namespace BLModule.Assets.Receivers
{
    class DroidSensorService : Service, IRangeNotifier, IBeaconConsumer
    {
        IBinder binder;
        Region _region;
        BeaconManager _bManager;


        public void DidRangeBeaconsInRegion(ICollection<Beacon> beacons, Region region)
        {
            
        }

        public override void OnCreate()
        {
            base.OnCreate();


        }

        public void OnBeaconServiceConnect()
        {
            _bManager.SetForegroundBetweenScanPeriod(5000);
            _bManager.SetRangeNotifier(this);
            _region = new AltBeaconOrg.BoundBeacon.Region("Region", null, null, null);
            _bManager.StartRangingBeaconsInRegion(_region);

        }

        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            System.Diagnostics.Debug.WriteLine("Service started");
            _bManager = BeaconManager.GetInstanceForApplication(ApplicationContext);
            _bManager.Bind(this);
            return StartCommandResult.Sticky;

            
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            System.Diagnostics.Debug.WriteLine("Service has beeh destroyed");
        }

        public override IBinder OnBind(Intent intent)
        {
            return binder; 
        }
    }
}