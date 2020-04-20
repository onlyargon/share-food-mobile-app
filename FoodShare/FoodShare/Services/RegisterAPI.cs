using FoodShare.Models;
using FoodShare.Models.RegisterUser;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FoodShare.Services
{
    public class RegisterAPI
    {
        public async Task<RegisterResponse> Signup(RegisterRequest registerRequest)
        {
            string url = $"auth/register";
            var requestBody = await Task.Run(() => JsonConvert.SerializeObject(registerRequest));
            using (HttpClient httpClient = new HttpClient())
            {
                RegisterResponse data = new RegisterResponse();
                try
                {

                    httpClient.BaseAddress = new Uri(Constants.BaseUrl);
                    StringContent content = new StringContent(JsonConvert.SerializeObject(registerRequest), Encoding.UTF8, "application/json");
                    HttpResponseMessage result = await httpClient.PostAsync(url, content);
                    string response = await result.Content.ReadAsStringAsync();
                    data = JsonConvert.DeserializeObject<RegisterResponse>(response);

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
