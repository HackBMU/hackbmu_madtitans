using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HelpingHand
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class authority_LoginSignup : ContentPage
	{
		public authority_LoginSignup ()
		{
			InitializeComponent ();
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

        async void authority_LoginSignupClicked(Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new authority_MainScreen());
        }
    }
}