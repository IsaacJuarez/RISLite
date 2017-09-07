using Fuji.RISLite.Entities;

namespace Fuji.RISLite.Site.Services.DataContract
{
    public class PacienteRequest
    {
        public clsUsuario mdlUser;
        public clsPaciente mdlPaciente;
        public clsDireccion mdlDireccion;
        public int intPacienteID { get; set; }
        public string busqueda { get; set; }

        public PacienteRequest()
        {
            mdlUser = new clsUsuario();
            mdlPaciente = new clsPaciente();
            mdlDireccion = new clsDireccion();
            intPacienteID = int.MinValue;
            busqueda = string.Empty;
        }
    }
}