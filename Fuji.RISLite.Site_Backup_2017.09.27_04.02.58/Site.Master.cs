using Fuji.RISLite.Entidades.Extensions;
using Fuji.RISLite.Entities;
using Fuji.RISLite.Site.Services;
using Fuji.RISLite.Site.Services.DataContract;
using System;
using System.Collections.Generic;
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
        private static List<clsVistasUsuarios> lstVistas = new List<clsVistasUsuarios>();

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
                                lblUser.Text = response.mdlUser.vchNombre;
                                Session["User"] = response.mdlUser;
                                Session["lstVistas"] = response.lstVistas;
                                usuario = response.mdlUser;
                                lstVistas = response.lstVistas;
                                configUser(response.mdlUser.intTipoUsuario, lstVistas);
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
                Log.EscribeLog("Existe un error en PageLoad de SiteMaster: " + ePL.Message, 3, "");
            }
        }

        private void configUser(int intTipoUsuario, List<clsVistasUsuarios> lstView)
        {
            try
            {
                btnShort1.Attributes.Remove("href");
                btnShort2.Attributes.Remove("href");
                btnShort3.Attributes.Remove("href");
                btnShort4.Attributes.Remove("href");
                btnShort1.Disabled = true;
                btnShort2.Disabled = true;
                btnShort3.Disabled = true;
                btnShort4.Disabled = true;
                Menu1.Attributes.Remove("href");
                Menu2.Attributes.Remove("href");
                Menu3.Attributes.Remove("href");
                Menu4.Attributes.Remove("href");
                Menu1.Visible = false;
                Menu2.Visible = false;
                Menu3.Visible = false;
                Menu4.Visible = false;
                lblMenu1.Text = "";
                lblMenu2.Text = "";
                lblMenu3.Text = "";
                lblMenu4.Text = "";


                clsVistasUsuarios vista = new clsVistasUsuarios();
                if (lstView.Exists(x => x.vchIdentificador == "btnShort1"))
                {
                    vista = lstView.Find(x => x.vchIdentificador == "btnShort1");
                    btnShort1.Attributes.Add("href", vista.vchVistaIdentificador);
                    btnShort1.Disabled = false;
                    btnShort1.Title = vista.vchNombreVista;
                    btn1.Attributes.Remove("class");
                    btn1.Attributes.Add("class", "ace-icon " + vista.vchIconFontAwesome);
                }

                vista = new clsVistasUsuarios();
                if (lstView.Exists(x => x.vchIdentificador == "btnShort2"))
                {
                    vista = lstView.Find(x => x.vchIdentificador == "btnShort2");
                    btnShort2.Attributes.Add("href", vista.vchVistaIdentificador);
                    btnShort2.Disabled = false;
                    btnShort2.Title = vista.vchNombreVista;
                    btn2.Attributes.Remove("class");
                    btn2.Attributes.Add("class", "ace-icon " + vista.vchIconFontAwesome);
                    lblMenu2.Text = vista.vchNombreVista;
                }

                vista = new clsVistasUsuarios();
                if (lstView.Exists(x => x.vchIdentificador == "btnShort3"))
                {
                    vista = lstView.Find(x => x.vchIdentificador == "btnShort3");
                    btnShort3.Attributes.Add("href", vista.vchVistaIdentificador);
                    btnShort3.Disabled = false;
                    btnShort3.Title = vista.vchNombreVista;
                    btn3.Attributes.Remove("class");
                    btn3.Attributes.Add("class", "ace-icon " + vista.vchIconFontAwesome);
                    lblMenu3.Text = vista.vchNombreVista;
                }

                vista = new clsVistasUsuarios();
                if (lstView.Exists(x => x.vchIdentificador == "btnShort4"))
                {
                    vista = lstView.Find(x => x.vchIdentificador == "btnShort4");
                    btnShort4.Attributes.Add("href", vista.vchVistaIdentificador);
                    btnShort4.Disabled = false;
                    btnShort4.Title = vista.vchNombreVista;
                    btn4.Attributes.Remove("class");
                    btn4.Attributes.Add("class", "ace-icon " + vista.vchIconFontAwesome);
                    lblMenu4.Text = vista.vchNombreVista;
                }

                vista = new clsVistasUsuarios();
                if (lstView.Exists(x => x.vchIdentificador == "Menu1"))
                {
                    vista = lstView.Find(x => x.vchIdentificador == "Menu1");
                    Menu1.Visible = true;
                    Menu1.Title = vista.vchNombreVista;
                    Menu1.Attributes.Add("href", vista.vchVistaIdentificador);
                    imgMenu1.Attributes.Remove("class");
                    imgMenu1.Attributes.Add("class", "menu-icon " + vista.vchIconFontAwesome);
                    lblMenu1.Text = vista.vchNombreVista;
                }
                vista = new clsVistasUsuarios();
                if (lstView.Exists(x => x.vchIdentificador == "Menu2"))
                {
                    vista = lstView.Find(x => x.vchIdentificador == "Menu2");
                    Menu2.Visible = true;
                    Menu2.Title = vista.vchNombreVista;
                    Menu2.Attributes.Add("href", vista.vchVistaIdentificador);
                    imgMenu2.Attributes.Remove("class");
                    imgMenu2.Attributes.Add("class", "menu-icon " + vista.vchIconFontAwesome);
                    lblMenu2.Text = vista.vchNombreVista;
                }
                vista = new clsVistasUsuarios();
                if (lstView.Exists(x => x.vchIdentificador == "Menu3"))
                {
                    vista = lstView.Find(x => x.vchIdentificador == "Menu3");
                    Menu3.Visible = true;
                    Menu3.Title = vista.vchNombreVista;
                    Menu3.Attributes.Add("href", vista.vchVistaIdentificador);
                    imgMenu3.Attributes.Remove("class");
                    imgMenu3.Attributes.Add("class", "menu-icon " + vista.vchIconFontAwesome);
                    lblMenu3.Text = vista.vchNombreVista;
                }
                vista = new clsVistasUsuarios();
                if (lstView.Exists(x => x.vchIdentificador == "Menu4"))
                {
                    vista = lstView.Find(x => x.vchIdentificador == "Menu4");
                    Menu4.Visible = true;
                    Menu4.Title = vista.vchNombreVista;
                    Menu4.Attributes.Add("href", vista.vchVistaIdentificador);
                    imgMenu4.Attributes.Remove("class");
                    imgMenu4.Attributes.Add("class", "menu-icon " + vista.vchIconFontAwesome);
                    lblMenu4.Text = vista.vchNombreVista;
                }
            }
            catch (Exception ecU)
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