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
    
    public partial class tbl_CAT_Catalogo
    {
        public int intCatalogoID { get; set; }
        public string vchNombreCat { get; set; }
        public string vchTabla { get; set; }
        public string vchDescripcion { get; set; }
        public string vchIdentityTabla { get; set; }
        public Nullable<System.DateTime> datFecha { get; set; }
        public Nullable<bool> bitActivo { get; set; }
        public string vchUserAdmin { get; set; }
    }
}
