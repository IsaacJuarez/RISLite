using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using Microsoft.AspNet.SignalR;
using Fuji.RISLite.Entities;

namespace Fuji.RISLite.Site.Services.DataContract
{
    public class AgendaRequest
    {
        public clsUsuario mdlUser;

        public clsConfAgenda mdlagenda;

        public AgendaRequest()
        {
            mdlUser = new clsUsuario();
            mdlagenda = new clsConfAgenda();
        }
    }
}