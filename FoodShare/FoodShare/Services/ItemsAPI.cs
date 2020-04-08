using FoodShare.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace FoodShare.Services
{
    public class ItemsAPI
    {
        public async Task<Item> GetAllItems()
        {
            string url = $"/item/get-all-active";

            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    var authHeader = new AuthenticationHeaderValue("Bearer", await SecureStorage.GetAsync("auth_token"));
                    httpClient.DefaultRequestHeaders.Authorization = authHeader;
                    httpClient.BaseAddress = new Uri(Constants.BaseUrl); 
                    HttpResponseMessage result = await httpClient.PostAsync(url, null);
                    string response = await result.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<Item>(response);

                    if (result.IsSuccessStatusCode && result.StatusCode == HttpStatusCode.OK)
                    {
                        return data;
                    }

                    return null;
                }

                catch (Exception exp)
                {
                    return null;
                }
            }
        }

        public async Task<AddItemResponse> AddItem(AddItemRequest item)
        {
            string url = $"/item/create";
            var requestBody = await Task.Run(() => JsonConvert.SerializeObject(item));
            using (HttpClient httpClient = new HttpClient())
            {
                AddItemResponse data = new AddItemResponse();
                try
                {
                    var authHeader = new AuthenticationHeaderValue("Bearer", await SecureStorage.GetAsync("auth_token"));
                    httpClient.DefaultRequestHeaders.Authorization = authHeader;
                    httpClient.BaseAddress = new Uri(Constants.BaseUrl);
                    StringContent content = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
                    HttpResponseMessage result = await httpClient.PostAsync(url, content);
                    string response = await result.Content.ReadAsStringAsync();
                    data = JsonConvert.DeserializeObject<AddItemResponse>(response);

                    if (result.IsSuccessStatusCode && result.StatusCode == HttpStatusCode.OK)
                    {
                        return data;
                    }

                    return null;
                }
                catch (Exception exp)
                {
                    return null;
                }
            }
        }
    }
}
