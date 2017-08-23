using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fuji.RISLite.Entities
{
    public class clsVistasUsuarios
    {
        public int intTipoUsuario { get; set; }
        public string vchTipoUsuario { get; set; }
        public int intBotonID { get; set; }
        public string vchNombreBoton { get; set; }
        public string vchIdentificador { get; set; }
        public string vchbtnImagenID { get; set; }
        public int intVistaID { get; set; }
        public string vchNombreVista { get; set; }
        public string vchVistaIdentificador { get; set; }
        public string vchIconFontAwesome { get; set; }
        public bool bitActivo { get; set; }

        public clsVistasUsuarios()
        {
            intTipoUsuario = int.MinValue;
            vchTipoUsuario = string.Empty;
            intBotonID = int.MinValue;
            vchNombreBoton = string.Empty;
            vchIdentificador = string.Empty;
            vchbtnImagenID = string.Empty;
            intVistaID = int.MinValue;
            vchNombreVista = string.Empty;
            vchVistaIdentificador = string.Empty;
            vchIconFontAwesome = string.Empty;
            bitActivo = false;
        }
    }
}
