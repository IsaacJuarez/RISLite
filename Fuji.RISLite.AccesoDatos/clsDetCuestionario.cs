using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fuji.RISLite.Entities
{
    public class clsDetCuestionario
    {
        public int intDETCuestionarioID { get; set; }
        public int intPrestacionID { get; set; }
        public string vchCuestionario { get; set; }        
        public DateTime datFecha { get; set; }
        public string vchUserAdmin { get; set; }

        public clsDetCuestionario()
        {
            intDETCuestionarioID = int.MinValue;
            intPrestacionID = int.MinValue;
            vchCuestionario = string.Empty;            
            datFecha = DateTime.MinValue;
            vchUserAdmin = string.Empty;
        }
    }
}
