using FoodShare.Models;
using FoodShare.Models.Favourites;
using FoodShare.Services;
using FoodShare.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static FoodShare.Models.Favourites.GetFavouriteItemsByUserIdResponse;

namespace FoodShare.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FavouritesPage : ContentPage
    {
        FavouritesViewModel favouritesViewModel = new FavouritesViewModel();
        ItemsViewModel itemsViewModel = new ItemsViewModel();
        public FavouritesPage()
        {
            InitializeComponent();
            _ = LoadFavourites();
            BindingContext = favouritesViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ChangeUI(OperationData.FavouriteItemList.Count);
        }

        async Task LoadFavourites()
        {
            var res = await favouritesViewModel.ExecuteLoadFavouritesCommand();
            if (res != null)
            {
                if (res.Code == 0)
                {
                    ChangeUI(res.Data.Count);
                }
                else
                {
                    await DisplayAlert("Message", "Could not retrieve favourite items, Please try again.", null, "OK");
                }
            }
            else
            {
                await DisplayAlert("Message", "Could not retrieve favourite items, Please try again.", null, "OK");
                FavouritesCollectionView.IsVisible = false;
                NoFavouritesView.IsVisible = true;
            }
        }

        private void Stepper_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            if (ToolbarItems.Count > 0)
            {
                DependencyService.Get<IToolbarItemBadgeService>().SetBadge(this, ToolbarItems.First(), $"{e.NewValue}", Color.Red, Color.White);
            }
        }

        void ChangeUI(int count)
        {
            if (count == 0)
            {
                FavouritesCollectionView.IsVisible = false;
                NoFavouritesView.IsVisible = true;
            }
            else
            {
                FavouritesCollectionView.IsVisible = true;
                NoFavouritesView.IsVisible = false;
            }
        }

        private async void BtnAddToFavourites_Tapped(object sender, EventArgs e)
        {
            Image button = (Image)sender;
            button.IsEnabled = false;
            try
            {
                var item = (FavouriteItems)button.BindingContext;

                var answer = await DisplayAlert("Message", "Are you sure you want to remove '" + item.foodName + "' from favourites?", "YES", "NO");
                if (answer)
                {
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
                            if (OperationData.FavouriteItemList.Count > 0)
                            {
                                foreach (var favitem in OperationData.FavouriteItemList.ToList())
                                {
                                    if (item.id == favitem.id)
                                    {
                                        OperationData.FavouriteItemList.Remove(favitem);
                                    }
                                }
                                await DisplayAlert("Message", item.foodName + " removed from favourites", null, "OK");
                                ChangeUI(OperationData.FavouriteItemList.Count);
                                ItemsPage itemsPage = new ItemsPage();
                            } 
                        }
                        else
                        {
                            await DisplayAlert("Message", "Couldn't remove " + item.foodName + " from favourites. Please try again", null, "OK");

                        }
                    }
                    else
                    {
                        await DisplayAlert("Message", "Couldn't remove " + item.foodName + " from favourites. Please try again", null, "OK");
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

        private void Items_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private async void FavItems_Refreshing(object sender, EventArgs e)
        {
            await LoadFavourites();
        }

        private async void Item_Tapped(object sender, EventArgs e)
        {
            Frame button = (Frame)sender;
            button.IsEnabled = false;
            try
            {
                var layout = (Frame)sender;
                var item = (FavouriteItems)layout.BindingContext;
                await App.Current.MainPage.Navigation.PushAsync(new ItemDetailPage(item.id));
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