using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondAidUWP.Model
{
    class SubProcedure
    {
        public int subProcedureId { set; get; }
        public string name { set; get; }
        public string description { set; get; }
        public int procedureId { set; get; }
        public Procedure procedure { set; get; }
        public string videos { set; get; }
        public PreInstruction preInstructions { set; get; }
    }
}
