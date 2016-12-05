using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondAidUWP.Model
{
    class Questionnaire
    {
        private int questionnaireId;
        private int subProcedureId;
        private string name;

        public List<Question> questions;

        public int getQuestionnaireId()
        {
            return questionnaireId;
        }

        public int getSubProcedureId()
        {
            return subProcedureId;
        }

        public string getName()
        {
            return name;
        }

        public List<Question> getQuestions()
        {
            return questions;
        }

        public void setQuestionnaireId(int questionnaireId)
        {
            this.questionnaireId = questionnaireId;
        }

        public void setSubProcedureId(int subprocedureId)
        {
            this.subProcedureId = subprocedureId;
        }

        public void setName(string name)
        {
            this.name = name;
        }

        public void setQuestions(List<Question> questions)
        {
            this.questions = questions;
        }
    }
}
