using FoodShare.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FoodShare.Services
{
    public class LoginAPI
    {
        public async Task<AuthResponse> Login(User user)
        {
            string url = $"/auth/validate";
            var requestBody = await Task.Run(() => JsonConvert.SerializeObject(user));
            using (HttpClient httpClient = new HttpClient())
            {
                AuthResponse data = new AuthResponse();
                try
                {

                    httpClient.BaseAddress = new Uri(Constants.BaseUrl);
                    StringContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                    HttpResponseMessage result = await httpClient.PostAsync(url, content);
                    string response = await result.Content.ReadAsStringAsync();
                    data = JsonConvert.DeserializeObject<AuthResponse>(response);

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
