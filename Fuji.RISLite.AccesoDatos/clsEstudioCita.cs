using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fuji.RISLite.AccesoDatos
{
    public class clsEstudioCita
    {
        public int intPacienteID { get; set; }
        public string vchNombrePaciente { get; set; }
        public int intCitaID { get; set; }
        public int intEstatusCita { get; set; }
        public string vchEstatusCita { get; set; }
        public DateTime datFechaCita { get; set; }

        public clsEstudioCita()
        {
            intPacienteID = int.MinValue;
            vchNombrePaciente = string.Empty;
            intCitaID = int.MinValue;
            vchEstatusCita = string.Empty;
            datFechaCita = DateTime.MinValue;

        }
    }
}
