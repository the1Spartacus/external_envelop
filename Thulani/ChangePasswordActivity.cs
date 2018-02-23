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
    [Activity(Label = "Change Password")]
    public class ChangePasswordActivity : Activity
    {
        private EditText oldPassword;
        private EditText newPassword;
        private EditText comfirmPassword;
        private Button savePassword;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Call settings view
            SetContentView(Resource.Layout.ChangePassword);
            FindViews();
            HandleEvents();
        }
        private void FindViews()
        {
            oldPassword = FindViewById<EditText>(Resource.Id.editTextOldPassword);
            newPassword = FindViewById<EditText>(Resource.Id.editTextNewPassword);
            comfirmPassword = FindViewById<EditText>(Resource.Id.editTextConfirmPassword);
            savePassword = FindViewById<Button>(Resource.Id.buttonSavePassword);

        }
        private void HandleEvents()
        {
            savePassword.Click += SavePassword_Click;
        }

        private void SavePassword_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(SettingsActivity));
            StartActivity(intent);
        }
    }
}