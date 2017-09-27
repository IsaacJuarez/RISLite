using System;

namespace Fuji.RISLite.Entities
{
    public class clsConfScheduler
    {

        public int intConfiguracionAgendaID { get; set; }
        public string vchConfiguracionAgenda { get; set; }
        public TimeSpan tmeInicioDia { get; set; }
        public TimeSpan tmeFinDia { get; set; }
        public DateTime datFecha { get; set; }
        public string vchUserAdmin { get; set; }
  
        public clsConfScheduler()
        {
            intConfiguracionAgendaID = int.MinValue;
            vchConfiguracionAgenda = string.Empty;
            tmeInicioDia = TimeSpan.MinValue;
            tmeFinDia = TimeSpan.MinValue;
            datFecha = DateTime.MinValue;
            vchUserAdmin = string.Empty;
   
        }
    }
}
