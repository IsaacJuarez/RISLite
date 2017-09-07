using Fuji.RISLite.Entities;

namespace Fuji.RISLite.Site.Services.DataContract
{
    public class DireccionRequest
    {
        public clsUsuario mdlUser = new clsUsuario();
        public string vchCodigoPostal { get; set; }

        public DireccionRequest()
        {
            mdlUser = new clsUsuario();
            vchCodigoPostal = string.Empty;
        }
    }
}