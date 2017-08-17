using Fuji.RISLite.Entidades.Extensions;
using Fuji.RISLite.Entities;
using Fuji.RISLite.Site.Services;
using System;
using System.Configuration;

namespace Fuji.RISLite.Site
{
    public partial class frmConfigAgenda : System.Web.UI.Page
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
                            cargarChecks();
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
                Log.EscribeLog("Existe un error en Page_Load de frmConfigAgenda: " + ePL.Message, 3, "");
            }
        }

        private void cargarChecks()
        {
            try
            {
                //string htmAdd = "<label class='btn btn-success '>" +
                //                    "<input type='checkbox' autocomplete='off' > L" +
                //                    "<span class='glyphicon glyphicon-ok'></span>" +
                //                "</label>" +
                //                "<label class='btn btn-primary '>" +
                //                    "<input type='checkbox' autocomplete='off' > M" +
                //                    "<span class='glyphicon glyphicon-ok'></span>" +
                //                "</label>" +
                //                "<label class='btn btn-info '>" +
                //                    " <input type='checkbox' autocomplete='off' > Mi" +
                //                    "  <span class='glyphicon glyphicon-ok'></span>" +
                //                "</label>" +
                //                "<label class='btn btn-default '>" +
                //                    "<input type='checkbox' autocomplete='off' > J" +
                //                    "<span class='glyphicon glyphicon-ok'></span>" +
                //                "</label>" +
                //                "<label class='btn btn-warning '>" +
                //                    "<input type='checkbox' autocomplete='off' > V" +
                //                    "<span class='glyphicon glyphicon-ok'></span>" +
                //                "</label>" +
                //                "<label class='btn btn-danger '>" +
                //                    "<input type='checkbox' autocomplete='off' > S" +
                //                    "<span class='glyphicon glyphicon-ok'></span>" +
                //                "</label>" +
                //                "<label class='btn btn-success '>" +
                //                    "<input type='checkbox' autocomplete='off' > D" +
                //                    "<span class='glyphicon glyphicon-ok'></span>" +
                //                "</label>";

                //divCheks.Controls.Add(new System.Web.UI.LiteralControl(htmAdd));
            }
            catch (Exception ecC)
            {
                Log.EscribeLog("Existe un error en cargarChecks: " + ecC.Message, 3, Usuario.vchNombre);
            }
        }
    }
}