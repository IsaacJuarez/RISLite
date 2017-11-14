using Fuji.RISLite.Entities;

namespace Fuji.RISLite.Site.Services.DataContract
{
    public class EstatusCita
    {
        public clsUsuario mdlUser;
        public clsListaDeTrabajo mdlListadeTrabajo;

        public EstatusCita()
        {
            mdlUser = new clsUsuario();
            mdlListadeTrabajo = new clsListaDeTrabajo();
        }
    }
}