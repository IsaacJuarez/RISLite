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
    
    public partial class tbl_DET_DireccionPaciente
    {
        public int intDireccionID { get; set; }
        public Nullable<long> intPacienteID { get; set; }
        public Nullable<int> intCodigoPostalID { get; set; }
        public string vchCalle { get; set; }
        public string vchNumero { get; set; }
        public Nullable<bool> bitActivo { get; set; }
        public Nullable<System.DateTime> datFecha { get; set; }
        public string vchUserAdmin { get; set; }
    
        public virtual tbl_CAT_CodigoPostal tbl_CAT_CodigoPostal { get; set; }
        public virtual tbl_MST_Paciente tbl_MST_Paciente { get; set; }
    }
}
