using Fuji.RISLite.Entidades.DataBase;
using Fuji.RISLite.Entities;
using System.Collections.Generic;

namespace Fuji.RISLite.Site.Services.DataContract
{
    public class PacienteRequest
    {
        public clsUsuario mdlUser;
        public clsPaciente mdlPaciente;
        public clsDireccion mdlDireccion;
        public int intPacienteID { get; set; }
        public string busqueda { get; set; }
        public List<tbl_REL_IdentificacionPaciente> lstIdent;
        public List<tbl_DET_PacienteDinamico> lstVarAdic;

        public PacienteRequest()
        {
            mdlUser = new clsUsuario();
            mdlPaciente = new clsPaciente();
            mdlDireccion = new clsDireccion();
            intPacienteID = int.MinValue;
            busqueda = string.Empty;
            lstIdent = new List<tbl_REL_IdentificacionPaciente>();
            lstVarAdic = new List<tbl_DET_PacienteDinamico>();
        }
    }
}