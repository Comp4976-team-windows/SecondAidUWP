﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondAidUWP.Model
{
    class Data
    {
        //Tokens
        public static string userToken = "";

        public static int clinicId;

        public static int procedureId;
        public static bool procedureIsCompleted;

        public static int subProcedureId;

        //Lists
        public static List<Procedure> procedures = new List<Procedure>();
    }
}
