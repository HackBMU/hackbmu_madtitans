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
    }
}