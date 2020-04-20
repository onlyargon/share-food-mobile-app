using FoodShare.CustomControls;
using FoodShare.Models;
using FoodShare.Models.DeleteItemById;
using FoodShare.Models.GetItemById;
using FoodShare.Models.GetUserProfileById;
using FoodShare.Services;
using FoodShare.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FoodShare.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        UserProfileViewModel userProfileViewModel = new UserProfileViewModel();
        ItemsViewModel viewModel = new ItemsViewModel();

        bool IsAnotherUser { get; set; }
        int UserId { get; set; }
        public List<ItemsWithRating> Items { get; set; }

        public ProfilePage(bool isAnotherUser, int userId)
        {
            InitializeComponent();
            this.IsAnotherUser = isAnotherUser;
            this.UserId = userId;
            Items = new List<ItemsWithRating>();

            BindingContext = userProfileViewModel;
        }
        public ProfilePage()
        {
            InitializeComponent();
            BindingContext = userProfileViewModel;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _ = LoadUser();
        }

        async Task LoadUser()
        {
            if (IsAnotherUser)
            {
                GetUserProfileByIdRequest reqObj = new GetUserProfileByIdRequest()
                {
                    userId = UserId.ToString()
                };
                _ = GetUser(reqObj);
            }
            else
            {
                GetUserProfileByIdRequest reqObj = new GetUserProfileByIdRequest()
                {
                    userId = OperationData.userId.ToString()
                };
                _ = GetUser(reqObj);
            }
        }

        async Task GetUser(GetUserProfileByIdRequest user)
        {
            var res = await userProfileViewModel.GetProfileById(user);
            if (res != null)
            {
                if (res.Code == 0)
                {
                    UserName.Text = res.Data.userInfo.username;
                    JoinedDate.Text = "Joined on :  " + Convert.ToDateTime(res.Data.userInfo.joinedDate).ToString("yyyy-MM-dd");
                    Items = res.Data.itemsWithRating;

                    if (!IsAnotherUser)
                    {
                        foreach (var item in Items)
                        {
                            item.item.isIconsVisible = true;
                        }
                    }
                    else
                    {
                        foreach (var item in Items)
                        {
                            item.item.isIconsVisible = false;
                        }
                    }

                    LoadItems();
                }
                else
                {
                    await DisplayAlert("Message", "Couldn't retrieve user profile details. Please try again.", null, "OK");
                }
            }
            else
            {
                await DisplayAlert("Message", "Couldn't retrieve user profile details. Please try again.", null, "OK");
            }
        }

        void LoadItems()
        {
            foreach (var item in Items)
            {
                if (item.item.isActive == true)
                {
                    item.item.status = "Active";
                    item.item.statusColor = Color.Accent;
                }
                else
                {
                    item.item.status = "Expired";
                    item.item.statusColor = Color.Red;
                }
            }
            ItemsCollectionView.ItemsSource = Items;
        }

        private async void Delete_Tapped(object sender, EventArgs e)
        {
            FontIconLabel button = (FontIconLabel)sender;
            button.IsEnabled = false;
            try
            {
                ItemsWithRating item = (ItemsWithRating)((Label)sender).BindingContext;

                var answer = await DisplayAlert("Message", "Are you sure you want to delete item '" + item.item.foodName + "' ?", "YES", "NO");
                if (answer)
                {
                    DeleteItemByIdRequest reqObj = new DeleteItemByIdRequest()
                    {
                        id = item.item.id.ToString()
                    };
                    var res = await userProfileViewModel.DeleteItemById(reqObj);
                    if (res != null)
                    {
                        if (res.Code == 0)
                        {
                            await DisplayAlert("Message", "Item deleted successfully!", null, "OK");
                            await LoadUser();
                            viewModel.LoadItemsCommand.Execute(null);
                            //viewModel.ExecuteLoadItemsCommand();
                        }
                        else
                        {
                            await DisplayAlert("Message", "Couldn't delete the item. Please try again.", null, "OK");
                        }
                    }
                    else
                    {
                        await DisplayAlert("Message", "Couldn't delete the item. Please try again.", null, "OK");
                    }
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                button.IsEnabled = true;
            }
            
        }

        private async void Edit_Tapped(object sender, EventArgs e)
        {
            FontIconLabel button = (FontIconLabel)sender;
            button.IsEnabled = false;

            try
            {
                ItemsWithRating item = (ItemsWithRating)((Label)sender).BindingContext;
                await OpenAddNewItemPage(item);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                button.IsEnabled = true;
            }
        }

        private async Task OpenAddNewItemPage(ItemsWithRating item)
        {
            NewItemPage newItemPage = new NewItemPage(true, item.item);
            newItemPage.CallbackEvent += async (object sender, object e) => await NewItemPageCallbackMethod();
            await Navigation.PushModalAsync(new NavigationPage(newItemPage));
        }

        async Task NewItemPageCallbackMethod()
        {

        }

        private async void ProfileRefreshView_Refreshing(object sender, EventArgs e)
        {
            await LoadUser();
            ProfileRefreshView.IsRefreshing = false;
        }
    }
}