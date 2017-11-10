using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Fuji.RISLite.Entities;

namespace Fuji.RISLite.Site.Services.DataContract
{
    public class ConfigDuracionModalidad
    {
        public clsUsuario mdlUser;
        public clsDuracionModalidad mdlduracionmodalidad;

        public ConfigDuracionModalidad()
        {
            mdlUser = new clsUsuario();
            mdlduracionmodalidad = new clsDuracionModalidad();
        }

    }
}