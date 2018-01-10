using Fuji.RISLite.Entidades.Extensions;
using Fuji.RISLite.Entities;
using Fuji.RISLite.Site.Services;
using Fuji.RISLite.Site.Services.DataContract;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using Telerik.Web.UI;
using Telerik.Web.UI.GridExcelBuilder;

namespace Fuji.RISLite.Site
{
    public partial class frmRepEstadisticas : System.Web.UI.Page
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
                if (!IsPostBack)
                {
                    if (Session["UserRISAxon"] != null && Session["lstVistas"] != null)
                    {
                        Usuario = (clsUsuario)Session["UserRISAxon"];
                        if (Security.ValidateToken(Usuario.Token, Usuario.intUsuarioID.ToString(), Usuario.vchUsuario))
                        {
                            List<clsVistasUsuarios> lstVista = (List<clsVistasUsuarios>)Session["lstVistas"];
                            if (lstVista != null)
                            {
                                string vista = "frmRepEstadisticas.aspx";
                                if (lstVista.Any(x => x.vchVistaIdentificador == vista))
                                {
                                    Usuario = (clsUsuario)Session["UserRISAxon"];
                                    if (Usuario != null)
                                    {
                                        int idsitio = Usuario.intSitioID;
                                        int id_usuario = Usuario.intUsuarioID;

                                        carga_sitios(id_usuario, idsitio);
                                        carga_modalidades();
                                        carga_usuarios();
                                        carga_estatus_Estudio();
                                        carga_estatus_Fechas();
                                    }
                                    else
                                    {
                                        var = Security.Encrypt("1");
                                        Response.Redirect(URL + "/frmSalir.aspx?var=" + var);
                                    }
                                }
                                else
                                {
                                    Response.Redirect(URL + "/frmSinPermiso.aspx");
                                }
                            }
                            else
                            {
                                Response.Redirect(URL + "/frmSinPermiso.aspx");
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
            }
            catch (Exception ePL)
            {
                Log.EscribeLog("Existe un error en Page_Load de frmRepEstadisticas: " + ePL.Message, 3, "");
            }
        }

        private void carga_modalidades()
        {
            try
            {
                RCB_Modalidad.DataSource = null;
                List<clsConfAgenda> lstTec = new List<clsConfAgenda>();
                AgendaRequest request = new AgendaRequest();
                request.mdlUser = Usuario;
                request.mdlagenda.intSitioID = Convert.ToInt32(RCB_Sitio.SelectedValue);
                lstTec = RisService.get_modalidades_estadistica(request);
                if (lstTec != null)
                {
                    if (lstTec.Count > 0)
                    {
                        RCB_Modalidad.DataSource = lstTec;
                    }
                }
                RCB_Modalidad.DataBind();
            }
            catch (Exception ecU)
            {
                Log.EscribeLog("Existe un error en carga_modalidades: " + ecU.Message, 3, Usuario.vchUsuario);
            }
        }

        private void carga_usuarios()
        {
            try
            {
                RCB_Personal.DataSource = null;
                List<clsUsuario> lstTec = new List<clsUsuario>();
                AgendaRequest request = new AgendaRequest();
                request.mdlUser = Usuario;
                request.mdlagenda.intSitioID = Convert.ToInt32(RCB_Sitio_Personal.SelectedValue);
                lstTec = RisService.get_personal_Estadistica(request);
                if (lstTec != null)
                {
                    if (lstTec.Count > 0)
                    {
                        RCB_Personal.DataSource = lstTec;
                    }
                }
                RCB_Personal.DataBind();
            }
            catch (Exception ecU)
            {
                Log.EscribeLog("Existe un error en carga_usuarios: " + ecU.Message, 3, Usuario.vchUsuario);
            }
        }

        private void carga_sitios(int id_usuariositio, int idsitio)
        {
            try
            {
                List<clsSitio> lstTec = new List<clsSitio>();
                AgendaRequest request = new AgendaRequest();
                request.mdlUser = Usuario;
                lstTec = RisService.get_sitio_estadistica(request);
                if (lstTec != null)
                {
                    if (lstTec.Count > 0)
                    {
                        RCB_Sitio.DataSource = lstTec;
                        RCB_Sitio_Personal.DataSource = lstTec;
                    }
                }
                RCB_Sitio.DataBind();
                RCB_Sitio_Personal.DataBind();
            }
            catch (Exception ecU)
            {
                Log.EscribeLog("Existe un error en cargarAgenda: " + ecU.Message, 3, Usuario.vchUsuario);
            }

            if (id_usuariositio != 1)//Si no es administrador
            {
                RCB_Sitio_Personal.Enabled = false;
                RCB_Sitio_Personal.SelectedValue = Convert.ToString(idsitio);
                RCB_Sitio.Enabled = false;
                RCB_Sitio.SelectedValue = Convert.ToString(idsitio);
            }
        }

        private void carga_estatus_Estudio()
        {
            try
            {
                List<clsEstatusEstudio> lstTec = new List<clsEstatusEstudio>();
                AgendaRequest request = new AgendaRequest();
                request.mdlUser = Usuario;
                lstTec = RisService.get_sitio_estatus_estudio(request);
                if (lstTec != null)
                {
                    if (lstTec.Count > 0)
                    {
                        RCB_Estatus.DataSource = lstTec;
                        RCB_Estatus_Personal.DataSource = lstTec;
                    }
                }
                RCB_Estatus.DataBind();
                RCB_Estatus_Personal.DataBind();
            }
            catch (Exception ecU)
            {
                Log.EscribeLog("Existe un error en cargarAgenda: " + ecU.Message, 3, Usuario.vchUsuario);
            }
        }

        private void carga_estatus_Fechas()
        {
            try
            {
                RDP_Desde.SelectedDate = DateTime.Now;
                RDP_Hasta.SelectedDate = DateTime.Now.AddDays(5);
                RDP_desde_personal.SelectedDate = DateTime.Now;
                RDP_hasta_personal.SelectedDate = DateTime.Now.AddDays(5);
            }
            catch (Exception ecU)
            {
                Log.EscribeLog("Existe un error en carga_estatus_Fechas: " + ecU.Message, 3, Usuario.vchUsuario);
            }
        }

        private void carga_grafica_modalidad()
        {
            try
            {
                List<clsSitio> lstTec = new List<clsSitio>();
                AgendaRequest request = new AgendaRequest();
                request.mdlUser = Usuario;
                lstTec = RisService.get_sitio_estadistica(request);
                if (lstTec != null)
                {
                    if (lstTec.Count > 0)
                    {
                        RCB_Sitio.DataSource = lstTec;
                    }
                }
                RCB_Sitio.DataBind();
            }
            catch (Exception ecU)
            {
                Log.EscribeLog("Existe un error en cargarAgenda: " + ecU.Message, 3, Usuario.vchUsuario);
            }
        }

        protected void RCB_Sitio_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            carga_modalidades();
        }

        protected void RCB_Sitio_Personal_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            carga_usuarios();
        }

