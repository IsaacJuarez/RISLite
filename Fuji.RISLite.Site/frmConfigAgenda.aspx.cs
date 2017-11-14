using Fuji.RISLite.Entidades.Extensions;
using Fuji.RISLite.Entities;
using Fuji.RISLite.Site.Services;
using Fuji.RISLite.Site.Services.DataContract;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Fuji.RISLite.Site;
using Fuji.RISLite.Entidades.DataBase;
using System.Linq;
using System.Globalization;


using System.Web;


namespace Fuji.RISLite.Site
{
    public partial class frmConfigAgenda : System.Web.UI.Page
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
                    if (Session["User"] != null && Session["lstVistas"] != null)
                    {
                        List<clsVistasUsuarios> lstVista = (List<clsVistasUsuarios>)Session["lstVistas"];
                        if (lstVista != null)
                        {
                            string vista = "frmConfigAgenda.aspx";
                            if (lstVista.Any(x => x.vchVistaIdentificador == vista))
                            {
                                Usuario = (clsUsuario)Session["User"];
                                if (Usuario != null)
                                {
                                    cargarAgenda(1);
                                    cargaConfigScheduler(1);
                                    carga_dias_feriados(1);
                                    carga_Horas_muertas(1);
                                    HF_validacion_HM.Value = "false";
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
                Log.EscribeLog("Existe un error en Page_Load de frmConfigAgenda: " + ePL.Message, 3, "");
            }
        }

    

        private void cargarAgenda()
        {
            try
            {
                GV_Agenda.DataSource = null;
                List<clsConfAgenda> lstTec = new List<clsConfAgenda>();
                AgendaRequest request = new AgendaRequest();
                request.mdlUser = Usuario;
                request.mdlagenda.intSitioID = 1;
                lstTec = RisService.getListAgenda(request);
                if (lstTec != null)
                {
                    if (lstTec.Count > 0)
                    {
                        GV_Agenda.DataSource = lstTec;
                    }
                }
                GV_Agenda.DataBind();
            }
            catch (Exception ecU)
            {
                Log.EscribeLog("Existe un error en cargarAgenda: " + ecU.Message, 3, Usuario.vchUsuario);
            }
        }

        private void cargarAgenda(int idsitio)
        {
            try
            {
                GV_Agenda.DataSource = null;
                List<clsConfAgenda> lstTec = new List<clsConfAgenda>();
                AgendaRequest request = new AgendaRequest();
                request.mdlUser = Usuario;
                request.mdlagenda.intSitioID = idsitio;
                lstTec = RisService.getListAgenda(request);
                if (lstTec != null)
                {
                    if (lstTec.Count > 0)
                    {
                        GV_Agenda.DataSource = lstTec;
                    }
                }
                GV_Agenda.DataBind();
            }
            catch (Exception ecU)
            {
                Log.EscribeLog("Existe un error en cargarAgenda: " + ecU.Message, 3, Usuario.vchUsuario);
            }
        }


        private void cargaConfigScheduler(int idsitio)
        {
            try
            {
                List<clsConfScheduler> lstTec = new List<clsConfScheduler>();
                ConfigSchedulerRequest request = new ConfigSchedulerRequest();
                request.mdlUser = Usuario;
                //request.mdlConfScheduler.intSitioID = Convert.ToInt32(RCB_Sitio_confiagenda.SelectedValue
                request.mdlConfScheduler.intSitioID = idsitio;
                lstTec = RisService.getConfScheduler_Sitio(request);

                foreach (var item in lstTec)
                {
                    RTP_Inicio.SelectedTime = item.tmeInicioDia;
                    RTP_Fin.SelectedTime = item.tmeFinDia;
                    TB_intervalo.Text = item.intIntervalo.ToString();
                }
            }

            catch (Exception ex)
            {
                Log.EscribeLog("Existe un error en cargaConfigScheduler: " + ex.Message, 3, Usuario.vchNombre);
            }

            try
            {
                List<clsDiaSemana> lstTec = new List<clsDiaSemana>();
                ConfigScheduler_DiaSemanaRequest request = new ConfigScheduler_DiaSemanaRequest();
                request.mdlUser = Usuario;
                //int idsitio = Convert.ToInt32(RCB_Sitio_confiagenda.SelectedValue);
                lstTec = RisService.getDiaSemanaConfScheduler_Sitio(request, idsitio);

                //int contador_semana = 0;

                RB_Lunes.Checked = false;
                RBMartes.Checked = false;
                RBMiercoles.Checked = false;
                RBJueves.Checked = false;
                RBViernes.Checked = false;
                RBSabado.Checked = false;
                RB_Domingo.Checked = false;

                foreach (var item in lstTec)
                {

                    if (item.vchDiaSemana == "Lunes")
                    {
                        RB_Lunes.Checked = item.bitActivo;
                    }

                    if (item.vchDiaSemana == "Martes")
                    {
                        RBMartes.Checked = item.bitActivo;
                    }

                    if (item.vchDiaSemana == "Miercoles")
                    {
                        RBMiercoles.Checked = item.bitActivo;
                    }

                    if (item.vchDiaSemana == "Jueves")
                    {
                        RBJueves.Checked = item.bitActivo;
                    }

                    if (item.vchDiaSemana == "Viernes")
                    {
                        RBViernes.Checked = item.bitActivo;
                    }

                    if (item.vchDiaSemana == "Sabado")
                    {
                        RBSabado.Checked = item.bitActivo;
                    }

                    if (item.vchDiaSemana == "Domingo")
                    {
                        RB_Domingo.Checked = item.bitActivo;
                    }
                    
                }
            }

            catch (Exception ex)
            {
                Log.EscribeLog("Existe un error en CargaDiaSemanaRequest: " + ex.Message, 3, Usuario.vchNombre);
            }         

            try
            {
                List<clsHoraMuerta> lstTec = new List<clsHoraMuerta>();
                ConfigScheduler_HoraMuertaRequest request = new ConfigScheduler_HoraMuertaRequest();
                request.mdlUser = Usuario;
                request.mdlHMScheduler.intSitioID = idsitio;
                lstTec = RisService.getHoraMuertaConfScheduler(request);

                foreach (var item in lstTec)
                {
                    RTP_HM_Inicio.SelectedTime = TimeSpan.Parse(item.tmeInicio);
                    RTP_HM_Fin.SelectedTime = TimeSpan.Parse(item.tmeFin);
                }
            }

            catch (Exception ex)
            {
                Log.EscribeLog("Existe un error en ConfigScheduler_HoraMuertaRequest: " + ex.Message, 3, Usuario.vchNombre);
            }

        }

        private void cargaConfigScheduler()
        {
            try
            {
                List<clsConfScheduler> lstTec = new List<clsConfScheduler>();
                ConfigSchedulerRequest request = new ConfigSchedulerRequest();
                request.mdlUser = Usuario;
                request.mdlConfScheduler.intSitioID = Convert.ToInt32(RCB_Sitio_confiagenda.SelectedValue);
                lstTec = RisService.getConfScheduler_Sitio(request);

                foreach (var item in lstTec)
                {
                    RTP_Inicio.SelectedTime = item.tmeInicioDia;
                    RTP_Fin.SelectedTime = item.tmeFinDia;
                    TB_intervalo.Text = item.intIntervalo.ToString();
                }
            }

            catch (Exception ex)
            {
                Log.EscribeLog("Existe un error en cargaConfigScheduler: " + ex.Message, 3, Usuario.vchNombre);
            }

            try
            {
                List<clsDiaSemana> lstTec = new List<clsDiaSemana>();
                ConfigScheduler_DiaSemanaRequest request = new ConfigScheduler_DiaSemanaRequest();
                request.mdlUser = Usuario;
                int idsitio = Convert.ToInt32(RCB_Sitio_confiagenda.SelectedValue);
                lstTec = RisService.getDiaSemanaConfScheduler_Sitio(request, idsitio);

                int contador_semana = 0;

                foreach (var item in lstTec)
                {

                    switch (contador_semana)
                    {
                        case 0:
                            RB_Lunes.Checked = item.bitActivo;
                            break;
                        case 1:
                            RBMartes.Checked = item.bitActivo;
                            break;
                        case 2:
                            RBMiercoles.Checked = item.bitActivo;
                            break;
                        case 3:
                            RBJueves.Checked = item.bitActivo;
                            break;
                        case 4:
                            RBViernes.Checked = item.bitActivo;
                            break;
                        case 5:
                            RBSabado.Checked = item.bitActivo;
                            break;
                        case 6:
                            RB_Domingo.Checked = item.bitActivo;
                            break;
                    }
                    contador_semana++;
                }
            }

            catch (Exception ex)
            {
                Log.EscribeLog("Existe un error en CargaDiaSemanaRequest: " + ex.Message, 3, Usuario.vchNombre);
            }

            //try
            //{
            //    List<clsDiaFeriado> lstTec = new List<clsDiaFeriado>();
            //    ConfigScheduler_DiaFeriado request = new ConfigScheduler_DiaFeriado();
            //    request.mdlUser = Usuario;
            //    lstTec = RisService.getDiaFeriadoConfScheduler(request);

            //    //foreach (var item in lstTec)
            //    //{
            //    //    //RTP_HM_Inicio.SelectedTime = item.tmeInicio;
            //    //    //RTP_HM_Fin.SelectedTime = item.tmeFin;
            //    //}
            //}

            //catch (Exception ex)
            //{
            //    Log.EscribeLog("Existe un error en ConfigScheduler_DiaFeriado: " + ex.Message, 3, Usuario.vchNombre);
            //}

            try
            {
                List<clsHoraMuerta> lstTec = new List<clsHoraMuerta>();
                ConfigScheduler_HoraMuertaRequest request = new ConfigScheduler_HoraMuertaRequest();
                request.mdlUser = Usuario;
                lstTec = RisService.getHoraMuertaConfScheduler(request);

                foreach (var item in lstTec)
                {
                    RTP_HM_Inicio.SelectedTime = TimeSpan.Parse(item.tmeInicio);
                    RTP_HM_Fin.SelectedTime = TimeSpan.Parse(item.tmeFin);
                }
            }

            catch (Exception ex)
            {
                Log.EscribeLog("Existe un error en ConfigScheduler_HoraMuertaRequest: " + ex.Message, 3, Usuario.vchNombre);
            }

        }

        private void cargaIntervalo()
        {
            try
            {
                GV_Agenda.DataSource = null;
                List<clsConfAgenda> lstTec = new List<clsConfAgenda>();
                AgendaRequest request = new AgendaRequest();
                request.mdlUser = Usuario;
                lstTec = RisService.getListAgenda(request);
                if (lstTec != null)
                {
                    if (lstTec.Count > 0)
                    {
                        GV_Agenda.DataSource = lstTec;
                    }
                }
                GV_Agenda.DataBind();
            }
            catch (Exception ecU)
            {
                Log.EscribeLog("Existe un error en cargarAgenda: " + ecU.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void ddlBandeja_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void txtBandeja_TextChanged(object sender, EventArgs e)
        {

        }

        protected void GV_Agenda_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            GV_Agenda.EditIndex = e.NewEditIndex;
            cargarAgenda(Convert.ToInt32(RCB_SItio.SelectedValue));
        }

        protected void GV_Agenda_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            try
            {
                int intmodalidadID = 0;
                clsUsuario mdl = new clsUsuario();
                switch (e.CommandName)
                {
                    case "Estatus":
                        intmodalidadID = Convert.ToInt32(e.CommandArgument.ToString());
                        AgendaRequest request = new AgendaRequest();
                        AgendaResponse response = new AgendaResponse();
                        request.mdlUser = Usuario;
                        clsConfAgenda mdlVar = new clsConfAgenda();
                        mdlVar.intModalidadID = intmodalidadID;
                        if (mdl != null)
                        {
                            request.mdlagenda = mdlVar;
                            response = RisService.setEstatusAgenda(request);
                            if (response != null)
                            {
                                if (response.Success)
                                {
                                    ShowMessage("Se actualizo correctamente la modalidad", MessageType.Correcto, "alert_container");
                                    //grvAddPaciente.EditIndex = -1;
                                    cargarAgenda();
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
                Log.EscribeLog("Existe un error GV_AGENDA: " + eRU.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void GV_Agenda_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Pager)
                {
                    Label lblTotalNumDePaginas = (Label)e.Row.FindControl("lblBandejaTotal");
                    lblTotalNumDePaginas.Text = GV_Agenda.PageCount.ToString();

                    TextBox txtIrAlaPagina = (TextBox)e.Row.FindControl("txtBandeja");
                    txtIrAlaPagina.Text = (GV_Agenda.PageIndex + 1).ToString();

                    DropDownList ddlTamPagina = (DropDownList)e.Row.FindControl("ddlBandeja");
                    ddlTamPagina.SelectedValue = GV_Agenda.PageSize.ToString();
                }

                if (e.Row.RowType != DataControlRowType.DataRow)
                {
                    return;
                }

                if (e.Row.DataItem != null)
                {
                    clsConfAgenda _mdl = (clsConfAgenda)e.Row.DataItem;
                    ImageButton ibtEstatus = (ImageButton)e.Row.FindControl("imbEstatus");
                    ibtEstatus.Attributes.Add("onclick", "javascript:return confirm('¿Desea realizar el cambio de estatus de la modalidad seleccionada?');");
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

        protected void GV_Agenda_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {
            
        }       

        protected void GV_Agenda_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            bool bandera_update_agenda = false;
            
            try
            {
                AgendaRequest request = new AgendaRequest();

                request.mdlUser = Usuario;
                clsConfAgenda agenda = new clsConfAgenda();

                TextBox idmodalidad = (TextBox)GV_Agenda.Rows[e.RowIndex].FindControl("TxtIDModalidad");
                TextBox modalidad = (TextBox)GV_Agenda.Rows[e.RowIndex].FindControl("Txtmodalidad");
                TextBox codigo = (TextBox)GV_Agenda.Rows[e.RowIndex].FindControl("Txtcodigo");
                DropDownList duracion = (DropDownList)GV_Agenda.Rows[e.RowIndex].FindControl("DDL_duracion");

                Control colorRCP = (Control)GV_Agenda.Rows[e.RowIndex].FindControl("ucRadColorPicker1");
                string color_seleccionado = ((Fuji.RISLite.Site.WebUserControl2)colorRCP).DBSelectedColor;



                agenda.intModalidadID = Convert.ToInt32(idmodalidad.Text);
                agenda.vchCodigo = codigo.Text;
                agenda.vchModalidad = modalidad.Text;
                agenda.intDuracionGen = Convert.ToInt32(duracion.SelectedValue);
                agenda.vchColor = color_seleccionado;
                agenda.intSitioID = Convert.ToInt32(RCB_SItio.SelectedValue);

                
                //System.Drawing.ColorTranslator.ToHtml(RadColorPicker1.SelectedColor);
                request.mdlagenda = agenda;

                bandera_update_agenda = RisService.UpdateAgenda(request);

                GV_Agenda.EditIndex = -1;
                cargarAgenda(Convert.ToInt32(RCB_SItio.SelectedValue));

                ShowMessage("Se actualizo correctamente la modalidad", MessageType.Correcto, "alert_container");
            }
            catch(Exception eRU)
            {
                ShowMessage("Favor de revisar la información.", MessageType.Error, "alert_container");
                Log.EscribeLog("Existe un error GV_Agenda_RowUpdating: " + eRU.Message, 3, Usuario.vchUsuario);
            }
        }

        private clsConfAgenda ObtenerDatosAgenda()
        {
            clsConfAgenda modalidad = new clsConfAgenda();
            try
            {
                modalidad.vchCodigo = txt_Modalidad.Text;
                modalidad.vchModalidad = txtDescripcion.Text;
                //modalidad.vchColor = RCP_add_modadlidad.SelectedColor.ToString();
                modalidad.vchColor = System.Drawing.ColorTranslator.ToHtml(RCP_add_modadlidad.SelectedColor);
                modalidad.datFecha = DateTime.Today;
                modalidad.intDuracionGen = Convert.ToInt32(DDL_duracion_Nuevo.SelectedValue);
                modalidad.vchUserAdmin = Usuario.vchUsuario.ToUpper();
                modalidad.intSitioID = Convert.ToInt32(RCB_SItio.SelectedValue);
                modalidad.bitActivo = true;
            }
            catch (Exception eODUA)
            {
                Log.EscribeLog("Existe un error en ObtenerDatosConfAgenda: " + eODUA.Message, 3, Usuario.vchUsuario);
            }
            return modalidad;
        }

        protected void GV_Agenda_RowCancelingEdit1(object sender, GridViewCancelEditEventArgs e)
        {
            try
            {
                GV_Agenda.EditIndex = -1;
                cargarAgenda();

            }
            catch (Exception eCE)
            {
                Log.EscribeLog("Existe un error en GV_Agenda_RowCancelingEdit: " + eCE.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void btnAgregar_Click1(object sender, EventArgs e)
        {
            try
            {
                clsConfAgenda datos_modalidad = new clsConfAgenda();
                datos_modalidad = ObtenerDatosAgenda();
                if (datos_modalidad != null)
                {
                    AgendaRequest request = new AgendaRequest();
                    AgendaResponse response = new AgendaResponse();
                    request.mdlUser = Usuario;
                    request.mdlagenda = datos_modalidad;
                    if (request != null)
                    {
                        response = RisService.setAgenda(request);
                        if (response != null)
                        {
                            if (response.Success)
                            {                               
                                txtDescripcion.Text = "";
                                txt_Modalidad.Text = "";
                                DDL_duracion_Nuevo.SelectedValue = "1";
                                RCP_add_modadlidad.SelectedColor = Color.White;
                                cargarAgenda(Convert.ToInt32(RCB_SItio.SelectedValue));
                                ShowMessage("Se agregó correctamente la modalidad. " + response.Mensaje, MessageType.Correcto, "alert_container");
                            }
                            else
                            {
                                ShowMessage("Existe un error, favor de verificar: " + response.Mensaje, MessageType.Advertencia, "alert_container");
                                Log.EscribeLog("Existe un error en btnAgregar_Click insert error:", 3, Usuario.vchUsuario);
                            }
                        }
                        else
                        {
                            ShowMessage("Existe un error, favor de verificar. ", MessageType.Error, "alert_container");
                            Log.EscribeLog("Existe un error en btnAgregar_Click datos null:", 3, Usuario.vchUsuario);
                        }
                    }
                }
                else
                {
                      ShowMessage("Verificar la información: ", MessageType.Advertencia, "alert_container");
                    Log.EscribeLog("Existe un error en btnAgregar_Click Modalidad null:", 3, Usuario.vchUsuario);
                }
            }
            catch (Exception eAddUser)
            {
                ShowMessage("Verificar la información: ", MessageType.Advertencia, "alert_container");
                Log.EscribeLog("Existe un error en btnAgregar_Click: " + eAddUser.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void RadButton1_Click(object sender, EventArgs e)
        {      
            string fecha_nueva = RDP_DiaFeriado.SelectedDate.ToString();

            fecha_nueva = fecha_nueva.Substring(0, 10);         

            bool bandera_duplicado = false;
            HD_DiaFeriado.Value = HD_DiaFeriado.Value.Replace("||", "|");

            string[] lista_dias_duplicado = HD_DiaFeriado.Value.Split('|');

            if (lista_dias_duplicado.Count() > 1)
            {
                foreach (var dia_duplicado in lista_dias_duplicado)
                {
                    if (fecha_nueva == dia_duplicado.ToString())
                    {
                        bandera_duplicado = true;
                    }
                }
            }

            if (bandera_duplicado == false)
            {
                string[] lista_dias = HD_DiaFeriado.Value.Split('|');

                if (lista_dias[0].ToString() == "")
                {
                    HD_DiaFeriado.Value = fecha_nueva;
                }

                else
                {
                    HD_DiaFeriado.Value = HD_DiaFeriado.Value + "|" + fecha_nueva;
                }
            }

                HD_DiaFeriado.Value = HD_DiaFeriado.Value.Replace("||", "|");

                string[] lista_dias_varios = HD_DiaFeriado.Value.Split('|');

            int contador_dias = lista_dias_varios.Count();
            int contador_agregar_dias = 0;

                if (lista_dias_varios.Count() > 1)
                {
                    foreach (var dia in lista_dias_varios)
                    {

                    if (dia.ToString() != "")
                    {
                        RadDock RD_Dia_Feriado = new RadDock();
                        RD_Dia_Feriado.ID = dia.ToString();
                        RD_Dia_Feriado.EnableDrag = false;
                        RD_Dia_Feriado.Collapsed = false;
                        RD_Dia_Feriado.Resizable = false;

                        RD_Dia_Feriado.DefaultCommands = Telerik.Web.UI.Dock.DefaultCommands.None;
                        RD_Dia_Feriado.DefaultCommands = Telerik.Web.UI.Dock.DefaultCommands.Close;

                        RD_Dia_Feriado.OnClientCommand = "cerrar_ventana";


                        RD_Dia_Feriado.Width = Unit.Pixel(140);

                        RD_Dia_Feriado.TitlebarContainer.BackColor = Color.AliceBlue;
                        RD_Dia_Feriado.BackColor = Color.AliceBlue;
                 
                        RD_Dia_Feriado.Title = dia.ToString();
                        RDZ_DiaFeriado.Controls.Add(RD_Dia_Feriado);

                        contador_agregar_dias++;


                        if (contador_agregar_dias == contador_dias)
                        {
                            try
                            {
                                if (bandera_duplicado == false)
                                {

                                    bool bandera_actualizar_DFeriado = false;
                                    ConfigScheduler_DiaFeriado request = new ConfigScheduler_DiaFeriado();

                                    request.mdlUser = Usuario;

                                    clsDiaFeriado conf_DiaFeriado = new clsDiaFeriado();

                                    conf_DiaFeriado.datDia = RDP_DiaFeriado.SelectedDate.Value;
                                    request.mdlDiaFeriado = conf_DiaFeriado;
                                    request.mdlDiaFeriado.intSitioID = Convert.ToInt32(RCB_Sitio_confiagenda.SelectedValue);
                                    bandera_actualizar_DFeriado = RisService.Set_DiaFeriado_Sitio(request);
                                }
                            }

                            catch (Exception eAddUser)
                            {
                                ShowMessage("Existe un error, favor de verificar. ", MessageType.Error, "alert_container");
                                Log.EscribeLog("Existe un error en InsertarDiaFeriado: " + eAddUser.Message, 3, Usuario.vchUsuario);
                            }
                        }
                    }                       
                    }
                ShowMessage("Se agregó correctamente el día feriado. " + fecha_nueva, MessageType.Correcto, "alert_container");
            }

                else
                {

                    try
                    {
                        bool bandera_actualizar_DFeriado = false;
                        ConfigScheduler_DiaFeriado request = new ConfigScheduler_DiaFeriado();

                        request.mdlUser = Usuario;

                        clsDiaFeriado conf_DiaFeriado = new clsDiaFeriado();

                        conf_DiaFeriado.datDia = RDP_DiaFeriado.SelectedDate.Value;                  
                        request.mdlDiaFeriado = conf_DiaFeriado;
                        request.mdlDiaFeriado.intSitioID = Convert.ToInt32(RCB_Sitio_confiagenda.SelectedValue);
                        bandera_actualizar_DFeriado = RisService.Set_DiaFeriado_Sitio(request);

                        RadDock RD_Dia_Feriado = new RadDock();
                        RD_Dia_Feriado.ID = fecha_nueva;
                        RD_Dia_Feriado.EnableDrag = false;
                        RD_Dia_Feriado.Resizable = false;
                        RD_Dia_Feriado.Collapsed = false;

                        RD_Dia_Feriado.DefaultCommands = Telerik.Web.UI.Dock.DefaultCommands.None;
                        RD_Dia_Feriado.DefaultCommands = Telerik.Web.UI.Dock.DefaultCommands.Close;

                        RD_Dia_Feriado.OnClientCommand = "cerrar_ventana";


                        RD_Dia_Feriado.Width = Unit.Pixel(140);


                        RD_Dia_Feriado.TitlebarContainer.BackColor = Color.AliceBlue;
                        RD_Dia_Feriado.BackColor = Color.AliceBlue;
         
                        RD_Dia_Feriado.Title = fecha_nueva;

                        RDZ_DiaFeriado.Controls.Add(RD_Dia_Feriado);
                        ShowMessage("Se agregó correctamente el día feriado. " + fecha_nueva, MessageType.Correcto, "alert_container");
                }

                    catch (Exception eAddUser)
                    {
                        ShowMessage("Existe un error, favor de verificar. ", MessageType.Error, "alert_container");
                        Log.EscribeLog("Existe un error en el primer DiaFeriado  al insertar: " + eAddUser.Message, 3, Usuario.vchUsuario);
                    }             
                }            
        }

        protected void RB_Agregar_HM_Click(object sender, EventArgs e)
        {

            try
            {


                if (HF_validacion_HM.Value == "false")
                {
                    string hora_inicio = RTP_HM_Inicio.SelectedTime.ToString();
                    string hora_fin = RTP_HM_Fin.SelectedTime.ToString();

                    string[] lista_horasmuesrtas = HD_HM.Value.Split('|');

                    if (lista_horasmuesrtas[0].ToString() == "")
                    {
                        HD_HM.Value = hora_inicio.Substring(0, 5) + "-" + hora_fin.Substring(0, 5);
                    }

                    else
                    {
                        HD_HM.Value = HD_HM.Value + "|" + hora_inicio.Substring(0, 5) + "-" + hora_fin.Substring(0, 5);
                    }

                    HD_HM.Value = HD_HM.Value.Replace("||", "|");
                    string[] lista_HM = HD_HM.Value.Split('|');


                    int contador_nuevaHM = 0;

                    if (lista_HM.Count() > 1)
                    {
                        foreach (var horas in lista_HM)
                        {
                            RadDock RD_HM = new RadDock();
                            RDL_HM.ID = horas.ToString();
                            RD_HM.EnableDrag = false;
                            RD_HM.Collapsed = false;
                            RD_HM.Resizable = false;

                            RD_HM.DefaultCommands = Telerik.Web.UI.Dock.DefaultCommands.None;
                            RD_HM.DefaultCommands = Telerik.Web.UI.Dock.DefaultCommands.Close;
                            RD_HM.OnClientCommand = "eliminar_HM";

                            RD_HM.Width = Unit.Pixel(150);

                            RD_HM.TitlebarContainer.BackColor = Color.SteelBlue;
                            RD_HM.BorderColor = Color.SteelBlue;

                            RD_HM.Title = horas.ToString();
                            RDZ_HM.Controls.Add(RD_HM);

                            contador_nuevaHM++;

                            if (lista_HM.Count() == contador_nuevaHM)
                            {
                                bool bandera_actualizar_DFeriado = false;
                                ConfigScheduler_HoraMuertaRequest request = new ConfigScheduler_HoraMuertaRequest();

                                request.mdlUser = Usuario;

                                clsHoraMuerta conf_HM = new clsHoraMuerta();

                                conf_HM.tmeInicio = RTP_HM_Inicio.SelectedTime.ToString();
                                conf_HM.tmeFin = RTP_HM_Fin.SelectedTime.ToString();

                                request.mdlHMScheduler = conf_HM;
                                request.mdlHMScheduler.intSitioID = Convert.ToInt32(RCB_Sitio_confiagenda.SelectedValue);

                                bandera_actualizar_DFeriado = RisService.Set_HoraMuerta_Sitio(request);
                            }
                        }
                    }

                    else
                    {
                        RadDock RD_HM = new RadDock();
                        RDL_HM.ID = hora_inicio.Substring(0, 5) + "-" + hora_fin.Substring(0, 5);
                        RD_HM.EnableDrag = false;
                        RD_HM.Resizable = false;
                        RD_HM.Collapsed = false;

                        RD_HM.DefaultCommands = Telerik.Web.UI.Dock.DefaultCommands.None;
                        RD_HM.DefaultCommands = Telerik.Web.UI.Dock.DefaultCommands.Close;
                        RD_HM.OnClientCommand = "eliminar_HM";

                        RD_HM.Width = Unit.Pixel(150);

                        RD_HM.TitlebarContainer.BackColor = Color.SteelBlue;
                        RD_HM.BorderColor = Color.SteelBlue;

                        RD_HM.Title = hora_inicio.Substring(0, 5) + "-" + hora_fin.Substring(0, 5);

                        RDZ_HM.Controls.Add(RD_HM);

                        bool bandera_actualizar_DFeriado = false;
                        ConfigScheduler_HoraMuertaRequest request = new ConfigScheduler_HoraMuertaRequest();

                        request.mdlUser = Usuario;

                        clsHoraMuerta conf_HM = new clsHoraMuerta();

                        conf_HM.tmeInicio = RTP_HM_Inicio.SelectedTime.ToString();
                        conf_HM.tmeFin = RTP_HM_Fin.SelectedTime.ToString();

                        request.mdlHMScheduler = conf_HM;
                        request.mdlHMScheduler.intSitioID = Convert.ToInt32(RCB_Sitio_confiagenda.SelectedValue);

                        bandera_actualizar_DFeriado = RisService.Set_HoraMuerta_Sitio(request);

                    }
                }
                else
                {
                    HD_HM.Value = HD_HM.Value.Replace("||", "|");

                    string[] lista_HM = HD_HM.Value.Split('|');

                    if (lista_HM.Count() > 1)
                    {
                        foreach (var horas in lista_HM)
                        {
                            if (horas != "")
                            {
                                RadDock RD_HM = new RadDock();
                                RDL_HM.ID = horas.ToString();
                                RD_HM.EnableDrag = false;
                                RD_HM.Collapsed = false;
                                RD_HM.Resizable = false;

                                RD_HM.DefaultCommands = Telerik.Web.UI.Dock.DefaultCommands.None;
                                RD_HM.DefaultCommands = Telerik.Web.UI.Dock.DefaultCommands.Close;

                                RD_HM.Width = Unit.Pixel(150);

                                RD_HM.TitlebarContainer.BackColor = Color.SteelBlue;
                                RD_HM.BorderColor = Color.SteelBlue;
                                RD_HM.Title = horas.ToString();
                                RDZ_HM.Controls.Add(RD_HM);
                            }
                        }
                    }
                }

                HF_validacion_HM.Value = "True";
                ShowMessage("Se agregó correctamente la hora muerta. ", MessageType.Correcto, "alert_container");
            }

            catch (Exception ex)
            {
                ShowMessage("Existe un error, favor de verificar. ", MessageType.Error, "alert_container");
                Log.EscribeLog("Existe un error en la hora muerta: " + ex.Message, 3, Usuario.vchUsuario);
            }
        }

        public void carga_dias_feriados(int idsitio)
        {

            try
            {
                List<clsDiaFeriado> lstTec = new List<clsDiaFeriado>();
                ConfigScheduler_DiaFeriado request = new ConfigScheduler_DiaFeriado();
                request.mdlUser = Usuario;
                request.mdlDiaFeriado.intSitioID = idsitio;
                lstTec = RisService.getDiaFeriadoConfScheduler_Sitio(request);
                if (lstTec != null)
                {
                    HD_DiaFeriado.Value = "";

                    foreach (var dia_ in lstTec)
                    {
                        RadDock RD_Dia_Feriado = new RadDock();
                        RD_Dia_Feriado.ID = "DF" + dia_.intDiaFeriadoID.ToString();
                        RD_Dia_Feriado.EnableDrag = false;
                        RD_Dia_Feriado.Collapsed = false;
                        RD_Dia_Feriado.Resizable = false;


                        RD_Dia_Feriado.AutoPostBack = true;

                        RD_Dia_Feriado.DefaultCommands = Telerik.Web.UI.Dock.DefaultCommands.None;
                        RD_Dia_Feriado.DefaultCommands = Telerik.Web.UI.Dock.DefaultCommands.Close;
                        RD_Dia_Feriado.OnClientCommand = "cerrar_ventana";

                        RD_Dia_Feriado.Width = Unit.Pixel(140);

                        RD_Dia_Feriado.TitlebarContainer.BackColor = Color.AliceBlue;
                        RD_Dia_Feriado.BackColor = Color.AliceBlue;

                        RD_Dia_Feriado.Title = dia_.datDia.ToShortDateString();
                        RDZ_DiaFeriado.Controls.Add(RD_Dia_Feriado);

                        if (HD_DiaFeriado.Value == "")
                        {
                            HD_DiaFeriado.Value = dia_.datDia.ToShortDateString() + "|";
                        }

                        else
                        {
                            HD_DiaFeriado.Value = HD_DiaFeriado.Value + dia_.datDia.ToShortDateString() + "|";
                        }

                    }
                }
            }

            catch (Exception ecU)
            {
                Log.EscribeLog("Existe un error en cargarDiaFeriadoConfScheduler: " + ecU.Message, 3, Usuario.vchUsuario);
            }

        }

        public void carga_dias_feriados()
        {

            try
            {           
                List<clsDiaFeriado> lstTec = new List<clsDiaFeriado>();
                ConfigScheduler_DiaFeriado request = new ConfigScheduler_DiaFeriado();
                request.mdlUser = Usuario;
                lstTec = RisService.getDiaFeriadoConfScheduler(request);
                if (lstTec != null)
                {
                    HD_DiaFeriado.Value = "";
                   
                        foreach (var dia_ in lstTec)
                        {
                            RadDock RD_Dia_Feriado = new RadDock();
                        RD_Dia_Feriado.ID = "DF" + dia_.intDiaFeriadoID.ToString();
                            RD_Dia_Feriado.EnableDrag = false;
                            RD_Dia_Feriado.Collapsed = false;
                            RD_Dia_Feriado.Resizable = false;
                       

                        RD_Dia_Feriado.AutoPostBack = true;

                        RD_Dia_Feriado.DefaultCommands = Telerik.Web.UI.Dock.DefaultCommands.None;
                        RD_Dia_Feriado.DefaultCommands = Telerik.Web.UI.Dock.DefaultCommands.Close;                 
                        RD_Dia_Feriado.OnClientCommand = "cerrar_ventana";

                        RD_Dia_Feriado.Width = Unit.Pixel(140);

                            RD_Dia_Feriado.TitlebarContainer.BackColor = Color.AliceBlue;
                            RD_Dia_Feriado.BackColor = Color.AliceBlue;                   

                        RD_Dia_Feriado.Title = dia_.datDia.ToShortDateString();
                        RDZ_DiaFeriado.Controls.Add(RD_Dia_Feriado);

                            if (HD_DiaFeriado.Value == "")
                            {
                                HD_DiaFeriado.Value = dia_.datDia.ToShortDateString() + "|";
                            }

                            else
                            {
                                HD_DiaFeriado.Value =  HD_DiaFeriado.Value + dia_.datDia.ToShortDateString() + "|";
                            }

                        }
                }                   
            }
                       
            catch (Exception ecU)
            {
                Log.EscribeLog("Existe un error en cargarDiaFeriadoConfScheduler: " + ecU.Message, 3, Usuario.vchUsuario);
            }
        
        }

        public void carga_Horas_muertas(int idsitio)
        {
            try
            {
                List<clsHoraMuerta> lstTec = new List<clsHoraMuerta>();
                ConfigScheduler_HoraMuertaRequest request = new ConfigScheduler_HoraMuertaRequest();
                request.mdlUser = Usuario;
                request.mdlHMScheduler.intSitioID = idsitio;
                lstTec = RisService.getHoraMuertaConfScheduler_Sitio(request);
                if (lstTec != null)
                {
                    HD_HM.Value = "";

                    foreach (var HorasMuertas in lstTec)
                    {
                        RadDock RD_HM = new RadDock();
                        RD_HM.ID = "HM" + HorasMuertas.intHorasMuertasID.ToString();
                        RD_HM.EnableDrag = false;
                        RD_HM.Collapsed = false;
                        RD_HM.Resizable = false;

                        RD_HM.DefaultCommands = Telerik.Web.UI.Dock.DefaultCommands.None;
                        RD_HM.DefaultCommands = Telerik.Web.UI.Dock.DefaultCommands.Close;
                        RD_HM.OnClientCommand = "eliminar_HM";

                        RD_HM.Width = Unit.Pixel(150);

                        RD_HM.TitlebarContainer.BackColor = Color.SteelBlue;
                        RD_HM.BorderColor = Color.SteelBlue;


                        string HI = HorasMuertas.tmeInicio.ToString();
                        string HF = HorasMuertas.tmeFin.ToString();

                        string HM_limpias = HI.Substring(0, 5) + "-" + HF.Substring(0, 5);

                        RD_HM.Title = HM_limpias;
                        RDZ_HM.Controls.Add(RD_HM);

                        if (HD_HM.Value == "")
                        {
                            HD_HM.Value = HM_limpias + "|";
                        }

                        else
                        {
                            HD_HM.Value = HD_HM.Value + HM_limpias + "|";
                        }
                    }
                }
            }

            catch (Exception ecU)
            {
                Log.EscribeLog("Existe un error en cargarHoraMuertaConfScheduler: " + ecU.Message, 3, Usuario.vchUsuario);
            }

        }

        public void carga_Horas_muertas()
        {
            try
            {
                List<clsHoraMuerta> lstTec = new List<clsHoraMuerta>();
                ConfigScheduler_HoraMuertaRequest request = new ConfigScheduler_HoraMuertaRequest();
                request.mdlUser = Usuario;
                lstTec = RisService.getHoraMuertaConfScheduler(request);
                if (lstTec != null)
                {
                    HD_HM.Value = "";

                    foreach (var HorasMuertas in lstTec)
                    {
                        RadDock RD_HM = new RadDock();
                        RD_HM.ID ="HM"  + HorasMuertas.intHorasMuertasID.ToString();
                        RD_HM.EnableDrag = false;
                        RD_HM.Collapsed = false;
                        RD_HM.Resizable = false;

                        RD_HM.DefaultCommands = Telerik.Web.UI.Dock.DefaultCommands.None;
                        RD_HM.DefaultCommands = Telerik.Web.UI.Dock.DefaultCommands.Close;
                        RD_HM.OnClientCommand = "eliminar_HM";

                        RD_HM.Width = Unit.Pixel(150);

                        RD_HM.TitlebarContainer.BackColor = Color.SteelBlue;
                        RD_HM.BorderColor = Color.SteelBlue;


                        string HI = HorasMuertas.tmeInicio.ToString();
                        string HF = HorasMuertas.tmeFin.ToString();

                        string HM_limpias = HI.Substring(0, 5) + "-" + HF.Substring(0, 5);

                        RD_HM.Title = HM_limpias;
                        RDZ_HM.Controls.Add(RD_HM);

                        if (HD_HM.Value == "")
                        {
                            HD_HM.Value = HM_limpias + "|";
                        }

                        else
                        {
                            HD_HM.Value = HD_HM.Value + HM_limpias + "|";
                        }
                    }
                }
            }

            catch (Exception ecU)
            {
                Log.EscribeLog("Existe un error en cargarHoraMuertaConfScheduler: " + ecU.Message, 3, Usuario.vchUsuario);
            }

        }

        protected void RB_dia_CheckedChanged(object sender, EventArgs e)
        {
            string boton_apreatdo = ((Telerik.Web.UI.RadButton)sender).Text;


            bool bandera_Lunes = false;

            try
            {
                ConfigScheduler_DiaSemanaRequest request = new ConfigScheduler_DiaSemanaRequest();

                request.mdlUser = Usuario;
                clsDiaSemana dia_Semana = new clsDiaSemana();

                switch (boton_apreatdo)
                {
                    case "Lunes":
                        dia_Semana.intSemanaID = 1;
                        dia_Semana.bitActivo = RB_Lunes.Checked;
                        break;
                    case "Martes":
                        dia_Semana.intSemanaID = 2;
                        dia_Semana.bitActivo = RBMartes.Checked;
                        break;
                    case "Miercoles":
                        dia_Semana.intSemanaID = 3;
                        dia_Semana.bitActivo = RBMiercoles.Checked;
                        break;
                    case "Jueves":
                        dia_Semana.intSemanaID = 4;
                        dia_Semana.bitActivo = RBJueves.Checked;
                        break;
                    case "Viernes":
                        dia_Semana.intSemanaID = 5;
                        dia_Semana.bitActivo = RBViernes.Checked;
                        break;
                    case "Sábado":
                        dia_Semana.intSemanaID = 6;
                        dia_Semana.bitActivo = RBSabado.Checked;
                        break;
                    case "Domingo":
                        dia_Semana.intSemanaID = 7;
                        dia_Semana.bitActivo = RB_Domingo.Checked;
                        break;
                }

                request.mdlDiaSemana = dia_Semana;
                request.mdlDiaSemana.intSitioID = Convert.ToInt32(RCB_Sitio_confiagenda.SelectedValue);

                bandera_Lunes = RisService.UpdateDiaSemana_Sitio(request);
                ShowMessage("Se modificó correctamente el día de la semana. ", MessageType.Correcto, "alert_container");
            }
            catch (Exception eAddUser)
            {
                ShowMessage("Existe un error al agregar el día de la semana, favor de verificar. ", MessageType.Error, "alert_container");
                Log.EscribeLog("Existe un error en la actualizacion de los dias de la semana laborables: " + eAddUser.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void RTP_HM_Inicio_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            try
            {
                string Horainicio = RTP_HM_Inicio.SelectedTime.ToString();
                int limpia_hora_inicio = Convert.ToInt32(Horainicio.Substring(0, 2)) + 1;
                int arreglo_total = 24 - limpia_hora_inicio;
                DateTime[] dtArray = new DateTime[arreglo_total];

                int Contador = 0;

                for (int hour = limpia_hora_inicio; hour < 24; hour++)
                {
                    dtArray[Contador] = DateTime.UtcNow.Date.AddHours(hour);
                    Contador++;
                }

                RTP_HM_Fin.TimeView.CustomTimeValues = dtArray;
            }
            catch(Exception eAddUser)
            {
                Log.EscribeLog("Existe un error en carga segunda  hora muerta: " + eAddUser.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void RTP_Inicio_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            try
            {
                string Horainicio = RTP_Inicio.SelectedTime.ToString();
                int limpia_hora_inicio = Convert.ToInt32(Horainicio.Substring(0, 2)) + 1;
                int arreglo_total = 24 - limpia_hora_inicio;
                DateTime[] dtArray = new DateTime[arreglo_total];

                int Contador = 0;

                for (int hour = limpia_hora_inicio; hour < 24; hour++)
                {
                    dtArray[Contador] = DateTime.UtcNow.Date.AddHours(hour);
                    Contador++;
                }

                RTP_Fin.TimeView.CustomTimeValues = dtArray;
            }

            catch(Exception eAddUser)
            {
                Log.EscribeLog("Existe un error en carga horas segundo horario : " + eAddUser.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void BTHorarioTotal_Click(object sender, EventArgs e)
        {

            try
            {
                bool bandera_actualizar_HRT = false;

                ConfigGeneralAgenda request = new ConfigGeneralAgenda();

                request.mdlUser = Usuario;


                clsGeneralConfigAgenda config_agenda = new clsGeneralConfigAgenda();

                config_agenda.tmeInicioDia = RTP_Inicio.SelectedTime.Value;
                config_agenda.tmeFinDia = RTP_Fin.SelectedTime.Value;

                request.mdlgenconfagenda = config_agenda;
                request.mdlgenconfagenda.intSitioID = Convert.ToInt32(RCB_Sitio_confiagenda.SelectedValue);

                bandera_actualizar_HRT = RisService.UpdateHR_Activo(request);
                ShowMessage("Se agrego el horario de atención. ", MessageType.Correcto, "alert_container");
            }

            catch (Exception eAddUser)
            {
                ShowMessage("Existe un error cuando se establece el horario de atención. ", MessageType.Error, "alert_container");
                Log.EscribeLog("Existe un error en BTHorarioTotal_Click: " + eAddUser.Message, 3, Usuario.vchUsuario);
            }
        }
      
        protected void RadAjaxPanel2_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            bool bandera_eliminado_DF = false;
            string[] lista_parametros_dias_feriados = e.Argument.Split('|');
            string fecha_eliminada = lista_parametros_dias_feriados[1].ToString();

            try
            {
                ConfigScheduler_DiaFeriado request = new ConfigScheduler_DiaFeriado();

                request.mdlUser = Usuario;
                clsDiaFeriado dia_feriado = new clsDiaFeriado();

                dia_feriado.datDia = Convert.ToDateTime(fecha_eliminada);
                dia_feriado.bitActivo = false;
                request.mdlDiaFeriado = dia_feriado;

                bandera_eliminado_DF = RisService.Eliminar_DiaFeriado(request);

                string[] lista_diasferiados = HD_DiaFeriado.Value.Split('|');
                HD_DiaFeriado.Value = "";
                int contador_DF = 0;

                foreach (var DF in lista_diasferiados)
                {
                    if (DF != fecha_eliminada)
                    {
                        if (contador_DF == 0)
                        {
                            HD_DiaFeriado.Value = DF + "|";
                            contador_DF++;
                        }

                        else
                        {
                            HD_DiaFeriado.Value = HD_DiaFeriado.Value + DF + "|";
                            contador_DF++;
                        }                        
                    }                   
                }

                carga_dias_feriados();
                ShowMessage("Se eliminó el día feriado. ", MessageType.Correcto, "alert_container");
            }

            catch (Exception eAddUser)
            {
                ShowMessage("Existe un error cuando se elimina el día feriado, favor de verificar. ", MessageType.Error, "alert_container");
                Log.EscribeLog("Existe un error al eliminar un dia feriado: " + eAddUser.Message, 3, Usuario.vchUsuario);
            }

        }

        protected void RadAjaxPanel3_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            bool bandera_eliminado_HM = false;
            string[] lista_parametros_horas_muertas = e.Argument.Split('|');
            string HM_eliminada = lista_parametros_horas_muertas[1].ToString();

            string [] lista_Horas_separadas = HM_eliminada.Split('-');

            string HM1 = lista_Horas_separadas[0];
            string HM2 = lista_Horas_separadas[1];

            try
            {
                ConfigScheduler_HoraMuertaRequest request = new ConfigScheduler_HoraMuertaRequest();

                request.mdlUser = Usuario;
                clsHoraMuerta Hora_muerta = new clsHoraMuerta();              

                Hora_muerta.tmeInicio = HM1;
                Hora_muerta.tmeFin = HM2;

                request.mdlHMScheduler = Hora_muerta;
                
                bandera_eliminado_HM = RisService.Eliminar_Hora_Muerta(request);              

                string[] lista_horasmuertas = HD_HM.Value.Split('|');
                HD_HM.Value = "";
                int contador_HM = 0;

                foreach (var Horas_M in lista_horasmuertas)
                {
                    if (Horas_M != HM_eliminada)
                    {
                        if (contador_HM == 0)
                        {
                            HD_DiaFeriado.Value = Horas_M + "|";
                            contador_HM++;
                        }

                        else
                        {
                            HD_DiaFeriado.Value = HD_DiaFeriado.Value + Horas_M + "|";
                            contador_HM++;
                        }
                    }
                }

                carga_Horas_muertas();
                ShowMessage("Se eliminó la hora muerta. ", MessageType.Correcto, "alert_container");
            }

            catch (Exception eAddUser)
            {
                ShowMessage("Existe un error cuando se elimina la hora muerta, favor de verificar. ", MessageType.Error, "alert_container");
                Log.EscribeLog("Existe un error al eliminar un dia feriado: " + eAddUser.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void RB_intervalo_Click(object sender, EventArgs e)
        {
            try
            {
                bool bandera_actualizar_HRT = false;

                ConfigGeneralAgenda request = new ConfigGeneralAgenda();

                request.mdlUser = Usuario;


                clsGeneralConfigAgenda config_agenda = new clsGeneralConfigAgenda();

                config_agenda.intIntervalo =  Convert.ToInt32(TB_intervalo.Text);
      
                request.mdlgenconfagenda = config_agenda;

                bandera_actualizar_HRT = RisService.Update_Intervalo(request);
            }

            catch (Exception eAddUser)
            {
                Log.EscribeLog("Existe un error en BTHorarioTotal_Click: " + eAddUser.Message, 3, Usuario.vchUsuario);
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

        protected void RadAjaxPanel4_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            GV_Agenda.DataSource = null;
            GV_Agenda.DataBind();
            cargarAgenda(Convert.ToInt32(e.Argument));                      
        }

        protected void RadAjaxPanel5_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {

            int x = 0;
            //cargarAgenda(Convert.ToInt32(e.Argument));
            //cargaConfigScheduler(Convert.ToInt32(e.Argument));
            //carga_dias_feriados(Convert.ToInt32(e.Argument));
            //carga_Horas_muertas(Convert.ToInt32(e.Argument));
            //HF_validacion_HM.Value = "false";
        }

        protected void RCB_Sitio_confiagenda_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //cargarAgenda(Convert.ToInt32(e.Value));
            cargaConfigScheduler(Convert.ToInt32(e.Value));
            carga_dias_feriados(Convert.ToInt32(e.Value));
            carga_Horas_muertas(Convert.ToInt32(e.Value));
            HF_validacion_HM.Value = "false";
        }

    
    }
}