using System;
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
using SQLite;

namespace Thulani
{

    [Activity(Label = "Register")]
    public class RegisterNewUserActivity : Activity
    {
        private Button registerUser;
        private EditText txtEmail;
        private EditText txtCellNumber;
        private EditText txtInitials;
        private EditText txtName;
        private EditText txtSurname;
        private EditText txtIDPassport;
        private EditText txtPassword;


        private Spinner spnTitle;
        private Spinner spnContact;
        private Spinner spnTypeOfID;

        string selectedTitle;
        string selectedTypeOfID;
        string selectedPreferedContact;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // assign register new user view to this activity
            SetContentView(Resource.Layout.RegisterNewUser);

            //pass views
            FindViews();
            //pass event handler
            HandleEvents();

            //Spinner for title
            spnTitle.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(SpnTitle_ItemSelected);
            var adapterTitle = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.title_array, Resource.Layout.spinnerLayout);

            adapterTitle.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spnTitle.Adapter = adapterTitle;

            //Spinner for type of ID
            spnTypeOfID.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(SpnTypeOfID_ItemSelected);
            var adapterTypeOfID = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.typeOfID_array, Resource.Layout.spinnerLayout);

            adapterTypeOfID.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spnTypeOfID.Adapter = adapterTypeOfID;

            //Spinner for prefered contact
            spnContact.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(SpnContact_ItemSelected);
            var adapterContact = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.preferredContact_array, Resource.Layout.spinnerLayout);

            adapterContact.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spnContact.Adapter = adapterContact;

        }
        private void FindViews()
        {
            registerUser = FindViewById<Button>(Resource.Id.buttonRegister);


            txtInitials = FindViewById<EditText>(Resource.Id.editTextInitials);
            txtName = FindViewById<EditText>(Resource.Id.editTextName);
            txtSurname = FindViewById<EditText>(Resource.Id.editTextSurname);
            txtPassword = FindViewById<EditText>(Resource.Id.editTextPassword);
            txtIDPassport = FindViewById<EditText>(Resource.Id.editTextIDPassport);
            txtEmail = FindViewById<EditText>(Resource.Id.editTextEmail);
            txtCellNumber = FindViewById<EditText>(Resource.Id.editTextCellNumber);


            spnTitle = FindViewById<Spinner>(Resource.Id.spinnerTitle);
            spnTypeOfID = FindViewById<Spinner>(Resource.Id.spinnerTypeOfID);
            spnContact = FindViewById<Spinner>(Resource.Id.spinnerPreferredContact);


        }
        private void HandleEvents()
        {
            spnTitle.ItemSelected += SpnTitle_ItemSelected;
            spnContact.ItemSelected += SpnContact_ItemSelected;
            spnTypeOfID.ItemSelected += SpnTypeOfID_ItemSelected;

            registerUser.Click += RegisterUser_Click;
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

        private void SpnTypeOfID_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            selectedTypeOfID = string.Format("{0}", spinner.GetItemAtPosition(e.Position));
            if (selectedTypeOfID == "ID")
            {
                txtIDPassport.Hint = "Enter ID";
            }
            else
            {
                txtIDPassport.Hint = "Enter Passport";
            }
        }

        private void SpnContact_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            selectedPreferedContact = string.Format("{0}", spinner.GetItemAtPosition(e.Position));

        }

        private void SpnTitle_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            selectedTitle = string.Format("{0}", spinner.GetItemAtPosition(e.Position));
        }

        private void RegisterUser_Click(object sender, EventArgs e)
        {
            var passwordResult = ValidatePassword(txtPassword.Text, out string error);

            var emailResult = IsValidEmail(txtEmail.Text);
            if (txtEmail.Text == "")
            {
                Toast.MakeText(this, "email can not be empty...,", ToastLength.Short).Show();
            }
            else if (emailResult == false)
            {
                Toast.MakeText(this, "invalid email...,", ToastLength.Short).Show();
            }
            else if (txtCellNumber.Text == "")
            {
                Toast.MakeText(this, "cell number can not be empty...,", ToastLength.Short).Show();
            }
            else if (selectedTitle == "")
            {
                Toast.MakeText(this, "Title can not be empty...,", ToastLength.Short).Show();
            }
            else if (txtInitials.Text == "")
            {
                Toast.MakeText(this, "Initials can not be empty...,", ToastLength.Short).Show();
            }
            else if (txtName.Text == "")
            {
                Toast.MakeText(this, "Name can not be empty...,", ToastLength.Short).Show();
            }
            else if (txtSurname.Text == "")
            {
                Toast.MakeText(this, "Surname can not be empty...,", ToastLength.Short).Show();
            }
            else if (selectedTypeOfID == "")
            {
                Toast.MakeText(this, "Type of ID can not be empty...,", ToastLength.Short).Show();
            }
            else if (txtIDPassport.Text == "")
            {
                Toast.MakeText(this, "ID/passport can not be empty...,", ToastLength.Short).Show();
            }
            else if (selectedPreferedContact == "")
            {
                Toast.MakeText(this, "ID/passport can not be empty...,", ToastLength.Short).Show();
            }
            else if (passwordResult == false)
            {
                Toast.MakeText(this, error , ToastLength.Short).Show();
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
                        Email = txtEmail.Text,
                        CellNumber = txtCellNumber.Text,
                        Title = selectedTitle,
                        Initials = txtInitials.Text,
                        Name = txtName.Text,
                        Surname = txtSurname.Text,
                        IdType = selectedTypeOfID,
                        IdPass = txtIDPassport.Text,
                        PreferedContact = selectedPreferedContact,
                        Password = txtPassword.Text
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

