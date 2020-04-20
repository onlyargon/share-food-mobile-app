using FoodShare.Models;
using FoodShare.Models.DeleteItemById;
using FoodShare.Models.GetItemById;
using FoodShare.Models.GetUserProfileById;
using FoodShare.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace FoodShare.ViewModels
{
    public class UserProfileViewModel : BaseViewModel
    {
        UserProfileAPI userProfileAPI = new UserProfileAPI();
        ItemsAPI itemsAPI = new ItemsAPI();

        public UserProfileViewModel()
        {

        }
        public async Task<GetUserProfileByIdResponse> GetProfileById(GetUserProfileByIdRequest item)
        {
            IsBusy = true;
            try
            {
                var res = await userProfileAPI.GetProfileById(item);

                if (res != null)
                {
                    if (res.Code == 0)
                    {
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

        public async Task<DeleteItemByIdResponse> DeleteItemById(DeleteItemByIdRequest item)
        {
            IsBusy = true;
            try
            {
                var res = await itemsAPI.DeleteItemById(item);

                if (res != null)
                {
                    if (res.Code == 0)
                    {
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
