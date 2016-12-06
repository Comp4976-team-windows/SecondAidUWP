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
        public static string connectTokenUrl = "http://secondaid.azurewebsites.net/connect/token";

        public static string procedureApiUrl = "http://secondaid.azurewebsites.net/api/procedures";

        public static string questionnaireApiUrl = "http://secondaid.azurewebsites.net/api/questionnaires/";
        public static string subProcedureApiUrl = "http://secondaid.azurewebsites.net/api/subprocedure";
        public static string PreInstructionApiUrl = "http://secondaid.azurewebsites.net/api/preinstructione";

        //public static string scheduleApiUrl = "http://secondaid.azurewebsites.net/api/schedules";
        public static string scheduleApiUrl = "http://secondaid.azurewebsites.net/api/schedules";

        public static string medicationInstruction = "http://secondaid.azurewebsites.net/api/medicationinstructions";
        public static string medicationApiUrl = "http://secondaid.azurewebsites.net/api/medications";

    }
}
