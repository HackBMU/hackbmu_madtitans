using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HelpingHand
{
	public partial class user_LoginSignup : ContentPage
	{
		public user_LoginSignup ()
		{
			InitializeComponent ();
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
            await Navigation.PushAsync(new users_MainScreen());
        }
    }
}