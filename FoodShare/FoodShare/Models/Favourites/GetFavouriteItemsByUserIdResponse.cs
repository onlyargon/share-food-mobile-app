using System;
using System.Collections.Generic;
using System.Text;

namespace FoodShare.Models.Favourites
{
    public class GetFavouriteItemsByUserIdResponse
    {
        public GetFavouriteItemsByUserIdResponse()
        {
            this.Data = new List<FavouriteItems>();
        }
        public int Code { get; set; }
        public string Message { get; set; }
        public List<FavouriteItems> Data { get; set; }

        //additional props
        public bool isNoFavItemsVisible { get; set; }

        public class FavouriteItems
        {
            public int id { get; set; }
            public int userId { get; set; }
            public string foodType { get; set; }
            public string foodName { get; set; }
            public string quantity { get; set; }
            public string description { get; set; }
            public string unitPrice { get; set; }
            public string itemImage { get; set; }
            public string preparedOn { get; set; }
            public string expiryDate { get; set; }
            public bool isActive { get; set; }
            public bool isDeleted { get; set; }
            public string createdAt { get; set; }
            public string updatedAt { get; set; }
        }
    }
}
