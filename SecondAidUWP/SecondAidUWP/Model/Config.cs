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

        //Form variables
        public static MainPage MainPage;
        public static ProcedureListPage ProcedureListPage;

        //Tokens
        public static string userToken = "";
    }
}
