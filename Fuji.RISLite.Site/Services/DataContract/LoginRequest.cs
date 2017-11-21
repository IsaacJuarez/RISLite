using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fuji.RISLite.Site.Services.DataContract
{
    public class LoginRequest
    {
        public string usuario { get; set; }
        public string password { get; set; }

        public LoginRequest()
        {
            usuario = string.Empty;
            password = string.Empty;
        }
    }
}