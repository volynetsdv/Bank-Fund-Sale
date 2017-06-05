using System;
using Windows.Data.Json;
using System.Net.Http;
using Windows.Storage;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_Fund_Sale
{
    class Get_and_Save
    {
        //string IDENTITY;
        //string TITLE;
        //string URL;
        //public Get_and_Save(string IDENTITY, string TITLE, string URL)
        //{
        //    this.IDENTITY = IDENTITY;this.TITLE = TITLE;this.URL = URL;
        //}
        public async void get_json_inf() // <<<<<<<<<<<<<<<<<<<<<<<<ВНИМАНИЕ!!!!! Поправить метод!! 
        {
            var client = new HttpClient(); // Add: using System.Net.Http;
            var response = await client.GetAsync(new Uri("https://bankfund.sale/api/bidding?landing=true&limit=6&project=FG&state=in__completed,canceled,refused&way=auction"));
            var result = await response.Content.ReadAsStringAsync();

            JsonValue json = JsonValue.Parse(result);

            string IDENTITY = json.GetObject().GetNamedString("identity");
            string TITLE = json.GetObject().GetNamedString("title");
            string URL = json.GetObject().GetNamedString("termsFileUrl");

            string data = IDENTITY + " * " + TITLE + " * " + URL;
            //Get_and_Save data = new Get_and_Save(IDENTITY, TITLE, URL);
            
            // Create sample file and save "data"
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            StorageFile infFile = await storageFolder.CreateFileAsync("json_inf.txt", Windows.Storage.CreationCollisionOption.OpenIfExists);
            await FileIO.WriteTextAsync(infFile, data);
          //  await new Windows.UI.Popups.MessageDialog("Файл создан и сохранен").ShowAsync();

        }
    }
}
