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
        public static List<stp_getListCatalogo_Result> lstCat = new List<stp_getListCatalogo_Result>();
        public static bool bitEdicion = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                String var = "";
                if (!IsPostBack)
                {
                    if (Session["User"] != null && Session["lstVistas"] != null)
                    {
                        List<clsVistasUsuarios> lstVista = (List<clsVistasUsuarios>)Session["lstVistas"];
                        if (lstVista != null)
                        {
                            string vista = "frmAdminCatalogo.aspx";
                            if (lstVista.Any(x => x.vchVistaIdentificador == vista))
                            {
                                Usuario = (clsUsuario)Session["User"];
                                if (Usuario != null)
                                {
                                    fillCat();
                                    bitEdicion = false;
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
                        var = Security.Encrypt("1");
                        Response.Redirect(URL + "/frmSalir.aspx?var=" + var);
                    }
                }
            }
            catch (Exception ePL)
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
            try
            {
                cargaCatalgo();
            }
            catch (Exception eddSC)
            {
                Log.EscribeLog("Existe un error en PageLoad de fillCat: " + eddSC.Message, 3, Usuario.vchUsuario);
            }
        }

        private void cargaCatalgo()
        {
            try
            {
                if (ddlListCatalogo.SelectedItem.Value != "" && ddlListCatalogo.SelectedItem.Value != "0")
                {
                    List<stp_getListCatalogo_Result> lst = new List<stp_getListCatalogo_Result>();
                    CatalogoRequest request = new CatalogoRequest();
                    clsUsuario user = new clsUsuario();
                    clsCatalogo cat = new clsCatalogo();
                    user = Usuario;
                    request.mdlUser = user;
                    cat.intCatalogoID = Convert.ToInt32(ddlListCatalogo.SelectedValue);
                    request.mdlCat = cat;
                    lst = RisService.getListCatalogo(request);
                    if (lst != null)
                    {
                        if (lst.Count > 0)
                        {
                            lstCat = lst;
                            grvCatalogo.DataSource = lst.OrderBy(x => x.vchCatalogoID).ToList();
                        }
                        else
                        {
                            lstCat = null;
                            grvCatalogo.DataSource = null;
                        }
                    }
                    else
                    {
                        lstCat = null;
                        grvCatalogo.DataSource = null;
                    }
                }
                else
                {
                    lstCat = null;
                    grvCatalogo.DataSource = null;
                }
                grvCatalogo.DataBind();
                limpiarControlAdd();
            }
            catch (Exception ecc)
            {
                Log.EscribeLog("Existe un error grvCatalogo_PageIndexChanging: " + ecc.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void grvCatalogo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                if (e.NewPageIndex >= 0)
                {
                    this.grvCatalogo.PageIndex = e.NewPageIndex;
                    cargaCatalgo();
                }
            }
            catch (Exception ex)
            {
                Log.EscribeLog("Existe un error grvCatalogo_PageIndexChanging: " + ex.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void grvCatalogo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Pager)
                {
                    Label lblTotalNumDePaginas = (Label)e.Row.FindControl("lblBandejaTotal");
                    lblTotalNumDePaginas.Text = grvCatalogo.PageCount.ToString();

                    TextBox txtIrAlaPagina = (TextBox)e.Row.FindControl("txtBandeja");
                    txtIrAlaPagina.Text = (grvCatalogo.PageIndex + 1).ToString();

                    DropDownList ddlTamPagina = (DropDownList)e.Row.FindControl("ddlBandeja");
                    ddlTamPagina.SelectedValue = grvCatalogo.PageSize.ToString();
                }

                if (e.Row.RowType != DataControlRowType.DataRow)
                {
                    return;
                }

                if (e.Row.DataItem != null)
                {
                    stp_getListCatalogo_Result _mdl = (stp_getListCatalogo_Result)e.Row.DataItem;
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

        protected void grvCatalogo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string intPrimaryCatalogoID = "";
                stp_getListCatalogo_Result mdl = new stp_getListCatalogo_Result();
                switch (e.CommandName)
                {
                    case "Estatus":
                        intPrimaryCatalogoID = e.CommandArgument.ToString();
                        mdl = lstCat.First(x => x.vchCatalogoID == intPrimaryCatalogoID);
                        mdl.bitActivo = !mdl.bitActivo;
                        CatalogoRequest request = new CatalogoRequest();
                        clsCatalogo cat = new clsCatalogo();
                        request.mdlUser = Usuario;
                        cat.intCatalogoID = Convert.ToInt32(ddlListCatalogo.SelectedValue.ToString());
                        cat.intPrimaryKey = Convert.ToInt32(intPrimaryCatalogoID);
                        cat.bitActivo = (bool)mdl.bitActivo;
                        request.mdlCat = cat;
                        stp_updateCatEstatus_Result result = new stp_updateCatEstatus_Result();
                        result = RisService.updateCatalogoEstatus(request);
                        if (result != null)
                        {
                            if(result.vchMensaje == "OK")
                            {
                                ShowMessage("Se actualizó correctamente.", MessageType.Correcto, "alert_container");
                                //fillCat();
                                cargaCatalgo();
                            }
                            else
                            {
                                ShowMessage("Existe un error al actualizar: " + result.vchDescripcion, MessageType.Error, "alert_container");
                            }
                        }
                        else
                        {
                            ShowMessage("Existe un error al actualizar, favor de revisar la información. ", MessageType.Correcto, "alert_container");
                        }
                        break;
                    case "viewEditar":
                        intPrimaryCatalogoID = e.CommandArgument.ToString();
                        mdl = lstCat.First(x => x.vchCatalogoID == intPrimaryCatalogoID);
                        txtValorCatalogo.Text = mdl.vchCatalogo;
                        lblCatalogo.Text = ddlListCatalogo.SelectedItem.Text;
                        lblPrimary.Text = "ID = " + mdl.vchCatalogoID;
                        bitEdicion = true;
                        break;
                }
            }
            catch (Exception eRU)
            {
                Log.EscribeLog("Existe un error grvCatalogo_RowCommand: " + eRU.Message, 3, Usuario.vchUsuario);
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

        protected void ddlBandeja_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DropDownList dropDownList = (DropDownList)sender;
                if (int.Parse(dropDownList.SelectedValue) != 0)
                {
                    this.grvCatalogo.AllowPaging = true;
                    this.grvCatalogo.PageSize = int.Parse(dropDownList.SelectedValue);
                }
                else
                    this.grvCatalogo.AllowPaging = false;
                this.cargaCatalgo();
            }
            catch (Exception eddS)
            {
                Log.EscribeLog("Existe un error ddlBandeja_SelectedIndexChanged: " + eddS.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void txtBandeja_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox txtBandejaAvaluosGoToPage = (TextBox)sender;
                int numeroPagina;
                if (int.TryParse(txtBandejaAvaluosGoToPage.Text.Trim(), out numeroPagina))
                    this.grvCatalogo.PageIndex = numeroPagina - 1;
                else
                    this.grvCatalogo.PageIndex = 0;
                this.cargaCatalgo();
            }
            catch (Exception ex)
            {
                Log.EscribeLog("Existe un error txtBandeja_TextChanged: " + ex.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void btnCancelCat_Click(object sender, EventArgs e)
        {
            try
            {
                limpiarControlAdd();
            }
            catch (Exception ebC)
            {
                Log.EscribeLog("Existe un error al cancelar btnCancelCat_Click: " + ebC.Message,3,Usuario.vchUsuario);
                ShowMessage("Existe un error al cancelar btnCancelCat_Click: " + ebC.Message, MessageType.Error, "alert_container");
            }
        }

        private void limpiarControlAdd()
        {
            try
            {
                lblCatalogo.Text = "";
                lblPrimary.Text = "ID: ";
                txtValorCatalogo.Text = "";
                bitEdicion = false;
            }
            catch(Exception eLC)
            {
                Log.EscribeLog("Existe un error en limpiarControlAdd: " + eLC.Message, 3, Usuario.vchUsuario);
                throw eLC;
            }
        }

        protected void btnAddItemCat_Click(object sender, EventArgs e)
        {
            try
            {
                CatalogoRequest request = new CatalogoRequest();
                clsUsuario user = new clsUsuario();
                clsCatalogo cat = new clsCatalogo();
                if (ddlListCatalogo.SelectedItem.Value != "" && ddlListCatalogo.SelectedItem.Value != "0")
                {
                    if (bitEdicion)//Editar
                    {
                        user = Usuario;
                        cat.vchUserAdmin = user.vchUsuario;
                        cat.intCatalogoID = Convert.ToInt32(ddlListCatalogo.SelectedValue.ToString());
                        cat.intPrimaryKey = Convert.ToInt32(lblPrimary.Text.Replace("ID =", "").Trim());
                        cat.vchValor = txtValorCatalogo.Text;
                        request.mdlCat = cat;
                        request.mdlUser = user;
                        stp_updateCatalogo_Result response = new stp_updateCatalogo_Result();
                        response = RisService.updateCatalogo(request);
                        if (response != null)
                        {
                            if (response.vchMensaje == "OK")
                            {
                                limpiarControlAdd();
                                cargaCatalgo();
                                ShowMessage("Cambios correctos.", MessageType.Correcto, "alert_container");
                            }
                            else
                            {
                                ShowMessage("Existe un error al guardar: " + response.vchDescripcion, MessageType.Error, "alert_container");
                            }
                        }
                    }
                    else//Agregar
                    {
                        user = Usuario;
                        cat.vchUserAdmin = user.vchUsuario;
                        cat.intCatalogoID = Convert.ToInt32(ddlListCatalogo.SelectedValue.ToString());
                        //cat.intPrimaryKey = Convert.ToInt32(lblPrimary.Text.Replace("ID =", "").Trim());
                        cat.vchValor = txtValorCatalogo.Text;
                        request.mdlCat = cat;
                        request.mdlUser = user;
                        stp_setItemCatalogo_Result response = new stp_setItemCatalogo_Result();
                        response = RisService.setItemCatalogo(request);
                        if (response != null)
                        {
                            if (response.vchMensaje == "OK")
                            {
                                limpiarControlAdd();
                                cargaCatalgo();
                                ShowMessage("Cambios correctos.", MessageType.Correcto, "alert_container");
                            }
                            else
                            {
                                ShowMessage("Existe un error al guardar: " + response.vchDescripcion, MessageType.Error, "alert_container");
                            }
                        }
                    }
                }
                else
                {
                    ShowMessage("Seleccionar el tipo de catálogo.", MessageType.Error, "alert_container");
                }
            }
            catch (Exception ebAI)
            {
                Log.EscribeLog("Existe un error en btnAddItemCat_Click: " + ebAI.Message, 3, Usuario.vchUsuario);
                ShowMessage("Existe un error al guardar: " + ebAI.Message, MessageType.Error, "alert_container");
            }
        }
    }
}