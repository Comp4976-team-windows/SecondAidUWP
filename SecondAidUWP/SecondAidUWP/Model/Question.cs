using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondAidUWP.Model
{
    class Question
    {
        private int questionId;
        private string questionBody;

        private string questionAnswer;

        public int getQuestionId()
        {
            return questionId;
        }

        public string getQuestionBody()
        {
            return questionBody;
        }

        public string getQuestionAnswer()
        {
            return questionAnswer;
        }

        public void setQuestionId(int id)
        {
            this.questionId = id;
        }

        public void setQuestionBody(string questionBody)
        {
            this.questionBody = questionBody;
        }

        public void setQuestionAnswer(string questionAnswer)
        {
            this.questionAnswer = questionAnswer;
        }
    }
}
