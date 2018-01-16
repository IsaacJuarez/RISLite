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
using Telerik.Web.UI;

namespace Fuji.RISLite.Site
{
    public partial class frmListaTrabajo : System.Web.UI.Page
    {
        public static string user = "";

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
                //Validar Token
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
                                string vista = "frmListaTrabajo.aspx";
                                if (lstVista.Any(x => x.vchVistaIdentificador == vista))
                                {
                                    Usuario = (clsUsuario)Session["UserRISAxon"];
                                    if (Usuario != null)
                                    {

                                        cargarlistadetrabajo(Usuario.intSitioID);
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
                Log.EscribeLog("Existe un error en Page_Load de frmListaTrabajo: " + ePL.Message, 3, "");
            }
        }



        private void cargarlistadetrabajo(int idsitio)
        {
            try
            {
                GV_ListaTrabajo.DataSource = null;
                List<clsListaDeTrabajo> lstTec = new List<clsListaDeTrabajo>();
                List<stp_getRELModalidadTecnico_Result> lstModTec = new List<stp_getRELModalidadTecnico_Result>();
                ModTecnicoRequest requestT = new ModTecnicoRequest();
                requestT.intUsuarioID = Usuario.intUsuarioID;
                requestT.mdlUser = Usuario;
                lstModTec = RisService.getModalidadTecnicoList(requestT);
                AgendaRequest request = new AgendaRequest();
                request.mdlUser = Usuario;
                request.mdlagenda.intSitioID = idsitio;
                lstTec = RisService.getListaDeTrabajo(request);
                GV_ListaTrabajo.DataSource = null;
                if (lstTec != null)
                {
                    if (lstTec.Count > 0)
                    {
                        lstTec = lstTec.Where(x => x.datFechaInicio.Day == DateTime.Today.Day && x.datFechaInicio.Month == DateTime.Today.Month && x.datFechaInicio.Year == DateTime.Today.Year && x.intEstatusID != 1).OrderBy(x=> x.datFecha).ToList();
                        lstTec = lstTec.Where(x => lstModTec.Any(p2 => p2.intModalidadID == x.intModalidadID)).ToList();
                        GV_ListaTrabajo.DataSource = lstTec;
                    }
                }
                GV_ListaTrabajo.DataBind();
            }
            catch (Exception ecU)
            {
                Log.EscribeLog("Existe un error en cargaLista de trabajo: " + ecU.Message, 3, Usuario.vchUsuario);
            }
        }


        protected void GV_ListaTrabajo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType != DataControlRowType.DataRow)
                {
                    return;
                }

