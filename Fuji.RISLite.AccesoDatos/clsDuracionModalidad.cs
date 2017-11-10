using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fuji.RISLite.Entities
{
    public class clsDuracionModalidad
    {

        public int intDuracionModalidadID { get; set; }
        public int intDuracion { get; set; }
        public DateTime datFecha { get; set; }
        public string vchUserAdmin { get; set; }
        public bool bitActivo { get; set; }

        public clsDuracionModalidad()
        {

            intDuracionModalidadID = int.MinValue;
            intDuracion = int.MinValue;
            datFecha = DateTime.MinValue;
            vchUserAdmin = string.Empty;
            bitActivo = false;

        }
    }
}
