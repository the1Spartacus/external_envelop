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
    [Activity(Label = "Settings")]
    public class SettingsActivity : Activity
    {
        private Button editProfile;
        private Button preference;
        private Button changePassword;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Call settings view
            SetContentView(Resource.Layout.Settings);
            FindViews();
            HandleEvents();
        }
        private void FindViews()
        {
            editProfile = FindViewById<Button>(Resource.Id.buttonEditProfile);
            preference = FindViewById<Button>(Resource.Id.buttonPreferences);
            changePassword = FindViewById<Button>(Resource.Id.buttonChangePassword);
        }
        private void HandleEvents()
        {
            editProfile.Click += EditProfile_Click;
            preference.Click += Preference_Click;
            changePassword.Click += ChangePassword_Click;
        }

        private void ChangePassword_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(ChangePasswordActivity));
            StartActivity(intent);
        }

        private void Preference_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(ForgetPassActivity));
            StartActivity(intent);
        }

        private void EditProfile_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(RegisterNewUserActivity));
            StartActivity(intent);
        }
    }
}