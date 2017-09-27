using System;

namespace Fuji.RISLite.Entities
{
    public class clsConfAgenda
    {     

        public int intModalidadID { get; set; }       
        public string vchModalidad { get; set; }
        public string vchCodigo { get; set; }
        public bool bitActivo { get; set; }
        public DateTime datFecha { get; set; }       
        public string vchUserAdmin { get; set; }
        public string vchColor { get; set; }

        public clsConfAgenda()
        {
            intModalidadID = int.MinValue;
            vchModalidad = string.Empty;
            vchCodigo = string.Empty;
            bitActivo = false;
            datFecha = DateTime.MinValue;          
            vchUserAdmin = string.Empty;
            vchColor = string.Empty;
        }
    }
}
