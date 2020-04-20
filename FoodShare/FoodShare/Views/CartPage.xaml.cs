using FoodShare.CustomControls;
using FoodShare.Models;
using FoodShare.ViewModels;
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
    public partial class CartPage : ContentPage
    {
        CartViewModel cartViewModel = new CartViewModel();
        public CartPage()
        {
            InitializeComponent();
            BindingContext = cartViewModel;
            UpdateCart();
        }

        void UpdateCart()
        {
            double totalCost = 0;
            int totalItems = 0;
            //CartList.ItemsSource = OperationData.CartItemList;
            foreach (var CartItem in OperationData.CartItemList)
            {

                totalCost += Convert.ToDouble(CartItem.unitPrice) * CartItem.numberOfItems;
                TotalLabel.Text = totalCost.ToString("N2");

                totalItems += Convert.ToInt32(CartItem.numberOfItems);
                TotalNumOfItems.Text = "( " + totalItems.ToString() + " items )";
            }

            if (OperationData.CartItemList.Count == 0)
            {
                EmptyCart.IsVisible = true;
                CartList.IsVisible = false;
                CartTotalSection.IsVisible = false;
            }
            else
            {
                EmptyCart.IsVisible = false;
                CartList.IsVisible = true;
                CartTotalSection.IsVisible = true;
            }
        }

        private async void Delete_Tapped(object sender, EventArgs e)
        {
            ItemResult item = (ItemResult)((Label)sender).BindingContext;
            var answer = await DisplayAlert("Message", "Are you sure you want to remove '" + item.foodName + "' from cart?", "YES", "NO");
            if (answer)
            {

                if (OperationData.CartItemList != null)
                {
                    foreach (var cartItem in OperationData.CartItemList.ToList())
                    {
                        if (item.id == cartItem.id)
                        {
                            OperationData.CartItemList.Remove(cartItem);
                        }
                    }
                }
                UpdateCart();
            }
        }

        private void Checkout_Clicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            button.IsEnabled = false;
            try
            {
                Navigation.PushAsync(new OrderConfirmationPage());
            }
            catch (Exception ex)
            {

            }
            finally
            {
                button.IsEnabled = true;
            }
        }

        private async void CustomStepper_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            try
            {
                double QtyValue = 0;
                ItemResult item = (ItemResult)((CustomStepper)sender).BindingContext;
                if (item != null)
                {
                    QtyValue = e.NewValue;
                    item.numberOfItems = QtyValue;
                    //await cartViewModel.Change_Quantity(item.id, QtyValue);
                    UpdateCart();
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}