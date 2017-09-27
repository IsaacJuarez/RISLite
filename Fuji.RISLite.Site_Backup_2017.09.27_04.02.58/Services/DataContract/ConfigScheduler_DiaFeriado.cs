using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Fuji.RISLite.Entities;

namespace Fuji.RISLite.Site.Services.DataContract
{    
    public class ConfigScheduler_DiaFeriado
    {
        public clsUsuario mdlUser;
        public clsDiaFeriado mdlDiaFeriado;

        public ConfigScheduler_DiaFeriado()
        {
            mdlUser = new clsUsuario();
            mdlDiaFeriado = new clsDiaFeriado();
        }
    }
}