using Fuji.RISLite.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fuji.RISLite.Site.Services.DataContract
{
    public class EstudioRequest
    {
        public clsEstudio mdlEstudio;
        public clsUsuario mdlUser;
        public int intPacienteID { get; set; }

        public EstudioRequest()
        {
            mdlEstudio = new clsEstudio();
            mdlUser = new clsUsuario();
            intPacienteID = int.MinValue;
        }
    }
}