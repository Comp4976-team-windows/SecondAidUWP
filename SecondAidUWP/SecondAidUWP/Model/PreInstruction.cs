using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondAidUWP.Model
{
    class PreInstruction
    {
        public int preInstructionId;
        public string title;
        public string description;

        public int getpreInstructionId()
        {
            return preInstructionId;
        }

        public string getTitle()
        {
            return title;
        }

        public string getDescription()
        {
            return description;
        }

        public void setPreInstructionId(int preInstructionId)
        {
            this.preInstructionId = preInstructionId;
        }

        public void setTitle(string title)
        {
            this.title = title;
        }

        public void setDescription(string description)
        {
            this.description = description;
        }
    }
}
