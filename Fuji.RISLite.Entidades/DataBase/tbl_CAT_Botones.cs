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
    
    public partial class tbl_CAT_Botones
    {
        public tbl_CAT_Botones()
        {
            this.tbl_REL_TipoUsuarioBoton = new HashSet<tbl_REL_TipoUsuarioBoton>();
        }
    
        public int intBotonID { get; set; }
        public string vchNombreBoton { get; set; }
        public string vchIdentificador { get; set; }
        public string vchbtnImagenID { get; set; }
        public Nullable<bool> bitActivo { get; set; }
        public Nullable<System.DateTime> datFecha { get; set; }
        public string vchUserAdmin { get; set; }
    
        public virtual ICollection<tbl_REL_TipoUsuarioBoton> tbl_REL_TipoUsuarioBoton { get; set; }
    }
}