        protected void RB_Buscar_Click(object sender, EventArgs e)
        {

            DataSet ds_tabla = new DataSet("modalidad_tabla");
            DataTable dt_tabla = new DataTable("tabla_Modalidad");
            dt_tabla.Columns.Add("Id", Type.GetType("System.Int32"));
            dt_tabla.Columns.Add("Modalidad", Type.GetType("System.String"));
            dt_tabla.Columns.Add("Estatus", Type.GetType("System.String"));
            dt_tabla.Columns.Add("Titulo", Type.GetType("System.String"));
            dt_tabla.Columns.Add("Descripcion", Type.GetType("System.String"));
            dt_tabla.Columns.Add("FechaInicio", Type.GetType("System.String"));
            dt_tabla.Columns.Add("FechaFin", Type.GetType("System.String"));

            DataSet ds = new DataSet("datos_graf");
            DataTable dt = new DataTable("tabla_Modalidad_Grafica");
            dt.Columns.Add("Id", Type.GetType("System.Int32"));
            dt.Columns.Add("Modalidad", Type.GetType("System.String"));
            dt.Columns.Add("Total", Type.GetType("System.Int32"));

            var modalidades_selec = RCB_Modalidad.CheckedItems;
            List<string> values = new List<string>();

            string sitioID = RCB_Sitio.SelectedValue;
            string datFechaInicio = RDP_Desde.ValidationDate + " 00:00:00.000";
            string datFechaFin = RDP_Hasta.ValidationDate + " 23:59:00.000";

            if (modalidades_selec.Count != 0)
            {
                foreach (var item in modalidades_selec)
                {
                    var estatus_selec = RCB_Estatus.CheckedItems;
                    List<string> valores_estaus = new List<string>();

                    int contador = 1;
                    int total_estudios = 0;

                    string intMOdalidadID = item.Value;

                    if (modalidades_selec.Count != 0)
                    {
                        foreach (var item_estatus in estatus_selec)
                        {
                            string intEstatusEstudio = item_estatus.Value;

                            List<clsGraficaModalidad> lstTec = new List<clsGraficaModalidad>();
                            AgendaRequest request = new AgendaRequest();
                            request.mdlUser = Usuario;
                            lstTec = RisService.stp_get_Datos_Modalidad_Estadistica(request, Convert.ToInt32(sitioID), Convert.ToInt32(intMOdalidadID), datFechaInicio, datFechaFin, intEstatusEstudio);

                            total_estudios = total_estudios + lstTec.Count();
                            //valores_estaus.Add(item_estatus.Text);                                                   

                            contador++;

                            foreach (var lista_datos in lstTec)
                            {
                                dt_tabla.Rows.Add(lista_datos.intEstudioID, lista_datos.vchModalidad, lista_datos.vchEstatus, lista_datos.vchTitulo, lista_datos.vchDescripcion, lista_datos.fechaInicio, lista_datos.fechaFin);
                            }
                        }
                    }
                    dt.Rows.Add(contador, item.Text, total_estudios);
                }
                ds.Tables.Add(dt);
                ds_tabla.Tables.Add(dt_tabla);

                RHC_grafica_modalidad.DataSource = ds;
                RHC_grafica_modalidad.DataBind();
                RHC_grafica_modalidad.Visible = true;

                RG_modalidad.DataSource = ds_tabla;
                RG_modalidad.DataBind();

                ////RB_export_modalidad.Visible = true;
                //IB_export_modalidad.Visible = true;
            }
        }

