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
    public partial class frmAdminUser : System.Web.UI.Page
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
                            cargarTipoUsuario();
                            cargarUsuarios();
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
                Log.EscribeLog("Existe un error en Page_Load de frmAdminUser: " + ePL.Message, 3, "");
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
            catch(Exception ecU)
            {
                Log.EscribeLog("Existe un error en cargarUsuarios: " + ecU.Message, 3, Usuario.vchUsuario);
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
                        ddlTipoUsuario.Items.Insert(0, new ListItem("Seleccionar Tipo de Usuario...", "0"));
                    }
                }
                ddlTipoUsuario.DataBind();
            }
            catch (Exception ecTU)
            {
                Log.EscribeLog("Existe un error en cargarTipoUsuario de frmConfiguracion: " + ecTU.Message, 3, Usuario.vchUsuario);
            }
        }

        private void cargarListaSitios()
        {
            try
            {
                ddlSitioUser.DataSource = null;
                List<tbl_CAT_Sitio> lstSitio = new List<tbl_CAT_Sitio>();
                SitioRequest request = new SitioRequest();
                request.mdlUser = Usuario;
                lstSitio = RisService.getListSitios(request);
                if (lstSitio != null)
                {
                    if (lstSitio.Count > 0)
                    {
                        ddlSitioUser.DataSource = lstSitio;
                        ddlSitioUser.DataTextField = "vchNombreSitio";
                        ddlSitioUser.DataValueField = "intSitioID";
                        ddlSitioUser.Items.Insert(0, new ListItem("Seleccionar Sitio...", "0"));
                    }
                }
                ddlSitioUser.DataBind();
            }
            catch (Exception eclS)
            {
                Log.EscribeLog("Existe un error en cargarListaSitios: " + eclS.Message, 3, Usuario.vchUsuario);
            }
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
                Log.EscribeLog("Existe un error ddlBandeja_SelectedIndexChanged de frmConfiguraciones: " + eddS.Message, 3, Usuario.vchUsuario);
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
                Log.EscribeLog("Existe un error txtBandeja_TextChanged de frmConfiguraciones: " + ex.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void ddlTipoUsuario_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(ddlTipoUsuario.SelectedValue) > 1)
                {
                    ddlSitioUser.Enabled = true;
                    cargarListaSitios();
                }
                else
                {
                    ddlSitioUser.Enabled = false;
                    ddlSitioUser.DataSource = null;
                    ddlSitioUser.DataBind();
                }
            }
            catch (Exception eTIPo)
            {
                Log.EscribeLog("Existe un error en ddlTipoUsuario_SelectedIndexChanged: " + eTIPo.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void grvUsuario_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            try
            {
                grvUsuario.EditIndex = -1;
                cargarUsuarios();
            }
            catch (Exception eCE)
            {
                Log.EscribeLog("Existe un error en grvUsuario_RowCancelingEdit: " + eCE.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void grvUsuario_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                grvUsuario.EditIndex = e.NewEditIndex;
                cargarUsuarios();
            }
            catch (Exception eGU)
            {
                Log.EscribeLog("Existe un error en grvUsuario_RowEditing : " + eGU.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void grvUsuario_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //try
            //{
            //    AdminUserRequest request = new AdminUserRequest();
            //    AdminUserResponse response = new AdminUserResponse();
            //    request.mdlUser = Usuario;
            //    clsUsuario mdlVar = new clsUsuario();
            //    mdlVar.bitActivo = true;
            //    mdlVar.datFecha = DateTime.Now;
            //    TextBox txtNameUser = (TextBox)grvUsuario.Rows[e.RowIndex].FindControl("txtNombreUsuario");
            //    TextBox txtUser = (TextBox)grvUsuario.Rows[e.RowIndex].FindControl("txtUsuario");
            //    mdlVar.vchNombre = txtNameUser.Text.ToUpper();
            //    mdlVar.vchUsuario = txtUser.Text.ToUpper();
            //    mdlVar.intUsuarioID = Convert.ToInt16(grvUsuario.DataKeys[e.RowIndex].Values["intUsuarioID"].ToString());
            //    if (mdlVar != null)
            //    {
            //        request.mdlAdminUser = mdlVar;
            //        response = RisService.setActualizaUsuario(request);
            //        if (response != null)
            //        {
            //            if (response.Success)
            //            {
            //                ShowMessage("Se actualizo correctamente el usuario", MessageType.Correcto, "alert_container");
            //                grvUsuario.EditIndex = -1;
            //                cargarUsuarios();
            //            }
            //            else
            //            {
            //                ShowMessage("Existe un error: " + response.Success, MessageType.Error, "alert_container");
            //            }
            //        }
            //        else
            //        {
            //            ShowMessage("Favor de revisar la información.", MessageType.Error, "alert_container");
            //        }
            //    }
            //}
            //catch (Exception eUpdating)
            //{
            //    ShowMessage("Existe un error: " + eUpdating.Message, MessageType.Error, "alert_container");
            //}
        }
    }
}