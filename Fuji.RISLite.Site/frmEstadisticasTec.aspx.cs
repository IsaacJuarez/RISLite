using Fuji.RISLite.Entidades.Extensions;
using Fuji.RISLite.Entities;
using Fuji.RISLite.Site.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Fuji.RISLite.Site
{
    public partial class frmEstadisticasTec : System.Web.UI.Page
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
                    if (Session["User"] != null && Session["lstVistas"] != null)
                    {
                        Usuario = (clsUsuario)Session["User"];
                        if (Security.ValidateToken(Usuario.Token, Usuario.intUsuarioID.ToString(), Usuario.vchUsuario))
                        {
                            List<clsVistasUsuarios> lstVista = (List<clsVistasUsuarios>)Session["lstVistas"];
                            if (lstVista != null)
                            {
                                string vista = "frmEstadisticasTec.aspx";
                                if (lstVista.Any(x => x.vchVistaIdentificador == vista))
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
                                    Response.Redirect(URL + "/frmSinPermiso.aspx");
                                }
                            }
                            else
                            {
                                Response.Redirect(URL + "/frmSinPermiso.aspx");
                            }
                        }
                        else
                        {
                            var = Security.Encrypt("4");
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
                Log.EscribeLog("Existe un error en Page_Load de frmEstadisticasTec: " + ePL.Message, 3, "");
            }
        }
    }
}