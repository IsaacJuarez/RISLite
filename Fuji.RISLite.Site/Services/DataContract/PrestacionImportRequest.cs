using Fuji.RISLite.Entities;

namespace Fuji.RISLite.Site.Services.DataContract
{

    public class PrestacionImportRequest
    {
        public int intModalidad { get; set; }
        public int intSitioID { get; set; }
        public int intRELModPres { get; set; }
        public clsUsuario mdlUser;
        public clsPrestacion mdlPres;
        public clsDetIndicacionPrestacion mdlDetIndPres;
        public clsDetCuestionario mdlDetCuest;
        public clsDetRestriccion mdlDetRest;

        public PrestacionImportRequest()
        {
            intModalidad = int.MinValue;
            intSitioID = int.MinValue;
            mdlUser = new clsUsuario();
            mdlPres = new clsPrestacion();
            intRELModPres = int.MinValue;
            mdlDetIndPres = new clsDetIndicacionPrestacion();
            mdlDetCuest = new clsDetCuestionario();
            mdlDetRest = new clsDetRestriccion();
        }
    }

}