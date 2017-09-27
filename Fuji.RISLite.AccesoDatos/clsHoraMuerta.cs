using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fuji.RISLite.Entities
{
    public class clsHoraMuerta
    {
        public int intHorasMuertasID { get; set; }
        public string tmeInicio { get; set; }
        public string tmeFin { get; set; }
        public bool bitRepetir { get; set; }
        public DateTime datFecha { get; set; }
        public string vchUserAdmin { get; set; }

        public clsHoraMuerta()
        {
            intHorasMuertasID = int.MinValue;
            tmeInicio = string.Empty;
            tmeFin = string.Empty;
            bitRepetir = false;
            datFecha = DateTime.MinValue;
            vchUserAdmin = string.Empty;
        }
    }
}
