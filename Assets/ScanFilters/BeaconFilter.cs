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

namespace BLModule.Assets.ScanFilters
{
    public class BeaconFilter
    {
        protected IList<ScanFilter> _filters;

        public IList<ScanFilter> Filters
        {
            get { return _filters; }
        }

        public static BeaconFilter BuildNew()
        {
            return new BeaconFilter();
        }

        

        public class Creator
        {
            private BeaconFilter filter;


            public Creator AddAdressToFilter(string adress)
            {
                filter._filters.Add(new ScanFilter.Builder()
                    .SetDeviceAddress(adress).Build());
                return this;
            }

            public BeaconFilter Create()
            {
                return this.filter;
            }
        }
    }
}