﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fuji.RISLite.Site.Services.DataContract
{
    public class AdminUserResponse
    {
        public bool Success { get; set; }
        public string Mensaje { get; set; }

        public AdminUserResponse()
        {
            Success = false;
            Mensaje = string.Empty;
        }
    }
}