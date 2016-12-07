using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondAidUWP.Model
{
    class MedInstruction
    {
        public int medicationId;
        public string medInstruction;

        public string getMedInstruction()
        {
            return medInstruction;
        }

        public void setMedInstruction(string medInstruction)
        {
            this.medInstruction = medInstruction;
        }
    }
}
