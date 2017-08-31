using System;

namespace Fuji.RISLite.Entities
{
    public class clsVarAcicionales
    {
        public int intVariableAdiID { get; set; }
        public string vchNombreVarAdi { get; set; }
        public string vchValorAdicional { get; set; }
        public bool bitActivo { get; set; }
        public DateTime datFecha { get; set; }
        public string vchUserAdmin { get; set; }

        public clsVarAcicionales()
        {
            intVariableAdiID = int.MinValue;
            vchNombreVarAdi = string.Empty;
            vchValorAdicional = string.Empty;
            bitActivo = false;
            datFecha = DateTime.MinValue;
            vchUserAdmin = string.Empty;
        }
    }
}
