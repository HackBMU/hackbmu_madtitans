using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HelpingHand
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class authority_LoginSignup : ContentPage
	{
        public bool ngo_signinStatus = false, ngo_registerStatus = false;
        private string errorMessage;
        private HttpClient _client = new HttpClient();

        public authority_LoginSignup ()
		{
			InitializeComponent ();

            ic_authorityLoginEmail.Source = MainPage.primaryDomain + "/images/ic_email.png";
            ic_authorityLoginPassword.Source = MainPage.primaryDomain + "/images/ic_password.png";

            ic_authoritySignupName.Source = MainPage.primaryDomain + "/images/ic_account.png";
            ic_authoritySignupEmail.Source = MainPage.primaryDomain + "/images/ic_email.png";
            ic_authoritySignupPhone.Source = MainPage.primaryDomain + "/images/ic_phone.png";
            ic_authoritySignupIdentification.Source = MainPage.primaryDomain + "/images/ic_perm_identity.png";
            ic_authoritySignupPassword.Source = MainPage.primaryDomain + "/images/ic_password.png";
            ic_authoritySignupAddress.Source = MainPage.primaryDomain + "/images/ic_address.png";

            authority_loginEmailID.Text = "kanojia24.10@gmail.com";
            authority_loginPassword.Text = "test";
		}
        void authority_LogintoSignup(Object sender, System.EventArgs e)
        {
            //DisplayAlert("Title", "Clicked you", "Ok");
            //await Navigation.PushAsync(new userAuthority_LoginSignup());
            if (grid_authoritySignupLayout.IsVisible)
            {
                grid_authorityLoginLayout.IsVisible = true;
                grid_authoritySignupLayout.IsVisible = false;
            }
            else
            {
                grid_authorityLoginLayout.IsVisible = false;
                grid_authoritySignupLayout.IsVisible = true;
            }

        }

        async void authority_SignupClicked(Object sender, System.EventArgs e)
        {
            if (String.IsNullOrEmpty(authority_SignupName.Text) | String.IsNullOrEmpty(authority_SignupEmail.Text) |
                String.IsNullOrEmpty(authority_SignupPhoneNumber.Text) | String.IsNullOrEmpty(authority_SignupPassword.Text) |
                String.IsNullOrEmpty(authority_SignupPassword.Text))
            {
                await DisplayAlert("Empty Fields", "Fields Cannot Be Empty.", "Ok");
            }
            else
            {
                await Task.Run(() => Register(authority_SignupName.Text, authority_SignupEmail.Text, authority_SignupPassword.Text, authority_RegistrationeNumber.Text, authority_SignupPhoneNumber.Text, authority_Address.Text));


                if (this.ngo_registerStatus)
                {
                    // Upon successful sign up, we move forward to next screen!
                    authority_SignupName.Text = "";
                    authority_SignupEmail.Text = "";
                    authority_SignupPassword.Text = "";
                    authority_RegistrationeNumber.Text = "";
                    authority_SignupPhoneNumber.Text = "";
                    authority_Address.Text = "";
                    await DisplayAlert("Successful!", "Please Verify Your Email ID Now.", "Ok");
                    base.OnBackButtonPressed();
                }
                else
                {
                    await DisplayAlert("Bad Login", this.errorMessage, "Ok");
                }
            }
        }

        async void authority_LoginSignupClicked(Object sender, System.EventArgs e)
        {
            if (String.IsNullOrEmpty(authority_loginEmailID.Text) | String.IsNullOrEmpty(authority_loginPassword.Text))
            {
                await DisplayAlert("Empty Fields", "Fields Cannot Be Empty.", "Ok");
            }
            else
            {
                await Task.Run(() => SignIn(authority_loginEmailID.Text, authority_loginPassword.Text));
                
                if (this.ngo_signinStatus)
                {
                    // Upon successful sign up, we move forward to next screen!
                    authority_loginEmailID.Text = "";
                    authority_loginPassword.Text = "";
                    
                    if (models.authority_loggedInAuthorityData.n_status == "1")
                    {
                        await DisplayAlert("Blocked Account", "Your Account Has Been Blocked!", "OK");
                        return;
                    }
                    else if (models.authority_loggedInAuthorityData.n_status == "2")
                    {
                        await DisplayAlert("Not Verified", "You Need To Verify Your Email Address First!", "OK");
                        return;
                    }
                    else if (models.authority_loggedInAuthorityData.n_status == "0")
                        await Navigation.PushAsync(new authority_MainScreen());
                    else
                    {
                        await DisplayAlert("OOps", "Some Error Occurred", "OK");
                        return;
                    }
                }
                else
                {
                    await DisplayAlert("Bad Login", this.errorMessage, "Ok");
                }
            }
        }

        private async Task SignIn(string userEmail, string userPassword)
        {
            try
            {
                var data_to_send = new FormUrlEncodedContent(new[]
                        {
                        new KeyValuePair<string, string>("ngo_email", userEmail),
                        new KeyValuePair<string, string>("ngo_password", userPassword)
                    });
                var content = await _client.PostAsync(MainPage.primaryDomain + "/api/ngo/registration/login.php", data_to_send);

                var temp_response = await content.Content.ReadAsStringAsync();

                try
                {
                    dynamic obj2 = Newtonsoft.Json.Linq.JObject.Parse(temp_response);
                    if (Convert.ToString(obj2.error_code) == "1")
                    {
                        Console.WriteLine(obj2.message);
                        //Toast.MakeText(this, obj2.message, ToastLength.Long).Show(); // Here the error will be generated. "obj2.message" will have the error message.
                        this.errorMessage = "Incorrect Details";
                        this.ngo_signinStatus = false; // Bad Input?
                    }
                    else
                    {
                        this.ngo_signinStatus = true; // Flag to tell the other methods about Sign up status
                        models.authority_loggedInAuthorityData.n_id = obj2.n_id;
                        models.authority_loggedInAuthorityData.n_email = obj2.n_email;
                        models.authority_loggedInAuthorityData.n_phone_no = obj2.n_phone_no;
                        models.authority_loggedInAuthorityData.n_registered_id = obj2.n_registered_id;
                        models.authority_loggedInAuthorityData.n_address = obj2.n_address;
                        models.authority_loggedInAuthorityData.n_status = obj2.n_status;
                        models.authority_loggedInAuthorityData.n_country = obj2.n_country;
                        models.authority_loggedInAuthorityData.n_state = obj2.n_state;
                        models.authority_loggedInAuthorityData.n_registered_name = obj2.n_registered_name;

                    }

                }
                catch (Exception)
                {
                    await DisplayAlert("No Internet", "Could Not Connect To Internet", "Ok");
                }
            }
            catch (Exception)
            {
                await DisplayAlert("No Internet", "Could Not Connect To Internet", "Ok");
            }
        }

        private async Task Register(string ngoName, string ngoEmail, string ngoPassword, string ngoIdNumber, string ngoPhoneNumber, string ngoAddress)
        {
            try
            {
                var data_to_send = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("ngo_name", ngoName),
                    new KeyValuePair<string, string>("ngo_email", ngoEmail),
                    new KeyValuePair<string, string>("ngo_password", ngoPassword),
                    new KeyValuePair<string, string>("ngo_id_no", ngoIdNumber),
                    new KeyValuePair<string, string>("ngo_address", ngoAddress),
                    new KeyValuePair<string, string>("ngo_phone", ngoPhoneNumber)
                });
                var content = await _client.PostAsync(MainPage.primaryDomain + "/api/ngo/registration/new_registration.php", data_to_send);

                var temp_response = await content.Content.ReadAsStringAsync();

                try
                {
                    dynamic obj2 = Newtonsoft.Json.Linq.JObject.Parse(temp_response);
                    if (Convert.ToString(obj2.error_code) == "1")
                    {
                        this.errorMessage = obj2.message;
                        this.ngo_registerStatus = false; // Bad Input?
                    }
                    else
                    {
                        this.ngo_registerStatus = true; // Flag to tell the other methods about Sign up status
                    }

                }
                catch (Exception)
                {
                    await DisplayAlert("No Internet", "Could Not Connect To Internet", "Ok");
                }
            }
            catch (Exception)
            {
                await DisplayAlert("No Internet", "Could Not Connect To Internet", "Ok");
            }
        }
    }
}