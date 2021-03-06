﻿using Fuji.RISLite.Entidades.Extensions;
using Fuji.RISLite.Entities;
using Fuji.RISLite.Site.Services;
using Fuji.RISLite.Site.Services.DataContract;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.UI.HtmlControls;

namespace Fuji.RISLite.Site
{
    public partial class frmAdminEquipoUsuario : System.Web.UI.Page
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
                                string vista = "frmAdminEquipoUsuario.aspx";
                                if (lstVista.Any(x => x.vchVistaIdentificador == vista))
                                {
                                    Usuario = (clsUsuario)Session["UserRISAxon"];
                                    if (Usuario != null)
                                    {
                                        createTableEquipo();
                                        createTableTecnico();
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
                Log.EscribeLog("Existe un error en Page_Load de frmAdminEquipoUsuario: " + ePL.Message, 3, "");
            }
        }

        private void createTableTecnico()
        {
            try
            {
                List<clsUsuario> lstTec = new List<clsUsuario>();
                TecnicoRequest request = new TecnicoRequest();
                request.mdlUser = Usuario;
                lstTec = RisService.getListTecnico(request);
                if (lstTec != null)
                {
                    if (lstTec.Count > 0)
                    {
                        construirTablaTecnico(lstTec);
                    }
                }
            }
            catch(Exception ecTT)
            {
                Log.EscribeLog("Existe un error en createTableTecnico: " + ecTT.Message, 3, Usuario.vchUsuario);
            }
        }

        private void construirTablaTecnico(List<clsUsuario> lstTec)
        {
            try
            {
                string htmlTable = "";
                htmlTable = "<table id='dynamic-table1' class='table table-striped table-bordered table-hover'>";
                htmlTable += "<thead><tr><th>Nombre</th><th>Usuario</th><th>Estatus</th><th><i class='ace-icon fa fa-pencil-square-o bigger-110'></i>Editar</th></tr></thead>";
                htmlTable += "<tbody>";
                foreach (clsUsuario item in lstTec)
                {
                    htmlTable += "<tr><td>" + item.vchNombre + "</td><td>" + item.vchUsuario + "</td><td>" + item.bitActivo.ToString() + "</td><td><a class='green' href='#'><i class='ace-icon fa fa-pencil bigger-130'></i></a></td></tr>";
                }
                htmlTable += "</tbody>";
                htmlTable += "</table>";
                tableTecnico.Controls.Add(new System.Web.UI.LiteralControl(htmlTable));
            }
            catch(Exception eccT)
            {
                Log.EscribeLog("Existe un error en construirTablaTecnico: " + eccT.Message, 3, Usuario.vchUsuario);
            }
        }

        private void createTableEquipo()
        {
            try
            {
                List<clsEquipo> lst = new List<clsEquipo>();
                EquipoRequest request = new EquipoRequest();
                request.mdlUser = Usuario;
                lst = RisService.getListaEquipos(request);
                if(lst != null)
                {
                    if(lst.Count > 0)
                    {
                        construirTabla(lst);
                    }
                }
            }
            catch(Exception ecE)
            {
                Log.EscribeLog("Existe un error al createTableEquipo: " + ecE.Message, 3, Usuario.vchUsuario);
            }
        }

        private void construirTabla(List<clsEquipo> lst)
        {
            try
            {
                string htmlTable = "";
                htmlTable = "<table id='dynamic-table' class='table table-striped table-bordered table-hover'>";
                htmlTable += "<thead><tr><th>Nombre</th><th>Modalidad</th><th>Estatus</th><th><i class='ace-icon fa fa-pencil-square-o bigger-110'></i>Editar</th></tr></thead>";
                htmlTable += "<tbody>";
                foreach(clsEquipo item in lst)
                {
                    htmlTable += "<tr><td>"+item.vchNombreEquipo + "</td><td>" + item.vchModalidad + "</td><td>" + item.bitActivo.ToString() + "</td><td><a class='green' href='#'><i class='ace-icon fa fa-pencil bigger-130'></i></a></td></tr>";
                }
                htmlTable += "</tbody>";
                htmlTable += "</table>";
                tableEquipos.Controls.Add(new System.Web.UI.LiteralControl(htmlTable));
            }
            catch(Exception ecT)
            {
                Log.EscribeLog("Existe un error en construirTabla: " + ecT.Message, 3, Usuario.vchUsuario);
            }
        }
    }
}