using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fuji.RISLite.Entities
{
   public class clsEventoCita
    {
        public int TaskID { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int OwnerID { get; set; }
        public bool IsAllDay { get; set; }
        public string RecurrenceRule { get; set; }
        public int RecurrenceID { get; set; }
        public string RecurrenceException { get; set; }
        public DateTime StarTimezone { get; set; }
        public DateTime EndTimezone { get; set; }
        public int intModalidadID { get; set; }
     
        public clsEventoCita()
        {
            TaskID = int.MinValue;
            Start = DateTime.MinValue;
            End = DateTime.MinValue;
            Title = string.Empty;
            Description = string.Empty;
            OwnerID = int.MinValue;
            IsAllDay = false;
            RecurrenceRule = string.Empty;
            RecurrenceID = int.MinValue;
            RecurrenceException = string.Empty;
            StarTimezone = DateTime.MinValue;
            EndTimezone = DateTime.MinValue;
            intModalidadID = int.MinValue;
        }
    }
}
