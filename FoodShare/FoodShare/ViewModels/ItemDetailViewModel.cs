using System;

using FoodShare.Models;

namespace FoodShare.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Item Item { get; set; }
        public ItemDetailViewModel(Data item = null)
        {
            //Title = item?.Text;
            //Item = item;
        }
    }
}
