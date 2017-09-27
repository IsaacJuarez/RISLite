using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Fuji.RISLite.Entities;

namespace Fuji.RISLite.Site.Services.DataContract
{
    public class ConfigScheduler_DiaSemanaRequest
    {
        public clsUsuario mdlUser;
        public clsDiaSemana mdlDiaSemana;

        public ConfigScheduler_DiaSemanaRequest()
        {
            mdlUser = new clsUsuario();
            mdlDiaSemana = new clsDiaSemana();
        }
    }
}