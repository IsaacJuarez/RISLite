//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Fuji.RISLite.Entidades.DataBase
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_CAT_HoraMuerta
    {
        public int intHorasMuertasID { get; set; }
        public System.TimeSpan tmeInicio { get; set; }
        public System.TimeSpan tmeFin { get; set; }
        public System.DateTime datFecha { get; set; }
        public string vchUserAdmin { get; set; }
        public bool bitActivo { get; set; }
    }
}