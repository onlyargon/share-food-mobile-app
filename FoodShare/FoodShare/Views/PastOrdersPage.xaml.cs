using FoodShare.Models;
using FoodShare.Models.GetOrdersByUserId;
using FoodShare.ViewModels;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FoodShare.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PastOrdersPage : ContentPage
    {
        PastOrdersViewModel viewModel = new PastOrdersViewModel();
        public PastOrdersPage()
        {
            InitializeComponent();
            _ = LoadOrders();
            BindingContext = viewModel;
        }

        async Task LoadOrders()
        {
            var res = await viewModel.ExecuteLoadOrdersCommand();
            if (res != null)
            {
                if (res.Code == 0)
                {
                    if (res.Data.Count == 0)
                    {
                        OrdersCollectionView.IsVisible = false;
                        NoOrdersView.IsVisible = true;
                    }
                    else
                    {
                        OrdersCollectionView.IsVisible = true;
                        NoOrdersView.IsVisible = false;
                    }
                }
                else
                {
                    await DisplayAlert("Message", "Could not retrieve past orders, Please try again.", null, "OK");
                }
            }
            else
            {
                await DisplayAlert("Message", "Could not retrieve past orders, Please try again.", null, "OK");
                OrdersCollectionView.IsVisible = false;
                NoOrdersView.IsVisible = true;
            }
        }

        private void RateOrder_Clicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            button.IsEnabled = false;

            try
            {
                OrderData orderData = (OrderData)button.BindingContext;
                PopupNavigation.PushAsync(new OrderItemStarRatingPopup(orderData));
            }
            catch (Exception ex)
            {

            }
            finally
            {
                button.IsEnabled = true;
            }
        }
    }
}