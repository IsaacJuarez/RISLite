using System;

namespace Fuji.RISLite.Entities
{
    public class clsVarAcicionales
    {
        public int intADIPacienteID { get; set; }
        public int intVariableAdiID { get; set; }
        public string vchNombreVarAdi { get; set; }
        public string vchValorAdicional { get; set; }
        public bool bitActivo { get; set; }
        public DateTime datFecha { get; set; }
        public string vchUserAdmin { get; set; }
        public int intSitioID { get; set; }

        public clsVarAcicionales()
        {
            intADIPacienteID = int.MinValue;
            intVariableAdiID = int.MinValue;
            vchNombreVarAdi = string.Empty;
            vchValorAdicional = string.Empty;
            bitActivo = false;
            datFecha = DateTime.MinValue;
            vchUserAdmin = string.Empty;
            intSitioID = int.MinValue;
        }
    }
}
