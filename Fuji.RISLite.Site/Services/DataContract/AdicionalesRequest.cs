using Fuji.RISLite.Entities;

namespace Fuji.RISLite.Site.Services.DataContract
{
    public class AdicionalesRequest
    {
        public clsUsuario mdlUser;
        public clsAdicionales mdlAdicional;
        public int intTipoAdicional { get; set; }
        public int intAdicionalesID { get; set; }
        public int intSitioID { get; set; }
        public int intMasculino { get; set; }
        public int intFemenino { get; set; }
        public int intMayor { get; set; }
        public int intMenor { get; set; }

        public AdicionalesRequest()
        {
            mdlUser = new clsUsuario();
            mdlAdicional = new clsAdicionales();
            intTipoAdicional = int.MinValue;
            intAdicionalesID = int.MinValue;
            intSitioID = int.MinValue;
            intMasculino = int.MinValue;
            intFemenino = int.MinValue;
            intMayor = int.MinValue;
            intMenor = int.MinValue;
        }
    }
}