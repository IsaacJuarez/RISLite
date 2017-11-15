using Fuji.RISLite.Entidades.Extensions;
using Fuji.RISLite.Entities;
using Fuji.RISLite.Site.Services;
using Fuji.RISLite.Site.Services.DataContract;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace Fuji.RISLite.Site
{
    public partial class Default : System.Web.UI.Page
    {
        public static string user = "";
        int usuario_ = 1;

        public string URL
        {
            get
            {
                return ConfigurationManager.AppSettings["URL"];
            }
        }
        public string debug
        {
            get
            {
                return ConfigurationManager.AppSettings["debug"];
            }
        }

        public static clsUsuario Usuario = new clsUsuario();
        RisLiteService RisService = new RisLiteService();

        protected void Page_Init(object sender, EventArgs e)
        {

            try
            {
                //Validar Token
                if (!IsPostBack)
                {
                    user = HttpContext.Current.User.Identity.Name.Substring(HttpContext.Current.User.Identity.Name.IndexOf(@"\") + 1);
                    Log.EscribeLog("Usuario de Login: " + user, 1, "");
                    if (debug == "1")
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

                                SqlDataSource1.SelectParameters.Add("@idsitioss_", System.Data.DbType.String, Convert.ToString(Usuario.intSitioID));
                                //sqlDataSource.Parameters.Add("@LastName", System.Data.DbType.String, "Smith");
                                Session["User"] = response.mdlUser;
                                Usuario = response.mdlUser;
                                //cargarAgenda();
                                RS_Agenda.SelectedDate = DateTime.Now;
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


        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //    //Usuario_ = (clsUsuario)Session["User"];
                //    //Usuario_ = (clsUsuario)Session["User"];

                //    //Validar Token
                //    if (!IsPostBack)
                //    {
                //        user = HttpContext.Current.User.Identity.Name.Substring(HttpContext.Current.User.Identity.Name.IndexOf(@"\") + 1);
                //        user = "ijuarez";
                //        string var = "";
                //        if (user == "")
                //        {
                //            var = Security.Encrypt("1");
                //            Response.Redirect(URL + "/frmSalir.aspx?var=" + var);
                //        }
                //        else
                //        {
                //            //validar usuario
                //            ValidaUserResponse response = new ValidaUserResponse();
                //            ValidaUserRequest request = new ValidaUserRequest();
                //            request.user = user;
                //            response = RisService.getUser(request);
                //            if (response != null)
                //            {
                //                if (response.Success)
                //                {
                //                    Session["User"] = response.mdlUser;
                //                    Usuario = response.mdlUser;
                //                    //cargarAgenda();
                //                    RS_Agenda.SelectedDate = DateTime.Now;
                carga_citas();

                //                }
                //                else
                //                {
                //                    var = Security.Encrypt("2");
                //                    Response.Redirect(URL + "/frmSalir.aspx?var=" + var);
                //                }
                //            }
                //        }
                //    }
                //    else
                //    {
                //        //Enviar al login;

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
                request.mdlevento.intSitioID = Usuario.intSitioID;              


                lstTec = RisService.getListEventoCita_SoloSitio(request);
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
                request.mdlagenda.intSitioID = Usuario.intSitioID;

                //request.mdlagenda.intSitioID = usuario_;


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
                        RS_Agenda.DataBind();
                    }
                }                
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
                request.mdlagenda.intSitioID = Usuario.intSitioID;

                //request.mdlagenda.intSitioID = usuario_;                
                //lstTec = RisService.getListColorModalidad(request);
                lstTec = RisService.getListColorModalidad_Sitio(request);
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
                request.mdlagenda.intSitioID = Usuario.intSitioID;

                //request.mdlagenda.intSitioID = usuario_;
                //lstTec = RisService.getListColorModalidad(request);
                lstTec = RisService.getListColorModalidad_Sitio(request);
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