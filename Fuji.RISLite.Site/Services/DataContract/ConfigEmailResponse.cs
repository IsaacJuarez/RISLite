using Fuji.RISLite.Entidades.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fuji.RISLite.Site.Services.DataContract
{
    public class ConfigEmailResponse
    {
        public bool Success { get; set; }
        public string Mensaje { get; set; }
        public tbl_Conf_CorreoSitio mldConfigEmail;

        public ConfigEmailResponse()
        {
            Success = false;
            Mensaje = string.Empty;
            mldConfigEmail = new tbl_Conf_CorreoSitio();
        }
    }
}