using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BLModule.Assets.Data
{
    class DataPusher
    {

        string GetFilePath(string fileName)
        {
            string p = Path.Combine(GetDocsPath(), fileName);

            //p = p.Replace('/', '\\');
            //p = p.Remove(0, 1);
            return p;
        }

        string GetDocsPath()
        {
            return Android.OS.Environment.ExternalStorageDirectory.Path;
        }

        private void AddText(string fileName, string text)
        {
            string filePath = GetFilePath(fileName);
            StreamWriter writer = File.AppendText(filePath);
            //writer.WriteAsync(text);

            writer.Write(text);
            writer.Close();

        }

        public void SaveData(object ob, string fileName)
        {
            if (ob != null)
            {
                AddLines(fileName, JsonConvert.SerializeObject(ob));
            }

        }

        public void AddLines(string fileName, string text)
        {
            string filePath = GetFilePath(fileName);
            File.AppendAllLines(filePath, new string[] { text });
        }

        public async Task SaveDataAsync(object ob, string fileName)
        {
            if(ob!= null)
            {
                //await AddTextAsync(fileName, JsonConvert.SerializeObject(ob));

                await AddLinesAsync(fileName, JsonConvert.SerializeObject(ob));
            }

            
        }

        public async Task AddTextAsync(string fileName, string text)
        {
            string filePath = GetFilePath(fileName);

            using (StreamWriter writer = File.AppendText(filePath))
            {
                await writer.WriteAsync(text);

            }
        }

        public async Task AddLinesAsync(string fileName, string text)
        {
            string filePath = GetFilePath(fileName);

            await Task.Factory.StartNew(() =>
            {
                File.AppendAllLines(fileName, new string[] { text });
            });
        }
    }
}