using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondAidUWP.Model
{
    class Procedure
    {
        
        public int procedureId;
        public string name;
        public string description;

        public int getProcedureId()
        {
            return procedureId;
        }

        public string getName()
        {
            return name;
        }

        public string getDescription()
        {
            return description;
        }

        public void setProcedureId(int procedureId)
        {
            this.procedureId = procedureId;
        }

        public void setName(string name)
        {
            this.name = name;
        }

        public void setDescription(string description)
        {
            this.description = description;
        }

        /*
        public int procedureId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        */

    }
}
