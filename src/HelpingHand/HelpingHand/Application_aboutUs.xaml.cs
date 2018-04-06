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
	public partial class Application_aboutUs : ContentPage
	{
		public Application_aboutUs ()
		{
			InitializeComponent ();
            ankit_PhotoImage.Source = MainPage.primaryDomain + "/images/ankit.jpg";
            ankit_githubButton.Source = MainPage.primaryDomain + "/images/github-circle.png";
            ankit_linkedInButton.Source = MainPage.primaryDomain + "/images/linkedin-box.png";
            ankit_behanceButton.Source = MainPage.primaryDomain + "/images/behance.png";

            dhruv_PhotoImage.Source = MainPage.primaryDomain + "/images/dhruv.jpg";
            dhruv_githubButton.Source = MainPage.primaryDomain + "/images/github-circle.png";
            dhruv_linkedInButton.Source = MainPage.primaryDomain + "/images/linkedin-box.png";
            dhruv_blogButton.Source = MainPage.primaryDomain + "/images/blogger.png";

            shivangi_PhotoImage.Source = MainPage.primaryDomain + "/images/shivangi.jpg";
            shivangi_githubButton.Source = MainPage.primaryDomain + "/images/github-circle.png";
            shivangi_linkedInButton.Source = MainPage.primaryDomain + "/images/linkedin-box.png";
            shivangi_facebookButton.Source = MainPage.primaryDomain + "/images/facebook-box.png";
        }

        void ankit_GitubTap(object sender, EventArgs args)
        {
            var imageSender = (Image)sender;
            Device.OpenUri(new Uri("https://github.com/ankitpassi141/"));
            //DisplayAlert("Alert", "Tap gesture recoganised", "OK");
        }

        void ankit_linkedInTap(object sender, EventArgs args)
        {
            var imageSender = (Image)sender;
            Device.OpenUri(new Uri("https://www.linkedin.com/in/ankitpassi141/"));
            //DisplayAlert("Alert", "Tap gesture recoganised", "OK");
        }

        void ankit_behanceTap(object sender, EventArgs args)
        {
            var imageSender = (Image)sender;
            Device.OpenUri(new Uri("https://www.behance.net/passiankitd8a0"));
            //DisplayAlert("Alert", "Tap gesture recoganised", "OK");
        }

        void dhruv__GitubTap(object sender, EventArgs args)
        {
            var imageSender = (Image)sender;
            Device.OpenUri(new Uri("https://github.com/xonshiz"));
            //DisplayAlert("Alert", "Tap gesture recoganised", "OK");
        }

        void dhruv_linkedInTap(object sender, EventArgs args)
        {
            var imageSender = (Image)sender;
            Device.OpenUri(new Uri("https://www.linkedin.com/in/dhruvkanojia/"));
            //DisplayAlert("Alert", "Tap gesture recoganised", "OK");
        }

        void dhruv_blogTap(object sender, EventArgs args)
        {
            var imageSender = (Image)sender;
            Device.OpenUri(new Uri("http://xonshiz.heliohost.org/blog/"));
            //DisplayAlert("Alert", "Tap gesture recoganised", "OK");
        }

        void shivangi_GitubTap(object sender, EventArgs args)
        {
            //DisplayAlert("Alert", "Tap gesture recoganised", "OK");
        }

        void shivangi_linkedInTap(object sender, EventArgs args)
        {
            var imageSender = (Image)sender;
            Device.OpenUri(new Uri("https://www.linkedin.com/in/shivangi-mittal-864844b5/"));
            //DisplayAlert("Alert", "Tap gesture recoganised", "OK");
        }

        void shivangi_facebookTap(object sender, EventArgs args)
        {
            var imageSender = (Image)sender;
            Device.OpenUri(new Uri("https://www.facebook.com/Shivangi4196"));
            //DisplayAlert("Alert", "Tap gesture recoganised", "OK");
        }
    }
}