using System;

namespace Fuji.RISLite.Entities
{
    public class clsAdicionales
    {
        public int intAdicionalesID { get; set; }
        public int intTipoBotonID { get; set; }
        public string vchTipoBoton { get; set; }
        public int intTipoAdicionalID { get; set; }
        public string vchTipoAdicional { get; set; }
        public string vchNombreAdicional { get; set; }
        public string vchURLImagen { get; set; }
        public bool bitIconBootstrap { get; set; }
        public bool bitObservaciones { get; set; }
        public bool bitActivo { get; set; }
        public DateTime datFecha { get; set; }
        public string vchUserAdmin { get; set; }

        public clsAdicionales()
        {
            intAdicionalesID = int.MinValue;
            intTipoAdicionalID = int.MinValue;
            intTipoBotonID = int.MinValue;
            vchTipoBoton = string.Empty;
            vchTipoAdicional = string.Empty;
            vchNombreAdicional = string.Empty;
            vchURLImagen = string.Empty;
            bitIconBootstrap = false;
            bitObservaciones = false;
            bitActivo = false;
            datFecha = DateTime.MinValue;
            vchUserAdmin = string.Empty;
    }
}
