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
    
    public partial class tbl_CAT_InstitucionProcedencia
    {
        public tbl_CAT_InstitucionProcedencia()
        {
            this.tbl_MST_Cita = new HashSet<tbl_MST_Cita>();
        }
    
        public int intInstitucionID { get; set; }
        public string vchNombreIns { get; set; }
        public Nullable<bool> bitActivo { get; set; }
        public Nullable<System.DateTime> datFecha { get; set; }
        public string vchUserAdmin { get; set; }
        public Nullable<int> intSitioID { get; set; }
    
        public virtual tbl_CAT_Sitio tbl_CAT_Sitio { get; set; }
        public virtual ICollection<tbl_MST_Cita> tbl_MST_Cita { get; set; }
    }
}
