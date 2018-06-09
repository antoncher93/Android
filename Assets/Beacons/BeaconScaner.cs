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
using Robotics.Mobile.Core.Bluetooth.LE;
using Android.Bluetooth.LE;
using Android.Bluetooth;

namespace BLModule.Assets.Beacons
{
    public class BeaconScaner
    {
        Robotics.Mobile.Core.Bluetooth.LE.Adapter _bleCoreAdapter;

        public BeaconScaner()
        {
            _bleCoreAdapter = new Robotics.Mobile.Core.Bluetooth.LE.Adapter();
        }

        public void BeginScan()
        {
            
            _bleCoreAdapter.ScanTimeoutElapsed += RestartScan;
            _bleCoreAdapter.StartScanningForDevices();
        }

        public IList<IDevice> GetScanResult()
        {
            return _bleCoreAdapter.DiscoveredDevices;
        }

        private void RestartScan(object sender, EventArgs e)
        {
            _bleCoreAdapter.StartScanningForDevices();
            
        }

    }
}