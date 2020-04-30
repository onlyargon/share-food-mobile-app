using System;
using System.Collections.Generic;
using System.Text;

namespace FoodShare.Models.UpdateItem
{
    public class UpdateItemRequest
	{
        public UpdateItemRequest()
        {

        }

        public int id { get; set; }
        public int userId { get; set; }
        public string foodType { get; set; }
        public string foodName { get; set; }
        public string unitPrice { get; set; }
        public string quantity { get; set; }
        public string description { get; set; }
        public string userLocation { get; set; }
        public string preparedOn { get; set; }
        public string expiryDate { get; set; }
        public bool isActive { get; set; }
    }
}
