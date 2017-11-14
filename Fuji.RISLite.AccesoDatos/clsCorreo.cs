using System;

namespace Fuji.RISLite.Entities
{
    public class clsCorreo
    {
        public string correo { get; set; }
        public string asunto { get; set; }
        public string usuarioCorreo { get; set; }
        public string passwordCorreo { get; set; }
        public string urlMensaje { get; set; }
        public string toEmail { get; set; }
        public string htmlCorreo { get; set; }
        public string NumAcc { get; set; }
        public DateTime FechaEstudio { get; set; }
        public string PatientID { get; set; }
        public string PatientName { get; set; }
        public string Edad { get; set; }
        public string Genero { get; set; }
        public string FechaNacimiento { get; set; }
        public string Interpretacion { get; set; }
        public bool bitReporte { get; set; }
        public string vchNombrePaciente { get; set; }
        public int intCitaID { get; set; }

        public clsCorreo()
        {
            correo = string.Empty;
            asunto = string.Empty;
            usuarioCorreo = string.Empty;
            passwordCorreo = string.Empty;
            urlMensaje = string.Empty;
            toEmail = string.Empty;
            htmlCorreo = string.Empty;
            NumAcc = string.Empty;
            FechaEstudio = DateTime.MinValue;
            PatientID = string.Empty;
            PatientName = string.Empty;
            Edad = string.Empty;
            Genero = string.Empty;
            FechaNacimiento = string.Empty;
            Interpretacion = string.Empty;
            bitReporte = false;
            vchNombrePaciente = string.Empty;
            intCitaID = int.MinValue;
        }
    }
}
