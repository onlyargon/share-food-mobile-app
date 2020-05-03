using FoodShare.Models;
using FoodShare.Models.GetItemById;
using FoodShare.Services;
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
    public partial class ItemDetailPage : ContentPage
    {
        ItemDetailViewModel _viewModel = new ItemDetailViewModel();
        ItemResult cartItem { get; set; }
        int UserId { get; set; }

        public ItemDetailPage(int itemId)
        {
            InitializeComponent();
            _ = GetItem(itemId);
            BtnAddToCart.IsEnabled = false;
            BindingContext = _viewModel;
        }

        private async void OnBack_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async Task GetItem(int id)
        {
            GetItemByIdRequest item = new GetItemByIdRequest()
            {
                itemId = id.ToString()
            };
            var res = await _viewModel.GetItemById(item);

            if (res != null)
            {
                if (res.Code == 0)
                {
                    cartItem = res.Data;
                    ItemName.Text = res.Data.foodName;
                    ItemDescription.Text = res.Data.description;
                    ItemImage.Source = res.Data.itemImage;
                    ItemPreparedOn.Text = res.Data.preparedOn;
                    ItemExpiryDate.Text = res.Data.expiryDate;
                    ItemUnitPrice.Text = res.Data.unitPrice;
                    QuantityStepper.Maximum = Convert.ToInt32(res.Data.quantity);
                    AvailableQty.Text = res.Data.quantity;
                    UserId = res.Data.userId;
                    SellerName.Text = res.Data.username;
                    BtnAddToCart.IsEnabled = true;
                }
                else
                {
                    await DisplayAlert("Message", "Couldn't retrieve item details. Please try again,", null, "OK");
                }
            }
            else
            {
                await DisplayAlert("Message", "Couldn't retrieve item details. Please try again,", null, "OK");
            }
        }

        private async void BtnAddToCart_Clicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            button.IsEnabled = false;
            try
            {
                cartItem.numberOfItems = QuantityStepper.Value;

                bool itemAlreadyAdded = false;

                foreach (var item in OperationData.CartItemList)
                {
                    if (item.id == cartItem.id)
                    {
                        item.numberOfItems += cartItem.numberOfItems;
                        itemAlreadyAdded = true;
                    }
                }
                if (!itemAlreadyAdded)
                {
                    //remove this condition and keep only the lines inside "if" to add multiple items to the cart
                    if (OperationData.CartItemList.Count == 0)
                    {
                        OperationData.CartItemList.Add(cartItem);

                        BtnAddToCart.Text = "Added to cart";
                        BtnAddToCart.TextColor = Color.FromHex("757575");

                        var answer = await DisplayAlert("Message", cartItem.numberOfItems + " " + cartItem.foodName + "(s) added to cart", "Go to cart", "OK");
                        if (answer)
                        {
                            await Navigation.PushAsync(new CartPage());
                        }
                    }
                    else
                    {
                        var answer = await DisplayAlert("Message", "Card is already full. Please checkout cart or remove item(s) from cart to add this item to the cart.", "Go to cart", "OK");
                        if (answer)
                        {
                            await Navigation.PushAsync(new CartPage());
                        }
                        BtnAddToCart.IsEnabled = true;
                        BtnAddToCart.TextColor = Color.White;
                    }
                }
                else
                {
                    var answer = await DisplayAlert("Message", "Card is already full. Please checkout cart or remove item(s) from cart to add this item to the cart.", "Go to cart", "OK");
                    if (answer)
                    {
                        await Navigation.PushAsync(new CartPage());
                    }
                    BtnAddToCart.IsEnabled = true;
                    BtnAddToCart.TextColor = Color.White;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                
            }
        }

        private void Stepper_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            double QtyValue = e.NewValue;
            QtyLabel.Text = QtyValue.ToString();

            ItemUnitPrice.Text = _viewModel.CalculateItemPrice(QtyValue);
        }

        private void OnSellerName_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ProfilePage(true, UserId));
        }
    }
}