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
        private List<Schedule> schedules = new List<Schedule>();

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
                    // request from ProcedureAPI
                    var request1 = new HttpRequestMessage()
                    {
                        RequestUri = new Uri(Config.procedureApiUrl),
                        Method = HttpMethod.Get
                    };

                    // request from ScheduleAPI
                    var request2 = new HttpRequestMessage()
                    {
                        RequestUri = new Uri(Config.scheduleApiUrl),
                        Method = HttpMethod.Get
                    };

                    // Procedure
                    request1.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

                    // Schedule
                    request2.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

                    //Add Token
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Data.userToken);

                    //Grab json of token from server
                    HttpResponseMessage response1 = await client.SendAsync(request1);
                    HttpResponseMessage response2 = await client.SendAsync(request2);

                    var json1 = await response1.Content.ReadAsStringAsync();
                    var json2 = await response2.Content.ReadAsStringAsync();

                    // Debug.WriteLine(json2);

                    //Parse json data
                    dynamic ProcedureData = JArray.Parse(json1);
                    dynamic ScheduleData = JArray.Parse(json2);

                    // outer foreach START
                    foreach (JObject item in ScheduleData)
                    {
                        //Debug.WriteLine(item.ToString());

                        Schedule tempSchedule = new Schedule();
                        Procedure tempProcedure = new Procedure();

                        //Debug.WriteLine((string)item["patientId"]);
                        //Debug.WriteLine((int)item["procedureId"]);
                        //Debug.WriteLine((Boolean)item["isCompleted"]);
                        //Debug.WriteLine((DateTime)item["time"]);

                        tempSchedule.patientId = ((string)item["patientId"]);
                        tempSchedule.procedureId = ((int)item["procedureId"]);
                        tempSchedule.isCompleted = ((Boolean)item["isCompleted"]);
                        tempSchedule.procedureDate = (DateTime)item["time"];

                        // inner foreach START
                        foreach (JObject i in ProcedureData)
                        {
                            if ((int)i["procedureId"] == (int)item["procedureId"])
                            {
                                tempProcedure.setProcedureId((int)i["procedureId"]);
                                tempProcedure.setName((string)i["name"]);
                                tempProcedure.setDescription((string)i["description"]);
                                tempSchedule.prodecure = tempProcedure;
                            }
                        }// inner foreach END

                        if (tempSchedule.isCompleted == false)
                        {
                            // Add schedule to the List<Schedule>
                            schedules.Add(tempSchedule);
                        }else
                        {
                            TextBlock textBlock = new TextBlock();
                            textBlock.Text = "No schedules are set up yet...";
                            textBlock.FontFamily = new FontFamily("Verdana");
                            textBlock.FontSize = 28;
                            textBlock.FontWeight = Windows.UI.Text.FontWeights.Bold;
                            textBlock.FontStyle = Windows.UI.Text.FontStyle.Italic;
                            textBlock.CharacterSpacing = 200;
                            //textBlock.Foreground = new SolidColorBrush(Windows.UI.Colors.Blue);

                            // Add the TextBox to the visual tree.
                            mainGrid.Children.Add(textBlock);
                        }
                        

                        scheduleView.ItemsSource = schedules;

                    }// outer foreach END

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex);
            }

        }

        private void MedicationButton_Click(object sender, RoutedEventArgs e)
        {
            Schedule obj = ((FrameworkElement)sender).DataContext as Schedule;
            Debug.WriteLine(obj.prodecure.procedureId);
            this.Frame.Navigate(typeof(MedicationPage));
        }

        private void PreInstructionButton_Click(object sender, RoutedEventArgs e)
        {
            Schedule obj = ((FrameworkElement)sender).DataContext as Schedule;
            Debug.WriteLine(obj.prodecure.procedureId);
            this.Frame.Navigate(typeof(PreInstructionPage));
        }

        private void SurveyButton_Click(object sender, RoutedEventArgs e)
        {
            Schedule obj = ((FrameworkElement)sender).DataContext as Schedule;
            Debug.WriteLine(obj.prodecure.procedureId);
        }

        private void SubProcedureButton_Click(object sender, RoutedEventArgs e)
        {
            Schedule obj = ((FrameworkElement)sender).DataContext as Schedule;
            Debug.WriteLine(obj.prodecure.procedureId);
        }
    }
}
