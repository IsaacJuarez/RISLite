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
    
    public partial class tbl_CAT_Vistas
    {
        public tbl_CAT_Vistas()
        {
            this.tbl_REL_BotonVista = new HashSet<tbl_REL_BotonVista>();
        }
    
        public int intVistaID { get; set; }
        public string vchNombreVista { get; set; }
        public string vchVistaIdentificador { get; set; }
        public string vchIconFontAwesome { get; set; }
        public Nullable<bool> bitActivo { get; set; }
        public Nullable<System.DateTime> datFecha { get; set; }
        public string vchUserAdmin { get; set; }
    
        public virtual ICollection<tbl_REL_BotonVista> tbl_REL_BotonVista { get; set; }
    }
}