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
    
    public partial class stp_getDetalleCitaPaciente_Result
    {
        public long intCitaID { get; set; }
        public Nullable<System.DateTime> datFechaCita { get; set; }
        public long intPacienteID { get; set; }
        public string vchNombrePaciente { get; set; }
        public long intEstudioID { get; set; }
        public Nullable<System.DateTime> datFechaFin { get; set; }
        public Nullable<int> intRELModPres { get; set; }
        public string vchModalidad { get; set; }
        public string vchPrestacion { get; set; }
        public Nullable<System.DateTime> datFechaInicio { get; set; }
        public Nullable<int> intEstatusEstudio { get; set; }
        public string vchEstatus { get; set; }
    }
}