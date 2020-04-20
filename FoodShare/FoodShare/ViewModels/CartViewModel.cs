using FoodShare.Models;
using FoodShare.Models.GetOrdersByUserId;
using FoodShare.Models.PlaceOrder;
using FoodShare.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace FoodShare.ViewModels
{
    public class CartViewModel: BaseViewModel
    {
        private PlaceOrderAPI placeOrderAPI = new PlaceOrderAPI();
        public ObservableCollection<ItemResult> Orders { get; set; }
        public ObservableCollection<ItemResult> CartItemList { get; set; }
        public CartViewModel()
        {
            Orders = new ObservableCollection<ItemResult>();
            CartItemList = new ObservableCollection<ItemResult>();
            Orders = OperationData.CartItemList;
            CartItemList = OperationData.CartItemList;
        }
        public async Task<PlaceOrderResponse> PlaceOrder(PlaceOrderRequest order)
        {
            IsBusy = true;
            var res = await placeOrderAPI.PlaceOrder(order);
            IsBusy = false;
            return res;
        }

        public async Task Change_Quantity(int itemId, double qty)
        {
            foreach (var item in CartItemList)
            {
                if (item.id == itemId)
                {
                    item.numberOfItems = qty;
                }
            }
        }
    }
}
