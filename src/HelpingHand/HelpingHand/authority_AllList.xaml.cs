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
	public partial class authority_AllList : ContentPage
	{
        private HttpClient _client = new HttpClient();
        public ObservableCollection<string> Items { get; set; }
        private ObservableCollection<models.AllEntriesList> _allEntriesList_Private = new ObservableCollection<models.AllEntriesList> { };

        //protected override async void OnAppearing()
        //{
        //    authorityAllEntries_ListView.IsRefreshing = true;
        //    await AllListFetching();
        //    authorityAllEntries_ListView.ItemsSource = _allEntriesList_Private;
        //    authorityAllEntries_ListView.EndRefresh();
        //}

        public authority_AllList()
        {
            InitializeComponent();
            AllListFetching();
            authorityAllEntries_ListView.ItemsSource = _allEntriesList_Private;
        }

        private void authorityAllEntries_Refreshing(object sender, EventArgs e)
        {
            authorityAllEntries_ListView.ItemsSource = _allEntriesList_Private;
            authorityAllEntries_ListView.EndRefresh();
        }

        private async void authorityAllEntries_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            var entrySelected = e.SelectedItem as models.AllEntriesList;
            await Navigation.PushModalAsync(new DetailsOfEntry(entrySelected.b_id, entrySelected.b_state, entrySelected.b_area_code, entrySelected.b_country, entrySelected.b_age, entrySelected.b_family_count, entrySelected.b_street_address, entrySelected.b_status, entrySelected.b_ngo, entrySelected.b_creation_date, entrySelected.b_photo, "ngo"));
            authorityAllEntries_ListView.SelectedItem = null;
        }

        public async Task AllListFetching()
        {
            try
            {

                var content = await _client.GetAsync(MainPage.primaryDomain + "/api/beggars/list_of_entries.php");

                var temp_response = await content.Content.ReadAsStringAsync();

                try
                {
                    dynamic dynObj = JsonConvert.DeserializeObject(Convert.ToString(temp_response));
                    Console.WriteLine("Records : " + Convert.ToString(dynObj));
                    _allEntriesList_Private.Clear();

                    foreach (var data in dynObj)
                    {
                        if (Convert.ToString(data.b_status) != "3")
                        {
                            if (Convert.ToString(data.b_id) != "0")
                            {
                                _allEntriesList_Private.Add(new models.AllEntriesList
                                {
                                    b_id = Convert.ToString(data.b_id),
                                    b_state = Convert.ToString(data.b_state),
                                    b_country = Convert.ToString(data.b_country),
                                    b_area_code = Convert.ToString(data.b_area_code),
                                    b_age = Convert.ToString(data.b_age),
                                    b_family_count = Convert.ToString(data.b_family_count),
                                    b_street_address = Convert.ToString(data.b_street_address),
                                    b_status = Convert.ToString(data.b_status),
                                    b_status_text = GlobalMethods.numberToStatus(Convert.ToString(data.b_status)),
                                    b_ngo = Convert.ToString(data.b_ngo),
                                    b_creation_date = Convert.ToString(data.b_creation_date),
                                    b_photo = Convert.ToString(data.b_photo)
                                });
                            }
                        }                            
                    }

                    //Reverse the Collection to show latest on top
                    // Reverse() returns an IEnumerable, it does not modify the collection. So, we iterate through it.
                    for (int i = 0; i < _allEntriesList_Private.Count; i++)
                        _allEntriesList_Private.Move(_allEntriesList_Private.Count - 1, i);

                    //foreach (var item in _allEntriesList_Private)
                    //{
                    //    Console.WriteLine("Current Item : " + Convert.ToString(item));
                    //}

                }
                catch (Exception)
                {
                    await DisplayAlert("Bad Request", "Wrong User ID", "Ok");
                }

            }
            catch (Exception)
            {
                await DisplayAlert("Failed", "Could Not Connect To Servers.", "Ok");
            }

        }
    }
}