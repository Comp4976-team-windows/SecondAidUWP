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
using SecondAidUWP.Model;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SecondAidUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ProcedureListPage : Page
    {
        private int numberOfProcedures;
        private List<Procedure> Procedures = new List<Procedure>();

        public ProcedureListPage()
        {
            this.InitializeComponent();
            getProcedures();
            UpdateLayout();
        }

        public async void getProcedures()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var request = new HttpRequestMessage()
                    {
                        RequestUri = new Uri(Config.procedureApiUrl),
                        Method = HttpMethod.Get
                    };
                    request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

                    //Add Token
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Data.userToken);

                    //Grab json of token from server
                    HttpResponseMessage response = await client.SendAsync(request);
                    var json = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine(json);

                    //Parse json data
                    dynamic data = JArray.Parse(json);

                    foreach (JObject item in data)
                    {
                        //Debug.WriteLine(item.ToString());
                        //numberOfProcedures += 1;

                        
                        Procedure tempProcedure = new Procedure();
                        tempProcedure.setProcedureId((int)item["procedureId"]);
                        tempProcedure.setName((string)item["name"]);
                        tempProcedure.setDescription((string)item["description"]);

                        //Data.procedures.Add(tempProcedure);
                        

                        Procedures.Add(tempProcedure);

                        /*foreach (Procedure i in Procedures)
                        {
                            Debug.WriteLine(i.procedureId);
                            Debug.WriteLine(i.name);
                            Debug.WriteLine(i.description);
                        }*/
                        
                    }
                    procedureView.ItemsSource = Procedures;
                    /*
                    TextBlock[] procedureId = new TextBlock[numberOfProcedures];
                    TextBlock[] name = new TextBlock[numberOfProcedures];
                    TextBlock[] description = new TextBlock[numberOfProcedures];
                    int i = 0;
                    int gridCount = 1;
                    */

                    //RowDefinition rowDef = new RowDefinition();
                    //rowDef.Height = new GridLength(100);











                    /*
                    foreach (JObject item in data)
                    {

                        mainGrid.RowDefinitions.Add(new RowDefinition());

                        procedureId[i] = new TextBlock();
                        procedureId[i].Text = (string)item["procedureId"];

                        name[i] = new TextBlock();
                        name[i].Text = (string)item["name"];

                        description[i] = new TextBlock();
                        description[i].Text = (string)item["description"];

                        Grid.SetRow(procedureId[i], gridCount++);
                        mainGrid.Children.Add(procedureId[i]);
                        Grid.SetRow(name[i], gridCount++);
                        mainGrid.Children.Add(name[i]);
                        Grid.SetRow(description[i], gridCount++);
                        mainGrid.Children.Add(description[i]);

                        i++;
                    }
                    */

                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex);
            }

        }

        private void MedicationButton_Click(object sender, RoutedEventArgs e)
        {
            Procedure obj = ((FrameworkElement)sender).DataContext as Procedure;
            Debug.WriteLine(obj.procedureId);
        }

        private void PreInstructionButton_Click(object sender, RoutedEventArgs e)
        {
            Procedure obj = ((FrameworkElement)sender).DataContext as Procedure;
            Debug.WriteLine(obj.procedureId);
        }

        private void SurveyButton_Click(object sender, RoutedEventArgs e)
        {
            Procedure obj = ((FrameworkElement)sender).DataContext as Procedure;
            Debug.WriteLine(obj.procedureId);
        }
    }
}
