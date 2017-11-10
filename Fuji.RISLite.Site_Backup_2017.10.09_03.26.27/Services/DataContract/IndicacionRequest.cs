using Fuji.RISLite.Entidades.DataBase;
using Fuji.RISLite.Entities;
using System.Collections.Generic;

namespace Fuji.RISLite.Site.Services.DataContract
{
    public class IndicacionRequest
    {
        public clsUsuario mdlUser;
        public tbl_DET_IndicacionPrestacion mdlIndicacion;
        public int intIndicacionID { get; set; }
        public int intPrestacionID { get; set; }

        public IndicacionRequest()
        {
            mdlUser = new clsUsuario();
            mdlIndicacion = new tbl_DET_IndicacionPrestacion();
            intIndicacionID = int.MinValue;
            intPrestacionID = int.MinValue;
        }
    }
}