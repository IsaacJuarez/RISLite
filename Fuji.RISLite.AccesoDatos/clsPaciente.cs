using System;

namespace Fuji.RISLite.Entities
{
    public class clsPaciente
    {
        public int intPacienteID { get; set; }
        public int intGeneroID { get; set; }
        public  string vchGenero { get; set; }
        public  string vchNombre { get; set; }
        public string vchApellidos { get; set; }
        public DateTime datFechaNac { get; set; }
        public string vchEmail { get; set; }
        public string vchNumeroContacto { get; set; }
        public int intDETPacienteID { get; set; }

        public clsPaciente()
        {
            intPacienteID = int.MinValue;
            intGeneroID = int.MinValue;
            vchGenero = string.Empty;
            vchNombre = string.Empty;
            vchApellidos = string.Empty;
            datFechaNac = DateTime.MinValue;
            vchEmail = string.Empty;
            vchNumeroContacto = string.Empty;
            intDETPacienteID = int.MinValue;
        }
    }
}
