using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Microcharts.Forms;
using Entry = Microcharts.Entry;
using SkiaSharp;
using Microcharts;

namespace HelpingHand
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Insights : ContentPage
    {
        private HttpClient _client = new HttpClient();
        private string errorMessage;
        private bool fetchStatus;
        private int range_1, range_2, range_3, range_4, range_5, range_6, range_7, range_8, range_9, range_10, range_11, total_range_country;

        public Insights()
        {
            InitializeComponent();
        }

        async void CountrySelected(object sender, EventArgs e)
        {
            if (InsightCountry_Picker.SelectedIndex == 0)
            {
                return;
            }
            else
            {
                if (Insight_Picker.SelectedIndex == 2)
                {
                    // If we have stats of a country selected, rather than the blood group. We need this.
                    mainWebView.Source = "http://40.71.17.14/api/web_views/country.php?country=" + (string) InsightCountry_Picker.SelectedItem;
                    return;
                }
                else if (Insight_Picker.SelectedIndex == 4)
                {
                    await Country_AgeGroupData_Fetcher();
                    // All the entries are in this!
                    var entries = new[]
                    {
                        new Entry(range_1)
                        {
                            Label = "1-10",
                            ValueLabel = Convert.ToString(range_1),
                            Color = SKColor.Parse("#266489")
                        },
                        new Entry(range_2)
                        {
                            Label = "11-20",
                            ValueLabel = Convert.ToString(range_2),
                            Color = SKColor.Parse("#266489")
                        },
                        new Entry(range_3)
                        {
                            Label = "21-30",
                            ValueLabel = Convert.ToString(range_3),
                            Color = SKColor.Parse("#68B9C0")
                        },
                        new Entry(range_4)
                        {
                            Label = "31-40",
                            ValueLabel = Convert.ToString(range_4),
                            Color = SKColor.Parse("#90D585")
                        },
                        new Entry(range_5)
                        {
                            Label = "41-50",
                            ValueLabel = Convert.ToString(range_5),
                            Color = SKColor.Parse("#90D585")
                        },
                        new Entry(range_6)
                        {
                            Label = "51-60",
                            ValueLabel = Convert.ToString(range_6),
                            Color = SKColor.Parse("#266489")
                        },
                        new Entry(range_7)
                        {
                            Label = "61-70",
                            ValueLabel = Convert.ToString(range_7),
                            Color = SKColor.Parse("#266489")
                        },
                        new Entry(range_8)
                        {
                            Label = "71-80",
                            ValueLabel = Convert.ToString(range_8),
                            Color = SKColor.Parse("#68B9C0")
                        },
                        new Entry(range_9)
                        {
                            Label = "81-90",
                            ValueLabel = Convert.ToString(range_9),
                            Color = SKColor.Parse("#90D585")
                        },
                        new Entry(range_10)
                        {
                            Label = "91-100",
                            ValueLabel = Convert.ToString(range_10),
                            Color = SKColor.Parse("#90D585")
                        },
                        new Entry(range_11)
                        {
                            Label = "101-110",
                            ValueLabel = Convert.ToString(range_11),
                            Color = SKColor.Parse("#90D585")
                        }
                    };
                    var chart = new BarChart() { Entries = entries };
                    chartView.Chart = chart;
                    chartView.IsVisible = true;
                }
            }
        }

        async void InsightSelected(object sender, EventArgs e)
        {
            mainWebView.IsVisible = true;
            InsightCountry_Picker.IsVisible = false;


            if (Insight_Picker.SelectedIndex == 0)
            {
                mainWebView.IsVisible = false;
                chartView.IsVisible = false;
                return;
            }
            else if (Insight_Picker.SelectedIndex == 1)
            {
                chartView.IsVisible = false;
                mainWebView.Source = "http://40.71.17.14/api/web_views/world.php";
                return;
            }
            else if (Insight_Picker.SelectedIndex == 2)
            {
                chartView.IsVisible = false;
                await CountryListFetcher();
                InsightCountry_Picker.IsVisible = true;
                return;
            }
            else if (Insight_Picker.SelectedIndex == 3)
            {
                mainWebView.IsVisible = false;
                await World_AgeGroupData_Fetcher();
                // All the entries are in this!
                var entries = new[]
                {
                        new Entry(range_1)
                        {
                            Label = "1-10",
                            ValueLabel = Convert.ToString(range_1),
                            Color = SKColor.Parse("#266489")
                        },
                        new Entry(range_2)
                        {
                            Label = "11-20",
                            ValueLabel = Convert.ToString(range_2),
                            Color = SKColor.Parse("#266489")
                        },
                        new Entry(range_3)
                        {
                            Label = "21-30",
                            ValueLabel = Convert.ToString(range_3),
                            Color = SKColor.Parse("#68B9C0")
                        },
                        new Entry(range_4)
                        {
                            Label = "31-40",
                            ValueLabel = Convert.ToString(range_4),
                            Color = SKColor.Parse("#90D585")
                        },
                        new Entry(range_5)
                        {
                            Label = "41-50",
                            ValueLabel = Convert.ToString(range_5),
                            Color = SKColor.Parse("#90D585")
                        },
                        new Entry(range_6)
                        {
                            Label = "51-60",
                            ValueLabel = Convert.ToString(range_6),
                            Color = SKColor.Parse("#266489")
                        },
                        new Entry(range_7)
                        {
                            Label = "61-70",
                            ValueLabel = Convert.ToString(range_7),
                            Color = SKColor.Parse("#266489")
                        },
                        new Entry(range_8)
                        {
                            Label = "71-80",
                            ValueLabel = Convert.ToString(range_8),
                            Color = SKColor.Parse("#68B9C0")
                        },
                        new Entry(range_9)
                        {
                            Label = "81-90",
                            ValueLabel = Convert.ToString(range_9),
                            Color = SKColor.Parse("#90D585")
                        },
                        new Entry(range_10)
                        {
                            Label = "91-100",
                            ValueLabel = Convert.ToString(range_10),
                            Color = SKColor.Parse("#90D585")
                        },
                        new Entry(range_11)
                        {
                            Label = "101-110",
                            ValueLabel = Convert.ToString(range_11),
                            Color = SKColor.Parse("#90D585")
                        }
                    };
                var chart = new BarChart() { Entries = entries };
                chartView.Chart = chart;
                chartView.IsVisible = true;
            }
            else if (Insight_Picker.SelectedIndex == 4)
            {
                mainWebView.IsVisible = false;
                await CountryListFetcher();
                InsightCountry_Picker.IsVisible = true;

            }
        }

        public async Task Country_AgeGroupData_Fetcher()
        {
            try
            {

                var content = await _client.GetAsync(MainPage.primaryDomain + "/api/insights/country_age_group.php?country=" + InsightCountry_Picker.SelectedItem.ToString());

                var temp_response = await content.Content.ReadAsStringAsync();
                Console.WriteLine("temp_response Age Group : " + temp_response);


                dynamic obj2 = Newtonsoft.Json.Linq.JObject.Parse(temp_response);
                if (Convert.ToString(obj2.error_code) == "1")
                {
                    this.errorMessage = obj2.message;
                    this.fetchStatus = false;
                }
                else
                {
                    try
                    {
                        dynamic dynObj = JsonConvert.DeserializeObject(Convert.ToString(obj2.records));
                        
                        foreach (var data in dynObj)
                        {
                            range_1 = (int) data.range_1;
                            range_2 = (int) data.range_2;
                            range_3 = (int) data.range_3;
                            range_4 = (int) data.range_4;
                            range_5 = (int) data.range_5;
                            range_6 = (int) data.range_6;
                            range_7 = (int) data.range_7;
                            range_8 = (int) data.range_8;
                            range_9 = (int) data.range_9;
                            range_10 = (int) data.range_10;
                            range_11 = (int) data.range_11;
                        }

                        total_range_country = range_1 + range_2 + range_3 + range_4 + range_5;
                    }
                    catch (Exception RecordTraverseException)
                    {
                        Console.WriteLine("RecordTraverseException : " + RecordTraverseException);
                        throw;
                    }
                }
                    
            }

	        catch (Exception AgeGroupTraverseException)
	        {
                Console.WriteLine("RecordTraverseException : " + AgeGroupTraverseException);
                throw;
	        }

        }

        public async Task World_AgeGroupData_Fetcher()
        {
            try
            {

                var content = await _client.GetAsync(MainPage.primaryDomain + "/api/insights/world_age_group.php");

                var temp_response = await content.Content.ReadAsStringAsync();


                dynamic obj2 = Newtonsoft.Json.Linq.JObject.Parse(temp_response);
                if (Convert.ToString(obj2.error_code) == "1")
                {
                    this.errorMessage = obj2.message;
                    this.fetchStatus = false;
                }
                else
                {
                    try
                    {
                        dynamic dynObj = JsonConvert.DeserializeObject(Convert.ToString(obj2.records));

                        foreach (var data in dynObj)
                        {
                            range_1 = (int)data.range_1;
                            range_2 = (int)data.range_2;
                            range_3 = (int)data.range_3;
                            range_4 = (int)data.range_4;
                            range_5 = (int)data.range_5;
                            range_6 = (int)data.range_6;
                            range_7 = (int)data.range_7;
                            range_8 = (int)data.range_8;
                            range_9 = (int)data.range_9;
                            range_10 = (int)data.range_10;
                            range_11 = (int)data.range_11;
                        }

                        total_range_country = range_1 + range_2 + range_3 + range_4 + range_5;
                    }
                    catch (Exception RecordTraverseException)
                    {
                        Console.WriteLine("RecordTraverseException : " + RecordTraverseException);
                        throw;
                    }
                }

            }

            catch (Exception AgeGroupTraverseException)
            {
                Console.WriteLine("RecordTraverseException : " + AgeGroupTraverseException);
                throw;
            }

        }

        private async Task CountryListFetcher()
        {
            Console.WriteLine("Function Called");
            try
            {
                var content = await _client.GetAsync(MainPage.primaryDomain + "/api/misc/list_of_countries.php");

                var temp_response = await content.Content.ReadAsStringAsync();

                try
                {
                    dynamic obj2 = Newtonsoft.Json.Linq.JObject.Parse(temp_response);
                    if (Convert.ToString(obj2.error_code) == "1")
                    {
                        this.errorMessage = obj2.message;
                        this.fetchStatus = false;
                    }
                    else
                    {
                        this.fetchStatus = true; // Flag to tell the other methods about Sign up status

                        try
                        {
                            dynamic dynObj = JsonConvert.DeserializeObject(Convert.ToString(obj2.records));
                            InsightCountry_Picker.Items.Clear();
                            InsightCountry_Picker.Items.Add("Select A Country");
                            foreach (var data in dynObj)
                            {
                                InsightCountry_Picker.Items.Add((string) data.country_name);

                            }

                        }
                        catch (Exception BadUserID)
                        {
                            Console.WriteLine("Exception BadUserID Country : " + BadUserID);
                            await DisplayAlert("Bad Request", "Wrong User ID", "Ok");
                        }
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