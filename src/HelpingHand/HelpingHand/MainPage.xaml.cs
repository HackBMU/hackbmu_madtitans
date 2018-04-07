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
		public MainPage()
		{
			InitializeComponent();
		}

        async void button_GetClicked(Object sender,System.EventArgs e)
        {
            //DisplayAlert("Title", "Clicked you", "Ok");
            //grid_UserLoginLayout.Isvisible = false;
            await Navigation.PushAsync(new userAuthority_LoginSignup());
        }

    }
}
