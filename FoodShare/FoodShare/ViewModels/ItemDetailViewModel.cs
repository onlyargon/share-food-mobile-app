using System;

using FoodShare.Models;

namespace FoodShare.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Data Item { get; set; }
        public ItemDetailViewModel(Data item = null)
        {
            Title = item?.foodName;
            Item = item;
        }
    }
}
