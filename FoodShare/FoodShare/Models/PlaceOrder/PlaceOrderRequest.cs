using System;
using System.Collections.Generic;
using System.Text;

namespace FoodShare.Models.PlaceOrder
{
    public class PlaceOrderRequest
    {
        public int userId { get; set; }
        public int itemId { get; set; }
        public int sellerId { get; set; }
        public int qty { get; set; }
    }
}
