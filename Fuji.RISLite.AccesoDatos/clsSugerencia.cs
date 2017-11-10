using System;

namespace Fuji.RISLite.Entities
{
    public class clsSugerencia
    {
        public DateTime datFechaInicio { get; set; }
        public DateTime datFechaFinal { get; set; }
        public int intModalidad { get; set; }
        public int intSitioID { get; set; }
        public string vchDias { get; set; }
        public string vchHoras { get; set; }


        public clsSugerencia()
        {
            datFechaInicio = DateTime.MinValue;
            datFechaFinal = DateTime.MinValue;
            intModalidad = int.MinValue;
            intSitioID = int.MinValue;
            vchDias = string.Empty;
            vchHoras = string.Empty;
        }
    }
}
