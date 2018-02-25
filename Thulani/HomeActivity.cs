using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;



namespace Thulani
{
    [Activity(Label = "City of Ekurhuleni")]
    public class HomeActivity : Activity
    {
        private Button siyakhokhaButton;
        private Button settingButton;
        private Button CRMButton;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Home);
          
            // Create your application here
            

            FindViews();
            HandleEvents();
        }
        private void FindViews()
        {
            siyakhokhaButton = FindViewById<Button>(Resource.Id.buttonSiyakhokha);
            settingButton = FindViewById<Button>(Resource.Id.buttonSettings);
            CRMButton = FindViewById<Button>(Resource.Id.buttonCRM);
        }
        private void HandleEvents()
        {
            siyakhokhaButton.Click += SiyakhokhaButton_Click;
            settingButton.Click += SettingButton_Click;
            CRMButton.Click += CRMButton_Click;
        }

        private void CRMButton_Click(object sender, EventArgs e)
        {

            Intent intent = Android.App.Application.Context.PackageManager.GetLaunchIntentForPackage("smartcitizen.ekurhuleni");

            // If not NULL run the app, if not, take the user to the app store
            if (intent != null)
            {
                intent.AddFlags(ActivityFlags.NewTask);

                Android.App.Application.Context.StartActivity(intent);
            }
            else
            {
                intent = new Intent(Intent.ActionView);
                intent.AddFlags(ActivityFlags.NewTask); 
                intent.SetData(Android.Net.Uri.Parse("https://drive.google.com/file/d/0B5unsMnPuO8kMzVyQkN1YS1Ld2c/view"));
                Android.App.Application.Context.StartActivity(intent);
            }
        }

        private void SettingButton_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(SettingsActivity));
            StartActivity(intent);
        }

        private void SiyakhokhaButton_Click(object sender, EventArgs e)
        {
           
            
                Intent intent = Android.App.Application.Context.PackageManager.GetLaunchIntentForPackage("za.co.esiyakhokha");

                // If not NULL run the app, if not, take the user to the app store
                if (intent != null)
                {
                    intent.AddFlags(ActivityFlags.NewTask);

                    Android.App.Application.Context.StartActivity(intent);
                }
                else
                {
                    intent = new Intent(Intent.ActionView);
                    intent.AddFlags(ActivityFlags.NewTask);
                    intent.SetData(Android.Net.Uri.Parse("https://play.google.com/store/apps/details?id=za.co.esiyakhokha"));
                    Android.App.Application.Context.StartActivity(intent);
                }

            
        }
    }
}