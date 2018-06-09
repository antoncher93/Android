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
using System.Runtime.Serialization;


namespace BLModule.Assets.Beacons
{
    [DataContract]
    public class BeaconInfo
    {
        string _adress;
        int rssi;
        string name;

        [DataMember]
        public string adress
        {
            get { return _adress; }
            set { _adress = value;  }
        }

        [DataMember]
        public int Rssi
        {
            get { return rssi; }
            set { rssi = value; }
        }

        [DataMember]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public class Creator
        {
            private BeaconInfo beaconInfo;

            public Creator()
            {
                beaconInfo = new BeaconInfo();
            }

            public Creator SetAdress(string adress)
            {
                beaconInfo._adress = adress;
                return this;
            }

            public Creator SetRssi(int _rssi)
            {
                beaconInfo.rssi = _rssi;
                return this;
            }

            public Creator SetName(string _name)
            {
                if(_name != null) beaconInfo.name = _name;

                return this;
            }

            public BeaconInfo Create()
            {
                return this.beaconInfo;
            }
        }
    }
}