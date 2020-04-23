using System;
using System.Collections.Generic;
using System.Text;

namespace FoodShare.Models.PlaceOrder
{
    public class PlaceOrderResponse
    {
        public PlaceOrderResponse()
        {
            this.Data = new PlaceOrderData();
        }
        public int Code { get; set; }
        public string Message { get; set; }
        public PlaceOrderData Data { get; set; }
    }

    public class PlaceOrderData
    {
        public int orderId { get; set; }
        public string orderNumber { get; set; }
    }
}
