using System;

namespace Fuji.RISLite.Entities
{
    public class clsconfscheduler_addcita
    {     
        public int intConfiguracionAgendaID { get; set; }
        public string vchConfiguracionAgenda { get; set; }
        public DateTime tmeInicioDia { get; set; }
        public DateTime tmeFinDia { get; set; }
        public DateTime datFecha { get; set; }
        public string vchUserAdmin { get; set; }
        public int intIntervalo { get; set; }

        public clsconfscheduler_addcita()
        {
            intConfiguracionAgendaID = int.MinValue;
            vchConfiguracionAgenda = string.Empty;
            tmeInicioDia = DateTime.MinValue;
            tmeFinDia = DateTime.MinValue;
            datFecha = DateTime.MinValue;
            vchUserAdmin = string.Empty;
            intIntervalo = int.MinValue;
        }
    }
}
