using System;
using System.Collections.Generic;
using Xamarin.Forms;
using static FoodShare.Models.Constants;

namespace FoodShare.Models.GetAllItems
{
    public class Item
    {
        public Item()
        {
            this.Data = new List<Data>();
        }
        public int Code { get; set; }
        public string Message { get; set; }
        public List<Data>  Data { get; set; }
    }

    public class Data
    {
        public int id { get; set; }
        public int userId { get; set; }
        public string foodType { get; set; }
        public string foodName { get; set; }
        public string unitPrice { get; set; }
        public string quantity { get; set; }
        public string description { get; set; }
        public string itemImage { get; set; }
        public string preparedOn { get; set; }
        public string expiryDate { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }
        public bool isFavorite { get; set; }
        public string createdAt { get; set; }
        public string updatedAt { get; set; }

        //additional props
        public DateTime formattedPreparedOn { get; set; }
        public DateTime formattedExpiryDate { get; set; }
        public bool isNotFavorite { get; set; }
        public ImageSource itemImageSource { get; set; }
    }
}