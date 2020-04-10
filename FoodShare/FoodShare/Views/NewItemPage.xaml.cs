using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using FoodShare.Models;
using Xamarin.Essentials;
using FoodShare.ViewModels;
using static FoodShare.ViewModels.ItemsViewModel;

namespace FoodShare.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class NewItemPage : ContentPage
    {
        ItemsViewModel itemsViewModel = new ItemsViewModel();
        public Data Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();
            DatePrepared.MaximumDate = DateTime.Today;
            DateExpiry.MinimumDate = DateTime.Today;
            BindingContext = itemsViewModel;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            var IsValid = ValidateItem();

            if (IsValid)
            {
                FoodType foodType = (FoodType)FoodTypeSelector.SelectedItem;
                AddItemRequest addItemRequest = new AddItemRequest
                {
                    userId = OperationData.userId,
                    foodType = foodType.description,
                    foodName = FoodName.Text,
                    unitPrice = UnitPrice.Text,
                    quantity = Quantity.Text,
                    description = Description.Text,
                    preparedOn = DatePrepared.Date.Date.ToString(),
                    expiryDate = DateExpiry.Date.Date.ToString()
                };
                //Item = new Data
                //{
                //    userId = OperationData.userId,
                //    foodType = foodType.description,
                //    foodName = FoodName.Text,
                //    unitPrice = UnitPrice.Text,
                //    quantity = Quantity.Text,
                //    description = Description.Text,
                //    preparedOn = DatePrepared.Date.Date.ToString(),
                //    expiryDate = DateExpiry.Date.Date.ToString()
                //};

                var res = await itemsViewModel.AddItem(addItemRequest);

                if (res != null)
                {
                    if (res.Code == 0)
                    {
                        MessagingCenter.Send(this, "AddItem", Item);
                        DisplayAlert("Message", "Item added successfully!", null, "OK");
                        await Navigation.PopModalAsync();
                    }
                    else
                    {
                        await DisplayAlert("Message", "Adding Item Failed. Please try again", null, "OK");
                    }
                }
                else
                {
                    await DisplayAlert("Message", "Adding Item Failed. Please try again", null, "OK");
                }
            }
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private void BtnClear_Clicked(object sender, EventArgs e)
        {
            FoodTypeSelector.SelectedItem = null;
            FoodName.Text = "";
            UnitPrice.Text = "";
            Quantity.Text = "";
            Description.Text = "";
            DatePrepared.Date = DateTime.Today;
            DateExpiry.Date = DateTime.Today;
        }

        private bool ValidateItem()
        {
            bool isItemCompleted = false;

            if (string.IsNullOrEmpty(FoodName.Text))
            {
                FoodNameErrorLabel.IsVisible = true;
            }
            else if (!string.IsNullOrEmpty(FoodName.Text) && string.IsNullOrEmpty(UnitPrice.Text))
            {
                UnitPriceErrorLabel.IsVisible = true;
            }
            else if (!string.IsNullOrEmpty(FoodName.Text) && !string.IsNullOrEmpty(UnitPrice.Text) && string.IsNullOrEmpty(Quantity.Text))
            {
                UnitPriceErrorLabel.IsVisible = true;
            }
            else
            {
                FoodName.Text = FoodName.Text.Trim();
                UnitPrice.Text = UnitPrice.Text.Trim();
                isItemCompleted = true;
            }
            return isItemCompleted;
        }

        private void FoodName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(FoodName.Text))
            {
                FoodName.Text = FoodName.Text.Trim();
            }
            else
            {
                FoodNameErrorLabel.IsVisible = false;
            }
        }

        private void UnitPrice_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(FoodName.Text))
            {
                UnitPrice.Text = UnitPrice.Text.Trim();
            }
            else
            {
                UnitPriceErrorLabel.IsVisible = false;
            }
        }

        private void UnitPrice_Unfocused(object sender, FocusEventArgs e)
        {
            UnitPrice.Text = Convert.ToDouble(UnitPrice.Text).ToString("N2");
        }

        private void Quantity_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Quantity.Text))
            {
                Quantity.Text = Quantity.Text.Trim();
            }
            else
            {
                QuantityErrorLabel.IsVisible = false;
            }
        }
    }
}