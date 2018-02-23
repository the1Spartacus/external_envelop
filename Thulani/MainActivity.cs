using Android.App;
using Android.Widget;
using Android.OS;
using System;
using Android.Content;
using Android.Views;
using Xamarin.Auth;
using Newtonsoft.Json;
using System.IO;
using SQLite;

namespace Thulani
{
    [Activity(Theme = "@android:style/Theme.DeviceDefault.NoActionBar")]
    public class MainActivity : Activity
    {
        private Button loginButton;
        private TextView createAccount;
        private TextView forgetPass;
        private ImageButton facebookButton;
        private EditText mEmail;
        private EditText mPassword;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            
            FindViews();
            HandleEvents();
            CreateDB();
        }
        // find views by ids
        private void FindViews()
        {
            loginButton = FindViewById<Button>(Resource.Id.button1);
            createAccount = FindViewById<TextView>(Resource.Id.textViewCreateAccount);
            forgetPass = FindViewById<TextView>(Resource.Id.textViewForgetPassword);
            facebookButton = FindViewById<ImageButton>(Resource.Id.buttonFacebook);
            mEmail = FindViewById<EditText>(Resource.Id.editTextEmail);
            mPassword = FindViewById<EditText>(Resource.Id.editTextPass);
        }
        //handling events
        private void HandleEvents()
        {
            loginButton.Click += LoginButton_Click;
            createAccount.Click += CreateAccount_Click;
            forgetPass.Click += ForgetPass_Click;
            facebookButton.Click += FacebookButton_Click;
        }

        // login/sign in with facebook
        private void FacebookButton_Click(object sender, System.EventArgs e)
        {
            var intent = new Intent(this, typeof(FacebookSignupActivity));
            StartActivity(intent);
        }

       

        //launch forget password views/activity
        private void ForgetPass_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(ForgetPassActivity));
            StartActivity(intent);
        }
        //launch create new account views/activity
        private void CreateAccount_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(RegisterNewUserActivity));
            StartActivity(intent);

        }
        //launch home views/activity
        private void LoginButton_Click(object sender, EventArgs e)
        {
            try
            {
                string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "user.db3"); //Call Database  
                var db = new SQLiteConnection(dpPath);
                var data = db.Table<LoginTable>(); //Call Table  
                var data1 = data.Where(x => x.Email == mEmail.Text && x.Password == mPassword.Text).FirstOrDefault(); //Linq Query  
                if (data1 != null)
                {
                    Toast.MakeText(this, "Login Success", ToastLength.Short).Show();
                    var intent = new Intent(this, typeof(HomeActivity));
                    StartActivity(intent);
                }
                else
                {
                    Toast.MakeText(this, "Username or Password invalid", ToastLength.Short).Show();
                }
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();
            }
          
        }
        public string CreateDB()
        {
            var output = "";
            output += "Creating Databse if it doesnt exists";
            string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "user.db3"); //Create New Database  
            var db = new SQLiteConnection(dpPath);
            output += "\n Database Created....";
            return output;
        }
    }
}

