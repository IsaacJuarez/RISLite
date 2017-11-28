using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fuji.RISLite.Site.Services.DataContract
{
    public class ModTecnicoResponse
    {
        public bool success { get; set; }
        public string mensaje { get; set; }

        public ModTecnicoResponse()
        {
            success = false;
            mensaje = string.Empty;
        }
    }
}