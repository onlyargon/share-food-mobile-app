using FoodShare.Models;
using FoodShare.Models.GetOrdersByUserId;
using FoodShare.Models.PlaceOrder;
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
    public class PlaceOrderAPI
    {
        public async Task<PlaceOrderResponse> PlaceOrder(PlaceOrderRequest order)
        {
            string url = $"/orders/create";
            var requestBody = await Task.Run(() => JsonConvert.SerializeObject(order));
            using (HttpClient httpClient = new HttpClient())
            {
                PlaceOrderResponse data = new PlaceOrderResponse();
                try
                {
                    var authHeader = new AuthenticationHeaderValue("Bearer", await SecureStorage.GetAsync("auth_token"));
                    httpClient.DefaultRequestHeaders.Authorization = authHeader;
                    httpClient.BaseAddress = new Uri(Constants.BaseUrl);
                    StringContent content = new StringContent(requestBody, Encoding.UTF8, "application/json");
                    HttpResponseMessage result = await httpClient.PostAsync(url, content);
                    string response = await result.Content.ReadAsStringAsync();
                    data = JsonConvert.DeserializeObject<PlaceOrderResponse>(response);

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

        public async Task<GetOrdersByUserIdResponse> GetOrdersByUserId(GetOrdersByUserIdRequest order)
        {
            string url = $"/orders/get-by-uid";
            var requestBody = await Task.Run(() => JsonConvert.SerializeObject(order));
            using (HttpClient httpClient = new HttpClient())
            {
                GetOrdersByUserIdResponse data = new GetOrdersByUserIdResponse();
                try
                {
                    var authHeader = new AuthenticationHeaderValue("Bearer", await SecureStorage.GetAsync("auth_token"));
                    httpClient.DefaultRequestHeaders.Authorization = authHeader;
                    httpClient.BaseAddress = new Uri(Constants.BaseUrl);
                    StringContent content = new StringContent(requestBody, Encoding.UTF8, "application/json");
                    HttpResponseMessage result = await httpClient.PostAsync(url, content);
                    string response = await result.Content.ReadAsStringAsync();
                    data = JsonConvert.DeserializeObject<GetOrdersByUserIdResponse>(response);

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
