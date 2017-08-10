using Fuji.RISLite.Entidades.DataBase;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fuji.RISLite.Entidades.Extensions
{
    public class Log
    {
        public static RISLiteEntities dbRisDA;

        public static void EscribeLog(String Mensaje, int TipoMensaje, string User)
        {
            try
            {
                

                //Database Log
                using (dbRisDA = new RISLiteEntities())
                {
                    tbl_MST_Bitacora mdl = new tbl_MST_Bitacora();
                    mdl.datFecha = DateTime.Now;
                    mdl.intTipoMensaje = TipoMensaje;
                    mdl.vchMensaje = Mensaje;
                    mdl.vchUserAdmin = User;
                    if (mdl != null)
                    {
                        dbRisDA.tbl_MST_Bitacora.Add(mdl);
                        dbRisDA.SaveChanges();
                    }
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine("Error en la escritura de la bitácora: " + exc.Message);
                string LogDirectory = ConfigurationManager.AppSettings["LogDirectory"].ToString();
                if (!Directory.Exists(LogDirectory))
                    Directory.CreateDirectory(LogDirectory);
                DateTime Fecha = DateTime.Now;
                string content = "[" + Fecha.ToString("yyyy/MM/dd HH:mm:ss") + "]" + " " + Mensaje;
                string ArchivoLog = LogDirectory + "RISLite[" + Fecha.ToShortDateString().Replace("/", "-") + "].txt";
                using (StreamWriter file = new StreamWriter(ArchivoLog, true))
                {
                    file.WriteLine(content);
                }
            }
        }
    }
}
