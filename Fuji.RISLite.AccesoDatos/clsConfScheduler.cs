using System;

namespace Fuji.RISLite.Entities
{
    public class clsConfScheduler
    {

        public int intConfiguracionAgendaID { get; set; }
        public int intSitioID { get; set; }
        public string vchConfiguracionAgenda { get; set; }
        public TimeSpan tmeInicioDia { get; set; }
        public TimeSpan tmeFinDia { get; set; }
        public DateTime datFecha { get; set; }
        public string vchUserAdmin { get; set; }
        public int intIntervalo { get; set; }
  
        public clsConfScheduler()
        {
            intConfiguracionAgendaID = int.MinValue;
            intSitioID = int.MinValue;
            vchConfiguracionAgenda = string.Empty;
            tmeInicioDia = TimeSpan.MinValue;
            tmeFinDia = TimeSpan.MinValue;
            datFecha = DateTime.MinValue;
            vchUserAdmin = string.Empty;
            intIntervalo = int.MinValue;   
        }
    }
}
