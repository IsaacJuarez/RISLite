using Fuji.RISLite.Entities;

namespace Fuji.RISLite.Site.Services.DataContract
{
    public class CitaModalidad
    {
        public clsUsuario mdlUser;
        public clsModalidad mdlModalidad;

        public CitaModalidad()
        {
            mdlUser = new clsUsuario();
            mdlModalidad = new clsModalidad();
        }
    }
}