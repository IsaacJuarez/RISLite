using Fuji.RISLite.Entities;

namespace Fuji.RISLite.Site.Services.DataContract
{
    public class CitaReporteRequest
    {
        public clsUsuario mdlUser;
        public int intCitaId;
        public int intPrestacionID { get; set; }
        public int intEstudioID { get; set; }
        public int intEstatusID { get; set; }
        public clsEstudioCita mdlEstudio;

        public CitaReporteRequest()
        {
            mdlUser = new clsUsuario();
            intPrestacionID = int.MinValue;
            intEstudioID = int.MinValue;
            intEstatusID = int.MinValue;
            mdlEstudio = new clsEstudioCita();
        }
    }
}