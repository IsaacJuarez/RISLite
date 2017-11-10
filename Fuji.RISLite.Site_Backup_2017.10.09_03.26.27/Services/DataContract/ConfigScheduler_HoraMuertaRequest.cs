using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Fuji.RISLite.Entities;


namespace Fuji.RISLite.Site.Services.DataContract
{
    public class ConfigScheduler_HoraMuertaRequest
    {
        public clsUsuario mdlUser;
        public clsHoraMuerta mdlHMScheduler;

        public ConfigScheduler_HoraMuertaRequest()
        {
            mdlUser = new clsUsuario();
            mdlHMScheduler = new clsHoraMuerta();
        }
    }
}