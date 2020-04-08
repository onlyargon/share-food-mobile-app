using FoodShare.Models;
using FoodShare.Services;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FoodShare.ViewModels
{
    public class AuthViewModel : BaseViewModel
    {
        private LoginAPI loginAPI = new LoginAPI();
        private RegisterAPI registerAPI = new RegisterAPI();

        public async Task<AuthResponse> Login(User user)
        {
            IsBusy = true;

            SHA256 sha256Hash = SHA256.Create();
            byte[] networkStatus1Bytes = Encoding.UTF8.GetBytes(user.networkStatus1);
            byte[] networkStatus2Bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(user.networkStatus2));

            User reqObj = new User()
            {
                networkStatus1 = Convert.ToBase64String(networkStatus1Bytes),
                networkStatus2 = Convert.ToBase64String(networkStatus2Bytes)
            };

            var res = await loginAPI.Login(reqObj);
            IsBusy = false;
            return res;
        }

        public async Task<RegisterResponse> Signup(RegisterRequest user)
        {
            IsBusy = true;

            SHA256 sha256Hash = SHA256.Create();
            byte[] networkStatus1Bytes = Encoding.UTF8.GetBytes(user.basicInfo.networkStatus1);
            byte[] networkStatus2Bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(user.basicInfo.networkStatus2));

            RegisterRequest reqObj = new RegisterRequest()
            {
                basicInfo = new BasicInfo
                {
                    networkStatus1 = Convert.ToBase64String(networkStatus1Bytes),
                    networkStatus2 = Convert.ToBase64String(networkStatus2Bytes),
                    userType = user.basicInfo.userType
                }
            };

            var res = await registerAPI.Signup(reqObj);
            IsBusy = false;
            return res;
        }
    }
}
