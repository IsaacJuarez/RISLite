using Fuji.RISLite.Entidades.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Fuji.RISLite.Site
{
    public partial class frmAdminCatalogo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                String mn = "";
                mn = "asdasd";
            }
            catch(Exception ePL)
            {
                Log.EscribeLog("Existe un error en PageLoad de frmAdminCatalogo: " + ePL.Message, 2, "");
            }
        }
    }
}