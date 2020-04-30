using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using static FoodShare.Models.Favourites.GetFavouriteItemsByUserIdResponse;

namespace FoodShare.Models
{
    public class OperationData
    {
        public OperationData()
        {
            CartItemList = new ObservableCollection<ItemResult>();
            
        }
        public static int userId { get; set; }
        public static string userLocation { get; set; }
        public static string filterLocation { get; set; }
        public static ObservableCollection<ItemResult> CartItemList { get; set; }
        public static ObservableCollection<FavouriteItems> FavouriteItemList { get; set; }
        public static byte[] ItemImage { get; set; }
        public static bool IsItemImageUpdated { get; set; }
    }
}
