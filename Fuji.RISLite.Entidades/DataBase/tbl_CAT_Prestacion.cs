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
    
    public partial class tbl_CAT_Prestacion
    {
        public tbl_CAT_Prestacion()
        {
            this.tbl_REL_ModalidadPrestacion = new HashSet<tbl_REL_ModalidadPrestacion>();
            this.tbl_DET_Restriccion = new HashSet<tbl_DET_Restriccion>();
            this.tbl_DET_Cuestionario = new HashSet<tbl_DET_Cuestionario>();
            this.tbl_DET_IndicacionPrestacion = new HashSet<tbl_DET_IndicacionPrestacion>();
        }
    
        public int intPrestacionID { get; set; }
        public string vchPrestacion { get; set; }
        public Nullable<int> intDuracionMin { get; set; }
        public Nullable<bool> bitActivo { get; set; }
        public Nullable<System.DateTime> datFecha { get; set; }
        public string vchUserAdmin { get; set; }
    
        public virtual ICollection<tbl_REL_ModalidadPrestacion> tbl_REL_ModalidadPrestacion { get; set; }
        public virtual ICollection<tbl_DET_Restriccion> tbl_DET_Restriccion { get; set; }
        public virtual ICollection<tbl_DET_Cuestionario> tbl_DET_Cuestionario { get; set; }
        public virtual ICollection<tbl_DET_IndicacionPrestacion> tbl_DET_IndicacionPrestacion { get; set; }
    }
}
