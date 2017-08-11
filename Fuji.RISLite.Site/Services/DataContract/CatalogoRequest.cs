
using Fuji.RISLite.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fuji.RISLite.Site.Services.DataContract
{
    public class CatalogoRequest
    {
        public clsUsuario mdlUser;

        public CatalogoRequest()
        {
            mdlUser = new clsUsuario();
        }
    }
}