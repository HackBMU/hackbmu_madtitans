using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HelpingHand
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class users_PastEntries : ContentPage
	{
        private HttpClient _client = new HttpClient();
        public ObservableCollection<string> Items { get; set; }
        private ObservableCollection<models.user_PastEntries> _pastEntriesList_Private = new ObservableCollection<models.user_PastEntries> { };

        protected override async void OnAppearing()
        {

            usersPastEntries_ListView.IsRefreshing = true;
            await PastListFetching();
            usersPastEntries_ListView.ItemsSource = _pastEntriesList_Private;
            usersPastEntries_ListView.EndRefresh();

        }

        public users_PastEntries ()
		{
			InitializeComponent ();
            ////PastListFetching();
            //usersPastEntries_ListView.ItemsSource = _pastEntriesList_Private;
        }

        private async void usersPastEntries_Refreshing(object sender, EventArgs e)
        {
            await PastListFetching();
            usersPastEntries_ListView.ItemsSource = _pastEntriesList_Private;
            usersPastEntries_ListView.EndRefresh();
        }

        private async void usersPastEntries_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            var entrySelected = e.SelectedItem as models.user_PastEntries;
            var pageDetails = new NavigationPage(new DetailsOfEntry(entrySelected.b_id,
                "", "", "",
                entrySelected.b_age, entrySelected.b_family_count, entrySelected.b_street_address,
                entrySelected.b_status, entrySelected.b_ngo_name, "",
                entrySelected.b_photo, "user"));
            await Navigation.PushModalAsync(pageDetails);
            //await Navigation.PushModalAsync(new DetailsOfEntry(entrySelected.b_id,
            //    "", "", "",
            //    entrySelected.b_age, entrySelected.b_family_count, entrySelected.b_street_address,
            //    entrySelected.b_status, entrySelected.b_ngo_name, "",
            //    entrySelected.b_photo, "user"));
            await PastListFetching();
            usersPastEntries_ListView.ItemsSource = _pastEntriesList_Private;
            usersPastEntries_ListView.EndRefresh();
            usersPastEntries_ListView.SelectedItem = null;
        }

        private async void newRequest_Clicked(object sender, EventArgs e)
        {
            var page = new NavigationPage(new NewEntry());
            await Navigation.PushModalAsync(page);
            //await Navigation.PushAsync(new NewEntry());
            //await Navigation.PushModalAsync
            await PastListFetching();
            usersPastEntries_ListView.ItemsSource = _pastEntriesList_Private;
            usersPastEntries_ListView.EndRefresh();
        }

        public async Task PastListFetching()
        {
            try
            {
                var data_to_send = new FormUrlEncodedContent(new[]
                        {
                        new KeyValuePair<string, string>("user_id", models.user_LoggedInUserData.u_id)
                    });

                var content = await _client.PostAsync(MainPage.primaryDomain + "/api/beggars/entries_by_a_user.php", data_to_send);

                var temp_response = await content.Content.ReadAsStringAsync();

                dynamic obj2 = Newtonsoft.Json.Linq.JObject.Parse(temp_response);
                if (Convert.ToString(obj2.error_code) != "1")
                {
                    try
                    {
                        dynamic dynObj = JsonConvert.DeserializeObject(Convert.ToString(obj2.records));
                        Console.WriteLine("Records : " + Convert.ToString(dynObj));
                        _pastEntriesList_Private.Clear();

                        foreach (var data in dynObj)
                        {
                            if (Convert.ToString(data.b_status) != "3")
                            {
                                if (Convert.ToString(data.b_id) != "0")
                                {
                                        _pastEntriesList_Private.Add(new models.user_PastEntries
                                        {
                                            b_id = Convert.ToString(data.b_id),
                                            b_status = Convert.ToString(data.b_status),
                                            b_status_text = GlobalMethods.numberToStatus(Convert.ToString(data.b_status)),
                                            b_photo = Convert.ToString(data.b_photo),
                                            b_street_address = Convert.ToString(data.b_street_address),
                                            b_age = Convert.ToString(data.b_age),
                                            b_extr_info = Convert.ToString(data.b_extr_info),
                                            b_family_count = Convert.ToString(data.b_family_count),
                                            b_ngo_name = Convert.ToString(data.b_ngo_name),
                                            b_reason = Convert.ToString(data.b_reason)
                                        });
                                    }
                            }
                        }

                        //Reverse the Collection to show latest on top
                        // Reverse() returns an IEnumerable, it does not modify the collection. So, we iterate through it.
                        for (int i = 0; i < _pastEntriesList_Private.Count; i++)
                            _pastEntriesList_Private.Move(_pastEntriesList_Private.Count - 1, i);

                    }
                    catch (Exception BadUserID)
                    {
                        await DisplayAlert("Bad Request", "Wrong User ID", "Ok");
                    }
                }
                
            }
            catch (Exception)
            {
                await DisplayAlert("Failed", "Could Not Connect To Servers.", "Ok");
            }

        }
    }
}