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
                    if (Session["User"] != null)
                    {
                        Usuario = (clsUsuario)Session["User"];
                        if (Usuario != null)
                        {
                            cargarAgenda();
                            cargaConfigScheduler();
                            carga_dias_feriados();
                            carga_Horas_muertas();
                            HF_validacion_HM.Value = "false";
                            cargarChecks();
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
                Log.EscribeLog("Existe un error en Page_Load de frmConfigAgenda: " + ePL.Message, 3, "");
            }
        }

        private void cargarChecks()
        {
            try
            {
                //string htmAdd = "<label class='btn btn-success '>" +
                //                    "<input type='checkbox' autocomplete='off' > L" +
                //                    "<span class='glyphicon glyphicon-ok'></span>" +
                //                "</label>" +
                //                "<label class='btn btn-primary '>" +
                //                    "<input type='checkbox' autocomplete='off' > M" +
                //                    "<span class='glyphicon glyphicon-ok'></span>" +
                //                "</label>" +
                //                "<label class='btn btn-info '>" +
                //                    " <input type='checkbox' autocomplete='off' > Mi" +
                //                    "  <span class='glyphicon glyphicon-ok'></span>" +
                //                "</label>" +
                //                "<label class='btn btn-default '>" +
                //                    "<input type='checkbox' autocomplete='off' > J" +
                //                    "<span class='glyphicon glyphicon-ok'></span>" +
                //                "</label>" +
                //                "<label class='btn btn-warning '>" +
                //                    "<input type='checkbox' autocomplete='off' > V" +
                //                    "<span class='glyphicon glyphicon-ok'></span>" +
                //                "</label>" +
                //                "<label class='btn btn-danger '>" +
                //                    "<input type='checkbox' autocomplete='off' > S" +
                //                    "<span class='glyphicon glyphicon-ok'></span>" +
                //                "</label>" +
                //                "<label class='btn btn-success '>" +
                //                    "<input type='checkbox' autocomplete='off' > D" +
                //                    "<span class='glyphicon glyphicon-ok'></span>" +
                //                "</label>";

                //divCheks.Controls.Add(new System.Web.UI.LiteralControl(htmAdd));


            }
            catch (Exception ecC)
            {
                Log.EscribeLog("Existe un error en cargarChecks: " + ecC.Message, 3, Usuario.vchNombre);
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

        private void cargaConfigScheduler()
        {
            try
            {
                List<clsConfScheduler> lstTec = new List<clsConfScheduler>();
                ConfigSchedulerRequest request = new ConfigSchedulerRequest();
                request.mdlUser = Usuario;
                lstTec = RisService.getConfScheduler(request);

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
                lstTec = RisService.getDiaSemanaConfScheduler(request);

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

            try
            {
                List<clsDiaFeriado> lstTec = new List<clsDiaFeriado>();
                ConfigScheduler_DiaFeriado request = new ConfigScheduler_DiaFeriado();
                request.mdlUser = Usuario;
                lstTec = RisService.getDiaFeriadoConfScheduler(request);

                foreach (var item in lstTec)
                {
                    //RTP_HM_Inicio.SelectedTime = item.tmeInicio;
                    //RTP_HM_Fin.SelectedTime = item.tmeFin;
                }
            }

            catch (Exception ex)
            {
                Log.EscribeLog("Existe un error en ConfigScheduler_DiaFeriado: " + ex.Message, 3, Usuario.vchNombre);
            }

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
            cargarAgenda();
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
                                    //ShowMessage("Se actualizo correctamente la variable", MessageType.Correcto, "alert_container");
                                    //grvAddPaciente.EditIndex = -1;
                                    cargarAgenda();
                                }
                                else
                                {
                                    //ShowMessage("Existe un error: " + response.Success, MessageType.Error, "alert_container");
                                }
                            }
                            else
                            {
                                //ShowMessage("Favor de revisar la información.", MessageType.Error, "alert_container");
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

        //protected void GV_Agenda_RowUpdated(object sender, System.Web.UI.WebControls.GridViewUpdatedEventArgs e)
        //{
        //}

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

                Control colorRCP = (Control)GV_Agenda.Rows[e.RowIndex].FindControl("ucRadColorPicker1");
                string color_seleccionado = ((Fuji.RISLite.Site.WebUserControl2)colorRCP).DBSelectedColor;



                agenda.intModalidadID = Convert.ToInt32(idmodalidad.Text);
                agenda.vchCodigo = codigo.Text;
                agenda.vchModalidad = modalidad.Text;
                agenda.vchColor = color_seleccionado;


                //System.Drawing.ColorTranslator.ToHtml(RadColorPicker1.SelectedColor);
                request.mdlagenda = agenda;

                bandera_update_agenda = RisService.UpdateAgenda(request);

                GV_Agenda.EditIndex = -1;
                cargarAgenda();

            }
            catch
            {

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
                modalidad.vchUserAdmin = Usuario.vchUsuario.ToUpper();

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
                                //ShowMessage("Se agregó correctamente. " + response.Mensaje, MessageType.Correcto, "alert_container");
                                txtDescripcion.Text = "";
                                txt_Modalidad.Text = "";
                                cargarAgenda();
                            }
                            else
                            {
                                //ShowMessage("Existe un error, favor de verificar: " + response.Mensaje, MessageType.Advertencia, "alert_container");
                            }
                        }
                        else
                        {
                            //  ShowMessage("Existe un error, favor de verificar. ", MessageType.Error, "alert_container");
                        }
                    }
                }
                else
                {
                    //  ShowMessage("Verificar la información: ", MessageType.Advertencia, "alert_container");
                }
            }
            catch (Exception eAddUser)
            {
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
                                    bandera_actualizar_DFeriado = RisService.Set_DiaFeriado(request);
                                }
                            }

                            catch (Exception eAddUser)
                            {
                                Log.EscribeLog("Existe un error en InsertarDiaFeriado: " + eAddUser.Message, 3, Usuario.vchUsuario);
                            }
                        }
                    }                       
                    }
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
                    bandera_actualizar_DFeriado = RisService.Set_DiaFeriado(request);

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
                }

                catch (Exception eAddUser)
                {
                    Log.EscribeLog("Existe un error en el primer DiaFeriado  al insertar: " + eAddUser.Message, 3, Usuario.vchUsuario);
                }             
                }            
        }

        protected void RB_Agregar_HM_Click(object sender, EventArgs e)
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

                            bandera_actualizar_DFeriado = RisService.Set_HoraMuerta(request);
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

                    bandera_actualizar_DFeriado = RisService.Set_HoraMuerta(request);

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
                        dia_Semana.intSemanaID = 3;
                        dia_Semana.bitActivo = RBMartes.Checked;
                        break;
                    case "Miercoles":
                        dia_Semana.intSemanaID = 4;
                        dia_Semana.bitActivo = RBMiercoles.Checked;
                        break;
                    case "Jueves":
                        dia_Semana.intSemanaID = 5;
                        dia_Semana.bitActivo = RBJueves.Checked;
                        break;
                    case "Viernes":
                        dia_Semana.intSemanaID = 6;
                        dia_Semana.bitActivo = RBViernes.Checked;
                        break;
                    case "Sabado":
                        dia_Semana.intSemanaID = 7;
                        dia_Semana.bitActivo = RBSabado.Checked;
                        break;
                    case "Domingo":
                        dia_Semana.intSemanaID = 8;
                        dia_Semana.bitActivo = RB_Domingo.Checked;
                        break;
                }

                request.mdlDiaSemana = dia_Semana;

                bandera_Lunes = RisService.UpdateDiaSemana(request);
            }
            catch (Exception eAddUser)
            {
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

                bandera_actualizar_HRT = RisService.UpdateHR_Activo(request);
            }

            catch (Exception eAddUser)
            {
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
            }

            catch (Exception eAddUser)
            {
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
            }

            catch (Exception eAddUser)
            {
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



        //public enum MessageType { Correcto, Error, Info, Advertencia };

        //protected void ShowMessage(string Message, MessageType type, String container)
        //{
        //    try
        //    {
        //        Message = Message.Replace("'", " ");
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('" + Message + "','" + type + "','" + container + "');", true);
        //    }
        //    catch (Exception eSM)
        //    {
        //        throw eSM;
        //    }
        //}
    }
}