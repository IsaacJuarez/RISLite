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
    public partial class frmPaciente : System.Web.UI.Page
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
        public static List<clsVarAcicionales> lstVarAdic = new List<clsVarAcicionales>();
        public static List<tbl_CAT_Identificacion> lstIdentificaciones = new List<tbl_CAT_Identificacion>();

        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
                String var = "";
                //if (!IsPostBack)
                //{
                if (Session["User"] != null)
                {
                    Usuario = (clsUsuario)Session["User"];
                    if (Usuario != null)
                    {
                        cargaFormaDetalle();
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
                //}
                //else
            }
            catch (Exception ePL)
            {
                Log.EscribeLog("Existe un error en Page_Load de frmAddDate: " + ePL.Message, 3, "");
            }
        }

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
                            if (Request.QueryString.Count > 0)
                            {
                                String ID = Security.Decrypt(Request.QueryString["var"].ToString());
                                txtBusqueda.Text = ID;
                                cargarPacientes(ID);
                            }
                            else
                            {
                                txtBusqueda.Text = "";
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
                        var = Security.Encrypt("1");
                        Response.Redirect(URL + "/frmSalir.aspx?var=" + var);
                    }
                }
            }
            catch (Exception ePL)
            {
                Log.EscribeLog("Existe un error en Page_Load de frmAgenda: " + ePL.Message, 3, "");
            }
        }



        private void cargaFormaDetalle()
        {
            try
            {
                cargaListagenero();
                cargaVariablesAdicionales();
                cargaIdentificaciones();
            }
            catch (Exception ecF)
            {
                Log.EscribeLog("Existe un error en cargaFormaDetalle: " + ecF.Message, 3, Usuario.vchUsuario);
            }
        }

        private void cargaListagenero()
        {
            try
            {
                CatalogoRequest request = new CatalogoRequest();
                request.mdlUser = Usuario;
                List<tbl_CAT_Genero> response = new List<tbl_CAT_Genero>();
                response = RisService.getListaGenero(request);
                if (response != null)
                {
                    if (response.Count > 0)
                    {
                        ddlGeneroDet.DataSource = response;
                        ddlGeneroDet.DataTextField = "vchGenero";
                        ddlGeneroDet.DataValueField = "intGeneroID";
                        ddlGeneroDet.DataBind();
                        ddlGeneroDet.Items.Insert(0, new ListItem("Seleccionar Genero...", "0"));
                    }
                }
            }
            catch (Exception eclg)
            {
                Log.EscribeLog("Existe un error en cargaListagenero: " + eclg.Message, 3, Usuario.vchUsuario);
            }
        }

        private void cargarPacientes(string iD)
        {
            try
            {
                grvPacientes.DataSource = null;
                PacienteRequest request = new PacienteRequest();
                PacienteResponse response = new PacienteResponse();
                request.mdlUser = Usuario;
                request.busqueda = iD;
                response = RisService.getBusquedaPacientesList(request);
                if (response != null)
                {
                    if (response.lstPacientes != null)
                    {
                        if (response.lstPacientes.Count > 0)
                        {
                            grvPacientes.DataSource = response.lstPacientes;
                        }
                    }
                }
                grvPacientes.DataBind();
            }
            catch (Exception ecP)
            {
                Log.EscribeLog("Existe un error en cargarPacientes: " + ecP.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void txtBusqueda_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtBusqueda.Text != "")
                    cargarPacientes(txtBusqueda.Text);
            }
            catch (Exception eBisqueda)
            {
                ShowMessage("Existe un error al buscar el paciente" + eBisqueda.Message, MessageType.Error, "alert_container");
                Log.EscribeLog("Existe un error en txtBusqueda_TextChanged: " + eBisqueda, 3, Usuario.vchUsuario);
            }
        }

        protected void grvPacientes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Pager)
                {
                    Label lblTotalNumDePaginas = (Label)e.Row.FindControl("lblBandejaTotal");
                    lblTotalNumDePaginas.Text = grvPacientes.PageCount.ToString();

                    TextBox txtIrAlaPagina = (TextBox)e.Row.FindControl("txtBandeja");
                    txtIrAlaPagina.Text = (grvPacientes.PageIndex + 1).ToString();

                    DropDownList ddlTamPagina = (DropDownList)e.Row.FindControl("ddlBandeja");
                    ddlTamPagina.SelectedValue = grvPacientes.PageSize.ToString();
                }

                if (e.Row.RowType != DataControlRowType.DataRow)
                {
                    return;
                }

                //if (e.Row.DataItem != null)
                //{
                //    clsVarAcicionales _mdl = (clsVarAcicionales)e.Row.DataItem;
                //    ImageButton ibtEstatus = (ImageButton)e.Row.FindControl("imbEstatus");
                //    ibtEstatus.Attributes.Add("onclick", "javascript:return confirm('¿Desea realizar el cambio de estatus del item seleccionado?');");
                //    if ((bool)_mdl.bitActivo)
                //        ibtEstatus.ImageUrl = @"~/Images/ic_action_tick.png";
                //    else
                //        ibtEstatus.ImageUrl = @"~/Images/ic_action_cancel.png";
                //}
            }
            catch (Exception eGUP)
            {
                throw eGUP;
            }
        }

        protected void grvPacientes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                if (e.NewPageIndex >= 0)
                {
                    this.grvPacientes.PageIndex = e.NewPageIndex;
                    cargarPacientes(txtBusqueda.Text);
                }
            }
            catch (Exception ex)
            {
                Log.EscribeLog("Existe un error grvPacientes_PageIndexChanging: " + ex.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void grvPacientes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int intPacienteID = 0;
                clsUsuario mdl = new clsUsuario();
                switch (e.CommandName)
                {
                    case "Detalle":
                        intPacienteID = Convert.ToInt32(e.CommandArgument.ToString());
                        cargarDetallePaciente(Convert.ToInt32(intPacienteID.ToString()));
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                        break;

                    case "CrearCita":
                        intPacienteID = Convert.ToInt32(e.CommandArgument.ToString());
                        string busqueda = Security.Encrypt(intPacienteID.ToString());
                        Response.Redirect(URL + "/frmAddDate.aspx?var=" + busqueda, false);
                        break;
                    case "Estudios":
                        intPacienteID = Convert.ToInt32(e.CommandArgument.ToString());
                        cargarEstudiosPaciente(Convert.ToInt32(intPacienteID.ToString()));
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "mdlEstudios", "$('#mdlEstudios').modal();", true);
                        break;

                }
            }
            catch (Exception eRU)
            {
                Log.EscribeLog("Existe un error grvAddCita_RowCommand: " + eRU.Message, 3, Usuario.vchUsuario);
            }
        }

        private void cargarEstudiosPaciente(int v)
        {
            try
            {

            }
            catch(Exception ecE)
            {
                Log.EscribeLog("Existe un error en cargarEstudiosPaciente: " + ecE.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void ddlBandeja_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DropDownList dropDownList = (DropDownList)sender;
                if (int.Parse(dropDownList.SelectedValue) != 0)
                {
                    this.grvPacientes.AllowPaging = true;
                    this.grvPacientes.PageSize = int.Parse(dropDownList.SelectedValue);
                }
                else
                    this.grvPacientes.AllowPaging = false;
                this.cargarPacientes(txtBusqueda.Text);
            }
            catch (Exception eddS)
            {
                Log.EscribeLog("Existe un error ddlBandejaCita_SelectedIndexChanged de frmConfiguracion: " + eddS.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void txtBandeja_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox txtBandejaAvaluosGoToPage = (TextBox)sender;
                int numeroPagina;
                if (int.TryParse(txtBandejaAvaluosGoToPage.Text.Trim(), out numeroPagina))
                    this.grvPacientes.PageIndex = numeroPagina - 1;
                else
                    this.grvPacientes.PageIndex = 0;
                this.cargarPacientes(txtBusqueda.Text);
            }
            catch (Exception ex)
            {
                Log.EscribeLog("Existe un error txtBusqueda_TextChanged de frmConfiguracion: " + ex.Message, 3, Usuario.vchUsuario);
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

        protected void txtCodigoPostal_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtCodigoPostal.Text.Length >= 4)
                {
                    cargarDireccion(txtCodigoPostal.Text);
                }
                //cargaIdentificaciones();
                //cargaVariablesAdicionales();
            }
            catch (Exception eCP)
            {
                Log.EscribeLog("Existe un error en txtCodigoPostal_TextChanged: " + eCP.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void btnCancelPacienteDet_Click(object sender, EventArgs e)
        {
            try
            {
                cargaIdentificaciones();
                cargaVariablesAdicionales();
            }
            catch(Exception ecd)
            {
                ShowMessage("Existe un error al limpiar el detalle:  " + ecd.Message, MessageType.Error, "alert_container");
                Log.EscribeLog("Existe un error en btnCancelPacienteDet_Click: " + ecd.Message, 3, Usuario.vchUsuario);
            } 
        }


        private void cargarDireccion(string codigoPostal)
        {
            try
            {
                DireccionRequest request = new DireccionRequest();
                DireccionResponse response = new DireccionResponse();
                request.mdlUser = Usuario;
                request.vchCodigoPostal = codigoPostal;
                response = RisService.getDireccionPaciente(request);
                ddlColoniaDet.DataSource = null;
                ddlColoniaDet.Items.Clear();
                ddlColoniaDet.DataBind();
                if (response != null)
                {
                    if (response.lstDireccion.Count > 0)
                    {
                        txtEstadoDet.Text = response.lstDireccion.First().vchEstado;
                        txtmunicipioDet.Text = response.lstDireccion.First().vchMunicipio;
                        ddlColoniaDet.DataSource = response.lstDireccion.OrderBy(x => x.vchColonia);
                        ddlColoniaDet.DataTextField = "vchColonia";
                        ddlColoniaDet.DataValueField = "intCodigoPostalID";
                        ddlColoniaDet.DataBind();
                        if (response.lstDireccion.Count == 1)
                        {
                            ddlColoniaDet.SelectedIndex = ddlColoniaDet.Items.IndexOf(ddlColoniaDet.Items.FindByValue(response.lstDireccion.First().intCodigoPostalID.ToString()));
                        }
                        else
                        {
                            ddlColoniaDet.Items.Insert(0, new ListItem("Seleccionar Colonia", "0"));
                        }
                    }
                }
            }
            catch (Exception eCColo)
            {
                Log.EscribeLog("Existe un error en cargarColonia: " + eCColo.Message, 3, Usuario.vchUsuario);
            }
        }

        private void cargarDetallePaciente(int intPacienteID)
        {
            try
            {
                PacienteRequest request = new PacienteRequest();
                PacienteResponse response = new PacienteResponse();
                request.mdlUser = Usuario;
                request.intPacienteID = intPacienteID;

                if (request != null)
                {
                    response = RisService.getPacienteDetalle(request);
                    if (response != null)
                    {
                        if (response.Success)
                        {
                            //bitEditar = true;
                            //HFintPacienteID.Value = intPacienteID.ToString();
                            //txtNombrePaciente.Text = response.mdlPaciente.vchNombre;
                            //txtApellidos.Text = response.mdlPaciente.vchApellidos;
                            //Date1.Text = response.mdlPaciente.datFechaNac.ToString("dd/MM/yyyy");
                            //lblIDs.Text = intPacienteID.ToString();
                            //lblIDs.Visible = true;
                            //btnEditPaciente.Visible = true;

                            if (response.mdlDireccion != null)
                            {
                                fillDireccion(response.mdlDireccion);
                            }

                            if (response.mdlPaciente != null)
                            {
                                fillPaciente(response.mdlPaciente);
                            }

                            if (response.lstIden != null)
                            {
                                if (response.lstIden.Count > 0)
                                {
                                    fillIdentificaciones(response.lstIden);
                                }
                            }

                            if (response.lstVarAdi != null)
                            {
                                if (response.lstVarAdi.Count > 0)
                                {
                                    fillVarAdicionalPaciente(response.lstVarAdi);
                                }
                            }
                        }
                        else
                        {
                            ShowMessage("Existe un error al cargar al paciente: " + response.Mensaje, MessageType.Error, "alert_container");
                        }
                    }
                }
            }
            catch (Exception eCP)
            {
                Log.EscribeLog("Existe un error en cargarDetallePaciente: " + eCP.Message, 3, Usuario.vchUsuario);
            }
        }

        private void fillDireccion(clsDireccion mdlDireccion)
        {
            try
            {
                Session["intDireccionID"] = mdlDireccion.intDireccionID;
                txtCalleDet.Text = mdlDireccion.vchCalle;
                txtNumeroDet.Text = mdlDireccion.vchNumero;
                cargarDireccion(mdlDireccion.vchCodigoPostal);
                txtCodigoPostal.Text = mdlDireccion.vchCodigoPostal;
                //txtEstadoDet.Text = mdlDireccion.vchEstado;
                //idEstadoDet.Value = mdlDireccion.intEstadoID.ToString();
                //txtmunicipioDet.Text = mdlDireccion.vchMunicipio;
                //intMunicipioID.Value = mdlDireccion.intMunicipioID.ToString();
                ddlColoniaDet.SelectedValue = mdlDireccion.intCodigoPostalID.ToString();
            }
            catch (Exception eFI)
            {
                Log.EscribeLog("Existe un error en fillDireccion: " + eFI.Message, 3, Usuario.vchUsuario);
            }
        }

        private void fillPaciente(clsPaciente mdlPaciente)
        {
            try
            {
                txtNombreDet.Text = mdlPaciente.vchNombre;
                txtApellidosDet.Text = mdlPaciente.vchApellidos;
                txtFecNacDet.Text = mdlPaciente.datFechaNac.ToString("dd/MM/yyyy");
                ddlGeneroDet.SelectedValue = mdlPaciente.intGeneroID.ToString();
                txtNumContactDet.Text = mdlPaciente.vchNumeroContacto;
                txtEmailDet.Text = mdlPaciente.vchEmail;
                Session["intDetPaciente"] = mdlPaciente.intDETPacienteID > 0 ? mdlPaciente.intDETPacienteID : 0;
            }
            catch (Exception eFI)
            {
                Log.EscribeLog("Existe un error en fillPaciente: " + eFI.Message, 3, Usuario.vchUsuario);
            }
        }

        private void fillVarAdicionalPaciente(List<clsVarAcicionales> lstVarAdi)
        {
            try
            {
                cargaVariablesAdicionales(lstVarAdi);
            }
            catch (Exception eFI)
            {
                Log.EscribeLog("Existe un error en fillVarAdicionalPaciente: " + eFI.Message, 3, Usuario.vchUsuario);
            }
        }

        private void fillIdentificaciones(List<tbl_REL_IdentificacionPaciente> lstIden)
        {
            try
            {
                cargaIdentificaciones(lstIden);
            }
            catch (Exception eFI)
            {
                Log.EscribeLog("Existe un error en fillIdentificaciones: " + eFI.Message, 3, Usuario.vchUsuario);
            }
        }

        private void cargaVariablesAdicionales()
        {
            try
            {
                pnlDinamicoContenido.Controls.Clear();
                VarAdicionalRequest request = new VarAdicionalRequest();
                request.mdlUser = Usuario;
                List<clsVarAcicionales> lst = new List<clsVarAcicionales>();
                lst = RisService.getVariablesAdicionalPaciente(request);
                if (lst != null)
                {
                    if (lst.Count > 0)
                    {
                        lstVarAdic = lst;
                        //-----
                        Table tb = new Table();
                        tb.ID = "tbAdic";
                        //-----
                        foreach (clsVarAcicionales item in lst)
                        {
                            //-----
                            TableRow tr = new TableRow();
                            //-----
                            //HtmlGenericControl createDiv = new HtmlGenericControl("DIV");
                            //createDiv.ID = "DivAdi" + item.intVariableAdiID;
                            //createDiv.Attributes.Add("class", "form-group");
                            ////createDiv.InnerHtml = " I'm a div, from code behind ";
                            //TextBox txt = new TextBox();
                            //txt.ID = "txtAdi" + item.intVariableAdiID;
                            //txt.CssClass = "form-control col-xs-10 col-sm-5";
                            //txt.Attributes.Add("placeholder", item.vchNombreVarAdi);
                            //Label lbl = new Label();
                            //lbl.Text = item.vchNombreVarAdi;
                            //lbl.ID = "lbl" + item.vchNombreVarAdi;
                            //lbl.AssociatedControlID = "txtAdi" + item.intVariableAdiID;
                            //lbl.Attributes.Add("class", "col-sm-3 control-label no-padding-right");
                            //HtmlGenericControl createDivText = new HtmlGenericControl();
                            //createDivText.Attributes.Add("class", "col-sm-9");
                            //createDivText.Controls.Add(txt);
                            //createDiv.Controls.Add(lbl);
                            //createDiv.Controls.Add(createDivText);
                            //divDinamicoContenido.Controls.Add(createDiv);
                            ////pnlDinamicoContenido.Controls.Add(lbl);
                            ////pnlDinamicoContenido.Controls.Add(txt);

                            //-----
                            TextBox txt = new TextBox();
                            txt.ID = "txtAdi" + item.intVariableAdiID;
                            txt.CssClass = "form-control col-xs-10 col-sm-5";
                            txt.Attributes.Add("placeholder", item.vchNombreVarAdi);
                            txt.Attributes.Add("width", "100%");
                            HiddenField hf = new HiddenField();
                            hf.ID = "hfVA" + +item.intVariableAdiID;
                            hf.Value = "";
                            hf.ClientIDMode = ClientIDMode.Static;
                            Label lbl = new Label();
                            lbl.Text = item.vchNombreVarAdi;
                            lbl.ID = "lbl" + item.vchNombreVarAdi;
                            lbl.AssociatedControlID = "txtAdi" + item.intVariableAdiID;
                            lbl.Attributes.Add("class", "control-label no-padding-left");
                            lbl.Attributes.Add("width", "100%");
                            TableCell tc = new TableCell();
                            tc.Controls.Add(lbl);
                            tc.Attributes.Add("width", "40%");
                            TableCell tc2 = new TableCell();
                            tc2.Controls.Add(txt);
                            tc2.Controls.Add(hf);
                            tc2.Attributes.Add("width", "60%");
                            tr.Cells.Add(tc);
                            tr.Cells.Add(tc2);
                            tb.Rows.Add(tr);
                            tb.Attributes.Add("width", "100%");
                            //-----
                        }
                        pnlDinamicoContenido.Controls.Add(tb);
                    }
                }
            }
            catch (Exception cvA)
            {
                Log.EscribeLog("Existe un error en cargaVariablesAdicionales: " + cvA.Message, 3, Usuario.vchUsuario);
            }
        }

        private void cargaVariablesAdicionales(List<clsVarAcicionales> lstVarAdi)
        {
            try
            {
                pnlDinamicoContenido.Controls.Clear();
                VarAdicionalRequest request = new VarAdicionalRequest();
                request.mdlUser = Usuario;
                List<clsVarAcicionales> lst = new List<clsVarAcicionales>();
                lst = RisService.getVariablesAdicionalPaciente(request);
                if (lst != null)
                {
                    if (lst.Count > 0)
                    {
                        lstVarAdic = lst;
                        //-----
                        Table tb = new Table();
                        tb.ID = "tbAdic";
                        //-----
                        foreach (clsVarAcicionales item in lst)
                        {
                            //-----
                            TableRow tr = new TableRow();
                            //-----

                            //-----
                            TextBox txt = new TextBox();
                            txt.ID = "txtAdi" + item.intVariableAdiID;
                            txt.CssClass = "form-control col-xs-10 col-sm-5";
                            txt.Attributes.Add("placeholder", item.vchNombreVarAdi);
                            txt.Attributes.Add("width", "100%");
                            txt.Text = lstVarAdi.First(x => x.intVariableAdiID == item.intVariableAdiID).vchValorAdicional;
                            txt.Enabled = false;
                            HiddenField hf = new HiddenField();
                            hf.ID = "hfVA" + +item.intVariableAdiID;
                            hf.Value = lstVarAdi.Where(x => x.intVariableAdiID == item.intVariableAdiID).First().intADIPacienteID.ToString();
                            hf.ClientIDMode = ClientIDMode.Static;
                            Label lbl = new Label();
                            lbl.Text = item.vchNombreVarAdi;
                            lbl.ID = "lbl" + item.vchNombreVarAdi;
                            lbl.AssociatedControlID = "txtAdi" + item.intVariableAdiID;
                            lbl.Attributes.Add("class", "control-label no-padding-left");
                            lbl.Attributes.Add("width", "100%");
                            TableCell tc = new TableCell();
                            tc.Controls.Add(lbl);
                            tc.Attributes.Add("width", "40%");
                            TableCell tc2 = new TableCell();
                            tc2.Controls.Add(txt);
                            tc2.Controls.Add(hf);
                            tc2.Attributes.Add("width", "60%");
                            tr.Cells.Add(tc);
                            tr.Cells.Add(tc2);
                            tb.Rows.Add(tr);
                            tb.Attributes.Add("width", "100%");
                            //-----
                        }
                        pnlDinamicoContenido.Controls.Add(tb);
                    }
                }
            }
            catch (Exception cvA)
            {
                Log.EscribeLog("Existe un error en cargaVariablesAdicionales: " + cvA.Message, 3, Usuario.vchUsuario);
            }
        }

        private void cargaIdentificaciones()
        {
            try
            {
                pnlIDContenido.Controls.Clear();
                VarAdicionalRequest request = new VarAdicionalRequest();
                request.mdlUser = Usuario;
                List<tbl_CAT_Identificacion> lst = new List<tbl_CAT_Identificacion>();
                lst = RisService.getVariablesAdicionalID(request);
                if (lst != null)
                {
                    if (lst.Count > 0)
                    {
                        lstIdentificaciones = lst;
                        //-----
                        Table tb = new Table();
                        tb.ID = "tbIden";
                        //-----
                        foreach (tbl_CAT_Identificacion item in lst)
                        {
                            //-----
                            TableRow tr = new TableRow();
                            //-----

                            //HtmlGenericControl createDiv = new HtmlGenericControl("DIV");
                            //createDiv.ID = "DivIden" + item.intIdentificacionID;
                            //createDiv.Attributes.Add("class", "form-group");
                            //createDiv.Attributes.Add("runat", "server");
                            ////createDiv.InnerHtml = " I'm a div, from code behind ";
                            //TextBox txt = new TextBox();
                            //txt.ID = "txtIden" + item.intIdentificacionID;
                            //txt.Attributes.Add("placeholder", item.vchNombreId);
                            //txt.CssClass = "form-control col-xs-10 col-sm-5";
                            //txt.ClientIDMode = ClientIDMode.Static;
                            //Label lbl = new Label();
                            //lbl.Text = item.vchNombreId;
                            //lbl.ID = "lbl" + item.vchNombreId;
                            //lbl.AssociatedControlID = "txtIden" + item.intIdentificacionID;
                            //lbl.Attributes.Add("class", "col-sm-3 control-label no-padding-right");
                            //HtmlGenericControl createDivText = new HtmlGenericControl();
                            //createDivText.Attributes.Add("class", "col-sm-9");
                            //createDivText.Controls.Add(txt);
                            //createDiv.Controls.Add(lbl);
                            //createDiv.Controls.Add(createDivText);
                            //divIDContenido.Controls.Add(createDiv);
                            ////pnlIDContenido.Controls.Add(lbl);
                            ////pnlIDContenido.Controls.Add(txt);



                            //-----
                            TextBox txt = new TextBox();
                            txt.ID = "txtIden" + item.intIdentificacionID;
                            txt.Attributes.Add("placeholder", item.vchNombreId);
                            txt.CssClass = "form-control col-xs-10 col-sm-5";
                            txt.ClientIDMode = ClientIDMode.Static;
                            txt.MaxLength = item.intIdentificacionID == 1 ? 11 : 30;
                            Label lbl = new Label();
                            lbl.Text = item.vchNombreId;
                            HiddenField hf = new HiddenField();
                            hf.ID = "hf" + item.intIdentificacionID;
                            hf.Value = "";
                            hf.ClientIDMode = ClientIDMode.Static;
                            lbl.ID = "lbl" + item.vchNombreId;
                            lbl.AssociatedControlID = "txtIden" + item.intIdentificacionID;
                            lbl.Attributes.Add("class", "control-label no-padding-left");
                            lbl.Attributes.Add("width", "100%");
                            TableCell tc = new TableCell();
                            tc.Controls.Add(lbl);
                            tc.Attributes.Add("width", "40%");
                            TableCell tc2 = new TableCell();
                            tc2.Controls.Add(txt);
                            tc2.Attributes.Add("width", "60%");
                            TableCell tc3 = new TableCell();
                            tc3.Controls.Add(hf);
                            tc3.Attributes.Add("width", "10%");
                            tr.Cells.Add(tc);
                            tr.Cells.Add(tc2);
                            tr.Cells.Add(tc3);
                            tb.Rows.Add(tr);
                            tb.Attributes.Add("width", "100%");
                            //-----

                        }
                        pnlIDContenido.Controls.Add(tb);
                    }
                }
            }
            catch (Exception cvA)
            {
                Log.EscribeLog("Existe un error en cargaIdentificaciones: " + cvA.Message, 3, Usuario.vchUsuario);
            }
        }

        private void cargaIdentificaciones(List<tbl_REL_IdentificacionPaciente> lstIden)
        {
            try
            {
                pnlIDContenido.Controls.Clear();
                VarAdicionalRequest request = new VarAdicionalRequest();
                request.mdlUser = Usuario;
                List<tbl_CAT_Identificacion> lst = new List<tbl_CAT_Identificacion>();
                lst = RisService.getVariablesAdicionalID(request);
                if (lst != null)
                {
                    if (lst.Count > 0)
                    {
                        lstIdentificaciones = lst;
                        //-----
                        Table tb = new Table();
                        tb.ID = "tbIden";

                        //-----
                        foreach (tbl_CAT_Identificacion item in lst)
                        {
                            //-----
                            TableRow tr = new TableRow();
                            //-----
                            //-----
                            TextBox txt = new TextBox();
                            txt.ID = "txtIden" + item.intIdentificacionID;
                            txt.Attributes.Add("placeholder", item.vchNombreId);
                            txt.CssClass = "form-control col-xs-10 col-sm-5";
                            txt.ClientIDMode = ClientIDMode.Static;
                            txt.Text = lstIden.Where(x => x.intIdentificacionID == item.intIdentificacionID).First().vchValor;
                            txt.Enabled = false;
                            HiddenField hf = new HiddenField();
                            hf.ID = "hf" + item.intIdentificacionID;
                            hf.Value = lstIden.Where(x => x.intIdentificacionID == item.intIdentificacionID).First().intRELIdenPacienteID.ToString();
                            hf.ClientIDMode = ClientIDMode.Static;
                            Label lbl = new Label();
                            lbl.Text = item.vchNombreId;
                            lbl.ID = "lbl" + item.vchNombreId;
                            lbl.AssociatedControlID = "txtIden" + item.intIdentificacionID;
                            lbl.Attributes.Add("class", "control-label no-padding-left");
                            lbl.Attributes.Add("width", "100%");
                            TableCell tc = new TableCell();
                            tc.Controls.Add(lbl);
                            tc.Attributes.Add("width", "40%");
                            TableCell tc2 = new TableCell();
                            tc2.Controls.Add(txt);
                            tc2.Attributes.Add("width", "50%");
                            TableCell tc3 = new TableCell();
                            tc3.Controls.Add(hf);
                            tc3.Attributes.Add("width", "10%");
                            tr.Cells.Add(tc);
                            tr.Cells.Add(tc2);
                            tr.Cells.Add(tc3);
                            tb.Rows.Add(tr);
                            tb.Attributes.Add("width", "100%");
                            //-----

                        }

                        pnlIDContenido.Controls.Add(tb);
                    }
                }
            }
            catch (Exception cvA)
            {
                Log.EscribeLog("Existe un error en cargaIdentificaciones: " + cvA.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void btnCancelEstudios_Click(object sender, EventArgs e)
        {

        }

        protected void grvEstudios_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void grvEstudios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void grvEstudios_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void txtBandejaEst_TextChanged(object sender, EventArgs e)
        {

        }

        protected void ddlBandejaEst_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}