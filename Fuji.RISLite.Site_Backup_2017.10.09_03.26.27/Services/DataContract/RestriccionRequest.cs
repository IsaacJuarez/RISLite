using Fuji.RISLite.Entidades.DataBase;
using Fuji.RISLite.Entities;

namespace Fuji.RISLite.Site.Services.DataContract
{
    public class RestriccionRequest
    {
        public clsUsuario mdlUser;
        public tbl_DET_Restriccion mdlRestriccion;
        public int intPrestacionID { get; set; }
        public int intReestriccionID { get; set; }

        public RestriccionRequest()
        {
            mdlUser = new clsUsuario();
            mdlRestriccion = new tbl_DET_Restriccion();
            intPrestacionID = int.MinValue;
            intReestriccionID = int.MinValue;
        }
    }
}