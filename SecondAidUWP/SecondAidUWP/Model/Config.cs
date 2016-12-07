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

        public static string subProceduresApiUrl = "http://secondaid.azurewebsites.net/api/subProcedures";
        public static string preInstructionApiUrl = "http://secondaid.azurewebsites.net/api/preinstructions";

        //public static string scheduleApiUrl = "http://secondaid.azurewebsites.net/api/schedules";
        public static string scheduleApiUrl = "http://secondaid.azurewebsites.net/api/schedules";

        public static string medicationInstructionApiUrl = "http://secondaid.azurewebsites.net/api/medicationinstructions";
        public static string medicationApiUrl = "http://secondaid.azurewebsites.net/api/medications";

        public static string patientProceduresUrl = "http://secondaid.azurewebsites.net/api/patientprocedures";
        public static string clinicApiUrl = "http://secondaid.azurewebsites.net/api/clinics";

    }
}
