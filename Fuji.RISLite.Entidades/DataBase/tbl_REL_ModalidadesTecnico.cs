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
    
    public partial class tbl_REL_ModalidadesTecnico
    {
        public int intRELModTecnicoID { get; set; }
        public Nullable<int> intUsuarioID { get; set; }
        public Nullable<int> intModalidadID { get; set; }
        public Nullable<bool> bitActivo { get; set; }
        public Nullable<System.DateTime> datFecha { get; set; }
        public string vchUserAdmin { get; set; }
    
        public virtual tbl_CAT_Modalidad tbl_CAT_Modalidad { get; set; }
        public virtual tbl_CAT_Usuario tbl_CAT_Usuario { get; set; }
    }
}
