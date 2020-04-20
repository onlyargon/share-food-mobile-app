using FoodShare.Models;
using FoodShare.Models.DeleteItemById;
using FoodShare.Models.GetItemById;
using FoodShare.Models.UpdateItem;
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
                    StringContent content = new StringContent(requestBody, Encoding.UTF8, "application/json");
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

        public async Task<GetItemByIdResponse> GetItemById(GetItemByIdRequest item)
        {
            string url = $"/item/get-item-details-by-item-id";
            var requestBody = await Task.Run(() => JsonConvert.SerializeObject(item));
            using (HttpClient httpClient = new HttpClient())
            {
                GetItemByIdResponse data = new GetItemByIdResponse();
                try
                {
                    var authHeader = new AuthenticationHeaderValue("Bearer", await SecureStorage.GetAsync("auth_token"));
                    httpClient.DefaultRequestHeaders.Authorization = authHeader;
                    httpClient.BaseAddress = new Uri(Constants.BaseUrl);
                    StringContent content = new StringContent(requestBody, Encoding.UTF8, "application/json");
                    HttpResponseMessage result = await httpClient.PostAsync(url, content);
                    string response = await result.Content.ReadAsStringAsync();
                    data = JsonConvert.DeserializeObject<GetItemByIdResponse>(response);

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

        public async Task<DeleteItemByIdResponse> DeleteItemById(DeleteItemByIdRequest item)
        {
            string url = $"/item/delete";
            var requestBody = await Task.Run(() => JsonConvert.SerializeObject(item));
            using (HttpClient httpClient = new HttpClient())
            {
                DeleteItemByIdResponse data = new DeleteItemByIdResponse();
                try
                {
                    var authHeader = new AuthenticationHeaderValue("Bearer", await SecureStorage.GetAsync("auth_token"));
                    httpClient.DefaultRequestHeaders.Authorization = authHeader;
                    httpClient.BaseAddress = new Uri(Constants.BaseUrl);
                    StringContent content = new StringContent(requestBody, Encoding.UTF8, "application/json");
                    HttpResponseMessage result = await httpClient.PostAsync(url, content);
                    string response = await result.Content.ReadAsStringAsync();
                    data = JsonConvert.DeserializeObject<DeleteItemByIdResponse>(response);

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

        public async Task<UpdateItemResponse> UpdateItem(UpdateItemRequest item)
        {
            string url = $"/item/update";
            var requestBody = await Task.Run(() => JsonConvert.SerializeObject(item));
            using (HttpClient httpClient = new HttpClient())
            {
                UpdateItemResponse data = new UpdateItemResponse();
                try
                {
                    var authHeader = new AuthenticationHeaderValue("Bearer", await SecureStorage.GetAsync("auth_token"));
                    httpClient.DefaultRequestHeaders.Authorization = authHeader;
                    httpClient.BaseAddress = new Uri(Constants.BaseUrl);
                    StringContent content = new StringContent(requestBody, Encoding.UTF8, "application/json");
                    HttpResponseMessage result = await httpClient.PostAsync(url, content);
                    string response = await result.Content.ReadAsStringAsync();
                    data = JsonConvert.DeserializeObject<UpdateItemResponse>(response);

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
