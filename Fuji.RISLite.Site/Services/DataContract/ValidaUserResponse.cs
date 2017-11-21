using Fuji.RISLite.Entities;
using System.Collections.Generic;

namespace Fuji.RISLite.Site.Services.DataContract
{
    public class ValidaUserResponse
    {
        public bool Success { get; set; }
        public clsUsuario mdlUser;
        public List<clsVistasUsuarios> lstVistas;
        public string mensaje { get; set; }

        public ValidaUserResponse()
        {
            Success = false;
            mdlUser = new clsUsuario();
            lstVistas = new List<clsVistasUsuarios>();
            mensaje = string.Empty;
        }
    }
}