using FoodShare.Services;
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
    public partial class FavouritesPage : ContentPage
    {
        public FavouritesPage()
        {
            InitializeComponent();
        }

        //private void Stepper_ValueChanged(object sender, ValueChangedEventArgs e)
        //{
        //    if (ToolbarItems.Count > 0)
        //    {
        //        DependencyService.Get<IToolbarItemBadgeService>().SetBadge(this, ToolbarItems.First(), $"{e.NewValue}", Color.Red, Color.White);
        //    }
        //}

        private void OnLogout_Clicked(object sender, EventArgs e)
        {

        }
    }
}