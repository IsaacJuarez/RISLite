using Fuji.RISLite.Entities;
using System.Collections.Generic;

namespace Fuji.RISLite.Site.Services.DataContract
{
    public class ArriboRequest
    {
        public clsUsuario mdlUser;
        public int intCitaID { get; set; }
        public clsEstudioCita mdlCita;
        public List<clsEstudio> lstEstudios;
        public int intEstudioID { get; set; }
        public int intEstatusID { get; set; }

        public ArriboRequest()
        {
            mdlUser = new clsUsuario();
            intCitaID = int.MinValue;
            mdlCita = new clsEstudioCita();
            lstEstudios = new List<clsEstudio>();
            intEstudioID = int.MinValue;
            intEstatusID = int.MinValue;
        }
    }
}