using System;

namespace Fuji.RISLite.Entities
{
    public class clsCatalogo
    {
        public int intCatalogoID { get; set; }
        public int intPrimaryKey { get; set; }
        public int intSitioID { get; set; }
        public string vchValor { get; set; }
        public string vchNombre { get; set; }
        public bool bitActivo { get; set; }
        public string vchUserAdmin { get; set; }
        public DateTime datFecha { get; set; }

        public clsCatalogo()
        {
            intCatalogoID = int.MinValue;
            intPrimaryKey = int.MinValue;
            intSitioID = int.MinValue;
            vchNombre = string.Empty;
            vchValor = string.Empty;
            bitActivo = false;
            vchUserAdmin = string.Empty;
            datFecha = DateTime.MinValue;
        }
    }
}
