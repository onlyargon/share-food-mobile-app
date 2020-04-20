using FoodShare.Models;
using FoodShare.Models.GetUserProfileById;
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
    public class UserProfileAPI
    {
        public async Task<GetUserProfileByIdResponse> GetProfileById(GetUserProfileByIdRequest user)
        {
            string url = $"/users/user-profile-by-id";
            var requestBody = await Task.Run(() => JsonConvert.SerializeObject(user));
            using (HttpClient httpClient = new HttpClient())
            {
                GetUserProfileByIdResponse data = new GetUserProfileByIdResponse();
                try
                {
                    var authHeader = new AuthenticationHeaderValue("Bearer", await SecureStorage.GetAsync("auth_token"));
                    httpClient.DefaultRequestHeaders.Authorization = authHeader;
                    httpClient.BaseAddress = new Uri(Constants.BaseUrl);
                    StringContent content = new StringContent(requestBody, Encoding.UTF8, "application/json");
                    HttpResponseMessage result = await httpClient.PostAsync(url, content);
                    string response = await result.Content.ReadAsStringAsync();
                    data = JsonConvert.DeserializeObject<GetUserProfileByIdResponse>(response);

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
