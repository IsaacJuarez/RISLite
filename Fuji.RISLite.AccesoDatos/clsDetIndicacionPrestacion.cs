using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fuji.RISLite.Entities
{
    public class clsDetIndicacionPrestacion
    {
        public int intIndicacionID { get; set; }
        public int intPrestacionID { get; set; }
        public string vchIndicacion { get; set; }
        public string vchComentario { get; set; }
        public DateTime datFecha { get; set; }
        public string vchUserAdmin { get; set; }

        public clsDetIndicacionPrestacion()
        {
            intIndicacionID = int.MinValue;
            intPrestacionID = int.MinValue;
            vchIndicacion = string.Empty;
            vchComentario = string.Empty;
            datFecha = DateTime.MinValue;
            vchUserAdmin = string.Empty;
        }
    }
}
