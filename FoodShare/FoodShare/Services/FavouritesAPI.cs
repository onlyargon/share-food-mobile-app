using FoodShare.Models;
using FoodShare.Models.DeleteItemById;
using FoodShare.Models.Favourites;
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
    public class FavouritesAPI
    {
        public async Task<AddToFavouritesResponse> AddToFavourites(AddToFavouritesRequest item)
        {
            string url = $"/item/add-fav";
            var requestBody = await Task.Run(() => JsonConvert.SerializeObject(item));
            using (HttpClient httpClient = new HttpClient())
            {
                AddToFavouritesResponse data = new AddToFavouritesResponse();
                try
                {
                    var authHeader = new AuthenticationHeaderValue("Bearer", await SecureStorage.GetAsync("auth_token"));
                    httpClient.DefaultRequestHeaders.Authorization = authHeader;
                    httpClient.BaseAddress = new Uri(Constants.BaseUrl);
                    StringContent content = new StringContent(requestBody, Encoding.UTF8, "application/json");
                    HttpResponseMessage result = await httpClient.PostAsync(url, content);
                    string response = await result.Content.ReadAsStringAsync();
                    data = JsonConvert.DeserializeObject<AddToFavouritesResponse>(response);

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

        public async Task<RemoveFromFavouriteResponse> RemoveFromFavourites(RemoveFromFavouriteRequest item)
        {
            string url = $"/item/remove-fav";
            var requestBody = await Task.Run(() => JsonConvert.SerializeObject(item));
            using (HttpClient httpClient = new HttpClient())
            {
                RemoveFromFavouriteResponse data = new RemoveFromFavouriteResponse();
                try
                {
                    var authHeader = new AuthenticationHeaderValue("Bearer", await SecureStorage.GetAsync("auth_token"));
                    httpClient.DefaultRequestHeaders.Authorization = authHeader;
                    httpClient.BaseAddress = new Uri(Constants.BaseUrl);
                    StringContent content = new StringContent(requestBody, Encoding.UTF8, "application/json");
                    HttpResponseMessage result = await httpClient.PostAsync(url, content);
                    string response = await result.Content.ReadAsStringAsync();
                    data = JsonConvert.DeserializeObject<RemoveFromFavouriteResponse>(response);

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
