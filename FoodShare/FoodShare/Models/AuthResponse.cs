using System;
using System.Collections.Generic;
using System.Text;

namespace FoodShare.Models
{
    public class AuthResponse
    {
        public AuthResponse()
        {
            this.data = new Result();
        }
        public string token { get; set; }
        public Result data { get; set; }

        public class Result
        {
            public Result()
            {
                this.Data = new Data();
            }
            public int Code { get; set; }
            public string Message { get; set; }
            public Data Data { get; set; }

        }

        public class Data
        {
            public int userId { get; set; }
            public string username { get; set; }
            public bool isProfileCompleted { get; set; }
            public string userLocation { get; set; }
        }
    }
}
