using Fuji.RISLite.Entities;

namespace Fuji.RISLite.Site.Services.DataContract
{
    public class VarAdicionalRequest
    {
        public clsUsuario mdlUser;
        public clsVarAcicionales mdlVariable;
        public int intTipoVariable { get; set; }

        public VarAdicionalRequest()
        {
            mdlUser = new clsUsuario();
            mdlVariable = new clsVarAcicionales();
            intTipoVariable = int.MinValue;
        }
    }
}