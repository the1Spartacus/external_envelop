﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using SQLite;
using Xamarin.Auth;

namespace Thulani
{
    [Activity(Label = "Facebook Sign Up")]
    public class FacebookSignupActivity : Activity
    {
        private Button facebookSignup;
        private EditText txtEmailF;
        private EditText txtCellNumberF;
        private EditText txtInitialsF;
        private EditText txtNameF;
        private EditText txtSurnameF;
        private EditText txtIDPassportF;
        private EditText txtPasswordF;


        private Spinner spnTitleF;
        private Spinner spnContactF;
        private Spinner spnTypeOfIDF;


        string selectedTitleF;
        string selectedTypeOfIDF;
        string selectedPreferedContactF;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            //Lauch facebook Sign up page

            var auth = new OAuth2Authenticator
               (clientId: "361849814291391",
               scope: "email",
               authorizeUrl: new System.Uri("https://www.facebook.com/dialog/oauth/"),
               redirectUrl: new System.Uri("http://www.facebook.com/connect/login_success.html"));

            auth.Completed += FacebookAuth_Completed;

            var ui = auth.GetUI(this);
            StartActivity(ui);

            SetContentView(Resource.Layout.FacebookSignUP);
            // Create your application here
            FindViews();
            HandleEvents();

            //Spinner for title
            spnTitleF.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(SpnTitleF_ItemSelected);
            var adapterTitleF = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.title_array, Resource.Layout.spinnerLayout);

            adapterTitleF.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spnTitleF.Adapter = adapterTitleF;

