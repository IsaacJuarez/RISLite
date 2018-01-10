using Fuji.RISLite.Entities;
using System.Collections.Generic;

namespace Fuji.RISLite.Site.Services.DataContract
{
    public class ArriboResponse
    {
        public bool Success { get; set; }
        public string mensaje { get; set; }
        public clsEstudioCita mdlCita;
        public List<clsEstudio> lstEstudio;

        public ArriboResponse()
        {
            Success = false;
            mensaje = string.Empty;
            mdlCita = new clsEstudioCita();
            lstEstudio = new List<clsEstudio>();
        }
    }
}