using System;

namespace Fuji.RISLite.Entities
{
    public class clsGraficaModalidad
    {
        public int intEstudioID { get; set; }
        public string vchModalidad { get; set; }
        public string vchEstatus { get; set; }
        public DateTime fechaInicio { get; set; }
        public DateTime fechaFin { get; set; }
        public string vchTitulo { get; set; }
        public string vchDescripcion { get; set; }

        public clsGraficaModalidad()
        {
            intEstudioID = int.MinValue;
            vchModalidad = string.Empty;
            vchEstatus = string.Empty;
            fechaInicio = DateTime.MinValue;
            fechaFin = DateTime.MinValue;
            vchTitulo = string.Empty;
            vchDescripcion = string.Empty;
        }
    }
}
