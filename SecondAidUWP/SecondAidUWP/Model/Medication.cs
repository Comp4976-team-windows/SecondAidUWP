using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondAidUWP.Model
{
    class Medication
    {
        public int medicationId;
        public string name;
        public string description;
        public string medInstruction;

        public int getMedicationId()
        {
            return medicationId;
        }

        public string getName()
        {
            return name;
        }

        public string getDescription()
        {
            return description;
        }

        public void setMedicationId(int medicationId)
        {
            this.medicationId = medicationId;
        }

        public void setName(string name)
        {
            this.name = name;
        }

        public void setDescription(string description)
        {
            this.description = description;
        }

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
