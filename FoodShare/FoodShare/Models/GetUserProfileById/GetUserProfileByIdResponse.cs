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
        }
        public ItemResult item { get; set; }

    }
}
