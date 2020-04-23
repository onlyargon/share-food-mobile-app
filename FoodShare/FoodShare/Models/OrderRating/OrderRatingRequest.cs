using System;
using System.Collections.Generic;
using System.Text;

namespace FoodShare.Models.OrderRating
{
    public class OrderRatingRequest
	{
        public int userId { get; set; }
        public int itemId { get; set; }
        public int sellerId { get; set; }
        public int orderId { get; set; }
        public string comment { get; set; }
        public int starRating { get; set; }
    }
}
