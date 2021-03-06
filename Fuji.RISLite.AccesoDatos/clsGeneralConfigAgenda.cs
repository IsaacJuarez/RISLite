﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fuji.RISLite.Entities
{
   public class clsGeneralConfigAgenda
    {
        public int intSitioID { get; set; }
        public int intConfiguracionAgendaID { get; set; }
        public string vchConfiguracionAgenda { get; set; }    
        public TimeSpan tmeInicioDia { get; set; }
        public TimeSpan tmeFinDia { get; set; }
        public DateTime datFecha { get; set; }        
        public string vchUserAdmin { get; set; }
        public int intIntervalo { get; set; }

        public clsGeneralConfigAgenda()
        {
            intSitioID = int.MinValue;
            intConfiguracionAgendaID = int.MinValue;
            vchConfiguracionAgenda = string.Empty;
            tmeInicioDia = TimeSpan.Zero;
            tmeFinDia = TimeSpan.Zero;
            datFecha = DateTime.MinValue;
            vchUserAdmin = string.Empty;         
            intIntervalo = int.MinValue;
        }
    }
}




