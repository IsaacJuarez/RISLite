using Fuji.RISLite.Entidades.DataBase;
using Fuji.RISLite.Entities;

namespace Fuji.RISLite.Site.Services.DataContract
{
    public class EquipoRequest
    {
        public clsUsuario mdlUser;
        public int intEquipoID { get; set; }
        public int intSitioID { get; set; }
        public int intModalidadID { get; set; }
        public tbl_CAT_Equipo mdlEquipo;

        public EquipoRequest()
        {
            mdlUser = new clsUsuario();
            intEquipoID = int.MinValue;
            mdlEquipo = new tbl_CAT_Equipo();
            intSitioID = int.MinValue;
        }
    }
}