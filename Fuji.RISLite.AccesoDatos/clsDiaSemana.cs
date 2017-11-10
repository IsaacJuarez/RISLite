using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fuji.RISLite.Entities
{
    public class clsDiaSemana
    {
        public int intSitioID { get; set; }
        public int intSemanaID { get; set; }
        public string vchDiaSemana { get; set; }
        public bool bitActivo { get; set; }
        public DateTime datFecha { get; set; }
        public string vchUserAdmin { get; set; }

        public clsDiaSemana()
        {
            intSitioID = int.MinValue;
            intSemanaID = int.MinValue;
            vchDiaSemana = string.Empty;
            bitActivo = false;
            datFecha = DateTime.MinValue;
            vchUserAdmin = string.Empty;
        }
    }
}
