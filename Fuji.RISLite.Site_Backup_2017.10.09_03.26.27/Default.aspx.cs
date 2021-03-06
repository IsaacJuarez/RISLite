﻿using Fuji.RISLite.Entidades.Extensions;
using System;
using System.Web;
using Telerik.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Web.UI.HtmlControls;
using System.Drawing;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI;
using Fuji.RISLite.Entities;
using System.Collections.Generic;
using Fuji.RISLite.Site.Services.DataContract;
using Fuji.RISLite.Site.Services;
using System.Globalization;

namespace Fuji.RISLite.Site
{
    public partial class Default : System.Web.UI.Page
    {
        public static string user = "";

        public string URL
        {
            get
            {
                return ConfigurationManager.AppSettings["URL"];
            }
        }
        public static clsUsuario Usuario = new clsUsuario();
        RisLiteService RisService = new RisLiteService();

        protected void Page_Init(object sender, EventArgs e)
        {
            carga_citas();
            //cargarAgenda();
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //Validar Token
                if (!IsPostBack)
                {
                    user = HttpContext.Current.User.Identity.Name.Substring(HttpContext.Current.User.Identity.Name.IndexOf(@"\") + 1);
                    user = "ijuarez";
                    string var = "";
                    if (user == "")
                    {
                        var = Security.Encrypt("1");
                        Response.Redirect(URL + "/frmSalir.aspx?var=" + var);
                    }
                    else
                    {
                        //validar usuario
                        ValidaUserResponse response = new ValidaUserResponse();
                        ValidaUserRequest request = new ValidaUserRequest();
                        request.user = user;
                        response = RisService.getUser(request);
                        if (response != null)
                        {
                            if (response.Success)
                            {
                                Session["User"] = response.mdlUser;
                                Usuario = response.mdlUser;
                                //cargarAgenda();
                                carga_citas();                               
                             
                            }
                            else
                            {
                                var = Security.Encrypt("2");
                                Response.Redirect(URL + "/frmSalir.aspx?var=" + var);
                            }
                        }
                    }
                }
                else
                {
                    //Enviar al login;
                }
            }
            catch (Exception ePL)
            {
                Log.EscribeLog("Existe un error en Page_Load de la pagina Default:" + ePL.Message, 3, "");
            }
        }
      

        private void carga_citas()
        {
            try
            {
                RS_Agenda.DataSource = null;
                List<clsEventoCita> lstTec = new List<clsEventoCita>();
                CitasRequest request = new CitasRequest();
                request.mdlUser = Usuario;
                lstTec = RisService.getListEventoCita(request);
                RS_Agenda.DataSource = lstTec;
                RS_Agenda.DataBind();
            }
            catch (Exception ecU)
            {
                Log.EscribeLog("Existe un error en la carga de las citas: " + ecU.Message, 3, Usuario.vchUsuario);
            }
        }

        private void cargarAgenda()
        {
            try
            {
                RS_Agenda.DataSource = null;
                List<clsConfAgenda> lstTec = new List<clsConfAgenda>();
                AgendaRequest request = new AgendaRequest();
                request.mdlUser = Usuario;
                lstTec = RisService.getListAgenda(request);
                if (lstTec != null)
                {
                    if (lstTec.Count > 0)
                    {
                        ResourceType RT_Modalidades = new ResourceType();
                        RT_Modalidades.KeyField = "intModalidadID";
                        RT_Modalidades.Name = "vchModalidad";
                        RT_Modalidades.TextField = "vchCodigo";
                        RT_Modalidades.ForeignKeyField = "intModalidadID";
                        RS_Agenda.ResourceTypes.Add(RT_Modalidades);
                        RT_Modalidades.DataSource = lstTec;
                       
                        //List<clsEventoCita> stTec2 = new List<clsEventoCita>();
                        //CitasRequest request2 = new CitasRequest();
                        //request2.mdlUser = Usuario;
                        //stTec2 = RisService.getListEventoCita(request2);
                        //RS_Agenda.DataSource = stTec2;

                        RS_Agenda.DataBind();
                    }
                }
                //RS_Agenda.DataBind();
            }
            catch (Exception ecU)
            {
                Log.EscribeLog("Existe un error en la carga de configuracion agenda: " + ecU.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void RS_Agenda_ResourceHeaderCreated1(object sender, ResourceHeaderCreatedEventArgs e)
        {
            ////////Carga de colores e imagenes en encabezados de el scheduler de citas
            Panel ResourceImageWrapper = e.Container.FindControl("ResourceImageWrapper_Agenda") as Panel;
            ResourceImageWrapper.CssClass = "Resource" + e.Container.Resource.Key.ToString();

            System.Web.UI.WebControls.Image img = e.Container.FindControl("Imagen_Modalidad_Agenda") as System.Web.UI.WebControls.Image;
            img.ImageUrl = "images/" + e.Container.Resource.Text + ".png";

            string lstTec = "";
            try
            {
                AgendaRequest request = new AgendaRequest();
                request.mdlUser = Usuario;
                request.mdlagenda.vchCodigo = e.Container.Resource.Text;
                lstTec = RisService.getListColorModalidad(request);
                lstTec = lstTec.TrimEnd();
            }
            catch (Exception ecU)
            {
                Log.EscribeLog("Existe un error en la busqueda de color de la modalidad: " + ecU.Message, 3, Usuario.vchUsuario);
            }

            Panel myControl1 = e.Container.FindControl("Panel_Agenda") as Panel;
            myControl1.Style.Add("Background", "linear-gradient(75deg, #CCCCCC, " + lstTec + " 10px, white);");

            
        
            //DataTable dt = new DataTable();
            //try
            //{
            //    string conexion = ConfigurationManager.ConnectionStrings["BD2"].ConnectionString;
            //    using (SqlConnection conn = new SqlConnection(conexion))
            //    {
            //        string query = "SELECT vchColor FROM [tbl_CAT_Modalidad] WHERE vchCodigo = '" + e.Container.Resource.Text + "'";
            //        SqlCommand cmd = new SqlCommand(query, conn);
            //        SqlDataAdapter da = new SqlDataAdapter(cmd);
            //        da.Fill(dt);
            //    }
            //}
            //catch
            //{ }

            //foreach (DataRow campo in dt.Rows)
            //{
            //    Panel myControl1 = e.Container.FindControl("Panel_Agenda") as Panel;
            //    myControl1.Style.Add("Background", "linear-gradient(75deg, #CCCCCC, " + campo[0].ToString() + " 10px, white);");
            //}
        }

        protected void RS_Agenda_AppointmentDataBound1(object sender, SchedulerEventArgs e)
        {
            //Carga de colores en las citas que el scheduler contenga
            string lstTec = "";
            try
            {             
                AgendaRequest request = new AgendaRequest();
                request.mdlUser = Usuario;
                request.mdlagenda.vchCodigo = e.Appointment.Resources.GetResourceByType("vchCodigo").Text;
                lstTec = RisService.getListColorModalidad(request);
                lstTec = lstTec.TrimEnd();
            }
            catch (Exception ecU)
            {
                Log.EscribeLog("Existe un error en la busqueda de color de la modalidad: " + ecU.Message, 3, Usuario.vchUsuario);
            }


            Color appointmentColor = new Color();
            appointmentColor = Color.FromName(lstTec);
            e.Appointment.BackColor = appointmentColor;

         
            //DataTable dt = new DataTable();
            //try
            //{
            //    string conexion = ConfigurationManager.ConnectionStrings["BD2"].ConnectionString;
            //    using (SqlConnection conn = new SqlConnection(conexion))
            //    {
            //        string query = "SELECT vchColor FROM [tbl_CAT_Modalidad] WHERE vchCodigo = '" + e.Appointment.Resources.GetResourceByType("vchCodigo").Text + "'";
            //        SqlCommand cmd = new SqlCommand(query, conn);
            //        SqlDataAdapter da = new SqlDataAdapter(cmd);
            //        da.Fill(dt);
            //    }
            //}
            //catch
            //{ }

            //Color appointmentColor = new Color();

            //foreach (DataRow campo in dt.Rows)
            //{
            //    appointmentColor = Color.FromName(campo[0].ToString());
            //    e.Appointment.BackColor = appointmentColor;
            //}
        }

        protected void RS_Agenda_NavigationCommand2(object sender, SchedulerNavigationCommandEventArgs e)
        {
            if (e.Command == SchedulerNavigationCommand.SwitchToTimelineView)
            {
                //RS_agenda.RowHeight = 50;
            }
            else
            {
                // RS_agenda.RowHeight = 20;
            }
        }

       
    }
}