using System;

namespace Fuji.RISLite.Entities
{
    public class clsGraficaUsuario
    {
        public int intEstudioID { get; set; }
        public string vchUsuario { get; set; }
        public string vchmodalidad { get; set; }
        public string vchEstatus { get; set; }
        public DateTime fechaInicio { get; set; }
        public DateTime fechaFin { get; set; }
        public string vchTitulo { get; set; }
        public string vchDescripcion { get; set; }

        public clsGraficaUsuario()
        {
            intEstudioID = int.MinValue;
            vchUsuario = string.Empty;
            vchmodalidad = string.Empty;
            vchEstatus = string.Empty;
            fechaInicio = DateTime.MinValue;
            fechaFin = DateTime.MinValue;
            vchTitulo = string.Empty;
            vchDescripcion = string.Empty;
        }
    }
}