            //Spinner for type of ID
            spnTypeOfIDF.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(SpnTypeOfIDF_ItemSelected);
            var adapterTypeOfIDF = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.typeOfID_array, Resource.Layout.spinnerLayout);

            adapterTypeOfIDF.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spnTypeOfIDF.Adapter = adapterTypeOfIDF;

            //Spinner for prefered contact
            spnContactF.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(SpnContactF_ItemSelected);
            var adapterContactF = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.preferredContact_array, Resource.Layout.spinnerLayout);

            adapterContactF.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spnContactF.Adapter = adapterContactF;
        }
        // on facebook authenticatio complate then
        private async void FacebookAuth_Completed(object sender, AuthenticatorCompletedEventArgs e)
        {
            var request = new OAuth2Request(
                    "GET",
                    new Uri("https://graph.facebook.com/me?fields=name,email,picture,work,website,religion,location,locale,link,cover,age_range,birthday,devices,first_name,last_name,gender,hometown,is_verified,languages"),
                    null,
                    e.Account);

            var fbResponse = await request.GetResponseAsync();

            var json = fbResponse.GetResponseText();


            var fbUser = JsonConvert.DeserializeObject<FacebookUser>(json);

            txtEmailF.Text = fbUser.Email;
            txtNameF.Text = fbUser.FirstName;
            txtSurnameF.Text = fbUser.LastName;
            
        }

        private void FindViews()
        {
            facebookSignup = FindViewById<Button>(Resource.Id.buttonFacebookSignup);
            
            txtEmailF = FindViewById<EditText>(Resource.Id.editTextEmailF);
            txtCellNumberF = FindViewById<EditText>(Resource.Id.editTextCellNumberF);
            txtInitialsF = FindViewById<EditText>(Resource.Id.editTextInitialsF);
            txtNameF = FindViewById<EditText>(Resource.Id.editTextNameF);
            txtSurnameF = FindViewById<EditText>(Resource.Id.editTextSurnameF);
            txtIDPassportF = FindViewById<EditText>(Resource.Id.editTextIDPassportF);
            txtPasswordF = FindViewById<EditText>(Resource.Id.editTextPasswordF);

            spnTitleF = FindViewById<Spinner>(Resource.Id.spinnerTitleF);
            spnContactF = FindViewById<Spinner>(Resource.Id.spinnerPreferredContactF);
            spnTypeOfIDF = FindViewById<Spinner>(Resource.Id.spinnerTypeOfIDF);



        }
        private void HandleEvents()
        {
            spnTitleF.ItemSelected += SpnTitleF_ItemSelected;
            spnContactF.ItemSelected += SpnContactF_ItemSelected;
            spnTypeOfIDF.ItemSelected += SpnTypeOfIDF_ItemSelected;
            facebookSignup.Click += FacebookSignup_Click;
        }


        private void SpnTypeOfIDF_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            selectedTypeOfIDF = string.Format("{0}", spinner.GetItemAtPosition(e.Position));
          
        }

        private void SpnContactF_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            selectedPreferedContactF = string.Format("{0}", spinner.GetItemAtPosition(e.Position));
        }

        private void SpnTitleF_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            selectedTitleF = string.Format("{0}", spinner.GetItemAtPosition(e.Position));
        }
        // check user entered email id is valid or not  
        public bool IsValidEmail(string email)
        {
            return Android.Util.Patterns.EmailAddress.Matcher(email).Matches();
        }
        // validate password
        private bool ValidatePassword(string password, out string ErrorMessage)
        {
            var input = password;
            ErrorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(input))
            {
                throw new Exception("Password should not be empty");

            }

            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMiniMaxChars = new Regex(@".{8,15}");
            var hasLowerChar = new Regex(@"[a-z]+");
            var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

            if (!hasLowerChar.IsMatch(input))
            {
                ErrorMessage = "Password should contain At least one lower case letter";
                return false;
            }
            else if (!hasUpperChar.IsMatch(input))
            {
                ErrorMessage = "Password should contain At least one upper case letter";
                return false;
            }
            else if (!hasMiniMaxChars.IsMatch(input))
            {
                ErrorMessage = "Password should not be less than or greater than 12 characters";
                return false;
            }
            else if (!hasNumber.IsMatch(input))
            {
                ErrorMessage = "Password should contain At least one numeric value";
                return false;
            }

            else if (!hasSymbols.IsMatch(input))
            {
                ErrorMessage = "Password should contain At least one special case characters";
                return false;
            }
            else
            {
                return true;
            }
        }
        private void FacebookSignup_Click(object sender, EventArgs e)
        {
            var passwordResult = ValidatePassword(txtPasswordF.Text, out string error);

            var emailResult = IsValidEmail(txtEmailF.Text);
            if (txtEmailF.Text == "")
            {
                Toast.MakeText(this, "email can not be empty...,", ToastLength.Short).Show();
            }
            else if (emailResult == false)
            {
                Toast.MakeText(this, "invalid email...,", ToastLength.Short).Show();
            }
            else if (txtCellNumberF.Text == "")
            {
                Toast.MakeText(this, "cell number can not be empty...,", ToastLength.Short).Show();
            }
            else if (selectedTitleF == "")
            {
                Toast.MakeText(this, "Title can not be empty...,", ToastLength.Short).Show();
            }
            else if (txtInitialsF.Text == "")
            {
                Toast.MakeText(this, "Initials can not be empty...,", ToastLength.Short).Show();
            }
            else if (txtNameF.Text == "")
            {
                Toast.MakeText(this, "Name can not be empty...,", ToastLength.Short).Show();
            }
            else if (txtSurnameF.Text == "")
            {
                Toast.MakeText(this, "Surname can not be empty...,", ToastLength.Short).Show();
            }
            else if (selectedTypeOfIDF == "")
            {
                Toast.MakeText(this, "Type of ID can not be empty...,", ToastLength.Short).Show();
            }
            else if (txtIDPassportF.Text == "")
            {
                Toast.MakeText(this, "ID/passport can not be empty...,", ToastLength.Short).Show();
            }
            else if (selectedPreferedContactF == "")
            {
                Toast.MakeText(this, "ID/passport can not be empty...,", ToastLength.Short).Show();
            }
            else if (passwordResult == false)
            {
                Toast.MakeText(this, error, ToastLength.Short).Show();
            }
            else
            {
                try
                {
                    string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "user.db3");
                    var db = new SQLiteConnection(dpPath);
                    db.CreateTable<LoginTable>();
                    LoginTable tbl = new LoginTable
                    {
                        Email = txtEmailF.Text,
                        CellNumber = txtCellNumberF.Text,
                        Title = selectedTitleF,
                        Initials = txtInitialsF.Text,
                        Name = txtNameF.Text,
                        Surname = txtSurnameF.Text,
                        IdType = selectedTypeOfIDF,
                        IdPass = txtIDPassportF.Text,
                        PreferedContact = selectedPreferedContactF,
                        Password = txtPasswordF.Text
                    };

                    db.Insert(tbl);
                    Toast.MakeText(this, "Record Added Successfully...,", ToastLength.Short).Show();
                    var intent = new Intent(this, typeof(MainActivity));

                    StartActivity(intent);
                }
                catch (Exception ex)
                {
                    Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();
                }
            }
        }
    }
}






