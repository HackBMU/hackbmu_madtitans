using Newtonsoft.Json;
using Rox;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static HelpingHand.NewEntry;

namespace HelpingHand
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DetailsOfEntry : ContentPage
	{
        private HttpClient _client = new HttpClient();
        private string errorMessage;
        private bool updationStatus = false;

        public string selected_b_id, selected_state, selected_area_code, selected_country, selected_age, selected_family_count,
            selected_street_address, selected_status, selected_ngo, selected_creation_date, selected_photo, current_user;

        public string base64String;

        public DetailsOfEntry (string b_id, string b_state, string b_area_code, string b_country, string b_age,
            string b_family_count, string b_street_address, string b_status, string b_ngo, string b_creation_date, string b_photo, string user_type)
		{
			InitializeComponent ();

            ic_entryDetailAge.Source = MainPage.primaryDomain + "/images/ic_age.png";
            ic_entryDetailAddress.Source = MainPage.primaryDomain + "/images/ic_address.png";
            ic_entryDetailDenialReason.Source = MainPage.primaryDomain + "/images/ic_cancel.png";
            ic_entryDetailFamily.Source = MainPage.primaryDomain + "/images/ic_supervisor_account.png";
            ic_entryDetailTime.Source = MainPage.primaryDomain + "/images/ic_age.png";
            

            // Tapping the Label to go back.
            var detailsBackLabel_tap = new TapGestureRecognizer();
            detailsBackLabel_tap.Tapped += (s, e) =>
            {
                Navigation.PopModalAsync();
            };
            //backBar_Label.GestureRecognizers.Add(detailsBackLabel_tap);

            current_user = user_type;
            selected_ngo = b_ngo;
            selected_status = b_status;

            selected_b_id = b_id;
            entry_Image.Source = b_photo;
            entry_Age.Text = b_age;
            entry_FamilyCount.Text = b_family_count;
            entry_StreetAddress.Text = b_street_address ;
            //DynamicStackLayout.IsVisible = false;
            //DynamicStackLayout.IsEnabled = false;

            //frame_EntryStatus
            AuthorityEditRights(); //Need to get the Authority rights!

            if (string.IsNullOrEmpty(b_age) || b_age == "0")
                entry_Age.IsVisible = false;
            if (string.IsNullOrEmpty(b_family_count) || b_family_count == "0")
                entry_Age.IsVisible = false;

        }

        private void TapGestureRecognizer_Tapped()
        {
            //entry_Status.Focus();
            user_entry_Status.Text = entry_Status.SelectedItem.ToString();
            ngo_entry_Status.Text = entry_Status.SelectedItem.ToString();
        }

        private void label_TapGesture(object sender, EventArgs e)
        {
            entry_Status.Focus();
        }

        public void AuthorityEditRights()
        {

            if (current_user == "ngo")
            {
                ngo_entry_Status.IsVisible = true;
                user_entry_Status.IsVisible = false;
                ngo_CandidateDetails.IsVisible = true;
                // If the current logged in user is AUTHORITY and has the EDIT rights to this entry, we let them edit this entry's status.
                if (selected_ngo == models.authority_loggedInAuthorityData.n_id)
                {
                    entry_Status.IsEnabled = true;
                    //frame_EntryStatus.IsVisible = true;
                }

                if (selected_status == "0")
                {
                    entry_Status.SelectedIndex = 0;
                    TapGestureRecognizer_Tapped();
                    ngo_CandidateDetails.IsVisible = false;
                    Submit_Button.IsEnabled = false;
                    Submit_Button.IsVisible = false;
                    Contacting_StackLayout.IsVisible = false;
                    AcceptanceProof_Button.IsEnabled = false;
                    AcceptanceProof_Button.IsVisible = false;
                }
                else if (selected_status == "1")
                {
                    ngo_CandidateDetails.IsVisible = false;
                    entry_Status.SelectedIndex = 1;
                    TapGestureRecognizer_Tapped();
                    entry_Status.IsEnabled = true;
                    Contacting_StackLayout.IsVisible = true;
                    Contacting_StackLayout.IsEnabled = true;
                    Submit_Button.IsVisible = true;
                    //frame_EntryStatus.IsVisible = true;
                }
                else if (selected_status == "2")
                {
                    ngo_CandidateDetails.IsVisible = false;
                    entry_Status.SelectedIndex = 2;
                    TapGestureRecognizer_Tapped();
                    entry_Status.IsEnabled = true;
                    Contacting_StackLayout.IsVisible = false;
                    AcceptanceProof_Button.IsVisible = false;

                    //frame_EntryStatus.IsVisible = true;
                }
                else if (selected_status == "3")
                {
                    entry_Status.SelectedIndex = 3;
                    TapGestureRecognizer_Tapped();
                    entry_Status.IsEnabled = true;
                    Contacting_StackLayout.IsVisible = false;
                    ngo_CandidateDetails.IsVisible = false;
                }
                else if (selected_status == "4")
                {
                    entry_Status.SelectedIndex = 4;
                    TapGestureRecognizer_Tapped();
                    entry_Status.IsEnabled = true;
                    ngo_CandidateDetails.IsVisible = false;
                    Contacting_StackLayout.IsVisible = false;
                }

            }
            else //User Side
            {
                user_entry_Status.IsVisible = true;
                ngo_entry_Status.IsVisible = false;
                Submit_Button.IsVisible = false;
                AcceptanceProof_Button.IsVisible = false;
                ngo_CandidateDetails.IsVisible = false;
                //ngo_entry_Status.IsVisible = false;
                //user_entry_Status.IsVisible = true;
                //ngo_CandidateDetails.IsVisible = false;
                //entry_Status.IsEnabled = false;
                //Submit_Button.IsEnabled = false;
                //Submit_Button.IsVisible = false;
                //AcceptanceProof_Button.IsEnabled = false;
                //AcceptanceProof_Button.IsVisible = false;

                if (selected_status == "0")
                {
                    entry_Status.SelectedIndex = 0;
                    TapGestureRecognizer_Tapped();

                    Submit_Button.IsVisible = false;
                    AcceptanceProof_Button.IsVisible = false;
                    ngo_CandidateDetails.IsVisible = false;
                }
                else if (selected_status == "1")
                {
                    entry_Status.SelectedIndex = 1;
                    TapGestureRecognizer_Tapped();

                    Submit_Button.IsVisible = false;
                    AcceptanceProof_Button.IsVisible = false;
                    ngo_CandidateDetails.IsVisible = false;
                }
                else if (selected_status == "2")
                {
                    entry_Status.SelectedIndex = 2;
                    TapGestureRecognizer_Tapped();

                    Submit_Button.IsVisible = false;
                    AcceptanceProof_Button.IsVisible = false;
                    ngo_CandidateDetails.IsVisible = false;
                }
                else if (selected_status == "3")
                {
                    entry_Status.SelectedIndex = 3;
                    TapGestureRecognizer_Tapped();

                    Submit_Button.IsVisible = false;
                    AcceptanceProof_Button.IsVisible = false;
                    ngo_CandidateDetails.IsVisible = false;
                }
                else if (selected_status == "4")
                {
                    entry_Status.SelectedIndex = 4;
                    TapGestureRecognizer_Tapped();

                    Submit_Button.IsVisible = false;
                    AcceptanceProof_Button.IsVisible = false;
                    ngo_CandidateDetails.IsVisible = false;
                }
            }
        }

        void StatusChanged(object sender, System.EventArgs e)
        {
            if (entry_Status.SelectedIndex == 0) //Accepted
            {
                TapGestureRecognizer_Tapped();

                ngo_CandidateDetails.IsVisible = true;

                Denied_StackLayout.IsVisible = false;
                Contacting_StackLayout.IsVisible = false;

                Submit_Button.IsVisible = true;
                AcceptanceProof_Button.IsEnabled = true;
                AcceptanceProof_Button.IsVisible = true;
                Accepted_StackLayout.IsVisible = true;
            }
            else if (entry_Status.SelectedIndex == 1) //In Contact
            {
                
                TapGestureRecognizer_Tapped();

                ngo_CandidateDetails.IsVisible = true;

                Denied_StackLayout.IsVisible = false;
                Accepted_StackLayout.IsVisible = false;
                AcceptanceProof_Button.IsEnabled = false;
                AcceptanceProof_Button.IsVisible = false;
                Accepted_StackLayout.IsVisible = false;

                Submit_Button.IsVisible = true;
                Contacting_StackLayout.IsVisible = true;
            }
            else if (entry_Status.SelectedIndex == 2 || entry_Status.SelectedIndex == 3) //Denied or Blocked
            {
                TapGestureRecognizer_Tapped();

                ngo_CandidateDetails.IsVisible = true;

                Contacting_StackLayout.IsVisible = false;
                Accepted_StackLayout.IsVisible = false;
                AcceptanceProof_Button.IsEnabled = false;
                AcceptanceProof_Button.IsVisible = false;
                Accepted_StackLayout.IsVisible = false;

                Submit_Button.IsVisible = true;
                Denied_StackLayout.IsVisible = true;
            }
        }

        async void proofButton_Clicked(object sender, System.EventArgs e)
        {
            try
            {
                ICameraProvider cameraProvider = DependencyService.Get<ICameraProvider>();

                var myImage = await cameraProvider.AcquirePicture();

                StreamImageSource streamImageSource = (StreamImageSource)myImage;
                System.Threading.CancellationToken cancellationToken = System.Threading.CancellationToken.None;
                Task<Stream> task = streamImageSource.Stream(cancellationToken);
                Stream stream = task.Result;

                var memoryStream = new MemoryStream();
                stream.CopyTo(memoryStream);
                Console.WriteLine("Wrote The Stream" + memoryStream.ToArray().Length);

                base64String = DependencyService.Get<MyImageCompressor>().ImageCompressor(memoryStream.ToArray());
                Console.WriteLine("base64 String : " + base64String);

            }
            catch (Exception CameraException)
            {
                //No camera presenet or no access provided.So, instead of crashing, let's not get the data.
                Console.WriteLine("Exception CameraException : " + CameraException);
                await DisplayAlert("No Camera Found!", "I could not get access to your camera.", "Ok");
            }
        }

        async void SubmitButton_Clicked(object sender, EventArgs e)
        {
            //DisplayAlert("Submit", "Submit Clicked", "ok");
            if (SubmissionVerifier())
            {
                await EntryUpdater();

                if (updationStatus)
                {
                    await DisplayAlert("Success!", "Record Has Been Updated!", "Ok");
                    base.OnBackButtonPressed();
                }
                else
                {
                    await DisplayAlert("Failure!", "Failed To Update The Record!", "Ok");
                }
            }
        }

        bool SubmissionVerifier()
        {
            if (entry_Status.SelectedIndex == 0) //Accepted
            {
                if (string.IsNullOrEmpty(base64String))
                {
                    DisplayAlert("No Proof", "You Need To Upload A Proof Photograph.", "Ok");
                    return false;
                }
            }
            else if (entry_Status.SelectedIndex == 1) //In Contact
            {
                if (string.IsNullOrEmpty(estimatedTime.Text))
                {
                    DisplayAlert("Estimated Time", "You Need To Enter Estimated Time (In Weeks).", "Ok");
                    return false;
                }
            }
            else if (entry_Status.SelectedIndex == 2 || entry_Status.SelectedIndex == 3) //Denied or Blocked
            {
                if (string.IsNullOrEmpty(denied_Entry.Text))
                {
                    DisplayAlert("Reason Needed", "You Need To Provide A Reason For This Action.", "Ok");
                    return false;
                }
            }

            return true;
        }

        private async Task EntryUpdater()
        {
            try
            {
                if (entry_Status.SelectedIndex == 4)
                {
                    await DisplayAlert("Not Permitted", "Action Not Allowed", "Ok");
                    return;
                }
                else
                {
                    if (string.IsNullOrEmpty(base64String))
                        base64String = "NONE";

                    var values = new Dictionary<string, string>();
                    values.Add("beggar_id", selected_b_id);
                    values.Add("ngo_id", models.authority_loggedInAuthorityData.n_id);
                    values.Add("entry_status", entry_Status.SelectedIndex.ToString());
                    values.Add("accept_proof", string.Format(@base64String));
                    values.Add("entry_reason", denied_Entry.Text);
                    Console.WriteLine("entry reason: {0}",denied_Entry.Text);
                    values.Add("estimated_weeks", estimatedTime.Text);

                    var content = new FormUrlEncodedContent(values);
                    var responseObj = await _client.PostAsync(MainPage.primaryDomain + "/api/beggars/status_update.php", content);
                    var responseString = await responseObj.Content.ReadAsStringAsync();
                    var responseobject = JsonConvert.DeserializeObject(responseString);
                    string temp_response = responseobject.ToString();
                    Console.WriteLine("Temp Resposne: {0}", temp_response);
                    try
                    {
                        dynamic obj2 = Newtonsoft.Json.Linq.JObject.Parse(temp_response);
                        if (Convert.ToString(obj2.error_code) == "1")
                        {
                            this.errorMessage = obj2.message;
                            Console.WriteLine("Error message: {0}", errorMessage);
                            this.updationStatus = false; // Bad Input?
                        }
                        else
                        {
                            this.updationStatus = true; // Flag to tell the other methods about Sign up status
                        }

                    }
                    catch (Exception SubmitInner)
                    {
                        Console.WriteLine("Exception SubmitInner : " + SubmitInner);
                        await DisplayAlert("No Internet", "Could Not Connect To Internet", "Ok");
                    }
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