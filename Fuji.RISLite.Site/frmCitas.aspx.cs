using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
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
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace Fuji.RISLite.Site
{
    public partial class frmCitas : System.Web.UI.Page
    {
        public string URL
        {
            get
            {
                return ConfigurationManager.AppSettings["URL"];
            }
        }

        public string dbLocalServer
        {
            get
            {
                return ConfigurationManager.AppSettings["dbLocalServer"];
            }
        }

        public string dbName
        {
            get
            {
                return ConfigurationManager.AppSettings["dbName"];
            }
        }

        public string dbUser
        {
            get
            {
                return ConfigurationManager.AppSettings["dbUser"];
            }
        }

        public string dbPass
        {
            get
            {
                return ConfigurationManager.AppSettings["dbPass"];
            }
        }

        public string CorreoString
        {
            get
            {
                return ConfigurationManager.AppSettings["CorreoString"];
            }
        }

        public string PassString
        {
            get
            {
                return ConfigurationManager.AppSettings["PassString"];
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
                    if (Session["User"] != null && Session["lstVistas"] != null)
                    {
                        Usuario = (clsUsuario)Session["User"];
                        if (Security.ValidateToken(Usuario.Token, Usuario.intUsuarioID.ToString(), Usuario.vchUsuario))
                        {
                            List<clsVistasUsuarios> lstVista = (List<clsVistasUsuarios>)Session["lstVistas"];
                            if (lstVista != null)
                            {
                                string vista = "frmCitas.aspx";
                                if (lstVista.Any(x => x.vchVistaIdentificador == vista))
                                {
                                    Usuario = (clsUsuario)Session["User"];
                                    if (Usuario != null)
                                    {
                                        RisService.updateEstatusCitaAutomatica(Usuario.vchUsuario);
                                        //customCalendarExtender.SelectedDate = Convert.ToDateTime("2017-01-01");
                                        //Date1.Text = "2017-01-01";
                                        RadDatePicker1.SelectedDate = Convert.ToDateTime("2017-01-01");
                                        //customCalendarExtender2.SelectedDate = DateTime.Now.AddDays(7);
                                        //Date2.Text = DateTime.Now.AddDays(7).ToString("yyyy-MM-dd");
                                        RadDatePicker2.SelectedDate = DateTime.Now.AddDays(7);
                                        cargarModalidad();
                                        cargarCitas(1);
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
                ShowMessage("Error al cargar la página: " + ePL.Message, MessageType.Error, "alert_container");
                Log.EscribeLog("Existe un error en Page_Load de frmConfiguracion: " + ePL.Message, 3, "");
            }
        }

        private void cargarModalidad()
        {
            try
            {
                CatalogoRequest request = new CatalogoRequest();
                clsUsuario _user = new clsUsuario();
                _user = Usuario;
                request.mdlUser = _user;
                List<tbl_CAT_Modalidad> response = new List<tbl_CAT_Modalidad>();
                response = RisService.getListModalidades(request);
                ddlModalidadBuesqueda.DataSource = null;
                ddlModalidadBuesqueda.Items.Clear();
                if (response != null)
                {
                    if (response.Count > 0)
                    {
                        ddlModalidadBuesqueda.DataSource = response.Where(x => x.intSitioID == Usuario.intSitioID);
                        ddlModalidadBuesqueda.DataTextField = "vchModalidad";
                        ddlModalidadBuesqueda.DataValueField = "intModalidadID";
                    }
                }
                ddlModalidadBuesqueda.DataBind();
                ddlModalidadBuesqueda.Items.Insert(0, new RadComboBoxItem("Todas...", "0"));
            }
            catch (Exception ecM)
            {
                Log.EscribeLog("Existe un error cargarModalidad: " + ecM.Message, 3, Usuario.vchUsuario);
            }
        }

        private void cargarCitas(int Inicio)
        {
            try
            {
                grvCitas.DataSource = null;
                List<stp_getCitas_Result> response = new List<stp_getCitas_Result>();
                CitaReporteRequest request = new CitaReporteRequest();
                request.mdlUser = Usuario;
                clsEstudioCita busqueda = new clsEstudioCita();
                busqueda = Inicio == 1 ? obtenerBusquedaCitaDefault() : obtenerBusquedaCita();
                request.mdlEstudio = busqueda;
                response = RisService.getCitas(request);
                if (response != null)
                {
                    if (response.Count > 0)
                        grvCitas.DataSource = response;
                }
                grvCitas.DataBind();
            }
            catch (Exception ecC)
            {
                Log.EscribeLog("Existe un error en cargarCitas: " + ecC.Message, 3, Usuario.vchUsuario);
            }
        }

        private clsEstudioCita obtenerBusquedaCitaDefault()
        {
            clsEstudioCita busqueda = new clsEstudioCita();
            try
            {
                busqueda.datFechaCita = Convert.ToDateTime(RadDatePicker1.SelectedDate);
                busqueda.datFechaCitaFin = Convert.ToDateTime(RadDatePicker2.SelectedDate);
                busqueda.vchNombrePaciente = txtNombreBus.Text;
                busqueda.intModalidadID = Convert.ToInt32(ddlModalidadBuesqueda.SelectedValue);
            }
            catch (Exception eOBC)
            {
                Log.EscribeLog("Existe un error en obtenerBusquedaCitaDefault: " + eOBC.Message, 3, Usuario.vchUsuario);
            }
            return busqueda;
        }

        private clsEstudioCita obtenerBusquedaCita()
        {
            clsEstudioCita busqueda = new clsEstudioCita();
            try
            {
                busqueda.datFechaCita = Convert.ToDateTime("2017-01-01");
                busqueda.datFechaCitaFin = DateTime.Now.AddDays(7);
                busqueda.vchNombrePaciente = txtNombreBus.Text;
                busqueda.intModalidadID = Convert.ToInt32(ddlModalidadBuesqueda.SelectedValue);
            }
            catch (Exception eOBC)
            {
                Log.EscribeLog("Existe un error en obtenerBusquedaCita: " + eOBC.Message, 3, Usuario.vchUsuario);
            }
            return busqueda;
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

        protected void btnBuscarCita_Click(object sender, EventArgs e)
        {
            try
            {
                cargarCitas(2);
            }
            catch (Exception eARC)
            {
                Log.EscribeLog("Existe un error en btnBuscarCita_Click: " + eARC.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void ajaxPanelCitas_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
        {
            try
            {
                cargarCitas(2);
            }
            catch (Exception eARC)
            {
                Log.EscribeLog("Existe un error en ajaxPanelCitas_AjaxRequest: " + eARC.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void grvCitas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Pager)
                {
                    Label lblTotalNumDePaginas = (Label)e.Row.FindControl("lblBandejaTotal");
                    lblTotalNumDePaginas.Text = grvCitas.PageCount.ToString();

                    TextBox txtIrAlaPagina = (TextBox)e.Row.FindControl("txtBandeja");
                    txtIrAlaPagina.Text = (grvCitas.PageIndex + 1).ToString();

                    DropDownList ddlTamPagina = (DropDownList)e.Row.FindControl("ddlBandeja");
                    ddlTamPagina.SelectedValue = grvCitas.PageSize.ToString();
                }

                if (e.Row.RowType != DataControlRowType.DataRow)
                {
                    return;
                }

                if (e.Row.DataItem != null)
                {
                    stp_getCitas_Result _mdl = (stp_getCitas_Result)e.Row.DataItem;
                    LinkButton ibtEstatus = (LinkButton)e.Row.FindControl("btnArribo");
                    //Label lblEstatus = (Label)e.Row.FindControl("lblArriboItem");

                    if (_mdl.intEstatusEstudio == 1)
                    {
                        ibtEstatus.ToolTip = "Marcar arribo del paciente a realizar estudio.";
                        ibtEstatus.Text = "<i class='fa fa-hand-pointer-o' aria-hidden='true' title='Marcar arribo del paciente a realizar estudio.' style='font-size:25px;'></i>";
                        ibtEstatus.Enabled = true;
                        ibtEstatus.Visible = true;
                    }
                    else
                    {
                        if (_mdl.intEstatusEstudio == 2)
                        {
                            //lblEstatus.Visible = true;
                            ibtEstatus.ToolTip = "Paciente Arribado a cita";
                            ibtEstatus.Text = "<i class='fa fa-pause' aria-hidden='true' title='Paciente Arribado a cita.' style='font-size:25px;'></i>";
                            ibtEstatus.Enabled = false;
                            ibtEstatus.Visible = true;
                            //lblEstatus.Text = "Paciente Arribado";
                        }
                        else
                        {
                            ibtEstatus.Enabled = false;
                            ibtEstatus.Visible = false;
                        }
                    }

                    LinkButton boton_entrega = (LinkButton)e.Row.FindControl("btnentrega");
                    LinkButton boton_agregar = (LinkButton)e.Row.FindControl("btnagregar");
                    LinkButton boton_renviarEMail = (LinkButton)e.Row.FindControl("btnReEmail");
                    LinkButton boton_imprimir = (LinkButton)e.Row.FindControl("btnImprimir");
                    LinkButton boton_cancelar = (LinkButton)e.Row.FindControl("btncancelar");

                    int Estatus_ = Convert.ToInt32(_mdl.intEstatusEstudio);

                    DateTime dt_inicio = Convert.ToDateTime(_mdl.datFechaInicio);
                    DateTime dt_hoy = DateTime.Now;

                    int result = DateTime.Compare(dt_inicio, dt_hoy);

                    if (result < 0)
                    {
                        string x = "";
                    }
                    else if (result == 0)
                    {
                        string y = "";
                    }
                    else
                    {

                    }
                    
                    switch (Estatus_)
                    {
                        case 1:
                            boton_entrega.Enabled = false;
                            boton_entrega.Visible = false;
                            boton_agregar.Enabled = true;
                            boton_agregar.Visible = true;
                            boton_renviarEMail.Enabled = true;
                            boton_renviarEMail.Visible = true;
                            boton_imprimir.Enabled = true;
                            boton_imprimir.Visible = true;
                            boton_cancelar.Enabled = true;
                            boton_cancelar.Visible = true;
                            break;
                        case 2:
                            boton_entrega.Enabled = false;
                            boton_entrega.Visible = false;
                            boton_agregar.Enabled = true;
                            boton_agregar.Visible = true;
                            boton_renviarEMail.Enabled = false;
                            boton_renviarEMail.Visible = false;
                            boton_imprimir.Enabled = false;
                            boton_imprimir.Visible = false;
                            boton_cancelar.Enabled = true;
                            boton_cancelar.Visible = true;
                            break;
                        case 3:
                            boton_entrega.Enabled = false;
                            boton_entrega.Visible = false;
                            boton_agregar.Enabled = false;
                            boton_agregar.Visible = false;
                            boton_renviarEMail.Enabled = false;
                            boton_renviarEMail.Visible = false;
                            boton_imprimir.Enabled = false;
                            boton_imprimir.Visible = false;
                            boton_cancelar.Enabled = true;
                            boton_cancelar.Visible = true;
                            break;
                        case 4:
                            boton_entrega.Enabled = true;
                            boton_entrega.Visible = true;
                            boton_agregar.Enabled = false;
                            boton_agregar.Visible = false;
                            boton_renviarEMail.Enabled = false;
                            boton_renviarEMail.Visible = false;
                            boton_imprimir.Enabled = false;
                            boton_imprimir.Visible = false;
                            boton_cancelar.Enabled = true;
                            boton_cancelar.Visible = true;
                            break;
                        case 5:
                            boton_entrega.Enabled = false;
                            boton_entrega.Visible = false;
                            boton_agregar.Enabled = false;
                            boton_agregar.Visible = false;
                            boton_renviarEMail.Enabled = false;
                            boton_renviarEMail.Visible = false;
                            boton_imprimir.Enabled = false;
                            boton_imprimir.Visible = false;
                            boton_cancelar.Enabled = true;
                            boton_cancelar.Visible = true;
                            break;
                        case 6:
                            boton_entrega.Enabled = false;
                            boton_entrega.Visible = false;
                            boton_agregar.Enabled = false;
                            boton_agregar.Visible = false;
                            boton_renviarEMail.Enabled = false;
                            boton_renviarEMail.Visible = false;
                            boton_imprimir.Enabled = false;
                            boton_imprimir.Visible = false;
                            boton_cancelar.Enabled = true;
                            boton_cancelar.Visible = true;
                            break;
                        case 7:

                            if (result < 0)
                            {
                                string x = "";
                            }
                            else if (result == 0)
                            {
                                string y = "";
                            }
                            else
                            {
                                ibtEstatus.ToolTip = "Marcar arribo del paciente a realizar estudio.";
                                ibtEstatus.Text = "<i class='fa fa-hand-pointer-o' aria-hidden='true' title='Marcar arribo del paciente a realizar estudio.' style='font-size:25px;'></i>";
                                ibtEstatus.Enabled = true;
                                ibtEstatus.Visible = true;
                            }

                            boton_entrega.Enabled = false;
                            boton_entrega.Visible = false;
                            boton_agregar.Enabled = false;
                            boton_agregar.Visible = false;
                            boton_renviarEMail.Enabled = false;
                            boton_renviarEMail.Visible = false;
                            boton_imprimir.Enabled = false;
                            boton_imprimir.Visible = false;
                            boton_cancelar.Enabled = false;
                            boton_cancelar.Visible = false;
                            break;
                        case 8:
                            boton_entrega.Enabled = false;
                            boton_entrega.Visible = false;
                            boton_agregar.Enabled = false;
                            boton_agregar.Visible = false;
                            boton_renviarEMail.Enabled = false;
                            boton_renviarEMail.Visible = false;
                            boton_imprimir.Enabled = false;
                            boton_imprimir.Visible = false;
                            boton_cancelar.Enabled = false;
                            boton_cancelar.Visible = false;
                            break;
                    }

                }
            }
            catch (Exception eGUP)
            {
                Log.EscribeLog("Existe un error en grvCitas_RowDataBound: " + eGUP.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void grvCitas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                if (e.NewPageIndex >= 0)
                {
                    this.grvCitas.PageIndex = e.NewPageIndex;
                    cargarCitas(2);
                }
            }
            catch (Exception ex)
            {
                Log.EscribeLog("Existe un error grvCitas_PageIndexChanging: " + ex.Message, 3, Usuario.vchUsuario);
            }
        }

        //protected void grvCitas_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        //{

        //}

        protected void grvCitas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int intEstudioID = 0;
                int intCitaID = 0;
                clsUsuario mdl = new clsUsuario();
                switch (e.CommandName)
                {
                    case "Email":
                        intCitaID = Convert.ToInt32(e.CommandArgument.ToString());
                        PrepararCorreo(intCitaID);
                        ShowMessage("Se envío el correo.", MessageType.Correcto, "alert_container");
                        break;
                    case "Imprimir":
                        intCitaID = Convert.ToInt32(e.CommandArgument.ToString());
                        imprimirCita(intCitaID);
                        ShowMessage("Impresion correcta.", MessageType.Correcto, "alert_container");
                        break;
                    case "Arribo":
                        intEstudioID = Convert.ToInt32(e.CommandArgument.ToString());
                        CitaReporteResponse response = new CitaReporteResponse();
                        CitaReporteRequest request = new CitaReporteRequest();
                        request.mdlUser = Usuario;
                        request.intEstatusID = 2;
                        request.intEstudioID = intEstudioID;
                        response = RisService.setEstatusEstudio(request);
                        if (response != null)
                        {
                            if (response.Success)
                            {
                                ShowMessage("Se marcó el estudio pendiente para iniciar.", MessageType.Correcto, "alert_container");
                                cargarCitas(2);
                            }
                            else
                            {
                                ShowMessage("Verificar la información: " + response.Mensaje, MessageType.Advertencia, "alert_container");
                            }
                        }
                        else
                        {
                            ShowMessage("Verificar la información.", MessageType.Error, "alert_container");
                        }
                        break;
                    case "Cancelar":
                        intEstudioID = Convert.ToInt32(e.CommandArgument.ToString());
                        CitaReporteResponse response_canc = new CitaReporteResponse();
                        CitaReporteRequest request_canc = new CitaReporteRequest();
                        request_canc.mdlUser = Usuario;
                        request_canc.intEstatusID = 7;
                        request_canc.intEstudioID = intEstudioID;
                        response_canc = RisService.setEstatusEstudio(request_canc);
                        if (response_canc != null)
                        {
                            if (response_canc.Success)
                            {
                                ShowMessage("Se marcó el estudio cancelado.", MessageType.Correcto, "alert_container");
                                cargarCitas(2);
                            }
                            else
                            {
                                ShowMessage("Verificar la información de cancelado: " + response_canc.Mensaje, MessageType.Advertencia, "alert_container");
                            }
                        }
                        else
                        {
                            ShowMessage("Verificar la información de cancelado.", MessageType.Error, "alert_container");
                        }
                        break;
                    case "Entrega":
                        intEstudioID = Convert.ToInt32(e.CommandArgument.ToString());
                        CitaReporteResponse response_entrega = new CitaReporteResponse();
                        CitaReporteRequest request_entrega = new CitaReporteRequest();
                        request_entrega.mdlUser = Usuario;
                        request_entrega.intEstatusID = 2;
                        request_entrega.intEstudioID = intEstudioID;
                        //response_entrega = RisService.setEstatusEstudio(request_entrega);
                        if (response_entrega != null)
                        {
                            if (response_entrega.Success)
                            {
                                ShowMessage("Se entrego el estudio.", MessageType.Correcto, "alert_container");
                                cargarCitas(2);
                            }
                            else
                            {
                                ShowMessage("Verificar la información de entregado: " + response_entrega.Mensaje, MessageType.Advertencia, "alert_container");
                            }
                        }
                        else
                        {
                            ShowMessage("Verificar la información de entregado.", MessageType.Error, "alert_container");
                        }
                        break;
                    case "Agregar":
                        //intEstudioID = Convert.ToInt32(e.CommandArgument.ToString());
                        //string[] lista_par_estudio = e.CommandArgument.ToString().Split('|') ;
                        //string idestudio = lista_par_estudio[0];
                        //string idcita = lista_par_estudio[1];

                        //int index = grvCitas.SelectedIndex;
                        //string ss = grvCitas.DataKeys[].Values[0];
                        //string idestudio = grvCitas.DataKeys[index].Value.ToString();



                        Control ctl = e.CommandSource as Control;
                        GridViewRow CurrentRow = ctl.NamingContainer as GridViewRow;
                        string _intEstudioID = grvCitas.DataKeys[CurrentRow.RowIndex].Values["intEstudioID"].ToString();
                        //string vchNombre = grvUsuario.DataKeys[CurrentRow.RowIndex].Values["vchNombre"].ToString();


                        string par_citas = e.CommandArgument.ToString();
                        string busqueda = Security.Encrypt(_intEstudioID + "|" + par_citas.ToString());
                        Response.Redirect(URL + "/frmAddDate.aspx?var=" + busqueda, true);
                        break;
                }
            }
            catch (Exception eRU)
            {
                Log.EscribeLog("Existe un error grvAddCita_RowCommand: " + eRU.Message, 3, Usuario.vchUsuario);
            }
        }

        private void imprimirCita(int intCitaId)
        {
            try
            {
                string urlabrir = "";
                string id = Security.Encrypt(intCitaId.ToString());
                urlabrir = URL + "/frmDownLoadCita.aspx?appointment=" + id;
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Cerrar", "javascript:Redirecciona('" + urlabrir + "');", true);
            }
            catch (Exception eImprimircita)
            {
                Log.EscribeLog("Existe un error en imprimirCita: " + eImprimircita, 3, Usuario.vchUsuario);
            }
        }

        private void PrepararCorreo(int intCitaID)
        {
            try
            {
                List<stp_getCitaReporte_Result> Citareporte = new List<stp_getCitaReporte_Result>();
                CitaReporteRequest request = new CitaReporteRequest();
                request.intCitaId = intCitaID;
                request.mdlUser = Usuario;
                Citareporte = RisService.getCitaReporte(request);
                if (Citareporte != null)
                {
                    clsCorreo correo = new clsCorreo();
                    correo.asunto = "Proxima Cita.";
                    correo.bitReporte = true;
                    correo.toEmail = Citareporte.First().vchEmail;
                    correo.vchNombrePaciente = Citareporte.First().vchNombre;
                    correo.intCitaID = intCitaID;
                    correo.htmlCorreo = obtenerMachote().Replace("SSSitio", Citareporte.First().vchNombreSitio);

                    tbl_Conf_CorreoSitio configCorreo = new tbl_Conf_CorreoSitio();
                    configCorreo = obtenerDatosCorreoSitio();
                    if (correo != null && configCorreo != null)
                    {
                        Log.EscribeLog("Correo para: " + correo.toEmail, 3, Usuario.vchUsuario);
                        Log.EscribeLog("Correo de: " + configCorreo.vchCorreo, 3, Usuario.vchUsuario);
                        enviarCorreo(correo, configCorreo, Citareporte);
                    }
                }

            }
            catch (Exception ePC)
            {
                Log.EscribeLog("Existe un error en PrepararCorreo: " + ePC.Message, 3, Usuario.vchUsuario);
            }
        }

        private tbl_Conf_CorreoSitio obtenerDatosCorreoSitio()
        {
            tbl_Conf_CorreoSitio correo = new tbl_Conf_CorreoSitio();
            try
            {

                ConfigEmailResponse response = new ConfigEmailResponse();
                ConfigEmailRequest request = new ConfigEmailRequest();
                request.intSitioID = Usuario.intSitioID;
                request.mdlUser = Usuario;
                response = RisService.getConfigEmail(request);
                if (response != null)
                {
                    correo = response.mldConfigEmail;
                }
            }
            catch (Exception eoDC)
            {
                Log.EscribeLog("Existe un error en obtenerDatosCorreoSitio: " + eoDC.Message, 3, Usuario.vchUsuario);
            }
            return correo;
        }

        private string obtenerMachote()
        {
            string texto = "";
            try
            {
                if (File.Exists(Server.MapPath("~/Data/CorreoCita.txt")))
                {
                    texto = File.ReadAllText(Server.MapPath("~/Data/CorreoCita.txt"));
                }
                else
                {
                    texto = "<table width='350px' style='FONT-SIZE:11px;font-family:Tahoma,Helvetica,sans-serif;padding:2px;background-color:#fdfffe;BORDER-RIGHT:#0c922e 3px solid;BORDER-TOP:#0c922e 3px solid;BORDER-LEFT:#0c922e 3px solid;BORDER-BOTTOM:#0c922e 3px solid'>" +
                            "<tbody>" +
                            "<tr><td colspan='2'>SSSitio</td></tr><tr><td colspan='2'><hr></td></tr><tr><td colspan='2' align='center' style='background-color:#fefefe'>Nueva <span class='il'>Cita</span></td></tr>" +
                            "<tr><td colspan='2'><hr></td></tr>" +
                            "<tr><td colspan='2'>Apreciable paciente:</td></tr>" +
                            "<tr><td colspan='2'>Adjunto encontrara información de su proxima cita.</td></tr>" +
                            "<tr><td align='center' colspan='2'><font color='#014615' size='3'>FUJIFILM MEXICO</font></td></tr>" +
                            "<tr><td colspan='2'>Este correo&nbsp; electronico&nbsp; es&nbsp; confidencial, esta&nbsp; legalmente&nbsp; protegido y/o puede contener informacion privilegiada. Si usted no es su destinatario o no es alguna persona autorizada por este para recibir sus correos electronicos, NO debera usted utilizar, copiar, revelar, o&nbsp; tomar&nbsp; ninguna&nbsp; accion&nbsp; basada&nbsp; en este correo electronico o cualquier otra informacion incluida en el (incluyendo todos los documentos adjuntos). </td></tr>" +
                            "</tbody></table>";
                }
            }
            catch (Exception eoM)
            {
                texto = "<table width='350px' style='FONT-SIZE:11px;font-family:Tahoma,Helvetica,sans-serif;padding:2px;background-color:#fdfffe;BORDER-RIGHT:#0c922e 3px solid;BORDER-TOP:#0c922e 3px solid;BORDER-LEFT:#0c922e 3px solid;BORDER-BOTTOM:#0c922e 3px solid'>" +
                            "<tbody>" +
                            "<tr><td colspan='2'>SSSitio</td></tr><tr><td colspan='2'><hr></td></tr><tr><td colspan='2' align='center' style='background-color:#fefefe'>Nueva <span class='il'>Cita</span></td></tr>" +
                            "<tr><td colspan='2'><hr></td></tr>" +
                            "<tr><td colspan='2'>Apreciable paciente:</td></tr>" +
                            "<tr><td colspan='2'>Adjunto encontrara información de su proxima cita.</td></tr>" +
                            "<tr><td align='center' colspan='2'><font color='#014615' size='3'>FUJIFILM MEXICO</font></td></tr>" +
                            "<tr><td colspan='2'>Este correo&nbsp; electronico&nbsp; es&nbsp; confidencial, esta&nbsp; legalmente&nbsp; protegido y/o puede contener informacion privilegiada. Si usted no es su destinatario o no es alguna persona autorizada por este para recibir sus correos electronicos, NO debera usted utilizar, copiar, revelar, o&nbsp; tomar&nbsp; ninguna&nbsp; accion&nbsp; basada&nbsp; en este correo electronico o cualquier otra informacion incluida en el (incluyendo todos los documentos adjuntos). </td></tr>" +
                            "</tbody></table>";
                Log.EscribeLog("Existe un error al obtener el machote del correo: " + eoM.Message, 3, Usuario.vchUsuario);
            }
            return texto;
        }

        private Attachment CreatePDF(int intCitaID, List<stp_getCitaReporte_Result> dataSource)
        {
            Attachment pdfAtt = null;
            try
            {
                ReportDocument crystalReport = new ReportDocument();
                crystalReport.Load(Server.MapPath("~/Data/rptCitaReporte.rpt"));
                //string ParameterName = "intCitaID";
                //object val = intCitaID;
                //ParameterValues prms;
                //ParameterDiscreteValue prm = new ParameterDiscreteValue();
                //prms = crystalReport.DataDefinition.ParameterFields[ParameterName].CurrentValues;
                //prm.Value = val;
                //prms.Add(prm);
                //crystalReport.DataDefinition.ParameterFields[ParameterName].ApplyCurrentValues(prms);
                crystalReport.SetParameterValue("@intCitaID", intCitaID);
                //crystalReport.SetParameterValue(0, intCitaID);
                crystalReport.SetDatabaseLogon(dbUser, dbPass, dbLocalServer, dbName);
                var stream = crystalReport.ExportToStream(ExportFormatType.PortableDocFormat);
                pdfAtt = new Attachment(stream, "Cita_" + intCitaID + ".pdf");
            }
            catch (Exception eCPDF)
            {
                Log.EscribeLog("Existe un error al crear el PDF: " + eCPDF.Message + " *" + eCPDF.InnerException, 3, Usuario.vchUsuario);
            }
            return pdfAtt;
        }

        private bool enviarCorreo(clsCorreo correo, tbl_Conf_CorreoSitio configCorreo, List<stp_getCitaReporte_Result> dataSource)
        {
            bool valido = false;
            try
            {
                Log.EscribeLog("Inicio de creacion de email.", 1, Usuario.vchUsuario);
                MailMessage mail = new MailMessage();
                Log.EscribeLog("From: " + configCorreo.vchCorreo, 1, Usuario.vchUsuario);
                mail.From = new MailAddress(configCorreo.vchCorreo);
                string[] lista_correos = correo.toEmail.Split(';');

                foreach (string destino in lista_correos)
                {
                    mail.To.Add(destino);
                }
                Log.EscribeLog("Email para: " + configCorreo.vchCorreo, 1, Usuario.vchUsuario);
                mail.Subject = correo.asunto;
                mail.IsBodyHtml = true;
                mail.Body = correo.htmlCorreo;

                Log.EscribeLog("Se inicia reporte", 1, Usuario.vchUsuario);
                if (correo.bitReporte)
                {
                    try
                    {
                        mail.Attachments.Add(CreatePDF(correo.intCitaID, dataSource));
                    }
                    catch (Exception e)
                    {
                        Log.EscribeLog("Existe un error al adjuntar el pdf: " + e.Message, 3, Usuario.vchUsuario);
                    }
                }
                try
                {
                    Log.EscribeLog("Proceso de envío", 1, Usuario.vchUsuario);
                    System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
                    //string correoSit = Security.Decrypt(CorreoString);
                    //string passSit = Security.Decrypt(PassString);
                    //smtp.Credentials = new System.Net.NetworkCredential(correoSit, passSit);
                    smtp.Credentials = new System.Net.NetworkCredential(configCorreo.vchCorreo, configCorreo.vchPassword);
                    smtp.Host = configCorreo.vchHost != "" ? configCorreo.vchHost : "smtp.gmail.com";
                    smtp.Port = (int)configCorreo.intPort > 0 ? (int)configCorreo.intPort : 587;
                    smtp.EnableSsl = (bool)configCorreo.BitEnableSsl;
                    smtp.Send(mail);
                    valido = true;
                }
                catch (Exception except)
                {
                    valido = false;
                    Log.EscribeLog("Existe un error al intentar enviar el correo: " + except.Message, 3, Usuario.vchUsuario);
                }
            }
            catch (Exception eeC)
            {
                Log.EscribeLog("Existe un error en enviarCorreo: " + eeC.Message, 3, Usuario.vchUsuario);
                valido = false;
            }
            return valido;
        }

        //protected void grvCitas_RowEditing(object sender, GridViewEditEventArgs e)
        //{

        //}

        //protected void grvCitas_RowUpdating(object sender, GridViewUpdateEventArgs e)
        //{

        //}

        protected void ddlBandeja_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DropDownList dropDownList = (DropDownList)sender;
                if (int.Parse(dropDownList.SelectedValue) != 0)
                {
                    this.grvCitas.AllowPaging = true;
                    this.grvCitas.PageSize = int.Parse(dropDownList.SelectedValue);
                }
                else
                    this.grvCitas.AllowPaging = false;
                this.cargarCitas(2);
            }
            catch (Exception eddS)
            {
                Log.EscribeLog("Existe un error ddlBandeja_SelectedIndexChanged de frmCitas: " + eddS.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void txtBandeja_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox txtBandejaAvaluosGoToPage = (TextBox)sender;
                int numeroPagina;
                if (int.TryParse(txtBandejaAvaluosGoToPage.Text.Trim(), out numeroPagina))
                    this.grvCitas.PageIndex = numeroPagina - 1;
                else
                    this.grvCitas.PageIndex = 0;
                this.cargarCitas(2);
            }
            catch (Exception ex)
            {
                Log.EscribeLog("Existe un error txtBandeja_TextChanged de frmCitas: " + ex.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void grvCitas_DataBound(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i <= grvCitas.Rows.Count - 1; i++)
                {
                    grvCitas.Rows[i].Cells[5].BackColor = System.Drawing.ColorTranslator.FromHtml(grvCitas.Rows[i].Cells[12].Text);
                }
            }
            catch (Exception eRDB)
            {
                Log.EscribeLog("Existe un error en grvCitas_DataBound: " + eRDB.Message, 3, Usuario.vchUsuario);
            }
        }
    }
}