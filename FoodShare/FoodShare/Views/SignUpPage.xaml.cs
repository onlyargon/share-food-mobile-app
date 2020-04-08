using FoodShare.Models;
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
    public partial class SignUpPage : ContentPage
    {
        AuthViewModel authViewModel = new AuthViewModel();
        public SignUpPage()
        {
            InitializeComponent();
            BindingContext = authViewModel;
        }

        private async void BtnCancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void BtnProceed_Clicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            button.IsEnabled = false;
            try
            {
                var isVaild = ValidateUser();
                if (isVaild)
                {
                    var user = new RegisterRequest
                    {
                        basicInfo = new BasicInfo
                        {
                            networkStatus1 = UserNameEntry.Text,
                            networkStatus2 = PasswordEntry.Text,
                            userType = HouseholdRadioButton.IsChecked == true ? "HOUSEHOLD" : "BUSINESS"
                        }
                    };

                    var res = await authViewModel.Signup(user);

                    if (res != null)
                    {
                        if (res.Code == 0)
                        {
                            await DisplayAlert("Message", "Successfully signed up. Please log in to continue", null, "OK");
                            await Navigation.PopModalAsync();
                        }
                        else
                        {
                            await DisplayAlert("Message", res.Message, null, "OK");
                        }
                    }
                    else
                    {
                        await DisplayAlert("Message", "We could not sign you up", null, "OK");
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
                    else if (!string.IsNullOrEmpty(UserNameEntry.Text) && !string.IsNullOrEmpty(PasswordEntry.Text) && string.IsNullOrEmpty(ConfirmPasswordEntry.Text))
                    {
                        ConfirmPasswordEntryInput.ErrorText = "Please confirm your password";
                        ConfirmPasswordEntryInput.HasError = true;
                    }
                    else if(ContactEntryInput.HasError == true)
                    {

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

        private void AccountType_CheckedChanged(object sender, Syncfusion.XForms.Buttons.CheckedChangedEventArgs e)
        {

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

        private void ConfirmPasswordEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(PasswordEntry.Text))
            {
                ConfirmPasswordEntry.Text = ConfirmPasswordEntry.Text.Trim();
            }
            else if(PasswordEntry.Text != ConfirmPasswordEntry.Text)
            {
                ConfirmPasswordEntryInput.ErrorText = "The passwords you entered do not match";
                ConfirmPasswordEntryInput.HasError = true;
            }
            else
            {
                ConfirmPasswordEntryInput.HasError = false;
            }
        }

        private void ContactEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ContactEntry.Text))
            {
                ContactEntry.Text = ConfirmPasswordEntry.Text.Trim();
            }
            else if (ContactEntry.Text.Length != 10)
            {
                ContactEntryInput.ErrorText = "The contact number should contain 10 digits";
                ContactEntryInput.HasError = true;
            }
            else
            {
                ContactEntryInput.HasError = false;
            }
        }
    }
}