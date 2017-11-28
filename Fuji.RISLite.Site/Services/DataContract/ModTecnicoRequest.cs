using Fuji.RISLite.Entities;

namespace Fuji.RISLite.Site.Services.DataContract
{
    public class ModTecnicoRequest
    {
        public clsUsuario mdlUser;
        public int intSitioID { get; set; }
        public int intUsuarioID { get; set; }
        public int intModalidadID { get; set; }
        public int intRELModTecnicoID { get; set; }

        public ModTecnicoRequest()
        {
            mdlUser = new clsUsuario();
            intSitioID = int.MinValue;
            intUsuarioID = int.MinValue;
            intModalidadID = int.MinValue;
            intRELModTecnicoID = int.MinValue;
        }
    }
}