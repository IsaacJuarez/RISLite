using Fuji.RISLite.Entities;

namespace Fuji.RISLite.Site.Services.DataContract
{
    public class PrestacionRequest
    {
        public int intModalidad { get; set; }
        public int intRELModPres { get; set; }
        public clsUsuario mdlUser;
        public clsPrestacion mdlPres;
        public PrestacionRequest()
        {
            intModalidad = int.MinValue;
            mdlUser = new clsUsuario();
            mdlPres = new clsPrestacion();
            intRELModPres = int.MinValue;
        }
    }
}