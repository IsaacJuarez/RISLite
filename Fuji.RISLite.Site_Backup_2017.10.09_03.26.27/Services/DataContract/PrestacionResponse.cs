using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fuji.RISLite.Site.Services.DataContract
{
    public class PrestacionResponse
    {
        public bool Success { get; set; }
        public string Mensaje { get; set; }

        public PrestacionResponse()
        {
            Success = false;
            Mensaje = string.Empty;
        }
    }
}