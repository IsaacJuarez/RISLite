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
    
    public partial class tbl_MST_ConfiguracionSistema
    {
        public int intConfigID { get; set; }
        public string vchNombreSitio { get; set; }
        public string vchDominio { get; set; }
        public string vchPrefijo { get; set; }
        public string vchVersion { get; set; }
        public byte[] vbLogoSitio { get; set; }
        public Nullable<bool> bitActivo { get; set; }
        public Nullable<System.DateTime> datFecha { get; set; }
        public string vchUserAdmin { get; set; }
    }
}
