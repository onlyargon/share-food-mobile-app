using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FoodShare.Services;
using FoodShare.Views;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Platform.Android;
using FoodShare.Models;
using System.Collections.ObjectModel;
using static FoodShare.Models.Favourites.GetFavouriteItemsByUserIdResponse;

namespace FoodShare
{
    public partial class App : Application
    {

        public static bool IsUserLoggedIn { get; set; }
        public static bool IsProfileCompleted { get; set; } = false;
        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("YOUR_LICENSE_KEY");
            InitializeComponent();
            OperationData.CartItemList = new ObservableCollection<ItemResult>();
            OperationData.FavouriteItemList = new ObservableCollection<FavouriteItems>();

            if (!IsUserLoggedIn)
            {
                MainPage = new NavigationPage(new LoginPage());
            }
            else if (IsUserLoggedIn && !IsProfileCompleted)
            {
                MainPage = new NavigationPage(new CompleteProfilePage(false,null));
            }
            else
            {
                MainPage = new NavigationPage(new MainPage());
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
