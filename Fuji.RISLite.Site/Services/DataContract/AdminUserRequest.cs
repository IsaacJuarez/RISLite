using Fuji.RISLite.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fuji.RISLite.Site.Services.DataContract
{
    public class AdminUserRequest
    {
        public clsUsuario mdlUser;
        public clsUsuario mdlAdminUser;

        public AdminUserRequest()
        {
            mdlAdminUser = new clsUsuario();
            mdlUser = new clsUsuario();
        }
    }
}