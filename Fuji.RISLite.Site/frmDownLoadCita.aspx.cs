using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Fuji.RISLite.Entidades.Extensions;
using Fuji.RISLite.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Fuji.RISLite.Site
{
    public partial class frmDownLoadCita : System.Web.UI.Page
    {
        public static clsUsuario Usuario = new clsUsuario();
        public string URL
        {
            get
            {
                return ConfigurationManager.AppSettings["URL"];
            }
        }

        public string dbLocalServer
        {
            get
            {
                return ConfigurationManager.AppSettings["dbLocalServer"];
            }
        }

        public string dbName
        {
            get
            {
                return ConfigurationManager.AppSettings["dbName"];
            }
        }

        public string dbUser
        {
            get
            {
                return ConfigurationManager.AppSettings["dbUser"];
            }
        }

        public string dbPass
        {
            get
            {
                return ConfigurationManager.AppSettings["dbPass"];
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                String var = "";
                if (!IsPostBack)
                {
                    if (Session["User"] != null)
                    {
                        Usuario = (clsUsuario)Session["User"];
                        if (Usuario != null)
                        {
                            if (Security.ValidateToken(Usuario.Token, Usuario.intUsuarioID.ToString(), Usuario.vchUsuario))
                            {
                                if (Request.QueryString.Count > 0)
                                {
                                    string intCitaiD = Security.Decrypt(Request.QueryString["appointment"].ToString());
                                    if (intCitaiD != "")
                                    {
                                        int intCita = 0;
                                        int.TryParse(intCitaiD, out intCita);
                                        ReportDocument crystalReport = new ReportDocument();
                                        Log.EscribeLog("Inicio Carga del Reporte.", 1, Usuario.vchUsuario);
                                        crystalReport.Load(Server.MapPath("~/Data/rptCitaReporte.rpt"));
                                        //string ParameterName = "intCitaID";
                                        //object val = intCitaID;
                                        //ParameterValues prms;
                                        //ParameterDiscreteValue prm = new ParameterDiscreteValue();
                                        //prms = crystalReport.DataDefinition.ParameterFields[ParameterName].CurrentValues;
                                        //prm.Value = val;
                                        //prms.Add(prm);
                                        //crystalReport.DataDefinition.ParameterFields[ParameterName].ApplyCurrentValues(prms);
                                        crystalReport.SetParameterValue("@intCitaID", intCita);
                                        //crystalReport.SetParameterValue(0, intCitaID);
                                        crystalReport.SetDatabaseLogon(dbUser, dbPass, dbLocalServer, dbName);
                                        Log.EscribeLog("Inyeccion de login.", 1, Usuario.vchUsuario);
                                        System.IO.MemoryStream oStream = new System.IO.MemoryStream();
                                        var stream = crystalReport.ExportToStream(ExportFormatType.PortableDocFormat);
                                        crystalReport.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "Cita_" + "." + intCitaiD + ".pdf");
                                        Log.EscribeLog("Formacion del reporte.", 1, Usuario.vchUsuario);
                                        //pdfAtt = new Attachment(stream, "Cita_" + intCitaID + ".pdf");
                                        Response.Buffer = true;
                                        Response.Clear();
                                        Response.ContentType = "application/pdf";
                                        Response.AddHeader("content-disposition", "attachment; filename=Cita_" + "." + intCitaiD + ".pdf");
                                        //Response.BinaryWrite(stream); // create the file
                                        Response.Flush(); // send it to the client to download
                                        Log.EscribeLog("Carga del Reporte.", 1, Usuario.vchUsuario);
                                    }
                                    else
                                    {
                                        lblError.Text = "No se pudo encontrar la cita.";
                                    }
                                }
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
                Log.EscribeLog("Existe un error en Page_Load de frmDownLoadCita: " + ePL.Message, 3, "");
                Log.EscribeLog("Existe un error en Page_Load de frmDownLoadCita: " + ePL.InnerException, 1, Usuario.vchUsuario);
            }
        }
    }
}