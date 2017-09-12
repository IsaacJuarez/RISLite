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
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Fuji.RISLite.Site
{
    public partial class frmAddDate : System.Web.UI.Page
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
        public static List<clsEstudio> lstEstudios = new List<clsEstudio>();

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
                Log.EscribeLog("Existe un error en Page_Load de frmAddDate: " + ePL.Message, 3, "");
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> obtenerPacienteBusqueda(string prefixText, int count)
        {
            List<string> lstPaciente = new List<string>();
            try
            {
                if (prefixText != "")
                {
                    PacienteRequest request = new PacienteRequest();
                    PacienteResponse response = new PacienteResponse();
                    RisLiteService service = new RisLiteService();
                    request.mdlUser = Usuario;
                    request.busqueda = prefixText;
                    response = service.getBusquedaPacientes(request);
                    if (response != null)
                    {
                        if (response.lstCadenas.Count > 0)
                            lstPaciente = response.lstCadenas;
                    }
                }

            }
            catch (Exception eOP)
            {
                Log.EscribeLog("Existe un error obtenerPacienteBusqueda:" + eOP.Message, 3, "");
            }
            return lstPaciente;
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> obtenerEstudioBusqueda(string prefixText, int count)
        {
            List<string> lstPaciente = new List<string>();
            try
            {
                if (prefixText != "")
                {
                    PacienteRequest request = new PacienteRequest();
                    PacienteResponse response = new PacienteResponse();
                    RisLiteService service = new RisLiteService();
                    request.mdlUser = Usuario;
                    request.busqueda = prefixText;
                    response = service.getBusquedaEstudio(request);
                    if (response != null)
                    {
                        if (response.lstCadenas.Count > 0)
                            lstPaciente = response.lstCadenas;
                    }
                }

            }
            catch (Exception eOP)
            {
                Log.EscribeLog("Existe un error obtenerEstudioBusqueda:" + eOP.Message, 3, "");
            }
            return lstPaciente;
        }

        protected void btnAddUser_Click(object sender, EventArgs e)
        {
            try
            {
                cargaFormaDetalle();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
            }
            catch(Exception eAU)
            {
                Log.EscribeLog("Existe un error en btnAddUser_Click:" + eAU.Message, 3, Usuario.vchUserAdmin);
            }
        }

        private void cargaFormaDetalle()
        {
            try
            {
                cargaListagenero();
                cargaVariablesAdicionales();
                cargaIdentificaciones();
                lblIDs.Visible = false;
            }
            catch(Exception ecF)
            {
                Log.EscribeLog("Existe un error en cargaFormaDetalle: " + ecF.Message, 3, Usuario.vchUsuario);
            }
        }

        private void cargaIdentificaciones()
        {
            try
            {
                VarAdicionalRequest request = new VarAdicionalRequest();
                request.mdlUser = Usuario;
                List<tbl_CAT_Identificacion> lst = new List<tbl_CAT_Identificacion>();
                lst = RisService.getVariablesAdicionalID(request);
                if (lst != null)
                {
                    if (lst.Count > 0)
                    {
                        foreach (tbl_CAT_Identificacion item in lst)
                        {
                            HtmlGenericControl createDiv = new HtmlGenericControl("DIV");
                            createDiv.ID = "Div" + item.vchNombreId;
                            createDiv.Attributes.Add("class", "form-group");
                            //createDiv.InnerHtml = " I'm a div, from code behind ";
                            TextBox txt = new TextBox();
                            txt.ID = "txt" + item.vchNombreId;
                            txt.Attributes.Add("placeholder", item.vchNombreId);
                            txt.CssClass = "form-control col-xs-10 col-sm-5";
                            Label lbl = new Label();
                            lbl.Text = item.vchNombreId;
                            lbl.ID = "lbl" + item.vchNombreId;
                            lbl.AssociatedControlID = "txt" + item.vchNombreId;
                            lbl.Attributes.Add("class", "col-sm-3 control-label no-padding-right");
                            HtmlGenericControl createDivText = new HtmlGenericControl();
                            createDivText.Attributes.Add("class", "col-sm-9");
                            createDivText.Controls.Add(txt);
                            createDiv.Controls.Add(lbl);
                            createDiv.Controls.Add(createDivText);
                            divIDContenido.Controls.Add(createDiv);
                        }
                    }
                }
            }
            catch (Exception cvA)
            {
                Log.EscribeLog("Existe un error en cargaIdentificaciones: " + cvA.Message, 3, Usuario.vchUsuario);
            }
        }

        private void cargaVariablesAdicionales()
        {
            try
            {
                VarAdicionalRequest request = new VarAdicionalRequest();
                request.mdlUser = Usuario;
                List<clsVarAcicionales> lst = new List<clsVarAcicionales>();
                lst = RisService.getVariablesAdicionalPaciente(request);
                if (lst != null)
                {
                    if (lst.Count > 0)
                    {
                        foreach(clsVarAcicionales item in lst)
                        {
                            HtmlGenericControl createDiv = new HtmlGenericControl("DIV");
                            createDiv.ID = "Div" + item.vchNombreVarAdi;
                            createDiv.Attributes.Add("class", "form-group");
                            //createDiv.InnerHtml = " I'm a div, from code behind ";
                            TextBox txt = new TextBox();
                            txt.ID = "txt" + item.vchNombreVarAdi;
                            txt.CssClass = "form-control col-xs-10 col-sm-5";
                            txt.Attributes.Add("placeholder", item.vchNombreVarAdi);
                            Label lbl = new Label();
                            lbl.Text = item.vchNombreVarAdi;
                            lbl.ID= "lbl" + item.vchNombreVarAdi;
                            lbl.AssociatedControlID = "txt" + item.vchNombreVarAdi;
                            lbl.Attributes.Add("class", "col-sm-3 control-label no-padding-right");
                            HtmlGenericControl createDivText = new HtmlGenericControl();
                            createDivText.Attributes.Add("class", "col-sm-9");
                            createDivText.Controls.Add(txt);
                            createDiv.Controls.Add(lbl);
                            createDiv.Controls.Add(createDivText);
                            divDinamicoContenido.Controls.Add(createDiv);
                        }
                    }
                }
            }
            catch(Exception cvA)
            {
                Log.EscribeLog("Existe un error en cargaVariablesAdicionales: " + cvA.Message, 3, Usuario.vchUsuario);
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
            catch(Exception eclg)
            {
                Log.EscribeLog("Existe un error en cargaListagenero: " + eclg.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void btnEditPaciente_Click(object sender, EventArgs e)
        {

        }

        protected void grvEstudios_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {

        }

        protected void grvEstudios_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {

        }

        protected void grvEstudios_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
        {

        }

        protected void grvEstudios_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            try
            {
                int inrRelModPres = 0;
                clsEstudio mdl = new clsEstudio();
                switch (e.CommandName)
                {
                    case "ElegirHorario":
                        inrRelModPres = Convert.ToInt32(e.CommandArgument.ToString());
                        EquipoRequest request = new EquipoRequest();
                        request.mdlUser = Usuario;
                        //request.intEquipoID = intEquipoID;
                        //EquipoResponse response = new EquipoResponse();
                        //response = RisService.setActualizaEquipo(request);
                        //if (response != null)
                        //{
                        //    if (response.Success)
                        //    {
                                ShowMessage("Se buscará un horario para el estudio.", MessageType.Correcto, "alert_container");
                        //        //fillCat();
                        //        cargarEquipo();
                        //    }
                        //    else
                        //    {
                        //        ShowMessage("Existe un error al actualizar: " + response.Mensaje, MessageType.Error, "alert_container");
                        //    }
                        //}
                        //else
                        //{
                        //    ShowMessage("Existe un error al actualizar, favor de revisar la información. ", MessageType.Advertencia, "alert_container");
                        //}
                        break;
                }
            }
            catch(Exception eRCE)
            {
                Log.EscribeLog("Existe un error en grvEstudios_RowCommand: " + eRCE.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void grvEstudios_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {

        }

        protected void grvEstudios_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
        {

        }

        protected void grvEstudios_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {

        }

        protected void ddlBandeja_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void txtBandeja_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtCodigoPostal_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtCodigoPostal.Text.Length >= 4)
                {
                    DireccionRequest request = new DireccionRequest();
                    DireccionResponse response = new DireccionResponse();
                    request.mdlUser = Usuario;
                    request.vchCodigoPostal = txtCodigoPostal.Text;
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
            }
            catch(Exception eCP)
            {
                Log.EscribeLog("Existe un error en txtCodigoPostal_TextChanged: " + eCP.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void bntAddPacienteDEt_Click(object sender, EventArgs e)
        {
            try
            {
                clsPaciente mdlPaciente = new clsPaciente();
                clsDireccion mdlDireccion = new clsDireccion();
                mdlPaciente = obtenerPacienteDet();
                mdlDireccion = obtenerDireccion();
                if (mdlPaciente != null)
                {
                    PacienteRequest request = new PacienteRequest();
                    PacienteResponse response = new PacienteResponse();
                    request.mdlUser = Usuario;
                    request.mdlDireccion = mdlDireccion;
                    request.mdlPaciente = mdlPaciente;
                    if (request != null)
                    {
                        response = RisService.setPaciente(request);
                        if(response!= null)
                        {
                            if (response.Success)
                            {
                                ShowMessage("Se agregó correctamente el paciente." + response.Mensaje, MessageType.Correcto, "alert_container");
                                cargarDetallePaciente(response.intPacienteID);
                                lblIDs.Text = response.intPacienteID.ToString();
                                lblIDs.Visible = true;
                            }
                            else
                            {
                                ShowMessage("Verificar la informacion: " + response.Mensaje, MessageType.Advertencia, "alert_container");
                            }
                        }
                        else
                        {
                            ShowMessage("Verificar la informacion. ", MessageType.Advertencia, "alert_container");
                        }
                    }
                    else
                    {
                        ShowMessage("Verificar la informacion. ", MessageType.Advertencia, "alert_container");
                    }
                }
            }
            catch(Exception eAP)
            {
                ShowMessage("Existe un error al agregar el paciente: " + eAP.Message, MessageType.Error, "alert_container");
                Log.EscribeLog("Existe un error en bntAddPacienteDEt_Click: " + eAP.Message, 3, Usuario.vchUsuario);
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
                            HFintPacienteID.Value = intPacienteID.ToString();
                            txtNombrePaciente.Text = response.mdlPaciente.vchNombre;
                            txtApellidos.Text = response.mdlPaciente.vchApellidos;
                            Date1.Text = response.mdlPaciente.datFechaNac.ToString("dd/MM/yyyy");
                            lblIDs.Text = intPacienteID.ToString();
                            lblIDs.Visible = true;
                        }
                        else
                        {
                            ShowMessage("Existe un error al cargar al paciente: " + response.Mensaje, MessageType.Error, "alert_container");
                        }
                    }
                }
            }
            catch(Exception eCP)
            {
                Log.EscribeLog("Existe un error en cargarDetallePaciente: " + eCP.Message, 3, Usuario.vchUsuario);
            }
        }

        private clsDireccion obtenerDireccion()
        {
            clsDireccion mdl = new clsDireccion();
            try
            {
                mdl.intCodigoPostalID = Convert.ToInt32(ddlColoniaDet.SelectedValue);
                mdl.vchCalle = txtCalleDet.Text;
                mdl.vchNumero = txtNumeroDet.Text;
            }
            catch(Exception eoD)
            {
                Log.EscribeLog("Existe un error al obtener la dirección: " + eoD.Message, 3, Usuario.vchUsuario);
            }
            return mdl;
        }

        private clsPaciente obtenerPacienteDet()
        {
            clsPaciente mdl = new clsPaciente();
            try
            {
                mdl.datFechaNac = Convert.ToDateTime(txtFecNacDet.Text);
                mdl.intGeneroID = Convert.ToInt32(ddlGeneroDet.SelectedValue.ToString());
                //mdl.intPacienteID
                mdl.vchApellidos = txtApellidosDet.Text;
                mdl.vchEmail = txtEmailDet.Text;
                mdl.vchNombre = txtNombreDet.Text;
                mdl.vchNumeroContacto = txtNumContactDet.Text;
            }
            catch (Exception eOP)
            {
                mdl = null;
                Log.EscribeLog("Existe un error en obtenerPacienteDet: " + eOP.Message, 3, Usuario.vchUsuario);
            }
            return mdl;
        }

        protected void btnCancelPacienteDet_Click(object sender, EventArgs e)
        {

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

        protected void txtBusquedaPaciente_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string id = txtBusquedaPaciente.Text;
                string[] paciente = txtBusquedaPaciente.Text.ToString().Split('|');
                id = paciente[0];
                txtBusquedaPaciente.Text = "";
                cargarDetallePaciente(Convert.ToInt32(id));
            }
            catch(Exception etC)
            {
                Log.EscribeLog("Existe un error en txtBusquedaPaciente_TextChanged: " + etC.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void txtBusquedaEstudio_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string id = txtBusquedaEstudio.Text;
                string[] estudio = txtBusquedaEstudio.Text.ToString().Split('|');
                id = estudio[0];
                txtBusquedaEstudio.Text = "";
                cargarEstudioGrid(Convert.ToInt32(id));
            }
            catch (Exception etC)
            {
                Log.EscribeLog("Existe un error en txtBusquedaEstudio_TextChanged: " + etC.Message, 3, Usuario.vchUsuario);
            }
        }

        private void cargarEstudioGrid(int v)
        {
            try
            {
                EstudioRequest request = new EstudioRequest();
                EstudioResponse response = new EstudioResponse();
                request.mdlUser = Usuario;
                clsEstudio mdlEstudio = new clsEstudio();
                mdlEstudio.intRelModPres = v;
                request.mdlEstudio = mdlEstudio;
                response = RisService.getEstudioDetalle(request);
                if (response != null)
                {
                    lstEstudios.Add(response.mdlEstudio);
                    grvEstudios.DataSource = null;
                    grvEstudios.DataBind();
                    grvEstudios.DataSource = lstEstudios;
                    grvEstudios.DataBind();
                }
            }
            catch(Exception ecEG)
            {
                Log.EscribeLog("Existe un error en cargarEstudioGrid: " + ecEG.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void btnAddCita_Click(object sender, EventArgs e)
        {
            try
            {
                if(lblIDs.Text != "")//Usuario
                {
                    if(lstEstudios!= null)
                    {
                        if (lstEstudios.Count > 0)
                        {

                        }
                        else
                        {
                            ShowMessage("Validar los estudios", MessageType.Advertencia, "alert_container");
                        }
                    }
                    else
                    {
                        ShowMessage("Validar los estudios", MessageType.Advertencia, "alert_container");
                    }
                }
                else
                {
                    ShowMessage("Validar el usuario", MessageType.Advertencia, "alert_container");
                }
            }
            catch (Exception eBC)
            {
                ShowMessage("Existe un error al agregar la cita: " + eBC.Message, MessageType.Error, "alert_container");
                Log.EscribeLog("Existe un error en btnAddCita_Click: " + eBC.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void btnCancelPaciente_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch(Exception eBC)
            {
                ShowMessage("Existe un error al cancelar: " + eBC.Message, MessageType.Error, "alert_container");
                Log.EscribeLog("Existe un error en btnCancelPaciente_Click: " + eBC.Message, 3, Usuario.vchUsuario);
            }
        }
    }
}