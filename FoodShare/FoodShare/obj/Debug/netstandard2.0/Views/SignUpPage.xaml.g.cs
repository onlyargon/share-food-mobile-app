//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

[assembly: global::Xamarin.Forms.Xaml.XamlResourceIdAttribute("FoodShare.Views.SignUpPage.xaml", "Views/SignUpPage.xaml", typeof(global::FoodShare.Views.SignUpPage))]

namespace FoodShare.Views {
    
    
    [global::Xamarin.Forms.Xaml.XamlFilePathAttribute("Views\\SignUpPage.xaml")]
    public partial class SignUpPage : global::Xamarin.Forms.ContentPage {
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Syncfusion.XForms.TextInputLayout.SfTextInputLayout UserNameEntryInput;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.Entry UserNameEntry;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Syncfusion.XForms.TextInputLayout.SfTextInputLayout PasswordEntryInput;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.Entry PasswordEntry;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Syncfusion.XForms.TextInputLayout.SfTextInputLayout ConfirmPasswordEntryInput;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.Entry ConfirmPasswordEntry;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Syncfusion.XForms.TextInputLayout.SfTextInputLayout ContactEntryInput;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.Entry ContactEntry;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Syncfusion.XForms.Buttons.SfRadioGroup AccountType;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Syncfusion.XForms.Buttons.SfRadioButton HouseholdRadioButton;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Syncfusion.XForms.Buttons.SfRadioButton BusinessRadioButton;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.Button BtnCancel;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.Button BtnProceed;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.StackLayout BusyIndicator;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.Label progressname;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private void InitializeComponent() {
            global::Xamarin.Forms.Xaml.Extensions.LoadFromXaml(this, typeof(SignUpPage));
            UserNameEntryInput = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Syncfusion.XForms.TextInputLayout.SfTextInputLayout>(this, "UserNameEntryInput");
            UserNameEntry = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.Entry>(this, "UserNameEntry");
            PasswordEntryInput = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Syncfusion.XForms.TextInputLayout.SfTextInputLayout>(this, "PasswordEntryInput");
            PasswordEntry = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.Entry>(this, "PasswordEntry");
            ConfirmPasswordEntryInput = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Syncfusion.XForms.TextInputLayout.SfTextInputLayout>(this, "ConfirmPasswordEntryInput");
            ConfirmPasswordEntry = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.Entry>(this, "ConfirmPasswordEntry");
            ContactEntryInput = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Syncfusion.XForms.TextInputLayout.SfTextInputLayout>(this, "ContactEntryInput");
            ContactEntry = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.Entry>(this, "ContactEntry");
            AccountType = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Syncfusion.XForms.Buttons.SfRadioGroup>(this, "AccountType");
            HouseholdRadioButton = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Syncfusion.XForms.Buttons.SfRadioButton>(this, "HouseholdRadioButton");
            BusinessRadioButton = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Syncfusion.XForms.Buttons.SfRadioButton>(this, "BusinessRadioButton");
            BtnCancel = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.Button>(this, "BtnCancel");
            BtnProceed = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.Button>(this, "BtnProceed");
            BusyIndicator = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.StackLayout>(this, "BusyIndicator");
            progressname = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.Label>(this, "progressname");
        }
    }
}