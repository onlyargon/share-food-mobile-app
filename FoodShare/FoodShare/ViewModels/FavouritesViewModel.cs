using FoodShare.Models.Favourites;
using FoodShare.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FoodShare.ViewModels
{
    public class FavouritesViewModel : BaseViewModel
    {
        FavouritesAPI favouritesAPI = new FavouritesAPI();

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
    }
}
