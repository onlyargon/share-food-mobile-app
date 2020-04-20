using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FoodShare.Models
{
    public class OperationData
    {
        public OperationData()
        {
            CartItemList = new ObservableCollection<ItemResult>();
        }
        public static int userId { get; set; }
        public static ObservableCollection<ItemResult> CartItemList { get; set; }
        public static byte[] ItemImage { get; set; }
    }
}
