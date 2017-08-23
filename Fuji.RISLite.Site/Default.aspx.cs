using Fuji.RISLite.Entidades.Extensions;
using System;
using System.Web;

namespace Fuji.RISLite.Site
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //Validar Token
                if (!IsPostBack)
                {
                   string strName = HttpContext.Current.User.Identity.Name.Substring(HttpContext.Current.User.Identity.Name.IndexOf(@"\") + 1);
                   //lblUser.Text = strName;
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