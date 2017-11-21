using Fuji.RISLite.Entidades.Extensions;
using Fuji.RISLite.Site.Services;
using Fuji.RISLite.Site.Services.DataContract;
using System;
using System.Configuration;

namespace Fuji.RISLite.Site
{
    public partial class frmLogin : System.Web.UI.Page
    {
        RisLiteService RisService = new RisLiteService();
        public string URL
        {
            get
            {
                return ConfigurationManager.AppSettings["URL"];
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                ValidaUserRequest request = new ValidaUserRequest();
                ValidaUserResponse response = new ValidaUserResponse();
                request.user = txtUsuario.Text;
                request.pass = Security.Encrypt(txtPass.Text);
                if (request != null)
                {
                    response = RisService.getLoginUser(request);
                    if(response != null)
                    {
                        if (response.Success)
                        {
                            Session["User"] = response.mdlUser;
                            Session["lstVistas"] = response.lstVistas;
                            Log.EscribeLog("Usuario logueado: " + response.mdlUser.vchUsuario, 1, "LOGIN");
                            Response.Redirect(URL + "/Default.aspx", false);
                            lblMensaje.Text = "Usuario Correcto.";
                        }
                        else
                        {
                            Log.EscribeLog("Mensaje de error: " + response.mensaje, 2, "LOGIN");
                            lblMensaje.Text = "Mensaje de error: " + response.mensaje;
                        }
                    }
                    else
                    {
                        Log.EscribeLog("Verificar la información", 2, "LOGIN");
                        lblMensaje.Text = "Verificar la información";
                    }
                }
                else
                {
                    Log.EscribeLog("Verificar la información", 2, "LOGIN");
                    lblMensaje.Text = "Verificar la información";
                }
            }
            catch(Exception ebt)
            {
                Log.EscribeLog("Existe un error en btnLogin_Click: " + ebt.Message, 3, "Login");
            }
        }
    }
}