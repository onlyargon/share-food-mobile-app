using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using FoodShare.Models;
using FoodShare.Views;
using static FoodShare.Models.Constants;
using FoodShare.Services;
using System.Collections.Generic;

namespace FoodShare.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        private ItemsAPI itemsAPI = new ItemsAPI();
        public ObservableCollection<Data> Items { get; set; }
        public Command LoadItemsCommand { get; set; }
        public class FoodType
        {
            public string description { get; set; }
        }
        public  List<FoodType> FoodTypes { get; set; }
        public ItemsViewModel()
        {
            Title = "Home";
            Items = new ObservableCollection<Data>();
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
            //MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            //    {
            //        var newItem = item as Item;
            //        Items.Add(newItem);
            //        //await DataStore.AddItemAsync(newItem);
            //    });
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();

                var res = await GetAllItems();
                foreach (var item in res.Data)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async Task<Item> GetAllItems()
        {
            IsBusy = true;
            Item res = await itemsAPI.GetAllItems();
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
    }
}