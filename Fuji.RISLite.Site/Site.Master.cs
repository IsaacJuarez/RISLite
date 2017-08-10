using Fuji.RISLite.Entidades.Extensions;
using Fuji.RISLite.Site.Services;
using Fuji.RISLite.Site.Services.DataContract;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    user = HttpContext.Current.User.Identity.Name.Substring(HttpContext.Current.User.Identity.Name.IndexOf(@"\") + 1);
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

        protected void btnAdminCatalogo_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(URL + "/frmAdminCatalogo.aspx");
            }
            catch(Exception eab)
            {
                Log.EscribeLog("Existe un error en btnAdminCatalogo_Click: " + eab.Message, 3, "");
            }
        }
    }
}