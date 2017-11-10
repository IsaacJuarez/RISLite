using Fuji.RISLite.Entities;

namespace Fuji.RISLite.Site.Services.DataContract
{
    public class CitaNumEquipos
    {
        public clsUsuario mdlUser;
        public clsEquipo mdlequipo;

        public CitaNumEquipos()
        {
            mdlUser = new clsUsuario();
            mdlequipo = new clsEquipo();
        }
    }
}