using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fuji.RISLite.Site.Services.DataContract
{
    public class PerfilResponse
    {
        public bool success { get; set; }
        public string mensaje { get; set; }

        public PerfilResponse()
        {
            success = false;
            mensaje = string.Empty;
        }
    }
}