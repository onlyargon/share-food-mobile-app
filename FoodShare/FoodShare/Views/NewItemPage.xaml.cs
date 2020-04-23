using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using FoodShare.Models;
using Xamarin.Essentials;
using FoodShare.ViewModels;
using static FoodShare.ViewModels.ItemsViewModel;
using FoodShare.Models.GetUserProfileById;
using System.Threading.Tasks;
using FoodShare.Models.UpdateItem;
using Plugin.Media;
using System.IO;

namespace FoodShare.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class NewItemPage : ContentPage
    {
        ItemsViewModel itemsViewModel = new ItemsViewModel();
        public ItemResult Item { get; set; }
        public bool IsFromUpdate;

        public NewItemPage(bool isFromUpdate, ItemResult item)
        {
            InitializeComponent();
            FoodName.Keyboard = Keyboard.Create(KeyboardFlags.CapitalizeWord);
            DatePrepared.MaximumDate = DateTime.Today;
            DateExpiry.MinimumDate = DateTime.Today;
            IsFromUpdate = isFromUpdate;
            BindingContext = itemsViewModel;
            Title = "New Item";
            if (isFromUpdate)
            {
                if (item != null)
                {
                    Item = item;
                    ShowCurrrentDetails(Item);
                }
            }
        }

        public event EventHandler<object> CallbackEvent;

        private void InvoceCallback()
        {
            CallbackEvent?.Invoke(this, EventArgs.Empty);
        }
        async void Save_Clicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            button.IsEnabled = false;
            try
            {
                var IsValid = ValidateItem();

                if (IsValid && !IsFromUpdate)
                {
                    var addItemRequest = await SaveNewItemDetails();

                    var res = await itemsViewModel.AddItem(addItemRequest);

                    if (res != null)
                    {
                        if (res.Code == 0)
                        {
                            InvoceCallback();
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
                else if(IsValid && IsFromUpdate)
                {
                    var updateItemRequest = await SaveEditItemDetails();

                    var res = await itemsViewModel.UpdateItem(updateItemRequest);

                    if (res != null)
                    {
                        if (res.Code == 0)
                        {
                            InvoceCallback();
                            DisplayAlert("Message", "Item updated successfully!", null, "OK");
                            await Navigation.PopModalAsync();
                        }
                        else
                        {
                            await DisplayAlert("Message", "Updating Item Failed. Please try again", null, "OK");
                        }
                    }
                    else
                    {
                        await DisplayAlert("Message", "Updating Item Failed. Please try again", null, "OK");
                    }
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

        async Task<AddItemRequest> SaveNewItemDetails()
        {
            IMGURResponse response = await itemsViewModel.SaveImage(OperationData.ItemImage);

            if (response != null)
            {
                if (response.success)
                {
                    FoodType foodType = (FoodType)FoodTypeSelector.SelectedItem;
                    AddItemRequest addItemRequest = new AddItemRequest
                    {
                        userId = OperationData.userId,
                        foodType = foodType.description,
                        foodName = FoodName.Text,
                        unitPrice = Convert.ToDouble(UnitPrice.Text).ToString("0.00"),
                        quantity = Quantity.Text,
                        description = Description.Text,
                        itemImage = response.data.link != null ? response.data.link : "",
                        preparedOn = DatePrepared.Date.Date.ToString("yyyy-MM-dd"),
                        expiryDate = DateExpiry.Date.Date.ToString("yyyy-MM-dd")
                    };
                    return addItemRequest;
                }
                else
                {
                    await DisplayAlert("Message", "Something went wrong while uploading your image. Please try again", null, "OK");
                }
            }
            else
            {
                await DisplayAlert("Message", "Something went wrong while uploading your image. Please try again", null, "OK");
            }
            return null;
        }

        async Task<UpdateItemRequest> SaveEditItemDetails()
        {
            IMGURResponse response = await itemsViewModel.SaveImage(OperationData.ItemImage);

            if (response != null)
            {
                if (response.success)
                {
                    FoodType foodType = (FoodType)FoodTypeSelector.SelectedItem;
                    UpdateItemRequest updateItemRequest = new UpdateItemRequest
                    {
                        id = Item.id,
                        userId = OperationData.userId,
                        foodType = foodType.description,
                        foodName = FoodName.Text,
                        unitPrice = Convert.ToDouble(UnitPrice.Text).ToString("0.00"),
                        quantity = Quantity.Text,
                        description = Description.Text,
                        preparedOn = DatePrepared.Date.Date.ToString("yyyy-MM-dd"),
                        expiryDate = DateExpiry.Date.Date.ToString("yyyy-MM-dd"),
                        isActive = DateExpiry.Date.Date >= DateTime.Today ? true : false
                    };
                    return updateItemRequest;
                }
                else
                {
                    await DisplayAlert("Message", "Something went wrong while uploading your image. Please try again", null, "OK");
                }
            }
            else
            {
                await DisplayAlert("Message", "Something went wrong while uploading your image. Please try again", null, "OK");
            }
            return null;
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

            if (FoodTypeSelector.SelectedItem == null)
            {
                FoodTypeErrorLabel.IsVisible = true;
            }
            else if (FoodTypeSelector.SelectedItem != null && string.IsNullOrEmpty(FoodName.Text))
            {
                FoodNameErrorLabel.IsVisible = true;
            }
            else if (FoodTypeSelector.SelectedItem != null && !string.IsNullOrEmpty(FoodName.Text) && string.IsNullOrEmpty(UnitPrice.Text))
            {
                UnitPriceErrorLabel.IsVisible = true;
            }
            else if (FoodTypeSelector.SelectedItem != null && !string.IsNullOrEmpty(FoodName.Text) && !string.IsNullOrEmpty(UnitPrice.Text) && string.IsNullOrEmpty(Quantity.Text))
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

        private void FoodTypeSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (FoodTypeSelector.SelectedItem != null)
            {
                FoodTypeErrorLabel.IsVisible = false;
            }
        }

        void ShowCurrrentDetails(ItemResult item)
        {
            foreach (var type in itemsViewModel.FoodTypes)
            {
                if(item.foodType.ToUpper() == type.description.ToUpper())
                {
                    FoodTypeSelector.SelectedItem = type;
                }
            }
            Title = "Edit Item";
            FoodName.Text = item.foodName;
            UnitPrice.Text = Convert.ToDouble(item.unitPrice).ToString("N2");
            Quantity.Text = item.quantity;
            Description.Text = item.description;
            DatePrepared.Date = Convert.ToDateTime(item.preparedOn);
            DateExpiry.Date = Convert.ToDateTime(item.expiryDate);
        }

        private async void FoodImagePicker_Clicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            button.IsEnabled = false;

            try
            {
                if (!CrossMedia.Current.IsPickPhotoSupported)
                {
                    DisplayAlert("Photos Not Supported", ":( Permission not granted to photos.", "OK");
                    return;
                }
                var file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                {
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,

                });


                if (file == null)
                    return;

                byte[] buffer = new byte[16 * 1024];
                using (MemoryStream ms = new MemoryStream())
                {
                    Stream stream = file.GetStream();
                    file.Dispose();
                    stream.CopyTo(ms);
                    buffer = ms.ToArray();
                }

                OperationData.ItemImage = null;
                OperationData.ItemImage = buffer;
                
                FoodImagePlaceholder.Source = ImageSource.FromStream(() =>
                {
                    return new MemoryStream(OperationData.ItemImage);
                });
            }
            catch (Exception ex)
            {

            }
            finally
            {
                button.IsEnabled = true;
            }
        }

        public static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        private async void FoodImageTaker_Clicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            button.IsEnabled = false;

            try
            {
                await CrossMedia.Current.Initialize();

                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    DisplayAlert("No Camera", ":( No camera available.", "OK");
                    return;
                }

                var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    Directory = "Sample",
                    Name = "test.jpg"
                });

                if (file == null)
                    return;

                //await DisplayAlert("File Location", file.Path, "OK");


                byte[] buffer = new byte[16 * 1024];
                using (MemoryStream ms = new MemoryStream())
                {
                    Stream stream = file.GetStream();
                    file.Dispose();
                    stream.CopyTo(ms);
                    buffer = ms.ToArray();
                }

                OperationData.ItemImage = null;
                OperationData.ItemImage = buffer;
                FoodImagePlaceholder.Source = ImageSource.FromStream(() =>
                {
                    return new MemoryStream(OperationData.ItemImage);
                });
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