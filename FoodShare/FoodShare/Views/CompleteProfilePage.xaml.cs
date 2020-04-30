using FoodShare.Models;
using FoodShare.Models.GetUserProfileById;
using FoodShare.Models.UserProfile.CreateUserProfile;
using FoodShare.Models.UserProfile.UpdateUserProfile;
using FoodShare.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static FoodShare.Models.Cities;
using static FoodShare.Models.OperationData;
using static FoodShare.ViewModels.UserProfileViewModel;

namespace FoodShare.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CompleteProfilePage : ContentPage
    {
        UserProfileViewModel userProfileViewModel = new UserProfileViewModel();
        public bool IsFromUpdate;
        City selectedCity = new City();
        Cities city = new Cities();
        public CompleteProfilePage(bool isFromUpdate, ResultData userInfo)
        {
            InitializeComponent();
            CitySelector.ItemsSource = city.MainCities;
            this.IsFromUpdate = isFromUpdate;
            DoB.MaximumDate = DateTime.Today;
            IsFromUpdate = isFromUpdate;
            LoadData(userInfo);
            BindingContext = userProfileViewModel;
        }

        void LoadData(ResultData userInfo)
        {
            if (IsFromUpdate && userInfo != null)
            {
                Title = "Edit Profile";
                FirstName.Text = userInfo.basicInfo.firstName;
                LastName.Text = !string.IsNullOrEmpty(userInfo.basicInfo.lastName) ? userInfo.basicInfo.lastName : "";
                DisplayName.Text = !string.IsNullOrEmpty(userInfo.basicInfo.displayName) ? userInfo.basicInfo.displayName : "";

                if (!string.IsNullOrEmpty(userInfo.basicInfo.gender))
                {
                    if (userInfo.basicInfo.gender == "Male")
                    {
                        GenderSelector.SelectedIndex = 0;
                    }
                    else if (userInfo.basicInfo.gender == "Female")
                    {
                        GenderSelector.SelectedIndex = 1;
                    }
                    else if (userInfo.basicInfo.gender == "Other")
                    {
                        GenderSelector.SelectedIndex = 2;
                    }
                    else
                    {
                        GenderSelector.SelectedIndex = -1;
                    }
                }
                else
                {
                    GenderSelector.SelectedIndex = -1;
                }
                
                DoB.Date = !string.IsNullOrEmpty(userInfo.basicInfo.dob) ? Convert.ToDateTime(userInfo.basicInfo.dob) : DateTime.Today;
                ContactNumber.Text = !string.IsNullOrEmpty(userInfo.basicInfo.mobileNumber) ? userInfo.basicInfo.mobileNumber : "";
                Email.Text = !string.IsNullOrEmpty(userInfo.basicInfo.email) ? userInfo.basicInfo.email : "";
                AddressLine1.Text = !string.IsNullOrEmpty(userInfo.address.addressLine1) ? userInfo.address.addressLine1 : "";
                AddressLine2.Text = !string.IsNullOrEmpty(userInfo.address.addressLine2) ? userInfo.address.addressLine2 : "";
                AddressLine3.Text = !string.IsNullOrEmpty(userInfo.address.addressLine3) ? userInfo.address.addressLine3 : "";
                PostalCode.Text = !string.IsNullOrEmpty(userInfo.address.zipCode) ? userInfo.address.zipCode : "";
                State.Text = !string.IsNullOrEmpty(userInfo.address.state) ? userInfo.address.state : "";
                //City.Text = !string.IsNullOrEmpty(userInfo.address.city) ? userInfo.address.city : "";
                foreach (var city in city.MainCities)
                {
                    if(userInfo.address.city.ToUpper() == city.description.ToUpper())
                    {
                        CitySelector.SelectedItem = city;
                    }
                }
            }
            else
            {
                Title = "Complete Profile";
            }
        }
        private void DisplayName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(DisplayName.Text))
            {
                DisplayName.Text = DisplayName.Text.Trim();
            }
            else
            {
                DisplayNameErrorLabel.IsVisible = false;
            }
        }

        private void GenderSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GenderSelector.SelectedItem != null)
            {
                GenderTypeErrorLabel.IsVisible = false;
            }
        }

        private void DoB_DateSelected(object sender, DateChangedEventArgs e)
        {
            if (DoB.Date < DateTime.Today)
            {
                DoBErrorLabel.IsVisible = false;
            }
            else
            {
                DoBErrorLabel.IsVisible = true;
            }
        }

        private void FirstName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(FirstName.Text))
            {
                FirstName.Text = FirstName.Text.Trim();
            }
            else
            {
                FirstNameErrorLabel.IsVisible = false;
            }
        }

        private void LastName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(LastName.Text))
            {
                LastName.Text = LastName.Text.Trim();
            }
            else
            {
                LastNameErrorLabel.IsVisible = false;
            }
        }

        private void ContactNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ContactNumber.Text))
            {
                ContactNumber.Text = ContactNumber.Text.Trim();
            }
            else if (ContactNumber.Text.Length != 10)
            {
                ContactNumberErrorLabel.Text = "The contact number should contain 10 digits";
                ContactNumberErrorLabel.IsVisible = true;
            }
            else
            {
                ContactNumberErrorLabel.IsVisible = false;
            }
        }

        private void Email_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Email.Text))
            {
                Email.Text = Email.Text.Trim();
            }
            else
            {
                EmailErrorLabel.IsVisible = false;
            }
        }

        private void AddressLine1_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(AddressLine1.Text))
            {
                AddressLine1.Text = AddressLine1.Text.Trim();
            }
            else
            {
                AddressLine1ErrorLabel.IsVisible = false;
            }
        }

        private void AddressLine2_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(AddressLine2.Text))
            {
                AddressLine2.Text = AddressLine2.Text.Trim();
            }
            else
            {
                AddressLine2ErrorLabel.IsVisible = false;
            }
        }

        private void AddressLine3_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(AddressLine3.Text))
            {
                AddressLine3.Text = AddressLine3.Text.Trim();
            }
            else
            {
                AddressLine3ErrorLabel.IsVisible = false;
            }
        }

        private void PostalCode_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(PostalCode.Text))
            {
                PostalCode.Text = PostalCode.Text.Trim();
            }
            else
            {
                PostalCodeErrorLabel.IsVisible = false;
            }
        }

        private void State_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(State.Text))
            {
                State.Text = State.Text.Trim();
            }
            else
            {
                StateErrorLabel.IsVisible = false;
            }
        }

        //private void City_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    if (string.IsNullOrWhiteSpace(City.Text))
        //    {
        //        City.Text = City.Text.Trim();
        //    }
        //    else
        //    {
        //        CityErrorLabel.IsVisible = false;
        //    }
        //}

        private void BtnClear_Clicked(object sender, EventArgs e)
        {
            FirstName.Text = "";
            LastName.Text = "";
            DisplayName.Text = "";
            GenderSelector.SelectedItem = null;
            DoB.Date = DateTime.Today;
            ContactNumber.Text = "";
            Email.Text = "";
            AddressLine1.Text = "";
            AddressLine2.Text = "";
            AddressLine3.Text = "";
            PostalCode.Text = "";
            State.Text = "";
            //City.Text = "";
            CitySelector.SelectedItem = null;
        }

        public event EventHandler<object> CallbackEvent;

        private void InvoceCallback()
        {
            CallbackEvent?.Invoke(this, EventArgs.Empty);
        }

        private async void Save_Clicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            button.IsEnabled = false;
            try
            {
                var IsValid = validateUserProfile();

                if (IsValid && !IsFromUpdate)
                {
                    var addItemRequest = await SaveNewProfileDetails();

                    var res = await userProfileViewModel.SaveProfile(addItemRequest);

                    if (res != null)
                    {
                        if (res.Code == 0)
                        {
                            InvoceCallback();
                            DisplayAlert("Message", "Your profile completed!", null, "OK");
                            Application.Current.MainPage = new NavigationPage(new MainPage());
                        }
                        else
                        {
                            await DisplayAlert("Message", "Completing profile Failed. Please try again", null, "OK");
                        }
                    }
                    else
                    {
                        await DisplayAlert("Message", "Completing profile Failed. Please try again", null, "OK");
                    }
                }
                else if (IsValid && IsFromUpdate)
                {
                    var updateItemRequest = await SaveEditProfileDetails();

                    var res = await userProfileViewModel.UpdateProfile(updateItemRequest);

                    if (res != null)
                    {
                        if (res.Code == 0)
                        {
                            InvoceCallback();
                            DisplayAlert("Message", "Profile updated successfully!", null, "OK");
                            await Navigation.PopModalAsync();
                        }
                        else
                        {
                            await DisplayAlert("Message", "Updating profile Failed. Please try again", null, "OK");
                        }
                    }
                    else
                    {
                        await DisplayAlert("Message", "Updating profile Failed. Please try again", null, "OK");
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

        bool validateUserProfile()
        {
            bool isItemCompleted = false;

            if (string.IsNullOrEmpty(FirstName.Text))
            {
                FirstNameErrorLabel.IsVisible = true;
            }
            if (string.IsNullOrEmpty(DisplayName.Text))
            {
                DisplayNameErrorLabel.IsVisible = true;
            }
            if (GenderSelector.SelectedItem == null)
            {
                GenderTypeErrorLabel.IsVisible = true;
            }
            if (DoB.Date == DateTime.Today)
            {
                DoBErrorLabel.IsVisible = true;
            }
            if (string.IsNullOrEmpty(ContactNumber.Text))
            {
                ContactNumberErrorLabel.IsVisible = true;
            }
            if (string.IsNullOrEmpty(AddressLine1.Text))
            {
                AddressLine1ErrorLabel.IsVisible = true;
            }
            if (string.IsNullOrEmpty(AddressLine2.Text))
            {
                AddressLine2ErrorLabel.IsVisible = true;
            }
            //if (string.IsNullOrEmpty(City.Text))
            //{
            //    CityErrorLabel.IsVisible = true;
            //}
            if (CitySelector.SelectedItem==null)
            {
                CityErrorLabel.IsVisible = true;
            }
            if (string.IsNullOrEmpty(ContactNumber.Text))
            {
                ContactNumberErrorLabel.IsVisible = true;
            }
            else if (ContactNumber.Text.Length != 10)
            {
                ContactNumberErrorLabel.Text = "The contact number should contain 10 digits";
                ContactNumberErrorLabel.IsVisible = true;
            }
            if (!string.IsNullOrEmpty(FirstName.Text) &&
                !string.IsNullOrEmpty(DisplayName.Text) &&
                GenderSelector.SelectedItem != null &&
                DoB.Date != DateTime.Today &&
                !string.IsNullOrEmpty(ContactNumber.Text) &&
                !string.IsNullOrEmpty(AddressLine1.Text) &&
                !string.IsNullOrEmpty(AddressLine2.Text) &&
                //!string.IsNullOrEmpty(City.Text) &&
                CitySelector.SelectedItem != null &&
                !(string.IsNullOrEmpty(ContactNumber.Text) &&
                (ContactNumber.Text.Length == 10)))
            {
                FirstName.Text = FirstName.Text.Trim();
                DisplayName.Text = DisplayName.Text.Trim();
                ContactNumber.Text = ContactNumber.Text.Trim();
                AddressLine1.Text = AddressLine1.Text.Trim();
                AddressLine2.Text = AddressLine2.Text.Trim();
                //City.Text = City.Text.Trim();
                isItemCompleted = true;
            }
            return isItemCompleted;
        }

        async Task<CreateUserProfileRequest> SaveNewProfileDetails()
        {
            GenderType foodType = (GenderType)GenderSelector.SelectedItem;
            CreateUserProfileRequest newProfileRequest = new CreateUserProfileRequest
            {
                basicInfo = new Models.UserProfile.CreateUserProfile.BasicInfo
                {
                    userId = OperationData.userId,
                    firstName = FirstName.Text,
                    lastName = !string.IsNullOrEmpty(LastName.Text) ? LastName.Text : "",
                    displayName = DisplayName.Text,
                    gender = foodType.description,
                    dob = DoB.Date.ToString("yyyy-MM-dd"),
                    mobileNumber = ContactNumber.Text,
                    email = !string.IsNullOrEmpty(Email.Text) ? Email.Text : "",
                    userLocation = selectedCity.description.ToUpper()
                },
                address = new Models.UserProfile.CreateUserProfile.Address
                {
                    userId = OperationData.userId,
                    addressLine1 = AddressLine1.Text,
                    addressLine2 = AddressLine2.Text,
                    addressLine3 = !string.IsNullOrEmpty(AddressLine3.Text) ? AddressLine3.Text : "",
                    zipCode = !string.IsNullOrEmpty(PostalCode.Text) ? PostalCode.Text : "",
                    state = !string.IsNullOrEmpty(State.Text) ? State.Text : "",
                    //city = City.Text
                    city = selectedCity.description
                }
                
            };
            return newProfileRequest;
        }

        async Task<UpdateUserProfileRequest> SaveEditProfileDetails()
        {
            GenderType foodType = (GenderType)GenderSelector.SelectedItem;
            UpdateUserProfileRequest newProfileRequest = new UpdateUserProfileRequest
            {
                basicInfo = new Models.UserProfile.UpdateUserProfile.BasicInfo
                {
                    userId = OperationData.userId,
                    firstName = FirstName.Text,
                    lastName = !string.IsNullOrEmpty(LastName.Text) ? LastName.Text : "",
                    displayName = DisplayName.Text,
                    gender = foodType.description,
                    dob = DoB.Date.ToString("yyyy-MM-dd"),
                    mobileNumber = ContactNumber.Text,
                    email = !string.IsNullOrEmpty(Email.Text) ? Email.Text : "",
                    userLocation = selectedCity.description.ToUpper()
                },
                address = new Models.UserProfile.UpdateUserProfile.Address
                {
                    userId = OperationData.userId,
                    addressLine1 = AddressLine1.Text,
                    addressLine2 = AddressLine2.Text,
                    addressLine3 = !string.IsNullOrEmpty(AddressLine3.Text) ? AddressLine3.Text : "",
                    zipCode = !string.IsNullOrEmpty(PostalCode.Text) ? PostalCode.Text : "",
                    state = !string.IsNullOrEmpty(State.Text) ? State.Text : "",
                    //city = City.Text
                    city = selectedCity.description
                }

            };
            return newProfileRequest;
        }

        private void CitySelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CitySelector.SelectedItem != null)
            {
                CityErrorLabel.IsVisible = false;
                selectedCity = (City)CitySelector.SelectedItem;
            }
        }
    }
}