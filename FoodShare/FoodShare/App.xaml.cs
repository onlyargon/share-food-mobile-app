using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FoodShare.Services;
using FoodShare.Views;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Platform.Android;

namespace FoodShare
{
    public partial class App : Application
    {

        public static bool IsUserLoggedIn { get; set; }
        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MjM0NzgxQDMxMzgyZTMxMmUzMGZWSTlhdmRaVEpzLzYxNEtkTFNEVWs0d085RWMraW5KRUFkc2RTTGdTSnM9");
            InitializeComponent();
            DependencyService.Register<MockDataStore>();
            
            if (!IsUserLoggedIn)
            {
                MainPage = new NavigationPage(new LoginPage());
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
