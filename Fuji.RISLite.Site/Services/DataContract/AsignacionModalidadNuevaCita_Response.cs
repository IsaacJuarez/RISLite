using Fuji.RISLite.Entities;

namespace Fuji.RISLite.Site.Services.DataContract
{
    public class AsignacionModalidadNuevaCita_Response
    {
        public clsEstudioNuevaCita mdlEstudio;
        public bool Success { get; set; }
        public string Mensaje { get; set; }


        public AsignacionModalidadNuevaCita_Response()
        {
            mdlEstudio = new clsEstudioNuevaCita();
            Mensaje = string.Empty;
            Success = false;
        }
    }
}