using System;
using System.Threading.Tasks;
using FoodShare.Models;
using FoodShare.Models.GetItemById;
using FoodShare.Services;

namespace FoodShare.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        ItemsAPI itemsAPI = new ItemsAPI();
        public ItemResult Item { get; set; }
        public double unitPrice { get; set; } = 0;
        public string unitPriceText { get; set; }
        public ItemDetailViewModel()
        {
            
        }

        public string CalculateItemPrice(double increment)
        {
            unitPriceText = (unitPrice * increment).ToString("N2");
            return unitPriceText;
        }

        public async Task<GetItemByIdResponse> GetItemById(GetItemByIdRequest item)
        {
            IsBusy = true;
            try
            {
                var res = await itemsAPI.GetItemById(item);

                if (res != null)
                {
                    if (res.Code == 0)
                    {
                        unitPrice = Convert.ToDouble(res.Data.unitPrice);
                        return res;
                    }
                    else
                    {
                        return null;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
