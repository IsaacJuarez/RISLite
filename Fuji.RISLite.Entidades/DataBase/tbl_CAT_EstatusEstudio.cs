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
    
    public partial class tbl_CAT_EstatusEstudio
    {
        public tbl_CAT_EstatusEstudio()
        {
            this.tbl_MST_Estudio = new HashSet<tbl_MST_Estudio>();
        }
    
        public int intEstatusEstudio { get; set; }
        public string vchEstatus { get; set; }
        public Nullable<bool> bitActivo { get; set; }
        public Nullable<System.DateTime> datFecha { get; set; }
        public string vchUserAdmin { get; set; }
        public string vchColor { get; set; }
    
        public virtual ICollection<tbl_MST_Estudio> tbl_MST_Estudio { get; set; }
    }
}
