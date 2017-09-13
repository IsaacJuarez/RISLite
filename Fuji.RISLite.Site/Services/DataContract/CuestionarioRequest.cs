using Fuji.RISLite.Entidades.DataBase;
using Fuji.RISLite.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fuji.RISLite.Site.Services.DataContract
{
    public class CuestionarioRequest
    {
        public clsUsuario mdlUser;
        public tbl_DET_Cuestionario mdlCuestionario;
        public int intCuestionarioID { get; set; }
        public int intPrestacionID { get; set; }

        public CuestionarioRequest()
        {
            mdlUser = new clsUsuario();
            mdlCuestionario = new tbl_DET_Cuestionario();
            intCuestionarioID = int.MinValue;
            intPrestacionID = int.MinValue;
        }
    }
}