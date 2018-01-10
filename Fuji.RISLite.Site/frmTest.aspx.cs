using Fuji.RISLite.Entidades.Extensions;
using System;
using System.IO;

namespace Fuji.RISLite.Site
{
    public partial class frmTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                lblEncript.Text = Security.Encrypt("12");
            }
            catch(Exception ePL)
            {
                string error = ePL.Message;
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                string folderPath = Server.MapPath("~/Files/");

                //Check whether Directory (Folder) exists.
                if (!Directory.Exists(folderPath))
                {
                    //If Directory (Folder) does not exists. Create it.
                    Directory.CreateDirectory(folderPath);
                }

                //Save the File to the Directory (Folder).
                FileUpload1.SaveAs(folderPath + Path.GetFileName(FileUpload1.FileName));

                //Display the success message.
                lblMessage.Text = Path.GetFileName(FileUpload1.FileName) + " has been uploaded.";
            }
        }
    }
}