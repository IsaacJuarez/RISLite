using Fuji.RISLite.Entidades.Extensions;
using System;

namespace Fuji.RISLite.Site
{
    public partial class frmSalir : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Session.Clear();
                if (!IsPostBack)
                {
                    if (Request.QueryString.Count > 0)
                    {
                        String ID = Security.Decrypt(Request.QueryString["var"].ToString());
                        switch (ID)
                        {
                            case "1"://Sin Usuario
                                lblMensaje.Text = "Se intento iniciar sin un usuario.";
                                lblMensaje.ForeColor = System.Drawing.Color.DarkGreen;
                                break;
                            case "2"://No se encontro usuario en base de datos
                                lblMensaje.Text = "No se encontró el usuario en base de datos, favor de consultar con el administrador.";
                                lblMensaje.ForeColor = System.Drawing.Color.Red;
                                break;
                            case "3"://Salida Normal
                                lblMensaje.Text = "Se cerró correctamente la sesión.";
                                lblMensaje.ForeColor = System.Drawing.Color.DarkGreen;
                                break;
                            case "4":

                                break;
                        }
                    }
                }

            }
            catch (Exception ePL)
            {
                Log.EscribeLog("Existe un error en PageLoad de frmSalir: " + ePL.Message, 3, "");
            }
        }
    }
}