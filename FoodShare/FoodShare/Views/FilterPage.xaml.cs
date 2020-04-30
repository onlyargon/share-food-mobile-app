using FoodShare.Models;
using FoodShare.ViewModels;
using Rg.Plugins.Popup.Services;
using Syncfusion.SfAutoComplete.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static FoodShare.Models.Cities;

namespace FoodShare.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FilterPage : CustomControls.CustomPopup
    {
        ItemsViewModel itemsViewModel = new ItemsViewModel();
        Cities city = new Cities();
        public ObservableCollection<City> Cities { get; set; }
        public FilterPage()
        {
            InitializeComponent();
            Cities = city.MainCities;
            autoComplete.DataSource = Cities;
            LoadData();
        }

        void LoadData()
        {
            foreach (var city in Cities)
            {
                if(city.description == OperationData.filterLocation)
                {
                    autoComplete.SelectedItem = city;
                }
            }
        }

        private void FilterSelection_Changed(object sender, Syncfusion.SfAutoComplete.XForms.SelectionChangedEventArgs e)
        {
            SfAutoComplete autoComplete = (SfAutoComplete)sender;
            if (autoComplete.SelectedItem != null)
            {
                Apply.IsEnabled = true;
            }
            else
            {
                Apply.IsEnabled = false;
            }
        }

        public event EventHandler<object> CallbackEvent;

        private void InvoceCallback()
        {
            CallbackEvent?.Invoke(null, EventArgs.Empty);
        }

        private async void Apply_Clicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            button.IsEnabled = false;

            try
            {
                OperationData.filterLocation = autoComplete.Text.ToUpper();
                InvoceCallback();
                await PopupNavigation.PopAsync();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                button.IsEnabled = true;
            }
        }

        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            button.IsEnabled = false;

            try
            {
                await PopupNavigation.PopAsync();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                button.IsEnabled = true;
            }
        }
    }
}