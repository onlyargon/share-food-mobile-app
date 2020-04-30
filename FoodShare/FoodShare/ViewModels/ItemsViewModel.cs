using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using FoodShare.Models;
using FoodShare.Models.GetAllItems;
using FoodShare.Views;
using static FoodShare.Models.Constants;
using FoodShare.Services;
using System.Collections.Generic;
using System.Windows.Input;
using FoodShare.Models.UpdateItem;
using System.IO;
using static FoodShare.Models.Favourites.GetFavouriteItemsByUserIdResponse;
using static FoodShare.Models.OperationData;
using static FoodShare.Models.Cities;

namespace FoodShare.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        private ItemsAPI itemsAPI = new ItemsAPI();
        public ObservableCollection<Data> Items { get; set; }
        public Command LoadItemsCommand { get; set; }
        Cities city = new Cities();
        public class FoodType
        {
            public string description { get; set; }
        }
        public  List<FoodType> FoodTypes { get; set; }
        public  ObservableCollection<City> Cities { get; set; }

        public ItemsViewModel()
        {
            Title = "Home";
            Items = new ObservableCollection<Data>();
            Cities = city.MainCities;
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            this.FoodTypes = new List<FoodType>();

            FoodTypes.Add(new FoodType { description = "Vegetables" });
            FoodTypes.Add(new FoodType { description = "Fruits" });
            FoodTypes.Add(new FoodType { description = "Grains" });
            FoodTypes.Add(new FoodType { description = "Meat and Poultry" });
            FoodTypes.Add(new FoodType { description = "Fish and Seafood" });
            FoodTypes.Add(new FoodType { description = "Dairy Foods" });
            FoodTypes.Add(new FoodType { description = "Beverages" });
            FoodTypes.Add(new FoodType { description = "Bakery" });
            FoodTypes.Add(new FoodType { description = "Rice" });
            FoodTypes.Add(new FoodType { description = "Sweets" });
        }

        public async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                GetAllItemsRequest request = new GetAllItemsRequest()
                {
                    userId = OperationData.userId,
                    userLocation = OperationData.filterLocation
                };
                var res = await GetAllItems(request);
                if (res != null)
                {
                    if (res.Code == 0)
                    {
                        foreach (var item in res.Data)
                        {
                            if (item.isFavorite == true)
                            {
                                item.isNotFavorite = false;
                            }
                            else 
                            {
                                item.isNotFavorite = true;
                            }
                            item.unitPrice = Convert.ToDouble(item.unitPrice).ToString("N2");
                            Items.Add(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsRefreshing = false;
                IsBusy = false;
            }
        }

        public async Task<Item> GetAllItems(GetAllItemsRequest request)
        {
            IsBusy = true;
            Item res = await itemsAPI.GetAllItems(request);
            IsBusy = false;
            return res;

        }

        public async Task<AddItemResponse> AddItem(AddItemRequest item)
        {
            IsBusy = true;
            var res = await itemsAPI.AddItem(item);
            IsBusy = false;
            return res;
        }

        public async Task<UpdateItemResponse> UpdateItem(UpdateItemRequest item)
        {
            IsBusy = true;
            var res = await itemsAPI.UpdateItem(item);
            IsBusy = false;
            return res;
        }

        public async Task<IMGURResponse> SaveImage(byte[] foodImage)
        {
            IsBusy = true;
            var res = await itemsAPI.SaveImageTest(foodImage);
            IsBusy = false;
            return res;
        }
    }
}