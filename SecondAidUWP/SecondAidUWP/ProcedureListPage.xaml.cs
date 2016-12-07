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
        Clinic clinic = new Clinic();

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

                    // request from ClinicAPI
                    var request3 = new HttpRequestMessage()
                    {
                        RequestUri = new Uri(Config.clinicApiUrl),
                        Method = HttpMethod.Get
                    };

                    // Procedure
                    request1.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

                    // Schedule
                    request2.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

                    // Clinic
                    request3.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

                    //Add Token
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Data.userToken);

                    //Grab json of token from server
                    HttpResponseMessage response1 = await client.SendAsync(request1);
                    HttpResponseMessage response2 = await client.SendAsync(request2);
                    HttpResponseMessage response3 = await client.SendAsync(request3);

                    var json1 = await response1.Content.ReadAsStringAsync();
                    var json2 = await response2.Content.ReadAsStringAsync();
                    var json3 = await response3.Content.ReadAsStringAsync();

                    // Debug.WriteLine(json2);

                    //Parse json data
                    dynamic ProcedureData = JArray.Parse(json1);
                    dynamic ScheduleData = JArray.Parse(json2);
                    dynamic ClinicData = JArray.Parse(json3);

                    foreach (JObject c in ClinicData)
                    {
                        if ((int)c["clinicId"] == Data.clinicId)
                        {
                            clinic.clinicId = (int)c["clinicId"];
                            clinic.clinicName = (string)c["clinicName"];
                            clinic.phoneNumber = (string)c["phoneNumber"];
                        }
                    }

                    // outer foreach START
                    foreach (JObject item in ScheduleData)
                    {
                        //Debug.WriteLine(item.ToString());

                        Schedule tempSchedule = new Schedule();
                        Procedure tempProcedure = new Procedure();

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
                        
                        schedules.Add(tempSchedule);

                        /*if (!tempSchedule.isCompleted)
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

                            // Add the TextBox to the visual tree.
                            mainGrid.Children.Add(textBlock);
                        }*/

                    }// outer foreach END
                    
                    scheduleView.ItemsSource = schedules;
                    
                    TextBlock clinicNameTextBlock = new TextBlock();
                    TextBlock phoneNumberTextBlock = new TextBlock();
                    StackPanel clinicStackPanel = new StackPanel();

                    clinicStackPanel.VerticalAlignment = VerticalAlignment.Top;
                    clinicStackPanel.HorizontalAlignment = HorizontalAlignment.Stretch;
                    clinicStackPanel.Background = new SolidColorBrush(Windows.UI.Colors.AliceBlue);
                    mainGrid.Children.Add(clinicStackPanel);

                    clinicNameTextBlock.Text = clinic.clinicName;
                    clinicNameTextBlock.FontSize = 20;
                    clinicStackPanel.Children.Add(clinicNameTextBlock);

                    phoneNumberTextBlock.Text = clinic.phoneNumber;
                    phoneNumberTextBlock.FontSize = 20;
                    clinicStackPanel.Children.Add(phoneNumberTextBlock);
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
            Data.procedureId = obj.prodecure.procedureId;
            Debug.WriteLine(obj.prodecure.procedureId);
            this.Frame.Navigate(typeof(MedicationPage));
        }

        private void SubProceduresButton_Click(object sender, RoutedEventArgs e)
        {
            Schedule obj = ((FrameworkElement)sender).DataContext as Schedule;
            Data.procedureId = obj.prodecure.procedureId;
            Data.procedureIsCompleted = obj.isCompleted;
            this.Frame.Navigate(typeof(SubProceduresPage));
        }
    }
}
