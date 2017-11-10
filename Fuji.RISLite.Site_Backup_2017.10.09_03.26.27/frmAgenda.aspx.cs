using Fuji.RISLite.Entidades.Extensions;
using Fuji.RISLite.Entities;
using Fuji.RISLite.Site.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.UI.WebControls;

namespace Fuji.RISLite.Site
{
    public partial class frmAgenda : System.Web.UI.Page
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
                            Date1.Attributes.Add("readonly", "readonly");
                            customCalendarExtender.EndDate = DateTime.Today;
                            cargarChecksPaciente();
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

        private void cargarChecksPaciente()
        {
            try
            {
                string htmAdd = "<label class='btn btn-success active'>" +
                "<input type='checkbox' autocomplete='off' checked> VIP" +
                "<span class='glyphicon glyphicon-ok'></span>" +
                "</label>" +

                "<label class='btn btn-primary'>" +
                    "<input type='checkbox' autocomplete='off'>Embarazo" +
                    "<span class='glyphicon glyphicon-ok'></span>" +
                "</label>			" +

                "<label class='btn btn-info'>" +
                    "<input type='checkbox' autocomplete='off'>Urgencia" +
                    "<span class='glyphicon glyphicon-ok'></span>" +
                "</label>			" +

                "<label class='btn btn-default'>" +
                    "<input type='checkbox' autocomplete='off'>Necesidades Especiales" +
                    "<span class='glyphicon glyphicon-ok'></span>" +
                "</label>		" +

                "<label class='btn btn-warning'>" +
                    "<input type='checkbox' autocomplete='off'>Alergia" +
                    "<span class='glyphicon glyphicon-ok'></span>" +
                "</label>	" +

                "<label class='btn btn-danger'>" +
                    "<input type='checkbox' autocomplete='off'>Hospitalizado" +
                    "<span class='glyphicon glyphicon-ok'></span>" +
                "</label>";
                divCheks.Controls.Add(new System.Web.UI.LiteralControl(htmAdd));

                //Agragar Cheks ***
                //CheckBox ck = new CheckBox();
                //ck.CssClass = "btn-sm btn-success active  glyphicon glyphicon-ok";
                //ck.Text = "Embarazo";
                //divCheks.Controls.Add(ck);
                //ck = new CheckBox();
                //ck.CssClass = "btn-sm btn-primary glyphicon glyphicon-ok";
                //ck.Text = "VIP";
                //divCheks.Controls.Add(ck);
                //ck = new CheckBox();
                //ck.CssClass = "btn-sm btn-info glyphicon glyphicon-ok";
                //ck.Text = "Necesidades Especiales";
                //divCheks.Controls.Add(ck);
                //ck = new CheckBox();
                //ck.CssClass = "btn-sm btn-default glyphicon glyphicon-ok";
                //ck.Text = "Urgencia";
                //divCheks.Controls.Add(ck);
                //ck = new CheckBox();
                //ck.CssClass = "btn-sm btn-danger glyphicon glyphicon-ok";
                //ck.Text = "Alergia";
                //divCheks.Controls.Add(ck);

            }
            catch (Exception ecCP)
            {
                Log.EscribeLog("Existe un error en cargarChecksPaciente: " + ecCP.Message, 3, Usuario.vchUsuario);
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
            }catch(Exception eOP)
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
    }
}