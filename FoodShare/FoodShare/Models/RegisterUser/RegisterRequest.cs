using System;
using System.Collections.Generic;
using System.Text;

namespace FoodShare.Models.RegisterUser
{
    public class RegisterRequest
    {
        public RegisterRequest()
        {
            this.basicInfo = new BasicInfo();
        }
        public BasicInfo basicInfo { get; set; }
    }

    public class BasicInfo
    {
        public string networkStatus1 { get; set; }
        public string networkStatus2 { get; set; }
        public string userType { get; set; }
    }
}
