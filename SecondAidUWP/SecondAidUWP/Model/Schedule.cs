using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondAidUWP.Model
{
    class Schedule
    {
        public DateTime procedureDate { set; get; }
        public Boolean isCompleted { set; get; }
        public int patientId { set; get; }
        public int procedureId { set; get; }

        public Procedure prodecure { set; get; }
    }
}
