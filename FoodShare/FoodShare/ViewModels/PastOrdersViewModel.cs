using FoodShare.Models;
using FoodShare.Models.GetOrdersByUserId;
using FoodShare.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FoodShare.ViewModels
{
    public class PastOrdersViewModel: BaseViewModel
    {
        private PlaceOrderAPI placeOrderAPI = new PlaceOrderAPI();
        public ObservableCollection<OrderData> Orders { get; set; }
        public Command LoadOrdersCommand { get; set; }

        public PastOrdersViewModel()
        {
            Orders = new ObservableCollection<OrderData>();
            LoadOrdersCommand = new Command(async () => await ExecuteLoadOrdersCommand());
        }

        public async Task<GetOrdersByUserIdResponse> GetOrdersByUserId(GetOrdersByUserIdRequest user)
        {
            IsBusy = true;
            var res = await placeOrderAPI.GetOrdersByUserId(user);
            IsBusy = false;
            return res;
        }

        public async Task<GetOrdersByUserIdResponse> ExecuteLoadOrdersCommand()
        {
            IsBusy = true;

            try
            {
                Orders.Clear();

                GetOrdersByUserIdRequest getOrdersByUserIdRequest = new GetOrdersByUserIdRequest()
                {
                    userId = OperationData.userId
                };
                var res = await GetOrdersByUserId(getOrdersByUserIdRequest);
                if (res != null)
                {
                    if (res.Code == 0)
                    {
                        foreach (var item in res.Data)
                        {
                            item.orderPrice = (Convert.ToDouble(item.unitPrice) * Convert.ToDouble(item.qty)).ToString("N2");
                            Orders.Add(item);
                        }
                    }
                    return res;
                }
            }
            catch (Exception ex)
            {
                
            }
            finally
            {
                IsRefreshing = false;
                IsBusy = false;
            }
            return null;
        }
    }
}
