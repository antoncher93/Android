using Android.App;
using Android.Widget;
using Android.OS;
using Android.Bluetooth;
using System.Collections.Generic;
using System.Linq;
//using Android.Bluetooth.LE;
using System;
using Android.Runtime;
using System.Collections;
using System.Threading.Tasks;
using System.Threading;
using BLModule.Assets.Beacons;
using BLModule.Assets.Adapters;
using Android.Bluetooth.LE;
using Android.Content;
using BLModule.Assets.ScanCallbacks;
using BLModule.Assets.ScanFilters;
using BLModule.Assets.Callbacks;
using Newtonsoft.Json.Linq;
using System.IO;
using Newtonsoft.Json.Converters;
using Java.IO;
using BLModule.Assets.Data;
using Com.Streethawk.Library.Beacon;
using AltBeaconOrg.BoundBeacon;

namespace BLModule
{
    [Activity(Label = "BLModule", MainLauncher = true)]
    public class MainActivity : Activity, IBeaconConsumer  /*, BluetoothAdapter.ILeScanCallback*/
    {
        Button button_Try;
        TextView textView_Info;
        TextView textView_scanInfo;
        ListView listView_devices;
        BluetoothAdapter adapter;

        BluetoothManager manager;
        BluetoothLeScanner BLScaner;

        DataPusher pusher;

      
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            button_Try = FindViewById<Button>(Resource.Id.button_try);
            listView_devices = FindViewById<ListView>(Resource.Id.listView1);
            textView_Info = FindViewById<TextView>(Resource.Id.textView1);
            textView_scanInfo = FindViewById<TextView>(Resource.Id.textView_scan);


            manager = (BluetoothManager)Application.Context.GetSystemService(Context.BluetoothService);
            adapter = manager.Adapter;


            pusher = new DataPusher();


            Count();
            
            button_Try.Click += delegate
            {
                //начать фоновое сканирование
                Task.Factory.StartNew(() =>
                {
                    StartScan();


                });


                //NewMethod();


            };
        }

       private void Count()
        {
            Task.Run(() =>
            {
                int i = 0;
                while (true)
                {
                    switch (i)
                    {
                        case 0:
                            RunOnUiThread(() =>
                            {
                                i = 1;
                                textView_Info.Text = "";
                                
                            });
                            Thread.Sleep(200);
                            break;

                        case 1:
                            RunOnUiThread(() =>
                            {
                                i = 2;
                                textView_Info.Text = ".";
                                
                            });
                            Thread.Sleep(200);
                            break;
                        case 2:
                            RunOnUiThread(() =>
                            {
                                i = 3;
                                textView_Info.Text = "..";
                                
                            });
                            Thread.Sleep(200);
                            break;
                        case 3:
                            RunOnUiThread(() =>
                            {
                                i = 0;
                                textView_Info.Text = "...";
                            });
                            Thread.Sleep(200);
                            break;
                    }
                }
            });
        }

        private void NewMethod()
        {
            Task.Factory.StartNew(() =>
            {
                bool started = false;
                BluetoothAdapter btAdapter = BluetoothAdapter.DefaultAdapter;

                while (true)
                {
                    if(btAdapter.IsDiscovering)
                    {
                        var devices = btAdapter.BondedDevices;

                        foreach (var d in devices)
                        {
                            System.Diagnostics.Debug.WriteLine(d.Address);

                        }
                        System.Diagnostics.Debug.WriteLine("------------------------------");

                        

                        Thread.Sleep(1000);
                    }
                    else
                    {
                        btAdapter.StartDiscovery();
                    }
                    

                    
                }

                


            });

            
        }
       
       private void StartScan()
        {
            BLScaner = adapter.BluetoothLeScanner;

            ScanSettings setting = new ScanSettings.Builder()
                .SetReportDelay(1000)
                .SetScanMode(Android.Bluetooth.LE.ScanMode.LowLatency)
                .Build();

            

            MyScanCallback myScanCallback = new MyScanCallback();

            myScanCallback.ScanResultsEvent += UpdateScanResult;
            myScanCallback.ScanResultsEvent += SaveScanResult;
            myScanCallback.ScanResultsEvent += ScanShowIteration;

            //var scanFilter = new BeaconFilter.Creator()
            //    .AddAdressToFilter("F4:B8:5E:DE:A7:30")
            //    .AddAdressToFilter("F4:B8:5E:DE:A5:63")
            //    .AddAdressToFilter("F4:B8:5E:DE:B4:C1")
            //    .Create();

            ScanFilter scanFilter = new ScanFilter.Builder()
                .SetDeviceName(null)
                .Build();

            List<ScanFilter> filters = new List<ScanFilter>
            {
                new ScanFilter.Builder().SetDeviceAddress("F4:B8:5E:DE:A7:30").Build(),
                new ScanFilter.Builder().SetDeviceAddress("F4:B8:5E:DE:B4:C1").Build(),
                new ScanFilter.Builder().SetDeviceAddress("F4:B8:5E:DE:A5:63").Build()
            };

            BLScaner.StartScan(filters, setting, myScanCallback);

            //BLScaner.StartScan(myScanCallback);
        }

        private void Discovering()
        {
            
        }


        private void UpdateScanResult(object sender, ScanResultsEventArgs e)
        {
            var beacons = GetBeconsFromScanResult(e.Results);

            RunOnUiThread(() =>
            {
                var adapter = new BeaconInfoAdapter(beacons);

                listView_devices.Adapter = adapter;
            });

            
        }

        private void SaveScanResult(object sender, ScanResultsEventArgs e)
        {
            var data = new BeaconsData( GetBeconsFromScanResult(e.Results), DateTime.Now);

            //pusher.SaveData(data, "BeaconsData.json");

            Task.Factory.StartNew(() =>
            {
                pusher.SaveData(data, "BeaconsData.json");
            });
        }

        private IList<BeaconInfo> GetBeconsFromScanResult(IList<ScanResult> results)
        {
            return results.Select(b =>
            {
                return new BeaconInfo.Creator()
                .SetName(b.Device.Name)
                .SetAdress(b.Device.Address)
                .SetRssi(b.Rssi)
                .Create();
            }).ToList();
        }

        private void ScanShowIteration(object sender, ScanResultsEventArgs e)
        {
            RunOnUiThread(() =>
            {
                if (textView_scanInfo.Text == "Scanning...")
                    textView_scanInfo.Text = "Scanning";
                else textView_scanInfo.Text += '.';
            });
        }

        public void OnBeaconServiceConnect()
        {
            throw new NotImplementedException();
        }
    }

    

   
}

