using Fuji.RISLite.Entidades.Extensions;
using Fuji.RISLite.Entities;
using Fuji.RISLite.Site.Services;
using Fuji.RISLite.Site.Services.DataContract;
using System;
using System.Configuration;
using System.Web;

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
        RisLiteService RisService = new RisLiteService();
        private static clsUsuario usuario = new clsUsuario();


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
                                usuario = response.mdlUser;
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
            catch(Exception ePL)
            {
                Log.EscribeLog("Existe un error en Page_Load de la pagina Default:" + ePL.Message, 3, "");
            }
        }
    }
}