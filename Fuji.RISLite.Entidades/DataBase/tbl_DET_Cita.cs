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
    
    public partial class tbl_DET_Cita
    {
        public long intDETCitaID { get; set; }
        public Nullable<long> intCitaID { get; set; }
        public Nullable<int> intAdicionalesID { get; set; }
        public string vchValor { get; set; }
        public Nullable<bool> bitDato { get; set; }
        public string vchObservaciones { get; set; }
        public Nullable<bool> bitActivo { get; set; }
        public Nullable<System.DateTime> datFecha { get; set; }
        public string vchUserAdmin { get; set; }
    
        public virtual tbl_MST_Adicionales tbl_MST_Adicionales { get; set; }
        public virtual tbl_MST_Cita tbl_MST_Cita { get; set; }
    }
}
