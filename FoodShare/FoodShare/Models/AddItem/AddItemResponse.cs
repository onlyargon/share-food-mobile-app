using System;
using System.Collections.Generic;
using System.Text;

namespace FoodShare.Models
{
    public class AddItemResponse
    {
        public AddItemResponse()
        {
            this.Data = new ItemData();
        }
        public int Code { get; set; }
        public string Message { get; set; }
        public ItemData Data { get; set; }
    }

    public class ItemData
    {
        public int id { get; set; }
        public int userId { get; set; }
        public string foodType { get; set; }
        public string foodName { get; set; }
        public string unitPrice { get; set; }
        public string quantity { get; set; }
        public string description { get; set; }
        public string preparedOn { get; set; }
        public string expiryDate { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }
        public string createdAt { get; set; }
        public string updatedAt { get; set; }

    }
}
