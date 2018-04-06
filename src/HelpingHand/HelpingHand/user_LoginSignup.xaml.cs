using Newtonsoft.Json;
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
	public partial class user_LoginSignup : ContentPage
	{
        private bool signinStatus=false, registerStatus=false;
        private string errorMessage;
        private HttpClient _client = new HttpClient();
        

        public user_LoginSignup()
		{
			InitializeComponent();

            ic_userloginEmail.Source = MainPage.primaryDomain + "/images/ic_email.png";
            ic_userloginPassword.Source = MainPage.primaryDomain + "/images/ic_password.png";
            ic_userSignupEmail.Source = MainPage.primaryDomain + "/images/ic_email.png";
            ic_userSignupName.Source = MainPage.primaryDomain + "/images/ic_account.png";
            ic_userSignupPhone.Source = MainPage.primaryDomain + "/images/ic_phone.png";
            ic_userSignupIdentification.Source = MainPage.primaryDomain + "/images/ic_perm_identity.png";
            ic_userSignupPassword.Source = MainPage.primaryDomain + "/images/ic_password.png";

            user_loginEmailID.Text = "kanojia24.10@gmail.com";
            user_loginPassword.Text = "lol";
        }

        void user_LogintoSignup(Object sender, System.EventArgs e)
        {
            //DisplayAlert("Title", "Clicked you", "Ok");
            //await Navigation.PushAsync(new userAuthority_LoginSignup());
            if(grid_UserSignupLayout.IsVisible)
            {
                grid_UserLoginLayout.IsVisible = true;
                grid_UserSignupLayout.IsVisible = false;
            }
            else
            {
                grid_UserLoginLayout.IsVisible = false;
                grid_UserSignupLayout.IsVisible = true;
            }
            
        }

        async void user_LoginSignupClicked(Object sender, System.EventArgs e)
        {
            if (String.IsNullOrEmpty(user_loginEmailID.Text) | String.IsNullOrEmpty(user_loginPassword.Text))
            {
                await DisplayAlert("Empty Fields", "Fields Cannot Be Empty.", "Ok");
            }
            else
            {
                await Task.Run(() => SignIn(user_loginEmailID.Text, user_loginPassword.Text));


                if (this.signinStatus)
                {
                    // Upon successful sign up, we move forward to next screen!
                    user_loginEmailID.Text = "";
                    user_loginPassword.Text = "";
                    if (models.user_LoggedInUserData.u_status == "1")
                    {
                        await DisplayAlert("Blocked Account", "Your Account Has Been Blocked!", "OK");
                        return;
                    }
                    else if (models.user_LoggedInUserData.u_status == "2")
                    {
                        await DisplayAlert("Not Verified", "You Need To Verify Your Email Address First!", "OK");
                        return;
                    }
                    else if (models.user_LoggedInUserData.u_status == "0")
                        await Navigation.PushAsync(new users_MainScreen());
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

        async void user_SignupClicked(Object sender, System.EventArgs e)
        {
            if (String.IsNullOrEmpty(user_SignupName.Text) || String.IsNullOrEmpty(user_SignupEmail.Text) ||
                String.IsNullOrEmpty(user_SignupPhoneNumber.Text) || String.IsNullOrEmpty(user_SignupPassword.Text) ||
                String.IsNullOrEmpty(user_IdentificationNumber.Text))
            {
                await DisplayAlert("Empty Fields", "Fields Cannot Be Empty.", "Ok");
            }
            else
            {
                await Task.Run(() => Register(user_SignupName.Text, user_SignupEmail.Text, user_SignupPassword.Text, user_IdentificationNumber.Text, user_SignupPhoneNumber.Text));


                if (this.registerStatus)
                {
                    // Upon successful sign up, we move forward to next screen!
                    user_SignupName.Text = "";
                    user_SignupEmail.Text = "";
                    user_SignupPhoneNumber.Text = "";
                    user_IdentificationNumber.Text = "";
                    user_SignupPassword.Text = "";
                    await DisplayAlert("Successful!", "Please Verify Your Email ID Now.", "Ok");
                    base.OnBackButtonPressed();
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
                        new KeyValuePair<string, string>("user_email", userEmail),
                        new KeyValuePair<string, string>("user_password", userPassword)
                    });
                var content = await _client.PostAsync(MainPage.primaryDomain + "/api/users/registration/login.php", data_to_send);

                var temp_response = await content.Content.ReadAsStringAsync();

                try
                {
                    dynamic obj2 = Newtonsoft.Json.Linq.JObject.Parse(temp_response);
                    if (Convert.ToString(obj2.error_code) == "1")
                    {
                        Console.WriteLine(obj2.message);
                        //Toast.MakeText(this, obj2.message, ToastLength.Long).Show(); // Here the error will be generated. "obj2.message" will have the error message.
                        this.errorMessage = "Incorrect Details";
                        this.signinStatus = false; // Bad Input?
                    }
                    else
                    {
                        this.signinStatus = true; // Flag to tell the other methods about Sign up status
                        models.user_LoggedInUserData.u_id = obj2.u_id;
                        models.user_LoggedInUserData.u_email = obj2.u_email;
                        models.user_LoggedInUserData.u_name = obj2.u_name;
                        models.user_LoggedInUserData.u_identification_no = obj2.u_identification_no;
                        models.user_LoggedInUserData.u_phone_number = obj2.u_phone_number;
                        models.user_LoggedInUserData.u_status = obj2.u_status;
                        models.user_LoggedInUserData.u_country = obj2.u_country;
                        models.user_LoggedInUserData.u_state = obj2.u_state;

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

        private async Task Register(string userName, string userEmail, string userPassword, string userIdNumber, string userPhoneNumber)
        {
            try
            {
                var data_to_send = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("user_name", userName),
                    new KeyValuePair<string, string>("user_email", userEmail),
                    new KeyValuePair<string, string>("user_password", userPassword),
                    new KeyValuePair<string, string>("user_id_no", userIdNumber),
                    new KeyValuePair<string, string>("user_phone", userPhoneNumber)
                });
                var content = await _client.PostAsync(MainPage.primaryDomain + "/api/users/registration/new_registration.php", data_to_send);

                var temp_response = await content.Content.ReadAsStringAsync();

                try
                {
                    dynamic obj2 = Newtonsoft.Json.Linq.JObject.Parse(temp_response);
                    if (Convert.ToString(obj2.error_code) == "1")
                    {
                        this.errorMessage = obj2.message;
                        this.registerStatus = false; // Bad Input?
                    }
                    else
                    {
                        this.registerStatus = true; // Flag to tell the other methods about Sign up status
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