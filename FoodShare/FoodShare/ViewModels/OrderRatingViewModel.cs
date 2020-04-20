using FoodShare.Models.OrderRating;
using FoodShare.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FoodShare.ViewModels
{
    public class OrderRatingViewModel: BaseViewModel
    {
        OrderRatingAPI orderRatingAPI = new OrderRatingAPI();
        public async Task<OrderRatingResponse> RateOrder(OrderRatingRequest rating)
        {
            IsBusy = true;
            var res = await orderRatingAPI.RateOrder(rating);
            IsBusy = false;
            return res;
        }
    }
}
