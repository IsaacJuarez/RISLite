using Fuji.RISLite.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fuji.RISLite.Site.Services.DataContract
{
    public class EstudioResponse
    {
        public clsEstudio mdlEstudio;
        public bool Success { get; set; }
        public string Mensaje { get; set; }

        public EstudioResponse()
        {
            mdlEstudio = new clsEstudio();
            Mensaje = string.Empty;
            Success = false;
        }
    }
}