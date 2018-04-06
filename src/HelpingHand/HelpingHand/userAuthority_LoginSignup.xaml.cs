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
    public partial class userAuthority_LoginSignup : TabbedPage
    {
        public userAuthority_LoginSignup ()
        {
            InitializeComponent();
            models.user_LoggedInUserData userData = new models.user_LoggedInUserData();
            models.authority_loggedInAuthorityData authorityData = new models.authority_loggedInAuthorityData();
        }

        async void aboutUs_Activated(Object sender, System.EventArgs e)
        {
            //await DisplayAlert("Title", "Clicked you", "Ok");
            //await Navigation.PushAsync(new userAuthority_LoginSignup());
            await Navigation.PushAsync(new Application_aboutUs());
        }
    }
}