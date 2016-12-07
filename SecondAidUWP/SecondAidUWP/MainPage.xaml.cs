using SecondAidUWP.Model;
using System;
using System.Net;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Collections.Generic;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Net.Http;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http.Headers;
using Windows.Data.Json;
using System.Text;
using Newtonsoft.Json.Linq;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SecondAidUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var name = UserNameTextBox.Text;
            var password = PasswordTextBox.Password;
            var clinicId = ClinicIdTextBox.Text;
            var url = Config.connectTokenUrl;
            
            //Set parameters for http request
            List<KeyValuePair<string,string>> myParameters = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("username", name),
                new KeyValuePair<string, string>("password", password),
                new KeyValuePair<string, string>("clinic_id", clinicId),
                new KeyValuePair<string, string>("grant_type", "password")
            };


            try
            {
                using (var client = new HttpClient())
                {
                    var request = new HttpRequestMessage()
                    {
                        RequestUri = new Uri(url),
                        Method = HttpMethod.Post,
                        Content = new FormUrlEncodedContent(myParameters)
                    };
                    request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

                    //Grab json of token from server
                    HttpResponseMessage response = await client.SendAsync(request);
                    var json = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine(json);

                    //Parse json data
                    dynamic data = JObject.Parse(json);

                    //Save information from json or print error
                    if (data.error != null)
                    {
                        errorMessageText.Text = data.error_description;
                    }
                    else
                    {
                        Data.userToken = data.access_token;
                        Data.clinicId = Convert.ToInt32(ClinicIdTextBox.Text);
                        errorMessageText.Text = "";

                        //Go to ProcedureListPage
                        this.Frame.Navigate(typeof(ProcedureListPage));
                    }
                }
                
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex);
            }

        }
    }
}
