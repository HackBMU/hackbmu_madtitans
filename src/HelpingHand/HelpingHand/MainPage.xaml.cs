using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HelpingHand
{
	public partial class MainPage : ContentPage
	{
        public static string primaryDomain = "http://40.71.17.14";
		public MainPage()
		{
			InitializeComponent();
            mainBanner.Source = primaryDomain + "/images/mainBanner.png";

        }

        async void button_GetClicked(Object sender,System.EventArgs e)
        {
            //DisplayAlert("Title", "Clicked you", "Ok");
            //grid_UserLoginLayout.Isvisible = false;
            await Navigation.PushAsync(new userAuthority_LoginSignup());
        }

    }
}
