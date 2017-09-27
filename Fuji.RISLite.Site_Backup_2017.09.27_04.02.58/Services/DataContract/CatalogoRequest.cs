using Fuji.RISLite.Entities;

namespace Fuji.RISLite.Site.Services.DataContract
{
    public class CatalogoRequest
    {
        public clsUsuario mdlUser;
        public clsCatalogo mdlCat;

        public CatalogoRequest()
        {
            mdlUser = new clsUsuario();
            mdlCat = new clsCatalogo();
        }
    }
}