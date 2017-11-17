using Fuji.RISLite.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fuji.RISLite.Site.Services.DataContract
{
    public class CitaNuevaRequest
    {
        public clsUsuario mdlUser;
        public clsPaciente mdlPaciente;
        public List<clsAdicionales> lstAdicionales;
        public List<clsEstudioNuevaCita> lstEstudios;
        public int intCitaID { get; set; }
        public CitaNuevaRequest()
        {
            mdlUser = new clsUsuario();
            mdlPaciente = new clsPaciente();
            lstAdicionales = new List<clsAdicionales>();
            lstEstudios = new List<clsEstudioNuevaCita>();
            intCitaID = int.MinValue;
        }
    }
}