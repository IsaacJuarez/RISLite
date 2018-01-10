using Fuji.RISLite.Entidades.DataBase;
using Fuji.RISLite.Entidades.Extensions;
using Fuji.RISLite.Entities;
using Fuji.RISLite.Site.Services;
using Fuji.RISLite.Site.Services.DataContract;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Fuji.RISLite.Site
{
    public partial class frmConfiguraciones : System.Web.UI.Page
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
                    if (Session["UserRISAxon"] != null && Session["lstVistas"] != null)
                    {
                        Usuario = (clsUsuario)Session["UserRISAxon"];
                        if (Security.ValidateToken(Usuario.Token, Usuario.intUsuarioID.ToString(), Usuario.vchUsuario))
                        {
                            List<clsVistasUsuarios> lstVista = (List<clsVistasUsuarios>)Session["lstVistas"];
                            if (lstVista != null)
                            {
                                string vista = "frmConfiguraciones.aspx";
                                if (lstVista.Any(x => x.vchVistaIdentificador == vista))
                                {
                                    Usuario = (clsUsuario)Session["UserRISAxon"];
                                    if (Usuario != null)
                                    {
                                        cargarTipoUsuario();
                                        cargarTipoUsuarioAdd();
                                        cargarBotones();
                                        cargarVistas();
                                        if (ddlTipoUsuario.SelectedValue != "")
                                        {
                                            if (Convert.ToInt32(ddlTipoUsuario.SelectedValue) > 0)
                                            {
                                                cargagridVistas(Convert.ToInt32(ddlTipoUsuario.SelectedValue));
                                            }
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
                Log.EscribeLog("Existe un error en Page_Load de frmConfiguraciones: " + ePL.Message, 3, "");
            }
        }

        private void cargarVistas()
        {
            try
            {
                ddlVistaAdd.DataSource = null;
                CatalogoRequest request = new CatalogoRequest();
                List<clsCatalogo> lstReponse = new List<clsCatalogo>();
                request.mdlUser = Usuario;
                lstReponse = RisService.getListaVista(request);
                if (lstReponse != null)
                {
                    if (lstReponse.Count > 0)
                    {
                        ddlVistaAdd.DataSource = lstReponse;
                        ddlVistaAdd.DataValueField = "intCatalogoID";
                        ddlVistaAdd.DataTextField = "vchNombre";
                    }
                }
                ddlVistaAdd.DataBind();
            }
            catch (Exception ecTU)
            {
                Log.EscribeLog("Existe un error en cargarTipoUsuario de frmConfiguraciones: " + ecTU.Message, 3, Usuario.vchUsuario);
            }
        }

        private void cargarBotones()
        {
            try
            {
                ddlBotonAdd.DataSource = null;
                CatalogoRequest request = new CatalogoRequest();
                List<clsCatalogo> lstReponse = new List<clsCatalogo>();
                request.mdlUser = Usuario;
                lstReponse = RisService.getListaBoton(request);
                if (lstReponse != null)
                {
                    if (lstReponse.Count > 0)
                    {
                        ddlBotonAdd.DataSource = lstReponse;
                        ddlBotonAdd.DataValueField = "intCatalogoID";
                        ddlBotonAdd.DataTextField = "vchNombre";
                    }
                }
                ddlBotonAdd.DataBind();
            }
            catch (Exception ecTU)
            {
                Log.EscribeLog("Existe un error en cargarTipoUsuario de frmConfiguraciones: " + ecTU.Message, 3, Usuario.vchUsuario);
            }
        }

        private void cargarTipoUsuarioAdd()
        {
            try
            {
                ddlTipoUsuarioAdd.DataSource = null;
                CatalogoRequest request = new CatalogoRequest();
                List<clsCatalogo> lstReponse = new List<clsCatalogo>();
                request.mdlUser = Usuario;
                lstReponse = RisService.getTipoUsuario(request);
                if (lstReponse != null)
                {
                    if (lstReponse.Count > 0)
                    {
                        ddlTipoUsuarioAdd.DataSource = lstReponse;
                        ddlTipoUsuarioAdd.DataValueField = "intCatalogoID";
                        ddlTipoUsuarioAdd.DataTextField = "vchNombre";
                    }
                }
                ddlTipoUsuarioAdd.DataBind();
            }
            catch (Exception ecTU)
            {
                Log.EscribeLog("Existe un error en cargarTipoUsuario de frmConfiguraciones: " + ecTU.Message, 3, Usuario.vchUsuario);
            }
        }

        private void cargagridVistas(int v)
        {
            try
            {
                if(v > 0)
                {
                    grvVista.DataSource = null;
                    CatalogoRequest request = new CatalogoRequest();
                    request.mdlUser = Usuario;
                    request.mdlUser.intTipoUsuario = v;
                    List<stp_getListaPaginas_Result> result = new List<stp_getListaPaginas_Result>();
                    result = RisService.getListVistas(request);
                    if(result!= null)
                    {
                        if (result.Count > 0)
                        {
                            grvVista.DataSource = result;
                        }
                    }
                    grvVista.DataBind();
                }
            }
            catch(Exception ecV)
            {
                Log.EscribeLog("Existe un error en cargagridVistas: " + ecV.Message, 3, Usuario.vchUsuario);
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
                if(lstReponse!= null)
                {
                    if (lstReponse.Count > 0)
                    {
                        ddlTipoUsuario.DataSource = lstReponse;
                        ddlTipoUsuario.DataValueField = "intCatalogoID";
                        ddlTipoUsuario.DataTextField = "vchNombre";
                    }
                }
                ddlTipoUsuario.DataBind();
            }
            catch(Exception ecTU)
            {
                Log.EscribeLog("Existe un error en cargarTipoUsuario de frmConfiguraciones: " + ecTU.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void ddlTipoUsuario_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlTipoUsuario.SelectedValue != "")
                {
                    if (Convert.ToInt32(ddlTipoUsuario.SelectedValue) > 0)
                    {
                        cargagridVistas(Convert.ToInt32(ddlTipoUsuario.SelectedValue));
                    }
                }
            }
            catch(Exception eddC)
            {
                Log.EscribeLog("Existe un error en ddlTipoUsuario_SelectedIndexChanged: " + eddC.Message,3,Usuario.vchUsuario);
            }
        }

        protected void grvVista_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Pager)
                {
                    Label lblTotalNumDePaginas = (Label)e.Row.FindControl("lblBandejaTotal");
                    lblTotalNumDePaginas.Text = grvVista.PageCount.ToString();

                    TextBox txtIrAlaPagina = (TextBox)e.Row.FindControl("txtBandeja");
                    txtIrAlaPagina.Text = (grvVista.PageIndex + 1).ToString();

                    DropDownList ddlTamPagina = (DropDownList)e.Row.FindControl("ddlBandeja");
                    ddlTamPagina.SelectedValue = grvVista.PageSize.ToString();
                }

                if (e.Row.RowType != DataControlRowType.DataRow)
                {
                    return;
                }

                if (e.Row.DataItem != null)
                {
                    stp_getListaPaginas_Result _mdl = (stp_getListaPaginas_Result)e.Row.DataItem;
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
                Log.EscribeLog("Existe un error en grvVista_RowDataBound: " + eGUP.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void grvVista_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {
            try
            {
                if (e.NewPageIndex >= 0)
                {
                    this.grvVista.PageIndex = e.NewPageIndex;
                    cargagridVistas(Convert.ToInt32(ddlTipoUsuario.SelectedValue));
                }
            }
            catch (Exception ex)
            {
                Log.EscribeLog("Existe un error grvVista_PageIndexChanging: " + ex.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void grvVista_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            try
            {
                string intRELUsuarioBotonID = "";
                stp_getListaPaginas_Result mdl = new stp_getListaPaginas_Result();
                switch (e.CommandName)
                {
                    case "Estatus":
                        intRELUsuarioBotonID = e.CommandArgument.ToString();
                        break;
                    case "viewEditar":
                        intRELUsuarioBotonID = e.CommandArgument.ToString();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
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
                    this.grvVista.AllowPaging = true;
                    this.grvVista.PageSize = int.Parse(dropDownList.SelectedValue);
                }
                else
                    this.grvVista.AllowPaging = false;
                this.cargagridVistas(Convert.ToInt32(ddlTipoUsuario.SelectedValue));
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
                    this.grvVista.PageIndex = numeroPagina - 1;
                else
                    this.grvVista.PageIndex = 0;
                this.cargagridVistas(Convert.ToInt32(ddlTipoUsuario.SelectedValue));
            }
            catch (Exception ex)
            {
                Log.EscribeLog("Existe un error txtBandeja_TextChanged de frmConfiguraciones: " + ex.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void btnCancelarVista_Click(object sender, EventArgs e)
        {

        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception eARVB)
            {
                ShowMessage("Existe un error al agregar la vista.", MessageType.Error, "alert_container");
                Log.EscribeLog("Existe un error en btnAgregar_Click: " + eARVB.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void btnCancelAdd_Click(object sender, EventArgs e)
        {
            try
            {
                limpiarControlesVista();
            }
            catch (Exception eARVB)
            {
                ShowMessage("Existe un error al limpiar los controles.", MessageType.Error, "alert_container");
                Log.EscribeLog("Existe un error en btnCancelAdd_Click: " + eARVB.Message, 3, Usuario.vchUsuario);
            }
        }

        private void limpiarControlesVista()
        {
            try
            {

            }
            catch (Exception eARVB)
            {
                Log.EscribeLog("Existe un error en limpiarControlesVista: " + eARVB.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void btnAddRelVistaBoton_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch(Exception eARVB)
            {
                ShowMessage("Existe un error al agregar la vista.", MessageType.Error, "alert_container");
                Log.EscribeLog("Existe un error en btnAddRelVistaBoton_Click: " + eARVB.Message, 3, Usuario.vchUsuario);
            }
        }

        public enum MessageType { Correcto, Error, Informacion, Advertencia };

        protected void ShowMessage(string Message, MessageType type, String container)
        {
            try
            {
                Message = Message.Replace("'", " ");
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('" + Message + "','" + type + "','" + container + "');", true);
            }
            catch (Exception eSM)
            {
                throw eSM;
            }
        }

        protected void radAjaxPanelVista_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
        {
            try
            {
                if (ddlTipoUsuario.SelectedValue != "")
                {
                    if (Convert.ToInt32(ddlTipoUsuario.SelectedValue) > 0)
                    {
                        cargagridVistas(Convert.ToInt32(ddlTipoUsuario.SelectedValue));
                    }
                }
            }
            catch (Exception eddC)
            {
                Log.EscribeLog("Existe un error en radAjaxPanelVista_AjaxRequest: " + eddC.Message, 3, Usuario.vchUsuario);
                //
            }
        }
    }
}