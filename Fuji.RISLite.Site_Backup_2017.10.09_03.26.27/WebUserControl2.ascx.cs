using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Fuji.RISLite.Site
{
    public partial class WebUserControl2 : System.Web.UI.UserControl
    {
        public string DBSelectedColor
        {
            get
            {
                return System.Drawing.ColorTranslator.ToHtml(RadColorPicker1.SelectedColor);
            }
            set
            {
                if (value != "")
                {
                    RadColorPicker1.SelectedColor = System.Drawing.ColorTranslator.FromHtml(value);
                }
            }
        }
    }
}