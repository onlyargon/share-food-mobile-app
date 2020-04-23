using FoodShare.Models;
using FoodShare.Models.Favourites;
using FoodShare.Services;
using FoodShare.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using static FoodShare.Models.Favourites.GetFavouriteItemsByUserIdResponse;

namespace FoodShare.ViewModels
{
    public class FavouritesViewModel : BaseViewModel
    {
        FavouritesAPI favouritesAPI = new FavouritesAPI();
        public ObservableCollection<FavouriteItems> Favourites { get; set; }
        public Command LoadFavouritesCommand { get; set; }

        public FavouritesViewModel()
        {
            OperationData.FavouriteItemList = new ObservableCollection<FavouriteItems>();
            Favourites = OperationData.FavouriteItemList;
            LoadFavouritesCommand = new Command(async () => await ExecuteLoadFavouritesCommand());
        }

        public async Task<GetFavouriteItemsByUserIdResponse> ExecuteLoadFavouritesCommand()
        {
            IsBusy = true;

            try
            {
                OperationData.FavouriteItemList.Clear();

                GetFavouriteItemsByUserIdRequest getFavouriteItemsByUserIdRequest = new GetFavouriteItemsByUserIdRequest()
                {
                    userId = OperationData.userId.ToString()
                };
                var res = await GetFavouriteItems(getFavouriteItemsByUserIdRequest);
                if (res != null)
                {
                    if (res.Code == 0)
                    {
                        foreach (var item in res.Data)
                        {

                            OperationData.FavouriteItemList.Add(item);
                        }
                    }
                    Favourites = OperationData.FavouriteItemList;
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
        public async Task<AddToFavouritesResponse> AddToFavourites(AddToFavouritesRequest order)
        {
            IsBusy = true;
            var res = await favouritesAPI.AddToFavourites(order);
            IsBusy = false;
            return res;
        }

        public async Task<RemoveFromFavouriteResponse> RemoveFromFavourites(RemoveFromFavouriteRequest order)
        {
            IsBusy = true;
            var res = await favouritesAPI.RemoveFromFavourites(order);
            IsBusy = false;
            return res;
        }

        public async Task<GetFavouriteItemsByUserIdResponse> GetFavouriteItems(GetFavouriteItemsByUserIdRequest user)
        {
            IsBusy = true;
            var res = await favouritesAPI.GetFavouriteItemsByUserId(user);
            IsBusy = false;
            return res;
        }
    }
}
