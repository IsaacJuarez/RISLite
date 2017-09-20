using System;

namespace Fuji.RISLite.Entities
{
    public class clsEstudioCita
    {
        public int intPacienteID { get; set; }
        public string vchNombrePaciente { get; set; }
        public int intCitaID { get; set; }
        public int intEstatusCita { get; set; }
        public string vchEstatusCita { get; set; }
        public DateTime datFechaCita { get; set; }
        public int intPrestacionID { get; set; }
        public string vchPrestacion { get; set; }

        public clsEstudioCita()
        {
            intPacienteID = int.MinValue;
            vchNombrePaciente = string.Empty;
            intCitaID = int.MinValue;
            vchEstatusCita = string.Empty;
            datFechaCita = DateTime.MinValue;
            intPrestacionID = int.MinValue;
            vchPrestacion = string.Empty;
        }
    }
}
