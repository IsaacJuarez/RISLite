using System;

namespace Fuji.RISLite.Entities
{
    public class clsModalidad
    {

        public int intModalidadID { get; set; }
        public int intSitioID { get; set; }
        public string vchModalidad { get; set; }
        public string vchCodigo { get; set; }
        public string vchColor { get; set; }
        public bool bitActivo { get; set; }
        public DateTime datFecha { get; set; }
        public string vchUserAdmin { get; set; }
        public int intDuracionGen { get; set; }

        public clsModalidad()
        {
            intModalidadID = int.MinValue;
            intSitioID = int.MinValue;
            vchModalidad = string.Empty;
            vchCodigo = string.Empty;
            vchColor = string.Empty;
            bitActivo = false;
            datFecha = DateTime.MinValue;
            vchUserAdmin = string.Empty;
            intDuracionGen = int.MinValue;
        }
    }
}
