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
	public partial class NewEntry : ContentPage
	{
		public NewEntry ()
		{
			InitializeComponent();
		}

        private async void CameraButton_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("...", "Send Nudes", "k");
        }

        private async void submit_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("1", "2", "Ok");
        }

    }
}