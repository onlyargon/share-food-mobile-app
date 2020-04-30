using System;
using System.Collections.Generic;
using System.Text;

namespace FoodShare.Models.GetAllItems
{
    public class GetAllItemsRequest
    {
        public int userId { get; set; }
        public string userLocation { get; set; }
    }
}
