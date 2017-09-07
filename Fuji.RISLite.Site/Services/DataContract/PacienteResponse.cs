using Fuji.RISLite.Entities;
using System.Collections.Generic;

namespace Fuji.RISLite.Site.Services.DataContract
{
    public class PacienteResponse
    {
        public bool Success { get; set; }
        public string Mensaje { get; set; }
        public int intPacienteID { get; set; }
        public clsPaciente mdlPaciente;
        public clsDireccion mdlDireccion;
        public List<string> lstCadenas;

        public PacienteResponse()
        {
            Success = false;
            Mensaje = string.Empty;
            intPacienteID = int.MinValue;
            mdlPaciente = new clsPaciente();
            mdlDireccion = new clsDireccion();
            lstCadenas = new List<string>();
        }
    }
}