using Fuji.RISLite.Entidades.Extensions;
using Fuji.RISLite.Entities;
using Fuji.RISLite.Site.Services;
using Fuji.RISLite.Site.Services.DataContract;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.UI.WebControls;

namespace Fuji.RISLite.Site
{
    public partial class frmConfiguracion : System.Web.UI.Page
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
                    TabName.Value = Request.Form[TabName.UniqueID];
                    if (Session["User"] != null)
                    {
                        Usuario = (clsUsuario)Session["User"];
                        if (Usuario != null)
                        {
                            cargarForma();
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
                Log.EscribeLog("Existe un error en Page_Load de frmConfiguracion: " + ePL.Message, 3, "");
            }
        }

        private void cargarForma()
        {
            try
            {
                cargarTipoUsuario();
                cargarUsuarios();
            }
            catch(Exception ecF)
            {
                Log.EscribeLog("Existe un error en cargarForma: " + ecF.Message, 3, "");
            }
        }

        private void cargarTipoUsuario()
        {
            try
            {
                ddlTipoUsuario.DataSource = null;
                CatalogoRequest request = new CatalogoRequest();
                List<clsCatalogo> lstReponse = new List<clsCatalogo>();
                request.mdlUser = Usuario;
                lstReponse = RisService.getTipoUsuario(request);
                if (lstReponse != null)
                {
                    if (lstReponse.Count > 0)
                    {
                        ddlTipoUsuario.DataSource = lstReponse;
                        ddlTipoUsuario.DataValueField = "intCatalogoID";
                        ddlTipoUsuario.DataTextField = "vchNombre";
                        ddlTipoUsuario.Items.Insert(0, new ListItem("Seleccionar Tipp de Usuario...", "0"));
                    }
                }
                ddlTipoUsuario.DataBind();
            }
            catch (Exception ecTU)
            {
                Log.EscribeLog("Existe un error en cargarTipoUsuario de frmConfiguracion: " + ecTU.Message, 3, Usuario.vchUsuario);
            }
        }

        private void cargarUsuarios()
        {
            try
            {
                grvUsuario.DataSource = null;
                List<clsUsuario> lstTec = new List<clsUsuario>();
                TecnicoRequest request = new TecnicoRequest();
                request.mdlUser = Usuario;
                lstTec = RisService.getListaUsuarios(request);
                if (lstTec != null)
                {
                    if (lstTec.Count > 0)
                    {
                        grvUsuario.DataSource = lstTec;
                    }
                }
                grvUsuario.DataBind();
            }
            catch (Exception ecU)
            {
                Log.EscribeLog("Existe un error en cargarUsuarios: " + ecU.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void btnAddUser_Click(object sender, EventArgs e)
        {

        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {

        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {

        }

        protected void grvUsuario_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Pager)
                {
                    Label lblTotalNumDePaginas = (Label)e.Row.FindControl("lblBandejaTotal");
                    lblTotalNumDePaginas.Text = grvUsuario.PageCount.ToString();

                    TextBox txtIrAlaPagina = (TextBox)e.Row.FindControl("txtBandeja");
                    txtIrAlaPagina.Text = (grvUsuario.PageIndex + 1).ToString();

                    DropDownList ddlTamPagina = (DropDownList)e.Row.FindControl("ddlBandeja");
                    ddlTamPagina.SelectedValue = grvUsuario.PageSize.ToString();
                }

                if (e.Row.RowType != DataControlRowType.DataRow)
                {
                    return;
                }

                if (e.Row.DataItem != null)
                {
                    clsUsuario _mdl = (clsUsuario)e.Row.DataItem;
                    ImageButton ibtEstatus = (ImageButton)e.Row.FindControl("imbEstatus");
                    ibtEstatus.Attributes.Add("onclick", "javascript:return confirm('¿Desea realizar el cambio de estatus del item seleccionado?');");
                    if ((bool)_mdl.bitActivo)
                        ibtEstatus.ImageUrl = @"~/Images/ic_action_tick.png";
                    else
                        ibtEstatus.ImageUrl = @"~/Images/ic_action_cancel.png";
                }
            }
            catch (Exception eGUP)
            {
                throw eGUP;
            }
        }

        protected void grvUsuario_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                if (e.NewPageIndex >= 0)
                {
                    this.grvUsuario.PageIndex = e.NewPageIndex;
                    cargarUsuarios();
                }
            }
            catch (Exception ex)
            {
                Log.EscribeLog("Existe un error grvVista_PageIndexChanging: " + ex.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void grvUsuario_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string intUsuarioId = "";
                clsUsuario mdl = new clsUsuario();
                switch (e.CommandName)
                {
                    case "Estatus":
                        intUsuarioId = e.CommandArgument.ToString();
                        break;
                    case "viewEditar":
                        intUsuarioId = e.CommandArgument.ToString();
                        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                        break;
                }
            }
            catch (Exception eRU)
            {
                Log.EscribeLog("Existe un error grvVista_RowCommand: " + eRU.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void ddlBandeja_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DropDownList dropDownList = (DropDownList)sender;
                if (int.Parse(dropDownList.SelectedValue) != 0)
                {
                    this.grvUsuario.AllowPaging = true;
                    this.grvUsuario.PageSize = int.Parse(dropDownList.SelectedValue);
                }
                else
                    this.grvUsuario.AllowPaging = false;
                this.cargarUsuarios();
            }
            catch (Exception eddS)
            {
                Log.EscribeLog("Existe un error ddlBandeja_SelectedIndexChanged de frmConfiguracion: " + eddS.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void txtBandeja_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox txtBandejaAvaluosGoToPage = (TextBox)sender;
                int numeroPagina;
                if (int.TryParse(txtBandejaAvaluosGoToPage.Text.Trim(), out numeroPagina))
                    this.grvUsuario.PageIndex = numeroPagina - 1;
                else
                    this.grvUsuario.PageIndex = 0;
                this.cargarUsuarios();
            }
            catch (Exception ex)
            {
                Log.EscribeLog("Existe un error txtBandeja_TextChanged de frmConfiguracion: " + ex.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void btnSaveConfig_Click(object sender, EventArgs e)
        {

        }

        protected void btnSaveEmailSistema_Click(object sender, EventArgs e)
        {

        }
    }
}