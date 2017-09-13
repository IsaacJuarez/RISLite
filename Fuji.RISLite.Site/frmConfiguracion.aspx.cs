using Fuji.RISLite.Entidades.DataBase;
using Fuji.RISLite.Entidades.Extensions;
using Fuji.RISLite.Entities;
using Fuji.RISLite.Site.Services;
using Fuji.RISLite.Site.Services.DataContract;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
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
        public static List<clsUsuario> lstUsuarios = new List<clsUsuario>();
        public static List<stp_getListCatalogo_Result> lstCat = new List<stp_getListCatalogo_Result>();
        public static List<clsPrestacion> lstPres = new List<clsPrestacion>();
        public static List<tbl_CAT_Equipo> lstEquipo = new List<tbl_CAT_Equipo>();

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
                cargaCatalogo();
                cargaPrestaciones();
                cargaEquipo();
            }
            catch(Exception ecF)
            {
                Log.EscribeLog("Existe un error en cargarForma: " + ecF.Message, 3, "");
            }
        }

        private void cargaEquipo()
        {
            try
            {
                cargaListaModalidadEquipo();
                cargarEquipo();
            }
            catch (Exception ecP)
            {
                Log.EscribeLog("Existe un error en cargaPrestaciones: " + ecP.Message, 3, Usuario.vchUsuario);
            }
        }

        private void cargarEquipo()
        {
            try
            {
                if (ddlModalidadEquipo.SelectedItem.Value != "" && ddlModalidadEquipo.SelectedItem.Value != "0")
                {
                    List<tbl_CAT_Equipo> lst = new List<tbl_CAT_Equipo>();
                    EquipoRequest request = new EquipoRequest();
                    request.mdlUser = Usuario;
                    request.intModalidadID = Convert.ToInt32(ddlModalidadEquipo.SelectedValue);
                    lst = RisService.getListEquipo(request);
                    if (lst != null)
                    {
                        if (lst.Count > 0)
                        {
                            lstEquipo = lst;
                            grvEquipo.DataSource = lst.OrderBy(x => x.vchNombreEquipo).ToList();
                        }
                        else
                        {
                            lstEquipo = null;
                            grvEquipo.DataSource = null;
                        }
                    }
                    else
                    {
                        lstEquipo = null;
                        grvEquipo.DataSource = null;
                    }
                }
                else
                {
                    lstEquipo = null;
                    grvEquipo.DataSource = null;
                }
                grvEquipo.DataBind();
                //limpiarControlAdd();
            }
            catch (Exception ecc)
            {
                Log.EscribeLog("Existe un error cargarPrestacion: " + ecc.Message, 3, Usuario.vchUsuario);
            }
        }

        private void cargaListaModalidadEquipo()
        {
            try
            {
                CatalogoRequest request = new CatalogoRequest();
                clsUsuario _user = new clsUsuario();
                _user = Usuario;
                request.mdlUser = _user;
                List<tbl_CAT_Modalidad> response = new List<tbl_CAT_Modalidad>();
                response = RisService.getListModalidades(request);
                if (response != null)
                {
                    if (response.Count > 0)
                    {
                        ddlModalidadEquipo.DataSource = response;
                        ddlModalidadEquipo.DataTextField = "vchModalidad";
                        ddlModalidadEquipo.DataValueField = "intModalidadID";
                        ddlModalidadEquipo.DataBind();
                        ddlModalidadEquipo.Items.Insert(0, new ListItem("Seleccionar Modalidad...", "0"));
                    }
                }
            }
            catch (Exception eFC)
            {
                Log.EscribeLog("Existe un error en cargaListaModalidadEquipo: " + eFC.Message, 3, "");
            }
        }

        private void cargaPrestaciones()
        {
            try
            {
                cargaListaModalidad();
                cargarPrestacion();
            }
            catch(Exception ecP)
            {
                Log.EscribeLog("Existe un error en cargaPrestaciones: " + ecP.Message, 3, Usuario.vchUsuario);
            }
        }

        private void cargaListaModalidad()
        {
            try
            {
                CatalogoRequest request = new CatalogoRequest();
                clsUsuario _user = new clsUsuario();
                _user = Usuario;
                request.mdlUser = _user;
                List<tbl_CAT_Modalidad> response = new List<tbl_CAT_Modalidad>();
                response = RisService.getListModalidades(request);
                if (response != null)
                {
                    if (response.Count > 0)
                    {
                        ddlModalidad.DataSource = response;
                        ddlModalidad.DataTextField = "vchModalidad";
                        ddlModalidad.DataValueField = "intModalidadID";
                        ddlModalidad.DataBind();
                        ddlModalidad.Items.Insert(0, new ListItem("Seleccionar Modalidad...", "0"));
                    }
                }
            }
            catch (Exception eFC)
            {
                Log.EscribeLog("Existe un error en cargaListaModalidad: " + eFC.Message, 3, "");
            }
        }

        private void cargarPrestacion()
        {
            try
            {
                if (ddlModalidad.SelectedItem.Value != "" && ddlModalidad.SelectedItem.Value != "0")
                {
                    List<clsPrestacion> lst = new List<clsPrestacion>();
                    PrestacionRequest request = new PrestacionRequest();
                    request.mdlUser = Usuario;
                    request.intModalidad = Convert.ToInt32(ddlModalidad.SelectedValue);
                    lst = RisService.getListPrestacion(request);
                    if (lst != null)
                    {
                        if (lst.Count > 0)
                        {
                            lstPres = lst;
                            grvPrestaciones.DataSource = lst.OrderBy(x => x.vchPrestacion).ToList();
                        }
                        else
                        {
                            lstPres = null;
                            grvPrestaciones.DataSource = null;
                        }
                    }
                    else
                    {
                        lstPres = null;
                        grvPrestaciones.DataSource = null;
                    }
                }
                else
                {
                    lstPres = null;
                    grvPrestaciones.DataSource = null;
                }
                grvPrestaciones.DataBind();
                //limpiarControlAdd();
            }
            catch (Exception ecc)
            {
                Log.EscribeLog("Existe un error cargarPrestacion: " + ecc.Message, 3, Usuario.vchUsuario);
            }
        }

        private void cargaCatalogo()
        {
            try
            {
                cargaLista();
                cargaCatalogos();
            }
            catch (Exception ecc)
            {
                Log.EscribeLog("Existe un error cargaCatalogo: " + ecc.Message, 3, Usuario.vchUsuario);
            }
        }

        private void cargaLista()
        {
            try
            {
                CatalogoRequest request = new CatalogoRequest();
                clsUsuario _user = new clsUsuario();
                _user = Usuario;
                request.mdlUser = _user;
                List<tbl_CAT_Catalogo> response = new List<tbl_CAT_Catalogo>();
                response = RisService.getListCatalogos(request);
                if (response != null)
                {
                    if (response.Count > 0)
                    {
                        ddlCatalogo.DataSource = response;
                        ddlCatalogo.DataTextField = "vchNombreCat";
                        ddlCatalogo.DataValueField = "intCatalogoID";
                        ddlCatalogo.DataBind();
                        ddlCatalogo.Items.Insert(0, new ListItem("Seleccionar...", "0"));
                    }
                }
            }
            catch (Exception eFC)
            {
                Log.EscribeLog("Existe un error en cargaLista: " + eFC.Message, 3, "");
            }
        }

        private void cargaCatalogos()
        {
            try
            {
                if (ddlCatalogo.SelectedItem.Value != "" && ddlCatalogo.SelectedItem.Value != "0")
                {
                    List<stp_getListCatalogo_Result> lst = new List<stp_getListCatalogo_Result>();
                    CatalogoRequest request = new CatalogoRequest();
                    clsUsuario user = new clsUsuario();
                    clsCatalogo cat = new clsCatalogo();
                    user = Usuario;
                    request.mdlUser = user;
                    cat.intCatalogoID = Convert.ToInt32(ddlCatalogo.SelectedValue);
                    request.mdlCat = cat;
                    lst = RisService.getListCatalogo(request);
                    if (lst != null)
                    {
                        if (lst.Count > 0)
                        {
                            lstCat = lst;
                            grvCatalogos.DataSource = lst.OrderBy(x => x.vchCatalogoID).ToList();
                        }
                        else
                        {
                            lstCat = null;
                            grvCatalogos.DataSource = null;
                        }
                    }
                    else
                    {
                        lstCat = null;
                        grvCatalogos.DataSource = null;
                    }
                }
                else
                {
                    lstCat = null;
                    grvCatalogos.DataSource = null;
                }
                grvCatalogos.DataBind();
                //limpiarControlAdd();
            }
            catch (Exception ecc)
            {
                Log.EscribeLog("Existe un error cargaCatalogos: " + ecc.Message, 3, Usuario.vchUsuario);
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
                cargarCita();
                cargarIds();
            }
            catch(Exception ecva)
            {
                Log.EscribeLog("Existe un error en cargarVarAdicioles: " + ecva.Message, 3, Usuario.vchUsuario);
            }
        }

        private void cargarIds()
        {
            try
            {
                VarAdicionalRequest request = new VarAdicionalRequest();
                request.mdlUser = Usuario;
                List<tbl_CAT_Identificacion> lst = new List<tbl_CAT_Identificacion>();
                lst = RisService.getVariablesAdicionalID(request);
                grvVarID.DataSource = null;
                if (lst != null)
                {
                    if (lst.Count > 0)
                    {
                        grvVarID.DataSource = lst;
                    }
                }
                grvVarID.DataBind();
            }
            catch (Exception ecP)
            {
                Log.EscribeLog("Existe un error en cargarCita: " + ecP.Message, 3, Usuario.vchUsuario);
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

        private void cargarCita()
        {
            try
            {
                VarAdicionalRequest request = new VarAdicionalRequest();
                request.mdlUser = Usuario;
                List<clsVarAcicionales> lst = new List<clsVarAcicionales>();
                lst = RisService.getVariablesAdicionalCita(request);
                grvAddCita.DataSource = null;
                if (lst != null)
                {
                    if (lst.Count > 0)
                    {
                        grvAddCita.DataSource = lst;
                    }
                }
                grvAddCita.DataBind();
            }
            catch (Exception ecP)
            {
                Log.EscribeLog("Existe un error en cargarCita: " + ecP.Message, 3, Usuario.vchUsuario);
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
                lstUsuarios = null;
                List<clsUsuario> lstTec = new List<clsUsuario>();
                TecnicoRequest request = new TecnicoRequest();
                request.mdlUser = Usuario;
                lstTec = RisService.getListaUsuarios(request);
                if (lstTec != null)
                {
                    if (lstTec.Count > 0)
                    {
                        grvUsuario.DataSource = lstTec;
                        lstUsuarios = lstTec;
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
            try
            {
                clsUsuario userAdmin = new clsUsuario();
                userAdmin = ObtenerDatosUserAdmin();
                if (userAdmin != null)
                {
                    AdminUserRequest request = new AdminUserRequest();
                    AdminUserResponse response = new AdminUserResponse();
                    request.mdlUser = Usuario;
                    request.mdlAdminUser = userAdmin;
                    if (request != null)
                    {
                        response = RisService.setUsuario(request);
                        if (response != null)
                        {
                            if (response.Success)
                            {
                                ShowMessage("Se agregó correctamente. " + response.Mensaje, MessageType.Correcto, "alert_container");
                                txtNombre.Text = "";
                                txtUsuario.Text = "";
                                ddlTipoUsuario.SelectedIndex = 0;
                                cargarUsuarios();
                            }
                            else
                            {
                                ShowMessage("Existe un error, favor de verificar: " + response.Mensaje, MessageType.Advertencia, "alert_container");
                            }
                        }
                        else
                        {
                            ShowMessage("Existe un error, favor de verificar. ", MessageType.Error, "alert_container");
                        }
                    }
                }
                else
                {
                    ShowMessage("Verificar la información: ", MessageType.Advertencia, "alert_container");
                }
            }
            catch(Exception eAddUser)
            {
                Log.EscribeLog("Existe un error en btnAgregar_Click: " + eAddUser.Message, 3, Usuario.vchUsuario);
            }
        }

        private clsUsuario ObtenerDatosUserAdmin()
        {
            clsUsuario user = new clsUsuario();
            try
            {
                user.bitActivo = true;
                user.datFecha = DateTime.Now;
                user.intTipoUsuario = Convert.ToInt32(ddlTipoUsuario.SelectedValue.ToString());
                //user.intUsuarioID = 
                user.vchNombre = txtNombre.Text.ToUpper();
                user.vchUserAdmin = Usuario.vchUsuario.ToUpper();
                user.vchUsuario = txtUsuario.Text.ToUpper();
            }
            catch(Exception eODUA)
            {
                Log.EscribeLog("Existe un error en ObtenerDatosUserAdmin: " + eODUA.Message, 3, Usuario.vchUsuario);
            }
            return user;
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
                Log.EscribeLog("Existe un error en grvUsuario_RowEditing : " + eGU.Message,3,Usuario.vchUsuario);
            }
        }

        protected void grvUsuario_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                AdminUserRequest request = new AdminUserRequest();
                AdminUserResponse response = new AdminUserResponse();
                request.mdlUser = Usuario;
                clsUsuario mdlVar = new clsUsuario();
                mdlVar.bitActivo = true;
                mdlVar.datFecha = DateTime.Now;
                TextBox txtNameUser = (TextBox)grvUsuario.Rows[e.RowIndex].FindControl("txtNombreUsuario");
                TextBox txtUser = (TextBox)grvUsuario.Rows[e.RowIndex].FindControl("txtUsuario");
                mdlVar.vchNombre = txtNameUser.Text.ToUpper();
                mdlVar.vchUsuario = txtUser.Text.ToUpper();
                mdlVar.intUsuarioID = Convert.ToInt16(grvUsuario.DataKeys[e.RowIndex].Values["intUsuarioID"].ToString());
                if (mdlVar != null)
                {
                    request.mdlAdminUser = mdlVar;
                    response = RisService.setActualizaUsuario(request);
                    if (response != null)
                    {
                        if (response.Success)
                        {
                            ShowMessage("Se actualizo correctamente el usuario", MessageType.Correcto, "alert_container");
                            grvUsuario.EditIndex = -1;
                            cargarUsuarios();
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
                Log.EscribeLog("Existe un error en grvUsuario_RowDataBound: " + eGUP.Message, 3, Usuario.vchUsuario);
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
                Log.EscribeLog("Existe un error grvUsuario_PageIndexChanging: " + ex.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void grvUsuario_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int intUsuarioId = 0;
                clsUsuario mdl = new clsUsuario();
                switch (e.CommandName)
                {
                    case "Estatus":
                        intUsuarioId =Convert.ToInt32(e.CommandArgument.ToString());
                        AdminUserRequest request = new AdminUserRequest();
                        AdminUserResponse response = new AdminUserResponse();
                        request.mdlUser = Usuario;
                        clsUsuario mdlVar = new clsUsuario();
                        mdlVar.intUsuarioID = intUsuarioId;
                        if (mdl != null)
                        {
                            request.mdlAdminUser = mdlVar;
                            response = RisService.setEstatusUsuario(request);
                            if (response != null)
                            {
                                if (response.Success)
                                {
                                    ShowMessage("Se actualizo correctamente la variable", MessageType.Correcto, "alert_container");
                                    //grvAddPaciente.EditIndex = -1;
                                    cargarUsuarios();
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
                Log.EscribeLog("Existe un error grvUsuario_RowCommand: " + eRU.Message, 3, Usuario.vchUsuario);
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

        #endregion AdminUser

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
        //Paciente
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
                mdl.vchNombreVarAdi = txtNombreVarCita.Text.ToUpper();
                mdl.datFecha = DateTime.Now;
            }
            catch(Exception eoVP)
            {
                Log.EscribeLog("Existe un error en obtenerVarAdicionalPaciente: " + eoVP.Message, 3, Usuario.vchUsuario);
            }
            return mdl;
        }

        private clsVarAcicionales obtenerVarAdicionalCita()
        {
            clsVarAcicionales mdl = new clsVarAcicionales();
            try
            {
                mdl.bitActivo = true;
                mdl.vchNombreVarAdi = txtAddVarPac.Text.ToUpper();
                mdl.datFecha = DateTime.Now;
            }
            catch (Exception eoVP)
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
                Log.EscribeLog("Existe un error grvAddPaciente_PageIndexChanging: " + ex.Message, 3, Usuario.vchUsuario);
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
                    lblTotalNumDePaginas.Text = grvAddPaciente.PageCount.ToString();

                    TextBox txtIrAlaPagina = (TextBox)e.Row.FindControl("txtBandejaPaciente");
                    txtIrAlaPagina.Text = (grvAddPaciente.PageIndex + 1).ToString();

                    DropDownList ddlTamPagina = (DropDownList)e.Row.FindControl("ddlBandejaPaciente");
                    ddlTamPagina.SelectedValue = grvAddPaciente.PageSize.ToString();
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

        //Citas
        protected void btnAddVarCita_Click(object sender, EventArgs e)
        {
            try
            {
                VarAdicionalRequest request = new VarAdicionalRequest();
                VarAdicionalResponse response = new VarAdicionalResponse();
                request.mdlUser = Usuario;
                clsVarAcicionales mdlVar = new clsVarAcicionales();
                mdlVar = obtenerVarAdicionalCita();
                if (mdlVar != null)
                {
                    request.mdlVariable = mdlVar;
                    request.intTipoVariable = 2;//Cita
                    response = RisService.setAgregarVariable(request);
                    if (response != null)
                    {
                        if (response.Success)
                        {
                            ShowMessage("Se agregó correctamente la variable", MessageType.Correcto, "alert_container");
                            cargarCita();
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

        protected void grvAddCita_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Pager)
                {
                    Label lblTotalNumDePaginas = (Label)e.Row.FindControl("lblBandejaTotal");
                    lblTotalNumDePaginas.Text = grvAddCita.PageCount.ToString();

                    TextBox txtIrAlaPagina = (TextBox)e.Row.FindControl("txtBandejaCita");
                    txtIrAlaPagina.Text = (grvAddCita.PageIndex + 1).ToString();

                    DropDownList ddlTamPagina = (DropDownList)e.Row.FindControl("ddlBandejaCita");
                    ddlTamPagina.SelectedValue = grvAddCita.PageSize.ToString();
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

        protected void grvAddCita_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                if (e.NewPageIndex >= 0)
                {
                    this.grvAddCita.PageIndex = e.NewPageIndex;
                    cargarCita();
                }
            }
            catch (Exception ex)
            {
                Log.EscribeLog("Existe un error grvAddCita_PageIndexChanging: " + ex.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void grvAddCita_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            try
            {
                grvAddCita.EditIndex = -1;
                cargarCita();
            }
            catch (Exception eCE)
            {
                Log.EscribeLog("Existe un error en grvAddCita_RowCancelingEdit: " + eCE.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void grvAddCita_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                VarAdicionalRequest request = new VarAdicionalRequest();
                VarAdicionalResponse response = new VarAdicionalResponse();
                request.mdlUser = Usuario;
                request.intTipoVariable = 2;//Paciente
                clsVarAcicionales mdlVar = new clsVarAcicionales();
                mdlVar.bitActivo = true;
                mdlVar.datFecha = DateTime.Now;
                TextBox txtNamevar = (TextBox)grvAddCita.Rows[e.RowIndex].FindControl("txtname");
                mdlVar.vchNombreVarAdi = txtNamevar.Text;
                mdlVar.intVariableAdiID = Convert.ToInt16(grvAddCita.DataKeys[e.RowIndex].Values["intVariableAdiID"].ToString());
                if (mdlVar != null)
                {
                    request.mdlVariable = mdlVar;
                    response = RisService.setActualizarVariable(request);
                    if (response != null)
                    {
                        if (response.Success)
                        {
                            ShowMessage("Se actualizo correctamente la variable", MessageType.Correcto, "alert_container");
                            grvAddCita.EditIndex = -1;
                            cargarCita();
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

        protected void grvAddCita_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                grvAddCita.EditIndex = e.NewEditIndex;
                cargarCita();
            }
            catch(Exception eRED)
            {
                Log.EscribeLog("Existe un error en grvAddCita_RowEditing: " + eRED.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void grvAddCita_RowCommand(object sender, GridViewCommandEventArgs e)
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
                        request.intTipoVariable = 2;//Cita
                        clsVarAcicionales mdlVar = new clsVarAcicionales();
                        mdlVar.intVariableAdiID = intVarAdiID;
                        if (mdl != null)
                        {
                            request.mdlVariable = mdlVar;
                            response = RisService.setEstatusVariable(request);
                            if (response != null)
                            {
                                if (response.Success)
                                {
                                    ShowMessage("Se actualizo correctamente la variable", MessageType.Correcto, "alert_container");
                                    //grvAddPaciente.EditIndex = -1;
                                    cargarCita();
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
                Log.EscribeLog("Existe un error grvAddCita_RowCommand: " + eRU.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void txtBandejaCita_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox txtBandejaAvaluosGoToPage = (TextBox)sender;
                int numeroPagina;
                if (int.TryParse(txtBandejaAvaluosGoToPage.Text.Trim(), out numeroPagina))
                    this.grvAddCita.PageIndex = numeroPagina - 1;
                else
                    this.grvAddCita.PageIndex = 0;
                this.cargarCita();
            }
            catch (Exception ex)
            {
                Log.EscribeLog("Existe un error txtBandejaCita_TextChanged de frmConfiguracion: " + ex.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void ddlBandejaCita_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DropDownList dropDownList = (DropDownList)sender;
                if (int.Parse(dropDownList.SelectedValue) != 0)
                {
                    this.grvAddCita.AllowPaging = true;
                    this.grvAddCita.PageSize = int.Parse(dropDownList.SelectedValue);
                }
                else
                    this.grvAddCita.AllowPaging = false;
                this.cargarCita();
            }
            catch (Exception eddS)
            {
                Log.EscribeLog("Existe un error ddlBandejaCita_SelectedIndexChanged de frmConfiguracion: " + eddS.Message, 3, Usuario.vchUsuario);
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

        #region Catalogos
        protected void ddlCatalogo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cargaCatalogos();
            }
            catch (Exception eCat)
            {
                Log.EscribeLog("Existe un error en ddlCatalogo_SelectedIndexChanged: " + eCat.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void btnAddItemCat_Click(object sender, EventArgs e)
        {
            try
            {
                CatalogoRequest request = new CatalogoRequest();
                clsUsuario user = new clsUsuario();
                clsCatalogo cat = new clsCatalogo();
                if (ddlCatalogo.SelectedItem.Value != "" && ddlCatalogo.SelectedItem.Value != "0")
                {

                        user = Usuario;
                        cat.vchUserAdmin = user.vchUsuario;
                        cat.intCatalogoID = Convert.ToInt32(ddlCatalogo.SelectedValue.ToString());
                        cat.vchValor = txtItemCat.Text.ToUpper();
                        request.mdlCat = cat;
                        request.mdlUser = user;
                        stp_setItemCatalogo_Result response = new stp_setItemCatalogo_Result();
                        response = RisService.setItemCatalogo(request);
                        if (response != null)
                        {
                            if (response.vchMensaje == "OK")
                            {
                                txtItemCat.Text = "";
                                cargaCatalogos();
                                ShowMessage("Cambios correctos.", MessageType.Correcto, "alert_container");
                            }
                            else
                            {
                                ShowMessage("Existe un error al guardar: " + response.vchDescripcion, MessageType.Error, "alert_container");
                            }
                        }
                }
                else
                {
                    ShowMessage("Seleccionar el tipo de catálogo.", MessageType.Error, "alert_container");
                }
            }
            catch (Exception eCat)
            {
                ShowMessage("Existe un error al agregar el catálogo: " + eCat.Message, MessageType.Error, "alert_container");
                Log.EscribeLog("Existe un error en btnAddItemCat_Click: " + eCat.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void grvCatalogos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Pager)
                {
                    Label lblTotalNumDePaginas = (Label)e.Row.FindControl("lblBandejaTotal");
                    lblTotalNumDePaginas.Text = grvUsuario.PageCount.ToString();

                    TextBox txtIrAlaPagina = (TextBox)e.Row.FindControl("txtBandejaCat");
                    txtIrAlaPagina.Text = (grvUsuario.PageIndex + 1).ToString();

                    DropDownList ddlTamPagina = (DropDownList)e.Row.FindControl("ddlBandejaCat");
                    ddlTamPagina.SelectedValue = grvUsuario.PageSize.ToString();
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
                Log.EscribeLog("Existe un error en grvCatalogos_RowDataBound: " + eGUP.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void grvCatalogos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                if (e.NewPageIndex >= 0)
                {
                    this.grvCatalogos.PageIndex = e.NewPageIndex;
                    cargaCatalogos();
                }
            }
            catch (Exception ex)
            {
                Log.EscribeLog("Existe un error grvCatalogos_PageIndexChanging: " + ex.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void grvCatalogos_RowCommand(object sender, GridViewCommandEventArgs e)
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
                        cat.intCatalogoID = Convert.ToInt32(ddlCatalogo.SelectedValue.ToString());
                        cat.intPrimaryKey = Convert.ToInt32(intPrimaryCatalogoID);
                        cat.bitActivo = (bool)mdl.bitActivo;
                        request.mdlCat = cat;
                        stp_updateCatEstatus_Result result = new stp_updateCatEstatus_Result();
                        result = RisService.updateCatalogoEstatus(request);
                        if (result != null)
                        {
                            if (result.vchMensaje == "OK")
                            {
                                ShowMessage("Se actualizó correctamente.", MessageType.Correcto, "alert_container");
                                //fillCat();
                                cargaCatalogos();
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
                }
            }
            catch (Exception eRU)
            {
                Log.EscribeLog("Existe un error grvCatalogos_RowCommand: " + eRU.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void grvCatalogos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            try
            {
                grvCatalogos.EditIndex = -1;
                cargaCatalogos();
            }
            catch (Exception eCE)
            {
                Log.EscribeLog("Existe un error en grvCatalogos_RowCancelingEdit: " + eCE.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void grvCatalogos_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                CatalogoRequest request = new CatalogoRequest();
                clsCatalogo cat = new clsCatalogo();
                request.mdlUser = Usuario;
                clsUsuario mdlVar = new clsUsuario();
                TextBox txtItemNombre = (TextBox)grvCatalogos.Rows[e.RowIndex].FindControl("txtItemNombre");
                mdlVar.vchNombre = txtItemNombre.Text.ToUpper();
                cat.intCatalogoID = Convert.ToInt32(ddlCatalogo.SelectedValue.ToString());
                cat.intPrimaryKey = Convert.ToInt32(grvCatalogos.DataKeys[e.RowIndex].Values["vchCatalogoID"].ToString());
                cat.vchValor = txtItemNombre.Text.ToUpper();
                request.mdlCat = cat;
                stp_updateCatalogo_Result response = new stp_updateCatalogo_Result();
                response = RisService.updateCatalogo(request);
                if (response != null)
                {
                    if (response.vchMensaje == "OK")
                    {
                        ShowMessage("Cambios correctos.", MessageType.Correcto, "alert_container");
                        grvCatalogos.EditIndex = -1;
                        cargaCatalogos();
                    }
                    else
                    {
                        ShowMessage("Existe un error al guardar: " + response.vchDescripcion, MessageType.Error, "alert_container");
                    }
                }
            }
            catch (Exception eUpdating)
            {
                ShowMessage("Existe un error: " + eUpdating.Message, MessageType.Error, "alert_container");
            }
        }

        protected void grvCatalogos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                grvCatalogos.EditIndex = e.NewEditIndex;
                cargaCatalogos();
            }
            catch (Exception eGU)
            {
                Log.EscribeLog("Existe un error en grvCatalogos_RowEditing : " + eGU.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void ddlBandejaCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DropDownList dropDownList = (DropDownList)sender;
                if (int.Parse(dropDownList.SelectedValue) != 0)
                {
                    this.grvCatalogos.AllowPaging = true;
                    this.grvCatalogos.PageSize = int.Parse(dropDownList.SelectedValue);
                }
                else
                    this.grvCatalogos.AllowPaging = false;
                this.cargaCatalogos();
            }
            catch (Exception eddS)
            {
                Log.EscribeLog("Existe un error ddlBandejaCat_SelectedIndexChanged de frmConfiguracion: " + eddS.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void txtBandejaCat_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox txtBandejaAvaluosGoToPage = (TextBox)sender;
                int numeroPagina;
                if (int.TryParse(txtBandejaAvaluosGoToPage.Text.Trim(), out numeroPagina))
                    this.grvCatalogos.PageIndex = numeroPagina - 1;
                else
                    this.grvCatalogos.PageIndex = 0;
                this.cargaCatalogos();
            }
            catch (Exception ex)
            {
                Log.EscribeLog("Existe un error txtBandejaCat_TextChanged de frmConfiguracion: " + ex.Message, 3, Usuario.vchUsuario);
            }
        }
        #endregion Catalogos


        #region Prestacion
        protected void btnAddPres_Click(object sender, EventArgs e)
        {
            try
            {
                PrestacionRequest request = new PrestacionRequest();
                clsPrestacion prestacion = new clsPrestacion();
                if (ddlModalidad.SelectedItem.Value != "" && ddlModalidad.SelectedItem.Value != "0")
                {
                    prestacion.bitActivo = true;
                    prestacion.intModalidadID = Convert.ToInt32(ddlModalidad.SelectedValue.ToString());
                    prestacion.intDuracionMin = Convert.ToInt32(txtDuracionPres.Text);
                    prestacion.vchPrestacion = txtPrestacion.Text.ToUpper();
                    request.mdlPres = prestacion;
                    request.mdlUser = Usuario;
                    PrestacionResponse response = new PrestacionResponse();
                    response = RisService.setPrestacion(request);
                    if (response != null)
                    {
                        if (response.Success)
                        {
                            txtDuracionPres.Text = "";
                            txtPrestacion.Text = "";
                            cargarPrestacion();
                            ShowMessage("Cambios correctos.", MessageType.Correcto, "alert_container");
                        }
                        else
                        {
                            ShowMessage("Existe un error al guardar: " + response.Mensaje, MessageType.Error, "alert_container");
                        }
                    }
                }
                else
                {
                    ShowMessage("Seleccionar el tipo de modalidad.", MessageType.Error, "alert_container");
                }
            }
            catch (Exception eCat)
            {
                ShowMessage("Existe un error al agregar la prestación: " + eCat.Message, MessageType.Error, "alert_container");
                Log.EscribeLog("Existe un error en btnAddPres_Click: " + eCat.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void ddlModalidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cargarPrestacion();
            }
            catch (Exception eCat)
            {
                Log.EscribeLog("Existe un error en ddlModalidad_SelectedIndexChanged: " + eCat.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void grvPrestaciones_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Pager)
                {
                    Label lblTotalNumDePaginas = (Label)e.Row.FindControl("lblBandejaTotal");
                    lblTotalNumDePaginas.Text = grvUsuario.PageCount.ToString();

                    TextBox txtIrAlaPagina = (TextBox)e.Row.FindControl("txtBandejaPres");
                    txtIrAlaPagina.Text = (grvUsuario.PageIndex + 1).ToString();

                    DropDownList ddlTamPagina = (DropDownList)e.Row.FindControl("ddlBandejaPres");
                    ddlTamPagina.SelectedValue = grvUsuario.PageSize.ToString();
                }

                if (e.Row.RowType != DataControlRowType.DataRow)
                {
                    return;
                }

                if (e.Row.DataItem != null)
                {
                    clsPrestacion _mdl = (clsPrestacion)e.Row.DataItem;
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
                Log.EscribeLog("Existe un error en grvPrestaciones_RowDataBound: " + eGUP.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void grvPrestaciones_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                if (e.NewPageIndex >= 0)
                {
                    this.grvPrestaciones.PageIndex = e.NewPageIndex;
                    cargarPrestacion();
                }
            }
            catch (Exception ex)
            {
                Log.EscribeLog("Existe un error grvPrestaciones_PageIndexChanging: " + ex.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void grvPrestaciones_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            try
            {
                grvPrestaciones.EditIndex = -1;
                cargarPrestacion();
            }
            catch (Exception eCE)
            {
                Log.EscribeLog("Existe un error en grvPrestaciones_RowCancelingEdit: " + eCE.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void grvPrestaciones_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int intRELModPres = 0;
                int intPrestacionID = 0;
                clsPrestacion mdl = new clsPrestacion();
                switch (e.CommandName)
                {
                    case "Estatus":
                        intRELModPres = Convert.ToInt32(e.CommandArgument.ToString());
                        mdl = lstPres.First(x => x.intRELModPres == intRELModPres);
                        PrestacionRequest request = new PrestacionRequest();
                        request.mdlUser = Usuario;
                        request.intRELModPres = intRELModPres;
                        PrestacionResponse response = new PrestacionResponse();
                        response = RisService.setEstatusPrestacion(request);
                        if (response != null)
                        {
                            if (response.Success)
                            {
                                ShowMessage("Se actualizó correctamente.", MessageType.Correcto, "alert_container");
                                //fillCat();
                                cargarPrestacion();
                            }
                            else
                            {
                                ShowMessage("Existe un error al actualizar: " + response.Mensaje, MessageType.Error, "alert_container");
                            }
                        }
                        else
                        {
                            ShowMessage("Existe un error al actualizar, favor de revisar la información. ", MessageType.Advertencia, "alert_container");
                        }
                        break;

                    case "Cuestionario":
                        intPrestacionID = Convert.ToInt32(e.CommandArgument.ToString());
                        Session["intPrestacionID"] = intPrestacionID;
                        cargarCuestionarios();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#mdlCuestionarios').modal();", true);
                        break;
                    case "Indicacion":
                        intPrestacionID = Convert.ToInt32(e.CommandArgument.ToString());
                        Session["intPrestacionID"] = intPrestacionID;
                        cargarIndicaciones();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#mdlIndicaciones').modal();", true);
                        break;
                    case "Restricciones":
                        intPrestacionID = Convert.ToInt32(e.CommandArgument.ToString());
                        Session["intPrestacionID"] = intPrestacionID;
                        cargarRestricciones();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#mdlRestricciones').modal();", true);
                        break;
                }
            }
            catch (Exception eRU)
            {
                ShowMessage("Existe un error al actualizar, favor de revisar la información: " + eRU.Message, MessageType.Error, "alert_container");
                Log.EscribeLog("Existe un error grvPrestaciones_RowCommand: " + eRU.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void grvPrestaciones_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                grvPrestaciones.EditIndex = e.NewEditIndex;
                cargarPrestacion();
            }
            catch (Exception eRED)
            {
                Log.EscribeLog("Existe un error en grvPrestaciones_RowEditing: " + eRED.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void grvPrestaciones_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                PrestacionRequest request = new PrestacionRequest();
                clsPrestacion prestacion = new clsPrestacion();
                request.mdlUser = Usuario;
                TextBox txtItemNombre = (TextBox)grvPrestaciones.Rows[e.RowIndex].FindControl("txtItemNombre");
                TextBox txtDuracionItm = (TextBox)grvPrestaciones.Rows[e.RowIndex].FindControl("txtDuracionItem");
                prestacion.vchPrestacion = txtItemNombre.Text;
                prestacion.intDuracionMin = Convert.ToInt32(txtDuracionItm.Text);
                prestacion.intPrestacionID = Convert.ToInt32(grvPrestaciones.DataKeys[e.RowIndex].Values["intPrestacionID"].ToString());
                request.mdlPres = prestacion;
                PrestacionResponse response = new PrestacionResponse();
                response = RisService.setActualizaPrestacion(request);
                if (response != null)
                {
                    if (response.Success)
                    {
                        ShowMessage("Cambios correctos.", MessageType.Correcto, "alert_container");
                        grvPrestaciones.EditIndex = -1;
                        cargarPrestacion();
                    }
                    else
                    {
                        ShowMessage("Existe un error al guardar: " + response.Mensaje, MessageType.Error, "alert_container");
                    }
                }
                else
                {
                    ShowMessage("Existe un error, verificar la información.", MessageType.Error, "alert_container");
                }
            }
            catch (Exception eUpdating)
            {
                ShowMessage("Existe un error: " + eUpdating.Message, MessageType.Error, "alert_container");
                Log.EscribeLog("Exiete un error: " + eUpdating.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void ddlBandejaPres_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DropDownList dropDownList = (DropDownList)sender;
                if (int.Parse(dropDownList.SelectedValue) != 0)
                {
                    this.grvPrestaciones.AllowPaging = true;
                    this.grvPrestaciones.PageSize = int.Parse(dropDownList.SelectedValue);
                }
                else
                    this.grvPrestaciones.AllowPaging = false;
                this.cargarPrestacion();
            }
            catch (Exception eddS)
            {
                Log.EscribeLog("Existe un error ddlBandejaPres_SelectedIndexChanged de frmConfiguracion: " + eddS.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void txtBandejaPres_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox txtBandejaAvaluosGoToPage = (TextBox)sender;
                int numeroPagina;
                if (int.TryParse(txtBandejaAvaluosGoToPage.Text.Trim(), out numeroPagina))
                    this.grvPrestaciones.PageIndex = numeroPagina - 1;
                else
                    this.grvPrestaciones.PageIndex = 0;
                this.cargarPrestacion();
            }
            catch (Exception ex)
            {
                Log.EscribeLog("Existe un error txtBandejaPres_TextChanged de frmConfiguracion: " + ex.Message, 3, Usuario.vchUsuario);
            }
        }
        #endregion Prestacion

        #region Equipo
        protected void btnAddEquipo_Click(object sender, EventArgs e)
        {
            try
            {
                EquipoRequest request = new EquipoRequest();
                tbl_CAT_Equipo equipo = new tbl_CAT_Equipo();
                if (ddlModalidadEquipo.SelectedItem.Value != "" && ddlModalidadEquipo.SelectedItem.Value != "0")
                {
                    equipo.bitActivo = true;
                    equipo.intModalidadID = Convert.ToInt32(ddlModalidadEquipo.SelectedValue.ToString());
                    equipo.datFecha = DateTime.Now;
                    equipo.vchAETitle = txtAEtitle.Text.ToUpper();
                    equipo.vchCodigoEquipo = txtCodeequipo.Text.ToUpper();
                    equipo.vchIPEquipo = "";
                    equipo.vchNombreEquipo = txtNomEquipo.Text.ToUpper();
                    equipo.vchUserAdmin = Usuario.vchUsuario;
                    request.mdlEquipo = equipo;
                    request.mdlUser = Usuario;
                    EquipoResponse response = new EquipoResponse();
                    response = RisService.setEquipo(request);
                    if (response != null)
                    {
                        if (response.Success)
                        {
                            txtNomEquipo.Text = "";
                            txtCodeequipo.Text = "";
                            txtAEtitle.Text = "";
                            cargarEquipo();
                            ShowMessage("Cambios correctos.", MessageType.Correcto, "alert_container");
                        }
                        else
                        {
                            ShowMessage("Existe un error al guardar: " + response.Mensaje, MessageType.Error, "alert_container");
                        }
                    }
                }
                else
                {
                    ShowMessage("Seleccionar el tipo de modalidad.", MessageType.Error, "alert_container");
                }
            }
            catch (Exception eCat)
            {
                ShowMessage("Existe un error al agregar el equipo: " + eCat.Message, MessageType.Error, "alert_container");
                Log.EscribeLog("Existe un error en btnAddEquipo_Click: " + eCat.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void ddlModalidadEquipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cargarEquipo();
            }
            catch (Exception eCat)
            {
                Log.EscribeLog("Existe un error en ddlModalidad_SelectedIndexChanged: " + eCat.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void grvEquipo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Pager)
                {
                    Label lblTotalNumDePaginas = (Label)e.Row.FindControl("lblBandejaTotal");
                    lblTotalNumDePaginas.Text = grvUsuario.PageCount.ToString();

                    TextBox txtIrAlaPagina = (TextBox)e.Row.FindControl("txtBandejaEquipo");
                    txtIrAlaPagina.Text = (grvUsuario.PageIndex + 1).ToString();

                    DropDownList ddlTamPagina = (DropDownList)e.Row.FindControl("ddlBandejaEquipo");
                    ddlTamPagina.SelectedValue = grvUsuario.PageSize.ToString();
                }

                if (e.Row.RowType != DataControlRowType.DataRow)
                {
                    return;
                }

                if (e.Row.DataItem != null)
                {
                    tbl_CAT_Equipo _mdl = (tbl_CAT_Equipo)e.Row.DataItem;
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
                Log.EscribeLog("Existe un error en grvEquipo_RowDataBound: " + eGUP.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void grvEquipo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                if (e.NewPageIndex >= 0)
                {
                    this.grvEquipo.PageIndex = e.NewPageIndex;
                    cargarEquipo();
                }
            }
            catch (Exception ex)
            {
                Log.EscribeLog("Existe un error grvEquipo_PageIndexChanging: " + ex.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void grvEquipo_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            try
            {
                grvEquipo.EditIndex = -1;
                cargarEquipo();
            }
            catch (Exception eCE)
            {
                Log.EscribeLog("Existe un error en grvEquipo_RowCancelingEdit: " + eCE.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void grvEquipo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int intEquipoID = 0;
                tbl_CAT_Equipo mdl = new tbl_CAT_Equipo();
                switch (e.CommandName)
                {
                    case "Estatus":
                        intEquipoID = Convert.ToInt32(e.CommandArgument.ToString());
                        EquipoRequest request = new EquipoRequest();
                        request.mdlUser = Usuario;
                        request.intEquipoID = intEquipoID;
                        EquipoResponse response = new EquipoResponse();
                        response = RisService.setActualizaEquipo(request);
                        if (response != null)
                        {
                            if (response.Success)
                            {
                                ShowMessage("Se actualizó correctamente.", MessageType.Correcto, "alert_container");
                                //fillCat();
                                cargarEquipo();
                            }
                            else
                            {
                                ShowMessage("Existe un error al actualizar: " + response.Mensaje, MessageType.Error, "alert_container");
                            }
                        }
                        else
                        {
                            ShowMessage("Existe un error al actualizar, favor de revisar la información. ", MessageType.Advertencia, "alert_container");
                        }
                        break;
                }
            }
            catch (Exception eRU)
            {
                ShowMessage("Existe un error al actualizar, favor de revisar la información: " + eRU.Message, MessageType.Error, "alert_container");
                Log.EscribeLog("Existe un error grvEquipo_RowCommand: " + eRU.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void grvEquipo_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                grvEquipo.EditIndex = e.NewEditIndex;
                cargarEquipo();
            }
            catch (Exception eRED)
            {
                Log.EscribeLog("Existe un error en grvEquipo_RowEditing: " + eRED.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void grvEquipo_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                EquipoRequest request = new EquipoRequest();
                tbl_CAT_Equipo equipo = new tbl_CAT_Equipo();
                request.mdlUser = Usuario;
                equipo.bitActivo = true;
                equipo.datFecha = DateTime.Now;
                equipo.vchUserAdmin = Usuario.vchUsuario;
                TextBox txtItemNombre = (TextBox)grvEquipo.Rows[e.RowIndex].FindControl("txtItemNombre");
                TextBox txtCodigoItem = (TextBox)grvEquipo.Rows[e.RowIndex].FindControl("txtCodigoItem");
                TextBox txtAEtitleItem = (TextBox)grvEquipo.Rows[e.RowIndex].FindControl("txtAEtitleItem");
                equipo.vchNombreEquipo = txtItemNombre.Text;
                equipo.vchCodigoEquipo = txtCodeequipo.Text;
                equipo.vchAETitle = txtAEtitleItem.Text;
                equipo.intModalidadID = Convert.ToInt32(grvEquipo.DataKeys[e.RowIndex].Values["intModalidadID"].ToString());
                request.mdlEquipo = equipo;
                EquipoResponse response = new EquipoResponse();
                response = RisService.setActualizaEquipo(request);
                if (response != null)
                {
                    if (response.Success)
                    {
                        ShowMessage("Cambios correctos.", MessageType.Correcto, "alert_container");
                        grvPrestaciones.EditIndex = -1;
                        cargarPrestacion();
                    }
                    else
                    {
                        ShowMessage("Existe un error al guardar: " + response.Mensaje, MessageType.Error, "alert_container");
                    }
                }
                else
                {
                    ShowMessage("Existe un error, verificar la información.", MessageType.Error, "alert_container");
                }
            }
            catch (Exception eUpdating)
            {
                ShowMessage("Existe un error: " + eUpdating.Message, MessageType.Error, "alert_container");
                Log.EscribeLog("Exiete un error en grvEquipo_RowUpdating: " + eUpdating.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void ddlBandejaEquipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DropDownList dropDownList = (DropDownList)sender;
                if (int.Parse(dropDownList.SelectedValue) != 0)
                {
                    this.grvEquipo.AllowPaging = true;
                    this.grvEquipo.PageSize = int.Parse(dropDownList.SelectedValue);
                }
                else
                    this.grvEquipo.AllowPaging = false;
                this.cargarEquipo();
            }
            catch (Exception eddS)
            {
                Log.EscribeLog("Existe un error ddlBandejaEquipo_SelectedIndexChanged de frmConfiguracion: " + eddS.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void txtBandejaEquipo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox txtBandejaAvaluosGoToPage = (TextBox)sender;
                int numeroPagina;
                if (int.TryParse(txtBandejaAvaluosGoToPage.Text.Trim(), out numeroPagina))
                    this.grvEquipo.PageIndex = numeroPagina - 1;
                else
                    this.grvEquipo.PageIndex = 0;
                this.cargarEquipo();
            }
            catch (Exception ex)
            {
                Log.EscribeLog("Existe un error txtBandejaEquipo_TextChanged de frmConfiguracion: " + ex.Message, 3, Usuario.vchUsuario);
            }
        }
        #endregion Equipo

        #region IDS
        protected void btnAddVarID_Click(object sender, EventArgs e)
        {
            try
            {
                VarAdicionalRequest request = new VarAdicionalRequest();
                VarAdicionalResponse response = new VarAdicionalResponse();
                request.mdlUser = Usuario;
                clsVarAcicionales mdlVar = new clsVarAcicionales();
                mdlVar.bitActivo = true;
                mdlVar.datFecha = DateTime.Now;
                mdlVar.vchNombreVarAdi = txtIdentificacion.Text;
                if (mdlVar != null)
                {
                    request.mdlVariable = mdlVar;
                    request.intTipoVariable = 3;//Identificaciones
                    response = RisService.setAgregarVariable(request);
                    if (response != null)
                    {
                        if (response.Success)
                        {
                            ShowMessage("Se agregó correctamente la variable", MessageType.Correcto, "alert_container");
                            cargarIds();
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
                Log.EscribeLog("Existe un error al agregar la Identificación:  " + ebAdd.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void grvVarID_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Pager)
                {
                    Label lblTotalNumDePaginas = (Label)e.Row.FindControl("lblBandejaTotal");
                    lblTotalNumDePaginas.Text = grvVarID.PageCount.ToString();

                    TextBox txtIrAlaPagina = (TextBox)e.Row.FindControl("txtBandejaID");
                    txtIrAlaPagina.Text = (grvVarID.PageIndex + 1).ToString();

                    DropDownList ddlTamPagina = (DropDownList)e.Row.FindControl("ddlBandejaID");
                    ddlTamPagina.SelectedValue = grvVarID.PageSize.ToString();
                }

                if (e.Row.RowType != DataControlRowType.DataRow)
                {
                    return;
                }

                if (e.Row.DataItem != null)
                {
                    tbl_CAT_Identificacion _mdl = (tbl_CAT_Identificacion)e.Row.DataItem;
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

        protected void grvVarID_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                if (e.NewPageIndex >= 0)
                {
                    this.grvVarID.PageIndex = e.NewPageIndex;
                    cargarIds();
                }
            }
            catch (Exception ex)
            {
                Log.EscribeLog("Existe un error grvVarID_PageIndexChanging: " + ex.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void grvVarID_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            try
            {
                grvVarID.EditIndex = -1;
                cargarIds();
            }
            catch (Exception eCE)
            {
                Log.EscribeLog("Existe un error en grvVarID_RowCancelingEdit: " + eCE.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void grvVarID_RowCommand(object sender, GridViewCommandEventArgs e)
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
                        request.intTipoVariable = 3;//identificaciones
                        clsVarAcicionales mdlVar = new clsVarAcicionales();
                        mdlVar.intVariableAdiID = intVarAdiID;
                        if (mdlVar != null)
                        {
                            request.mdlVariable = mdlVar;
                            response = RisService.setEstatusVariable(request);
                            if (response != null)
                            {
                                if (response.Success)
                                {
                                    ShowMessage("Se actualizo correctamente la identificación", MessageType.Correcto, "alert_container");
                                    //grvAddPaciente.EditIndex = -1;
                                    cargarIds();
                                }
                                else
                                {
                                    ShowMessage("Existe un error: " + response.Mensaje, MessageType.Error, "alert_container");
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

        protected void grvVarID_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                grvVarID.EditIndex = e.NewEditIndex;
                cargarIds();
            }
            catch(Exception eRW)
            {
                Log.EscribeLog("Existe un error en grvVarID_RowEditing: " + eRW.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void grvVarID_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                VarAdicionalRequest request = new VarAdicionalRequest();
                VarAdicionalResponse response = new VarAdicionalResponse();
                request.mdlUser = Usuario;
                request.intTipoVariable = 3;//Identificaciones
                clsVarAcicionales mdlVar = new clsVarAcicionales();
                mdlVar.bitActivo = true;
                mdlVar.datFecha = DateTime.Now;
                TextBox txtNamevar = (TextBox)grvVarID.Rows[e.RowIndex].FindControl("txtname");
                mdlVar.vchNombreVarAdi = txtNamevar.Text;
                mdlVar.intVariableAdiID = Convert.ToInt16(grvVarID.DataKeys[e.RowIndex].Values["intIdentificacionID"].ToString());
                if (mdlVar != null)
                {
                    request.mdlVariable = mdlVar;
                    response = RisService.setActualizarVariable(request);
                    if (response != null)
                    {
                        if (response.Success)
                        {
                            ShowMessage("Se actualizo correctamente la identificación", MessageType.Correcto, "alert_container");
                            grvVarID.EditIndex = -1;
                            cargarIds();
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

        protected void ddlBandejaID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DropDownList dropDownList = (DropDownList)sender;
                if (int.Parse(dropDownList.SelectedValue) != 0)
                {
                    this.grvVarID.AllowPaging = true;
                    this.grvVarID.PageSize = int.Parse(dropDownList.SelectedValue);
                }
                else
                    this.grvVarID.AllowPaging = false;
                this.cargarIds();
            }
            catch (Exception eddS)
            {
                Log.EscribeLog("Existe un error ddlBandejaID_SelectedIndexChanged de frmConfiguracion: " + eddS.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void txtBandejaID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox txtBandejaAvaluosGoToPage = (TextBox)sender;
                int numeroPagina;
                if (int.TryParse(txtBandejaAvaluosGoToPage.Text.Trim(), out numeroPagina))
                    this.grvVarID.PageIndex = numeroPagina - 1;
                else
                    this.grvVarID.PageIndex = 0;
                this.cargarIds();
            }
            catch (Exception ex)
            {
                Log.EscribeLog("Existe un error txtBandejaID_TextChanged de frmConfiguracion: " + ex.Message, 3, Usuario.vchUsuario);
            }
        }
        #endregion IDS

        #region Indicaciones
        protected void btnCancelIndicaciones_Click(object sender, EventArgs e)
        {

        }

        protected void btnAddIndicaciones_Click(object sender, EventArgs e)
        {
            try
            {
                IndicacionRequest request = new IndicacionRequest();
                IndicacionResponse response = new IndicacionResponse();
                request.mdlUser = Usuario;
                tbl_DET_IndicacionPrestacion mdlInd = new tbl_DET_IndicacionPrestacion();
                mdlInd.bitActivo = true;
                mdlInd.datFecha = DateTime.Now;
                mdlInd.vchIndicacion = txtInstruccion.Text;
                mdlInd.intPrestacionID = Convert.ToInt32(Session["intPrestacionID"].ToString());
                if (mdlInd != null)
                {
                    request.mdlIndicacion = mdlInd;
                    response = RisService.setIndicacion(request);
                    if (response != null)
                    {
                        if (response.Success)
                        {
                            ShowMessage("Se agregó correctamente la indicación", MessageType.Correcto, "alert_container");
                            cargarIndicaciones();
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
                else
                {
                    ShowMessage("Favor de revisar la información.", MessageType.Error, "alert_container");
                }
            }
            catch (Exception ebAdd)
            {
                ShowMessage("Favor de revisar la información." + ebAdd.Message, MessageType.Error, "alert_container");
                Log.EscribeLog("Existe un error al agregar la indicación:  " + ebAdd.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void grvIndicaciones_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Pager)
                {
                    Label lblTotalNumDePaginas = (Label)e.Row.FindControl("lblBandejaTotal");
                    lblTotalNumDePaginas.Text = grvIndicaciones.PageCount.ToString();

                    TextBox txtIrAlaPagina = (TextBox)e.Row.FindControl("txtBandejaInd");
                    txtIrAlaPagina.Text = (grvIndicaciones.PageIndex + 1).ToString();

                    DropDownList ddlTamPagina = (DropDownList)e.Row.FindControl("ddlBandejaInd");
                    ddlTamPagina.SelectedValue = grvIndicaciones.PageSize.ToString();
                }

                if (e.Row.RowType != DataControlRowType.DataRow)
                {
                    return;
                }

                if (e.Row.DataItem != null)
                {
                    tbl_DET_IndicacionPrestacion _mdl = (tbl_DET_IndicacionPrestacion)e.Row.DataItem;
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

        protected void grvIndicaciones_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                if (e.NewPageIndex >= 0)
                {
                    this.grvIndicaciones.PageIndex = e.NewPageIndex;
                    cargarIndicaciones();
                }
            }
            catch (Exception ex)
            {
                Log.EscribeLog("Existe un error grvIndicaciones_PageIndexChanging: " + ex.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void grvIndicaciones_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            try
            {
                grvIndicaciones.EditIndex = -1;
                cargarIndicaciones();
            }
            catch (Exception eCE)
            {
                Log.EscribeLog("Existe un error en grvIndicaciones_RowCancelingEdit: " + eCE.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void grvIndicaciones_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int intIndicacionID = 0;
                switch (e.CommandName)
                {
                    case "Estatus":
                        intIndicacionID = Convert.ToInt32(e.CommandArgument.ToString());
                        IndicacionRequest request = new IndicacionRequest();
                        IndicacionResponse response = new IndicacionResponse();
                        request.mdlUser = Usuario;
                        request.intIndicacionID = intIndicacionID;
                        if (request != null)
                        {
                            response = RisService.setEstatusIndicacion(request);
                            if (response != null)
                            {
                                if (response.Success)
                                {
                                    ShowMessage("Se actualizo correctamente la indicación", MessageType.Correcto, "alert_container");
                                    //grvAddPaciente.EditIndex = -1;
                                    cargarIndicaciones();
                                }
                                else
                                {
                                    ShowMessage("Existe un error: " + response.Mensaje, MessageType.Error, "alert_container");
                                }
                            }
                            else
                            {
                                ShowMessage("Favor de revisar la información.", MessageType.Error, "alert_container");
                            }
                        }
                        else
                        {
                            ShowMessage("Favor de revisar la información.", MessageType.Error, "alert_container");
                        }
                        break;
                }
            }
            catch (Exception eRU)
            {
                ShowMessage("Favor de revisar la información: " + eRU.Message, MessageType.Error, "alert_container");
                Log.EscribeLog("Existe un error grvIndicaciones_RowCommand: " + eRU.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void grvIndicaciones_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                grvIndicaciones.EditIndex = e.NewEditIndex;
                cargarIndicaciones();
            }
            catch (Exception eRW)
            {
                Log.EscribeLog("Existe un error en grvIndicaciones_RowEditing: " + eRW.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void grvIndicaciones_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                IndicacionRequest request = new IndicacionRequest();
                IndicacionResponse response = new IndicacionResponse();
                request.mdlUser = Usuario;
                tbl_DET_IndicacionPrestacion mdlInd = new tbl_DET_IndicacionPrestacion();
                mdlInd.bitActivo = true;
                mdlInd.datFecha = DateTime.Now;
                TextBox txtNamevar = (TextBox)grvIndicaciones.Rows[e.RowIndex].FindControl("txtname");
                mdlInd.vchIndicacion = txtNamevar.Text;
                mdlInd.intIndicacionID = Convert.ToInt16(grvIndicaciones.DataKeys[e.RowIndex].Values["intIndicacionID"].ToString());
                mdlInd.intPrestacionID = Convert.ToInt16(grvIndicaciones.DataKeys[e.RowIndex].Values["intPrestacionID"].ToString());
                if (mdlInd != null)
                {
                    request.mdlIndicacion = mdlInd;
                    response = RisService.setActualizaIndicacion(request);
                    if (response != null)
                    {
                        if (response.Success)
                        {
                            ShowMessage("Se actualizo correctamente la indicación", MessageType.Correcto, "alert_container");
                            grvIndicaciones.EditIndex = -1;
                            cargarIndicaciones();
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

        protected void ddlBandejaInd_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DropDownList dropDownList = (DropDownList)sender;
                if (int.Parse(dropDownList.SelectedValue) != 0)
                {
                    this.grvIndicaciones.AllowPaging = true;
                    this.grvIndicaciones.PageSize = int.Parse(dropDownList.SelectedValue);
                }
                else
                    this.grvIndicaciones.AllowPaging = false;
                this.cargarIndicaciones();
            }
            catch (Exception eddS)
            {
                Log.EscribeLog("Existe un error ddlBandejaInd_SelectedIndexChanged de frmConfiguracion: " + eddS.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void txtBandejaInd_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox txtBandejaAvaluosGoToPage = (TextBox)sender;
                int numeroPagina;
                if (int.TryParse(txtBandejaAvaluosGoToPage.Text.Trim(), out numeroPagina))
                    this.grvIndicaciones.PageIndex = numeroPagina - 1;
                else
                    this.grvIndicaciones.PageIndex = 0;
                this.cargarIndicaciones();
            }
            catch (Exception ex)
            {
                Log.EscribeLog("Existe un error txtBandejaInd_TextChanged de frmConfiguracion: " + ex.Message, 3, Usuario.vchUsuario);
            }
        }

        private void cargarIndicaciones()
        {
            try
            {
                IndicacionRequest request = new IndicacionRequest();
                List<tbl_DET_IndicacionPrestacion> response = new List<tbl_DET_IndicacionPrestacion>();
                request.mdlUser = Usuario;
                request.intPrestacionID = Convert.ToInt32(Session["intPrestacionID"].ToString());
                response = RisService.getListIndicacion(request);
                grvIndicaciones.DataSource = null;
                grvIndicaciones.DataBind();
                if (response != null)
                {
                    if (response.Count > 0)
                    {
                        grvIndicaciones.DataSource = response;
                    }
                }
                grvIndicaciones.DataBind();
            }
            catch(Exception ecI)
            {
                Log.EscribeLog("Existe un error en cargarIndicaciones: " + ecI.Message, 3, Usuario.vchUsuario);
            }
        }

        #endregion Indicaciones

        #region Cuestionarios
        protected void btnCancelCuestionarios_Click(object sender, EventArgs e)
        {

        }

        protected void btnAddCuestionario_Click(object sender, EventArgs e)
        {
            try
            {
                CuestionarioRequest request = new CuestionarioRequest();
                CuestionarioResponse response = new CuestionarioResponse();
                request.mdlUser = Usuario;
                tbl_DET_Cuestionario mdlCuest = new tbl_DET_Cuestionario();
                mdlCuest.bitActivo = true;
                mdlCuest.datFecha = DateTime.Now;
                mdlCuest.vchCuestionario = txtCuestionario.Text;
                mdlCuest.intPrestacionID = Convert.ToInt32(Session["intPrestacionID"].ToString());
                if (mdlCuest != null)
                {
                    request.mdlCuestionario = mdlCuest;
                    response = RisService.setCuestionario(request);
                    if (response != null)
                    {
                        if (response.Success)
                        {
                            ShowMessage("Se agregó correctamente el cuestionario", MessageType.Correcto, "alert_container");
                            cargarCuestionarios();
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
                else
                {
                    ShowMessage("Favor de revisar la información.", MessageType.Error, "alert_container");
                }
            }
            catch (Exception ebAdd)
            {
                ShowMessage("Favor de revisar la información." + ebAdd.Message, MessageType.Error, "alert_container");
                Log.EscribeLog("Existe un error al agregar la indicación:  " + ebAdd.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void grvCuestionario_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                if (e.NewPageIndex >= 0)
                {
                    this.grvCuestionario.PageIndex = e.NewPageIndex;
                    cargarCuestionarios();
                }
            }
            catch (Exception ex)
            {
                Log.EscribeLog("Existe un error grvCuestionario_PageIndexChanging: " + ex.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void grvCuestionario_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Pager)
                {
                    Label lblTotalNumDePaginas = (Label)e.Row.FindControl("lblBandejaTotal");
                    lblTotalNumDePaginas.Text = grvCuestionario.PageCount.ToString();

                    TextBox txtIrAlaPagina = (TextBox)e.Row.FindControl("txtBandejaCues");
                    txtIrAlaPagina.Text = (grvCuestionario.PageIndex + 1).ToString();

                    DropDownList ddlTamPagina = (DropDownList)e.Row.FindControl("ddlBandejaCues");
                    ddlTamPagina.SelectedValue = grvCuestionario.PageSize.ToString();
                }

                if (e.Row.RowType != DataControlRowType.DataRow)
                {
                    return;
                }

                if (e.Row.DataItem != null)
                {
                    tbl_DET_Cuestionario _mdl = (tbl_DET_Cuestionario)e.Row.DataItem;
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

        protected void grvCuestionario_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            try
            {
                grvCuestionario.EditIndex = -1;
                cargarCuestionarios();
            }
            catch (Exception eCE)
            {
                Log.EscribeLog("Existe un error en grvCuestionario_RowCancelingEdit: " + eCE.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void grvCuestionario_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int intCuestionarioID = 0;
                switch (e.CommandName)
                {
                    case "Estatus":
                        intCuestionarioID = Convert.ToInt32(e.CommandArgument.ToString());
                        CuestionarioRequest request = new CuestionarioRequest();
                        CuestionarioResponse response = new CuestionarioResponse();
                        request.mdlUser = Usuario;
                        request.intCuestionarioID = intCuestionarioID;
                        if (request != null)
                        {
                            response = RisService.setEstatusCuestionario(request);
                            if (response != null)
                            {
                                if (response.Success)
                                {
                                    ShowMessage("Se actualizo correctamente el cuestionarioS", MessageType.Correcto, "alert_container");
                                    //grvAddPaciente.EditIndex = -1;
                                    cargarCuestionarios();
                                }
                                else
                                {
                                    ShowMessage("Existe un error: " + response.Mensaje, MessageType.Error, "alert_container");
                                }
                            }
                            else
                            {
                                ShowMessage("Favor de revisar la información.", MessageType.Error, "alert_container");
                            }
                        }
                        else
                        {
                            ShowMessage("Favor de revisar la información.", MessageType.Error, "alert_container");
                        }
                        break;
                }
            }
            catch (Exception eRU)
            {
                ShowMessage("Favor de revisar la información: " + eRU.Message, MessageType.Error, "alert_container");
                Log.EscribeLog("Existe un error grvCuestionario_RowCommand: " + eRU.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void grvCuestionario_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                grvCuestionario.EditIndex = e.NewEditIndex;
                cargarCuestionarios();
            }
            catch (Exception eRW)
            {
                Log.EscribeLog("Existe un error en grvCuestionario_RowEditing: " + eRW.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void grvCuestionario_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                CuestionarioRequest request = new CuestionarioRequest();
                CuestionarioResponse response = new CuestionarioResponse();
                request.mdlUser = Usuario;
                tbl_DET_Cuestionario mdlCues = new tbl_DET_Cuestionario();
                mdlCues.bitActivo = true;
                mdlCues.datFecha = DateTime.Now;
                TextBox txtNamevar = (TextBox)grvCuestionario.Rows[e.RowIndex].FindControl("txtname");
                mdlCues.vchCuestionario = txtNamevar.Text;
                mdlCues.intDETCuestionarioID = Convert.ToInt16(grvCuestionario.DataKeys[e.RowIndex].Values["intDETCuestionarioID"].ToString());
                mdlCues.intPrestacionID = Convert.ToInt16(grvCuestionario.DataKeys[e.RowIndex].Values["intPrestacionID"].ToString());
                if (mdlCues != null)
                {
                    request.mdlCuestionario = mdlCues;
                    response = RisService.setActualizaCuestionario(request);
                    if (response != null)
                    {
                        if (response.Success)
                        {
                            ShowMessage("Se actualizo correctamente el cuestionario", MessageType.Correcto, "alert_container");
                            grvCuestionario.EditIndex = -1;
                            cargarCuestionarios();
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

        protected void txtBandejaCues_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox txtBandejaAvaluosGoToPage = (TextBox)sender;
                int numeroPagina;
                if (int.TryParse(txtBandejaAvaluosGoToPage.Text.Trim(), out numeroPagina))
                    this.grvCuestionario.PageIndex = numeroPagina - 1;
                else
                    this.grvCuestionario.PageIndex = 0;
                this.cargarCuestionarios();
            }
            catch (Exception ex)
            {
                Log.EscribeLog("Existe un error txtBandejaCues_TextChanged de frmConfiguracion: " + ex.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void ddlBandejaCues_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DropDownList dropDownList = (DropDownList)sender;
                if (int.Parse(dropDownList.SelectedValue) != 0)
                {
                    this.grvCuestionario.AllowPaging = true;
                    this.grvCuestionario.PageSize = int.Parse(dropDownList.SelectedValue);
                }
                else
                    this.grvCuestionario.AllowPaging = false;
                this.cargarCuestionarios();
            }
            catch (Exception eddS)
            {
                Log.EscribeLog("Existe un error ddlBandejaInd_SelectedIndexChanged de frmConfiguracion: " + eddS.Message, 3, Usuario.vchUsuario);
            }
        }

        private void cargarCuestionarios()
        {
            try
            {
                CuestionarioRequest request = new CuestionarioRequest();
                List<tbl_DET_Cuestionario> response = new List<tbl_DET_Cuestionario>();
                request.mdlUser = Usuario;
                request.intPrestacionID = Convert.ToInt32(Session["intPrestacionID"].ToString());
                response = RisService.getListCuestionario(request);
                grvCuestionario.DataSource = null;
                grvCuestionario.DataBind();
                if (response != null)
                {
                    if (response.Count > 0)
                    {
                        grvCuestionario.DataSource = response;
                    }
                }
                grvCuestionario.DataBind();
            }
            catch (Exception ecI)
            {
                Log.EscribeLog("Existe un error en cargarCuestionarios: " + ecI.Message, 3, Usuario.vchUsuario);
            }
        }
        #endregion Cuestionarios

        #region restriccion
        protected void btnAddRestricciones_Click(object sender, EventArgs e)
        {
            try
            {
                RestriccionRequest request = new RestriccionRequest();
                RestriccionResponse response = new RestriccionResponse();
                request.mdlUser = Usuario;
                tbl_DET_Restriccion mdlRes = new tbl_DET_Restriccion();
                mdlRes.bitActivo = true;
                mdlRes.datFecha = DateTime.Now;
                mdlRes.vchNombreReestriccion = txtRestriccion.Text;
                mdlRes.intPrestacionID = Convert.ToInt32(Session["intPrestacionID"].ToString());
                if (mdlRes != null)
                {
                    request.mdlRestriccion = mdlRes;
                    response = RisService.setRestriccion(request);
                    if (response != null)
                    {
                        if (response.Success)
                        {
                            ShowMessage("Se agregó correctamente la restricción", MessageType.Correcto, "alert_container");
                            cargarRestricciones();
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
                else
                {
                    ShowMessage("Favor de revisar la información.", MessageType.Error, "alert_container");
                }
            }
            catch (Exception ebAdd)
            {
                ShowMessage("Favor de revisar la información." + ebAdd.Message, MessageType.Error, "alert_container");
                Log.EscribeLog("Existe un error al agregar la restricción:  " + ebAdd.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void btnCancelRestricciones_Click(object sender, EventArgs e)
        {

        }

        protected void grvRestriccion_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Pager)
                {
                    Label lblTotalNumDePaginas = (Label)e.Row.FindControl("lblBandejaTotal");
                    lblTotalNumDePaginas.Text = grvRestriccion.PageCount.ToString();

                    TextBox txtIrAlaPagina = (TextBox)e.Row.FindControl("txtBandejaRes");
                    txtIrAlaPagina.Text = (grvRestriccion.PageIndex + 1).ToString();

                    DropDownList ddlTamPagina = (DropDownList)e.Row.FindControl("ddlBandejaRes");
                    ddlTamPagina.SelectedValue = grvRestriccion.PageSize.ToString();
                }

                if (e.Row.RowType != DataControlRowType.DataRow)
                {
                    return;
                }

                if (e.Row.DataItem != null)
                {
                    tbl_DET_Restriccion _mdl = (tbl_DET_Restriccion)e.Row.DataItem;
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

        protected void grvRestriccion_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                if (e.NewPageIndex >= 0)
                {
                    this.grvRestriccion.PageIndex = e.NewPageIndex;
                    cargarRestricciones();
                }
            }
            catch (Exception ex)
            {
                Log.EscribeLog("Existe un error grvRestriccion_PageIndexChanging: " + ex.Message, 3, Usuario.vchUsuario);
            }
        }

        private void cargarRestricciones()
        {
            try
            {
                RestriccionRequest request = new RestriccionRequest();
                List<tbl_DET_Restriccion> response = new List<tbl_DET_Restriccion>();
                request.mdlUser = Usuario;
                request.intPrestacionID = Convert.ToInt32(Session["intPrestacionID"].ToString());
                response = RisService.getListRestriccion(request);
                grvRestriccion.DataSource = null;
                grvRestriccion.DataBind();
                if (response != null)
                {
                    if (response.Count > 0)
                    {
                        grvRestriccion.DataSource = response;
                    }
                }
                grvRestriccion.DataBind();
            }
            catch (Exception ecI)
            {
                Log.EscribeLog("Existe un error en cargarRestricciones: " + ecI.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void grvRestriccion_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            try
            {
                grvRestriccion.EditIndex = -1;
                cargarRestricciones();
            }
            catch (Exception eCE)
            {
                Log.EscribeLog("Existe un error en grvRestriccion_RowCancelingEdit: " + eCE.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void grvRestriccion_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int intRestriccionID = 0;
                switch (e.CommandName)
                {
                    case "Estatus":
                        intRestriccionID = Convert.ToInt32(e.CommandArgument.ToString());
                        RestriccionRequest request = new RestriccionRequest();
                        RestriccionResponse response = new RestriccionResponse();
                        request.mdlUser = Usuario;
                        request.intReestriccionID = intRestriccionID;
                        if (request != null)
                        {
                            response = RisService.setEstatusRestriccion(request);
                            if (response != null)
                            {
                                if (response.Success)
                                {
                                    ShowMessage("Se actualizo correctamente la restricción", MessageType.Correcto, "alert_container");
                                    //grvAddPaciente.EditIndex = -1;
                                    cargarRestricciones();
                                }
                                else
                                {
                                    ShowMessage("Existe un error: " + response.Mensaje, MessageType.Error, "alert_container");
                                }
                            }
                            else
                            {
                                ShowMessage("Favor de revisar la información.", MessageType.Error, "alert_container");
                            }
                        }
                        else
                        {
                            ShowMessage("Favor de revisar la información.", MessageType.Error, "alert_container");
                        }
                        break;
                }
            }
            catch (Exception eRU)
            {
                ShowMessage("Favor de revisar la información: " + eRU.Message, MessageType.Error, "alert_container");
                Log.EscribeLog("Existe un error grvRestriccion_RowCommand: " + eRU.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void grvRestriccion_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                grvRestriccion.EditIndex = e.NewEditIndex;
                cargarRestricciones();
            }
            catch (Exception eRW)
            {
                Log.EscribeLog("Existe un error en grvRestriccion_RowEditing: " + eRW.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void grvRestriccion_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                RestriccionRequest request = new RestriccionRequest();
                RestriccionResponse response = new RestriccionResponse();
                request.mdlUser = Usuario;
                tbl_DET_Restriccion mdlRes = new tbl_DET_Restriccion();
                mdlRes.bitActivo = true;
                mdlRes.datFecha = DateTime.Now;
                TextBox txtNamevar = (TextBox)grvRestriccion.Rows[e.RowIndex].FindControl("txtname");
                mdlRes.vchNombreReestriccion = txtNamevar.Text;
                mdlRes.intReestriccionID = Convert.ToInt16(grvRestriccion.DataKeys[e.RowIndex].Values["intReestriccionID"].ToString());
                mdlRes.intPrestacionID = Convert.ToInt16(grvRestriccion.DataKeys[e.RowIndex].Values["intPrestacionID"].ToString());
                if (mdlRes != null)
                {
                    request.mdlRestriccion = mdlRes;
                    response = RisService.setActualizaRestriccion(request);
                    if (response != null)
                    {
                        if (response.Success)
                        {
                            ShowMessage("Se actualizo correctamente la restricción", MessageType.Correcto, "alert_container");
                            grvRestriccion.EditIndex = -1;
                            cargarRestricciones();
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

        protected void ddlBandejaRes_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DropDownList dropDownList = (DropDownList)sender;
                if (int.Parse(dropDownList.SelectedValue) != 0)
                {
                    this.grvRestriccion.AllowPaging = true;
                    this.grvRestriccion.PageSize = int.Parse(dropDownList.SelectedValue);
                }
                else
                    this.grvRestriccion.AllowPaging = false;
                this.cargarRestricciones();
            }
            catch (Exception eddS)
            {
                Log.EscribeLog("Existe un error ddlBandejaRes_SelectedIndexChanged de frmConfiguracion: " + eddS.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void txtBandejaRes_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox txtBandejaAvaluosGoToPage = (TextBox)sender;
                int numeroPagina;
                if (int.TryParse(txtBandejaAvaluosGoToPage.Text.Trim(), out numeroPagina))
                    this.grvRestriccion.PageIndex = numeroPagina - 1;
                else
                    this.grvRestriccion.PageIndex = 0;
                this.cargarRestricciones();
            }
            catch (Exception ex)
            {
                Log.EscribeLog("Existe un error txtBandejaRes_TextChanged de frmConfiguracion: " + ex.Message, 3, Usuario.vchUsuario);
            }
        }
        #endregion restriccion


    }
}