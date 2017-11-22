using System;

namespace Fuji.RISLite.Entities
{
    public class clsUsuario
    {
        public int intUsuarioID { get; set; }
        public int intTipoUsuario { get; set; }
        public string vchTipoUsuario { get; set; }
        public string vchUsuario { get; set; }
        public string vchPassword { get; set; }
        public string vchNombre { get; set; }
        public DateTime datFecha { get; set; }
        public bool bitActivo { get; set; }
        public string vchUserAdmin { get; set; }
        public string Token { get; set; }
        public int intSitioID { get; set; }
        public string vchSitio { get; set; }
        public string vchEmail { get; set; }
        public string vchRutaIcono { get; set; }

        public clsUsuario()
        {
            intUsuarioID = int.MinValue;
            intTipoUsuario = int.MinValue;
            vchUsuario = string.Empty;
            vchPassword = string.Empty;
            vchNombre = string.Empty;
            datFecha = DateTime.MinValue;
            bitActivo = false;
            vchUserAdmin = string.Empty;
            Token = string.Empty;
            intSitioID = int.MinValue;
            vchSitio = string.Empty;
            vchEmail = string.Empty;
            vchRutaIcono = string.Empty;
        }
    }
}
