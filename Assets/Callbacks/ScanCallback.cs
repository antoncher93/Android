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
using Android.Bluetooth.LE;
using Java.Interop;
using Java.Lang;
using BLModule.Assets.Callbacks;

namespace BLModule.Assets.ScanCallbacks
{
    public class MyScanCallback : ScanCallback
    {
        public override JniPeerMembers JniPeerMembers => base.JniPeerMembers;

        protected override IntPtr ThresholdClass => base.ThresholdClass;

        protected override Type ThresholdType => base.ThresholdType;

        public event Action<object, ScanResultsEventArgs> ScanResultsEvent;
        public event Action<object, ScanREsultEventArgs> ResultEvent;

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override bool Equals(Java.Lang.Object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override void OnBatchScanResults(IList<ScanResult> results)
        {
            base.OnBatchScanResults(results);

            ScanResultsEvent(this, new ScanResultsEventArgs(results));


            System.Diagnostics.Debug.WriteLine("===========================================");
            foreach(var result in results)
            {
                System.Diagnostics.Debug.WriteLine("Adress: " + result.Device.Address + ", RSSI: " + result.Rssi);
            }
        }

        public override void OnScanFailed([GeneratedEnum] ScanFailure errorCode)
        {
            base.OnScanFailed(errorCode);
        }

        public override void OnScanResult([GeneratedEnum] ScanCallbackType callbackType, ScanResult result)
        {
            base.OnScanResult(callbackType, result);

            //ResultEvent(this, new ScanREsultEventArgs(result));

            System.Diagnostics.Debug.WriteLine("===========================================");
            System.Diagnostics.Debug.WriteLine("Adress: " + result.Device.Address + ", RSSI: " + result.Rssi);
        }

        public override string ToString()
        {
            return base.ToString();
        }

        protected override Java.Lang.Object Clone()
        {
            return base.Clone();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        protected override void JavaFinalize()
        {
            base.JavaFinalize();
        }
    }
}