using FoodShare.Models;
using FoodShare.Models.DeleteItemById;
using FoodShare.Models.GetItemById;
using FoodShare.Models.OrderRating;
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
    public class OrderRatingAPI
    {
        public async Task<OrderRatingResponse> RateOrder(OrderRatingRequest rating)
        {
            string url = $"/rate/create";
            var requestBody = await Task.Run(() => JsonConvert.SerializeObject(rating));
            using (HttpClient httpClient = new HttpClient())
            {
                OrderRatingResponse data = new OrderRatingResponse();
                try
                {
                    var authHeader = new AuthenticationHeaderValue("Bearer", await SecureStorage.GetAsync("auth_token"));
                    httpClient.DefaultRequestHeaders.Authorization = authHeader;
                    httpClient.BaseAddress = new Uri(Constants.BaseUrl);
                    StringContent content = new StringContent(requestBody, Encoding.UTF8, "application/json");
                    HttpResponseMessage result = await httpClient.PostAsync(url, content);
                    string response = await result.Content.ReadAsStringAsync();
                    data = JsonConvert.DeserializeObject<OrderRatingResponse>(response);

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
