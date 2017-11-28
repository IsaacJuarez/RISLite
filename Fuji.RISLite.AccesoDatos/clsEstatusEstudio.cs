using System;

namespace Fuji.RISLite.Entities
{
    public class clsEstatusEstudio
    {
        public int intEstatusEstudio { get; set; }
        public string vchEstatus { get; set; }
        public DateTime datFecha { get; set; }
        public bool bitActivo { get; set; }
        public string vchUserAdmin { get; set; }

        public clsEstatusEstudio()
        {
            intEstatusEstudio = int.MinValue;
            vchEstatus = string.Empty;
            datFecha = DateTime.MinValue;
            bitActivo = false;
            vchUserAdmin = string.Empty;
        }

    }
}
