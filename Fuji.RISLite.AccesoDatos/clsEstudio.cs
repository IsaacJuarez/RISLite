using System;

namespace Fuji.RISLite.Entities
{
    public class clsEstudio
    {

        public int intEstudioID { get; set; }
        public int intRelModPres { get; set; }
        public DateTime fechaInicio { get; set; }
        public DateTime fechaFin { get; set; }
        public string vchTitulo { get; set; }
        public string vchDescripcion { get; set; }
        public string vchPrestacion { get; set; }
        public string vchModalidad { get; set; }
        public string cadena { get; set; }
        public int intModalidadID { get; set; }
        public int intPrestacionID { get; set; }
        public string vchCodigo { get; set; }
        public int intDuracionMin { get; set; }

        public clsEstudio()
        {
            intEstudioID = int.MinValue;
            intRelModPres = int.MinValue;
            fechaFin = DateTime.MinValue;
            fechaInicio = DateTime.MinValue;
            vchTitulo = string.Empty;
            vchDescripcion = string.Empty;
            vchPrestacion = string.Empty;
            vchModalidad = string.Empty;
            cadena = string.Empty;
            intModalidadID = int.MinValue;
            intPrestacionID = int.MinValue;
            vchCodigo = string.Empty;
            intDuracionMin = int.MinValue;
        }
    }
}
