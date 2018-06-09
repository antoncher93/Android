using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BLModule.Assets.Beacons
{
    [DataContract]
    public class BeaconsData
    {
        // Класс для сохранения данных о прочитвнных биконах
        public BeaconsData(IList<BeaconInfo> _beacons, DateTime dateTime)
        {
            this.Beacons = _beacons;
            this.dateTime = dateTime;
        }

        [DataMember]
        public BeaconInfo this[int i]
        {
            get { return Beacons[i]; }
            set { Beacons.Add(value); }
        }

        [DataMember]
        public IList<BeaconInfo> Beacons; // список прочитанных биконов

        [DataMember]
        public DateTime dateTime; // время прочтения (записи)
    }

    [DataContract]
    class BeaconsDataArray
    {
        [DataMember]
        IList<BeaconsData> Beacons = new List<BeaconsData>();

        public BeaconsData this[int i]
        {
            get { return Beacons[i]; }
            set { Beacons.Add(value); }
        }

        public void Add(BeaconsData data)
        {
            Beacons.Add(data);
        }
    }
}