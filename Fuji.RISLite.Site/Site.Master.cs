using Fuji.RISLite.Entidades.Extensions;
using Fuji.RISLite.Entities;
using Fuji.RISLite.Site.Services;
using Fuji.RISLite.Site.Services.DataContract;
using System;
using System.Configuration;
using System.Web;

namespace Fuji.RISLite.Site
{
    public partial class Site : System.Web.UI.MasterPage
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
        private static clsUsuario usuario = new clsUsuario();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    user = HttpContext.Current.User.Identity.Name.Substring(HttpContext.Current.User.Identity.Name.IndexOf(@"\") + 1);
                    user = "ijuarez";
                    string var = "";
                    if (user == "")
                    {
                        var = Security.Encrypt("1");
                        Response.Redirect(URL + "/frmSalir.aspx?var="+ var);
                    }
                    else
                    {
                        //validar usuario
                        ValidaUserResponse response = new ValidaUserResponse();
                        ValidaUserRequest request = new ValidaUserRequest();
                        request.user = user;
                        response = RisService.getUser(request);
                        if(response != null)
                        {
                            if (response.Success)
                            {
                                lblUser.Text = response.mdlUser.vchNombre;
                                Session["User"] = response.mdlUser;
                                usuario = response.mdlUser;
                                configUser(response.mdlUser.intTipoUsuario);
                            }
                            else
                            {
                                var = Security.Encrypt("2");
                                Response.Redirect(URL + "/frmSalir.aspx?var=" + var);
                            }
                        }
                    }
                }
            }
            catch (Exception ePL)
            {
                Log.EscribeLog("Existe un error en PageLoad de SiteMaster: " + ePL.Message,3,"");
            }
        }

        private void configUser(int intTipoUsuario)
        {
            try
            {
                switch(intTipoUsuario)
                {
                    case 1://Administrador 
                        btnShort1.Attributes.Remove("href");
                        btnShort1.Attributes.Add("href", "frmConfigAgenda.aspx");
                        btnShort1.Title = "Parámetros Agenda";
                        btn1.Attributes.Remove("class");
                        btn1.Attributes.Add("class", "ace-icon fa fa-cogs");
                        btnShort2.Attributes.Remove("href");
                        btnShort2.Attributes.Add("href", "frmConfiguraciones.aspx");
                        btnShort2.Title = "Configuración general";
                        btn2.Attributes.Remove("class");
                        btn2.Attributes.Add("class", "ace-icon fa fa-cog");
                        btnShort3.Attributes.Remove("href");
                        btnShort3.Attributes.Add("href", "frmAdminUser.aspx");
                        btnShort3.Title = "Usuarios";
                        btn3.Attributes.Remove("class");
                        btn3.Attributes.Add("class", "ace-icon fa fa-users");
                        btnShort4.Attributes.Remove("href");
                        btnShort4.Attributes.Add("href", "frmAdminCatalogo.aspx");
                        btnShort4.Title = "Catálogos";
                        btn4.Attributes.Remove("class");
                        btn4.Attributes.Add("class", "ace-icon fa fa-tags");

                        //AgregarExtras
                        break;
                    case 2://Agenda
                        btnShort1.Attributes.Remove("href");
                        btnShort1.Attributes.Add("href", "frmConfigAgenda.aspx");
                        btnShort1.Title = "Parámetros Agenda";
                        btn1.Attributes.Remove("class");
                        btn1.Attributes.Add("class", "ace-icon fa fa-cogs");
                        btnShort2.Attributes.Remove("href");
                        btnShort2.Attributes.Add("href", "frmConfiguraciones.aspx");
                        btnShort2.Title = "Configuración general";
                        btn2.Attributes.Remove("class");
                        btn2.Attributes.Add("class", "ace-icon fa fa-cog");
                        btnShort3.Attributes.Remove("href");
                        btnShort3.Attributes.Add("href", "frmAdminEquipoUsuario.aspx");
                        btnShort3.Title = "Usuarios y Equipos";
                        btn3.Attributes.Remove("class");
                        btn3.Attributes.Add("class", "ace-icon fa fa-users");
                        btnShort4.Attributes.Remove("href");
                        btnShort4.Attributes.Add("href", "frmAdminCatalogo.aspx");
                        btnShort4.Title = "Catálogos";
                        btn4.Attributes.Remove("class");
                        btn4.Attributes.Add("class", "ace-icon fa fa-tags");
                        break;
                    case 3://Tecnico
                        btnShort1.Attributes.Remove("href");
                        btnShort1.Attributes.Add("href", "frmEstadisticasTec.aspx");
                        btnShort1.Title = "Estadística";
                        btn1.Attributes.Remove("class");
                        btn1.Attributes.Add("class", "ace-icon fa fa-pie-chart");
                        btnShort2.Attributes.Remove("href");
                        btnShort2.Attributes.Add("href", "frmConfiguraciones.aspx");
                        btnShort2.Title = "Configuración general";
                        btn2.Attributes.Remove("class");
                        btn2.Attributes.Add("class", "ace-icon fa fa-cog");
                        btnShort3.Attributes.Remove("href");
                        btnShort3.Attributes.Add("href", "frmAdminUser.aspx");
                        btnShort3.Title = "Usuarios";
                        btn3.Attributes.Remove("class");
                        btn3.Attributes.Add("class", "ace-icon fa fa-users");
                        btnShort4.Attributes.Remove("href");
                        btnShort4.Attributes.Add("href", "frmAdminCatalogo.aspx");
                        btnShort4.Title = "Catálogos";
                        btn4.Attributes.Remove("class");
                        btn4.Attributes.Add("class", "ace-icon fa fa-tags");
                        break;
                }
            }
            catch(Exception ecU)
            {
                Log.EscribeLog("Existe un error en configUser: " + ecU.Message, 3, "");
            }
        }

        protected void btnAdminCatalogo_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(URL + "/frmAdminCatalogo.aspx");
            }
            catch(Exception eab)
            {
                Log.EscribeLog("Existe un error en btnAdminCatalogo_Click: " + eab.Message, 3, usuario.vchUsuario);
            }
        }

        protected void txtBusquedaPaciente_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string busqueda = "";
                busqueda = Security.Encrypt(txtBusquedaPaciente.Text);
                Response.Redirect(URL + "/frmPaciente.aspx?var=" + busqueda, false);
            }
            catch (Exception etxBP)
            {
                Log.EscribeLog("Existe un error en txtBusquedaPaciente_TextChanged: " + etxBP.Message, 3, usuario.vchUsuario);
            }
        }
    }
}