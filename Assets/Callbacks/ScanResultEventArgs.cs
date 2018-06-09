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

namespace BLModule.Assets.Callbacks
{
    public class ScanResultsEventArgs : EventArgs
    {
        IList<ScanResult> results;

        public ScanResultsEventArgs(IList<ScanResult> _results)
        {
            results = _results;
        }

        public IList<ScanResult> Results
        {
            get { return results; }
        }
    }

    public class ScanREsultEventArgs : EventArgs
    {
        ScanResult result;

        public ScanREsultEventArgs(ScanResult _result)
        {
            result = _result;
        }

        public ScanResult Result
        {
            get { return result; }
        }
    }
}