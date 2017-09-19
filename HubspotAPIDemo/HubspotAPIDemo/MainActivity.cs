using Android.App;
using Android.Widget;
using Android.OS;
using Xamarin.Auth;
using System;

namespace HubspotAPIDemo
{
    [Activity(Label = "HubspotAPIDemo", MainLauncher = true)]
    public class MainActivity : Activity
    {
        Button AuthenticationButton;
        TextView Status;
        string clientid;
        string clientsecret;
        string constants;
        string autorizeurl;
        string redirecturl;
        string accesstokenurl;
        string userinfo;
        private string token;
        public OAuth2Authenticator authenticator { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            Xamarin.Auth.Presenters.XamarinAndroid.AuthenticationConfiguration.Init(this, savedInstanceState);

            clientid = "d45c3fe3-0e97-4f57-af9d-f517c85a3882";
            clientsecret = "a63ff38c-f409-4e39-a50f-3fed1e179262";
            constants = "contacts";
            autorizeurl = "https://app.hubspot.com/oauth/authorize";
            redirecturl = "https://www.hubspot.es/";
            accesstokenurl = "https://api.hubapi.com/oauth/v1/token";
            userinfo = "https://api.neur.io/v1/name";

            AuthenticationButton = FindViewById<Button>(Resource.Id.buttonAuthenticationh);
            Status = FindViewById<TextView>(Resource.Id.textViewStatus);

            authenticator = new OAuth2Authenticator(
                clientid,
                clientsecret,
                constants,
                new Uri(autorizeurl),
                new Uri(redirecturl),
                new Uri(accesstokenurl));

            authenticator.Completed += OAuthCompleted;

            authenticator.Error += OAuthError;

            AuthenticationButton.Click += LaunchOAuth;
        }

        private void LaunchOAuth(object sender, EventArgs e)
        {
            var ui = authenticator.GetUI(this);
            StartActivity(ui);
        }

        private void OAuthError(object sender, AuthenticatorErrorEventArgs e)
        {
            Status.Text = e.Message.ToString();
        }

        private void OAuthCompleted(object sender, AuthenticatorCompletedEventArgs e)
        {
            if (e.IsAuthenticated)
            {
                token = e.Account.Properties["access_token"];
                Status.Text = token;
                GetUser(e.Account);
            }
        }

        private void GetUser(Account account)
        {

        }
    }
}

