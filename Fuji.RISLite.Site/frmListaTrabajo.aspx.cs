using Fuji.RISLite.Entidades.Extensions;
using Fuji.RISLite.Entities;
using Fuji.RISLite.Site.Services;
using Fuji.RISLite.Site.Services.DataContract;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
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
                    if (Session["User"] != null && Session["lstVistas"] != null)
                    {
                        List<clsVistasUsuarios> lstVista = (List<clsVistasUsuarios>)Session["lstVistas"];
                        if (lstVista != null)
                        {
                            string vista = "frmListaTrabajo.aspx";
                            if (lstVista.Any(x => x.vchVistaIdentificador == vista))
                            {
                                Usuario = (clsUsuario)Session["User"];
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
                AgendaRequest request = new AgendaRequest();
                request.mdlUser = Usuario;
                request.mdlagenda.intSitioID = idsitio;
                lstTec = RisService.getListaDeTrabajo(request);
                if (lstTec != null)
                {
                    if (lstTec.Count > 0)
                    {
                        lstTec = lstTec.Where(x => x.datFechaInicio.Day == DateTime.Today.Day && x.datFechaInicio.Month == DateTime.Today.Month && x.datFechaInicio.Year == DateTime.Today.Year && x.intEstatusID == 2).ToList();
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

            }
            catch(Exception eDB)
            {
                Log.EscribeLog("Existe un error en GV_ListaTrabajo_RowDataBound: " + eDB.Message, 3, Usuario.vchUsuario);
            }
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