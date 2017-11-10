using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fuji.RISLite.Entities
{
    public class clsDiaFeriado
    {
        public int intDiaFeriadoID { get; set; }
        public int intSitioID { get; set; }
        public DateTime datDia { get; set; }
        public DateTime datFecha { get; set; }
        public string vchUserAdmin { get; set; }
        public bool bitActivo { get; set; }

        public clsDiaFeriado()
        {
            intSitioID = int.MinValue;
        intDiaFeriadoID = int.MinValue;
        datDia = DateTime.MinValue;
        datFecha = DateTime.MinValue;
        vchUserAdmin = string.Empty;
        bitActivo = false;

        }
    }
}
