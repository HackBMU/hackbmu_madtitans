﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HelpingHand
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class authority_PastEntries : ContentPage
	{
        public ObservableCollection<string> Items { get; set; }

        IEnumerable<models.SingleEntryStructure> authority_GetPastEntries(string searchText = null)
        {
            var comicList = new List<models.SingleEntryStructure>
            {
                new models.SingleEntryStructure { e_id = "1", e_status = "0", e_photo = "http://cdn.mangaeden.com/mangasimg/05/05d5df58e440371496b217f94cc4894abeba1671bd9edf0e7cd774a1.jpg", e_address = "Definitely not home"},
                new models.SingleEntryStructure { e_id = "2", e_status = "2", e_photo = "http://cdn.mangaeden.com/mangasimg/05/05d5df58e440371496b217f94cc4894abeba1671bd9edf0e7cd774a1.jpg", e_address = "Definitely not home"},
                new models.SingleEntryStructure { e_id = "3", e_status = "4", e_photo = "http://cdn.mangaeden.com/mangasimg/05/05d5df58e440371496b217f94cc4894abeba1671bd9edf0e7cd774a1.jpg", e_address = "Definitely not home"},
                new models.SingleEntryStructure { e_id = "4", e_status = "1", e_photo = "http://cdn.mangaeden.com/mangasimg/05/05d5df58e440371496b217f94cc4894abeba1671bd9edf0e7cd774a1.jpg", e_address = "Definitely not home"}
            };
            if (String.IsNullOrWhiteSpace(searchText))
                return comicList;
            return comicList.Where(c => c.e_id.ToLower().StartsWith(searchText.ToLower()));
        }

        public authority_PastEntries()
        {
            InitializeComponent();
            authorityPastEntries_ListView.ItemsSource = authority_GetPastEntries();
        }

        private void authorityPastEntries_Refreshing(object sender, EventArgs e)
        {
            authorityPastEntries_ListView.ItemsSource = authority_GetPastEntries();
            authorityPastEntries_ListView.EndRefresh();
        }

        private async void authorityPastEntries_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            var entrySelected = e.SelectedItem as models.SingleEntryStructure;
            //await Navigation.PushAsync(new ComicDetailsPage(comicSelected, comicSelected.CoverImageUrl));
            await DisplayAlert("Item Selected", entrySelected.e_id, "ok");
            authorityPastEntries_ListView.SelectedItem = null;

        }
    }
}