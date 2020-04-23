using FoodShare.Models;
using FoodShare.Models.GetOrdersByUserId;
using FoodShare.Models.OrderRating;
using FoodShare.ViewModels;
using Rg.Plugins.Popup.Services;
using Syncfusion.SfRating.XForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static FoodShare.Models.OrderRating.OrderRatingResponse;

namespace FoodShare.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderItemStarRatingPopup : CustomControls.CustomPopup
    {
        OrderRatingViewModel orderRatingViewModel = new OrderRatingViewModel();
        PastOrdersViewModel pastOrdersViewModel = new PastOrdersViewModel();
        OrderData order = new OrderData();
        public OrderItemStarRatingPopup(OrderData orderData)
        {
            InitializeComponent();
            this.order = orderData;
            FoodName.Text = order.foodName;
            BindingContext = orderRatingViewModel;
        }

        private async void MaybeLater_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.PopAsync();
        }

        private async void Submit_Clicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            button.IsEnabled = false;
            try
            {
                OrderRatingRequest rating = new OrderRatingRequest()
                {
                    userId = OperationData.userId,
                    itemId = order.itemId,
                    sellerId = order.SellerId,
                    orderId = order.id,
                    comment = !string.IsNullOrEmpty(OrderCommentEditor.Text) == true ? OrderCommentEditor.Text : "",
                    starRating = RatingView.Value != null ? Convert.ToInt32(RatingView.Value) : 0
                };
                var res = await orderRatingViewModel.RateOrder(rating);
                if (res != null)
                {
                    if (res.Code == 0)
                    {
                        await PopupNavigation.PopAsync();
                        //pastOrdersViewModel.LoadOrdersCommand.Execute(null);
                        MessagingCenter.Send(res.Data, "ORDER_RATED");
                    }
                    else
                    {
                        await DisplayAlert("Something went wrong", "Please try again.", null, "OK");
                    }
                }
                else
                {
                    await DisplayAlert("Something went wrong", "Please try again.", null, "OK");
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

        private void OrderCommentEditor_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(OrderCommentEditor.Text))
            {
                SubmitButton.IsEnabled = true;
            }
        }

        private void RatingView_ValueChanged(object sender, ValueEventArgs e)
        {
            SfRating sfRating = (SfRating)sender;
            if (sfRating.Value > 0)
            {
                SubmitButton.IsEnabled = true;
            }
        }
    }
}