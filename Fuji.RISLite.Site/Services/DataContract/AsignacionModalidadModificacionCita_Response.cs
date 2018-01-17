using Fuji.RISLite.Entities;
using System.Collections.Generic;

namespace Fuji.RISLite.Site.Services.DataContract
{
    public class AsignacionModalidadModificacionCita_Response
    {
        public List<clsEstudioNuevaCita> mdlEstudio;
        public bool Success { get; set; }
        public string Mensaje { get; set; }


        public AsignacionModalidadModificacionCita_Response()
        {
            mdlEstudio = new List<clsEstudioNuevaCita>();
            Mensaje = string.Empty;
            Success = false;
        }
    }    
}