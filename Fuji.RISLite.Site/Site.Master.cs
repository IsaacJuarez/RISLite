﻿using Fuji.RISLite.Entidades.Extensions;
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
        //public static string user = "";
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
        RisLiteService RisService = new RisLiteService();
        private static clsUsuario usuario = new clsUsuario();
        private static List<clsVistasUsuarios> lstVistas = new List<clsVistasUsuarios>();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    //user = HttpContext.Current.User.Identity.Name.Substring(HttpContext.Current.User.Identity.Name.IndexOf(@"\") + 1);
                    //Log.EscribeLog("Usuario de Login: " + user, 1, "");
                    //if(debug == "1")
                    //    user = "ijuarez";
                    string var = "";
                    //if (user == "")
                    //{
                    //    var = Security.Encrypt("1");
                    //    Response.Redirect(URL + "/frmSalir.aspx?var=" + var);
                    //}
                    //else
                    //{
                    //validar usuario
                    //ValidaUserResponse response = new ValidaUserResponse();
                    //ValidaUserRequest request = new ValidaUserRequest();
                    //request.user = user;
                    //response = RisService.getUser(request);
                    //if (response != null)
                    //{
                    if (Session["UserRISAxon"] != null && Session["lstVistas"] != null)
                    {
                        usuario = (clsUsuario)Session["UserRISAxon"];
                        if (Security.ValidateToken(usuario.Token, usuario.intUsuarioID.ToString(), usuario.vchUsuario))
                        {
                            imgUser.Src = "/Users/" + usuario.vchRutaIcono;
                            imgUser.Alt = usuario.vchNombre;
                            lblUser.Text = usuario.vchNombre;
                            lstVistas = (List<clsVistasUsuarios>)Session["lstVistas"];
                            configUser(usuario.intTipoUsuario, lstVistas);
                        }
                        else
                        {
                            var = Security.Encrypt("4");
                            Response.Redirect(URL + "/frmSalir.aspx?var=" + var);
                        }
                    }
                    else
                    {
                        var = Security.Encrypt("2");
                        Response.Redirect(URL + "/frmSalir.aspx?var=" + var);
                    }
                    //}
                    //}
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
                //btnShort1.Attributes.Remove("href");
                //btnShort2.Attributes.Remove("href");
                //btnShort3.Attributes.Remove("href");
                //btnShort4.Attributes.Remove("href");
                //btnShort1.Disabled = true;
                //btnShort2.Disabled = true;
                //btnShort3.Disabled = true;
                //btnShort4.Disabled = true;
                Menu1.Attributes.Remove("href");
                Menu2.Attributes.Remove("href");
                Menu3.Attributes.Remove("href");
                Menu4.Attributes.Remove("href");
                Menu5.Attributes.Remove("href");
                Menu6.Attributes.Remove("href");
                Menu7.Attributes.Remove("href");
                Menu8.Attributes.Remove("href");
                Menu1.Visible = false;
                Menu2.Visible = false;
                Menu3.Visible = false;
                Menu4.Visible = false;
                Menu5.Visible = false;
                Menu6.Visible = false;
                Menu7.Visible = false;
                Menu8.Visible = false;
                lblMenu1.Text = "";
                lblMenu2.Text = "";
                lblMenu3.Text = "";
                lblMenu4.Text = "";
                lblMenu5.Text = "";
                lblMenu6.Text = "";
                lblMenu7.Text = "";
                lblMenu8.Text = "";


                clsVistasUsuarios vista = new clsVistasUsuarios();
                //if (lstView.Exists(x => x.vchIdentificador == "btnShort1"))
                //{
                //    vista = lstView.Find(x => x.vchIdentificador == "btnShort1");
                //    btnShort1.Attributes.Add("href", vista.vchVistaIdentificador);
                //    btnShort1.Disabled = false;
                //    btnShort1.Title = vista.vchNombreVista;
                //    btn1.Attributes.Remove("class");
                //    btn1.Attributes.Add("class", "ace-icon " + vista.vchIconFontAwesome);
                //}

                //vista = new clsVistasUsuarios();
                //if (lstView.Exists(x => x.vchIdentificador == "btnShort2"))
                //{
                //    vista = lstView.Find(x => x.vchIdentificador == "btnShort2");
                //    btnShort2.Attributes.Add("href", vista.vchVistaIdentificador);
                //    btnShort2.Disabled = false;
                //    btnShort2.Title = vista.vchNombreVista;
                //    btn2.Attributes.Remove("class");
                //    btn2.Attributes.Add("class", "ace-icon " + vista.vchIconFontAwesome);
                //    lblMenu2.Text = vista.vchNombreVista;
                //}

                //vista = new clsVistasUsuarios();
                //if (lstView.Exists(x => x.vchIdentificador == "btnShort3"))
                //{
                //    vista = lstView.Find(x => x.vchIdentificador == "btnShort3");
                //    btnShort3.Attributes.Add("href", vista.vchVistaIdentificador);
                //    btnShort3.Disabled = false;
                //    btnShort3.Title = vista.vchNombreVista;
                //    btn3.Attributes.Remove("class");
                //    btn3.Attributes.Add("class", "ace-icon " + vista.vchIconFontAwesome);
                //    lblMenu3.Text = vista.vchNombreVista;
                //}

                //vista = new clsVistasUsuarios();
                //if (lstView.Exists(x => x.vchIdentificador == "btnShort4"))
                //{
                //    vista = lstView.Find(x => x.vchIdentificador == "btnShort4");
                //    btnShort4.Attributes.Add("href", vista.vchVistaIdentificador);
                //    btnShort4.Disabled = false;
                //    btnShort4.Title = vista.vchNombreVista;
                //    btn4.Attributes.Remove("class");
                //    btn4.Attributes.Add("class", "ace-icon " + vista.vchIconFontAwesome);
                //    lblMenu4.Text = vista.vchNombreVista;
                //}

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
                vista = new clsVistasUsuarios();
                if (lstView.Exists(x => x.vchIdentificador == "Menu5"))
                {
                    vista = lstView.Find(x => x.vchIdentificador == "Menu5");
                    Menu5.Visible = true;
                    Menu5.Title = vista.vchNombreVista;
                    Menu5.Attributes.Add("href", vista.vchVistaIdentificador);
                    imgMenu5.Attributes.Remove("class");
                    imgMenu5.Attributes.Add("class", "menu-icon " + vista.vchIconFontAwesome);
                    lblMenu5.Text = vista.vchNombreVista;
                }
                vista = new clsVistasUsuarios();
                if (lstView.Exists(x => x.vchIdentificador == "Menu6"))
                {
                    vista = lstView.Find(x => x.vchIdentificador == "Menu6");
                    Menu6.Visible = true;
                    Menu6.Title = vista.vchNombreVista;
                    Menu6.Attributes.Add("href", vista.vchVistaIdentificador);
                    imgMenu6.Attributes.Remove("class");
                    imgMenu6.Attributes.Add("class", "menu-icon " + vista.vchIconFontAwesome);
                    lblMenu6.Text = vista.vchNombreVista;
                }
                vista = new clsVistasUsuarios();
                if (lstView.Exists(x => x.vchIdentificador == "Menu7"))
                {
                    vista = lstView.Find(x => x.vchIdentificador == "Menu7");
                    Menu7.Visible = true;
                    Menu7.Title = vista.vchNombreVista;
                    Menu7.Attributes.Add("href", vista.vchVistaIdentificador);
                    imgMenu7.Attributes.Remove("class");
                    imgMenu7.Attributes.Add("class", "menu-icon " + vista.vchIconFontAwesome);
                    lblMenu7.Text = vista.vchNombreVista;
                }
                vista = new clsVistasUsuarios();
                if (lstView.Exists(x => x.vchIdentificador == "Menu8"))
                {
                    vista = lstView.Find(x => x.vchIdentificador == "Menu8");
                    Menu8.Visible = true;
                    Menu8.Title = vista.vchNombreVista;
                    Menu8.Attributes.Add("href", vista.vchVistaIdentificador);
                    imgMenu8.Attributes.Remove("class");
                    imgMenu8.Attributes.Add("class", "menu-icon " + vista.vchIconFontAwesome);
                    lblMenu8.Text = vista.vchNombreVista;
                }
                vista = new clsVistasUsuarios();
                if (lstView.Exists(x => x.vchIdentificador == "Menu9"))
                {
                    vista = lstView.Find(x => x.vchIdentificador == "Menu9");
                    Menu9.Visible = true;
                    Menu9.Title = vista.vchNombreVista;
                    Menu9.Attributes.Add("href", vista.vchVistaIdentificador);
                    imgMenu9.Attributes.Remove("class");
                    imgMenu9.Attributes.Add("class", "menu-icon " + vista.vchIconFontAwesome);
                    lblMenu9.Text = vista.vchNombreVista;
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
                Session.Clear();
                Response.Redirect(URL + "/frmLogin.aspx", false);
            }
            catch (Exception ebc)
            {
                Log.EscribeLog("Existe un error al cerrar la sesion: " + ebc.Message, 3, "SALIR");
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