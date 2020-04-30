using FoodShare.Models;
using FoodShare.Models.GetOrdersByUserId;
using FoodShare.Models.PlaceOrder;
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
    public partial class OrderConfirmationPage : ContentPage
    {
        CartViewModel cartViewModel = new CartViewModel();
        ItemsViewModel itemsViewModel = new ItemsViewModel();
        PastOrdersViewModel pastOrdersViewModel = new PastOrdersViewModel();
        public OrderConfirmationPage()
        {
            InitializeComponent();
            //OrdersCollectionViewRow.Height = OperationData.CartItemList.Count * 130 + 150;
            LoadData();
            BindingContext = cartViewModel;
        }

        void LoadData()
        {
            double subTotal = 0, additionalCharges = 0, deliveryCharges = 0, total = 0;
            foreach (var cartItem in OperationData.CartItemList)
            {
                subTotal += Convert.ToDouble(cartItem.unitPrice) * cartItem.numberOfItems;
                total += subTotal + additionalCharges + deliveryCharges;
            }
            SubTotal.Text = subTotal.ToString("N2");
            AdditionalCharges.Text = additionalCharges.ToString("N2");
            Total.Text=total.ToString("N2");
        }

        private async void PlaceOrder_Clicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            button.IsEnabled = false;
            try
            {
                PlaceOrderRequest placeOrderRequest = new PlaceOrderRequest()
                {
                    userId = OperationData.userId,
                    itemId = OperationData.CartItemList[0].id,
                    sellerId = OperationData.CartItemList[0].userId,
                    qty = Convert.ToInt32(OperationData.CartItemList[0].numberOfItems)
                };
                var res = await cartViewModel.PlaceOrder(placeOrderRequest);
                if (res != null)
                {
                    if (res.Code == 0)
                    {
                        await DisplayAlert("Success!", "Order placed successfully with order number " + res.Data.orderNumber, null, "OK");

                        OrderData order = new OrderData()
                        {
                            itemId = OperationData.CartItemList[0].id,
                            foodName = OperationData.CartItemList[0].foodName,
                            id = res.Data.orderId,
                            SellerId = placeOrderRequest.sellerId
                        };
                        await PopupNavigation.PushAsync(new OrderItemStarRatingPopup(order));
                        Application.Current.MainPage = new NavigationPage(new MainPage());
                        OperationData.CartItemList.Clear();
                    }
                    else
                    {
                        await DisplayAlert("Something went wrong", "We could not place your order, Please try again.", null, "OK");
                    }
                }
                else
                {
                    await DisplayAlert("Something went wrong", "We could not place your order, Please try again.", null, "OK");
                }
            }
            catch (Exception ex)
            {
                
            }
            finally
            {
                button.IsEnabled = true;
            }
        }

        private async void AddNote_Tapped(object sender, EventArgs e)
        {
            string result = await DisplayPromptAsync("Add Note", "Note to seller: ",  maxLength: 100, keyboard: Keyboard.Chat);
        }

        private void Change_Address_Clicked(object sender, EventArgs e)
        {

        }
    }
}