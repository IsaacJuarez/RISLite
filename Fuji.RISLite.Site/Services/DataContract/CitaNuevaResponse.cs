using Fuji.RISLite.Entidades.DataBase;
using System.Collections.Generic;

namespace Fuji.RISLite.Site.Services.DataContract
{
    public class CitaNuevaResponse
    {
        public bool Success { get; set; }
        public string Mensaje { get; set; }
        public tbl_MST_Cita cita;
        public List<tbl_DET_Cita> mdlDetCita;

        public CitaNuevaResponse()
        {
            Success = false;
            Mensaje = string.Empty;
            cita = new tbl_MST_Cita();
            mdlDetCita = new List<tbl_DET_Cita>();
        }
    }
}