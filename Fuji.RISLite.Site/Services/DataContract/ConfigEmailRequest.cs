using Fuji.RISLite.Entidades.DataBase;
using Fuji.RISLite.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fuji.RISLite.Site.Services.DataContract
{
    public class ConfigEmailRequest
    {
        public clsUsuario mdlUser;
        public tbl_Conf_CorreoSitio mdlEmail;
        public int intSitioID { get; set; }

        public ConfigEmailRequest()
        {
            mdlEmail = new tbl_Conf_CorreoSitio();
            mdlUser = new clsUsuario();
            intSitioID = int.MinValue;
        }
    }
}