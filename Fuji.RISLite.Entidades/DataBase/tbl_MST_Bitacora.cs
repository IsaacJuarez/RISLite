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
    
    public partial class tbl_MST_Bitacora
    {
        public int intBitacoraID { get; set; }
        public Nullable<int> intTipoMensaje { get; set; }
        public Nullable<System.DateTime> datFecha { get; set; }
        public string vchMensaje { get; set; }
        public string vchUserAdmin { get; set; }
    
        public virtual tbl_CAT_TipoMensaje tbl_CAT_TipoMensaje { get; set; }
    }
}
