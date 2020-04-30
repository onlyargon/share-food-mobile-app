using System;
using System.Collections.Generic;
using System.Text;

namespace FoodShare.Models.UserProfile.UpdateUserProfile
{
    public class UpdateUserProfileRequest
    {
        public UpdateUserProfileRequest()
        {
            this.basicInfo = new BasicInfo();
            this.address = new Address();
        }
        public BasicInfo basicInfo { get; set; }
        public Address address { get; set; }
    }
    public class BasicInfo
    {
        public int userId { get; set; }
        public string displayName { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string gender { get; set; }
        public string dob { get; set; }
        public string mobileNumber { get; set; }
        public string email { get; set; }
        public string userLocation { get; set; }
    }

    public class Address
    {
        public int userId { get; set; }
        public string addressLine1 { get; set; }
        public string addressLine2 { get; set; }
        public string addressLine3 { get; set; }
        public string zipCode { get; set; }
        public string state { get; set; }
        public string mobileNumber { get; set; }
        public string city { get; set; }
    }
}
