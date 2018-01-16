using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fuji.RISLite.Entities
{
   public class clsDetRestriccion
    {
        public int intReestriccionID { get; set; }
        public int intPrestacionID { get; set; }
        public string vchNombreReestriccion { get; set; }
        public string vchDetalle { get; set; }              
        public DateTime datFecha { get; set; }
        public string vchUserAdmin { get; set; }      

        public clsDetRestriccion()
        {
            intReestriccionID = int.MinValue;
            intPrestacionID = int.MinValue;
            vchNombreReestriccion = string.Empty;
            vchDetalle = string.Empty;          
            datFecha = DateTime.MinValue;
            vchUserAdmin = string.Empty;          
        }
    }
}
