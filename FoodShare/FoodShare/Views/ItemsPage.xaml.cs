using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using FoodShare.Models;
using FoodShare.Views;
using FoodShare.ViewModels;
using FoodShare.Services;
using Xamarin.Essentials;
using FoodShare.Models.Favourites;

namespace FoodShare.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class ItemsPage : ContentPage
    {
        ItemsViewModel viewModel = new ItemsViewModel();
        FavouritesViewModel favouritesViewModel = new FavouritesViewModel();

        public ItemsPage()
        {
            InitializeComponent();
            Items_Refreshed(ItemsRefreshView, null);
            BindingContext = viewModel;
        }

        async void OnItemSelected(object sender, EventArgs args)
        {
            Frame button = (Frame)sender;
            button.IsEnabled = false;
            try
            {
                var layout = (Frame)sender;
                var item = (Data)layout.BindingContext;
                await App.Current.MainPage.Navigation.PushAsync(new ItemDetailPage(item.id));


                //await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));
                //await ((MainPage)App.Current.MainPage).Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));
                //Application.Current.MainPage = new ItemDetailPage(new ItemDetailViewModel(item));
            }
            catch (Exception ex)
            {

            }
            finally
            {
                button.IsEnabled = true;
            }
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            Frame button = (Frame)sender;
            button.IsEnabled = false;

            await OpenAddNewItemPage();

            button.IsEnabled = true;
        }

        private async Task OpenAddNewItemPage()
        {
            NewItemPage newItemPage = new NewItemPage(false, null);
            newItemPage.CallbackEvent += async (object sender, object e) => await NewItemPageCallbackMethod();
            await Navigation.PushModalAsync(new NavigationPage(newItemPage));
        }

        private async Task NewItemPageCallbackMethod()
        {
            //await viewModel.ExecuteLoadItemsCommand();
            viewModel.LoadItemsCommand.Execute(null);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Items.Count == 0)
                viewModel.IsBusy = true;
        }

        private async void OnLogout_Clicked(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Message", "Are you sure you want to log out?", "YES", "NO");
            if (answer)
            {
                App.IsUserLoggedIn = false;

                //removing stored token from secure storage
                SecureStorage.Remove("auth_token");

                await Navigation.PopAsync();

                Application.Current.MainPage = new LoginPage();
            }
            
        }


        private void AnimateIn()
        {
            FloatingActionButton.TranslateTo(0, 100, 250, Easing.BounceOut);
        }
        private void AnimateOut()
        {
            FloatingActionButton.TranslateTo(0, 0, 250, Easing.BounceIn);
        }

        private void Stepper_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            if (ToolbarItems.Count > 0)
            {
                DependencyService.Get<IToolbarItemBadgeService>().SetBadge(this, ToolbarItems.First(), $"{e.NewValue}", Color.Red, Color.White);
            }
        }

        private async void OnCart_Clicked(object sender, EventArgs e)
        {
            //await Navigation.PushModalAsync(new NavigationPage(new CartPage()));
            await App.Current.MainPage.Navigation.PushAsync(new CartPage());
        }

        private async void BtnAddToFavourites_Tapped(object sender, EventArgs e)
        {
            Image button = (Image)sender;
            button.IsEnabled = false;
            try
            {
                var item = (Data)button.BindingContext;
                var fontImageSource = (FontImageSource)button.Source;

                if(fontImageSource.Glyph == "󰋕" && fontImageSource.Color == Color.FromHex("#C3C3C3"))
                {
                    await AnimateHeart(button);

                    fontImageSource.Glyph = "󰋑";
                    fontImageSource.Color = Color.FromHex("#F25278");

                    //add to favourites here
                    AddToFavouritesRequest addToFavouritesRequest = new AddToFavouritesRequest()
                    {
                        userId = OperationData.userId.ToString(),
                        itemId = item.id.ToString()
                    };
                    var res = await favouritesViewModel.AddToFavourites(addToFavouritesRequest);
                    if (res != null)
                    {
                        if (res.Code == 0)
                        {
                            await DisplayAlert("Message", item.foodName + " added to favourites!", null, "OK");
                        }
                        else
                        {
                            await DisplayAlert("Message", "Couldn't add " + item.foodName + " to favourites. Please try again", null, "OK");

                            await AnimateHeart(button);
                            fontImageSource.Glyph = "󰋕";
                            fontImageSource.Color = Color.FromHex("#C3C3C3");
                        }
                    }
                    else
                    {
                        await DisplayAlert("Message", "Couldn't add " + item.foodName + " to favourites. Please try again", null, "OK");
                        await AnimateHeart(button);
                        fontImageSource.Glyph = "󰋕";
                        fontImageSource.Color = Color.FromHex("#C3C3C3");
                    }
                }
                else
                {
                    await AnimateHeart(button);

                    fontImageSource.Glyph = "󰋕";
                    fontImageSource.Color = Color.FromHex("#C3C3C3");

                    //remove from favourites here
                    RemoveFromFavouriteRequest removeFromFavouriteRequest = new RemoveFromFavouriteRequest()
                    {
                        userId = OperationData.userId.ToString(),
                        itemId = item.id.ToString()
                    };
                    var res = await favouritesViewModel.RemoveFromFavourites(removeFromFavouriteRequest);
                    if (res != null)
                    {
                        if (res.Code == 0)
                        {
                            await DisplayAlert("Message", item.foodName + " removed from favourites", null, "OK");
                        }
                        else
                        {
                            await DisplayAlert("Message", "Couldn't remove " + item.foodName + " from favourites. Please try again", null, "OK");

                            await AnimateHeart(button);
                            fontImageSource.Glyph = "󰋑";
                            fontImageSource.Color = Color.FromHex("#F25278");
                        }
                    }
                    else
                    {
                        await DisplayAlert("Message", "Couldn't remove " + item.foodName + " from favourites. Please try again", null, "OK");
                        await AnimateHeart(button);
                        fontImageSource.Glyph = "󰋑";
                        fontImageSource.Color = Color.FromHex("#F25278");
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

        async Task AnimateHeart(Image button)
        {
            await button.ScaleTo(0.8, 70);
            await button.ScaleTo(1.2, 70);
            await button.ScaleTo(1, 70);
        }

        private async void Items_Refreshed(object sender, EventArgs e)
        {
            RefreshView refreshView = (RefreshView)sender;
            viewModel.LoadItemsCommand.Execute(null);
        }
    }
}