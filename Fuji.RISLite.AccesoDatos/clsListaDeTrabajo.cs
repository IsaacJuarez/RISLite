using System;

namespace Fuji.RISLite.Entities
{
    public class clsListaDeTrabajo
    {
        public int intEstudioID { get; set; }
        public string vchNombre { get; set; }
        public string vchModalidad { get; set; }
        public string vchCodigo { get; set; }
        public string vchtitulo { get; set; }
        public DateTime datFechaInicio { get; set; }
        public string vchUserAdmin { get; set; }
        public string vchEstatus { get; set; }

        public clsListaDeTrabajo()
        {
            intEstudioID = int.MinValue;
            vchNombre = string.Empty;
            vchModalidad = string.Empty;
            vchCodigo = string.Empty;
            vchtitulo = string.Empty;
            datFechaInicio = DateTime.MinValue;
            vchUserAdmin = string.Empty;
            vchEstatus = string.Empty;
        }
    }
}