                if (e.Row.DataItem != null)
                {
                    clsListaDeTrabajo item = (clsListaDeTrabajo)e.Row.DataItem;
                    LinkButton btn = (LinkButton)e.Row.FindControl("btn1");
                    LinkButton btn2 = (LinkButton)e.Row.FindControl("btn2");
                    LinkButton btn3 = (LinkButton)e.Row.FindControl("btn3");
                    LinkButton btn_Adicional = (LinkButton)e.Row.FindControl("btn_Adicional");

                    AgendaRequest request = new AgendaRequest();
                    request.mdlUser = Usuario;
                    int idcita = item.intCitaID;
                    bool bandera_detalleCIta = RisService.getListaDetalleCita(request, idcita);


                    if (bandera_detalleCIta == false)
                    {
                        btn_Adicional.Visible = false;
                    }

                    if (btn != null)
                    {
                        if(item.intEstatusID == 2)
                        {
                            btn.Visible = true;
                            btn2.Visible = false;
                            btn3.Visible = false;
                        }
                        else
                        {
                            btn.Visible = false;
                            if(item.intEstatusID == 3)
                            {
                                btn2.Visible = true;
                                btn3.Visible = true;
                            }
                            else
                            {
                                btn2.Visible = false;
                                btn3.Visible = false;
                            }
                        }
                    }
                }
            }
            catch (Exception eDB)
            {
                Log.EscribeLog("Existe un error en GV_ListaTrabajo_RowDataBound: " + eDB.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void GV_ListaTrabajo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                if (e.NewPageIndex >= 0)
                {
                    this.GV_ListaTrabajo.PageIndex = e.NewPageIndex;
                    cargarlistadetrabajo(Usuario.intSitioID);
                }
            }
            catch (Exception ex)
            {
                Log.EscribeLog("Existe un error grvVista_PageIndexChanging: " + ex.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void GV_ListaTrabajo_RowCommand1(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(e.CommandArgument);
                bool bandera_Actualizar = false;

                switch (e.CommandName)
                {
                    case "Tomar":
                        EstatusCita request = new EstatusCita();
                        request.mdlUser = Usuario;
                        bandera_Actualizar = RisService.UpdateEstatus_Cita(request, 3, index);

                        cargarlistadetrabajo(Usuario.intSitioID);

                        int rowIndex = int.Parse(e.CommandArgument.ToString());
                        break;
                    case "Finalizar":
                        EstatusCita request2 = new EstatusCita();
                        request2.mdlUser = Usuario;
                        bandera_Actualizar = RisService.UpdateEstatus_Cita(request2, 4, index);

                        cargarlistadetrabajo(Usuario.intSitioID);

                        int rowIndex2 = int.Parse(e.CommandArgument.ToString());

                        break;
                    case "Cancelar":
                        EstatusCita request3 = new EstatusCita();
                        request3.mdlUser = Usuario;
                        bandera_Actualizar = RisService.UpdateEstatus_Cita(request3, 2, index);

                        cargarlistadetrabajo(Usuario.intSitioID);

                        int rowIndex3 = int.Parse(e.CommandArgument.ToString());
                        break;
                    case "Adicional":
                        cargarAdicionalesCita(index);
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "mdlCita", "$('#mdlCita').modal();", true);
                        break;
                }
            }
            catch (Exception eRU)
            {
                Log.EscribeLog("Existe un error GV_AGENDA: " + eRU.Message, 3, Usuario.vchUsuario);
            }
        }

        private void cargarAdicionalesCita(int intCitaID)
        {
            try
            {
               
                CitaNuevaRequest request = new CitaNuevaRequest();
                List<stp_getDetalleCita_Result> response = new List<stp_getDetalleCita_Result>();
                request.mdlUser = Usuario;
                request.intCitaID = intCitaID;
                //response = RisService.getCitaAdicionales(request);
                response = RisService.get_stpDetalleCita(request);

                if (response != null)
                {
                   
                    if (response[0].vchValor == "1")
                    {
                        Label LB_1 = new Label();
                        LB_1.Text = response[0].vchNombre;
                        pnlDinamicoContenido.Controls.Add(LB_1);
                    }

                    if (response[0].bitObservaciones == true)
                    {
                        Label LB_2 = new Label();
                        LB_2.Text = "           " + response[0].vchObservaciones;
                        pnlDinamicoContenido.Controls.Add(LB_2);
                    }
                }
            }
            catch(Exception eCAC)
            {
                Log.EscribeLog("Existe un error en cargarAdicionalesCita: " + eCAC.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void ddlBandeja_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DropDownList dropDownList = (DropDownList)sender;
                if (int.Parse(dropDownList.SelectedValue) != 0)
                {
                    this.GV_ListaTrabajo.AllowPaging = true;
                    this.GV_ListaTrabajo.PageSize = int.Parse(dropDownList.SelectedValue);
                }
                else
                    this.GV_ListaTrabajo.AllowPaging = false;
                this.cargarlistadetrabajo(Usuario.intSitioID);
            }
            catch (Exception eddS)
            {
                Log.EscribeLog("Existe un error ddlBandeja_SelectedIndexChanged de frmListaTrabajo: " + eddS.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void txtBandeja_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox txtBandejaAvaluosGoToPage = (TextBox)sender;
                int numeroPagina;
                if (int.TryParse(txtBandejaAvaluosGoToPage.Text.Trim(), out numeroPagina))
                    this.GV_ListaTrabajo.PageIndex = numeroPagina - 1;
                else
                    this.GV_ListaTrabajo.PageIndex = 0;
                this.cargarlistadetrabajo(Usuario.intSitioID);
            }
            catch (Exception ex)
            {
                Log.EscribeLog("Existe un error txtBandeja_TextChanged de frmListaTrabajo: " + ex.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void btnCancelEstudios_Click(object sender, EventArgs e)
        {

        }
    }
}