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
    
    public partial class tbl_MST_Adicionales
    {
        public tbl_MST_Adicionales()
        {
            this.tbl_DET_Cita = new HashSet<tbl_DET_Cita>();
        }
    
        public int intAdicionalesID { get; set; }
        public Nullable<int> intTipoBotonID { get; set; }
        public Nullable<int> intTipoAdicional { get; set; }
        public string vchNombre { get; set; }
        public string vchURLImagen { get; set; }
        public Nullable<bool> bitIconBoostrap { get; set; }
        public Nullable<bool> bitObservaciones { get; set; }
        public Nullable<bool> bitActivo { get; set; }
        public Nullable<System.DateTime> datFecha { get; set; }
        public string vchUserAdmin { get; set; }
        public string vchHtmlControl { get; set; }
    
        public virtual tbl_CAT_TipoAdicional tbl_CAT_TipoAdicional { get; set; }
        public virtual tbl_CAT_TipoBoton tbl_CAT_TipoBoton { get; set; }
        public virtual ICollection<tbl_DET_Cita> tbl_DET_Cita { get; set; }
    }
}
