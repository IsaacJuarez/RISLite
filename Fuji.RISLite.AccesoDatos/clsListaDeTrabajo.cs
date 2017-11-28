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
        public DateTime datFecha { get; set; }
        public string vchUserAdmin { get; set; }
        public string vchEstatus { get; set; }
        public string vchPrestacion { get; set; }
        public int intEstatusID { get; set; }
        public int intCitaID { get; set; }
        public int intModalidadID { get; set; }

        public clsListaDeTrabajo()
        {
            intEstudioID = int.MinValue;
            vchNombre = string.Empty;
            vchModalidad = string.Empty;
            vchCodigo = string.Empty;
            vchtitulo = string.Empty;
            datFechaInicio = DateTime.MinValue;
            datFecha = DateTime.MinValue;
            vchUserAdmin = string.Empty;
            vchEstatus = string.Empty;
            vchPrestacion = string.Empty;
            intEstatusID = int.MinValue;
            intCitaID = int.MinValue;
            intModalidadID = int.MinValue;
        }
    }
}
