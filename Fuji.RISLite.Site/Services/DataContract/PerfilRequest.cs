using Fuji.RISLite.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fuji.RISLite.Site.Services.DataContract
{
    public class PerfilRequest
    {
        public clsUsuario mdlUser;
        public clsUsuario mdlPerfil;
        public int intVariableID { get; set; }

        public PerfilRequest()
        {
            mdlUser = new clsUsuario();
            mdlPerfil = new clsUsuario();
            intVariableID = int.MinValue;
        }
    }
}