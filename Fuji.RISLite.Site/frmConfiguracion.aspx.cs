using Fuji.RISLite.Entidades.DataBase;
using Fuji.RISLite.Entidades.Extensions;
using Fuji.RISLite.Entities;
using Fuji.RISLite.Site.Services;
using Fuji.RISLite.Site.Services.DataContract;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

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
        public static bool bitActualizaEmail = false;
        public static List<clsUsuario> lstUsuarios = new List<clsUsuario>();
        public static List<stp_getListCatalogo_Result> lstCat = new List<stp_getListCatalogo_Result>();
        public static List<clsPrestacion> lstPres = new List<clsPrestacion>();
        public static List<tbl_CAT_Equipo> lstEquipo = new List<tbl_CAT_Equipo>();
        public static List<tbl_CAT_TipoAdicional> lstTipoAdicional = new List<tbl_CAT_TipoAdicional>();
        public static List<tbl_CAT_TipoBoton> lstTipoControl = new List<tbl_CAT_TipoBoton>();

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
                            cargarSitios();
                            cargaSitiosList();
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



        private void cargaSitiosList()
        {
            try
            {
                ddlSitioSistema.DataSource = null;
                ddlSitioCorreo.DataSource = null;
                ddlSitioVarAdi.DataSource = null;
                radCbxSitioCatalogo.DataSource = null;
                ddlSitioMod.DataSource = null;
                ddlSitioModEquipo.DataSource = null;
                ddlSitioAdicionales.DataSource = null;
                ddlSitioSistema.Items.Clear();
                ddlSitioCorreo.Items.Clear();
                ddlSitioVarAdi.Items.Clear();
                radCbxSitioCatalogo.Items.Clear();
                ddlSitioMod.Items.Clear();
                ddlSitioModEquipo.Items.Clear();
                ddlSitioAdicionales.Items.Clear();
                List<tbl_CAT_Sitio> lstSitio = new List<tbl_CAT_Sitio>();
                SitioRequest request = new SitioRequest();
                request.mdlUser = Usuario;
                lstSitio = RisService.getListSitios(request);
                if (lstSitio != null)
                {
                    if (lstSitio.Count > 0)
                    {
                        ddlSitioSistema.DataSource = lstSitio;
                        ddlSitioSistema.DataTextField = "vchNombreSitio";
                        ddlSitioSistema.DataValueField = "intSitioID";
                        ddlSitioCorreo.DataSource = lstSitio;
                        ddlSitioCorreo.DataTextField = "vchNombreSitio";
                        ddlSitioCorreo.DataValueField = "intSitioID";
                        ddlSitioVarAdi.DataSource = lstSitio;
                        ddlSitioVarAdi.DataTextField = "vchNombreSitio";
                        ddlSitioVarAdi.DataValueField = "intSitioID";
                        radCbxSitioCatalogo.DataSource = lstSitio;
                        radCbxSitioCatalogo.DataTextField = "vchNombreSitio";
                        radCbxSitioCatalogo.DataValueField = "intSitioID";
                        ddlSitioMod.DataSource = lstSitio;
                        ddlSitioMod.DataTextField = "vchNombreSitio";
                        ddlSitioMod.DataValueField = "intSitioID";
                        ddlSitioModEquipo.DataSource = lstSitio;
                        ddlSitioModEquipo.DataTextField = "vchNombreSitio";
                        ddlSitioModEquipo.DataValueField = "intSitioID";
                        ddlSitioAdicionales.DataSource = lstSitio;
                        ddlSitioAdicionales.DataTextField = "vchNombreSitio";
                        ddlSitioAdicionales.DataValueField = "intSitioID";
                    }
                }
                ddlSitioSistema.DataBind();
                ddlSitioSistema.Items.Insert(0, new ListItem("Seleccionar...", "0"));
                ddlSitioCorreo.DataBind();
                ddlSitioCorreo.Items.Insert(0, new ListItem("Seleccionar...", "0"));
                ddlSitioVarAdi.DataBind();
                ddlSitioVarAdi.Items.Insert(0, new ListItem("Seleccionar...", "0"));
                radCbxSitioCatalogo.DataBind();
                ddlSitioMod.DataBind();
                ddlSitioModEquipo.DataBind();
                ddlSitioAdicionales.DataBind();
                //                ddlSitioMod.Items.Insert(0, new RadComboBoxItem("Seleccionar...", "0"));
            }
            catch (Exception ecU)
            {
                Log.EscribeLog("Existe un error en cargaSitiosList: " + ecU.Message, 3, Usuario.vchUsuario);
            }
        }

        private void cargarForma()
        {
            try
            {
                cargarTipoUsuario();
                cargarUsuarios();
                if (Convert.ToInt32(ddlSitioVarAdi.SelectedValue) > 0)
                {
                    cargarVarAdicionales();
                }
                cargaConfigEmail();
                cargaCatalogo();
                cargaPrestaciones();
                cargaEquipo();
                cargarAdicionales();
                clearConfigSistema();
                if (Convert.ToInt32(ddlSitioSistema.SelectedValue) > 0)
                {
                    cargaConfigSistema();
                    //btnSaveConfig.Enabled = true;
                }
                //else
                //{
                //    btnSaveConfig.Enabled = false;
                //}
            }
            catch (Exception ecF)
            {
                Log.EscribeLog("Existe un error en cargarForma: " + ecF.Message, 3, "");
            }
        }

        private void cargarAdicionales()
        {
            try
            {
                cargaTipoAdicional();
                cargaTipoControl();
                cargarAdicional();
            }
            catch (Exception ecP)
            {
                Log.EscribeLog("Existe un error en cargarAdicionales: " + ecP.Message, 3, Usuario.vchUsuario);
            }
        }

        private void cargarAdicional()
        {
            try
            {
                List<clsAdicionales> response = new List<clsAdicionales>();
                AdicionalesRequest request = new AdicionalesRequest();
                request.mdlUser = Usuario;
                request.intSitioID = Convert.ToInt32(ddlSitioAdicionales.SelectedValue);
                request.intTipoAdicional = Convert.ToInt32(ddlTipoVariable.SelectedValue);
                response = RisService.getAdicionales(request);
                grvAdicional.DataSource = null;
                if (response != null)
                {
                    if (response.Count > 0)
                    {
                        grvAdicional.DataSource = response;
                    }
                }
                grvAdicional.DataBind();
            }
            catch (Exception ecA)
            {
                Log.EscribeLog("Existe un error en cargarAdicional: " + ecA.Message, 3, Usuario.vchUsuario);
            }
        }

        private void cargaTipoControl()
        {
            try
            {
                AdicionalesRequest request = new AdicionalesRequest();
                clsUsuario _user = new clsUsuario();
                _user = Usuario;
                request.mdlUser = _user;
                List<tbl_CAT_TipoBoton> response = new List<tbl_CAT_TipoBoton>();
                response = RisService.getCATTipoBoton(request);
                if (response != null)
                {
                    if (response.Count > 0)
                    {
                        lstTipoControl = response;
                        ddlTipoControl.DataSource = response;
                        ddlTipoControl.DataTextField = "vchTipoBoton";
                        ddlTipoControl.DataValueField = "intTipoBotonID";
                        ddlTipoControl.DataBind();
                        //ddlTipoControl.Items.Insert(0, new RadComboBoxItem("Seleccionar Tipo de Control...", "0"));
                    }
                }
            }
            catch (Exception eFC)
            {
                Log.EscribeLog("Existe un error en cargaTipoAdicional: " + eFC.Message, 3, "");
            }
        }

        private void cargaTipoAdicional()
        {
            try
            {
                AdicionalesRequest request = new AdicionalesRequest();
                clsUsuario _user = new clsUsuario();
                _user = Usuario;
                request.mdlUser = _user;
                List<tbl_CAT_TipoAdicional> response = new List<tbl_CAT_TipoAdicional>();
                response = RisService.getCATTipoAdicional(request);
                if (response != null)
                {
                    if (response.Count > 0)
                    {
                        lstTipoAdicional = response;
                        ddlTipoVariable.DataSource = response;
                        ddlTipoVariable.DataTextField = "vchNombre";
                        ddlTipoVariable.DataValueField = "intTipoAdicional";
                        ddlTipoVariable.DataBind();
                        //ddlTipoVariable.Items.Insert(0, new RadComboBoxItem("Seleccionar Tipo de Variable...", "0"));
                    }
                }
            }
            catch (Exception eFC)
            {
                Log.EscribeLog("Existe un error en cargaTipoAdicional: " + eFC.Message, 3, "");
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
                grvEquipo.DataSource = null;
                grvEquipo.DataBind();
                if (ddlModalidadEquipo.SelectedValue != "" && ddlModalidadEquipo.SelectedValue != "0" && ddlSitioMod.SelectedValue != "" && ddlSitioMod.SelectedValue != "0")
                {
                    List<tbl_CAT_Equipo> lst = new List<tbl_CAT_Equipo>();
                    EquipoRequest request = new EquipoRequest();
                    request.mdlUser = Usuario;
                    request.intModalidadID = Convert.ToInt32(ddlModalidadEquipo.SelectedValue);
                    request.intSitioID = Convert.ToInt32(ddlSitioModEquipo.SelectedValue);
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
                ddlModalidadEquipo.DataSource = null;
                ddlModalidadEquipo.Items.Clear();
                if (response != null)
                {
                    if (response.Count > 0)
                    {
                        ddlModalidadEquipo.DataSource = response.Where(x => x.intSitioID == Convert.ToInt32(ddlSitioModEquipo.SelectedValue));
                        ddlModalidadEquipo.DataTextField = "vchModalidad";
                        ddlModalidadEquipo.DataValueField = "intModalidadID";
                        //ddlModalidadEquipo.Items.Insert(0, new RadComboBoxItem("Seleccionar Modalidad...", "0"));
                    }
                }
                ddlModalidadEquipo.DataBind();
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
            catch (Exception ecP)
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
                ddlModalidad.DataSource = null;
                ddlModalidad.Items.Clear();
                if (response != null)
                {
                    if (response.Count > 0)
                    {
                        ddlModalidad.DataSource = response.Where(x => x.intSitioID == Convert.ToInt32(ddlSitioMod.SelectedValue));
                        ddlModalidad.DataTextField = "vchModalidad";
                        ddlModalidad.DataValueField = "intModalidadID";

                    }
                    //ddlModalidad.Items.Insert(0, new RadComboBoxItem("Seleccionar Modalidad...", "0"));
                }
                ddlModalidad.DataBind();
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
                grvPrestaciones.DataSource = null;
                grvPrestaciones.DataBind();
                if (ddlModalidad.SelectedValue != "" && ddlModalidad.SelectedValue != "0" && ddlSitioMod.SelectedValue != "" && ddlSitioMod.SelectedValue != "0")
                {
                    List<clsPrestacion> lst = new List<clsPrestacion>();
                    PrestacionRequest request = new PrestacionRequest();
                    request.mdlUser = Usuario;
                    request.intModalidad = Convert.ToInt32(ddlModalidad.SelectedValue);
                    request.intSitioID = Convert.ToInt32(ddlSitioMod.SelectedValue);
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
                        radCbxCatalogo.DataSource = response;
                        radCbxCatalogo.DataTextField = "vchNombreCat";
                        radCbxCatalogo.DataValueField = "intCatalogoID";
                        radCbxCatalogo.DataBind();
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
                grvCatalogos.DataSource = null;
                grvCatalogos.DataBind();
                if (radCbxCatalogo.SelectedItem.Value != "" && radCbxCatalogo.SelectedItem.Value != "0" && radCbxSitioCatalogo.SelectedItem.Value != "" && radCbxSitioCatalogo.SelectedItem.Value != "0")
                {
                    List<stp_getListCatalogo_Result> lst = new List<stp_getListCatalogo_Result>();
                    CatalogoRequest request = new CatalogoRequest();
                    clsUsuario user = new clsUsuario();
                    clsCatalogo cat = new clsCatalogo();
                    user = Usuario;
                    request.mdlUser = user;
                    cat.intCatalogoID = Convert.ToInt32(radCbxCatalogo.SelectedValue);
                    cat.intSitioID = Convert.ToInt32(radCbxSitioCatalogo.SelectedValue);
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
                request.intSitioID = Convert.ToInt32(ddlSitioCorreo.SelectedValue);
                response = RisService.getConfigEmail(request);
                if (response != null)
                {
                    if (response.Success)
                    {
                        fillConfigEmail(response.mldConfigEmail);
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
                request.intSitioId = Convert.ToInt32(ddlSitioSistema.SelectedValue);
                response = RisService.getConfigSitio(request);
                if (response != null)
                {
                    if (response.Success)
                    {
                        fillConfigSistema(response.mdlConfig);
                    }
                }
            }
            catch (Exception eCCS)
            {
                Log.EscribeLog("Existe un error en cargaConfigSistema: " + eCCS.Message, 3, Usuario.vchUsuario);
            }
        }

        private void clearConfigSistema()
        {
            try
            {
                txtNombreSitio.Text = "";
                txtDireccionSitio.Text = "";
                imgLogo.ImageUrl = "";
                txtPathRepositorio.Text = "";
                Session["logo"] = null;
            }
            catch (Exception ecCS)
            {
                Log.EscribeLog("Existe un error en clearConfigSistema: " + ecCS.Message, 3, Usuario.vchUsuario);
            }
        }

        private void fillConfigSistema(tbl_MST_ConfiguracionSistema mdlConfig)
        {
            try
            {
                Session["idActializa"] = mdlConfig.intConfigID;
                txtNombreSitio.Text = ddlSitioSistema.SelectedItem.Text;
                txtDireccionSitio.Text = mdlConfig.vchDominio;
                if (mdlConfig.vbLogoSitio != null)
                {
                    Session["logo"] = mdlConfig.vbLogoSitio;
                    string base64String = Convert.ToBase64String(mdlConfig.vbLogoSitio, 0, mdlConfig.vbLogoSitio.Length);
                    imgLogo.ImageUrl = "data:image/png;base64," + base64String;
                    imgLogo.Visible = true;
                    if (mdlConfig.intWidthImage < 250 && mdlConfig.intWidthImage > 0)
                        imgLogo.Width = (int)mdlConfig.intWidthImage;
                    else
                        imgLogo.Width = 250;
                    if (mdlConfig.intHeigthImage < 150 && mdlConfig.intHeigthImage > 0)
                        imgLogo.Height = (int)mdlConfig.intHeigthImage;
                    else
                        imgLogo.Height = 150;
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
            catch (Exception ecva)
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
                request.intSitioID = Convert.ToInt32(ddlSitioVarAdi.SelectedValue);
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
                request.intSitioID = Convert.ToInt32(ddlSitioVarAdi.SelectedValue);
                List<clsVarAcicionales> lst = new List<clsVarAcicionales>();
                lst = RisService.getVariablesAdicionalPaciente(request);
                grvAddPaciente.DataSource = null;
                if (lst != null)
                {
                    if (lst.Count > 0)
                    {
                        grvAddPaciente.DataSource = lst;
                    }
                }
                grvAddPaciente.DataBind();
            }
            catch (Exception ecP)
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
                request.intSitioID = Convert.ToInt32(ddlSitioVarAdi.SelectedValue);
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
                                ddlSitioSistema.Items.Clear();
                                ddlSitioSistema.Enabled = false;
                                txtEmailUser.Text = "";
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
                    else
                    {
                        ShowMessage("Existe un error, favor de verificar. ", MessageType.Error, "alert_container");
                    }
                }
                else
                {
                    ShowMessage("Verificar la información: ", MessageType.Advertencia, "alert_container");
                }
            }
            catch (Exception eAddUser)
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
                user.vchEmail = txtEmailUser.Text;
                if (user.intTipoUsuario > 1)
                    user.intSitioID = Convert.ToInt32(ddlSitioUser.SelectedValue.ToString());
                user.vchNombre = txtNombre.Text.ToUpper();
                user.vchUserAdmin = Usuario.vchUsuario.ToUpper();
                user.vchUsuario = txtUsuario.Text.ToUpper();
            }
            catch (Exception eODUA)
            {
                user = null;
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
                Log.EscribeLog("Existe un error en grvUsuario_RowEditing : " + eGU.Message, 3, Usuario.vchUsuario);
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
                TextBox txtEmail = (TextBox)grvUsuario.Rows[e.RowIndex].FindControl("txtEmailUser");
                mdlVar.vchNombre = txtNameUser.Text.ToUpper();
                mdlVar.vchUsuario = txtUser.Text.ToUpper();
                mdlVar.vchEmail = txtEmail.Text;
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
                        intUsuarioId = Convert.ToInt32(e.CommandArgument.ToString());
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
                if (mdlConfig != null)
                {
                    ConfigSitioRequest request = new ConfigSitioRequest();
                    ConfigSitioResponse response = new ConfigSitioResponse();
                    request.mdlUser = Usuario;
                    request.mdlConfig = mdlConfig;
                    response = RisService.setActualizarConfigSitio(request);
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
                else
                {
                    ShowMessage("Verificar la información.", MessageType.Correcto, "alert_container");
                }
            }
            catch (Exception eSC)
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
                mdl.intConfigID = Session["idActializa"] != null ? Convert.ToInt32(Session["idActializa"].ToString()) : 0;
                mdl.intSitioID = Convert.ToInt32(ddlSitioSistema.SelectedValue);
                Byte[] bytes = null;
                int height = 0;
                int width = 0;
                if (fuLogo.HasFile)
                {
                    Stream fs = fuLogo.PostedFile.InputStream;

                    BinaryReader br = new BinaryReader(fs);
                    bytes = br.ReadBytes((Int32)fs.Length);
                    using (System.Drawing.Image myImage = System.Drawing.Image.FromStream(fs))
                    {
                        height = myImage.Height;
                        width = myImage.Width;
                    }
                    mdl.intWidthImage = width > 0 ? width : 0;
                    mdl.intHeigthImage = height > 0 ? height : 0;
                }
                else
                {
                    if (Session["logo"] != null)
                    {
                        Stream stream = new MemoryStream((Byte[])Session["logo"]);
                        using (System.Drawing.Image myImage = System.Drawing.Image.FromStream(stream))
                        {
                            height = myImage.Height;
                            width = myImage.Width;
                        }
                        mdl.intWidthImage = width > 0 ? width : 0;
                        mdl.intHeigthImage = height > 0 ? height : 0;
                    }
                }
                mdl.vbLogoSitio = fuLogo.HasFile ? bytes : (Byte[])Session["logo"];
                //mdl.vchNombreSitio = txtNombreSitio.Text;
                mdl.vchDominio = txtDireccionSitio.Text;
                mdl.vchUserAdmin = Usuario.vchUsuario;
                mdl.vchVersion = txtPathRepositorio.Text;
            }
            catch (Exception eODS)
            {
                Log.EscribeLog("Existe un error en obtenerDatosSitio: " + eODS.Message, 3, Usuario.vchUsuario);
            }
            return mdl;
        }

        public Bitmap ResizeImage(Stream stream)
        {
            System.Drawing.Image originalImage = Bitmap.FromStream(stream);

            int height = 331;
            int width = 495;

            Bitmap scaledImage = new Bitmap(width, height);

            using (Graphics g = Graphics.FromImage(scaledImage))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(originalImage, 0, 0, width, height);
                return scaledImage;
            }

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
            catch (Exception eSEmail)
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
                mdlConfig.intSitioID = Convert.ToInt32(ddlSitioCorreo.SelectedValue);
            }
            catch (Exception eOCE)
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
                request.intSitioID = Convert.ToInt32(ddlSitioVarAdi.SelectedValue);
                clsVarAcicionales mdlVar = new clsVarAcicionales();
                mdlVar = obtenerVarAdicionalPaciente();
                if (mdlVar != null)
                {
                    request.mdlVariable = mdlVar;
                    request.intTipoVariable = 1;//Paciente
                    response = RisService.setAgregarVariable(request);
                    if (response != null)
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
                mdl.intSitioID = Convert.ToInt32(ddlSitioVarAdi.SelectedValue);
            }
            catch (Exception eoVP)
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
                mdl.vchNombreVarAdi = txtNombreVarCita.Text.ToUpper();
                mdl.datFecha = DateTime.Now;
                mdl.intSitioID = Convert.ToInt32(ddlSitioVarAdi.SelectedValue);
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
                            if (response != null)
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
                request.intSitioID = Convert.ToInt32(ddlSitioVarAdi.SelectedValue);
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
            catch (Exception eRED)
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
        protected void ddlCatalogo_SelectedIndexChanged(object sender, EventArgs e)//Telerik
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
                if (radCbxCatalogo.SelectedItem.Value != "" && radCbxCatalogo.SelectedItem.Value != "0" && radCbxSitioCatalogo.SelectedItem.Value != "" && radCbxSitioCatalogo.SelectedItem.Value != "0")
                {
                    user = Usuario;
                    cat.vchUserAdmin = user.vchUsuario;
                    cat.intCatalogoID = Convert.ToInt32(radCbxCatalogo.SelectedValue.ToString());
                    cat.intSitioID = Convert.ToInt32(radCbxSitioCatalogo.SelectedValue.ToString());
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
                    else
                    {
                        ShowMessage("Verificar la información.", MessageType.Error, "alert_container");
                    }
                }
                else
                {
                    ShowMessage("Seleccionar el tipo de catálogo o el sitio.", MessageType.Error, "alert_container");
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
                        cat.intCatalogoID = Convert.ToInt32(radCbxCatalogo.SelectedValue.ToString());
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
                cat.intCatalogoID = Convert.ToInt32(radCbxCatalogo.SelectedValue.ToString());
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
                if (ddlModalidad.SelectedItem.Value != "" && ddlModalidad.SelectedItem.Value != "0" && ddlSitioMod.SelectedItem.Value != "" && ddlSitioMod.SelectedItem.Value != "0")
                {
                    prestacion.bitActivo = true;
                    prestacion.intModalidadID = Convert.ToInt32(ddlModalidad.SelectedValue.ToString());
                    prestacion.intSitioId = Convert.ToInt32(ddlSitioMod.SelectedValue);
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
                    ShowMessage("Seleccionar el tipo de modalidad y el Sitio para la prestación.", MessageType.Error, "alert_container");
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
                prestacion.intSitioId = Convert.ToInt32(ddlSitioMod.SelectedValue);
                request.mdlPres = prestacion;
                PrestacionResponse response = new PrestacionResponse();
                response = RisService.setActualizaPrestacion(request);
                if (response != null)
                {
                    if (response.Success)
                    {
                        limpiarCotrolesPrestaciones();
                        ShowMessage("Cambios correctos.", MessageType.Correcto, "alert_container");
                    }
                    else
                    {
                        ShowMessage("Existe un error al guardar: " + response.Mensaje, MessageType.Error, "alert_container");
                    }
                    grvPrestaciones.EditIndex = -1;
                    cargarPrestacion();
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

        private void limpiarCotrolesPrestaciones()
        {
            try
            {
                txtPrestacion.Text = "";
                txtDuracionPres.Text = "";
            }
            catch (Exception eLCP)
            {
                Log.EscribeLog("Existe un error en limpiarCotrolesPrestaciones: " + eLCP.Message, 3, Usuario.vchUsuario);
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
                if (ddlModalidadEquipo.SelectedItem.Value != "" && ddlModalidadEquipo.SelectedItem.Value != "0" && ddlSitioModEquipo.SelectedValue != "" && ddlSitioModEquipo.SelectedValue != "0")
                {
                    equipo.bitActivo = true;
                    equipo.intModalidadID = Convert.ToInt32(ddlModalidadEquipo.SelectedValue.ToString());
                    equipo.intSitioID = Convert.ToInt32(ddlSitioModEquipo.SelectedValue);
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
                    ShowMessage("Seleccionar el tipo de modalidad y el sitio para el equipo.", MessageType.Error, "alert_container");
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
                equipo.intSitioID = Convert.ToInt32(ddlSitioModEquipo.SelectedValue);
                request.mdlEquipo = equipo;
                EquipoResponse response = new EquipoResponse();
                response = RisService.setActualizaEquipo(request);
                if (response != null)
                {
                    if (response.Success)
                    {
                        limpiarControlesEquipos();
                        ShowMessage("Cambios correctos.", MessageType.Correcto, "alert_container");
                    }
                    else
                    {
                        ShowMessage("Existe un error al guardar: " + response.Mensaje, MessageType.Error, "alert_container");
                    }
                    grvPrestaciones.EditIndex = -1;
                    cargarPrestacion();
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

        private void limpiarControlesEquipos()
        {
            try
            {
                txtNomEquipo.Text = "";
                txtCodeequipo.Text = "";
                txtAEtitle.Text = "";
            }
            catch (Exception elCE)
            {
                Log.EscribeLog("Existe un error en limpiarControlesEquipos: " + elCE.Message, 3, Usuario.vchUsuario);
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
                request.intSitioID = Convert.ToInt32(ddlSitioVarAdi.SelectedValue);
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
            catch (Exception eRW)
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
            catch (Exception ecI)
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

        protected void btnAgregarAdicional_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlSitioAdicionales.SelectedValue != "" && ddlSitioAdicionales.SelectedValue != "0" && ddlTipoControl.SelectedValue != "" && ddlTipoControl.SelectedValue != "0" && ddlTipoVariable.SelectedValue != "" && ddlTipoVariable.SelectedValue != "0")
                {
                    AdicionalesResponse response = new AdicionalesResponse();
                    AdicionalesRequest request = new AdicionalesRequest();
                    request.mdlUser = Usuario;
                    clsAdicionales mdlAdi = new clsAdicionales();
                    if (RadFileUp.UploadedFiles[0].GetName() != "")
                    {
                        mdlAdi = obtenerAdicional();
                        if (mdlAdi != null)
                        {
                            request.mdlAdicional = mdlAdi;
                            response = RisService.setAdicionales(request);
                            if (response != null)
                            {
                                if (response.Success)
                                {
                                    ShowMessage("Se guardó correctamente.", MessageType.Correcto, "alert_container");
                                    limpiarControlesAdicional();
                                    cargarAdicional();
                                }
                                else
                                {
                                    ShowMessage("Verificar la información: " + response.Mensaje, MessageType.Error, "alert_container");
                                }
                            }
                            else
                            {
                                ShowMessage("Verificar la información", MessageType.Error, "alert_container");
                            }
                        }
                        else
                        {
                            ShowMessage("Verificar la información.", MessageType.Error, "alert_container");
                        }
                    }
                    else
                    {
                        ShowMessage("Se requiere una imagen para el control.", MessageType.Error, "alert_container");
                    }
                }
                else
                {
                    ShowMessage("Favor de verificar la información.", MessageType.Error, "alert_container");
                }
            }
            catch (Exception ebA)
            {
                ShowMessage("Existe un error al guardar la información: " + ebA.Message, MessageType.Error, "alert_container");
                Log.EscribeLog("Existe un error en btnAgregarAdicional_Click: " + ebA.Message, 3, Usuario.vchUsuario);
            }
        }

        private void limpiarControlesAdicional()
        {
            try
            {
                txtNomAdi.Text = "";
                //txtImagenAdi.Text = "";
                chkObservaciones.Checked = false;
                RadFileUp.UploadedFiles.RemoveAt(0);
                //chkIcono.Checked = false;
            }
            catch (Exception elCOA)
            {
                Log.EscribeLog("Existe un error en limpiarControlesAdicional: " + elCOA.Message, 3, Usuario.vchUsuario);
            }
        }

        private clsAdicionales obtenerAdicional()
        {
            clsAdicionales mdl = new clsAdicionales();
            try
            {
                mdl.bitActivo = true;
                //mdl.bitIconBootstrap = chkIcono.Checked;
                mdl.intSitioID = Convert.ToInt32(ddlSitioAdicionales.SelectedValue);
                mdl.bitObservaciones = chkObservaciones.Checked;
                mdl.datFecha = DateTime.Now;
                mdl.intTipoAdicionalID = Convert.ToInt32(ddlTipoVariable.SelectedValue);
                mdl.intTipoBotonID = Convert.ToInt32(ddlTipoControl.SelectedValue);
                mdl.vchNombreAdicional = txtNomAdi.Text;
                string folderPath = Server.MapPath("~/Iconos/" + "/" + ddlSitioAdicionales.SelectedValue + "/");
                foreach (UploadedFile f in RadFileUp.UploadedFiles)
                {
                    if (!Directory.Exists(folderPath))
                    {
                        //If Directory (Folder) does not exists. Create it.
                        Directory.CreateDirectory(folderPath);
                    }
                    if (File.Exists(folderPath + f.GetName()))
                    {
                        File.Delete(folderPath + f.GetName());
                    }
                    f.SaveAs(folderPath + f.GetName(), true);
                }
                mdl.vchURLImagen = RadFileUp.UploadedFiles[0].GetName();
                mdl.vchUserAdmin = Usuario.vchUsuario;
            }
            catch (Exception eOA)
            {
                Log.EscribeLog("Existe un error en obtenerAdicional: " + eOA.Message, 3, Usuario.vchUsuario);
            }
            return mdl;
        }

        protected void ddlTipoVariable_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Session["ddlSelectedTipoVariable"] = ddlTipoVariable.SelectedValue;
                cargarAdicional();
            }
            catch (Exception edd)
            {
                Log.EscribeLog("Existe un error en ddlTipoVariable_SelectedIndexChanged: " + edd.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void grvAdicional_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Pager)
                {
                    Label lblTotalNumDePaginas = (Label)e.Row.FindControl("lblBandejaTotal");
                    lblTotalNumDePaginas.Text = grvAdicional.PageCount.ToString();

                    TextBox txtIrAlaPagina = (TextBox)e.Row.FindControl("txtBandejaAdi");
                    txtIrAlaPagina.Text = (grvAdicional.PageIndex + 1).ToString();

                    DropDownList ddlTamPagina = (DropDownList)e.Row.FindControl("ddlBandejaAdi");
                    ddlTamPagina.SelectedValue = grvAdicional.PageSize.ToString();
                }

                if (e.Row.RowType != DataControlRowType.DataRow)
                {
                    return;
                }

                if (e.Row.DataItem != null)
                {
                    clsAdicionales _mdl = (clsAdicionales)e.Row.DataItem;
                    ImageButton ibtEstatus = (ImageButton)e.Row.FindControl("imbEstatus");
                    ibtEstatus.Attributes.Add("onclick", "javascript:return confirm('¿Desea realizar el cambio de estatus del item seleccionado?');");
                    if ((bool)_mdl.bitActivo)
                        ibtEstatus.ImageUrl = @"~/Images/ic_action_tick.png";
                    else
                        ibtEstatus.ImageUrl = @"~/Images/ic_action_cancel.png";
                }

                if (e.Row.RowType == DataControlRowType.DataRow && grvAdicional.EditIndex == e.Row.RowIndex)
                {
                    DropDownList ddlControl = (DropDownList)e.Row.FindControl("ddlTipoControlITem");
                    ddlControl.DataSource = lstTipoControl;
                    ddlControl.DataTextField = "vchTipoBoton";
                    ddlControl.DataValueField = "intTipoBotonID";
                    ddlControl.DataBind();
                    ddlControl.Items.FindByText((e.Row.FindControl("lblControl") as Label).Text).Selected = true;

                    DropDownList ddlAdicional = (DropDownList)e.Row.FindControl("ddlTipoAdicionalItem");
                    ddlAdicional.DataSource = lstTipoAdicional;
                    ddlAdicional.DataTextField = "vchNombre";
                    ddlAdicional.DataValueField = "intTipoAdicional";
                    ddlAdicional.DataBind();
                    ddlAdicional.Items.FindByText((e.Row.FindControl("lblAdicional") as Label).Text).Selected = true;
                }
            }
            catch (Exception eGUP)
            {
                throw eGUP;
            }
        }

        protected void grvAdicional_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                if (e.NewPageIndex >= 0)
                {
                    this.grvAdicional.PageIndex = e.NewPageIndex;
                    cargarAdicional();
                }
            }
            catch (Exception ex)
            {
                Log.EscribeLog("Existe un error grvAdicional_PageIndexChanging: " + ex.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void grvAdicional_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            try
            {
                grvAdicional.EditIndex = -1;
                cargarAdicional();
            }
            catch (Exception eCE)
            {
                Log.EscribeLog("Existe un error en grvAdicional_RowCancelingEdit: " + eCE.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void grvAdicional_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int intAdicionalesID = 0;
                switch (e.CommandName)
                {
                    case "Estatus":
                        intAdicionalesID = Convert.ToInt32(e.CommandArgument.ToString());
                        AdicionalesRequest request = new AdicionalesRequest();
                        AdicionalesResponse response = new AdicionalesResponse();
                        request.mdlUser = Usuario;
                        request.intTipoAdicional = intAdicionalesID;
                        if (request != null)
                        {
                            response = RisService.setEstatusAdicional(request);
                            if (response != null)
                            {
                                if (response.Success)
                                {
                                    ShowMessage("Se actualizo correctamente", MessageType.Correcto, "alert_container");
                                    //grvAddPaciente.EditIndex = -1;
                                    cargarAdicional();
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
                Log.EscribeLog("Existe un error grvAdicional_RowCommand: " + eRU.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void grvAdicional_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                grvAdicional.EditIndex = e.NewEditIndex;
                cargarAdicional();
            }
            catch (Exception eRW)
            {
                Log.EscribeLog("Existe un error en grvAdicional_RowEditing: " + eRW.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void grvAdicional_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                AdicionalesRequest request = new AdicionalesRequest();
                AdicionalesResponse response = new AdicionalesResponse();
                request.mdlUser = Usuario;
                clsAdicionales mdlRes = new clsAdicionales();
                mdlRes.bitActivo = true;
                mdlRes.datFecha = DateTime.Now;
                TextBox txtNamevar = (TextBox)grvAdicional.Rows[e.RowIndex].FindControl("txtItemNombre");
                mdlRes.vchNombreAdicional = txtNamevar.Text;
                mdlRes.intAdicionalesID = Convert.ToInt32(grvAdicional.DataKeys[e.RowIndex].Values["intAdicionalesID"].ToString());
                Label txtImagen = (Label)grvAdicional.Rows[e.RowIndex].FindControl("txtImagenItem");
                RadAsyncUpload fileup = (RadAsyncUpload)grvAdicional.Rows[e.RowIndex].FindControl("RadFileUpItem");
                string folderPath = Server.MapPath("~/Iconos/" + "/" + ddlSitioAdicionales.SelectedValue + "/");

                if (fileup.UploadedFiles.Count == 0)
                {
                    mdlRes.vchURLImagen = txtImagen.Text;
                }
                else
                {
                    foreach (UploadedFile f in fileup.UploadedFiles)
                    {
                        if (!Directory.Exists(folderPath))
                        {
                            //If Directory (Folder) does not exists. Create it.
                            Directory.CreateDirectory(folderPath);
                        }
                        if (File.Exists(folderPath + f.GetName()))
                        {
                            File.Delete(folderPath + f.GetName());
                        }
                        f.SaveAs(folderPath + f.GetName(), true);
                    }
                    mdlRes.vchURLImagen = fileup.UploadedFiles[0].GetName();
                }
                //mdlRes.vchURLImagen = txtImagen.Text;
                mdlRes.intTipoAdicionalID = Convert.ToInt32((grvAdicional.Rows[e.RowIndex].FindControl("ddlTipoAdicionalItem") as DropDownList).SelectedItem.Value);
                mdlRes.intTipoBotonID = Convert.ToInt32((grvAdicional.Rows[e.RowIndex].FindControl("ddlTipoControlITem") as DropDownList).SelectedItem.Value);
                mdlRes.bitObservaciones = (grvAdicional.Rows[e.RowIndex].FindControl("chkObsItem") as CheckBox).Checked;
                mdlRes.vchUserAdmin = Usuario.vchUsuario;
                if (mdlRes != null)
                {
                    request.mdlAdicional = mdlRes;
                    response = RisService.setActualizarAdicionales(request);
                    if (response != null)
                    {
                        if (response.Success)
                        {
                            ShowMessage("Se actualizo correctamente.", MessageType.Correcto, "alert_container");
                            limpiarControlesAdicional();
                            grvAdicional.EditIndex = -1;
                            cargarAdicional();
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
            }
            catch (Exception eUpdating)
            {
                ShowMessage("Existe un error: " + eUpdating.Message, MessageType.Error, "alert_container");
                Log.EscribeLog("Existe un error en grvAdicional_RowUpdating: " + eUpdating.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void txtBandejaAdi_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox txtBandejaAvaluosGoToPage = (TextBox)sender;
                int numeroPagina;
                if (int.TryParse(txtBandejaAvaluosGoToPage.Text.Trim(), out numeroPagina))
                    this.grvAdicional.PageIndex = numeroPagina - 1;
                else
                    this.grvAdicional.PageIndex = 0;
                this.cargarAdicional();
            }
            catch (Exception ex)
            {
                Log.EscribeLog("Existe un error txtBandejaAdi_TextChanged de frmConfiguracion: " + ex.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void ddlBandejaAdi_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DropDownList dropDownList = (DropDownList)sender;
                if (int.Parse(dropDownList.SelectedValue) != 0)
                {
                    this.grvAdicional.AllowPaging = true;
                    this.grvAdicional.PageSize = int.Parse(dropDownList.SelectedValue);
                }
                else
                    this.grvAdicional.AllowPaging = false;
                this.cargarAdicional();
            }
            catch (Exception eddS)
            {
                Log.EscribeLog("Existe un error ddlBandejaAdi_SelectedIndexChanged de frmConfiguracion: " + eddS.Message, 3, Usuario.vchUsuario);
            }
        }

        #region sitio
        private void cargarSitios()
        {
            try
            {
                grvSitio.DataSource = null;
                List<tbl_CAT_Sitio> lstSitio = new List<tbl_CAT_Sitio>();
                SitioRequest request = new SitioRequest();
                request.mdlUser = Usuario;
                lstSitio = RisService.getListSitios(request);
                if (lstSitio != null)
                {
                    if (lstSitio.Count > 0)
                    {
                        grvSitio.DataSource = lstSitio;
                    }
                }
                grvSitio.DataBind();
            }
            catch (Exception ecU)
            {
                Log.EscribeLog("Existe un error en cargarSitios: " + ecU.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void btnAddSitio_Click(object sender, EventArgs e)
        {
            try
            {
                tbl_CAT_Sitio sitio = new tbl_CAT_Sitio();
                sitio.vchNombreSitio = txtAddSitio.Text;
                sitio.bitActivo = true;
                sitio.datFecha = DateTime.Now;
                sitio.vchUserAdmin = Usuario.vchUserAdmin;
                if (sitio != null)
                {
                    SitioRequest request = new SitioRequest();
                    SitioResponse response = new SitioResponse();
                    request.mdlUser = Usuario;
                    request.mdlSitio = sitio;
                    if (request != null)
                    {
                        response = RisService.setSitio(request);
                        if (response != null)
                        {
                            if (response.Success)
                            {
                                ShowMessage("Se agregó correctamente. " + response.Mensaje, MessageType.Correcto, "alert_container");
                                txtAddSitio.Text = "";
                                cargarSitios();
                                cargarListaSitios();
                                cargaSitiosList();
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
                    else
                    {
                        ShowMessage("Existe un error, favor de verificar. ", MessageType.Error, "alert_container");
                    }
                }
                else
                {
                    ShowMessage("Verificar la información: ", MessageType.Advertencia, "alert_container");
                }
            }
            catch (Exception eAddUser)
            {
                ShowMessage("Existe un error: " + eAddUser.Message, MessageType.Error, "alert_container");
                Log.EscribeLog("Existe un error en btnAddSitio_Click: " + eAddUser.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void grvSitio_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Pager)
                {
                    Label lblTotalNumDePaginas = (Label)e.Row.FindControl("lblBandejaTotal");
                    lblTotalNumDePaginas.Text = grvSitio.PageCount.ToString();

                    TextBox txtIrAlaPagina = (TextBox)e.Row.FindControl("txtBandejaSitio");
                    txtIrAlaPagina.Text = (grvSitio.PageIndex + 1).ToString();

                    DropDownList ddlTamPagina = (DropDownList)e.Row.FindControl("ddlBandejaSitio");
                    ddlTamPagina.SelectedValue = grvSitio.PageSize.ToString();
                }

                if (e.Row.RowType != DataControlRowType.DataRow)
                {
                    return;
                }

                if (e.Row.DataItem != null)
                {
                    tbl_CAT_Sitio _mdl = (tbl_CAT_Sitio)e.Row.DataItem;
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
                Log.EscribeLog("Existe un error en grvSitio_RowDataBound: " + eGUP.Message, 3, Usuario.vchUserAdmin);
            }
        }

        protected void grvSitio_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                if (e.NewPageIndex >= 0)
                {
                    this.grvSitio.PageIndex = e.NewPageIndex;
                    cargarSitios();
                }
            }
            catch (Exception ex)
            {
                Log.EscribeLog("Existe un error grvSitio_PageIndexChanging: " + ex.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void grvSitio_RowCommand(object sender, GridViewCommandEventArgs e)
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
                Log.EscribeLog("Existe un error grvSitio_RowCommand: " + eRU.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void grvSitio_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                grvSitio.EditIndex = e.NewEditIndex;
                cargarSitios();
            }
            catch (Exception eGU)
            {
                Log.EscribeLog("Existe un error en grvSitio_RowEditing : " + eGU.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void grvSitio_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                SitioRequest request = new SitioRequest();
                SitioResponse response = new SitioResponse();
                request.mdlUser = Usuario;
                tbl_CAT_Sitio mdlVar = new tbl_CAT_Sitio();
                mdlVar.bitActivo = true;
                mdlVar.datFecha = DateTime.Now;
                TextBox txtNameSitio = (TextBox)grvSitio.Rows[e.RowIndex].FindControl("txtNombreSitio");
                mdlVar.vchNombreSitio = txtNameSitio.Text.ToUpper();
                mdlVar.intSitioID = Convert.ToInt16(grvSitio.DataKeys[e.RowIndex].Values["intSitioID"].ToString());
                if (mdlVar != null)
                {
                    request.mdlUser = Usuario;
                    request.mdlSitio = mdlVar;
                    response = RisService.setActualizaSitio(request);
                    if (response != null)
                    {
                        if (response.Success)
                        {
                            ShowMessage("Se actualizo correctamente el sitio", MessageType.Correcto, "alert_container");
                            grvSitio.EditIndex = -1;
                            cargarSitios();
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
                Log.EscribeLog("Existe un error en grvSitio_RowUpdating" + eUpdating, 3, Usuario.vchUserAdmin);
                ShowMessage("Existe un error: " + eUpdating.Message, MessageType.Error, "alert_container");
            }
        }

        protected void grvSitio_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            try
            {
                grvSitio.EditIndex = -1;
                cargarSitios();
            }
            catch (Exception eCE)
            {
                Log.EscribeLog("Existe un error en grvSitio_RowCancelingEdit: " + eCE.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void ddlBandejaSitio_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DropDownList dropDownList = (DropDownList)sender;
                if (int.Parse(dropDownList.SelectedValue) != 0)
                {
                    this.grvSitio.AllowPaging = true;
                    this.grvSitio.PageSize = int.Parse(dropDownList.SelectedValue);
                }
                else
                    this.grvSitio.AllowPaging = false;
                this.cargarSitios();
            }
            catch (Exception eddS)
            {
                Log.EscribeLog("Existe un error ddlBandejaSitio_SelectedIndexChanged de frmAdminUser: " + eddS.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void txtBandejaSitio_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox txtBandejaAvaluosGoToPage = (TextBox)sender;
                int numeroPagina;
                if (int.TryParse(txtBandejaAvaluosGoToPage.Text.Trim(), out numeroPagina))
                    this.grvSitio.PageIndex = numeroPagina - 1;
                else
                    this.grvSitio.PageIndex = 0;
                this.cargarSitios();
            }
            catch (Exception ex)
            {
                Log.EscribeLog("Existe un error txtBandejaSitio_TextChanged de frmAdminUser: " + ex.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void ddlTipoUsuario_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(ddlTipoUsuario.SelectedValue) > 1)
                {
                    ddlSitioUser.Enabled = true;
                    rfvSitioUser.Enabled = true;
                    cargarListaSitios();
                }
                else
                {
                    ddlSitioUser.Enabled = false;
                    rfvSitioUser.Enabled = false;
                    ddlSitioUser.DataSource = null;
                    ddlSitioUser.Items.Clear();
                    ddlSitioUser.DataBind();
                }
            }
            catch (Exception eTIPo)
            {
                Log.EscribeLog("Existe un error en ddlTipoUsuario_SelectedIndexChanged: " + eTIPo.Message, 3, Usuario.vchUsuario);
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

        #endregion sitio

        protected void ddlSitioSistema_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                clearConfigSistema();
                if (Convert.ToInt32(ddlSitioSistema.SelectedValue) > 0)
                {
                    cargaConfigSistema();
                    //btnSaveConfig.Enabled = true;
                }
                //else
                //{
                //    btnSaveConfig.Enabled = false;
                //}
            }
            catch (Exception eddS)
            {
                Log.EscribeLog("Existe un error en ddlSitioSistema_SelectedIndexChanged: " + eddS.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void ddlSitioCorreo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                clearEmailSistema();
                if (Convert.ToInt32(ddlSitioCorreo.SelectedValue) > 0)
                {
                    cargaConfigEmail();
                    btnSaveEmailSistema.Enabled = true;
                }
                else
                {
                    btnSaveEmailSistema.Enabled = false;
                }
            }
            catch (Exception eddS)
            {
                Log.EscribeLog("Existe un error en ddlSitioCorreo_SelectedIndexChanged: " + eddS.Message, 3, Usuario.vchUsuario);
            }
        }

        private void clearEmailSistema()
        {
            try
            {
                txtEmailSistema.Text = "";
                txtPasswordSistema.Text = "";
                txtHost.Text = "";
                txtPortCorreo.Text = "";
                chkSSL.Checked = false;
            }
            catch (Exception ecCS)
            {
                Log.EscribeLog("Existe un error en clearEmailSistema: " + ecCS.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void ddlSitioVarAdi_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cleanAdminVarAdi();
                if (Convert.ToInt32(ddlSitioVarAdi.SelectedValue) > 0)
                {
                    cargarVarAdicionales();
                }
            }
            catch (Exception edsvA)
            {
                Log.EscribeLog("Existe un errro en ddlSitioVarAdi_SelectedIndexChanged:" + edsvA.Message, 3, Usuario.vchUsuario);
            }
        }

        private void cleanAdminVarAdi()
        {
            try
            {
                grvAddPaciente.DataSource = null;
                grvAddPaciente.DataBind();
                grvAddCita.DataSource = null;
                grvAddCita.DataBind();
                grvVarID.DataSource = null;
                grvVarID.DataBind();
            }
            catch (Exception ecAVA)
            {
                Log.EscribeLog("Existe un error en cleanAdminVarAdi: " + ecAVA.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void radCbxSitioCatalogo_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                cargaCatalogos();
            }
            catch (Exception eCat)
            {
                Log.EscribeLog("Existe un error en radCbxSitioCatalogo_SelectedIndexChanged: " + eCat.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void radCbxCatalogo_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                cargaCatalogos();
            }
            catch (Exception eCat)
            {
                Log.EscribeLog("Existe un error en radCbxCatalogo_SelectedIndexChanged: " + eCat.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void ajxPanelAdminCat_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            try
            {
                cargaCatalogos();
            }
            catch (Exception eCat)
            {
                Log.EscribeLog("Existe un error en ajxPanelAdminCat_AjaxRequest: " + eCat.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void ddlSitioMod_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cargarPrestacion();
            }
            catch (Exception eCat)
            {
                Log.EscribeLog("Existe un error en ddlSitioMod_SelectedIndexChanged: " + eCat.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void radAjaxPanel2_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            try
            {
                cargarPrestacion();
            }
            catch (Exception eCat)
            {
                Log.EscribeLog("Existe un error en radAjaxPanel2_AjaxRequest: " + eCat.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void AjaxPanelEquipo_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            try
            {
                cargarEquipo();
            }
            catch (Exception eCat)
            {
                Log.EscribeLog("Existe un error en AjaxPanelEquipo_AjaxRequest: " + eCat.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void radAjaxPanelAdicionales_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            try
            {
                Session["ddlSelectedTipoVariable"] = ddlTipoVariable.SelectedValue;
                grvAdicional.EditIndex = -1;
                cargarAdicional();
            }
            catch (Exception edd)
            {
                Log.EscribeLog("Existe un error en ddlTipoVariable_SelectedIndexChanged: " + edd.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void btnSearchEquipo_Click(object sender, EventArgs e)
        {
            try
            {
                cargarEquipo();
            }
            catch(Exception ebS)
            {
                Log.EscribeLog("Existe un error en btnSearchEquipo_Click: " + ebS.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void AjaxPanelModalidadEquipo_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            try
            {
                cargaListaModalidadEquipo();
                //cargarEquipo();
            }
            catch (Exception eCat)
            {
                Log.EscribeLog("Existe un error en AjaxPanelModalidadEquipo_AjaxRequest: " + eCat.Message, 3, Usuario.vchUsuario);
            }
        }
    }
}