using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Fuji.RISLite.Entidades.DataBase;
using Fuji.RISLite.Entidades.Extensions;
using Fuji.RISLite.Entities;
using Fuji.RISLite.Site.Services;
using Fuji.RISLite.Site.Services.DataContract;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace Fuji.RISLite.Site
{
    public partial class frmAddDate : System.Web.UI.Page
    {
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

        public string CorreoString
        {
            get
            {
                return ConfigurationManager.AppSettings["CorreoString"];
            }
        }

        public string PassString
        {
            get
            {
                return ConfigurationManager.AppSettings["PassString"];
            }
        }


        RisLiteService RisService = new RisLiteService();
        public static clsUsuario Usuario = new clsUsuario();
        public static List<clsEstudio> lstEstudios = new List<clsEstudio>();
        public static List<stp_getCitaDisponible_Result> lstSug = new List<stp_getCitaDisponible_Result>();
        public static List<tbl_CAT_Identificacion> lstIdentificaciones = new List<tbl_CAT_Identificacion>();
        public static List<clsEstudioNuevaCita> do_stEstudios = new List<clsEstudioNuevaCita>();
        public static List<clsVarAcicionales> lstVarAdic = new List<clsVarAcicionales>();
        public static List<clsAdicionales> lstAdicionalesClinicos = new List<clsAdicionales>();
        public static List<clsAdicionales> lstAdicionalesOper = new List<clsAdicionales>();
        public static List<clsAdicionales> lstObser = new List<clsAdicionales>();
        public static int Masculino = 0;
        public static int Femenino = 0;
        public static int Mayor = 0;
        public static int Menor = 0;

        DataTable DT_Modalidad_Horario = new DataTable();



        public static bool bitEditar = false;

        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
                String var = "";
                if (Session["UserRISAxon"] != null)
                {
                    Usuario = (clsUsuario)Session["UserRISAxon"];
                    if (Security.ValidateToken(Usuario.Token, Usuario.intUsuarioID.ToString(), Usuario.vchUsuario))
                    {
                        if (Usuario != null)
                        {
                            lstEstudios = new List<clsEstudio>();
                            //lstSug = null;
                            cargaAdicionales();
                            cargaFormaDetalle();
                        }
                        else
                        {
                            var = Security.Encrypt("1");
                            Response.Redirect(URL + "/frmSalir.aspx?var=" + var);
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
            catch (Exception ePL)
            {
                Log.EscribeLog("Existe un error en Page_Load de frmAddDate: " + ePL.Message, 3, "");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                String var = "";
                if (!IsPostBack)
                {
                    //Busqueda_estudio();
                    //getEstudioDetalle_citaNueva();
                    limpiarControlesIniciales();
                    calFecIni.MinDate = DateTime.Today.Date;
                    calFecFin.MinDate = DateTime.Today.Date;
                    grvEstudios.DataSource = null;
                    grvEstudios.DataBind();
                    lstSug = null;

                    if (Session["UserRISAxon"] != null && Session["lstVistas"] != null)
                    {
                        List<clsVistasUsuarios> lstVista = (List<clsVistasUsuarios>)Session["lstVistas"];
                        if (lstVista != null)
                        {
                            string vista = "frmAddDate.aspx";
                            if (lstVista.Any(x => x.vchVistaIdentificador == vista))
                            {
                                Usuario = (clsUsuario)Session["UserRISAxon"];
                                if (Usuario != null)
                                {
                                    lstObser.Clear();
                                    pnlObservaciones.Controls.Clear();
                                    bitEditar = false;
                                    if (Request.QueryString.Count > 0)
                                    {

                                       string parametros = Security.Decrypt(Request.QueryString["var"].ToString());

                                        string[] lista_par = parametros.Split('|');

                                        if (lista_par.Count() == 2)
                                        {
                                            ModificacionCita(Request.QueryString["var"].ToString());
                                        }
                                        else
                                        {
                                            if (Request.QueryString["var"] != null)
                                                nuevaCita(Request.QueryString["var"].ToString());
                                        }
                                        
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
                                Response.Redirect(URL + "/frmSinPermiso.aspx", false);
                            }
                        }
                        else
                        {
                            Response.Redirect(URL + "/frmSinPermiso.aspx", false);
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
                Log.EscribeLog("Existe un error en Page_Load de frmAddDate: " + ePL.Message, 3, "");
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> obtenerPacienteBusqueda(string prefixText, int count)
        {
            List<string> lstPaciente = new List<string>();
            try
            {
                if (prefixText != "")
                {
                    PacienteRequest request = new PacienteRequest();
                    PacienteResponse response = new PacienteResponse();
                    RisLiteService service = new RisLiteService();
                    request.mdlUser = Usuario;
                    request.intSitioID = Usuario.intSitioID;
                    request.busqueda = prefixText;
                    response = service.getBusquedaPacientesMod(request);
                    if (response != null)
                    {
                        if (response.lstPacientes.Count > 0)
                        {
                            foreach(clsPaciente item in response.lstPacientes)
                            {
                                string cadena = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(item.vchNombre, item.intPacienteID.ToString());
                                lstPaciente.Add(cadena);
                            }
                        }
                            
                    }
                }

            }
            catch (Exception eOP)
            {
                Log.EscribeLog("Existe un error obtenerPacienteBusqueda:" + eOP.Message, 3, "");
            }
            return lstPaciente;
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> obtenerEstudioBusqueda(string prefixText, int count)
        {
            List<string> lstPaciente = new List<string>();
            try
            {
                if (prefixText != "")
                {
                    PacienteRequest request = new PacienteRequest();
                    PacienteResponse response = new PacienteResponse();
                    RisLiteService service = new RisLiteService();
                    request.mdlUser = Usuario;
                    request.busqueda = prefixText;
                    response = service.getBusquedaEstudio(request);
                    if (response != null)
                    {
                        if (response.lstCadenas.Count > 0)
                            lstPaciente = response.lstCadenas;
                    }
                }

            }
            catch (Exception eOP)
            {
                Log.EscribeLog("Existe un error obtenerEstudioBusqueda:" + eOP.Message, 3, "");
            }
            return lstPaciente;
        }

        protected void btnAddUser_Click(object sender, EventArgs e)
        {
            try
            {
                bitEditar = false;
                limpiarControlesPaciente();
                cargaFormaDetalle();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
            }
            catch (Exception eAU)
            {
                Log.EscribeLog("Existe un error en btnAddUser_Click:" + eAU.Message, 3, Usuario.vchUserAdmin);
            }
        }

        private void cargaAdicionales()
        {
            try
            {
                if (Masculino > 0 || Femenino > 0 || Mayor > 0 || Menor > 0)
                    cargaAdicionalesClinicosPac();
                else
                    cargaAdicionalesClinicos();
                cargaAdicionalesOperativos();
                cargarObservacionesPanel();
            }
            catch (Exception eca)
            {
                Log.EscribeLog("Existe un error en cargaAdicionales: " + eca.Message, 3, Usuario.vchUsuario);
            }
        }

        private void cargarObservacionesPanel()
        {
            try
            {
                if (lstObser.Count > 0)
                {
                    foreach(clsAdicionales item in lstObser)
                    {
                        string intAdicionalID = item.intAdicionalesID.ToString();
                        intAdicionalID = "lblAdicionalID" + intAdicionalID;
                        Label txtObs = new Label();
                        txtObs.ID = intAdicionalID;
                        txtObs.Text = lstAdicionalesClinicos.First(x=> x.intAdicionalesID == item.intAdicionalesID).vchNombreAdicional + ": " + item.vchObservaciones;
                        txtObs.ForeColor = System.Drawing.Color.DarkGreen;
                        txtObs.ClientIDMode = ClientIDMode.Static;
                        pnlObservaciones.Controls.Add(txtObs);
                        HtmlGenericControl gen = new HtmlGenericControl("br");
                        pnlObservaciones.Controls.Add(gen);
                    }
                }
                else
                {
                    pnlObservaciones.Controls.Clear();
                }
            }
            catch(Exception ecoP)
            {
                Log.EscribeLog("Existe un error en cargarObservacionesPanel: " + ecoP.Message, 3, Usuario.vchUsuario);
            }
        }

        private void cargaAdicionalesOperativos()
        {
            try
            {
                pnlAdiOpe.Controls.Clear();
                List<clsAdicionales> lstresponse = new List<clsAdicionales>();
                AdicionalesRequest request = new AdicionalesRequest();
                request.mdlUser = Usuario;
                request.intTipoAdicional = 2;//Operativo
                request.intSitioID = Usuario.intSitioID;
                lstresponse = RisService.getAdicionales(request).Where(x => x.bitActivo).ToList();
                if (lstresponse != null)
                {
                    if (lstresponse.Count > 0)
                    {
                        lstAdicionalesOper = lstresponse;
                        foreach (clsAdicionales item in lstresponse)
                        {
                            LinkButton lnk = new LinkButton();
                            lnk.CssClass = "btn btn-app btn-success radius-4";
                            lnk.ID = "lnk" + item.intAdicionalesID;
                            lnk.ClientIDMode = ClientIDMode.Static;
                            lnk.ToolTip = item.vchNombreAdicional;
                            HtmlGenericControl img = new HtmlGenericControl("i");
                            string imagen = "<i class='fa /imagen/ - o' aria-hidden='true'  title='/Titulo/' style='font - size:25px; '></i>";
                            img.InnerHtml = imagen.Replace("/imagen/", item.vchURLImagen).Replace("/Titulo/", item.vchNombreAdicional);
                            lnk.Controls.Add(img);
                            lnk.CommandArgument = item.vchNombreAdicional;
                            lnk.Click += onclickAdicionalOperativo;
                            pnlAdiOpe.Controls.Add(lnk);
                        }
                    }
                }
            }
            catch (Exception eca)
            {
                Log.EscribeLog("Existe un error en cargaAdicionalesOperativos: " + eca.Message, 3, Usuario.vchUsuario);
            }
        }

        private void onclickAdicionalOperativo(object sender, EventArgs e)
        {
            try
            {
                LinkButton btn = (LinkButton)sender;
                switch (btn.CommandArgument)
                {
                    case "Imprimir":
                        ShowMessage("Proceso: " + btn.CommandArgument, MessageType.Correcto, "alert_container");
                        break;
                    case "Interpretación":
                        ShowMessage("Proceso: " + btn.CommandArgument, MessageType.Correcto, "alert_container");
                        break;
                    case "Enviar Email":
                        ShowMessage("Proceso: " + btn.CommandArgument, MessageType.Correcto, "alert_container");
                        break;
                }
            }
            catch (Exception eonclik)
            {
                ShowMessage("Existe un error al realizar el proceso: " + eonclik.Message, MessageType.Error, "alert_container");
                Log.EscribeLog("Existe un error al cargar el proceso en onclickAdicionalOperativo: " + eonclik.Message, 3, Usuario.vchUsuario);
            }
        }

        private void cargaAdicionalesClinicos()
        {
            try
            {
                pnlAdiClin.Controls.Clear();
                List<clsAdicionales> lstresponse = new List<clsAdicionales>();
                AdicionalesRequest request = new AdicionalesRequest();
                request.mdlUser = Usuario;
                request.intTipoAdicional = 1;//Clinico
                request.intSitioID = Usuario.intSitioID;
                lstresponse = RisService.getAdicionales(request);
                if (lstresponse != null)
                {
                    if (lstresponse.Count > 0)
                    {
                        lstAdicionalesClinicos = lstresponse;
                        foreach (clsAdicionales item in lstresponse)
                        {
                            switch(item.intTipoBotonID)
                            {
                                case 1:
                                    break;
                                case 2:
                                    LinkButton lnkbtn = new LinkButton();
                                    lnkbtn.CssClass = "btn btn-empty";
                                    lnkbtn.ID = "radBtn" + item.intAdicionalesID;
                                    lnkbtn.BackColor = System.Drawing.Color.Transparent;
                                    lnkbtn.ToolTip = item.vchNombreAdicional;
                                    lnkbtn.ClientIDMode = ClientIDMode.Static;
                                    lnkbtn.CommandArgument = item.intAdicionalesID.ToString();
                                    Image imgTest = new Image();
                                    imgTest.ImageUrl = "Iconos/" + Usuario.intSitioID + "/" + item.vchURLImagen;
                                    imgTest.Width = 25;
                                    imgTest.Height = 25;
                                    CheckBox chk = new CheckBox();
                                    chk.Enabled = false;
                                    chk.ID = "chk" + item.intAdicionalesID;
                                    chk.ClientIDMode = ClientIDMode.Static;
                                    lnkbtn.Controls.Add(imgTest);
                                    lnkbtn.Controls.Add(chk);


                                    //RadButton btn = new RadButton();
                                    //btn.RenderMode = RenderMode.Lightweight;
                                    //btn.ToggleType = ButtonToggleType.CheckBox;
                                    //btn.CssClass = "btn btn-primary";
                                    //Literal radButtonContent = new Literal();
                                    //radButtonContent.ID = "radButtonContent";
                                    //string img = "<img src='/imagen/' />";
                                    //radButtonContent.Text = img.Replace("/imagen/", "Iconos/" + item.vchURLImagen);


                                    //btn.Controls.Add(imgTest);
                                    //RadButtonToggleState st0 = new RadButtonToggleState();
                                    //st0.CssClass = "";
                                    //btn.ToggleStates.Add(st0);
                                    //RadButtonToggleState st1 = new RadButtonToggleState();
                                    //st1.CssClass = "btn btn-empty";
                                    //btn.ToggleStates.Add(st1);
                                    //btn.ID = "radBtn" + item.intAdicionalesID;
                                    //btn.ToolTip = item.vchNombreAdicional;
                                    //btn.ClientIDMode = ClientIDMode.Static;
                                    //btn.CommandArgument = item.intAdicionalesID.ToString();
                                    //btn.Checked = false;
                                    //if (item.bitObservaciones)
                                    //{
                                    //lnkbtn.Click += cargarObservaciones;
                                    //}
                                    //pnlAdiClin.Controls.Add(btn);
                                    lnkbtn.Click += new EventHandler(linkButton_Click);
                                    pnlAdiClin.Controls.Add(lnkbtn);
                                    break;
                                case 3:
                                    break;
                            }
                            
                        }
                    }
                }

            }
            catch (Exception eca)
            {
                Log.EscribeLog("Existe un error en cargaAdicionalesClinicos: " + eca.Message, 3, Usuario.vchUsuario);
            }
        }

        private void cargaAdicionalesClinicosPac()
        {
            try
            {
                pnlAdiClin.Controls.Clear();
                List<clsAdicionales> lstresponse = new List<clsAdicionales>();
                AdicionalesRequest request = new AdicionalesRequest();
                request.mdlUser = Usuario;
                request.intTipoAdicional = 1;//Clinico
                request.intSitioID = Usuario.intSitioID;
                request.intMasculino = Masculino;
                request.intFemenino = Femenino;
                request.intMayor = Mayor;
                request.intMenor = Menor;
                lstresponse = RisService.getAdicionalesPac(request);
                if (lstresponse != null)
                {
                    if (lstresponse.Count > 0)
                    {
                        lstAdicionalesClinicos = lstresponse;
                        foreach (clsAdicionales item in lstresponse)
                        {
                            switch (item.intTipoBotonID)
                            {
                                case 1:
                                    break;
                                case 2:
                                    LinkButton lnkbtn = new LinkButton();
                                    lnkbtn.CssClass = "btn btn-empty";
                                    lnkbtn.ID = "radBtn" + item.intAdicionalesID;
                                    lnkbtn.BackColor = System.Drawing.Color.Transparent;
                                    lnkbtn.ToolTip = item.vchNombreAdicional;
                                    lnkbtn.ClientIDMode = ClientIDMode.Static;
                                    lnkbtn.CommandArgument = item.intAdicionalesID.ToString();
                                    Image imgTest = new Image();
                                    imgTest.ImageUrl = "Iconos/" + Usuario.intSitioID + "/" + item.vchURLImagen;
                                    imgTest.Width = 25;
                                    imgTest.Height = 25;
                                    CheckBox chk = new CheckBox();
                                    chk.Enabled = false;
                                    chk.ID = "chk" + item.intAdicionalesID;
                                    chk.ClientIDMode = ClientIDMode.Static;
                                    lnkbtn.Controls.Add(imgTest);
                                    lnkbtn.Controls.Add(chk);


                                    //RadButton btn = new RadButton();
                                    //btn.RenderMode = RenderMode.Lightweight;
                                    //btn.ToggleType = ButtonToggleType.CheckBox;
                                    //btn.CssClass = "btn btn-primary";
                                    //Literal radButtonContent = new Literal();
                                    //radButtonContent.ID = "radButtonContent";
                                    //string img = "<img src='/imagen/' />";
                                    //radButtonContent.Text = img.Replace("/imagen/", "Iconos/" + item.vchURLImagen);


                                    //btn.Controls.Add(imgTest);
                                    //RadButtonToggleState st0 = new RadButtonToggleState();
                                    //st0.CssClass = "";
                                    //btn.ToggleStates.Add(st0);
                                    //RadButtonToggleState st1 = new RadButtonToggleState();
                                    //st1.CssClass = "btn btn-empty";
                                    //btn.ToggleStates.Add(st1);
                                    //btn.ID = "radBtn" + item.intAdicionalesID;
                                    //btn.ToolTip = item.vchNombreAdicional;
                                    //btn.ClientIDMode = ClientIDMode.Static;
                                    //btn.CommandArgument = item.intAdicionalesID.ToString();
                                    //btn.Checked = false;
                                    //if (item.bitObservaciones)
                                    //{
                                    //lnkbtn.Click += cargarObservaciones;
                                    //}
                                    //pnlAdiClin.Controls.Add(btn);
                                    lnkbtn.Click += new EventHandler(linkButton_Click);
                                    pnlAdiClin.Controls.Add(lnkbtn);
                                    break;
                                case 3:
                                    break;
                            }

                        }
                    }
                }

            }
            catch (Exception eca)
            {
                Log.EscribeLog("Existe un error en cargaAdicionalesClinicos: " + eca.Message, 3, Usuario.vchUsuario);
            }
        }

        private void linkButton_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton rbt = (LinkButton)sender;
                lblTitObs.Text = rbt.ToolTip;
                hfintAdicionalID.Value = rbt.CommandArgument;
                CheckBox chk = new CheckBox();
                chk = (CheckBox)rbt.FindControl("chk" + rbt.CommandArgument);
                if (chk != null)
                {
                    chk.Checked = !chk.Checked;
                }
                if (chk.Checked)
                {
                    if (lstAdicionalesClinicos.First(x => x.intAdicionalesID == Convert.ToInt32(rbt.CommandArgument)).bitObservaciones)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalObs", "$('#modalObs').modal();", true);
                    }
                    else
                    {
                        string intAdicionalID = hfintAdicionalID.Value;
                        intAdicionalID = "lblAdicionalID" + intAdicionalID;
                        Label obs = (Label)pnlObservaciones.FindControl(intAdicionalID);
                        if (obs != null)
                        {
                            pnlObservaciones.Controls.Remove(obs);
                        }
                        lstObser.RemoveAll(x => x.intAdicionalesID == Convert.ToInt32(hfintAdicionalID.Value));
                    }
                }
                else
                {
                    if (lstAdicionalesClinicos.First(x => x.intAdicionalesID == Convert.ToInt32(rbt.CommandArgument)).bitObservaciones)
                    {
                        string intAdicionalID = hfintAdicionalID.Value;
                        intAdicionalID = "lblAdicionalID" + intAdicionalID;
                        Label obs = (Label)pnlObservaciones.FindControl(intAdicionalID);
                        if (obs != null)
                        {
                            pnlObservaciones.Controls.Remove(obs);
                        }
                        lstObser.RemoveAll(x => x.intAdicionalesID == Convert.ToInt32(hfintAdicionalID.Value));
                    }
                }
            }
            catch (Exception eObs)
            {
                Log.EscribeLog("Existe un error en cargarObservaciones: " + eObs.Message, 3, Usuario.vchUsuario);
            }
        }

        private void cargarObservaciones(object sender, EventArgs e)
        {
            try
            {
                LinkButton rbt = (LinkButton)sender;
                lblTitObs.Text = rbt.ToolTip;
                hfintAdicionalID.Value = rbt.CommandArgument;
                //rbt.Controls.Contains
                //if (rbt.Checked)
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalObs", "$('#modalObs').modal();", true);
                //}
                //else
                //{
                //    string intAdicionalID = hfintAdicionalID.Value;
                //    intAdicionalID = "lblAdicionalID" + intAdicionalID;
                //    Label obs = (Label)pnlObservaciones.FindControl(intAdicionalID);
                //    if (obs != null)
                //    {
                //        pnlObservaciones.Controls.Remove(obs);
                //    }
                //    lstObser.RemoveAll(x => x.intAdicionalesID == Convert.ToInt32(hfintAdicionalID.Value));
                //}
            }
            catch (Exception eObs)
            {
                Log.EscribeLog("Existe un error en cargarObservaciones: " + eObs.Message, 3, Usuario.vchUsuario);
            }
        }

        private void cargaFormaDetalle()
        {
            try
            {
                cargaListagenero();
                cargaVariablesAdicionales();
                cargaIdentificaciones();
                lblIDs.Visible = false;
                btnEditPaciente.Visible = false;
            }
            catch (Exception ecF)
            {
                Log.EscribeLog("Existe un error en cargaFormaDetalle: " + ecF.Message, 3, Usuario.vchUsuario);
            }
        }

        private void cargaIdentificaciones()
        {
            try
            {
                pnlIDContenido.Controls.Clear();
                VarAdicionalRequest request = new VarAdicionalRequest();
                request.mdlUser = Usuario;
                request.intSitioID = Usuario.intSitioID;
                List<tbl_CAT_Identificacion> lst = new List<tbl_CAT_Identificacion>();
                lst = RisService.getVariablesAdicionalID(request);
                if (lst != null)
                {
                    if (lst.Count > 0)
                    {
                        lstIdentificaciones = lst;
                        //-----
                        System.Web.UI.WebControls.Table tb = new System.Web.UI.WebControls.Table();
                        tb.ID = "tbIden";
                        //-----
                        foreach (tbl_CAT_Identificacion item in lst)
                        {
                            //-----
                            TableRow tr = new TableRow();
                            //-----

                            //HtmlGenericControl createDiv = new HtmlGenericControl("DIV");
                            //createDiv.ID = "DivIden" + item.intIdentificacionID;
                            //createDiv.Attributes.Add("class", "form-group");
                            //createDiv.Attributes.Add("runat", "server");
                            ////createDiv.InnerHtml = " I'm a div, from code behind ";
                            //TextBox txt = new TextBox();
                            //txt.ID = "txtIden" + item.intIdentificacionID;
                            //txt.Attributes.Add("placeholder", item.vchNombreId);
                            //txt.CssClass = "form-control col-xs-10 col-sm-5";
                            //txt.ClientIDMode = ClientIDMode.Static;
                            //Label lbl = new Label();
                            //lbl.Text = item.vchNombreId;
                            //lbl.ID = "lbl" + item.vchNombreId;
                            //lbl.AssociatedControlID = "txtIden" + item.intIdentificacionID;
                            //lbl.Attributes.Add("class", "col-sm-3 control-label no-padding-right");
                            //HtmlGenericControl createDivText = new HtmlGenericControl();
                            //createDivText.Attributes.Add("class", "col-sm-9");
                            //createDivText.Controls.Add(txt);
                            //createDiv.Controls.Add(lbl);
                            //createDiv.Controls.Add(createDivText);
                            //divIDContenido.Controls.Add(createDiv);
                            ////pnlIDContenido.Controls.Add(lbl);
                            ////pnlIDContenido.Controls.Add(txt);



                            //-----
                            TextBox txt = new TextBox();
                            txt.ID = "txtIden" + item.intIdentificacionID;
                            txt.Attributes.Add("placeholder", item.vchNombreId);
                            txt.CssClass = "form-control col-xs-10 col-sm-5";
                            txt.ClientIDMode = ClientIDMode.Static;
                            Label lbl = new Label();
                            lbl.Text = item.vchNombreId;
                            HiddenField hf = new HiddenField();
                            hf.ID = "hf" + item.intIdentificacionID;
                            hf.Value = "";
                            hf.ClientIDMode = ClientIDMode.Static;
                            lbl.ID = "lbl" + item.vchNombreId;
                            lbl.AssociatedControlID = "txtIden" + item.intIdentificacionID;
                            lbl.Attributes.Add("class", "control-label no-padding-left");
                            lbl.Attributes.Add("width", "100%");
                            TableCell tc = new TableCell();
                            tc.Controls.Add(lbl);
                            tc.Attributes.Add("width", "40%");
                            TableCell tc2 = new TableCell();
                            tc2.Controls.Add(txt);
                            tc2.Attributes.Add("width", "60%");
                            TableCell tc3 = new TableCell();
                            tc3.Controls.Add(hf);
                            tc3.Attributes.Add("width", "10%");
                            tr.Cells.Add(tc);
                            tr.Cells.Add(tc2);
                            tr.Cells.Add(tc3);
                            tb.Rows.Add(tr);
                            tb.Attributes.Add("width", "100%");
                            //-----

                        }

                        pnlIDContenido.Controls.Add(tb);
                    }
                }
            }
            catch (Exception cvA)
            {
                Log.EscribeLog("Existe un error en cargaIdentificaciones: " + cvA.Message, 3, Usuario.vchUsuario);
            }
        }

        private void cargaIdentificaciones(List<tbl_REL_IdentificacionPaciente> lstIden)
        {
            try
            {
                pnlIDContenido.Controls.Clear();
                VarAdicionalRequest request = new VarAdicionalRequest();
                request.mdlUser = Usuario;
                request.intSitioID = Usuario.intSitioID;
                List<tbl_CAT_Identificacion> lst = new List<tbl_CAT_Identificacion>();
                lst = RisService.getVariablesAdicionalID(request);
                if (lst != null)
                {
                    if (lst.Count > 0)
                    {
                        lstIdentificaciones = lst;
                        //-----
                        System.Web.UI.WebControls.Table tb = new System.Web.UI.WebControls.Table();
                        tb.ID = "tbIden";
                        
                        //-----
                        foreach (tbl_CAT_Identificacion item in lst)
                        {
                            //-----
                            TableRow tr = new TableRow();
                            //-----
                            //-----
                            TextBox txt = new TextBox();
                            txt.ID = "txtIden" + item.intIdentificacionID;
                            txt.Attributes.Add("placeholder", item.vchNombreId);
                            txt.CssClass = "form-control col-xs-10 col-sm-5";
                            txt.ClientIDMode = ClientIDMode.Static;
                            txt.Text = lstIden.Where(x => x.intIdentificacionID == item.intIdentificacionID).First().vchValor;
                            HiddenField hf = new HiddenField();
                            hf.ID = "hf" + item.intIdentificacionID;
                            hf.Value = lstIden.Where(x => x.intIdentificacionID == item.intIdentificacionID).First().intRELIdenPacienteID.ToString();
                            hf.ClientIDMode = ClientIDMode.Static;
                            Label lbl = new Label();
                            lbl.Text = item.vchNombreId;
                            lbl.ID = "lbl" + item.vchNombreId;
                            lbl.AssociatedControlID = "txtIden" + item.intIdentificacionID;
                            lbl.Attributes.Add("class", "control-label no-padding-left");
                            lbl.Attributes.Add("width", "100%");
                            TableCell tc = new TableCell();
                            tc.Controls.Add(lbl);
                            tc.Attributes.Add("width", "40%");
                            TableCell tc2 = new TableCell();
                            tc2.Controls.Add(txt);
                            tc2.Attributes.Add("width", "50%");
                            TableCell tc3 = new TableCell();
                            tc3.Controls.Add(hf);
                            tc3.Attributes.Add("width", "10%");
                            tr.Cells.Add(tc);
                            tr.Cells.Add(tc2);
                            tr.Cells.Add(tc3);
                            tb.Rows.Add(tr);
                            tb.Attributes.Add("width", "100%");
                            //-----

                        }

                        pnlIDContenido.Controls.Add(tb);
                    }
                }
            }
            catch (Exception cvA)
            {
                Log.EscribeLog("Existe un error en cargaIdentificaciones: " + cvA.Message, 3, Usuario.vchUsuario);
            }
        }

        private void cargaVariablesAdicionales()
        {
            try
            {
                pnlDinamicoContenido.Controls.Clear();
                VarAdicionalRequest request = new VarAdicionalRequest();
                request.mdlUser = Usuario;
                request.intSitioID = Usuario.intSitioID;
                List<clsVarAcicionales> lst = new List<clsVarAcicionales>();
                lst = RisService.getVariablesAdicionalPaciente(request);
                if (lst != null)
                {
                    if (lst.Count > 0)
                    {
                        lstVarAdic = lst;
                        //-----
                        System.Web.UI.WebControls.Table tb = new System.Web.UI.WebControls.Table();
                        tb.ID = "tbAdic";
                        //-----
                        foreach (clsVarAcicionales item in lst)
                        {
                            //-----
                            TableRow tr = new TableRow();
                            //-----
                            //HtmlGenericControl createDiv = new HtmlGenericControl("DIV");
                            //createDiv.ID = "DivAdi" + item.intVariableAdiID;
                            //createDiv.Attributes.Add("class", "form-group");
                            ////createDiv.InnerHtml = " I'm a div, from code behind ";
                            //TextBox txt = new TextBox();
                            //txt.ID = "txtAdi" + item.intVariableAdiID;
                            //txt.CssClass = "form-control col-xs-10 col-sm-5";
                            //txt.Attributes.Add("placeholder", item.vchNombreVarAdi);
                            //Label lbl = new Label();
                            //lbl.Text = item.vchNombreVarAdi;
                            //lbl.ID = "lbl" + item.vchNombreVarAdi;
                            //lbl.AssociatedControlID = "txtAdi" + item.intVariableAdiID;
                            //lbl.Attributes.Add("class", "col-sm-3 control-label no-padding-right");
                            //HtmlGenericControl createDivText = new HtmlGenericControl();
                            //createDivText.Attributes.Add("class", "col-sm-9");
                            //createDivText.Controls.Add(txt);
                            //createDiv.Controls.Add(lbl);
                            //createDiv.Controls.Add(createDivText);
                            //divDinamicoContenido.Controls.Add(createDiv);
                            ////pnlDinamicoContenido.Controls.Add(lbl);
                            ////pnlDinamicoContenido.Controls.Add(txt);

                            //-----
                            TextBox txt = new TextBox();
                            txt.ID = "txtAdi" + item.intVariableAdiID;
                            txt.CssClass = "form-control col-xs-10 col-sm-5";
                            txt.Attributes.Add("placeholder", item.vchNombreVarAdi);
                            txt.Attributes.Add("width", "100%");
                            HiddenField hf = new HiddenField();
                            hf.ID = "hfVA" + +item.intVariableAdiID;
                            hf.Value = "";
                            hf.ClientIDMode = ClientIDMode.Static;
                            Label lbl = new Label();
                            lbl.Text = item.vchNombreVarAdi;
                            lbl.ID = "lbl" + item.vchNombreVarAdi;
                            lbl.AssociatedControlID = "txtAdi" + item.intVariableAdiID;
                            lbl.Attributes.Add("class", "control-label no-padding-left");
                            lbl.Attributes.Add("width", "100%");
                            TableCell tc = new TableCell();
                            tc.Controls.Add(lbl);
                            tc.Attributes.Add("width", "40%");
                            TableCell tc2 = new TableCell();
                            tc2.Controls.Add(txt);
                            tc2.Controls.Add(hf);
                            tc2.Attributes.Add("width", "60%");
                            tr.Cells.Add(tc);
                            tr.Cells.Add(tc2);
                            tb.Rows.Add(tr);
                            tb.Attributes.Add("width", "100%");
                            //-----
                        }
                        pnlDinamicoContenido.Controls.Add(tb);
                    }
                }
            }
            catch (Exception cvA)
            {
                Log.EscribeLog("Existe un error en cargaVariablesAdicionales: " + cvA.Message, 3, Usuario.vchUsuario);
            }
        }

        private void cargaVariablesAdicionales(List<clsVarAcicionales> lstVarAdi)
        {
            try
            {
                pnlDinamicoContenido.Controls.Clear();
                VarAdicionalRequest request = new VarAdicionalRequest();
                request.mdlUser = Usuario;
                request.intSitioID = Usuario.intSitioID;
                List<clsVarAcicionales> lst = new List<clsVarAcicionales>();
                lst = RisService.getVariablesAdicionalPaciente(request);
                if (lst != null)
                {
                    if (lst.Count > 0)
                    {
                        lstVarAdic = lst;
                        //-----
                        System.Web.UI.WebControls.Table tb = new System.Web.UI.WebControls.Table();
                        tb.ID = "tbAdic";
                        //-----
                        foreach (clsVarAcicionales item in lst)
                        {
                            //-----
                            TableRow tr = new TableRow();
                            //-----

                            //-----
                            TextBox txt = new TextBox();
                            txt.ID = "txtAdi" + item.intVariableAdiID;
                            txt.CssClass = "form-control col-xs-10 col-sm-5";
                            txt.Attributes.Add("placeholder", item.vchNombreVarAdi);
                            txt.Attributes.Add("width", "100%");
                            txt.Text = lstVarAdi.First(x => x.intVariableAdiID == item.intVariableAdiID).vchValorAdicional;
                            HiddenField hf = new HiddenField();
                            hf.ID = "hfVA" + +item.intVariableAdiID;
                            hf.Value = lstVarAdi.Where(x => x.intVariableAdiID == item.intVariableAdiID).First().intADIPacienteID.ToString();
                            hf.ClientIDMode = ClientIDMode.Static;
                            Label lbl = new Label();
                            lbl.Text = item.vchNombreVarAdi;
                            lbl.ID = "lbl" + item.vchNombreVarAdi;
                            lbl.AssociatedControlID = "txtAdi" + item.intVariableAdiID;
                            lbl.Attributes.Add("class", "control-label no-padding-left");
                            lbl.Attributes.Add("width", "100%");
                            TableCell tc = new TableCell();
                            tc.Controls.Add(lbl);
                            tc.Attributes.Add("width", "40%");
                            TableCell tc2 = new TableCell();
                            tc2.Controls.Add(txt);
                            tc2.Controls.Add(hf);
                            tc2.Attributes.Add("width", "60%");
                            tr.Cells.Add(tc);
                            tr.Cells.Add(tc2);
                            tb.Rows.Add(tr);
                            tb.Attributes.Add("width", "100%");
                            //-----
                        }
                        pnlDinamicoContenido.Controls.Add(tb);
                    }
                }
            }
            catch (Exception cvA)
            {
                Log.EscribeLog("Existe un error en cargaVariablesAdicionales: " + cvA.Message, 3, Usuario.vchUsuario);
            }
        }

        private void cargaListagenero()
        {
            try
            {
                CatalogoRequest request = new CatalogoRequest();
                request.mdlUser = Usuario;
                List<tbl_CAT_Genero> response = new List<tbl_CAT_Genero>();
                response = RisService.getListaGenero(request);
                if (response != null)
                {
                    if (response.Count > 0)
                    {
                        ddlGeneroDet.DataSource = response;
                        ddlGeneroDet.DataTextField = "vchGenero";
                        ddlGeneroDet.DataValueField = "intGeneroID";
                        ddlGeneroDet.DataBind();
                        ddlGeneroDet.Items.Insert(0, new ListItem("Seleccionar Genero...", "0"));
                    }
                }
            }
            catch (Exception eclg)
            {
                Log.EscribeLog("Existe un error en cargaListagenero: " + eclg.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void btnEditPaciente_Click(object sender, EventArgs e)
        {
            try
            {
                if (lblIDs.Text != "")
                {
                    bitEditar = true;
                    cargarDetallePaciente(Convert.ToInt32(lblIDs.Text));
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                }
                else
                {
                    ShowMessage("Existe un error al cargar el detalle del paciente.", MessageType.Error, "alert_container");
                }
            }
            catch(Exception ebEdit)
            {
                ShowMessage("Existe un error al cargar el detalle del paciente: " + ebEdit.Message, MessageType.Error, "alert_container");
                Log.EscribeLog("Existe un error en btnEditPaciente_Click: " + ebEdit.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void grvEstudios_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Pager)
                {
                    Label lblTotalNumDePaginas = (Label)e.Row.FindControl("lblBandejaTotal");
                    lblTotalNumDePaginas.Text = grvEstudios.PageCount.ToString();

                    TextBox txtIrAlaPagina = (TextBox)e.Row.FindControl("txtBandeja");
                    txtIrAlaPagina.Text = (grvEstudios.PageIndex + 1).ToString();

                    DropDownList ddlTamPagina = (DropDownList)e.Row.FindControl("ddlBandeja");
                    ddlTamPagina.SelectedValue = grvEstudios.PageSize.ToString();
                }

                if (e.Row.RowType != DataControlRowType.DataRow)
                {
                    return;
                }

                if (e.Row.DataItem != null)
                {
                    clsEstudioNuevaCita _mdl = (clsEstudioNuevaCita)e.Row.DataItem;
                    if(_mdl.fechaInicio == DateTime.MinValue)
                    {
                        Label lblFI = (Label)e.Row.FindControl("lblFechaInicio");
                        lblFI.ForeColor = System.Drawing.Color.White;
                        lblFI.Visible = false;
                    }
                    else
                    {
                        Label lblFI = (Label)e.Row.FindControl("lblFechaInicio");
                        lblFI.ForeColor = System.Drawing.Color.DarkGreen;
                        lblFI.Visible = true;
                    }

                    if (_mdl.fechaInicio == DateTime.MinValue)
                    {
                        Label lblHI = (Label)e.Row.FindControl("lblHoraInicio");
                        lblHI.ForeColor = System.Drawing.Color.White;
                        lblHI.Visible = false;
                    }
                    else
                    {
                        Label lblHI = (Label)e.Row.FindControl("lblHoraInicio");
                        lblHI.ForeColor = System.Drawing.Color.DarkGreen;
                        lblHI.Visible = true;
                    }
                }
            }
            catch (Exception eEst)
            {
                Log.EscribeLog("Existe un error en grvEstudios_RowDataBound: " + eEst.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void grvEstudios_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {

        }

        protected void grvEstudios_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
        {

        }

        protected void grvEstudios_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            try
            {
                int inrRelModPres = 0;
                clsEstudio mdl = new clsEstudio();
                switch (e.CommandName)
                {
                    case "ElegirHorario":
                        inrRelModPres = Convert.ToInt32(e.CommandArgument.ToString());
                        Session["inrRelModPres"] = inrRelModPres;
                        EquipoRequest request = new EquipoRequest();
                        request.mdlUser = Usuario;
                        lblTituloSug.Text = "Horarios para " + do_stEstudios.First(x => x.intRelModPres == inrRelModPres).vchModalidad;
                        string IDMODALIDAD = do_stEstudios.First(x => x.intRelModPres == inrRelModPres).intModalidadID.ToString();
                        string ID_modalidad_seleccionada = do_stEstudios.First(x => x.intRelModPres == inrRelModPres).intconsecutivo_Modalidad.ToString();
                        Session["intModSug"] = do_stEstudios.First(x => x.intRelModPres == inrRelModPres).intModalidadID.ToString();
                        carga_tabla_citas(do_stEstudios.First(x => x.intRelModPres == inrRelModPres).intModalidadID.ToString(), DateTime.Today, IDMODALIDAD, ID_modalidad_seleccionada, Usuario.intSitioID);

                        //RB_antes_fecha.Enabled = true;
                        //RB_despues_feha.Enabled = true;
                        calFecIni.SelectedDate = DateTime.Today;
                        calFecFin.SelectedDate = DateTime.Today.AddDays(7);
                        btnCargarSug.Enabled = true;
                        obtenerSugerenciasDefault();
                        //request.intEquipoID = intEquipoID;
                        //EquipoResponse response = new EquipoResponse();
                        //response = RisService.setActualizaEquipo(request);
                        //if (response != null)
                        //{
                        //    if (response.Success)
                        //    {
                        //ShowMessage("Se buscará un horario para el estudio.", MessageType.Correcto, "alert_container");
                        //        //fillCat();
                        //        cargarEquipo();
                        //    }
                        //    else
                        //    {
                        //        ShowMessage("Existe un error al actualizar: " + response.Mensaje, MessageType.Error, "alert_container");
                        //    }
                        //}
                        //else
                        //{
                        //    ShowMessage("Existe un error al actualizar, favor de revisar la información. ", MessageType.Advertencia, "alert_container");
                        //}
                        break;
                    case "Eliminar":
                        inrRelModPres = Convert.ToInt32(e.CommandArgument.ToString());
                        var itemToRemove = do_stEstudios.Single(r => r.intRelModPres == inrRelModPres);
                        do_stEstudios.Remove(itemToRemove);
                        grvEstudios.DataSource = do_stEstudios;
                        grvEstudios.DataBind();
                        break;
                }
            }
            catch (Exception eRCE)
            {
                Log.EscribeLog("Existe un error en grvEstudios_RowCommand: " + eRCE.Message, 3, Usuario.vchUsuario);
            }
        }

        private void obtenerSugerenciasDefault()
        {
            try
            {
                grvSugerencia.DataSource = null;
                List<stp_getCitaDisponible_Result> response = new List<stp_getCitaDisponible_Result>();
                SugerenciasRequest request = new SugerenciasRequest();
                request = obtenerBusquedaCompleta();
                if (request != null)
                {
                    response = RisService.getSugerenciasCita(request);
                    if (response != null)
                    {
                        lstSug = response;
                        grvSugerencia.DataSource = response;
                    }
                    grvSugerencia.DataBind();
                }
            }
            catch(Exception eoS)
            {
                Log.EscribeLog("Existe un error en obtenerSugerenciasDefault: " + eoS.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void grvEstudios_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {

        }

        protected void grvEstudios_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
        {

        }

        protected void grvEstudios_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {

        }

        protected void ddlBandeja_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void txtBandeja_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtCodigoPostal_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtCodigoPostal.Text.Length >= 4)
                {
                    cargarDireccion(txtCodigoPostal.Text);
                }
                //cargaIdentificaciones();
                //cargaVariablesAdicionales();
            }
            catch (Exception eCP)
            {
                Log.EscribeLog("Existe un error en txtCodigoPostal_TextChanged: " + eCP.Message, 3, Usuario.vchUsuario);
            }
        }

        private void cargarDireccion(string codigoPostal)
        {
            try
            {
                DireccionRequest request = new DireccionRequest();
                DireccionResponse response = new DireccionResponse();
                request.mdlUser = Usuario;
                request.vchCodigoPostal = codigoPostal;
                response = RisService.getDireccionPaciente(request);
                ddlColoniaDet.DataSource = null;
                ddlColoniaDet.Items.Clear();
                ddlColoniaDet.DataBind();
                if (response != null)
                {
                    if (response.lstDireccion.Count > 0)
                    {
                        txtEstadoDet.Text = response.lstDireccion.First().vchEstado;
                        txtmunicipioDet.Text = response.lstDireccion.First().vchMunicipio;
                        ddlColoniaDet.DataSource = response.lstDireccion.OrderBy(x => x.vchColonia);
                        ddlColoniaDet.DataTextField = "vchColonia";
                        ddlColoniaDet.DataValueField = "intCodigoPostalID";
                        ddlColoniaDet.DataBind();
                        if (response.lstDireccion.Count == 1)
                        {
                            ddlColoniaDet.SelectedIndex = ddlColoniaDet.Items.IndexOf(ddlColoniaDet.Items.FindByValue(response.lstDireccion.First().intCodigoPostalID.ToString()));
                        }
                        else
                        {
                            ddlColoniaDet.Items.Insert(0, new ListItem("Seleccionar Colonia", "0"));
                        }
                    }
                }
            }
            catch(Exception eCColo)
            {
                Log.EscribeLog("Existe un error en cargarColonia: " + eCColo.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void bntAddPacienteDEt_Click(object sender, EventArgs e)
        {
            try
            {
                if (validaFechaNac())
                {
                    clsPaciente mdlPaciente = new clsPaciente();
                    clsDireccion mdlDireccion = new clsDireccion();
                    List<tbl_REL_IdentificacionPaciente> lstIden = new List<tbl_REL_IdentificacionPaciente>();
                    List<tbl_DET_PacienteDinamico> lstVar = new List<tbl_DET_PacienteDinamico>();
                    mdlPaciente = obtenerPacienteDet();
                    mdlDireccion = obtenerDireccion();
                    lstIden = obtenerIdentificaciones();
                    lstVar = obtenerVarAdicionales();
                    if (mdlPaciente != null)
                    {
                        PacienteRequest request = new PacienteRequest();
                        PacienteResponse response = new PacienteResponse();
                        request.mdlUser = Usuario;
                        request.intSitioID = Usuario.intSitioID;
                        request.mdlDireccion = mdlDireccion;
                        request.mdlPaciente = mdlPaciente;
                        request.mdlPaciente.intSitioID = Usuario.intSitioID;
                        request.lstIdent = lstIden;
                        request.lstVarAdic = lstVar;
                        if (request != null)
                        {
                            if (bitEditar)
                            {
                                Log.EscribeLog("Paciente Editado", 3, Usuario.vchUsuario);
                                if (request.mdlDireccion != null)
                                {
                                    request.mdlDireccion.intDireccionID = Convert.ToInt32(Session["intDireccionID"]);
                                }

                                if (request.mdlPaciente != null && lblIDs.Text != "")
                                {
                                    request.mdlPaciente.intPacienteID = Convert.ToInt32(lblIDs.Text);
                                }
                                response = RisService.setActualizaPaciente(request);
                                if (response != null)
                                {
                                    if (response.Success)
                                    {
                                        ShowMessage("Se actualizó correctamente el paciente." + response.Mensaje, MessageType.Correcto, "alert_container");
                                        cargarDetallePaciente(request.mdlPaciente.intPacienteID);
                                        lblIDs.Text = request.mdlPaciente.intPacienteID.ToString();
                                        lblIDs.Visible = true;
                                        btnEditPaciente.Visible = true;
                                    }
                                    else
                                    {
                                        ShowMessage("Verificar la informacion: " + response.Mensaje, MessageType.Advertencia, "alert_container");
                                    }
                                }
                                else
                                {
                                    ShowMessage("Verificar la informacion. ", MessageType.Advertencia, "alert_container");
                                }
                            }
                            else
                            {
                                Log.EscribeLog("Paciente Insertado", 3, Usuario.vchUsuario);
                                response = RisService.setPaciente(request);
                                if (response != null)
                                {
                                    if (response.Success)
                                    {
                                        ShowMessage("Se agregó correctamente el paciente." + response.Mensaje, MessageType.Correcto, "alert_container");
                                        cargarDetallePaciente(response.intPacienteID);
                                        lblIDs.Text = response.intPacienteID.ToString();
                                        lblIDs.Visible = true;
                                        btnEditPaciente.Visible = true;
                                    }
                                    else
                                    {
                                        ShowMessage("Verificar la informacion: " + response.Mensaje, MessageType.Advertencia, "alert_container");
                                    }
                                }
                                else
                                {
                                    ShowMessage("Verificar la informacion. ", MessageType.Advertencia, "alert_container");
                                }
                            }
                        }
                        else
                        {
                            ShowMessage("Verificar la informacion. ", MessageType.Advertencia, "alert_container");
                        }
                    }
                    else
                    {
                        ShowMessage("Verificar la informacion. ", MessageType.Advertencia, "alert_container");
                    }
                }
                else
                {
                    ShowMessage("Verificar el formato de fecha de naciemiento. ", MessageType.Advertencia, "alert_container");
                }
            }
            catch (Exception eAP)
            {
                ShowMessage("Existe un error al agregar el paciente: " + eAP.Message, MessageType.Error, "alert_container");
                Log.EscribeLog("Existe un error en bntAddPacienteDEt_Click: " + eAP.Message, 3, Usuario.vchUsuario);
            }
        }

        private bool validaFechaNac()
        {
            bool valido = false;
            try
            {
                DateTime dateValue;
                valido = DateTime.TryParse(txtFecNacDet.Text, out dateValue);
            }
            catch(Exception evFN)
            {
                valido = false;
                Log.EscribeLog("Existe un errro validaFechaNac: " + evFN.Message, 3, Usuario.vchUsuario);
            }
            return valido;
        }

        private List<tbl_DET_PacienteDinamico> obtenerVarAdicionales()
        {
            List<tbl_DET_PacienteDinamico> lst = new List<tbl_DET_PacienteDinamico>();
            try
            {
                if (lstIdentificaciones.Count > 0)
                {
                    foreach (clsVarAcicionales item in lstVarAdic)
                    {
                        tbl_DET_PacienteDinamico mdl = new tbl_DET_PacienteDinamico();
                        mdl.bitActivo = true;
                        mdl.datFecha = DateTime.Now;
                        mdl.intVarAdiPacienteID = item.intVariableAdiID;
                        mdl.vchUserAdmin = Usuario.vchUsuario;
                        TextBox txt = (TextBox)pnlDinamicoContenido.FindControl("tbAdic").FindControl("txtAdi" + item.intVariableAdiID);
                        HiddenField hf = (HiddenField)pnlDinamicoContenido.FindControl("tbAdic").FindControl("hfVA" + item.intVariableAdiID);
                        if (txt != null)
                            mdl.vchValorVar = txt.Text.ToUpper();
                        if (hf != null)
                            if (hf.Value != "")
                                mdl.intADIPacienteID = Convert.ToInt32(hf.Value);
                        if (mdl != null)
                        {
                            lst.Add(mdl);
                        }
                    }
                }
            }
            catch (Exception eOIden)
            {
                Log.EscribeLog("Existe un error en obtenerVarAdicionales: " + eOIden.Message, 3, Usuario.vchUsuario);
            }
            return lst;
        }

        private List<tbl_REL_IdentificacionPaciente> obtenerIdentificaciones()
        {
            List<tbl_REL_IdentificacionPaciente> lst = new List<tbl_REL_IdentificacionPaciente>();
            try
            {
                if (lstIdentificaciones.Count > 0)
                {
                    foreach (tbl_CAT_Identificacion item in lstIdentificaciones)
                    {
                        tbl_REL_IdentificacionPaciente mdl = new tbl_REL_IdentificacionPaciente();
                        mdl.bitActivo = true;
                        mdl.datFecha = DateTime.Now;
                        mdl.intIdentificacionID = item.intIdentificacionID;
                        mdl.vchUserAdmin = Usuario.vchUsuario;
                        TextBox txt = (TextBox)pnlIDContenido.FindControl("tbIden").FindControl("txtIden" + item.intIdentificacionID);
                        HiddenField hf = (HiddenField)pnlIDContenido.FindControl("tbIden").FindControl("hf" + item.intIdentificacionID);
                        if (txt!= null)
                            mdl.vchValor = txt.Text.ToUpper();
                        if (hf != null )
                            if(hf.Value != "")
                                mdl.intRELIdenPacienteID = Convert.ToInt32(hf.Value);
                        if (mdl != null)
                        {
                            lst.Add(mdl);
                        }
                    }
                }
            }
            catch (Exception eOIden)
            {
                Log.EscribeLog("Existe un error en obtenerIdentificaciones: " + eOIden.Message, 3, Usuario.vchUsuario);
            }
            return lst;
        }

        public void getEstudioDetalle_citaNueva()
        {
            int contador_tabla = 0;

            if (Lcontador.Text != "")
            {
                contador_tabla = Convert.ToInt32(Lcontador.Text);
            }

            try
            {
                AsignacionModalidadNuevaCita_Request request = new AsignacionModalidadNuevaCita_Request();
                AsignacionModalidadNuevaCita_Response response = new AsignacionModalidadNuevaCita_Response();
                request.mdlUser = Usuario;
                //EstudioRequest request = new EstudioRequest();
                //EstudioResponse response = new EstudioResponse();
                //request.mdlUser = Usuario;

                response = RisService.getEstudioDetalle_citaNueva(request, contador_tabla);

                do_stEstudios.Add(response.mdlEstudio);
            }

            catch (Exception eCP)
            {
                Log.EscribeLog("Existe un error en getEstudioDetalle_citaNueva: " + eCP.Message, 3, Usuario.vchUsuario);
            }
        }


        private void cargarDetallePaciente(int intPacienteID)
        {
            try
            {
                PacienteRequest request = new PacienteRequest();
                PacienteResponse response = new PacienteResponse();
                request.mdlUser = Usuario;
                request.intPacienteID = intPacienteID;

                if (request != null)
                {
                    response = RisService.getPacienteDetalle(request);
                    if (response != null)
                    {
                        if (response.Success)
                        {
                            bitEditar = true;
                            HFintPacienteID.Value = intPacienteID.ToString();
                            txtNombrePaciente.Text = response.mdlPaciente.vchNombre;
                            txtApellidos.Text = response.mdlPaciente.vchApellidos;
                            Date1.Text = response.mdlPaciente.datFechaNac.ToString("dd/MM/yyyy");
                            lblIDs.Text = intPacienteID.ToString();
                            lblIDs.Visible = true;
                            btnEditPaciente.Visible = true;
                            int edad = 0;
                            try
                            {
                                edad = (DateTime.Now.Year - response.mdlPaciente.datFechaNac.Year);
                            }
                            catch(Exception eeedad)
                            {
                                edad = 0;
                                Log.EscribeLog("Existe un error al obtener la edad del paciente: " + eeedad.Message, 3, Usuario.vchUsuario);
                            }

                            if (edad >= 60)
                            {
                                Mayor = 3;
                            }
                            else
                            {
                                if(edad <= 10)
                                {
                                    Menor = 4;
                                }
                            }
                            try
                            {
                                Masculino = response.mdlPaciente.intGeneroID == 1 ? 1 : 0;
                                Femenino = response.mdlPaciente.intGeneroID == 2 ? 2 : 0;
                            }
                            catch(Exception eGenero)
                            {
                                Log.EscribeLog("Existe un error al obtener el genero del paciente: " + eGenero.Message, 3, Usuario.vchUsuario);
                            }
                            cargaAdicionalesClinicosPac();

                            if (response.mdlDireccion != null)
                            {
                                fillDireccion(response.mdlDireccion);
                            }

                            if(response.mdlPaciente != null)
                            {
                                fillPaciente(response.mdlPaciente);
                            }

                            if (response.lstIden != null)
                            {
                                if(response.lstIden.Count > 0)
                                {
                                    fillIdentificaciones(response.lstIden);
                                }
                            }

                            if (response.lstVarAdi != null)
                            {
                                if (response.lstVarAdi.Count > 0)
                                {
                                    fillVarAdicionalPaciente(response.lstVarAdi);
                                }
                            }
                        }
                        else
                        {
                            ShowMessage("Existe un error al cargar al paciente: " + response.Mensaje, MessageType.Error, "alert_container");
                        }
                    }
                }
            }
            catch (Exception eCP)
            {
                Log.EscribeLog("Existe un error en cargarDetallePaciente: " + eCP.Message, 3, Usuario.vchUsuario);
            }
        }

        private void fillPaciente(clsPaciente mdlPaciente)
        {
            try
            {
                txtNombreDet.Text = mdlPaciente.vchNombre;
                txtApellidosDet.Text = mdlPaciente.vchApellidos;
                txtFecNacDet.Text = mdlPaciente.datFechaNac.ToString("dd/MM/yyyy");
                ddlGeneroDet.SelectedValue = mdlPaciente.intGeneroID.ToString();
                txtNumContactDet.Text = mdlPaciente.vchNumeroContacto;
                txtEmailDet.Text = mdlPaciente.vchEmail;
                Session["intDetPaciente"] = mdlPaciente.intDETPacienteID > 0 ? mdlPaciente.intDETPacienteID : 0;
            }
            catch (Exception eFI)
            {
                Log.EscribeLog("Existe un error en fillPaciente: " + eFI.Message, 3, Usuario.vchUsuario);
            }
        }

        private void fillDireccion(clsDireccion mdlDireccion)
        {
            try
            {
                Session["intDireccionID"] = mdlDireccion.intDireccionID;
                txtCalleDet.Text = mdlDireccion.vchCalle;
                txtNumeroDet.Text = mdlDireccion.vchNumero;
                cargarDireccion(mdlDireccion.vchCodigoPostal);
                txtCodigoPostal.Text = mdlDireccion.vchCodigoPostal;
                //txtEstadoDet.Text = mdlDireccion.vchEstado;
                //idEstadoDet.Value = mdlDireccion.intEstadoID.ToString();
                //txtmunicipioDet.Text = mdlDireccion.vchMunicipio;
                //intMunicipioID.Value = mdlDireccion.intMunicipioID.ToString();
                ddlColoniaDet.SelectedValue = mdlDireccion.intCodigoPostalID.ToString();
            }
            catch (Exception eFI)
            {
                Log.EscribeLog("Existe un error en fillDireccion: " + eFI.Message, 3, Usuario.vchUsuario);
            }
        }

        private void fillVarAdicionalPaciente(List<clsVarAcicionales> lstVarAdi)
        {
            try
            {
                cargaVariablesAdicionales(lstVarAdi);
            }
            catch (Exception eFI)
            {
                Log.EscribeLog("Existe un error en fillVarAdicionalPaciente: " + eFI.Message, 3, Usuario.vchUsuario);
            }
        }

        private void fillIdentificaciones(List<tbl_REL_IdentificacionPaciente> lstIden)
        {
            try
            {
                cargaIdentificaciones(lstIden);
            }
            catch(Exception eFI)
            {
                Log.EscribeLog("Existe un error en fillIdentificaciones: " + eFI.Message, 3, Usuario.vchUsuario);
            }
        }

        private clsDireccion obtenerDireccion()
        {
            clsDireccion mdl = new clsDireccion();
            try
            {
                mdl.intCodigoPostalID = Convert.ToInt32(ddlColoniaDet.SelectedValue);
                mdl.vchCalle = txtCalleDet.Text.ToUpper();
                mdl.vchNumero = txtNumeroDet.Text.ToUpper();
            }
            catch (Exception eoD)
            {
                Log.EscribeLog("Existe un error al obtener la dirección: " + eoD.Message, 3, Usuario.vchUsuario);
            }
            return mdl;
        }

        private clsPaciente obtenerPacienteDet()
        {
            clsPaciente mdl = new clsPaciente();
            try
            {
                Log.EscribeLog("Fecha nacimiento: " + txtFecNacDet.Text, 2, Usuario.vchUsuario);
                mdl.datFechaNac = Convert.ToDateTime(txtFecNacDet.Text);
                mdl.intGeneroID = Convert.ToInt32(ddlGeneroDet.SelectedValue.ToString());
                //mdl.intPacienteID
                mdl.vchApellidos = txtApellidosDet.Text.ToUpper();
                mdl.vchEmail = txtEmailDet.Text;
                mdl.vchNombre = txtNombreDet.Text.ToUpper();
                mdl.vchNumeroContacto = txtNumContactDet.Text.ToUpper();
                mdl.intDETPacienteID = Session["intDetPaciente"] != null ? Convert.ToInt32(Session["intDetPaciente"]) : 0;
            }
            catch (Exception eOP)
            {
                mdl = null;
                Log.EscribeLog("Existe un error en obtenerPacienteDet: " + eOP.Message, 3, Usuario.vchUsuario);
            }
            return mdl;
        }

        protected void btnCancelPacienteDet_Click(object sender, EventArgs e)
        {
            try
            {
                limpiarControlesPaciente();
            }
            catch(Exception eCPa)
            {
                Log.EscribeLog("Existe un error en btnCancelPacienteDet_Click: " + eCPa.Message, 3, Usuario.vchUsuario);
            }
        }

        private void limpiarControlesPaciente()
        {
            try
            {
                txtNombreDet.Text = "";
                txtApellidosDet.Text = "";
                txtFecNacDet.Text = "";
                ddlGeneroDet.SelectedIndex = -1;
                txtNumContactDet.Text = "";
                txtEmailDet.Text = "";
                txtCalleDet.Text = "";
                txtNumeroDet.Text = "";
                txtCodigoPostal.Text = "";
                txtEstadoDet.Text = "";
                txtmunicipioDet.Text = "";
                ddlColoniaDet.SelectedIndex = -1;
                pnlDinamicoContenido.Controls.Clear();
                pnlIDContenido.Controls.Clear();
            }
            catch (Exception eCPa)
            {
                Log.EscribeLog("Existe un error en limpiarControlesPaciente: " + eCPa.Message, 3, Usuario.vchUsuario);
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

        protected void txtBusquedaPaciente_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //int intid = Convert.ToInt32(sender.ToString());
                //string id = txtBusquedaPaciente.Text;
                //string[] paciente = txtBusquedaPaciente.Text.ToString().Split('|');
                //id = paciente[0];
                //txtBusquedaPaciente.Text = "";
                //cargarDetallePaciente(Convert.ToInt32(id));
                //txtBusquedaEstudio.Enabled = true;
            }
            catch (Exception etC)
            {
                Log.EscribeLog("Existe un error en txtBusquedaPaciente_TextChanged: " + etC.Message, 3, Usuario.vchUsuario);
            }
        }

        public void nuevaCita(string id_Codificado)
        {
            try
            {
                int id = Convert.ToInt32(Security.Decrypt(id_Codificado));
                if(id > 0)
                    cargarDetallePaciente(id);
            }
            catch(Exception eNC)
            {
                ShowMessage("Existe un error al consultar el paciente: " + eNC.Message, MessageType.Error, "alert_container");
                Log.EscribeLog("Existe un error en nuevaCita: " + eNC.Message, 3, Usuario.vchUsuario);
            }
        }

        public void ModificacionCita(string id_s)
        {
            try
            {
                HF_Modificacion_cita.Value = "1";
                int contador_tabla = 1;

                if (Lcontador.Text != "")
                {
                    contador_tabla = Convert.ToInt32(Lcontador.Text);
                }

                string parametros = Security.Decrypt(id_s);
                string[] lista_parametros = parametros.Split('|');


                AsignacionModalidadModificacionCita_Response response = new AsignacionModalidadModificacionCita_Response();
                CitaNuevaRequest_Modif_Cita request = new CitaNuevaRequest_Modif_Cita();
                request.mdlUser = Usuario;
                response = RisService.getEstudioDetalle_ModificacionCIta(request, lista_parametros[1], contador_tabla);
                HF_IDcita_Modificacion.Value = lista_parametros[1];


                cargarDetallePaciente(Convert.ToInt32(lista_parametros[0]));
                cargarEstudioGrid_modificacionCita(response);
                cargaAdicionalesClinicosPac();
            }
            catch (Exception eNC)
            {
                ShowMessage("Existe un error al consultar el paciente: " + eNC.Message, MessageType.Error, "alert_container");
                Log.EscribeLog("Existe un error en nuevaCita: " + eNC.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void txtBusquedaEstudio_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string id = txtBusquedaEstudio.Text;
                string[] estudio = txtBusquedaEstudio.Text.ToString().Split('|');
                id = estudio[0];
                txtBusquedaEstudio.Text = "";
                //cargarEstudioGrid(Convert.ToInt32(id));
                //HFcargacalendario.Value = "1";

                //HFIDModalidad_calendario.Value = id;
                //cargarEstudioGrid(Convert.ToInt32(id));
                //txtBusquedaEstudio.Text = "";
                //grvEstudios.Visible = true;
            }
            catch (Exception etC)
            {
                Log.EscribeLog("Existe un error en txtBusquedaEstudio_TextChanged: " + etC.Message, 3, Usuario.vchUsuario);
            }
        }

        private void cargarEstudioGrid_modificacionCita(AsignacionModalidadModificacionCita_Response response)
        {           
            try
            {                
                if (response != null)
                {
                    int contador_tabla = 0;
                    contador_tabla++;
                    //HF_contador_tabla_modalidad.Value = Convert.ToString(contador_tabla);
                    Lcontador.Text = Convert.ToString(contador_tabla);

                    HFIDModalidad_calendario.Value = "";

                    do_stEstudios.Clear();

                    foreach(var x in response.mdlEstudio)
                    {
                        do_stEstudios.Add(x);
                    }
                   
                    grvEstudios.DataSource = null;
                    grvEstudios.DataBind();
                    grvEstudios.DataSource = do_stEstudios;
                    grvEstudios.DataBind();
                }
            }
            catch (Exception ecEG)
            {
                Log.EscribeLog("Existe un error en cargarEstudioGrid: " + ecEG.Message, 3, Usuario.vchUsuario);
            }
        }

        private void cargarEstudioGrid(int v)
        {
            int contador_tabla = 1;

            if (Lcontador.Text != "")
            {
                contador_tabla = Convert.ToInt32(Lcontador.Text);
            }            

                try
                {
                    AsignacionModalidadNuevaCita_Request request = new AsignacionModalidadNuevaCita_Request();
                    AsignacionModalidadNuevaCita_Response response = new AsignacionModalidadNuevaCita_Response();
                    request.mdlUser = Usuario;
                    clsEstudioNuevaCita mdlEstudio = new clsEstudioNuevaCita();
                    mdlEstudio.intRelModPres = v;
                    request.mdlEstudio = mdlEstudio;
                    response = RisService.getEstudioDetalle_citaNueva(request, contador_tabla);
                    
                if (response != null)
                {
                    contador_tabla++;
                    //HF_contador_tabla_modalidad.Value = Convert.ToString(contador_tabla);
                    Lcontador.Text = Convert.ToString(contador_tabla);

                    HFIDModalidad_calendario.Value = Convert.ToString(v);
                   
                        do_stEstudios.Add(response.mdlEstudio);
                        grvEstudios.DataSource = null;
                        grvEstudios.DataBind();
                        grvEstudios.DataSource = do_stEstudios;
                        grvEstudios.DataBind();                        
                    }
                }
                catch (Exception ecEG)
                {
                    Log.EscribeLog("Existe un error en cargarEstudioGrid: " + ecEG.Message, 3, Usuario.vchUsuario);
                }                     
        }

        protected void btnAddCita_Click(object sender, EventArgs e)
        {
            try
            {
                bool paciente = validarPaciente();
                bool estudios = validarEstudios();
                bool horarios = validarHorarios();
                if (paciente)
                {
                    if (estudios)
                    {
                        if (horarios)
                        {
                            if (HF_Modificacion_cita.Value == "1")
                            {

                                clsPaciente mdlPaciete = new clsPaciente();
                                mdlPaciete.intPacienteID = Convert.ToInt32(lblIDs.Text.ToString());
                                List<clsEstudioNuevaCita> lstEst = new List<clsEstudioNuevaCita>();
                                lstEst = do_stEstudios;
                                List<clsAdicionales> lstAdi = new List<clsAdicionales>();
                                lstAdi = lstObser;
                                if (mdlPaciete != null && lstEst != null)
                                {
                                    if (mdlPaciete.intPacienteID > 0 && lstEst.Count > 0)
                                    {
                                        List<clsAdicionales> lstVarAdi = getAdicionales();
                                        CitaNuevaResponse response = new CitaNuevaResponse();
                                        CitaNuevaRequest request = new CitaNuevaRequest();
                                        request.mdlUser = Usuario;
                                        request.lstAdicionales = lstVarAdi;

                                        int total_estudios = request.lstEstudios.Count();

                                        clsEstudioNuevaCita nueva_cita = new clsEstudioNuevaCita();

                                        nueva_cita = do_stEstudios[total_estudios];

                                        request.lstEstudios = lstEst;

                                        request.mdlPaciente = mdlPaciete;
                                        if (request != null)
                                        {
                                            response = RisService.ModificacionCita(request, Convert.ToInt32(HF_IDcita_Modificacion.Value));
                                            if (response != null)
                                            {
                                                if (response.Success)
                                                {
                                                    ShowMessage("Se guardo correctamente la cita.", MessageType.Correcto, "alert_container");
                                                    limpiarControlesIniciales();
                                                    //Imprimir.
                                                    imprimirCita(Convert.ToInt32(HF_IDcita_Modificacion.Value));
                                                    //Enviar por correo.
                                                    PrepararCorreo(Convert.ToInt32(HF_IDcita_Modificacion.Value));
                                                    //Mostrar Indicaciones y restricciones.
                                                }
                                                else
                                                {
                                                    ShowMessage("Favor de verificar la información: " + response.Mensaje, MessageType.Error, "alert_container");
                                                }
                                            }
                                            else
                                            {
                                                ShowMessage("Favor de verificar la información.", MessageType.Advertencia, "alert_container");
                                            }
                                        }
                                        else
                                        {
                                            ShowMessage("Favor de verificar la información.", MessageType.Advertencia, "alert_container");
                                        }
                                    }
                                    else
                                    {
                                        ShowMessage("Favor de verificar la información del Paciente", MessageType.Advertencia, "alert_container");
                                    }
                                }

                            }

                            else
                            {

                                clsPaciente mdlPaciete = new clsPaciente();
                                mdlPaciete.intPacienteID = Convert.ToInt32(lblIDs.Text.ToString());
                                List<clsEstudioNuevaCita> lstEst = new List<clsEstudioNuevaCita>();
                                lstEst = do_stEstudios;
                                List<clsAdicionales> lstAdi = new List<clsAdicionales>();
                                lstAdi = lstObser;
                                if (mdlPaciete != null && lstEst != null)
                                {
                                    if (mdlPaciete.intPacienteID > 0 && lstEst.Count > 0)
                                    {
                                        List<clsAdicionales> lstVarAdi = getAdicionales();
                                        CitaNuevaResponse response = new CitaNuevaResponse();
                                        CitaNuevaRequest request = new CitaNuevaRequest();
                                        request.mdlUser = Usuario;
                                        request.lstAdicionales = lstVarAdi;
                                        request.lstEstudios = lstEst;
                                        request.mdlPaciente = mdlPaciete;
                                        if (request != null)
                                        {
                                            response = RisService.setCitaNueva(request);
                                            if (response != null)
                                            {
                                                if (response.Success)
                                                {
                                                    ShowMessage("Se guardo correctamente la cita.", MessageType.Correcto, "alert_container");
                                                    limpiarControlesIniciales();
                                                    //Imprimir.
                                                    imprimirCita((int)response.cita.intCitaID);
                                                    //Enviar por correo.
                                                    PrepararCorreo((int)response.cita.intCitaID);
                                                    //Mostrar Indicaciones y restricciones.
                                                }
                                                else
                                                {
                                                    ShowMessage("Favor de verificar la información: " + response.Mensaje, MessageType.Error, "alert_container");
                                                }
                                            }
                                            else
                                            {
                                                ShowMessage("Favor de verificar la información.", MessageType.Advertencia, "alert_container");
                                            }
                                        }
                                        else
                                        {
                                            ShowMessage("Favor de verificar la información.", MessageType.Advertencia, "alert_container");
                                        }
                                    }
                                    else
                                    {
                                        ShowMessage("Favor de verificar la información del Paciente", MessageType.Advertencia, "alert_container");
                                    }
                                }
                            }
                        }
                        else
                        {
                            ShowMessage("Favor de verificar los horarios.", MessageType.Advertencia, "alert_container");
                        }
                    }
                    else
                    {
                        ShowMessage("Favor de verificar los estudios.", MessageType.Advertencia, "alert_container");
                    }
                }
                else
                {
                    ShowMessage("Favor de verificar el paciente.", MessageType.Advertencia, "alert_container");
                }
            }
            catch (Exception eBC)
            {
                ShowMessage("Existe un error al agregar la cita: " + eBC.Message, MessageType.Error, "alert_container");
                Log.EscribeLog("Existe un error en btnAddCita_Click: " + eBC.Message, 3, Usuario.vchUsuario);
            }
        }

        private void imprimirCita(int intCitaId)
        {
            try
            {
                string urlabrir = "";
                string id = Security.Encrypt(intCitaId.ToString());
                urlabrir = URL + "/frmDownLoadCita.aspx?appointment=" + id;
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Cerrar", "javascript:Redirecciona('" + urlabrir + "');", true);
            }
            catch (Exception eImprimircita)
            {
                Log.EscribeLog("Existe un error en imprimirCita: " + eImprimircita, 3, Usuario.vchUsuario);
            }
        }

        private Attachment CreatePDF(int intCitaID, List<stp_getCitaReporte_Result> dataSource)
        {
            Attachment pdfAtt = null;
            try
            {
                ReportDocument crystalReport = new ReportDocument();
                crystalReport.Load(Server.MapPath("~/Data/rptCitaReporte.rpt"));
                //string ParameterName = "intCitaID";
                //object val = intCitaID;
                //ParameterValues prms;
                //ParameterDiscreteValue prm = new ParameterDiscreteValue();
                //prms = crystalReport.DataDefinition.ParameterFields[ParameterName].CurrentValues;
                //prm.Value = val;
                //prms.Add(prm);
                //crystalReport.DataDefinition.ParameterFields[ParameterName].ApplyCurrentValues(prms);
                crystalReport.SetParameterValue("@intCitaID", intCitaID);
                //crystalReport.SetParameterValue(0, intCitaID);
                crystalReport.SetDatabaseLogon(dbUser, dbPass, dbLocalServer, dbName);
                var stream = crystalReport.ExportToStream(ExportFormatType.PortableDocFormat);
                pdfAtt = new Attachment(stream, "Cita_" + intCitaID + ".pdf");
            }
            catch (Exception eCPDF)
            {
                Log.EscribeLog("Existe un error al crear el PDF: " + eCPDF.Message + " *" + eCPDF.InnerException, 3, Usuario.vchUsuario);
            }
            return pdfAtt;
        }

        private void SetDBLogonForReport(ConnectionInfo connectionInfo, ReportDocument reportDocument)
        {
            Tables tables = reportDocument.Database.Tables;
            foreach (CrystalDecisions.CrystalReports.Engine.Table table in tables)
            {
                TableLogOnInfo tableLogonInfo = table.LogOnInfo;
                tableLogonInfo.ConnectionInfo = connectionInfo;
                table.ApplyLogOnInfo(tableLogonInfo);
            }
        }

        private void PrepararCorreo(int intCitaID)
        {
            try
            {
                List<stp_getCitaReporte_Result> Citareporte = new List<stp_getCitaReporte_Result>();
                CitaReporteRequest request = new CitaReporteRequest();
                request.intCitaId = intCitaID;
                request.mdlUser = Usuario;
                Citareporte = RisService.getCitaReporte(request);
                if (Citareporte != null)
                {
                    clsCorreo correo = new clsCorreo();
                    correo.asunto = "Proxima Cita.";
                    correo.bitReporte = true;
                    correo.toEmail = Citareporte.First().vchEmail;
                    correo.vchNombrePaciente = Citareporte.First().vchNombre;
                    correo.intCitaID = intCitaID;
                    correo.htmlCorreo = obtenerMachote().Replace("SSSitio", Citareporte.First().vchNombreSitio);

                    tbl_Conf_CorreoSitio configCorreo = new tbl_Conf_CorreoSitio();
                    configCorreo = obtenerDatosCorreoSitio();
                    if (correo != null && configCorreo != null)
                    {
                        enviarCorreo(correo, configCorreo, Citareporte);
                    }
                }

            }
            catch (Exception ePC)
            {
                Log.EscribeLog("Existe un error en PrepararCorreo: " + ePC.Message, 3, Usuario.vchUsuario);
            }
        }

        private tbl_Conf_CorreoSitio obtenerDatosCorreoSitio()
        {
            tbl_Conf_CorreoSitio correo = new tbl_Conf_CorreoSitio();
            try
            {
                
                ConfigEmailResponse response = new ConfigEmailResponse();
                ConfigEmailRequest request = new ConfigEmailRequest();
                request.intSitioID = Usuario.intSitioID;
                request.mdlUser = Usuario;
                response = RisService.getConfigEmail(request);
                if(response != null)
                {
                    correo = response.mldConfigEmail;
                }
            }
            catch(Exception eoDC)
            {
                Log.EscribeLog("Existe un error en obtenerDatosCorreoSitio: " + eoDC.Message, 3, Usuario.vchUsuario);
            }
            return correo;
        }

        private string obtenerMachote()
        {
            string texto = "";
            try
            {
                if (File.Exists(Server.MapPath("~/Data/CorreoCita.txt")))
                {
                    texto = File.ReadAllText(Server.MapPath("~/Data/CorreoCita.txt"));
                }
                else
                {
                    texto = "<table width='350px' style='FONT-SIZE:11px;font-family:Tahoma,Helvetica,sans-serif;padding:2px;background-color:#fdfffe;BORDER-RIGHT:#0c922e 3px solid;BORDER-TOP:#0c922e 3px solid;BORDER-LEFT:#0c922e 3px solid;BORDER-BOTTOM:#0c922e 3px solid'>" +
                            "<tbody>" +
                            "<tr><td colspan='2'>SSSitio</td></tr><tr><td colspan='2'><hr></td></tr><tr><td colspan='2' align='center' style='background-color:#fefefe'>Nueva <span class='il'>Cita</span></td></tr>" +
                            "<tr><td colspan='2'><hr></td></tr>" +
                            "<tr><td colspan='2'>Apreciable paciente:</td></tr>" +
                            "<tr><td colspan='2'>Adjunto encontrara información de su proxima cita.</td></tr>" +
                            "<tr><td align='center' colspan='2'><font color='#014615' size='3'>FUJIFILM MEXICO</font></td></tr>" +
                            "<tr><td colspan='2'>Este correo&nbsp; electronico&nbsp; es&nbsp; confidencial, esta&nbsp; legalmente&nbsp; protegido y/o puede contener informacion privilegiada. Si usted no es su destinatario o no es alguna persona autorizada por este para recibir sus correos electronicos, NO debera usted utilizar, copiar, revelar, o&nbsp; tomar&nbsp; ninguna&nbsp; accion&nbsp; basada&nbsp; en este correo electronico o cualquier otra informacion incluida en el (incluyendo todos los documentos adjuntos). </td></tr>" +
                            "</tbody></table>";
                }
            }
            catch (Exception eoM)
            {
                texto = "<table width='350px' style='FONT-SIZE:11px;font-family:Tahoma,Helvetica,sans-serif;padding:2px;background-color:#fdfffe;BORDER-RIGHT:#0c922e 3px solid;BORDER-TOP:#0c922e 3px solid;BORDER-LEFT:#0c922e 3px solid;BORDER-BOTTOM:#0c922e 3px solid'>" +
                            "<tbody>" +
                            "<tr><td colspan='2'>SSSitio</td></tr><tr><td colspan='2'><hr></td></tr><tr><td colspan='2' align='center' style='background-color:#fefefe'>Nueva <span class='il'>Cita</span></td></tr>" +
                            "<tr><td colspan='2'><hr></td></tr>" +
                            "<tr><td colspan='2'>Apreciable paciente:</td></tr>" +
                            "<tr><td colspan='2'>Adjunto encontrara información de su proxima cita.</td></tr>" +
                            "<tr><td align='center' colspan='2'><font color='#014615' size='3'>FUJIFILM MEXICO</font></td></tr>" +
                            "<tr><td colspan='2'>Este correo&nbsp; electronico&nbsp; es&nbsp; confidencial, esta&nbsp; legalmente&nbsp; protegido y/o puede contener informacion privilegiada. Si usted no es su destinatario o no es alguna persona autorizada por este para recibir sus correos electronicos, NO debera usted utilizar, copiar, revelar, o&nbsp; tomar&nbsp; ninguna&nbsp; accion&nbsp; basada&nbsp; en este correo electronico o cualquier otra informacion incluida en el (incluyendo todos los documentos adjuntos). </td></tr>" +
                            "</tbody></table>";
                Log.EscribeLog("Existe un error al obtener el machote del correo: " + eoM.Message, 3, Usuario.vchUsuario);
            }
            return texto;
        }

        private bool enviarCorreo(clsCorreo correo, tbl_Conf_CorreoSitio configCorreo, List<stp_getCitaReporte_Result> dataSource)
        {
            bool valido = false;
            try
            {
                Log.EscribeLog("Inicio de creacion de email.", 1, Usuario.vchUsuario);
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(configCorreo.vchCorreo);
                string[] lista_correos = correo.toEmail.Split(';');

                foreach (string destino in lista_correos)
                {
                    mail.To.Add(destino);
                }
                Log.EscribeLog("Email para: " + configCorreo.vchCorreo, 1, Usuario.vchUsuario);
                mail.Subject = correo.asunto;
                mail.IsBodyHtml = true;
                mail.Body = correo.htmlCorreo;

                Log.EscribeLog("Se inicia reporte", 1, Usuario.vchUsuario);
                if (correo.bitReporte)
                {
                    try
                    {
                        mail.Attachments.Add(CreatePDF(correo.intCitaID, dataSource));
                    }
                    catch (Exception e)
                    {
                        Log.EscribeLog("Existe un error al adjuntar el pdf: " + e.Message, 3, Usuario.vchUsuario);
                    }
                }
                try
                {
                    Log.EscribeLog("Proceso de envío", 1, Usuario.vchUsuario);
                    System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
                    //string correoSit = Security.Decrypt(CorreoString);
                    //string passSit = Security.Decrypt(PassString);
                    //smtp.Credentials = new System.Net.NetworkCredential(correoSit, passSit);
                    smtp.Credentials = new System.Net.NetworkCredential(configCorreo.vchCorreo, configCorreo.vchPassword);
                    smtp.Host = configCorreo.vchHost != "" ? configCorreo.vchHost : "smtp.gmail.com";
                    smtp.Port = (int)configCorreo.intPort > 0 ? (int)configCorreo.intPort : 587;
                    smtp.EnableSsl = (bool)configCorreo.BitEnableSsl;
                    smtp.Send(mail);
                    valido = true;
                }
                catch (Exception except)
                {
                    valido = false;
                    Log.EscribeLog("Existe un error al intentar enviar el correo: " + except.Message, 3, Usuario.vchUsuario);
                }
            }
            catch (Exception eeC)
            {
                Log.EscribeLog("Existe un error en enviarCorreo: " + eeC.Message, 3, Usuario.vchUsuario);
                valido = false;
            }
            return valido;
        }

        private List<clsAdicionales> getAdicionales()
        {
            List<clsAdicionales> lst = new List<clsAdicionales>();
            try
            {
                if(lstAdicionalesClinicos!= null)
                {
                    if(lstAdicionalesClinicos.Count > 0)
                    {
                        foreach(clsAdicionales item in lstAdicionalesClinicos)
                        {
                            LinkButton btn = new LinkButton();
                            btn = (LinkButton)pnlAdiClin.FindControl("radBtn" + item.intAdicionalesID);
                            if (btn != null)
                            {
                                CheckBox chk = new CheckBox();
                                chk = (CheckBox)btn.FindControl("chk" + btn.CommandArgument);
                                if (chk.Checked)
                                {
                                    clsAdicionales mdl = new clsAdicionales();
                                    mdl.intAdicionalesID = item.intAdicionalesID;
                                    if(lstObser.Any(x=> x.intAdicionalesID == item.intAdicionalesID))
                                        mdl.vchObservaciones = lstObser.First(x => x.intAdicionalesID == item.intAdicionalesID).vchObservaciones;
                                    mdl.vchValor = chk.Checked ? "1" : "0";
                                    lst.Add(mdl);
                                }
                            }
                        }
                    }
                }
            }
            catch(Exception egA)
            {
                Log.EscribeLog("Existe un error en getAdicionales: " + egA.Message, 3, Usuario.vchUsuario);
            }
            return lst;
        }

        protected void btnCancelPaciente_Click(object sender, EventArgs e)
        {
            try
            {
                limpiarControlesIniciales();
            }
            catch (Exception eSOBs)
            {
                ShowMessage("Existe un error al limpiar los controles: " + eSOBs.Message, MessageType.Error, "alert_container");
                Log.EscribeLog("Existe un error en btnCancelPaciente_Click: " + eSOBs.Message, 3, Usuario.vchUsuario);
            }
        }

        private bool validarHorarios()
        {
            bool valido = true;
            try
            {
                if(lstEstudios.Count > 0)
                {
                    foreach(clsEstudio item in lstEstudios)
                    {
                        if (item.fechaInicio == DateTime.MinValue)
                        {
                            valido = false;
                            break;
                        }
                    }
                }
            }
            catch (Exception eBC)
            {
                valido = false;
                //ShowMessage("Existe un error al cancelar: " + eBC.Message, MessageType.Error, "alert_container");
                Log.EscribeLog("Existe un error en validarHorarios: " + eBC.Message, 3, Usuario.vchUsuario);
            }
            return valido;
        }

        private bool validarEstudios()
        {
            bool valido = false;
            try
            {
                if (do_stEstudios.Count > 0)
                {
                    valido = true;
                }
            }
            catch (Exception eBC)
            {
                valido = false;
                //ShowMessage("Existe un error al cancelar: " + eBC.Message, MessageType.Error, "alert_container");
                Log.EscribeLog("Existe un error en validarEstudios: " + eBC.Message, 3, Usuario.vchUsuario);
            }
            return valido;
        }

        private bool validarPaciente()
        {
            bool valido = false;
            try
            {
                if(lblIDs.Text != "")
                {
                    int idPaciente;
                    if(Int32.TryParse(lblIDs.Text,out idPaciente))
                    {
                        valido = true;
                    }
                }
            }
            catch (Exception eBC)
            {
                valido = false;
                //ShowMessage("Existe un error al cancelar: " + eBC.Message, MessageType.Error, "alert_container");
                Log.EscribeLog("Existe un error en validarPaciente: " + eBC.Message, 3, Usuario.vchUsuario);
            }
            return valido;
        }

        protected void btnCancelObs_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtObservaciones.Text == "")
                {
                    string btnID = hfintAdicionalID.Value;
                    LinkButton btn = (LinkButton)pnlAdiClin.FindControl("radBtn" + btnID);
                    CheckBox chk = new CheckBox();
                    chk = (CheckBox)btn.FindControl("chk" + btn.CommandArgument);
                    if (btn != null)
                    {
                        chk.Checked = false;
                    }
                }
            }
            catch (Exception eSOBs)
            {
                ShowMessage("Existe un error al cancelar las observaciones: " + eSOBs.Message, MessageType.Error, "alert_container");
                Log.EscribeLog("Existe un error en btnCancelObs_Click: " + eSOBs.Message, 3, Usuario.vchUsuario);
            }
        }

        private void limpiarControlesIniciales()
        {
            try
            {
                HFintPacienteID.Value = "0";
                lblIDs.Visible = false;
                lblIDs.Text = "";
                txtNombrePaciente.Text = "";
                txtApellidos.Text = "";
                Date1.Text = "";
                lstEstudios.Clear();
                do_stEstudios.Clear();
                grvEstudios.DataSource = null;
                grvEstudios.DataBind();
                lstObser.Clear();
                pnlObservaciones.Controls.Clear();
                cargaAdicionales();
                txtBusquedaPaciente.Text = "";
                txtBusquedaEstudio.Text = "";
                Masculino = 0;
                Femenino = 0;
                Mayor = 0;
                Menor = 0;
            }
            catch (Exception eSOBs)
            {
                ShowMessage("Existe un error al limpiar los controles: " + eSOBs.Message, MessageType.Error, "alert_container");
                Log.EscribeLog("Existe un error en btnCancelObs_Click: " + eSOBs.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void btnSaveObs_Click(object sender, EventArgs e)
        {
            try
            {
                string intAdicionalID = hfintAdicionalID.Value;
                intAdicionalID = "lblAdicionalID" + intAdicionalID;
                Label obs = (Label)pnlObservaciones.FindControl(intAdicionalID);
                if (obs == null)
                {
                    Label txtObs = new Label();
                    txtObs.ID = intAdicionalID;
                    txtObs.Text = lblTitObs.Text + ": " + txtObservaciones.Text;
                    txtObs.ForeColor = System.Drawing.Color.DarkGreen;
                    txtObs.ClientIDMode = ClientIDMode.Static;
                    pnlObservaciones.Controls.Add(txtObs);
                    clsAdicionales mdl = new clsAdicionales();
                    mdl.intAdicionalesID = Convert.ToInt32(hfintAdicionalID.Value);
                    mdl.vchObservaciones = txtObservaciones.Text;
                    lstObser.Add(mdl);
                }
                else
                {
                    lstObser.Where(x=> x.intAdicionalesID == Convert.ToInt32(hfintAdicionalID.Value)).First().vchObservaciones = txtObservaciones.Text;
                    obs.Text = lblTitObs.Text + ": " + txtObservaciones.Text;
                }
                txtObservaciones.Text = "";
            }
            catch(Exception eSOBs)
            {
                ShowMessage("Existe un error al agregar las observaciones: " + eSOBs.Message, MessageType.Error, "alert_container");
                Log.EscribeLog("Existe un error en btnSaveObs_Click: " + eSOBs.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void lnkImprimir_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception eBI)
            {
                ShowMessage("Existe un error al imprimir la cita: " + eBI.Message, MessageType.Error, "alert_Container");
                Log.EscribeLog("Existe un error en lnkImprimir_Click: " + eBI.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void lnkReEnviarCorreo_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception eBI)
            {
                ShowMessage("Existe un error al re-enviar el correo: " + eBI.Message, MessageType.Error, "alert_Container");
                Log.EscribeLog("Existe un error en lnkReEnviarCorreo_Click: " + eBI.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void lnkInterpretacion_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch(Exception eBI)
            {
                ShowMessage("Existe un error al generar la interpretación: " + eBI.Message, MessageType.Error, "alert_Container");
                Log.EscribeLog("Existe un error en lnkInterpretacion_Click: " + eBI.Message, 3, Usuario.vchUsuario);
            }
        }

        #region CargaAgenda

        protected void Button2_Click(object sender, EventArgs e)
        {
            //carga_citas_("1");

        }

        private void carga_tabla_citas(string id, DateTime fecha, string IDMODALIDAD, string id_modalidad_seleccionada, int idsitio)
        {
            try
            {
                RC_Agenda.Visible = true;
                RC_Agenda.SelectedDate = fecha;                

                L_modalidad_seleccion.Text = id_modalidad_seleccionada;

                int id_mod = Convert.ToInt32(id);
                string HR_INICIO_DIA = "";
                string HR_FIN_DIA = "";
                int duracionGen = 0;

                DataTable dt_hrs_muertas = new DataTable();

                try
                {
                    string descrip_modalidad = "";
                    try
                    {
                        AgendaRequest request_desc_mod = new AgendaRequest();
                        request_desc_mod.mdlUser = Usuario;
                        request_desc_mod.mdlagenda.intModalidadID = id_mod;
                        request_desc_mod.mdlagenda.intSitioID = idsitio;
                        descrip_modalidad = RisService.getDescripcionModalidad_sitio(request_desc_mod);
                    }

                    catch (Exception ecU)
                    {
                        Log.EscribeLog("Existe un error en la busqueda de el tipo de modadlidad: " + ecU.Message, 3, Usuario.vchUsuario);
                    }

                    try
                    {
                        CitaModalidad request_DuracionGen = new CitaModalidad();
                        request_DuracionGen.mdlUser = Usuario;
                        request_DuracionGen.mdlModalidad.intModalidadID = id_mod;
                        request_DuracionGen.mdlModalidad.intSitioID = idsitio;
                        duracionGen = RisService.getListDuracionGen_Sitio(request_DuracionGen);
                    }

                    catch (Exception ecU)
                    {
                        Log.EscribeLog("Existe un error en la busqueda duracion modadlidad: " + ecU.Message, 3, Usuario.vchUsuario);
                    }
                    IMG_encabezado.ImageUrl = "images/" + descrip_modalidad + ".png";
                    Ltitulo.Text = descrip_modalidad;
                    LIDModalidad.Text = IDMODALIDAD;

                    string carga_color = "";
                    try
                    {
                        AgendaRequest request_ = new AgendaRequest();
                        request_.mdlUser = Usuario;
                        request_.mdlagenda.vchCodigo = descrip_modalidad;
                        request_.mdlagenda.intSitioID = idsitio;
                        carga_color = RisService.getListColorModalidad_Sitio(request_);
                        carga_color = carga_color.TrimEnd();
                    }
                    catch (Exception ecU)
                    {
                        Log.EscribeLog("Existe un error en la busqueda de color de la modalidad: " + ecU.Message, 3, Usuario.vchUsuario);
                    }
                    Image1.Style["Background"] = "linear-gradient(75deg, #CCCCCC, " + carga_color + " 10px, white);";

                    List<clsEventoCita> lstTec = new List<clsEventoCita>();
                    CitasRequest request = new CitasRequest();
                    request.mdlUser = Usuario;
                    request.mdlevento.intModalidadID = id_mod;
                    request.mdlevento.intSitioID = idsitio;
                    lstTec = RisService.getListCitas_Sitio(request);
                }
                catch (Exception ecU)
                {
                    Log.EscribeLog("Existe un error en la carga de las citas: " + ecU.Message, 3, Usuario.vchUsuario);
                }

                try
                {
                    List<clsConfScheduler> lstTec = new List<clsConfScheduler>();
                    ConfigSchedulerRequest request = new ConfigSchedulerRequest();
                    request.mdlUser = Usuario;
                    request.mdlConfScheduler.intSitioID = idsitio;
                    lstTec = RisService.getConfScheduler_Sitio(request);

                    TimeSpan dt_inicio = new TimeSpan();
                    TimeSpan dt_fin = new TimeSpan();
                    int intervalo_agenda = 0;

                    foreach (var item in lstTec)
                    {
                        dt_inicio = item.tmeInicioDia;
                        dt_fin = item.tmeFinDia;
                    }

                    var lista_HRSMuertas = new List<int>();

                    try
                    {
                        List<clsHoraMuerta> _lstTec = new List<clsHoraMuerta>();
                        ConfigScheduler_HoraMuertaRequest _request = new ConfigScheduler_HoraMuertaRequest();
                        _request.mdlUser = Usuario;
                        _request.mdlHMScheduler.intSitioID = idsitio;
                        _lstTec = RisService.getHoraMuertaConfScheduler_Sitio(_request);

                        TimeSpan HM_inicio = new TimeSpan();
                        TimeSpan HM_FIn = new TimeSpan();
                        TimeSpan HM_Periodo = new TimeSpan();

                        List<string> lista_HM = new List<string>();
                        dt_hrs_muertas.Columns.Add("HM_Inicio");
                        dt_hrs_muertas.Columns.Add("HM_FIn");

                        foreach (var _item in _lstTec)
                        {

                            HM_inicio = TimeSpan.Parse(_item.tmeInicio);
                            HM_FIn = TimeSpan.Parse(_item.tmeFin);
                            dt_hrs_muertas.Rows.Add(HM_inicio, HM_FIn);
                            lista_HM.Add(HM_inicio.ToString() + "-" + HM_FIn.ToString());
                            HM_Periodo = HM_FIn - HM_inicio;

                            int horas_contenidas = Convert.ToInt32(HM_Periodo.TotalHours.ToString());
                            lista_HRSMuertas.Add((Convert.ToInt32(HM_inicio.Hours.ToString())));

                            TimeSpan H_Agregada = new TimeSpan();
                            H_Agregada = HM_inicio;

                            for (int i = 1; i <= horas_contenidas; i++)
                            {
                                TimeSpan duration_ = new TimeSpan(0, 1, 0, 0);

                                H_Agregada = H_Agregada.Add(duration_);
                                lista_HRSMuertas.Add(Convert.ToInt32(H_Agregada.Hours.ToString()));
                            }
                            lista_HRSMuertas.Sort();
                        }
                    }

                    catch (Exception ex)
                    {
                        Log.EscribeLog("Existe un error en ConfigScheduler_HoraMuertaRequest: " + ex.Message, 3, Usuario.vchNombre);
                    }


                    DataTable dt_horas_agenda = new DataTable();
                    dt_horas_agenda.Columns.Add("Horas");
                    dt_horas_agenda.Columns.Add("Cupo");
                    int horario = dt_fin.Hours - dt_inicio.Hours;
                    horario = horario - 1;

                    int numero_de_estudios_x_hr = 0;

                    switch (duracionGen)
                    {
                        case 1:
                            numero_de_estudios_x_hr = 12;
                            break;
                        case 2:
                            numero_de_estudios_x_hr = 6;
                            break;
                        case 3:
                            numero_de_estudios_x_hr = 5;
                            break;
                        case 4:
                            numero_de_estudios_x_hr = 4;
                            break;
                        case 5:
                            numero_de_estudios_x_hr = 3;
                            break;
                        case 6:
                            numero_de_estudios_x_hr = 2;
                            break;
                        case 7:
                            numero_de_estudios_x_hr = 1;
                            break;
                    }


                    int numero_de_equipos = 0;
                    try
                    {
                        List<clsEquipo> lst_CitaEquipo = new List<clsEquipo>();
                        CitaNumEquipos request_CitaEquipo = new CitaNumEquipos();
                        request_CitaEquipo.mdlUser = Usuario;
                        request_CitaEquipo.mdlequipo.intModalidadID = id_mod;
                        request_CitaEquipo.mdlequipo.intSitioID = idsitio;
                        lst_CitaEquipo = RisService.getCitaEquipo_Sitio(request_CitaEquipo);

                        numero_de_equipos = lst_CitaEquipo.Count();
                    }

                    catch (Exception ecU)
                    {
                        Log.EscribeLog("Existe un error en la busqueda de el tipo de modadlidad: " + ecU.Message, 3, Usuario.vchUsuario);
                    }

                    int estudios_posibles_x_modalidad = 0;
                    int numero_De_Equipos = numero_de_equipos;
                    estudios_posibles_x_modalidad = numero_De_Equipos * numero_de_estudios_x_hr;

                    List<clsEventoCita> lstcitasagendadas = new List<clsEventoCita>();

                    DataTable dt_citas_generadas = new DataTable();
                    dt_citas_generadas.Columns.Add("ID_tabla");
                    dt_citas_generadas.Columns.Add("Hr");
                    dt_citas_generadas.Columns.Add("Capacidad");
                    dt_citas_generadas.Columns.Add("Libres");
                    dt_citas_generadas.Columns.Add("Rating");

                    DataTable dt_dia2 = new DataTable();
                    dt_dia2.Columns.Add("ID_tabla");
                    dt_dia2.Columns.Add("Hr");
                    dt_dia2.Columns.Add("Capacidad");
                    dt_dia2.Columns.Add("Libres");
                    dt_dia2.Columns.Add("Rating");

                    DataTable dt_dia3 = new DataTable();
                    dt_dia3.Columns.Add("ID_tabla");
                    dt_dia3.Columns.Add("Hr");
                    dt_dia3.Columns.Add("Capacidad");
                    dt_dia3.Columns.Add("Libres");
                    dt_dia3.Columns.Add("Rating");

                    DataTable dt_dia4 = new DataTable();
                    dt_dia4.Columns.Add("ID_tabla");
                    dt_dia4.Columns.Add("Hr");
                    dt_dia4.Columns.Add("Capacidad");
                    dt_dia4.Columns.Add("Libres");
                    dt_dia4.Columns.Add("Rating");

                    DataTable dt_dia5 = new DataTable();
                    dt_dia5.Columns.Add("ID_tabla");
                    dt_dia5.Columns.Add("Hr");
                    dt_dia5.Columns.Add("Capacidad");
                    dt_dia5.Columns.Add("Libres");
                    dt_dia5.Columns.Add("Rating");


                    try
                    {
                        CitasRequest request_citasagendadas = new CitasRequest();
                        request_citasagendadas.mdlUser = Usuario;
                        request_citasagendadas.mdlevento.intModalidadID = id_mod;
                        //request_citasagendadas.mdlevento.Start = DateTime.Today;
                        request_citasagendadas.mdlevento.Start = fecha;
                        request_citasagendadas.mdlevento.intSitioID = idsitio;
                        lstcitasagendadas = RisService.getListCitas_en_agenda_Sitio(request_citasagendadas);

                        for (int z = 0; z <= 4; z++)
                        {
                            DateTime fecha_revision = request_citasagendadas.mdlevento.Start;
                            fecha_revision = fecha_revision.AddDays(z);

                            for (int i = dt_inicio.Hours; i <= dt_fin.Hours; i++)
                            {
                                bool bandera_igual = false;
                                foreach (var hr in lista_HRSMuertas)
                                {
                                    if (hr == i)
                                    {
                                        bandera_igual = true;
                                    }
                                }

                                if (bandera_igual == false)
                                {
                                    int contador = lstcitasagendadas.Count(x => x.Start.Hour == i && x.Start.Day == fecha_revision.Day);
                                    double porcen_Rating = 0;

                                    if (contador != 0)
                                    {
                                        int revision_porcentaje = (contador * 100) / estudios_posibles_x_modalidad;

                                        if (revision_porcentaje >= 0 && revision_porcentaje < 13)
                                            porcen_Rating = 1;

                                        if (revision_porcentaje >= 13 && revision_porcentaje < 26)
                                            porcen_Rating = 1.5;

                                        if (revision_porcentaje >= 26 && revision_porcentaje < 39)
                                            porcen_Rating = 2;

                                        if (revision_porcentaje >= 39 && revision_porcentaje < 52)
                                            porcen_Rating = 2.5;

                                        if (revision_porcentaje >= 52 && revision_porcentaje < 65)
                                            porcen_Rating = 3;

                                        if (revision_porcentaje >= 65 && revision_porcentaje < 78)
                                            porcen_Rating = 3.5;

                                        if (revision_porcentaje >= 78 && revision_porcentaje < 91)
                                            porcen_Rating = 4;

                                        if (revision_porcentaje >= 91 && revision_porcentaje < 99)
                                            porcen_Rating = 4.5;

                                        if (revision_porcentaje == 100)
                                            porcen_Rating = 5;
                                    }


                                    int dia = z;
                                    switch (z)
                                    {
                                        case 0:
                                            LDia1.Text = fecha_revision.ToShortDateString();
                                            dt_citas_generadas.Rows.Add(1, i, contador + " / " + estudios_posibles_x_modalidad, (estudios_posibles_x_modalidad - contador), porcen_Rating);
                                            break;
                                        case 1:
                                            //LDia2.Text = fecha_revision.ToShortDateString();
                                            //dt_dia2.Rows.Add(2, i, contador + " / " + estudios_posibles_x_modalidad, (estudios_posibles_x_modalidad - contador), porcen_Rating);
                                            break;
                                        case 2:
                                            //LDia3.Text = fecha_revision.ToShortDateString();
                                            //dt_dia3.Rows.Add(3, i, contador + " / " + estudios_posibles_x_modalidad, (estudios_posibles_x_modalidad - contador), porcen_Rating);
                                            break;
                                        case 3:
                                            //LDia4.Text = fecha_revision.ToShortDateString();
                                            //dt_dia4.Rows.Add(4, i, contador + " / " + estudios_posibles_x_modalidad, (estudios_posibles_x_modalidad - contador), porcen_Rating);
                                            break;
                                        case 4:
                                            //LDia5.Text = fecha_revision.ToShortDateString();
                                            //dt_dia5.Rows.Add(5, i, contador + " / " + estudios_posibles_x_modalidad, (estudios_posibles_x_modalidad - contador), porcen_Rating);
                                            break;
                                    }
                                }
                            }
                        }

                        RG_Dia1.DataSource = dt_citas_generadas;
                        RG_Dia1.DataBind();

                        //RG_Dia2.DataSource = dt_dia2;
                        //RG_Dia2.DataBind();

                        //RG_Dia3.DataSource = dt_dia3;
                        //RG_Dia3.DataBind();

                        //RG_Dia4.DataSource = dt_dia4;
                        //RG_Dia4.DataBind();

                        //RG_Dia5.DataSource = dt_dia5;
                        //RG_Dia5.DataBind();
                    }
                    catch (Exception ecU)
                    {
                        Log.EscribeLog("Existe un error en la carga de las citas ya generadas: " + ecU.Message, 3, Usuario.vchUsuario);
                    }
                }

                catch (Exception ex)
                {
                    Log.EscribeLog("Existe un error en cargaConfigScheduler: " + ex.Message, 3, Usuario.vchNombre);
                }
            }
            catch(Exception Global)
            {
                Log.EscribeLog("Existe un error en la tabla: " + Global.Message, 3, Usuario.vchUsuario);
            }
        }

        #endregion CargaAgenda

        protected void btnCargarSug_Click(object sender, EventArgs e)
        {
            try
            {
                cargarSugerencias();
            }
            catch(Exception eCS)
            {
                ShowMessage("No se pudieron cargar las sugerencias: " + eCS.Message, MessageType.Error, "alert_container");
                Log.EscribeLog("Existe un error al cargar las sugerencias: " + eCS.Message, 3, Usuario.vchUsuario);
            }
        }

        private void cargarSugerencias()
        {
            try
            {
                List<stp_getCitaDisponible_Result> response = new List<stp_getCitaDisponible_Result>();
                SugerenciasRequest request = new SugerenciasRequest();
                request = obtenerBusquedaSug();
                grvSugerencia.DataSource = null;
                if (request != null)
                {
                    response = RisService.getSugerenciasCita(request);
                    if (response != null)
                    {
                        lstSug = response;
                        grvSugerencia.DataSource = response;
                    }
                }
                grvSugerencia.DataBind();
            }
            catch(Exception ecS)
            {
                Log.EscribeLog("Existe un error en cargarSugerencias: " + ecS.Message, 3, Usuario.vchUsuario);
            }
        }

        private SugerenciasRequest obtenerBusquedaSug()
        {
            SugerenciasRequest sug = new SugerenciasRequest();
            try
            {
                sug.mdlUser = Usuario;
                clsSugerencia mdl = new clsSugerencia();
                mdl.datFechaInicio = calFecIni.SelectedDate == null ? DateTime.Now : (DateTime)calFecIni.SelectedDate;
                mdl.datFechaFinal = calFecFin.SelectedDate == null ? DateTime.Now.AddDays(7): (DateTime)calFecFin.SelectedDate;
                mdl.intModalidad = Convert.ToInt32(Session["intModSug"].ToString());
                string lunes = (bool)chkLunes.Checked ? "1," : "";
                string martes = (bool)chkMartes.Checked ? "2," : "";
                string miercoles = (bool)chkMiercoles.Checked ? "3," : "";
                string jueves = (bool)chkJueves.Checked ? "4," : "";
                string viernes = (bool)chkViernes.Checked ? "5," : "";
                string sabado = (bool)chkSabado.Checked ? "6," : "";
                string domingo = (bool)chkDomingo.Checked ? "7," : "";
                string dias = lunes + martes + miercoles + jueves + viernes + sabado + domingo;
                mdl.vchDias = dias;
                string manana = chkOpManana.Checked ? "1," : "";
                string tarde = chkOpTarde.Checked ? "2," : "";
                string noche = chkOpNoche.Checked ? "3," : "";
                string horas = manana + tarde + noche;
                mdl.vchHoras = horas;
                if (mdl != null)
                    sug.mdlSug = mdl;
                mdl.intSitioID = Usuario.intSitioID;
            }
            catch(Exception eoBS)
            {
                sug = null;
                Log.EscribeLog("Existe un error en obtenerBusquedaSug: " + eoBS.Message, 3, Usuario.vchUsuario);
            }
            return sug;
        }

        private SugerenciasRequest obtenerBusquedaCompleta()
        {
            SugerenciasRequest sug = new SugerenciasRequest();
            try
            {
                sug.mdlUser = Usuario;
                clsSugerencia mdl = new clsSugerencia();
                mdl.datFechaInicio = DateTime.Now;
                mdl.datFechaFinal = DateTime.Now.AddDays(7);
                mdl.intModalidad = Convert.ToInt32(Session["intModSug"].ToString());
                mdl.vchDias = "1,2,3,4,5,6,7,";
                mdl.vchHoras = "";
                mdl.intSitioID = Usuario.intSitioID;
                sug.mdlSug = mdl;
            }
            catch (Exception eoBS)
            {
                sug = null;
                Log.EscribeLog("Existe un error en obtenerBusquedaCompleta: " + eoBS.Message, 3, Usuario.vchUsuario);
            }
            return sug;
        }

        protected void grvSugerencia_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Pager)
                {
                    Label lblTotalNumDePaginas = (Label)e.Row.FindControl("lblBandejaTotal");
                    lblTotalNumDePaginas.Text = grvSugerencia.PageCount.ToString();

                    TextBox txtIrAlaPagina = (TextBox)e.Row.FindControl("txtBandejaS");
                    txtIrAlaPagina.Text = (grvSugerencia.PageIndex + 1).ToString();

                    DropDownList ddlTamPagina = (DropDownList)e.Row.FindControl("ddlBandejaS");
                    ddlTamPagina.SelectedValue = grvSugerencia.PageSize.ToString();
                }

                if (e.Row.RowType != DataControlRowType.DataRow)
                {
                    return;
                }

                if (e.Row.DataItem != null)
                {
                    stp_getCitaDisponible_Result _mdl = (stp_getCitaDisponible_Result)e.Row.DataItem;
                    if (_mdl.intHora > 0)
                    {
                        Label lblHora = (Label)e.Row.FindControl("lblHora");
                        string horas = _mdl.intHora.ToString() + ":00 - " + (_mdl.intHora + 1).ToString() + ":00";
                        lblHora.Text = horas;
                    }
                }
            }
            catch (Exception eEst)
            {
                Log.EscribeLog("Existe un error en grvEstudios_RowDataBound: " + eEst.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void grvSugerencia_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                if (e.NewPageIndex >= 0)
                {
                    this.grvSugerencia.PageIndex = e.NewPageIndex;
                    cargarSugerencias();
                }
            }
            catch (Exception ex)
            {
                Log.EscribeLog("Existe un error grvSugerencia_PageIndexChanging: " + ex.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void grvSugerencia_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int intSugerencia = 0;
                switch (e.CommandName)
                {
                    case "Seleccionar":
                        intSugerencia = Convert.ToInt32(e.CommandArgument.ToString());
                        stp_getCitaDisponible_Result mdl = new stp_getCitaDisponible_Result();
                        mdl = lstSug.First(x => x.intSugerenciaID == intSugerencia);
                        TimeSpan horas = new TimeSpan(mdl.intHora, 0, 0);
                        mdl.datFecha = mdl.datFecha + horas;
                        DateTime datFechaFin = mdl.datFecha.AddHours(1);
                        do_stEstudios.First(x => x.intRelModPres == Convert.ToInt32(Session["inrRelModPres"])).fechaInicio = mdl.datFecha;
                        do_stEstudios.First(x => x.intRelModPres == Convert.ToInt32(Session["inrRelModPres"])).fechaFin = datFechaFin;
                        grvEstudios.DataSource = null;
                        grvEstudios.DataSource = do_stEstudios;
                        grvEstudios.DataBind();
                        lstSug = null;
                        grvSugerencia.DataSource = null;
                        grvSugerencia.DataBind();
                        break;
                }
            }
            catch(Exception eS)
            {
                Log.EscribeLog("Existe un error en grvSugerencia_RowCommand: " + eS.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void ddlBandejaS_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DropDownList dropDownList = (DropDownList)sender;
                if (int.Parse(dropDownList.SelectedValue) != 0)
                {
                    this.grvSugerencia.AllowPaging = true;
                    this.grvSugerencia.PageSize = int.Parse(dropDownList.SelectedValue);
                }
                else
                    this.grvSugerencia.AllowPaging = false;
                this.cargarSugerencias();
            }
            catch (Exception eddS)
            {
                Log.EscribeLog("Existe un error ddlBandejaS_SelectedIndexChanged de frmAddDate: " + eddS.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void txtBandejaS_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox txtBandejaAvaluosGoToPage = (TextBox)sender;
                int numeroPagina;
                if (int.TryParse(txtBandejaAvaluosGoToPage.Text.Trim(), out numeroPagina))
                    this.grvSugerencia.PageIndex = numeroPagina - 1;
                else
                    this.grvSugerencia.PageIndex = 0;
                this.cargarSugerencias();
            }
            catch (Exception ex)
            {
                Log.EscribeLog("Existe un error txtBandejaS_TextChanged de frmAddDate: " + ex.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void RG_Dia1_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Agregar_cita")
            {
                RC_Agenda.Visible = false;
                string IDMOD = HFIDModalidad_calendario.Value;
                GridDataItem item = (GridDataItem)e.Item;
                string strtxt = item["ID_tabla"].Text;
                string Hora_seleccionada = item["Hr"].Text;

                int dia = Convert.ToInt32(strtxt);
                string dia_cita = "";

                switch (dia)
                {
                    case (1):
                        dia_cita = LDia1.Text;
                        break;
                    case (2):
                        //dia_cita = LDia2.Text;
                        break;
                    case (3):
                        //dia_cita = LDia3.Text;
                        break;
                    case (4):
                        //dia_cita = LDia4.Text;
                        break;
                    case (5):
                        //dia_cita = LDia5.Text;
                        break;
                }


                foreach (var x in do_stEstudios)
                {

                    if (L_modalidad_seleccion.Text == Convert.ToString(x.intconsecutivo_Modalidad))
                    {
                        x.fechaInicio = Convert.ToDateTime(dia_cita + " " + Hora_seleccionada + ":00");
                        x.fechaFin = Convert.ToDateTime(dia_cita + " " + Hora_seleccionada + ":00");
                    }
                    //response.mdlEstudio.fechaInicio = Convert.ToDateTime("10/08/2017");

                    //lstEstudios.

                    grvEstudios.DataSource = null;
                    grvEstudios.DataBind();
                    grvEstudios.DataSource = do_stEstudios;
                    grvEstudios.DataBind();

                    RG_Dia1.DataSource = null;
                    RG_Dia1.DataSourceID = null;
                    RG_Dia1.DataBind();
                    LDia1.Text = "";
                    //RG_Dia2.DataSource = null;
                    //RG_Dia2.DataSourceID = null;
                    //RG_Dia2.DataBind();
                    //LDia2.Text = "";
                    //RG_Dia3.DataSource = null;
                    //RG_Dia3.DataSourceID = null;
                    //RG_Dia3.DataBind();
                    //LDia3.Text = "";
                    //RG_Dia4.DataSource = null;
                    //RG_Dia4.DataSourceID = null;
                    //RG_Dia4.DataBind();
                    //LDia4.Text = "";
                    //RG_Dia5.DataSource = null;
                    //RG_Dia5.DataSourceID = null;
                    //RG_Dia5.DataBind();
                    //LDia5.Text = "";

                    Ltitulo.Text = "";
                    IMG_encabezado.ImageUrl = "";
                    Image1.Style["Background"] = "White";

                    //RB_antes_fecha.Enabled = false;
                    //RB_despues_feha.Enabled = false;

                    //string z = "";
                }

            }
        }

        protected void RB_antes_fecha_Click(object sender, EventArgs e)
        {

            if (Convert.ToDateTime(LDia1.Text) > DateTime.Today)
            {
                RG_Dia1.DataSource = null;
                RG_Dia1.DataSourceID = null;
                RG_Dia1.DataBind();
                //RG_Dia2.DataSource = null;
                //RG_Dia2.DataSourceID = null;
                //RG_Dia2.DataBind();
                //RG_Dia3.DataSource = null;
                //RG_Dia3.DataSourceID = null;
                //RG_Dia3.DataBind();
                //RG_Dia4.DataSource = null;
                //RG_Dia4.DataSourceID = null;
                //RG_Dia4.DataBind();
                //RG_Dia5.DataSource = null;
                //RG_Dia5.DataSourceID = null;
                //RG_Dia5.DataBind();

                DateTime FECHA = Convert.ToDateTime(LDia1.Text).AddDays(-5);

                carga_tabla_citas(LIDModalidad.Text, FECHA, LIDModalidad.Text, L_modalidad_seleccion.Text, Usuario.intSitioID);
            }
        }

        protected void RB_despues_feha_Click(object sender, EventArgs e)
        {
            //RB_antes_fecha.Enabled = true;
            //RB_despues_feha.Enabled = true;
            RG_Dia1.DataSource = null;
            RG_Dia1.DataSourceID = null;
            RG_Dia1.DataBind();
            //RG_Dia2.DataSource = null;
            //RG_Dia2.DataSourceID = null;
            //RG_Dia2.DataBind();
            //RG_Dia3.DataSource = null;
            //RG_Dia3.DataSourceID = null;
            //RG_Dia3.DataBind();
            //RG_Dia4.DataSource = null;
            //RG_Dia4.DataSourceID = null;
            //RG_Dia4.DataBind();
            //RG_Dia5.DataSource = null;
            //RG_Dia5.DataSourceID = null;
            //RG_Dia5.DataBind();

            RG_Dia1.Dispose();
            DateTime FECHA = Convert.ToDateTime(LDia1.Text).AddDays(5);
            carga_tabla_citas(LIDModalidad.Text, FECHA, LIDModalidad.Text, L_modalidad_seleccion.Text, Usuario.intSitioID);
        }

        public void Busqueda_estudio()
        {
            //PacienteRequest request = new PacienteRequest();
            //PacienteResponse response = new PacienteResponse();
            //RisLiteService service = new RisLiteService();
            //request.mdlUser = Usuario;
            //request.busqueda = RadAutoCompleteBox1.Text;
            //response = service.getBusquedaEstudio(request);



            //RadAutoCompleteBox1.DataSource = response;
            //RadAutoCompleteBox1.DataBind();

        }

        protected void RadAjaxPanel1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            try
            {
                //string id = txtBusquedaEstudio.Text;
                HFIDModalidad_calendario.Value = e.Argument;
                cargarEstudioGrid(Convert.ToInt32(e.Argument));
                txtBusquedaEstudio.Text = "";
                grvEstudios.Visible = true;
                //int x = 0;
            }
            catch(Exception erPA)
            {
                Log.EscribeLog("Existe un error en RadAjaxPanel1_AjaxRequest: " + erPA.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void radAjaxPanelSugerencia_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {

        }

        protected void radAjaxPanelPaciente_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            try
            {
                string id = "";// txtBusquedaPaciente.Text;
                //string[] paciente = txtBusquedaPaciente.Text.ToString().Split('|');
                id = e.Argument;
                txtBusquedaPaciente.Text = "";
                cargarDetallePaciente(Convert.ToInt32(id));
                txtBusquedaEstudio.Enabled = true;

            }
            catch (Exception etC)
            {
                Log.EscribeLog("Existe un error en radAjaxPanelPaciente_AjaxRequest: " + etC.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void RC_Agenda_SelectionChanged(object sender, Telerik.Web.UI.Calendar.SelectedDatesEventArgs e)
        {
            DateTime fecha_calendario = ((Telerik.Web.UI.RadCalendar)sender).SelectedDate;

            RG_Dia1.DataSourceID = null;
            RG_Dia1.DataBind();         
            RG_Dia1.Dispose();
            //DateTime FECHA = Convert.ToDateTime(LDia1.Text).AddDays(5);
            carga_tabla_citas(LIDModalidad.Text, fecha_calendario, LIDModalidad.Text, L_modalidad_seleccion.Text, Usuario.intSitioID);

        }
    }
}