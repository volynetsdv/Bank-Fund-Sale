using System;
using Windows.Data.Json;
using System.Net.Http;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_Fund_Sale
{
    class Get_and_Save
    {
        public async string get_json_inf() // <<<<<<<<<<<<<<<<<<<<<<<<ВНИМАНИЕ!!!!! Поправить метод!! 
        {
            var client = new HttpClient(); // Add: using System.Net.Http;
            var response = await client.GetAsync(new Uri("https://bankfund.sale/api/bidding?landing=true&limit=6&project=FG&state=in__completed,canceled,refused&way=auction"));
            var result = await response.Content.ReadAsStringAsync();

            JsonValue json = JsonValue.Parse(result);

            string IDENTITY = json.GetObject().GetNamedString("identity");
            string TITLE = json.GetObject().GetNamedString("title");
            string URL = json.GetObject().GetNamedString("termsFileUrl");

        }
        async void json_inf_file() // <<<<<<<<<<<<<<<<<<<<<<< Написать условие создания файла, если его еще нет. Записать в мейн
        {
            // Create sample file; replace if exists.
            Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            Windows.Storage.StorageFile sampleFile = await storageFolder.CreateFileAsync("sample.txt", Windows.Storage.CreationCollisionOption.ReplaceExisting);
            sampleFile.OpenTransactedWriteAsync(get_json_inf.IDENTITY);
        }
    }
}
