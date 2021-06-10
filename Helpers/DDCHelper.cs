using ETL.DataLoader.Generic.Contracts.FileModels;
using ETL.DataLoader.Generic.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ETL.DataLoader.Generic.Helpers
{
    public class DDCHelper : IDisposable
    {

        HttpClient _client;

        public DDCHelper()
        {
            _client = new HttpClient();
        }

        public async Task<bool> ValidateInventory(DDCFileModel inventory)
        {
            try
            {
                if (string.IsNullOrEmpty(inventory.DetailPageUrl))
                    return false;

                HttpResponseMessage response = await _client.GetAsync(inventory.DetailPageUrl);

                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsStringAsync().Result.ToLower().Contains(inventory.Vin);
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }

            return false;
        }

        public List<DDCImageModel> BuildInventoryDDCImageModel(string urls, string imageStockStatus)
        {
            string[] urlArray = urls.Split(';');
            int index = 0;
            List<DDCImageModel> images = new List<DDCImageModel>();

            foreach (var imageUrl in urlArray)
            {
                var image = new DDCImageModel
                {
                    ImageUrl = imageUrl.ToString(),
                    ImageIsStock = CheckBool(imageStockStatus),
                    ImageIndexNo = index
                };

                images.Add(image);
                index++;
            }

            return images;
        }

        private DateTime? CheckTime(string incomingDate)
        {
            if (DateTime.TryParse(incomingDate, out DateTime date))
                return date;

            return null;
        }

        private bool CheckBool(string incomingBool)
        {
            bool result;
            bool.TryParse(incomingBool, out result);
            return result;
        }

        public void Dispose()
        {
            _client?.Dispose();
        }
    }
}
