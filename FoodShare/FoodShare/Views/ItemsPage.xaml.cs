using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using FoodShare.Models;
using FoodShare.Views;
using FoodShare.ViewModels;
using FoodShare.Services;

namespace FoodShare.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class ItemsPage : ContentPage
    {
        ItemsViewModel viewModel;

        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new ItemsViewModel();
        }

        async void OnItemSelected(object sender, EventArgs args)
        {
            var layout = (BindableObject)sender;
            var item = (Item)layout.BindingContext;
            await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Items.Count == 0)
                viewModel.IsBusy = true;
        }

        private async void OnLogout_Clicked(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Message", "Are you sure you want to log out?", "YES", "NO");
            if (answer)
            {
                App.IsUserLoggedIn = false;
                //Navigation.InsertPageBefore(new LoginPage(), this);
                await Navigation.PopAsync();

                Application.Current.MainPage = new LoginPage();
            }
            
        }


        private void AnimateIn()
        {
            FloatingActionButton.TranslateTo(0, 100, 250, Easing.BounceOut);
        }
        private void AnimateOut()
        {
            FloatingActionButton.TranslateTo(0, 0, 250, Easing.BounceIn);
        }

        private void Stepper_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            if (ToolbarItems.Count > 0)
            {
                DependencyService.Get<IToolbarItemBadgeService>().SetBadge(this, ToolbarItems.First(), $"{e.NewValue}", Color.Red, Color.White);
            }
        }

        private async void OnCart_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new CartPage()));
        }
    }
}