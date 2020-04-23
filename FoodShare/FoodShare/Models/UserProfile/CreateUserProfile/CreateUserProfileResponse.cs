using System;
using System.Collections.Generic;
using System.Text;

namespace FoodShare.Models.UserProfile.CreateUserProfile
{
    public class CreateUserProfileResponse
    {
        public CreateUserProfileResponse()
        {
            this.Data = new UserProfile();
        }
        public int Code { get; set; }
        public string Message { get; set; }
        public UserProfile Data { get; set; }

        public class UserProfile
        {
            
        }
    }
}
