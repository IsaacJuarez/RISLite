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
    
    public partial class stp_getCitas_Result
    {
        public long intEstudioID { get; set; }
        public Nullable<long> intCitaID { get; set; }
        public Nullable<long> intPacienteID { get; set; }
        public string vchNombre { get; set; }
        public Nullable<System.DateTime> datFechaInicio { get; set; }
        public Nullable<int> intModalidadID { get; set; }
        public string vchModalidad { get; set; }
        public string vchPrestacion { get; set; }
        public Nullable<int> intEstatusEstudio { get; set; }
        public string vchEstatus { get; set; }
    }
}
