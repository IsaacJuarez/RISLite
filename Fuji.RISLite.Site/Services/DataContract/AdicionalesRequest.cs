using Fuji.RISLite.Entities;

namespace Fuji.RISLite.Site.Services.DataContract
{
    public class AdicionalesRequest
    {
        public clsUsuario mdlUser;
        public clsAdicionales mdlAdicional;
        public int intTipoAdicional { get; set; }
        public int intAdicionalesID { get; set; }

        public AdicionalesRequest()
        {
            mdlUser = new clsUsuario();
            mdlAdicional = new clsAdicionales();
            intTipoAdicional = int.MinValue;
            intAdicionalesID = int.MinValue;
        }
    }
}