        protected void RB_Buscar_Personal_Click(object sender, EventArgs e)
        {
            DataSet ds_tabla = new DataSet("modalidad_tabla");
            DataTable dt_tabla = new DataTable("tabla_Modalidad");
            dt_tabla.Columns.Add("Id", Type.GetType("System.Int32"));
            dt_tabla.Columns.Add("Usuario", Type.GetType("System.String"));
            dt_tabla.Columns.Add("Modalidad", Type.GetType("System.String"));
            dt_tabla.Columns.Add("Estatus", Type.GetType("System.String"));
            dt_tabla.Columns.Add("Titulo", Type.GetType("System.String"));
            dt_tabla.Columns.Add("Descripcion", Type.GetType("System.String"));
            dt_tabla.Columns.Add("FechaInicio", Type.GetType("System.String"));
            dt_tabla.Columns.Add("FechaFin", Type.GetType("System.String"));

            DataSet ds = new DataSet("datos_graf");
            DataTable dt = new DataTable("tabla_usuarios_Grafica");
            dt.Columns.Add("Id", Type.GetType("System.Int32"));
            dt.Columns.Add("Usuario", Type.GetType("System.String"));
            dt.Columns.Add("Total", Type.GetType("System.Int32"));

            var usuario_selec = RCB_Personal.CheckedItems;
            List<string> values = new List<string>();

            string sitioID = RCB_Sitio_Personal.SelectedValue;
            string datFechaInicio = RDP_desde_personal.ValidationDate + " 00:00:00.000";
            string datFechaFin = RDP_Hasta.ValidationDate + " 23:59:00.000";

            if (usuario_selec.Count != 0)
            {
                foreach (var item in usuario_selec)
                {
                    var estatus_selec = RCB_Estatus_Personal.CheckedItems;
                    List<string> valores_estaus = new List<string>();

                    int contador = 1;
                    int total_estudios = 0;

                    string intusuario = item.Value;

                    if (usuario_selec.Count != 0)
                    {
                        foreach (var item_estatus in estatus_selec)
                        {
                            string intEstatusEstudio = item_estatus.Value;

                            List<clsGraficaUsuario> lstTec = new List<clsGraficaUsuario>();
                            AgendaRequest request = new AgendaRequest();
                            request.mdlUser = Usuario;
                            lstTec = RisService.stp_get_Datos_Usuarios_Estadistica(request, Convert.ToInt32(sitioID), Convert.ToInt32(intusuario), datFechaInicio, datFechaFin, intEstatusEstudio);

                            total_estudios = total_estudios + lstTec.Count();
                            //valores_estaus.Add(item_estatus.Text);                                                   

                            contador++;

                            foreach (var lista_datos in lstTec)
                            {
                                dt_tabla.Rows.Add(lista_datos.intEstudioID, lista_datos.vchUsuario, lista_datos.vchmodalidad, lista_datos.vchEstatus, lista_datos.vchTitulo, lista_datos.vchDescripcion, lista_datos.fechaInicio, lista_datos.fechaFin);
                            }
                        }
                    }
                    dt.Rows.Add(contador, item.Text, total_estudios);
                }
                ds.Tables.Add(dt);
                ds_tabla.Tables.Add(dt_tabla);

                RCH_Personal.DataSource = ds;
                RCH_Personal.DataBind();
                RCH_Personal.Visible = true;

                RG_Usuarios.DataSource = ds_tabla;
                RG_Usuarios.DataBind();

                //RB_export.Visible = true;
                //IB_Export_usuarios.Visible = true;
            }
        }

