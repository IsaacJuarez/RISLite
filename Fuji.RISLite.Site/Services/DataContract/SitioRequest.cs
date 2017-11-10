using Fuji.RISLite.Entidades.DataBase;
using Fuji.RISLite.Entities;

namespace Fuji.RISLite.Site.Services.DataContract
{
    public class SitioRequest
    {
        public clsUsuario mdlUser;
        public int intSitioID { get; set; }
        public tbl_CAT_Sitio mdlSitio;

        public SitioRequest()
        {
            mdlUser = new clsUsuario();
            intSitioID = int.MinValue;
            mdlSitio = new tbl_CAT_Sitio();
        }
    }
}