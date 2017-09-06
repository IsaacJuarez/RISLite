using Fuji.RISLite.Entidades.DataBase;
using Fuji.RISLite.Entidades.Extensions;
using Fuji.RISLite.Entities;
using Fuji.RISLite.Site.Services;
using Fuji.RISLite.Site.Services.DataContract;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.UI;
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
        public static List<string> obtenerPacienteBusqueda()
        {
            List<string> lstPaciente = new List<string>();
            try
            {
                string paciente1 = "Paciente 1 || NSS : 11111";
                lstPaciente.Add(paciente1);
                string paciente2 = "Paciente 2 || NSS : 22222";
                lstPaciente.Add(paciente2);
                string paciente3 = "Paciente 3 || NSS : 33333";
                lstPaciente.Add(paciente3);
            }
            catch (Exception eOP)
            {
                Log.EscribeLog("Existe un error obtenerPacienteBusqueda:" + eOP.Message, 3, "");
            }
            return lstPaciente;
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> obtenerEstudioBusqueda()
        {
            List<string> lstPaciente = new List<string>();
            try
            {
                string paciente1 = "Estudio 1 || Tipo :  US";
                lstPaciente.Add(paciente1);
                string paciente2 = "Estudio 2 || Tipo : RX";
                lstPaciente.Add(paciente2);
                string paciente3 = "Estudio 3 || Tipo : CT";
                lstPaciente.Add(paciente3);
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
            }
            catch(Exception ecF)
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

        }

        protected void bntAddPacienteDEt_Click(object sender, EventArgs e)
        {

        }
    }
}