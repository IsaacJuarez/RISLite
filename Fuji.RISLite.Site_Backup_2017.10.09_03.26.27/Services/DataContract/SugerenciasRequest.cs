using Fuji.RISLite.Entities;

namespace Fuji.RISLite.Site.Services.DataContract
{
    public class SugerenciasRequest
    {
        public clsUsuario mdlUser;
        public clsSugerencia mdlSug;
        public SugerenciasRequest()
        {
            mdlUser = new clsUsuario();
            mdlSug = new clsSugerencia();
        }
    }
}