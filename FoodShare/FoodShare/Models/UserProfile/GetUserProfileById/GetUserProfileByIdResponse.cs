using FoodShare.Models.UserProfile.CreateUserProfile;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodShare.Models.GetUserProfileById
{
    public class GetUserProfileByIdResponse
    {
        public GetUserProfileByIdResponse()
        {
            this.Data = new ResultData();
        }
        public int Code { get; set; }
        public string Message { get; set; }
        public ResultData Data { get; set; }
    }
    public class ResultData
    {
        public ResultData()
        {
            this.userInfo = new UserInfo();
            this.itemsWithRating = new List<ItemsWithRating>();
        }
        public UserInfo userInfo { get; set; }
        public BasicInfo basicInfo { get; set; }
        public Address address { get; set; }
        public List<ItemsWithRating> itemsWithRating { get; set; }
    }
    public class UserInfo
    {
        public string username { get; set; }
        public string joinedDate { get; set; }
    }
    public class ItemsWithRating
    {
        public ItemsWithRating()
        {
            this.item = new ItemResult();
            this.rating = new List<Rating>();
        }
        public ItemResult item { get; set; }
        public List<Rating> rating { get; set; }
    }

    public class Rating
    {
        public int userId { get; set; }
        public string itemId { get; set; }
        public string orderId { get; set; }
        public string comment { get; set; }
        public int starRating { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }
        public string createdAt { get; set; }
        public string updatedAt { get; set; }
    }
}
