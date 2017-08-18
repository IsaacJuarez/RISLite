﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fuji.RISLite.Entities
{
    public class clsEquipo
    {
        public int intEquipoID { get; set; }
        public int intModalidadID { get; set; }
        public string vchModalidad { get; set; }
        public string vchNombreEquipo { get; set; }
        public string vchCodigoEquipo { get; set; }
        public bool bitActivo { get; set; }
        public DateTime datFecha { get; set; }
        public string vchUserAdmin { get; set; }

        public clsEquipo()
        {
            intEquipoID = int.MinValue;
            intModalidadID = int.MinValue;
            vchModalidad = string.Empty;
            vchNombreEquipo = string.Empty;
            vchCodigoEquipo = string.Empty;
            bitActivo = false;
            datFecha = DateTime.MinValue;
            vchUserAdmin = string.Empty;
        }
    }
}
