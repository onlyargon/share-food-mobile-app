using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace FoodShare.Models
{
    public class GetItemByIdResponse
    {
        public GetItemByIdResponse()
        {
            this.Data = new ItemResult();
        }
        public int Code { get; set; }
        public string Message { get; set; }
        public ItemResult Data { get; set; }
    }

    public class ItemResult
    {
        public int id { get; set; }
        public int userId { get; set; }
        public string username { get; set; }
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
        public string createdAt { get; set; }
        public string updatedAt { get; set; }

        //additional props
        public double numberOfItems { get; set; }
        public string status { get; set; }
        public Color statusColor { get; set; }
        public bool isIconsVisible { get; set; }
    }
}
