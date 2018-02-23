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
    [Activity(Label = "ForgetPassActivity")]
    public class ForgetPassActivity : Activity
    {
        private Button resetPass;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            //Assign the Forgetpass.axml to this Activity
            SetContentView(Resource.Layout.ForgetPass);

            //pass views
            FindViews();
            HandleEvents();
        }
        //find views
        private void FindViews()
        {
            resetPass = FindViewById<Button>(Resource.Id.buttonResetPass);
        }
        //handling events
        private void HandleEvents()
        {
            resetPass.Click += ResetPass_Click;
        }

        private void ResetPass_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);
        }
    }
}