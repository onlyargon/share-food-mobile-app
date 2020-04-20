using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using FoodShare.CustomControls;
using FoodShare.Droid.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomStepper), typeof(CustomStepperRenderer))]
namespace FoodShare.Droid.CustomRenderers
{
    public class CustomStepperRenderer : ViewRenderer<Stepper, LinearLayout>
    {
        Android.Widget.Button _downButton;
        Android.Widget.Button _upButton;

        [Obsolete]
        public CustomStepperRenderer()
        {
            AutoPackage = false;
        }

        protected override LinearLayout CreateNativeControl()
        {
            return new LinearLayout(Context)
            {

                Orientation = Orientation.Horizontal
            };
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Stepper> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                _downButton = new Android.Widget.Button(Context) { Text = "-", Gravity = GravityFlags.Center, Tag = this };
                //Set the MinWidth of Button
                _downButton.SetMinWidth(50);

                _downButton.SetOnClickListener(StepperListener.Instance);

                _upButton = new Android.Widget.Button(Context) { Text = "+", Tag = this };
                _upButton.SetOnClickListener(StepperListener.Instance);
                //Set the MinWidth of Button
                _upButton.SetMinWidth(50);

                if (e.NewElement != null)
                {
                    //Set the Width and Height of the button according to the WidthRequest
                    _downButton.LayoutParameters = new LayoutParams((int)e.NewElement.WidthRequest, LayoutParams.MatchParent);
                    _upButton.LayoutParameters = new LayoutParams((int)e.NewElement.WidthRequest, LayoutParams.MatchParent);
                }

                var layout = CreateNativeControl();

                layout.AddView(_downButton);
                layout.AddView(_upButton);

                SetNativeControl(layout);
            }

            UpdateButtonEnabled();
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            switch (e.PropertyName)
            {
                case "Minimum":
                    UpdateButtonEnabled();
                    break;
                case "Maximum":
                    UpdateButtonEnabled();
                    break;
                case "Value":
                    UpdateButtonEnabled();
                    break;
                case "IsEnabled":
                    UpdateButtonEnabled();
                    break;
            }
        }

        void UpdateButtonEnabled()
        {
            Stepper view = Element;
            _upButton.Enabled = view.IsEnabled ? view.Value < view.Maximum : view.IsEnabled;
            _downButton.Enabled = view.IsEnabled ? view.Value > view.Minimum : view.IsEnabled;
        }

        class StepperListener : Java.Lang.Object, IOnClickListener
        {
            public static readonly StepperListener Instance = new StepperListener();

            public void OnClick(global::Android.Views.View v)
            {
                var renderer = v.Tag as CustomStepperRenderer;
                if (renderer == null)
                    return;

                Stepper stepper = renderer.Element;
                if (stepper == null)
                    return;

                if (v == renderer._upButton)
                    ((IElementController)stepper).SetValueFromRenderer(Stepper.ValueProperty, stepper.Value + stepper.Increment);
                else if (v == renderer._downButton)
                    ((IElementController)stepper).SetValueFromRenderer(Stepper.ValueProperty, stepper.Value - stepper.Increment);
            }
        }
    }
}