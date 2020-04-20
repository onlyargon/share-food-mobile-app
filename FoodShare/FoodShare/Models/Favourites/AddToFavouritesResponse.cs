using System;
using System.Collections.Generic;
using System.Text;

namespace FoodShare.Models.Favourites
{
    public class AddToFavouritesResponse
    {
        public AddToFavouritesResponse()
        {
            this.Data = new ItemFavourite();
        }
        public int Code { get; set; }
        public string Message { get; set; }
        public ItemFavourite Data { get; set; }
    }

    public class ItemFavourite
    {
        public bool isItem { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }
        public int id { get; set; }
        public string userId { get; set; }
        public string itemId { get; set; }
        public string updatedAt { get; set; }
        public string createdAt { get; set; }
    }
}
