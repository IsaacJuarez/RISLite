using Fuji.RISLite.Entidades.Extensions;
using Fuji.RISLite.Entities;
using Fuji.RISLite.Site.Services;
using Fuji.RISLite.Site.Services.DataContract;
using System;
using System.Configuration;
using System.IO;
using System.Web.UI;
using Telerik.Web.UI;

namespace Fuji.RISLite.Site
{
    public partial class frmUsuario : System.Web.UI.Page
    {
        public string URL
        {
            get
            {
                return ConfigurationManager.AppSettings["URL"];
            }
        }
        RisLiteService RisService = new RisLiteService();
        public static clsUsuario Usuario = new clsUsuario();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                String var = "";
                //Validar Token
                if (!IsPostBack)
                {
                    if (Session["User"] != null && Session["lstVistas"] != null)
                    {
                        Usuario = (clsUsuario)Session["User"];
                        if (Usuario != null)
                        {
                            if (Security.ValidateToken(Usuario.Token, Usuario.intUsuarioID.ToString(), Usuario.vchUsuario))
                            {
                                cargarPerfil(Usuario);
                            }
                            else
                            {
                                var = Security.Encrypt("4");
                                Response.Redirect(URL + "/frmSalir.aspx?var=" + var);
                            }
                        }
                        else
                        {
                            var = Security.Encrypt("1");
                            Response.Redirect(URL + "/frmSalir.aspx?var=" + var);
                        }
                    }
                    else
                    {
                        var = Security.Encrypt("1");
                        Response.Redirect(URL + "/frmSalir.aspx?var=" + var);
                    }
                }
            }
            catch (Exception ePL)
            {
                Log.EscribeLog("Existe un error en Page_Load de frmListaTrabajo: " + ePL.Message, 3, "");
            }
        }

        private void cargarPerfil(clsUsuario usuario)
        {
            try
            {
                txtNameUser.Text = usuario.vchNombre;
                lblPassword.Text = usuario.vchPassword;
                //imgUser.ImageUrl = "/User/" + usuario.vchRutaIcono;
                preview.ImageUrl = "/Users/" + usuario.vchRutaIcono;
            }
            catch (Exception eCP)
            {
                Log.EscribeLog("Existe un error al cargar el perfil de usuario: " + eCP.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void btnSavePerfil_Click(object sender, EventArgs e)
        {
            try
            {
                PerfilResponse response = new PerfilResponse();
                PerfilRequest request = new PerfilRequest();
                request.mdlPerfil.intUsuarioID = Usuario.intUsuarioID;
                request.mdlPerfil.vchNombre = txtNameUser.Text;
                request.mdlPerfil.vchPassword = txtPassUser.Text == "" ? lblPassword.Text : Security.Encrypt(txtPassUser.Text);
                request.intVariableID = 1; //Solo icono
                request.mdlUser = Usuario;
                response = RisService.setPerfil(request);
                if (response != null)
                {
                    if (response.success)
                    {
                        Usuario.vchRutaIcono = request.mdlPerfil.vchRutaIcono;
                        Usuario.vchPassword = request.mdlPerfil.vchPassword;
                        Usuario.vchNombre = request.mdlPerfil.vchNombre;
                        Session["User"] = Usuario;
                        ShowMessage("Se actualizó correctamente la imagen de usuario.", MessageType.Correcto, "alert_container");
                    }
                    else
                    {
                        ShowMessage("Existe un error al cambiar la imagen: " + response.mensaje, MessageType.Error, "alert_container");
                    }
                }
                else
                {
                    ShowMessage("Existe un error al cambiar la imagen.", MessageType.Error, "alert_container");
                }
            }
            catch (Exception eFU)
            {
                ShowMessage("Existe un error: " + eFU.Message, MessageType.Error, "alert_container");
                Log.EscribeLog("Existe un error en btnSavePerfil_Click: " + eFU.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void RadFileUp_FileUploaded(object sender, Telerik.Web.UI.FileUploadedEventArgs e)
        {
            try
            {
                Stream st = e.File.InputStream;
                BinaryReader reader = new BinaryReader(e.File.InputStream);
                Byte[] data = reader.ReadBytes((int)e.File.InputStream.Length);
                preview.DataValue = data;
                string folderPath = Server.MapPath("~/Users/");
                string filename = e.File.FileName;
                var fileStream = new FileStream(folderPath + filename, FileMode.Create, FileAccess.Write);
                st.CopyTo(fileStream);
                fileStream.Dispose();
                PerfilResponse response = new PerfilResponse();
                PerfilRequest request = new PerfilRequest();
                request.mdlPerfil.intUsuarioID = Usuario.intUsuarioID;
                request.mdlPerfil.vchRutaIcono = filename;
                request.intVariableID = 2; //Solo icono
                request.mdlUser = Usuario;
                response = RisService.setPerfil(request);
                if(response!= null)
                {
                    if (response.success)
                    {
                        Usuario.vchRutaIcono = request.mdlPerfil.vchRutaIcono;
                        Session["User"] = Usuario;
                        ShowMessage("Se actualizó correctamente la imagen de usuario.", MessageType.Correcto, "alert_container");
                    }
                    else
                    {
                        ShowMessage("Existe un error al cambiar la imagen: " + response.mensaje, MessageType.Error, "alert_container");
                    }
                }
                else
                {
                    ShowMessage("Existe un error al cambiar la imagen.", MessageType.Error, "alert_container");
                }
            }
            catch (Exception eFU)
            {
                ShowMessage("Existe un error: " + eFU.Message, MessageType.Error, "alert_container");
                Log.EscribeLog("Existe un error en RadFileUp_FileUploaded: " + eFU.Message, 3, Usuario.vchUsuario);
            }
        }

        public enum MessageType { Correcto, Error, Informacion, Advertencia };

        protected void ShowMessage(string Message, MessageType type, String container)
        {
            try
            {
                Message = Message.Replace("'", " ");
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('" + Message + "','" + type + "','" + container + "');", true);
            }
            catch (Exception eSM)
            {
                throw eSM;
            }
        }

        protected void ajaxPanelFileUpload_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            try
            {
                //BinaryReader reader = new BinaryReader(e.File.InputStream);
                //Byte[] data = reader.ReadBytes((int)e.File.InputStream.Length);
                //preview.DataValue = data;
                //string folderPath = Server.MapPath("~/Users/");
                //foreach (UploadedFile f in RadFileUp.UploadedFiles)
                //{
                //    if (!Directory.Exists(folderPath))
                //    {
                //        //If Directory (Folder) does not exists. Create it.
                //        Directory.CreateDirectory(folderPath);
                //    }
                //    if (File.Exists(folderPath + f.GetName()))
                //    {
                //        File.Delete(folderPath + f.GetName());
                //    }
                //    f.SaveAs(folderPath + f.GetName(), true);
                //}

            }
            catch (Exception eFU)
            {
                Log.EscribeLog("Existe un error en RadFileUp_FileUploaded: " + eFU.Message, 3, Usuario.vchUsuario);
            }
        }
    }
}