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
    
    public partial class tbl_Conf_CorreoSitio
    {
        public int intConfigCorreoID { get; set; }
        public string vchCorreo { get; set; }
        public string vchUsuarioCorreo { get; set; }
        public string vchPassword { get; set; }
        public string vchHost { get; set; }
        public Nullable<int> intPort { get; set; }
        public Nullable<bool> BitEnableSsl { get; set; }
        public Nullable<bool> bitActivo { get; set; }
        public Nullable<System.DateTime> datFecha { get; set; }
        public string vchUserAdmin { get; set; }
    }
}
