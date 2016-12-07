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
    public sealed partial class QuestionListPage : Page
    {

        private Questionnaire questionnaire;
        private int questionIndex = 1;

        public QuestionListPage()
        {
            this.InitializeComponent();
            getQuestionnaire();
        }

        public async void getQuestionnaire()
        {
            Debug.WriteLine("Starting Get");

            try
            {
                using (var client = new HttpClient())
                {
                    var request = new HttpRequestMessage()
                    {
                        RequestUri = new Uri(Config.questionnaireApiUrl + Data.subProcedureId),
                        Method = HttpMethod.Get
                    };
                    request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

                    //Add Token
                    //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Data.userToken);

                    //Grab json of token from server
                    HttpResponseMessage response = await client.SendAsync(request);
                    var json = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine(json);

                    //Parse json data
                    dynamic data = JObject.Parse(json);

                    List<Question> questions = new List<Question>();
                    questionnaire = new Questionnaire();

                    questionnaire.setQuestionnaireId((int) data["questionnaireId"]);
                    questionnaire.setName((string) data["name"]);
                    questionnaire.setSubProcedureId((int)data["subProcedureId"]);

                    foreach (JObject item in data["questions"])
                    {
                        Debug.WriteLine(item.ToString());

                        Question tempQuestion = new Question();

                        tempQuestion.setQuestionId((int)item["questionId"]);
                        tempQuestion.setQuestionBody((string)item["questionBody"]);

                        questions.Add(tempQuestion);
                    }

                    questionnaire.setQuestions(questions);
                    populateQuestionnaire();
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex);
            }

        }

        //Update the page
        public void populateQuestionnaire()
        {
            questionnaireNameTextBlock.Text = questionnaire.getName();
            questionNumberBox.Text = "" + questionIndex + " / " + questionnaire.questions.Count;
            questionTextBox.Text = questionnaire.questions[questionIndex - 1].getQuestionBody();

            if(questionnaire.questions[questionIndex - 1].getQuestionAnswer() == null)
            {
                answerTextBox.Text = "";
            }else
            {
                answerTextBox.Text = questionnaire.questions[questionIndex - 1].getQuestionAnswer();
            }
        }

        //Previous Button
        private void previousButton_Click(object sender, RoutedEventArgs e)
        {
            if(questionIndex > 1)
            {
                questionnaire.questions[questionIndex - 1].setQuestionAnswer(answerTextBox.Text);
                questionIndex--;

                populateQuestionnaire();
            }
        }

        //Next Button
        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            if (questionIndex < questionnaire.questions.Count)
            {
                questionnaire.questions[questionIndex - 1].setQuestionAnswer(answerTextBox.Text);
                questionIndex++;

                populateQuestionnaire();
            }
        }

        private void doneButton_Click(object sender, RoutedEventArgs e)
        {
            questionnaire.questions[questionIndex - 1].setQuestionAnswer(answerTextBox.Text);
            this.Frame.Navigate(typeof(SubProceduresPage));
        }
    }

    
}
