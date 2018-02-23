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
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your application here
            SetContentView(Resource.Layout.Home);

            FindViews();
            HandleEvents();
        }
        private void FindViews()
        {
            siyakhokhaButton = FindViewById<Button>(Resource.Id.buttonSiyakhokha);
            settingButton = FindViewById<Button>(Resource.Id.buttonSettings);
        }
        private void HandleEvents()
        {
            siyakhokhaButton.Click += SiyakhokhaButton_Click;
            settingButton.Click += SettingButton_Click;
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