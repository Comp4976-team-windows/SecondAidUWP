using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondAidUWP.Model
{
    class Config
    {
        //Urls
        public static string connectTokenUrl = "http://2aid.azurewebsites.net/connect/token";
        public static string procedureApiUrl = "http://2aid.azurewebsites.net/api/procedures";

        public static string questionnaireApiUrl = "http://secondaid.azurewebsites.net/api/questionnaires/";
        public static string subProcedureApiUrl = "http://2aid.azurewebsites.net/api/subprocedure";
        public static string preInstructionApiUrl = "http://2aid.azurewebsites.net/api/preinstructions";
        public static string scheduleApiUrl = "http://2aid.azurewebsites.net/api/schedules";
        public static string medicationInstruction = "http://2aid.azurewebsites.net/api/medicationinstructions";
        public static string medicationApiUrl = "http://2aid.azurewebsites.net/api/medications";

    }
}
