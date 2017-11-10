using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Fuji.RISLite.Entities;

namespace Fuji.RISLite.Site.Services.DataContract
{
    public class ConfigGeneralAgenda
    {       
            public clsUsuario mdlUser;
            public clsGeneralConfigAgenda mdlgenconfagenda;

            public ConfigGeneralAgenda()
            {
                mdlUser = new clsUsuario();
                mdlgenconfagenda = new clsGeneralConfigAgenda();
            }      
    }
}