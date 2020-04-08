using System;
using System.Collections.Generic;
using System.Text;

namespace FoodShare.Models
{
    public class RegisterResponse
    {
        public RegisterResponse()
        {
            this.data = new Data();
        }
        public int Code { get; set; }
        public string Message { get; set; }
        public Data data { get; set; }

        public class Data
        {
            public int userId { get; set; }
            public string username { get; set; }
            public bool isProfileCompleted { get; set; }
        }
    }
}
