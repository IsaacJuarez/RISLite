using Fuji.RISLite.Entities;

namespace Fuji.RISLite.Site.Services.DataContract
{
    public class AsignacionModalidadNuevaCita_Request
    {
        public clsEstudioNuevaCita mdlEstudio;
        public clsUsuario mdlUser;
        public int intPacienteID { get; set; }

        public AsignacionModalidadNuevaCita_Request()
        {
            mdlEstudio = new clsEstudioNuevaCita();
            mdlUser = new clsUsuario();
            intPacienteID = int.MinValue;
        }
    }
}