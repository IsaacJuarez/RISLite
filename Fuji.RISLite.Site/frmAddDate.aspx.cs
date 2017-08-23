using Fuji.RISLite.Entidades.Extensions;
using Fuji.RISLite.Entities;
using Fuji.RISLite.Site.Services;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace Fuji.RISLite.Site
{
    public partial class frmAddDate : System.Web.UI.Page
    {
        public string URL
        {
            get
            {
                return ConfigurationManager.AppSettings["URL"];
            }
        }
        RisLiteService RisService = new RisLiteService();
        public static clsUsuario Usuario = new clsUsuario();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                String var = "";
                if (!IsPostBack)
                {
                    if (Session["User"] != null)
                    {
                        Usuario = (clsUsuario)Session["User"];
                        if (Usuario != null)
                        {

                        }
                        else
                        {
                            var = Security.Encrypt("1");
                            Response.Redirect(URL + "/frmSalir.aspx?var=" + var);
                        }
                    }
                    else
                    {
                        var = Security.Encrypt("1");
                        Response.Redirect(URL + "/frmSalir.aspx?var=" + var);
                    }
                }
            }
            catch (Exception ePL)
            {
                Log.EscribeLog("Existe un error en Page_Load de frmAddDate: " + ePL.Message, 3, "");
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> obtenerPacienteBusqueda()
        {
            List<string> lstPaciente = new List<string>();
            try
            {
                string paciente1 = "Paciente 1 || NSS : 11111";
                lstPaciente.Add(paciente1);
                string paciente2 = "Paciente 2 || NSS : 22222";
                lstPaciente.Add(paciente2);
                string paciente3 = "Paciente 3 || NSS : 33333";
                lstPaciente.Add(paciente3);
            }
            catch (Exception eOP)
            {
                Log.EscribeLog("Existe un error obtenerPacienteBusqueda:" + eOP.Message, 3, "");
            }
            return lstPaciente;
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> obtenerEstudioBusqueda()
        {
            List<string> lstPaciente = new List<string>();
            try
            {
                string paciente1 = "Estudio 1 || Tipo :  US";
                lstPaciente.Add(paciente1);
                string paciente2 = "Estudio 2 || Tipo : RX";
                lstPaciente.Add(paciente2);
                string paciente3 = "Estudio 3 || Tipo : CT";
                lstPaciente.Add(paciente3);
            }
            catch (Exception eOP)
            {
                Log.EscribeLog("Existe un error obtenerEstudioBusqueda:" + eOP.Message, 3, "");
            }
            return lstPaciente;
        }
    }
}