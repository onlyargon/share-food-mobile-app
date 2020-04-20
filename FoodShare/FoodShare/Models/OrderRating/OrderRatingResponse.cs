using System;
using System.Collections.Generic;
using System.Text;

namespace FoodShare.Models.OrderRating
{
    public class OrderRatingResponse
    {
        public OrderRatingResponse()
        {
            this.Data = new RatingData();
        }
        public int Code { get; set; }
        public string Message { get; set; }
        public RatingData Data { get; set; }

        public class RatingData
        {
            public string username { get; set; }
            public bool isActive { get; set; }
            public bool isDeleted { get; set; }
            public int userId { get; set; }
            public int itemId { get; set; }
            public int orderId { get; set; }
            public string comment { get; set; }
            public int starRating { get; set; }
            public string updatedAt { get; set; }
            public string createdAt { get; set; }
        }
    }
}
