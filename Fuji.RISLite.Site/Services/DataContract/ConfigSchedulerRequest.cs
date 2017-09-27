using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Fuji.RISLite.Entities;

namespace Fuji.RISLite.Site.Services.DataContract
{
    public class ConfigSchedulerRequest
    {
        public clsUsuario mdlUser;
        public clsConfScheduler mdlConfScheduler;

        public ConfigSchedulerRequest()
        {
            mdlUser = new clsUsuario();
            mdlConfScheduler = new clsConfScheduler();
        }
    }
}