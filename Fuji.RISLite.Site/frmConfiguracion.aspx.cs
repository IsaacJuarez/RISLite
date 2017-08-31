using Fuji.RISLite.Entidades.DataBase;
using Fuji.RISLite.Entidades.Extensions;
using Fuji.RISLite.Entities;
using Fuji.RISLite.Site.Services;
using Fuji.RISLite.Site.Services.DataContract;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Web.UI;
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
        public static bool bitActualiza = false;
        public static bool bitActualizaEmail = false;

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
                            Session["idActializa"] = 0;
                            Session["idActializaEmail"] = 0;
                            Session["logo"] = null;
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
                ShowMessage("Error al cargar la página: " + ePL.Message, MessageType.Error, "alert_container");
                Log.EscribeLog("Existe un error en Page_Load de frmConfiguracion: " + ePL.Message, 3, "");
            }
        }

        private void cargarForma()
        {
            try
            {
                cargarTipoUsuario();
                cargarUsuarios();
                cargarVarAdicionales();
                cargaConfigSistema();
                cargaConfigEmail();
            }
            catch(Exception ecF)
            {
                Log.EscribeLog("Existe un error en cargarForma: " + ecF.Message, 3, "");
            }
        }

        private void cargaConfigEmail()
        {
            try
            {
                ConfigEmailRequest request = new ConfigEmailRequest();
                ConfigEmailResponse response = new ConfigEmailResponse();
                request.mdlUser = Usuario;
                response = RisService.getConfigEmail(request);
                if (response != null)
                {
                    if (response.Success)
                    {
                        bitActualizaEmail = true;
                        fillConfigEmail(response.mldConfigEmail);
                    }
                    else
                    {
                        bitActualizaEmail = false;
                    }
                }
            }
            catch (Exception eCCS)
            {
                Log.EscribeLog("Existe un error en cargaConfigSistema: " + eCCS.Message, 3, Usuario.vchUsuario);
            }
        }

        private void fillConfigEmail(tbl_Conf_CorreoSitio mldConfigEmail)
        {
            try
            {
                Session["idActializaEmail"] = mldConfigEmail.intConfigCorreoID;
                txtEmailSistema.Text = mldConfigEmail.vchCorreo;
                txtPasswordSistema.Text = mldConfigEmail.vchPassword;
                txtHost.Text = mldConfigEmail.vchHost;
                txtPortCorreo.Text = mldConfigEmail.intPort.ToString();
                chkSSL.Checked = (bool)mldConfigEmail.BitEnableSsl;
            }
            catch (Exception eFCS)
            {
                Log.EscribeLog("Existe un error en fillConfigSistema: " + eFCS.Message, 3, Usuario.vchUsuario);
            }
        }

        private void cargaConfigSistema()
        {
            try
            {
                ConfigSitioRequest request = new ConfigSitioRequest();
                ConfigSitioResponse response = new ConfigSitioResponse();
                request.mdlUser = Usuario;
                response = RisService.getConfigSitio(request);
                if (response != null)
                {
                    if (response.Success)
                    {
                        bitActualiza = true;
                        fillConfigSistema(response.mdlConfig);
                    }
                    else
                    {
                        bitActualiza = false;
                    }
                }
            }
            catch (Exception eCCS)
            {
                Log.EscribeLog("Existe un error en cargaConfigSistema: " + eCCS.Message, 3, Usuario.vchUsuario);
            }
        }

        private void fillConfigSistema(tbl_MST_ConfiguracionSistema mdlConfig)
        {
            try
            {
                Session["idActializa"] = mdlConfig.intConfigID;
                txtNombreSitio.Text = mdlConfig.vchNombreSitio;
                txtDireccionSitio.Text = mdlConfig.vchDominio;
                if (mdlConfig.vbLogoSitio != null)
                {
                    Session["logo"] = mdlConfig.vbLogoSitio;
                    string base64String = Convert.ToBase64String(mdlConfig.vbLogoSitio, 0, mdlConfig.vbLogoSitio.Length);
                    imgLogo.ImageUrl = "data:image/png;base64," + base64String;
                    imgLogo.Visible = true;
                }
                txtPathRepositorio.Text = mdlConfig.vchVersion;

            }
            catch (Exception eFCS)
            {
                Log.EscribeLog("Existe un error en fillConfigSistema: " + eFCS.Message, 3, Usuario.vchUsuario);
            }
        }

        private void cargarVarAdicionales()
        {
            try
            {
                cargarPaciente();
            }
            catch(Exception ecva)
            {
                Log.EscribeLog("Existe un error en cargarVarAdicioles: " + ecva.Message, 3, Usuario.vchUsuario);
            }
        }

        private void cargarPaciente()
        {
            try
            {
                VarAdicionalRequest request = new VarAdicionalRequest();
                request.mdlUser = Usuario;
                List<clsVarAcicionales> lst = new List<clsVarAcicionales>();
                lst = RisService.getVariablesAdicionalPaciente(request);
                grvAddPaciente.DataSource = null;
                if (lst!= null)
                {
                    if (lst.Count > 0)
                    {
                        grvAddPaciente.DataSource = lst;
                    }
                }
                grvAddPaciente.DataBind();
            }
            catch(Exception ecP)
            {
                Log.EscribeLog("Existe un error en cargarPaciente: " + ecP.Message, 3, Usuario.vchUsuario);
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

        #region AdminUser
        protected void btnAgregar_Click(object sender, EventArgs e)
        {

        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {

        }
        #endregion AdminUser


        

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

        #region sistema
        protected void btnSaveConfig_Click(object sender, EventArgs e)
        {
            try
            {
                tbl_MST_ConfiguracionSistema mdlConfig = new tbl_MST_ConfiguracionSistema();
                mdlConfig = obtenerDatosSitio();
                if(mdlConfig!= null)
                {
                    ConfigSitioRequest request = new ConfigSitioRequest();
                    ConfigSitioResponse response = new ConfigSitioResponse();
                    request.mdlUser = Usuario;
                    request.mdlConfig = mdlConfig;
                    if (bitActualiza)
                    {
                        response = RisService.setActualizarConfigSitio(request);
                        if(response!= null)
                        {
                            if (response.Success)
                            {
                                ShowMessage("Cambios correctos.", MessageType.Correcto, "alert_container");
                                cargaConfigSistema();
                            }
                            else
                            {
                                ShowMessage("Existe un error: " + response.Mensaje, MessageType.Correcto, "alert_container");
                            }
                        }
                        else
                        {
                            ShowMessage("Verificar la información.", MessageType.Correcto, "alert_container");
                        }
                    }
                    else
                    {
                        response = RisService.setConfigSitio(request);
                        if (response != null)
                        {
                            if (response.Success)
                            {
                                ShowMessage("Cambios correctos.", MessageType.Correcto, "alert_container");
                                cargaConfigSistema();
                            }
                            else
                            {
                                ShowMessage("Existe un error: " + response.Mensaje, MessageType.Correcto, "alert_container");
                            }
                        }
                        else
                        {
                            ShowMessage("Verificar la información.", MessageType.Correcto, "alert_container");
                        }
                    }
                }
            }
            catch(Exception eSC)
            {
                Log.EscribeLog("Existe un error en btnSaveConfig_Click: " + eSC.Message, 3, Usuario.vchUsuario);
            }
        }

        private tbl_MST_ConfiguracionSistema obtenerDatosSitio()
        {
            tbl_MST_ConfiguracionSistema mdl = new tbl_MST_ConfiguracionSistema();
            try
            {
                mdl.bitActivo = true;
                mdl.datFecha = DateTime.Now;
                mdl.intConfigID = bitActualiza ? Convert.ToInt32(Session["idActializa"].ToString()) : 0;
                Byte[] bytes = null;
                if (fuLogo.HasFile)
                {
                    Stream fs = fuLogo.PostedFile.InputStream;
                    BinaryReader br = new BinaryReader(fs);
                    bytes = br.ReadBytes((Int32)fs.Length);
                }
                mdl.vbLogoSitio = fuLogo.HasFile ? bytes : (Byte[])Session["logo"];
                mdl.vchNombreSitio = txtNombreSitio.Text;
                mdl.vchDominio = txtDireccionSitio.Text;
                mdl.vchUserAdmin = Usuario.vchUsuario;
                mdl.vchVersion = txtPathRepositorio.Text;
            }
            catch(Exception eODS)
            {
                Log.EscribeLog("Existe un error en obtenerDatosSitio: " + eODS.Message, 3, Usuario.vchUsuario);
            }
            return mdl;
        }
        #endregion sistema

        #region email
        protected void btnSaveEmailSistema_Click(object sender, EventArgs e)
        {
            try
            {
                tbl_Conf_CorreoSitio mdlConfig = new tbl_Conf_CorreoSitio();
                mdlConfig = obtenerConfigEmail();
                if (mdlConfig != null)
                {
                    ConfigEmailRequest request = new ConfigEmailRequest();
                    ConfigEmailResponse response = new ConfigEmailResponse();
                    request.mdlUser = Usuario;
                    request.mdlEmail = mdlConfig;
                    if (bitActualizaEmail)
                    {
                        response = RisService.setActualizarConfigEmail(request);
                        if (response != null)
                        {
                            if (response.Success)
                            {
                                ShowMessage("Cambios correctos.", MessageType.Correcto, "alert_container");
                                cargaConfigEmail();
                            }
                            else
                            {
                                ShowMessage("Existe un error: " + response.Mensaje, MessageType.Correcto, "alert_container");
                            }
                        }
                        else
                        {
                            ShowMessage("Verificar la información.", MessageType.Correcto, "alert_container");
                        }
                    }
                    else
                    {
                        response = RisService.setConfigEmail(request);
                        if (response != null)
                        {
                            if (response.Success)
                            {
                                ShowMessage("Cambios correctos.", MessageType.Correcto, "alert_container");
                                cargaConfigEmail();
                            }
                            else
                            {
                                ShowMessage("Existe un error: " + response.Mensaje, MessageType.Correcto, "alert_container");
                            }
                        }
                        else
                        {
                            ShowMessage("Verificar la información.", MessageType.Correcto, "alert_container");
                        }
                    }
                }
            }
            catch(Exception eSEmail)
            {
                Log.EscribeLog("Existe un error en btnSaveEmailSistema_Click: " + eSEmail.Message, 3, Usuario.vchUsuario);
            }
        }

        private tbl_Conf_CorreoSitio obtenerConfigEmail()
        {
            tbl_Conf_CorreoSitio mdlConfig = new tbl_Conf_CorreoSitio();
            try
            {
                mdlConfig.bitActivo = true;
                mdlConfig.BitEnableSsl = chkSSL.Checked;
                mdlConfig.datFecha = DateTime.Now;
                mdlConfig.intConfigCorreoID = bitActualizaEmail ? Convert.ToInt32(Session["idActializaEmail"].ToString()) : 0;
                mdlConfig.intPort = Convert.ToInt32(txtPortCorreo.Text);
                mdlConfig.vchCorreo = txtEmailSistema.Text;
                mdlConfig.vchHost = txtHost.Text;
                mdlConfig.vchPassword = txtPasswordSistema.Text;
                mdlConfig.vchUserAdmin = Usuario.vchUsuario;
                mdlConfig.vchUsuarioCorreo = "";
            }
            catch(Exception eOCE)
            {
                Log.EscribeLog("Existe un error en obtenerConfigEmail: " + eOCE.Message, 3, Usuario.vchUsuario);
            }
            return mdlConfig;
        }
        #endregion email


        #region varAdicionales
        protected void btnBuscarVarAdi_Click(object sender, EventArgs e)
        {

        }

        protected void btnBuscarVarPac_Click(object sender, EventArgs e)
        {

        }

        protected void btnAddVarPac_Click(object sender, EventArgs e)
        {
            try
            {
                VarAdicionalRequest request = new VarAdicionalRequest();
                VarAdicionalResponse response = new VarAdicionalResponse();
                request.mdlUser = Usuario;
                clsVarAcicionales mdlVar = new clsVarAcicionales();
                mdlVar = obtenerVarAdicionalPaciente();
                if (mdlVar != null)
                {
                    request.mdlVariable = mdlVar;
                    request.intTipoVariable = 1;//Paciente
                    response = RisService.setAgregarVariable(request);
                    if(response!= null)
                    {
                        if (response.Success)
                        {
                            ShowMessage("Se agregó correctamente la variable", MessageType.Correcto, "alert_container");
                            cargarPaciente();
                        }
                        else
                        {
                            ShowMessage("Existe un error favor de revisar: " + response.Mensaje, MessageType.Error, "alert_container");
                        }
                    }
                    else
                    {
                        ShowMessage("Favor de revisar la información.", MessageType.Error, "alert_container");
                    }
                }
            }
            catch (Exception ebAdd)
            {
                Log.EscribeLog("Existe un error al agregar la variable:  " + ebAdd.Message, 3, Usuario.vchUsuario);
            }
        }

        private clsVarAcicionales obtenerVarAdicionalPaciente()
        {
            clsVarAcicionales mdl = new clsVarAcicionales();
            try
            {
                mdl.bitActivo = true;
                mdl.vchNombreVarAdi = txtAddVarPac.Text.ToUpper();
                mdl.datFecha = DateTime.Now;
            }
            catch(Exception eoVP)
            {
                Log.EscribeLog("Existe un error en obtenerVarAdicionalPaciente: " + eoVP.Message, 3, Usuario.vchUsuario);
            }
            return mdl;
        }

        protected void grvAddPaciente_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                if (e.NewPageIndex >= 0)
                {
                    this.grvAddPaciente.PageIndex = e.NewPageIndex;
                    cargarPaciente();
                }
            }
            catch (Exception ex)
            {
                Log.EscribeLog("Existe un error grvVista_PageIndexChanging: " + ex.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void grvAddPaciente_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int intVarAdiID = 0;
                clsUsuario mdl = new clsUsuario();
                switch (e.CommandName)
                {
                    case "Estatus":
                        intVarAdiID = Convert.ToInt32(e.CommandArgument.ToString());
                        VarAdicionalRequest request = new VarAdicionalRequest();
                        VarAdicionalResponse response = new VarAdicionalResponse();
                        request.mdlUser = Usuario;
                        request.intTipoVariable = 1;
                        clsVarAcicionales mdlVar = new clsVarAcicionales();
                        mdlVar.intVariableAdiID = intVarAdiID;
                        if (mdl != null)
                        {
                            request.mdlVariable = mdlVar;
                            response = RisService.setEstatusVariable(request);
                            if(response!= null)
                            {
                                if (response.Success)
                                {
                                    ShowMessage("Se actualizo correctamente la variable", MessageType.Correcto, "alert_container");
                                    //grvAddPaciente.EditIndex = -1;
                                    cargarPaciente();
                                }
                                else
                                {
                                    ShowMessage("Existe un error: " + response.Success, MessageType.Error, "alert_container");
                                }
                            }
                            else
                            {
                                ShowMessage("Favor de revisar la información.", MessageType.Error, "alert_container");
                            }
                        }
                        break;
                }
            }
            catch (Exception eRU)
            {
                Log.EscribeLog("Existe un error grvVista_RowCommand: " + eRU.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void grvAddPaciente_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Pager)
                {
                    Label lblTotalNumDePaginas = (Label)e.Row.FindControl("lblBandejaTotalPaciente");
                    lblTotalNumDePaginas.Text = grvUsuario.PageCount.ToString();

                    TextBox txtIrAlaPagina = (TextBox)e.Row.FindControl("txtBandejaPaciente");
                    txtIrAlaPagina.Text = (grvUsuario.PageIndex + 1).ToString();

                    DropDownList ddlTamPagina = (DropDownList)e.Row.FindControl("ddlBandejaPaciente");
                    ddlTamPagina.SelectedValue = grvUsuario.PageSize.ToString();
                }

                if (e.Row.RowType != DataControlRowType.DataRow)
                {
                    return;
                }

                if (e.Row.DataItem != null)
                {
                    clsVarAcicionales _mdl = (clsVarAcicionales)e.Row.DataItem;
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

        protected void grvAddPaciente_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grvAddPaciente.EditIndex = e.NewEditIndex;
            cargarPaciente();
        }

        protected void grvAddPaciente_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                VarAdicionalRequest request = new VarAdicionalRequest();
                VarAdicionalResponse response = new VarAdicionalResponse();
                request.mdlUser = Usuario;
                request.intTipoVariable = 1;//Paciente
                clsVarAcicionales mdlVar = new clsVarAcicionales();
                mdlVar.bitActivo = true;
                mdlVar.datFecha = DateTime.Now;
                TextBox txtNamevar = (TextBox)grvAddPaciente.Rows[e.RowIndex].FindControl("txtname");
                mdlVar.vchNombreVarAdi = txtNamevar.Text;
                mdlVar.intVariableAdiID = Convert.ToInt16(grvAddPaciente.DataKeys[e.RowIndex].Values["intVariableAdiID"].ToString());
                if (mdlVar != null)
                {
                    request.mdlVariable = mdlVar;
                    response = RisService.setActualizarVariable(request);
                    if (response != null)
                    {
                        if (response.Success)
                        {
                            ShowMessage("Se actualizo correctamente la variable", MessageType.Correcto, "alert_container");
                            grvAddPaciente.EditIndex = -1;
                            cargarPaciente();
                        }
                        else
                        {
                            ShowMessage("Existe un error: " + response.Success, MessageType.Error, "alert_container");
                        }
                    }
                    else
                    {
                        ShowMessage("Favor de revisar la información.", MessageType.Error, "alert_container");
                    }
                }
            }
            catch (Exception eUpdating)
            {
                ShowMessage("Existe un error: " + eUpdating.Message, MessageType.Error, "alert_container");
            }
        }

        protected void ddlBandejaPaciente_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DropDownList dropDownList = (DropDownList)sender;
                if (int.Parse(dropDownList.SelectedValue) != 0)
                {
                    this.grvAddPaciente.AllowPaging = true;
                    this.grvAddPaciente.PageSize = int.Parse(dropDownList.SelectedValue);
                }
                else
                    this.grvAddPaciente.AllowPaging = false;
                this.cargarPaciente();
            }
            catch (Exception eddS)
            {
                Log.EscribeLog("Existe un error ddlBandejaPaciente_SelectedIndexChanged de frmConfiguracion: " + eddS.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void txtBandejaPaciente_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox txtBandejaAvaluosGoToPage = (TextBox)sender;
                int numeroPagina;
                if (int.TryParse(txtBandejaAvaluosGoToPage.Text.Trim(), out numeroPagina))
                    this.grvAddPaciente.PageIndex = numeroPagina - 1;
                else
                    this.grvAddPaciente.PageIndex = 0;
                this.cargarPaciente();
            }
            catch (Exception ex)
            {
                Log.EscribeLog("Existe un error txtBandejaPaciente_TextChanged de frmConfiguracion: " + ex.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void grvAddPaciente_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            try
            {
                grvAddPaciente.EditIndex = -1;
                cargarPaciente();
            }
            catch (Exception eCE)
            {
                Log.EscribeLog("Existe un error en grvAddPaciente_RowCancelingEdit: " + eCE.Message, 3, Usuario.vchUsuario);
            }
        }
        #endregion varAdicionales

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

        
    }
}