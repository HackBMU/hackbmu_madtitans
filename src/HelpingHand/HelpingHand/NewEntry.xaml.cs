using Rox;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Maps;

namespace HelpingHand
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewEntry : ContentPage
    {
        private HttpClient _client = new HttpClient();
        private string f_city, f_country, f_geocode, b_areaCode, b_extraInformation, errorMessage;
        public string base64String;
        Geocoder geoCoder;
        private bool isDataCorrect = false, isLocationFetched=false, additionStatus=false;

        public interface MyImageCompressor
        {
            string ImageCompressor(byte[] bitmap);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                geoCoder = new Geocoder();
                await LocationFetcher();
            }
            catch (Exception)
            {
                //Do Nothing...
            }
        }

        public NewEntry()
        {
            InitializeComponent();

            var goBackLabel_tap = new TapGestureRecognizer();
            goBackLabel_tap.Tapped += (s, e) =>
            {
                Navigation.PopModalAsync();
            };
            //backBar_Label.GestureRecognizers.Add(goBackLabel_tap);
        }

        private async void submit_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(base64String))
            {
                await Task.Delay(3000);
            }
            if (string.IsNullOrEmpty(e_areaCode.Text) || string.IsNullOrEmpty(e_streetAddress.Text)
                || string.IsNullOrEmpty(base64String) || string.IsNullOrEmpty(models.user_LoggedInUserData.u_id))
            {
                await DisplayAlert("Empty Fields", "Make Sure All The Necessary Fields Are Filled", "Ok");
            }
            else
            {
                await EntryAdder();

                if (additionStatus)
                {
                    await DisplayAlert("Success!", "Record Has Been Added!", "Ok");
                    base.OnBackButtonPressed();
                }
                else
                {
                    await DisplayAlert("Failure!", "Failed To Add The Record!", "Ok");
                }
            }

        }

        private async void CameraButton_Clicked(object sender, EventArgs e)
        {

            try
            {
                ICameraProvider cameraProvider = DependencyService.Get<ICameraProvider>();

                var myImage = await cameraProvider.AcquirePicture();

                PhotoImage.Source = myImage;
                var uri = PhotoImage.Source.GetValue(StreamImageSource.StreamProperty);

                StreamImageSource streamImageSource = (StreamImageSource)PhotoImage.Source;
                System.Threading.CancellationToken cancellationToken = System.Threading.CancellationToken.None;
                Task<Stream> task = streamImageSource.Stream(cancellationToken);
                Stream stream = task.Result;

                var memoryStream = new MemoryStream();
                stream.CopyTo(memoryStream);

                Console.WriteLine("Working Till Stream Making");

                //var base64String = Convert.ToBase64String(memoryStream.ToArray());
                //base64String = Convert.ToBase64String(memoryStream.ToArray());
                base64String = DependencyService.Get<MyImageCompressor>().ImageCompressor(memoryStream.ToArray());
                Console.WriteLine("String Compressed");
                //base64String = byte[] test = DependencyService.Get<IImageResizer>().ResizeImage(AByteArrayHereCauseFun, 400, 400);

            }
            catch (Exception CameraException)
            {
                //No camera presenet or no access provided. So, instead of crashing, let's not get the data.
                Console.WriteLine("Exception CameraException : " + CameraException);
                isDataCorrect = false;
                await DisplayAlert("No Camera Found!", "I could not get access to your camera.", "Ok");
            }


        }

        private async Task LocationFetcher()
        {
            try
            {
                var content = await _client.GetAsync("https://api.ipdata.co/");

                var temp_response = await content.Content.ReadAsStringAsync();

                try
                {
                    dynamic obj2 = Newtonsoft.Json.Linq.JObject.Parse(temp_response);
                    this.isLocationFetched = true; // Flag to tell the other methods about Sign up status
                    f_city = obj2.city;
                    f_country = obj2.country_name;
                    f_geocode = Convert.ToString(obj2.latitude) + Convert.ToString(obj2.longitude);
                    var position = new Position(Convert.ToDouble(obj2.latitude), Convert.ToDouble(obj2.longitude));
                    var possibleAddresses = await geoCoder.GetAddressesForPositionAsync(position);
                    if (!string.IsNullOrEmpty(e_streetAddress.Text))
                        e_streetAddress.Text += Convert.ToString(possibleAddresses.First());
                    //foreach (var address in possibleAddresses)
                    //    e_streetAddress.Text += address + "\n";
                }
                catch (Exception InnerException)
                {
                    Console.WriteLine("InnerException : " + InnerException);
                    await DisplayAlert("No Internet", "Could Not Connect To Internet", "Ok");
                }
            }
            catch (Exception OuterException)
            {
                Console.WriteLine("OuterException : " + OuterException);
                await DisplayAlert("No Internet", "Could Not Connect To Internet", "Ok");
            }
        }

        private async Task EntryAdder()
        {
            try
            {
                var data_to_send = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("b_photo", base64String),
                    new KeyValuePair<string, string>("b_adder", models.user_LoggedInUserData.u_id),
                    new KeyValuePair<string, string>("b_country", f_country),
                    new KeyValuePair<string, string>("b_city", f_city),
                    new KeyValuePair<string, string>("b_area_code", e_areaCode.Text),
                    new KeyValuePair<string, string>("b_street_address", e_streetAddress.Text),
                    new KeyValuePair<string, string>("b_age", e_age.Text),
                    new KeyValuePair<string, string>("b_family_count", e_familyCount.Text),
                    new KeyValuePair<string, string>("b_extra_info", e_extraInformation.Text),
                    new KeyValuePair<string, string>("b_geocode", f_geocode)
                });
                var content = await _client.PostAsync(MainPage.primaryDomain + "/api/beggars/new_entry.php", data_to_send);

                var temp_response = await content.Content.ReadAsStringAsync();

                try
                {
                    dynamic obj2 = Newtonsoft.Json.Linq.JObject.Parse(temp_response);
                    if (Convert.ToString(obj2.error_code) == "1")
                    {
                        this.errorMessage = obj2.message;
                        this.additionStatus = false; // Bad Input?
                    }
                    else
                    {
                        this.additionStatus = true; // Flag to tell the other methods about Sign up status
                    }

                }
                catch (Exception SubmitInner)
                {
                    Console.WriteLine("Exception SubmitInner : " + SubmitInner);
                    await DisplayAlert("No Internet", "Could Not Connect To Internet", "Ok");
                }
            }
            catch (Exception SubmitOuter)
            {
                Console.WriteLine("Exception SubmitOuter : " + SubmitOuter);
                await DisplayAlert("No Internet", "Could Not Connect To Internet", "Ok");
            }
        }


    }
}