using Fuji.RISLite.Entidades.Extensions;
using Fuji.RISLite.Entities;
using Fuji.RISLite.Site.Services;
using Fuji.RISLite.Site.Services.DataContract;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Configuration;

namespace Fuji.RISLite.Site
{
    public partial class frmArribo : System.Web.UI.Page
    {
        public static clsUsuario Usuario = new clsUsuario();
        RisLiteService RisService = new RisLiteService();
        public string URL
        {
            get
            {
                return ConfigurationManager.AppSettings["URL"];
            }
        }
        public static List<clsEstudio> lstEstudios = new List<clsEstudio>();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Session["User"] != null)
                    {
                        Usuario = (clsUsuario)Session["User"];
                    }
                    else
                    {
                        Usuario = null;
                    }
                    if (Request.QueryString.Count > 0)
                    {
                        if (Request.QueryString["var"] != null)
                        {
                            divMensaje.Visible = false;
                            divPrincipal.Visible = true;
                            cargarCita(Request.QueryString["var"].ToString());
                        }
                        else
                        {
                            lblMensaje.Text = "No se reconoce como una cita valida.";
                            divMensaje.Visible = true;
                            //divPrincipal.Visible = false;
                        }
                    }
                    else
                    {
                        lblMensaje.Text = "Existe un error al verificar la cita";
                        divMensaje.Visible = true;
                        //divPrincipal.Visible = false;
                    }
                }
                //else
                //{
                //    lblMensaje.Text = "Existe un error al verificar la cita";
                //    divMensaje.Visible = true;
                //    //divPrincipal.Visible = false;
                //}
            }
            catch (Exception ePL)
            {
                Log.EscribeLog("Existe un error en Page_Load de frmArribo.aspx: " + ePL.Message, 3, "");
            }
        }

        private void cargarCita(string CitaID)
        {
            try
            {
                int intCitaID = Convert.ToInt32(Security.Decrypt(CitaID));
                ArriboRequest request = new ArriboRequest();
                ArriboResponse response = new ArriboResponse();
                request.intCitaID = intCitaID;
                clsUsuario mdlUser = new clsUsuario();
                mdlUser.vchUsuario = DateTime.Now.ToString("dd/MM/yyyy");
                request.mdlUser = mdlUser;
                response = RisService.getDetalleCitaPaciente(request);
                if (response != null)
                {
                    if (response.Success)
                    {
                        fillForma(response.mdlCita, response.lstEstudio);
                    }
                    else
                    {
                        ShowMessage("Existe un error: " + response.mensaje, MessageType.Error, "alert_container");
                    }
                }
                else
                {
                    ShowMessage("Verificar la información.", MessageType.Error, "alert_container");
                }
            }
            catch (Exception eSM)
            {
                Log.EscribeLog("Existe un error en cargarCita: " + eSM.Message, 3, "Arribos");
            }
        }

        private void fillForma(clsEstudioCita mdlCita, List<clsEstudio> lstEstudio)
        {
            try
            {
                lblNamePacient.Text = mdlCita.vchNombrePaciente;
                hfintCitaID.Value = mdlCita.intCitaID.ToString();
                lstEstudios = null;
                grvEstudios.DataSource = null;
                if (lstEstudio != null)
                {
                    if (lstEstudio.Count > 0)
                    {
                        lblFechaCita.Text = lstEstudio.OrderBy(x => x.fechaInicio).First().fechaInicio.ToString("dd/MM/yyyy HH:mm tt");
                        lstEstudios = lstEstudio;
                        grvEstudios.DataSource = lstEstudio;
                    }
                    else
                    {
                        lblFechaCita.Text = mdlCita.datFechaCita.ToString("dd/MM/yyyy HH:mm tt");
                    }
                }
                grvEstudios.DataBind();
            }
            catch (Exception eSM)
            {
                Log.EscribeLog("Existe un error en fillForma: " + eSM.Message, 3, "");
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

        protected void btnEditPaciente_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception eDP)
            {
                ShowMessage("Existe un error: " + eDP, MessageType.Error, "alert_container");
                Log.EscribeLog("Existe un error en btnEditPaciente_Click: " + eDP.Message, 3, "");
            }
        }

        protected void grvEstudios_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {

            }
            catch (Exception eDP)
            {
                ShowMessage("Existe un error: " + eDP, MessageType.Error, "alert_container");
                Log.EscribeLog("Existe un error en grvEstudios_RowDataBound: " + eDP.Message, 3, "");
            }
        }

        protected void grvEstudios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {

            }
            catch (Exception eDP)
            {
                ShowMessage("Existe un error: " + eDP, MessageType.Error, "alert_container");
                Log.EscribeLog("Existe un error en grvEstudios_PageIndexChanging: " + eDP.Message, 3, "");
            }
        }

        protected void grvEstudios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {

            }
            catch (Exception eDP)
            {
                ShowMessage("Existe un error: " + eDP, MessageType.Error, "alert_container");
                Log.EscribeLog("Existe un error en grvEstudios_RowCommand: " + eDP.Message, 3, "");
            }
        }

        protected void ddlBandeja_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception eDP)
            {
                ShowMessage("Existe un error: " + eDP, MessageType.Error, "alert_container");
                Log.EscribeLog("Existe un error en ddlBandeja_SelectedIndexChanged: " + eDP.Message, 3, "");
            }
        }

        protected void txtBandeja_TextChanged(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception eDP)
            {
                ShowMessage("Existe un error: " + eDP, MessageType.Error, "alert_container");
                Log.EscribeLog("Existe un error en txtBandeja_TextChanged: " + eDP.Message, 3, "");
            }
        }

        protected void btnArribo_Click(object sender, EventArgs e)
        {
            try
            {
                if (validarCheck())
                {
                    if (Session["User"] != null)
                    {
                        Usuario = (clsUsuario)Session["User"];
                        actualizarEstudios();
                    }
                    else
                    {
                        Usuario = null;
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalLogin", "$('#modalLogin').modal();", true);
                    }

                }
                else
                {
                    ShowMessage("Elegir al menos un estudio.", MessageType.Advertencia, "alert_container");
                }
            }
            catch (Exception eDP)
            {
                ShowMessage("Existe un error: " + eDP, MessageType.Error, "alert_container");
                Log.EscribeLog("Existe un error en btnArribo_Click: " + eDP.Message, 3, "");
            }
        }

        private bool validarCheck()
        {
            bool valido = false;
            try
            {
                foreach (GridViewRow row in grvEstudios.Rows)
                {
                    CheckBox chk = row.Cells[4].FindControl("chkEstudio") as CheckBox;
                    if (chk != null && chk.Checked)
                    {
                        valido = true;
                    }
                }
            }
            catch (Exception eDP)
            {
                ShowMessage("Existe un error: " + eDP, MessageType.Error, "alert_container");
                Log.EscribeLog("Existe un error en btnArribo_Click: " + eDP.Message, 3, "");
            }
            return valido;
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                ValidaUserRequest request = new ValidaUserRequest();
                ValidaUserResponse response = new ValidaUserResponse();
                request.user = txtUsuario.Text;
                request.pass = Security.Encrypt(txtPass.Text);
                if (request != null)
                {
                    response = RisService.getLoginUser(request);
                    if (response != null)
                    {
                        if (response.Success)
                        {
                            Session["User"] = response.mdlUser;
                            Session["lstVistas"] = response.lstVistas;
                            Label1.Text = "Usuario Correcto.";
                            Log.EscribeLog("Usuario logueado: " + response.mdlUser.vchUsuario, 1, "LOGIN");
                            Usuario = response.mdlUser;
                            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalLogin", "$('#modalLogin').modal('hide');", true);
                            actualizarEstudios();
                        }
                        else
                        {
                            Log.EscribeLog("Mensaje de error: " + response.mensaje, 2, "LOGIN");
                            lblMensaje.Text = "Mensaje de error: " + response.mensaje;
                        }
                    }
                    else
                    {
                        Log.EscribeLog("Verificar la información", 2, "LOGIN");
                        lblMensaje.Text = "Verificar la información";
                    }
                }
                else
                {
                    Log.EscribeLog("Verificar la información", 2, "LOGIN");
                    lblMensaje.Text = "Verificar la información";
                }
            }
            catch (Exception eLogin)
            {
                Log.EscribeLog("Existe un error en btnCancelLogin_Click: " + eLogin.Message, 3, "Usuario");
            }
        }

        private void actualizarEstudios()
        {
            try
            {
                List<clsEstudio> lstEst = new List<clsEstudio>();
                lstEst = obtenerListaEstudios();
                if (lstEst != null && lstEst.Count > 0)
                {
                    string result = marcarArriboEstudios(lstEst);
                    if (result == "OK")
                    {
                        ShowMessage("Se marcarón correctamente los estudios.", MessageType.Correcto, "alert_container");
                        Thread.Sleep((int)TimeSpan.FromSeconds(2).TotalMilliseconds);
                        Response.Redirect(URL + "/Default.aspx", false);
                    }
                    else
                    {
                        ShowMessage("Verificar la información de los estudios: " + result, MessageType.Advertencia, "alert_container");
                    }
                }
                else
                {
                    ShowMessage("Verificar la información de los estudios.", MessageType.Advertencia, "alert_container");
                }
            }
            catch(Exception eAE)
            {
                Log.EscribeLog("Existe un error en : " + eAE.Message, 3, Usuario.vchUsuario);
            }
        }

        private string marcarArriboEstudios(List<clsEstudio> lstEst)
        {
            string result = "NOK";
            try
            {
                foreach(clsEstudio estudio in lstEst)
                {
                    ArriboResponse response = new ArriboResponse();
                    ArriboRequest request = new ArriboRequest();
                    request.intEstatusID = 2;
                    request.intEstudioID = estudio.intEstudioID;
                    request.mdlUser = Usuario;
                    response = RisService.setActualizaEstudioEstatus(request);
                    if (response != null)
                    {
                        if (response.Success)
                        {
                            result = "OK";
                        }
                        else
                        {
                            result = response.mensaje;
                        }
                    }
                    else
                    {
                        result = "Existe un error";
                    }
                }
            }
            catch(Exception eMaE)
            {
                Log.EscribeLog("Existe un error en marcarArriboEstudios: " + eMaE.Message, 3, Usuario.vchUsuario);
            }
            return result;
        }

        private List<clsEstudio> obtenerListaEstudios()
        {
            List<clsEstudio> lst = new List<clsEstudio>();
            try
            {
                foreach(GridViewRow row in grvEstudios.Rows)
                {
                    CheckBox chk = row.Cells[4].FindControl("chkEstudio") as CheckBox;
                    if (chk != null && chk.Checked)
                    {
                        int intEstudioID = Convert.ToInt32(grvEstudios.DataKeys[row.RowIndex].Values["intEstudioID"].ToString());
                        clsEstudio mdlEst = new clsEstudio();
                        mdlEst = lstEstudios.First(x => x.intEstudioID == intEstudioID);
                        if (mdlEst != null)
                        {
                            lst.Add(mdlEst);
                        }
                    }
                }
            }
            catch (Exception eOLE)
            {
                Log.EscribeLog("Existe un error en obtenerListaEstudios: " + eOLE.Message, 3, Usuario.vchUsuario);
            }
            return lst;
        }

        protected void btnCancelLogin_Click(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalLogin", "$('#modalLogin').modal('hide');", true);
            }
            catch (Exception eLogin)
            {
                Log.EscribeLog("Existe un error en btnCancelLogin_Click: " + eLogin.Message, 3, "Usuario");
            }
        }
    }
}