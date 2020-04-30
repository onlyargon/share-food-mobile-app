using FoodShare.Models;
using FoodShare.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FoodShare.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private AuthViewModel authViewModel = new AuthViewModel();
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = authViewModel;
        }

        private async void BtnLogin_Clicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            button.IsEnabled = false;
            try
            {
                var isVaild = ValidateUser();
                if (isVaild)
                {
                    var user = new User
                    {
                        networkStatus1 = UserNameEntry.Text,
                        networkStatus2 = PasswordEntry.Text
                    };

                    var res = await authViewModel.Login(user);

                    if (res != null)
                    {
                        if (res.data.Code == 0)
                        {
                            App.IsUserLoggedIn = true;

                            //storing token in secure storage
                            try
                            {
                                await SecureStorage.SetAsync("auth_token", res.token);
                            }
                            catch (Exception ex)
                            {
                                // Possible that device doesn't support secure storage on device.
                            }
                            OperationData.userId = res.data.Data.userId;
                            OperationData.userLocation = !string.IsNullOrEmpty(res.data.Data.userLocation) ? res.data.Data.userLocation : "";
                            OperationData.filterLocation = OperationData.userLocation;

                            if (!res.data.Data.isProfileCompleted)
                            {
                                Application.Current.MainPage = new NavigationPage(new CompleteProfilePage(false, null));
                            }
                            else
                            {
                                App.IsProfileCompleted = true;
                                Application.Current.MainPage = new NavigationPage(new MainPage());
                            }

                            //Application.Current.MainPage = new MainPage();
                        }
                        else
                        {
                            await DisplayAlert("Message", "We could not log you in. Please try again", null, "OK");
                        }
                    }
                    else
                    {
                        await DisplayAlert("Message", "We could not log you in. Please try again", null, "OK");
                    }
                }


                bool ValidateUser()
                {
                    bool areCredentialsCorrect = false;

                    if (string.IsNullOrEmpty(UserNameEntry.Text))
                    {
                        UserNameEntryInput.HasError = true;
                    }
                    else if (!string.IsNullOrEmpty(UserNameEntry.Text) && string.IsNullOrEmpty(PasswordEntry.Text))
                    {
                        PasswordEntryInput.HasError = true;
                    }
                    else
                    {
                        UserNameEntry.Text = UserNameEntry.Text.Trim();
                        PasswordEntry.Text = PasswordEntry.Text.Trim();
                        areCredentialsCorrect = true;
                    }
                    return areCredentialsCorrect;
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

        private async void BtnSignUp_Clicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            button.IsEnabled = false;
            try
            {
                await Navigation.PushModalAsync(new SignUpPage());
            }
            catch (Exception ex)
            {


            }
            finally
            {
                button.IsEnabled = true;
            }
        }

        private void UserNameEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(UserNameEntry.Text))
            {
                UserNameEntry.Text = UserNameEntry.Text.Trim();
            }
            else
            {
                UserNameEntryInput.HasError = false;
            }
        }

        private void PasswordEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(PasswordEntry.Text))
            {
                PasswordEntry.Text = PasswordEntry.Text.Trim();
            }
            else
            {
                PasswordEntryInput.HasError = false;
            }
        }
    }
}