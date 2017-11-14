using Fuji.RISLite.Entidades.Extensions;
using Fuji.RISLite.Entities;
using Fuji.RISLite.Site.Services;
using System;
using System.Configuration;

using System.Web;
using Telerik.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Web.UI.HtmlControls;
using System.Drawing;
using System.Data.SqlClient;
using System.Web.UI;
using System.Collections.Generic;
using Fuji.RISLite.Site.Services.DataContract;
using System.Globalization;


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
                            Usuario = (clsUsuario)Session["User"];
                            if (response.Success)
                            {

                                cargarlistadetrabajo(Usuario.intSitioID);
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
                Log.EscribeLog("Existe un error en Page_Load de frmListaTrabajo: " + ePL.Message, 3, "");
            }        
        }



            private void cargarlistadetrabajo(int idsitio)
        {
            try
            {
                GV_ListaTrabajo.DataSource = null;
                List<clsListaDeTrabajo> lstTec = new List<clsListaDeTrabajo>();
                AgendaRequest request = new AgendaRequest();
                request.mdlUser = Usuario;
                request.mdlagenda.intSitioID = idsitio;
                lstTec = RisService.getListaDeTrabajo(request);
                if (lstTec != null)
                {
                    if (lstTec.Count > 0)
                    {
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

        }

        protected void GV_ListaTrabajo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }
       
        protected void GV_ListaTrabajo_RowCommand1(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int intmodalidadID = 0;

                int index = Convert.ToInt32(e.CommandArgument);
                //GridViewRow row = GV_ListaTrabajo.Rows[index];

                //ListItem item = new ListItem();
                //string celda_estatus = "";
                //celda_estatus = Server.HtmlDecode(row.Cells[5].Text);

                //clsUsuario mdl = new clsUsuario();
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
                        bandera_Actualizar = RisService.UpdateEstatus_Cita(request2, 2, index);

                        cargarlistadetrabajo(Usuario.intSitioID);

                        int rowIndex2 = int.Parse(e.CommandArgument.ToString());

                        break;
                    case "Cancelar":
                        EstatusCita request3 = new EstatusCita();
                        request3.mdlUser = Usuario;
                        bandera_Actualizar = RisService.UpdateEstatus_Cita(request3, 1, index);

                        cargarlistadetrabajo(Usuario.intSitioID);

                        int rowIndex3 = int.Parse(e.CommandArgument.ToString());
                        break;
                }
            }
            catch (Exception eRU)
            {
                Log.EscribeLog("Existe un error GV_AGENDA: " + eRU.Message, 3, Usuario.vchUsuario);
            }
        }


   
    }
}