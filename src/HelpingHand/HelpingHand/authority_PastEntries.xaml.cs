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
	public partial class authority_PastEntries : ContentPage
	{
        private HttpClient _client = new HttpClient();
        public ObservableCollection<string> Items { get; set; }
        private ObservableCollection<models.user_PastEntries> _pastEntriesList_Private = new ObservableCollection<models.user_PastEntries> { };

        //IEnumerable<models.SingleEntryStructure> authority_GetPastEntries(string searchText = null)
        //{
        //    var comicList = new List<models.SingleEntryStructure>
        //    {
        //        new models.SingleEntryStructure { e_id = "1", e_status = "0", e_photo = "http://cdn.mangaeden.com/mangasimg/05/05d5df58e440371496b217f94cc4894abeba1671bd9edf0e7cd774a1.jpg", e_address = "Definitely not home"},
        //        new models.SingleEntryStructure { e_id = "2", e_status = "2", e_photo = "http://cdn.mangaeden.com/mangasimg/05/05d5df58e440371496b217f94cc4894abeba1671bd9edf0e7cd774a1.jpg", e_address = "Definitely not home"},
        //        new models.SingleEntryStructure { e_id = "3", e_status = "4", e_photo = "http://cdn.mangaeden.com/mangasimg/05/05d5df58e440371496b217f94cc4894abeba1671bd9edf0e7cd774a1.jpg", e_address = "Definitely not home"},
        //        new models.SingleEntryStructure { e_id = "4", e_status = "1", e_photo = "http://cdn.mangaeden.com/mangasimg/05/05d5df58e440371496b217f94cc4894abeba1671bd9edf0e7cd774a1.jpg", e_address = "Definitely not home"}
        //    };
        //    if (String.IsNullOrWhiteSpace(searchText))
        //        return comicList;
        //    return comicList.Where(c => c.e_id.ToLower().StartsWith(searchText.ToLower()));
        //}

        protected override async void OnAppearing()
        {
            authorityPastEntries_ListView.IsRefreshing = true;
            await PastListFetching();
            authorityPastEntries_ListView.ItemsSource = _pastEntriesList_Private;
            authorityPastEntries_ListView.EndRefresh();
        }

        public authority_PastEntries()
        {
            InitializeComponent();
            ////PastListFetching();
            //authorityPastEntries_ListView.ItemsSource = _pastEntriesList_Private;
        }

        private async void authorityPastEntries_Refreshing(object sender, EventArgs e)
        {
            await PastListFetching();
            authorityPastEntries_ListView.ItemsSource = _pastEntriesList_Private;
            authorityPastEntries_ListView.EndRefresh();
        }

        private async void authorityPastEntries_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            var entrySelected = e.SelectedItem as models.user_PastEntries;
            await Navigation.PushModalAsync(new DetailsOfEntry(entrySelected.b_id,
                "", "", "",
                entrySelected.b_age, entrySelected.b_family_count, entrySelected.b_street_address,
                entrySelected.b_status, entrySelected.b_ngo_name, "",
                entrySelected.b_photo, "ngo"));
            authorityPastEntries_ListView.SelectedItem = null;
            await PastListFetching();
            authorityPastEntries_ListView.ItemsSource = _pastEntriesList_Private;
            authorityPastEntries_ListView.EndRefresh();
        }

        public async Task PastListFetching()
        {
            try
            {
                var data_to_send = new FormUrlEncodedContent(new[]
                        {
                        new KeyValuePair<string, string>("ngo_id", models.authority_loggedInAuthorityData.n_id)
                    });

                var content = await _client.PostAsync(MainPage.primaryDomain + "/api/beggars/entries_by_a_ngo.php", data_to_send);

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
                            if (data.b_status != "3")
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

                        //Reverse the Collection to show latest on top
                        // Reverse() returns an IEnumerable, it does not modify the collection. So, we iterate through it.
                        for (int i = 0; i < _pastEntriesList_Private.Count; i++)
                            _pastEntriesList_Private.Move(_pastEntriesList_Private.Count - 1, i);

                        //foreach (var item in _pastEntriesList_Private)
                        //{
                        //    Console.WriteLine("Current Item : " + Convert.ToString(item));
                        //}

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