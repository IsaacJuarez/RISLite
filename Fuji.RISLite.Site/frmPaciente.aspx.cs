using Fuji.RISLite.Entidades.Extensions;
using Fuji.RISLite.Entities;
using Fuji.RISLite.Site.Services;
using System;
using System.Configuration;
using System.Web.UI.WebControls;

namespace Fuji.RISLite.Site
{
    public partial class frmPaciente : System.Web.UI.Page
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
                            if (Request.QueryString.Count > 0)
                            {
                                String ID = Security.Decrypt(Request.QueryString["var"].ToString());
                                txtBusqueda.Text = ID;
                            }
                            else
                            {
                                txtBusqueda.Text = "";
                            }
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
                Log.EscribeLog("Existe un error en Page_Load de frmAgenda: " + ePL.Message, 3, "");
            }
        }

        protected void txtBusqueda_TextChanged(object sender, EventArgs e)
        {

        }

        protected void grvPacientes_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void grvPacientes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void grvPacientes_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void ddlBandeja_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void txtBandeja_TextChanged(object sender, EventArgs e)
        {

        }
    }
}