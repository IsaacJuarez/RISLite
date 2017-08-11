using Fuji.RISLite.Entidades.DataBase;
using Fuji.RISLite.Entidades.Extensions;
using Fuji.RISLite.Entities;
using Fuji.RISLite.Site.Services;
using Fuji.RISLite.Site.Services.DataContract;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Fuji.RISLite.Site
{
    public partial class frmAdminCatalogo : System.Web.UI.Page
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
                if(!IsPostBack)
                {
                    if(Session["User"]!= null)
                    {
                        Usuario = (clsUsuario)Session["User"];
                        if (Usuario != null)
                        {
                            fillCat();
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
            catch(Exception ePL)
            {
                Log.EscribeLog("Existe un error en PageLoad de frmAdminCatalogo: " + ePL.Message, 3, "");
            }
        }

        private void fillCat()
        {
            try
            {
                CatalogoRequest request = new CatalogoRequest();
                clsUsuario _user = new clsUsuario();
                _user = Usuario;
                request.mdlUser = _user;
                List<tbl_CAT_Catalogo> response = new List<tbl_CAT_Catalogo>();
                response = RisService.getListCatalogos(request);
                if(response != null)
                {
                    if(response.Count > 0)
                    {
                        ddlListCatalogo.DataSource = response;
                        ddlListCatalogo.DataTextField = "vchNombreCat";
                        ddlListCatalogo.DataValueField = "intCatalogoID";
                        ddlListCatalogo.DataBind();
                        ddlListCatalogo.Items.Insert(0, new ListItem("Seleccionar...", "0"));
                    }
                }
            }
            catch(Exception eFC)
            {
                Log.EscribeLog("Existe un error en PageLoad de fillCat: " + eFC.Message, 3, "");
            }
        }

        protected void ddlListCatalogo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void grvCatalogo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void grvCatalogo_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void grvCatalogo_RowCommand(object sender, GridViewCommandEventArgs e)
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