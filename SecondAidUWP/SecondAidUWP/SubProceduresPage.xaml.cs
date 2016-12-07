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
    public sealed partial class SubProceduresPage : Page
    {
        SubProcedure subProcedure = new SubProcedure();
        private List<SubProcedure> subProcedureList = new List<SubProcedure>();

        public SubProceduresPage()
        {
            this.InitializeComponent();
            getSubProcedures();
            UpdateLayout();
        }

        public async void getSubProcedures()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var subProcedureRequest = new HttpRequestMessage()
                    {
                        RequestUri = new Uri(Config.subProceduresApiUrl),
                        Method = HttpMethod.Get
                    };

                    // SubProcedures
                    subProcedureRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

                    //Add Token
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Data.userToken);

                    //Grab json of token from server
                    HttpResponseMessage response = await client.SendAsync(subProcedureRequest);

                    var json = await response.Content.ReadAsStringAsync();

                    dynamic SubProcedureData = JArray.Parse(json);

                    foreach (JObject sp in SubProcedureData)
                    {
                        if ((int)sp["procedureId"] == Data.procedureId)
                        {
                            subProcedure.subProcedureId = (int)sp["subProcedureId"];
                            subProcedure.name = (string)sp["name"];
                            subProcedure.description = (string)sp["description"];
                            subProcedure.procedureId = (int)sp["procedureId"];
                            subProcedureList.Add(subProcedure);
                        }
                    }

                    subProcedureView.ItemsSource = subProcedureList;
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
        private void PreInstructionButton_Click(object sender, RoutedEventArgs e)
        {
            SubProcedure obj = ((FrameworkElement)sender).DataContext as SubProcedure;
            Data.subProcedureId = obj.subProcedureId;
            this.Frame.Navigate(typeof(PreInstructionPage));
        }

        private void QuestionnaireButton_Click(object sender, RoutedEventArgs e)
        {
            SubProcedure obj = ((FrameworkElement)sender).DataContext as SubProcedure;
            Data.subProcedureId = obj.subProcedureId;
            this.Frame.Navigate(typeof(QuestionListPage));
        }
    }
}
