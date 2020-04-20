using System;
using System.Collections.Generic;
using System.Text;

namespace FoodShare.Models.Favourites
{
    public class RemoveFromFavouriteRequest
    {
        public string userId { get; set; }
        public string itemId { get; set; }
    }
}
