using Fuji.RISLite.Entidades.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fuji.RISLite.Site.Services.DataContract
{
    public class ConfigSitioResponse
    {
        public tbl_MST_ConfiguracionSistema mdlConfig;
        public bool Success { get; set; }
        public string Mensaje { get; set; }

        public ConfigSitioResponse()
        {
            mdlConfig = new tbl_MST_ConfiguracionSistema();
            Success = false;
            Mensaje = string.Empty;
        }
    }
}