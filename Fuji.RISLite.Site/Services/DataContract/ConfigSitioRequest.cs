﻿using Fuji.RISLite.Entidades.DataBase;
using Fuji.RISLite.Entities;

namespace Fuji.RISLite.Site.Services.DataContract
{
    public class ConfigSitioRequest
    {
        public clsUsuario mdlUser;
        public tbl_MST_ConfiguracionSistema mdlConfig;

        public ConfigSitioRequest()
        {
            mdlUser = new clsUsuario();
            mdlConfig = new tbl_MST_ConfiguracionSistema();
        }
    }
}