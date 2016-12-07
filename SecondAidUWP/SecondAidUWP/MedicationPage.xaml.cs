using Newtonsoft.Json.Linq;
using SecondAidUWP.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SecondAidUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MedicationPage : Page
    {
        private List<Medication> MedicationList = new List<Medication>();

        public MedicationPage()
        {
            this.InitializeComponent();
            getMedication();
            UpdateLayout();
        }

        public async void getMedication()
        {
            try
            {
                using (var client = new HttpClient())
                {

                    var request = new HttpRequestMessage()
                    {
                        RequestUri = new Uri(Config.medicationApiUrl),
                        Method = HttpMethod.Get
                    };
                    request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Data.userToken);

                    //Grab json from server
                    HttpResponseMessage response = await client.SendAsync(request);
                    var json = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine(json);

                    //Parse json data
                    dynamic data = JArray.Parse(json);

                    var request2 = new HttpRequestMessage()
                    {
                        RequestUri = new Uri(Config.medicationInstructionApiUrl),
                        Method = HttpMethod.Get
                    };
                    request2.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

                    //Grab json from server
                    HttpResponseMessage response2 = await client.SendAsync(request2);
                    var json2 = await response2.Content.ReadAsStringAsync();
                    Debug.WriteLine(json2);

                    //Parse json data
                    dynamic data2 = JArray.Parse(json2);

                    foreach (JObject item in data)
                    {
                        Medication newMed = new Medication();
                        newMed.setMedicationId((int)item["medicationId"]);
                        newMed.setName((string)item["name"]);
                        newMed.setDescription((string)item["description"]);
                            foreach (JObject stuff in data2)
                            {
                                if (((int)stuff["medicationId"] == newMed.getMedicationId()))
                                {
                                    newMed.setMedInstruction((string)stuff["instruction"]);
                                string test = newMed.getMedInstruction();
                                Debug.WriteLine(test);
                            }
                            }
                            MedicationList.Add(newMed);
                        }
                        medicationView.ItemsSource = MedicationList;
                    }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex);
            }

        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ProcedureListPage));
        }
    }
}
