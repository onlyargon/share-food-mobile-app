using FoodShare.Models;
using FoodShare.Models.DeleteItemById;
using FoodShare.Models.GetItemById;
using FoodShare.Models.GetUserProfileById;
using FoodShare.Models.UserProfile.CreateUserProfile;
using FoodShare.Models.UserProfile.UpdateUserProfile;
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
        public class GenderType
        {
            public string description { get; set; }
        }
        public List<GenderType> GenderTypes { get; set; }
        public UserProfileViewModel()
        {
            this.GenderTypes = new List<GenderType>();

            GenderTypes.Add(new GenderType { description = "Male" });
            GenderTypes.Add(new GenderType { description = "Female" });
            GenderTypes.Add(new GenderType { description = "Other" });
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

        public async Task<CreateUserProfileResponse> SaveProfile(CreateUserProfileRequest user)
        {
            IsBusy = true;
            try
            {
                var res = await userProfileAPI.CreateUserProfile(user);

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
        public async Task<CreateUserProfileResponse> UpdateProfile(UpdateUserProfileRequest user)
        {
            IsBusy = true;
            try
            {
                var res = await userProfileAPI.UpdateUserProfile(user);

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
