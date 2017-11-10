using Fuji.RISLite.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fuji.RISLite.Site.Services.DataContract
{
    public class DireccionResponse
    {
        public string Mensaje { get; set; }
        public List<clsDireccion> lstDireccion;

        public DireccionResponse()
        {
            Mensaje = string.Empty;
            lstDireccion = new List<clsDireccion>();
        }
    }
}