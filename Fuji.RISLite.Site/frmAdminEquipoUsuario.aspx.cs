using Fuji.RISLite.Entidades.Extensions;
using Fuji.RISLite.Entities;
using Fuji.RISLite.Site.Services;
using Fuji.RISLite.Site.Services.DataContract;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.UI.HtmlControls;

namespace Fuji.RISLite.Site
{
    public partial class frmAdminEquipoUsuario : System.Web.UI.Page
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
                            createTableEquipo();
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
                Log.EscribeLog("Existe un error en Page_Load de frmAdminEquipoUsuario: " + ePL.Message, 3, "");
            }
        }

        private void createTableEquipo()
        {
            try
            {
                List<clsEquipo> lst = new List<clsEquipo>();
                EquipoRequest request = new EquipoRequest();
                request.mdlUser = Usuario;
                lst = RisService.getListaEquipos(request);
                if(lst != null)
                {
                    if(lst.Count > 0)
                    {
                        construirTabla(lst);
                    }
                }
            }
            catch(Exception ecE)
            {
                Log.EscribeLog("Existe un error al createTableEquipo: " + ecE.Message, 3, Usuario.vchUsuario);
            }
        }

        private void construirTabla(List<clsEquipo> lst)
        {
            try
            {
                string htmlTable = "";
                htmlTable = "<table id='dynamic-table' class='table table-striped table-bordered table-hover'>";
                htmlTable += "<thead><tr><th>Nombre</th><th>Modalidad</th><th>Estatus</th><th><i class='ace-icon fa fa-pencil-square-o bigger-110'></i>Editar</th></tr></thead>";
                htmlTable += "<tbody>";
                foreach(clsEquipo item in lst)
                {
                    htmlTable += "<tr><td>"+item.vchNombreEquipo + "</td><td>" + item.vchModalidad + "</td><td>" + item.bitActivo.ToString() + "</td><td><a class='green' href='#'><i class='ace-icon fa fa-pencil bigger-130'></i></a></td></tr>";
                }
                htmlTable += "</tbody>";
                htmlTable += "</table>";
                tableEquipos.Controls.Add(new System.Web.UI.LiteralControl(htmlTable));
            }
            catch(Exception ecT)
            {
                Log.EscribeLog("Existe un error en construirTabla: " + ecT.Message, 3, Usuario.vchUsuario);
            }
        }
    }
}