        protected void RB_export_Click(object sender, EventArgs e)
        {

            if (RG_Usuarios.Items.Count > 0)
            {
                //RG_Usuarios.ExportSettings.Excel.Format = GridExcelExportFormat.Xlsx;
                RG_Usuarios.ExportSettings.IgnorePaging = true;
                RG_Usuarios.ExportSettings.ExportOnlyData = true;
                RG_Usuarios.ExportSettings.OpenInNewWindow = true;
                RG_Usuarios.ExportSettings.UseItemStyles = true;
                RG_Usuarios.MasterTableView.ExportToExcel();
            }

        }

        protected void RG_Usuarios_HTMLExporting(object sender, GridHTMLExportingEventArgs e)
        {
            e.Styles.Append("@page table .ID { background-color: #d3d3d3; }");
        }

        protected void RG_Usuarios_BiffExporting(object sender, GridBiffExportingEventArgs e)
        {
            e.ExportStructure.Tables[0].Columns[1].Style.BackColor = System.Drawing.Color.LightGray;
        }

        protected void RG_Usuarios_ExcelMLWorkBookCreated(object sender, Telerik.Web.UI.GridExcelBuilder.GridExcelMLWorkBookCreatedEventArgs e)
        {
            foreach (RowElement row in e.WorkBook.Worksheets[0].Table.Rows)
            {
                row.Cells[0].StyleValue = "Style1";
            }

            StyleElement style = new StyleElement("Style1");
            style.InteriorStyle.Pattern = InteriorPatternType.Solid;
            style.InteriorStyle.Color = System.Drawing.Color.LightGray;
            e.WorkBook.Styles.Add(style);
        }

        protected void RG_Usuarios_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == RadGrid.ExportToExcelCommandName)
            {
                RG_Usuarios.ExportSettings.Excel.Format = GridExcelExportFormat.Biff;
                RG_Usuarios.ExportSettings.IgnorePaging = true;
                RG_Usuarios.ExportSettings.ExportOnlyData = true;
                RG_Usuarios.ExportSettings.FileName = "Usuarios" + DateTime.Today.ToString("ddMMyyyy");
                RG_Usuarios.ExportSettings.OpenInNewWindow = true;
            }
        }

        protected void RB_export_modalidad_Click(object sender, EventArgs e)
        {

            if (RG_modalidad.Items.Count > 0)
            {
                //RG_modalidad.ExportSettings.Excel.Format = GridExcelExportFormat.Xlsx;
                RG_modalidad.ExportSettings.IgnorePaging = true;
                RG_modalidad.ExportSettings.ExportOnlyData = true;
                RG_modalidad.ExportSettings.OpenInNewWindow = true;
                RG_modalidad.ExportSettings.UseItemStyles = true;
                RG_modalidad.ExportSettings.FileName = "Modalidad_" + DateTime.Today.ToString("ddMMyyyy");
                RG_modalidad.MasterTableView.ExportToExcel();
            }


        }

        protected void RG_Usuarios_ItemCommand1(object sender, GridCommandEventArgs e)
        {
            //RG_Usuarios.ExportSettings.Excel.Format = GridExcelExportFormat.Xlsx;
            //RG_Usuarios.ExportSettings.IgnorePaging = true;
            //RG_Usuarios.ExportSettings.ExportOnlyData = true;
            //RG_Usuarios.ExportSettings.OpenInNewWindow = true;
            //RG_Usuarios.ExportSettings.UseItemStyles = true;
            //RG_Usuarios.MasterTableView.ExportToExcel();
        }
    }
}