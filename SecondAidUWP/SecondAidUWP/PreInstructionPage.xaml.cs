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
    public sealed partial class PreInstructionPage : Page
    {
        private List<PreInstruction> PreInstructionList = new List<PreInstruction>();
        public PreInstructionPage()
        {
            this.InitializeComponent();
            getPreInstruction();
            UpdateLayout();
        }
        public async void getPreInstruction()
        {
            try
            {
                using (var client = new HttpClient())
                {

                    var request = new HttpRequestMessage()
                    {
                        RequestUri = new Uri(Config.preInstructionApiUrl),
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

                    foreach (JObject item in data)
                    {
                        PreInstruction newPreInstruction = new PreInstruction();
                        newPreInstruction.setPreInstructionId((int)item["preInstructionId"]);
                        newPreInstruction.setTitle((string)item["title"]);
                        newPreInstruction.setDescription((string)item["description"]);
                        PreInstructionList.Add(newPreInstruction);
                    }
                    preInstructionView.ItemsSource = PreInstructionList;
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
