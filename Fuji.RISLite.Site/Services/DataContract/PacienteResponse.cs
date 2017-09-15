using Fuji.RISLite.Entidades.DataBase;
using Fuji.RISLite.Entities;
using System.Collections.Generic;

namespace Fuji.RISLite.Site.Services.DataContract
{
    public class PacienteResponse
    {
        public bool Success { get; set; }
        public string Mensaje { get; set; }
        public int intPacienteID { get; set; }
        public clsPaciente mdlPaciente;
        public clsDireccion mdlDireccion;
        public List<string> lstCadenas;
        public List<tbl_REL_IdentificacionPaciente> lstIden;
        public List<clsVarAcicionales> lstVarAdi;

        public PacienteResponse()
        {
            Success = false;
            Mensaje = string.Empty;
            intPacienteID = int.MinValue;
            mdlPaciente = new clsPaciente();
            mdlDireccion = new clsDireccion();
            lstCadenas = new List<string>();
            lstIden = new List<tbl_REL_IdentificacionPaciente>();
            lstVarAdi = new List<clsVarAcicionales>();
        }
    }
}