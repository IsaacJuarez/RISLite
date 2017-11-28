using System;

namespace Fuji.RISLite.Entities
{
    public class clsSitio
    {
        public int intSitioID { get; set; }
        public string vchNombreSitio { get; set; }
        public DateTime datFecha { get; set; }
        public bool bitActivo { get; set; }
        public string vchUserAdmin { get; set; }

        public clsSitio()
        {
            intSitioID = int.MinValue;
            vchNombreSitio = string.Empty;
            datFecha = DateTime.MinValue;
            bitActivo = false;
            vchUserAdmin = string.Empty;
        }
    }
}
