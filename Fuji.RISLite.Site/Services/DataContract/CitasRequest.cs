using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using Microsoft.AspNet.SignalR;
using Fuji.RISLite.Entities;

namespace Fuji.RISLite.Site.Services.DataContract
{
    public class CitasRequest
    {
        public clsUsuario mdlUser;
        public clsEventoCita mdlevento;

        public CitasRequest()
        {
            mdlUser = new clsUsuario();
            mdlevento = new clsEventoCita();
        }
    }
}