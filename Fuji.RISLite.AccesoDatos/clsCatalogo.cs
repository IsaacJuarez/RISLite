using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fuji.RISLite.AccesoDatos
{
    public class clsCatalogo
    {
        public int idCatalogo { get; set; }
        public string vchNombre { get; set; }
        public string vchValor { get; set; }
        public bool bitActivo { get; set; }
        public string vchUserAdmin { get; set; }
        public DateTime datFecha { get; set; }

        public clsCatalogo()
        {
            idCatalogo = int.MinValue;
            vchNombre = string.Empty;
            vchValor = string.Empty;
            bitActivo = false;
            vchUserAdmin = string.Empty;
            datFecha = DateTime.MinValue;
        }
    }
}
