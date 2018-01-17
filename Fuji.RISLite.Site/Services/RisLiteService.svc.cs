using Fuji.RISLite.DataAccess;
using Fuji.RISLite.Entidades.DataBase;
using Fuji.RISLite.Entidades.Extensions;
using Fuji.RISLite.Entities;
using Fuji.RISLite.Site.Services.DataContract;
using System;
using System.Collections.Generic;

namespace Fuji.RISLite.Site.Services
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "RisLiteService" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione RisLiteService.svc o RisLiteService.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class RisLiteService : IRisLiteService
    {
        public void DoWork()
        {
        }

        public ValidaUserResponse getUser(ValidaUserRequest request)
        {
            ValidaUserResponse response = new ValidaUserResponse();
            try
            {
                clsUsuario mdlUser = new clsUsuario();
                List<clsVistasUsuarios> lstVistas = new List<clsVistasUsuarios>();
                RISLiteDataAccess controller = new RISLiteDataAccess();
                response.Success = controller.getUser(request.user, ref mdlUser, ref lstVistas);
                response.mdlUser = mdlUser;
                response.lstVistas = lstVistas;
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en getUser: " + egU.Message, 3, "");
            }
            return response;
        }

        public ValidaUserResponse getLoginUser(ValidaUserRequest request)
        {
            ValidaUserResponse response = new ValidaUserResponse();
            try
            {
                clsUsuario mdlUser = new clsUsuario();
                List<clsVistasUsuarios> lstVistas = new List<clsVistasUsuarios>();
                RISLiteDataAccess controller = new RISLiteDataAccess();
                string mensaje = "";
                response.Success = controller.getLoginUser(request.user, request.pass, ref mdlUser, ref lstVistas, ref mensaje);
                response.mdlUser = mdlUser;
                response.mensaje = mensaje;
                response.lstVistas = lstVistas;
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en getLoginUser: " + egU.Message, 3, "");
            }
            return response;
        }

        #region catalogo
        public List<tbl_CAT_Catalogo> getListCatalogos(CatalogoRequest request)
        {
            List<tbl_CAT_Catalogo> response = new List<tbl_CAT_Catalogo>();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    response = controller.getListCatalogos(request.mdlUser.vchUsuario);
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en getUser: " + egU.Message, 3, "");
            }
            return response;
        }

        public List<stp_getListCatalogo_Result> getListCatalogo(CatalogoRequest request)
        {
            List<stp_getListCatalogo_Result> response = new List<stp_getListCatalogo_Result>();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    response = controller.getListCatalogo(request.mdlCat.intCatalogoID, request.mdlCat.intSitioID, request.mdlUser.vchUsuario);
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en getUser: " + egU.Message, 3, "");
            }
            return response;
        }

        public stp_updateCatEstatus_Result updateCatalogoEstatus(CatalogoRequest request)
        {
            stp_updateCatEstatus_Result response = new stp_updateCatEstatus_Result();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    response = controller.updateCatalogoEstatus(request.mdlCat.intCatalogoID, request.mdlCat.bitActivo, request.mdlCat.intPrimaryKey, request.mdlUser.vchUsuario);
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en getUser: " + egU.Message, 3, "");
            }
            return response;
        }

        public stp_updateCatalogo_Result updateCatalogo(CatalogoRequest request)
        {
            stp_updateCatalogo_Result response = new stp_updateCatalogo_Result();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    response = controller.updateCatalogo(request.mdlCat.intCatalogoID, request.mdlCat.intPrimaryKey, request.mdlCat.vchValor, request.mdlUser.vchUsuario);
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en getUser: " + egU.Message, 3, "");
            }
            return response;
        }

        public stp_setItemCatalogo_Result setItemCatalogo(CatalogoRequest request)
        {
            stp_setItemCatalogo_Result response = new stp_setItemCatalogo_Result();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    response = controller.setItemCatalogo(request.mdlCat.intCatalogoID, request.mdlCat.intSitioID, request.mdlCat.vchValor, request.mdlUser.vchUsuario);
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en getUser: " + egU.Message, 3, "");
            }
            return response;
        }

        public List<clsCatalogo> getTipoUsuario(CatalogoRequest request)
        {
            List<clsCatalogo> response = new List<clsCatalogo>();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    response = controller.getTipoUsuario(request.mdlUser.vchUsuario);
                }
            }
            catch (Exception egtU)
            {
                Log.EscribeLog("Existe un error en getTipoUsuario: " + egtU.Message, 3, request.mdlUser.vchUsuario);
            }
            return response;
        }

        public List<clsCatalogo> getListaBoton(CatalogoRequest request)
        {
            List<clsCatalogo> response = new List<clsCatalogo>();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    response = controller.getListaBoton(request.mdlUser.vchUsuario);
                }
            }
            catch (Exception egtU)
            {
                Log.EscribeLog("Existe un error en getListaBoton: " + egtU.Message, 3, request.mdlUser.vchUsuario);
            }
            return response;
        }

        public List<clsCatalogo> getListaVista(CatalogoRequest request)
        {
            List<clsCatalogo> response = new List<clsCatalogo>();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    response = controller.getListaVista(request.mdlUser.vchUsuario);
                }
            }
            catch (Exception egtU)
            {
                Log.EscribeLog("Existe un error en getListaVista: " + egtU.Message, 3, request.mdlUser.vchUsuario);
            }
            return response;
        }

        public List<stp_getListaPaginas_Result> getListVistas(CatalogoRequest request)
        {
            List<stp_getListaPaginas_Result> response = new List<stp_getListaPaginas_Result>();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    response = controller.getListVistas(request.mdlUser.intTipoUsuario, request.mdlUser.vchUsuario);
                }
            }
            catch (Exception egtU)
            {
                Log.EscribeLog("Existe un error en getTipoUsuario: " + egtU.Message, 3, request.mdlUser.vchUsuario);
            }
            return response;
        }


        #endregion catalogo

        #region equipo
        public List<clsEquipo> getListaEquipos(EquipoRequest request)
        {
            List<clsEquipo> response = new List<clsEquipo>();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    response = controller.getListaEquipos(request.mdlUser.vchUsuario);
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en getUser: " + egU.Message, 3, "");
            }
            return response;
        }

        #endregion equipo

        #region tecnico
        public List<clsUsuario> getListTecnico(TecnicoRequest request)
        {
            List<clsUsuario> response = new List<clsUsuario>();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    response = controller.getListTecnico(request.mdlUser.vchUsuario);
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en getListTecnico: " + egU.Message, 3, "");
            }
            return response;
        }
        #endregion tecnico

        #region sitios
        public List<tbl_CAT_Sitio> getListSitios(SitioRequest request)
        {
            List<tbl_CAT_Sitio> response = new List<tbl_CAT_Sitio>();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    response = controller.getListSitios(request.mdlUser.vchUsuario);
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en getListSitios: " + egU.Message, 3, "");
            }
            return response;
        }

        public SitioResponse setSitio(SitioRequest request)
        {
            SitioResponse response = new SitioResponse();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    string mensaje = "";
                    response.Success = controller.setSitio(request.mdlSitio, request.mdlUser.vchUsuario, ref mensaje);
                    response.Mensaje = mensaje;
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en setSitio: " + egU.Message, 3, "");
            }
            return response;
        }

        public SitioResponse setActualizaSitio(SitioRequest request)
        {
            SitioResponse response = new SitioResponse();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    string mensaje = "";
                    response.Success = controller.setActualizaSitio(request.mdlSitio, request.mdlUser.vchUsuario, ref mensaje);
                    response.Mensaje = mensaje;
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en setActualizaSitio: " + egU.Message, 3, "");
            }
            return response;
        }

        public SitioResponse setEstatusSitio(SitioRequest request)
        {
            SitioResponse response = new SitioResponse();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    string mensaje = "";
                    response.Success = controller.setEstatusSitio(request.intSitioID, request.mdlUser.vchUsuario, ref mensaje);
                    response.Mensaje = mensaje;
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en setEstatusSitio: " + egU.Message, 3, "");
            }
            return response;
        }
        #endregion sitios



        #region ConfigSitio
        public ConfigSitioResponse getConfigSitio(ConfigSitioRequest request)
        {
            ConfigSitioResponse response = new ConfigSitioResponse();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    tbl_MST_ConfiguracionSistema mdl = new tbl_MST_ConfiguracionSistema();
                    string mensaje = "";
                    response.Success = controller.getConfigSitio(request.intSitioId,request.mdlUser.vchUsuario,ref mdl,ref mensaje);
                    response.mdlConfig = mdl;
                    response.Mensaje = mensaje;
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en getConfigSitio: " + egU.Message, 3, "");
            }
            return response;
        }

        public ConfigSitioResponse setConfigSitio(ConfigSitioRequest request)
        {
            ConfigSitioResponse response = new ConfigSitioResponse();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    string mensaje = "";
                    response.Success = controller.setConfigSitio(request.mdlConfig, request.mdlUser.vchUsuario, ref mensaje);
                    response.Mensaje = mensaje;
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en setConfigSitio: " + egU.Message, 3, "");
            }
            return response;
        }

        public ConfigSitioResponse setActualizarConfigSitio(ConfigSitioRequest request)
        {
            ConfigSitioResponse response = new ConfigSitioResponse();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    string mensaje = "";
                    response.Success = controller.setActualizarConfigSitio(request.mdlConfig, request.mdlUser.vchUsuario, ref mensaje);
                    response.Mensaje = mensaje;
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en setActualizarConfigSitio: " + egU.Message, 3, "");
            }
            return response;
        }
        #endregion ConfigSitio

        #region ConfigEmail
        public ConfigEmailResponse getConfigEmail(ConfigEmailRequest request)
        {
            ConfigEmailResponse response = new ConfigEmailResponse();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    tbl_Conf_CorreoSitio mdl = new tbl_Conf_CorreoSitio();
                    string mensaje = "";
                    response.Success = controller.getConfigEmail(request.intSitioID, request.mdlUser.vchUsuario, ref mdl, ref mensaje);
                    response.mldConfigEmail = mdl;
                    response.Mensaje = mensaje;
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en getConfigEmail: " + egU.Message, 3, "");
            }
            return response;
        }

        public ConfigEmailResponse setConfigEmail(ConfigEmailRequest request)
        {
            ConfigEmailResponse response = new ConfigEmailResponse();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    string mensaje = "";
                    response.Success = controller.setConfigEmail(request.mdlEmail, request.mdlUser.vchUsuario, ref mensaje);
                    response.Mensaje = mensaje;
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en setConfigEmail: " + egU.Message, 3, "");
            }
            return response;
        }

        public ConfigEmailResponse setActualizarConfigEmail(ConfigEmailRequest request)
        {
            ConfigEmailResponse response = new ConfigEmailResponse();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    string mensaje = "";
                    response.Success = controller.setActualizarConfigEmail(request.mdlEmail, request.mdlUser.vchUsuario, ref mensaje);
                    response.Mensaje = mensaje;
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en setActualizarConfigEmail: " + egU.Message, 3, "");
            }
            return response;
        }
        #endregion ConfigEmail

        #region varAdicionales
        public List<clsVarAcicionales> getVariablesAdicionalPaciente(VarAdicionalRequest request)
        {
            List<clsVarAcicionales> response = new List<clsVarAcicionales>();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    response = controller.getVariablesAdicionalPaciente(request.mdlUser.vchUsuario, request.intSitioID);
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en getVariablesAdicionalPaciente: " + egU.Message, 3, "");
            }
            return response;
        }

        public List<clsVarAcicionales> getVariablesAdicionalCita(VarAdicionalRequest request)
        {
            List<clsVarAcicionales> response = new List<clsVarAcicionales>();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    response = controller.getVariablesAdicionalCita(request.mdlUser.vchUsuario, request.intSitioID);
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en getVariablesAdicionalCita: " + egU.Message, 3, "");
            }
            return response;
        }

        public List<tbl_CAT_Identificacion> getVariablesAdicionalID(VarAdicionalRequest request)
        {
            List<tbl_CAT_Identificacion> response = new List<tbl_CAT_Identificacion>();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    response = controller.getVariablesAdicionalID(request.mdlUser.vchUsuario, request.intSitioID);
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en getVariablesAdicionalID: " + egU.Message, 3, "");
            }
            return response;
        }

        public VarAdicionalResponse setAgregarVariable(VarAdicionalRequest request)
        {
            VarAdicionalResponse response = new VarAdicionalResponse();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    string mensaje = "";
                    response.Success = controller.setAgregarVariable(request.intTipoVariable,request.mdlVariable.vchNombreVarAdi, request.intSitioID, request.mdlUser.vchUsuario,ref mensaje);
                    response.Mensaje = mensaje;
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en setAgregarVariable: " + egU.Message, 3, "");
            }
            return response;
        }

        public VarAdicionalResponse setActualizarVariable(VarAdicionalRequest request)
        {
            VarAdicionalResponse response = new VarAdicionalResponse();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    string mensaje = "";
                    response.Success = controller.setActualizarVariable(request.intTipoVariable, request.mdlVariable.intVariableAdiID, request.mdlVariable.vchNombreVarAdi, request.mdlUser.vchUsuario, ref mensaje);
                    response.Mensaje = mensaje;
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en setActualizarVariable: " + egU.Message, 3, "");
            }
            return response;
        }

        public VarAdicionalResponse setEstatusVariable(VarAdicionalRequest request)
        {
            VarAdicionalResponse response = new VarAdicionalResponse();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    string mensaje = "";
                    response.Success = controller.setEstatusVariable(request.intTipoVariable, request.mdlUser.vchUsuario, ref mensaje);
                    response.Mensaje = mensaje;
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en setEstatusVariable: " + egU.Message, 3, "");
            }
            return response;
        }
        #endregion varAdicionales

        #region AdminUsers
        public List<clsUsuario> getListaUsuarios(TecnicoRequest request)
        {
            List<clsUsuario> response = new List<clsUsuario>();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    response = controller.getListaUsuarios(request.mdlUser.vchUsuario);
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en getListaUsuarios: " + egU.Message, 3, "");
            }
            return response;
        }

        public AdminUserResponse setUsuario(AdminUserRequest request)
        {
            AdminUserResponse response = new AdminUserResponse();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    string mensaje = "";
                    response.Success = controller.setUsuario(request.mdlAdminUser, request.mdlUser.vchUsuario, ref mensaje);
                    response.Mensaje = mensaje;
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en setUsuario: " + egU.Message, 3, "");
            }
            return response;
        }

        public AdminUserResponse setActualizaUsuario(AdminUserRequest request)
        {
            AdminUserResponse response = new AdminUserResponse();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    string mensaje = "";
                    response.Success = controller.setActualizaUsuario(request.mdlAdminUser, request.mdlUser.vchUsuario, ref mensaje);
                    response.Mensaje = mensaje;
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en setActualizaUsuario: " + egU.Message, 3, "");
            }
            return response;
        }

        public AdminUserResponse setEstatusUsuario(AdminUserRequest request)
        {
            AdminUserResponse response = new AdminUserResponse();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    string mensaje = "";
                    response.Success = controller.setEstatusUsuario(request.mdlAdminUser.intUsuarioID, request.mdlUser.vchUsuario, ref mensaje);
                    response.Mensaje = mensaje;
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en setEstatusUsuario: " + egU.Message, 3, "");
            }
            return response;
        }

        public List<tbl_CAT_Modalidad> getModalidadTecnico(ModTecnicoRequest request)
        {
            List<tbl_CAT_Modalidad> response = new List<tbl_CAT_Modalidad>();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    response = controller.getModalidadTecnico(request.intSitioID, request.mdlUser.vchUsuario);
                }
            }
            catch (Exception egmT)
            {
                Log.EscribeLog("Existe un error en getModalidadTecnico : " + egmT.Message, 3, request.mdlUser.vchUsuario);
            }
            return response;
        }

        public List<stp_getRELModalidadTecnico_Result> getModalidadTecnicoList(ModTecnicoRequest request)
        {
            List<stp_getRELModalidadTecnico_Result> response = new List<stp_getRELModalidadTecnico_Result>();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    response = controller.getModalidadTecnicoList(request.intUsuarioID, request.mdlUser.vchUsuario);
                }
            }
            catch (Exception egmT)
            {
                Log.EscribeLog("Existe un error en getModalidadTecnicoList : " + egmT.Message, 3, request.mdlUser.vchUsuario);
            }
            return response;
        }

        public ModTecnicoResponse setModalidadTecnico(ModTecnicoRequest request)
        {
            ModTecnicoResponse response = new ModTecnicoResponse();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    string mensaje = "";
                    response.success = controller.setModalidadTecnico(request.intUsuarioID, request.intModalidadID, request.mdlUser.vchUsuario, ref mensaje);
                    response.mensaje = mensaje;
                }
            }
            catch(Exception esM)
            {
                Log.EscribeLog("Existe un error en setModalidadTecnico: " + esM.Message, 3, request.mdlUser.vchUsuario);
            }
            return response;
        }

        public ModTecnicoResponse setEstatusModalidadTecnico(ModTecnicoRequest request)
        {
            ModTecnicoResponse response = new ModTecnicoResponse();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    string mensaje = "";
                    response.success = controller.setEstatusModalidadTecnico(request.intRELModTecnicoID, request.mdlUser.vchUsuario, ref mensaje);
                    response.mensaje = mensaje;
                }
            }
            catch (Exception esM)
            {
                Log.EscribeLog("Existe un error en setEstatusModalidadTecnico: " + esM.Message, 3, request.mdlUser.vchUsuario);
            }
            return response;
        }

        #endregion AdminUsers

        #region Prestacion
        public List<tbl_CAT_Modalidad> getListModalidades(CatalogoRequest request)
        {
            List<tbl_CAT_Modalidad> response = new List<tbl_CAT_Modalidad>();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    response = controller.getListModalidades(request.mdlUser.vchUsuario);
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en getListModalidades: " + egU.Message, 3, "");
            }
            return response;
        }

        public List<clsPrestacion> getListPrestacion(PrestacionRequest request)
        {
            List<clsPrestacion> response = new List<clsPrestacion>();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    response = controller.getListPrestacion(request.intModalidad, request.intSitioID,request.mdlUser.vchUsuario);
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en getListPrestacion: " + egU.Message, 3, "");
            }
            return response;
        }

        public PrestacionResponse setPrestacion(PrestacionRequest request)
        {
            PrestacionResponse response = new PrestacionResponse();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    string mensaje = "";
                    response.Success = controller.setPrestacion(request.mdlPres, request.mdlUser.vchUsuario, ref mensaje);
                    response.Mensaje = mensaje;
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en setPrestacion: " + egU.Message, 3, "");
            }
            return response;
        }

        public PrestacionResponse setActualizaPrestacion(PrestacionRequest request)
        {
            PrestacionResponse response = new PrestacionResponse();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    string mensaje = "";
                    response.Success = controller.setActualizaPrestacion(request.mdlPres, request.mdlUser.vchUsuario, ref mensaje);
                    response.Mensaje = mensaje;
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en setActualizaPrestacion: " + egU.Message, 3, "");
            }
            return response;
        }

        public PrestacionResponse setEstatusPrestacion(PrestacionRequest request)
        {
            PrestacionResponse response = new PrestacionResponse();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    string mensaje = "";
                    response.Success = controller.setEstatusPrestacion(request.intRELModPres, request.mdlUser.vchUsuario, ref mensaje);
                    response.Mensaje = mensaje;
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en setEstatusPrestacion: " + egU.Message, 3, "");
            }
            return response;
        }

        #endregion Prestacion

        #region agenda
        public List<clsConfAgenda> getListAgenda(AgendaRequest request)
        {
            List<clsConfAgenda> response = new List<clsConfAgenda>();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    response = controller.getListAgenda(request.mdlUser.vchUsuario, request.mdlagenda.intSitioID);
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en getAgenda: " + egU.Message, 3, "");
            }
            return response;
        }

        public bool UpdateAgenda(AgendaRequest request)
        {
            bool bandera_update_agenda = false;
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    bandera_update_agenda = controller.UpdateAgenda(request.mdlUser.vchUsuario, request.mdlagenda.intModalidadID, request.mdlagenda.vchCodigo, request.mdlagenda.vchModalidad, request.mdlagenda.vchColor, request.mdlagenda.intDuracionGen, request.mdlagenda.intSitioID);
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en getAgenda: " + egU.Message, 3, "");
            }
            return bandera_update_agenda;
        }

        public AgendaResponse setEstatusAgenda(AgendaRequest request)
        {
            AgendaResponse response = new AgendaResponse();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    string mensaje = "";
                    response.Success = controller.setEstatusAgenda(request.mdlUser.vchNombre, request.mdlagenda.intModalidadID, ref mensaje);
                    response.Mensaje = mensaje;
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en setEstatusUsuario: " + egU.Message, 3, "");
            }
            return response;
        }

        public AgendaResponse setAgenda(AgendaRequest request)
        {
            AgendaResponse response = new AgendaResponse();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    string mensaje = "";
                    response.Success = controller.setAgenda(request.mdlagenda, request.mdlUser.vchUsuario, ref mensaje);
                    response.Mensaje = mensaje;
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en setUsuario: " + egU.Message, 3, "");
            }
            return response;
        }

        public string getListColorModalidad_Sitio(AgendaRequest request)
        {
            string response = "";
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    response = controller.getListColorModalidad_Sitio(request.mdlUser.vchUsuario, request.mdlagenda.vchCodigo, request.mdlagenda.intSitioID);
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en getAgenda: " + egU.Message, 3, "");
            }
            return response;
        }

        public string getListColorModalidad(AgendaRequest request)
        {
            string response = "";
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    response = controller.getListColorModalidad(request.mdlUser.vchUsuario, request.mdlagenda.vchCodigo);
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en getAgenda: " + egU.Message, 3, "");
            }
            return response;
        }

        public int getListDuracionGen_Sitio(CitaModalidad request)
        {
            int response = 0;
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    response = controller.getListDuracionGen_Sitio(request.mdlUser.vchUsuario, request.mdlModalidad.intModalidadID, request.mdlModalidad.intSitioID);
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en getAgenda: " + egU.Message, 3, "");
            }
            return response;
        }

        public int getListDuracionGen(CitaModalidad request)
        {
            int response = 0;
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    response = controller.getListDuracionGen(request.mdlUser.vchUsuario, request.mdlModalidad.intModalidadID);
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en getAgenda: " + egU.Message, 3, "");
            }
            return response;
        }

        public string getDescripcionModalidad_sitio(AgendaRequest request)
        {
            string response = "";

            int id_mod = Convert.ToInt32(request.mdlagenda.intModalidadID);

            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    response = controller.getDescripcionModalidad_Sitio(request.mdlUser.vchUsuario, id_mod, request.mdlagenda.intSitioID);
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en getAgenda: " + egU.Message, 3, "");
            }
            return response;
        }

        public string getDescripcionModalidad(AgendaRequest request)
        {
            string response = "";

            int id_mod = Convert.ToInt32(request.mdlagenda.intModalidadID);

            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    response = controller.getDescripcionModalidad(request.mdlUser.vchUsuario, id_mod);
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en getAgenda: " + egU.Message, 3, "");
            }
            return response;
        }

        #endregion agenda

        #region scheduler

        public List<clsConfScheduler> getConfScheduler_Sitio(ConfigSchedulerRequest request)
        {
            List<clsConfScheduler> response = new List<clsConfScheduler>();

            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    response = controller.getListConfigScheduler_Sito(request.mdlUser.vchUsuario, request.mdlConfScheduler.intSitioID);
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en getScheduler: " + egU.Message, 3, "");
            }
            return response;

        }

        public List<clsConfScheduler> getConfScheduler(ConfigSchedulerRequest request)
        {
            List<clsConfScheduler> response = new List<clsConfScheduler>();

            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    response = controller.getListConfigScheduler(request.mdlUser.vchUsuario);
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en getScheduler: " + egU.Message, 3, "");
            }
            return response;

        }

        public List<clsDiaSemana> getDiaSemanaConfScheduler_Sitio(ConfigScheduler_DiaSemanaRequest request, int idsitio)
        {
            List<clsDiaSemana> response = new List<clsDiaSemana>();

            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    response = controller.getListDiaSemana_sitio(request.mdlUser.vchUsuario, idsitio);
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en getScheduler: " + egU.Message, 3, "");
            }
            return response;

        }

        public List<clsDiaSemana> getDiaSemanaConfScheduler(ConfigScheduler_DiaSemanaRequest request)
        {
            List<clsDiaSemana> response = new List<clsDiaSemana>();

            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    response = controller.getListDiaSemana(request.mdlUser.vchUsuario);
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en getScheduler: " + egU.Message, 3, "");
            }
            return response;

        }

        public List<clsDiaFeriado> getDiaFeriadoConfScheduler_Sitio(ConfigScheduler_DiaFeriado request)
        {
            List<clsDiaFeriado> response = new List<clsDiaFeriado>();

            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    response = controller.getListDiaFeriado_Sitio(request.mdlUser.vchUsuario, request.mdlDiaFeriado.intSitioID);
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en getDiaFeriadoConfScheduler: " + egU.Message, 3, "");
            }
            return response;

        }

        public List<clsDiaFeriado> getDiaFeriadoConfScheduler(ConfigScheduler_DiaFeriado request)
        {
            List<clsDiaFeriado> response = new List<clsDiaFeriado>();

            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    response = controller.getListDiaFeriado(request.mdlUser.vchUsuario);
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en getDiaFeriadoConfScheduler: " + egU.Message, 3, "");
            }
            return response;

        }

        public List<clsHoraMuerta> getHoraMuertaConfScheduler_Sitio(ConfigScheduler_HoraMuertaRequest request)
        {
            List<clsHoraMuerta> response = new List<clsHoraMuerta>();

            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    response = controller.getListHorasMuertas_Sitio(request.mdlUser.vchUsuario, request.mdlHMScheduler.intSitioID);
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en getHoraMuertaConfScheduler: " + egU.Message, 3, "");
            }
            return response;

        }

        public List<clsHoraMuerta> getHoraMuertaConfScheduler(ConfigScheduler_HoraMuertaRequest request)
        {
            List<clsHoraMuerta> response = new List<clsHoraMuerta>();

            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    response = controller.getListHorasMuertas(request.mdlUser.vchUsuario);
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en getHoraMuertaConfScheduler: " + egU.Message, 3, "");
            }
            return response;

        }
      

        public bool UpdateDiaSemana_Sitio(ConfigScheduler_DiaSemanaRequest request)
        {
            bool bandera_update_agenda = false;
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    bandera_update_agenda = controller.UpdateDiaSemana_Sitio(request.mdlUser.vchUsuario, request.mdlDiaSemana.intSemanaID, request.mdlDiaSemana.bitActivo, request.mdlDiaSemana.intSitioID);
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en getAgenda: " + egU.Message, 3, "");
            }
            return bandera_update_agenda;
        }

        public bool UpdateDiaSemana(ConfigScheduler_DiaSemanaRequest request)
        {
            bool bandera_update_agenda = false;
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    bandera_update_agenda = controller.UpdateDiaSemana(request.mdlUser.vchUsuario, request.mdlDiaSemana.intSemanaID, request.mdlDiaSemana.bitActivo);
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en getAgenda: " + egU.Message, 3, "");
            }
            return bandera_update_agenda;
        }

        public bool UpdateHR_Activo(ConfigGeneralAgenda request)
        {
            bool bandera_update_agenda = false;
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    bandera_update_agenda = controller.UpdateHR_Activo_Sitio(request.mdlUser.vchUsuario, request.mdlgenconfagenda.tmeInicioDia, request.mdlgenconfagenda.tmeFinDia, request.mdlgenconfagenda.intSitioID);
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en getAgenda: " + egU.Message, 3, "");
            }
            return bandera_update_agenda;
        }

        public bool Update_Intervalo(ConfigGeneralAgenda request)
        {
            bool bandera_update_agenda = false;
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    bandera_update_agenda = controller.Update_Intervalo(request.mdlUser.vchUsuario, request.mdlgenconfagenda.intIntervalo);
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en getAgenda: " + egU.Message, 3, "");
            }
            return bandera_update_agenda;
        }

        public bool Set_DiaFeriado_Sitio(ConfigScheduler_DiaFeriado request)
        {
            bool bandera_update_agenda = false;
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    bandera_update_agenda = controller.Set_DiaFeriado_Sitio(request.mdlUser.vchUsuario, request.mdlDiaFeriado.datDia, request.mdlDiaFeriado.intSitioID);
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en getAgenda: " + egU.Message, 3, "");
            }
            return bandera_update_agenda;
        }

        public bool Set_DiaFeriado(ConfigScheduler_DiaFeriado request)
        {
            bool bandera_update_agenda = false;
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    bandera_update_agenda = controller.Set_DiaFeriado(request.mdlUser.vchUsuario, request.mdlDiaFeriado.datDia);
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en getAgenda: " + egU.Message, 3, "");
            }
            return bandera_update_agenda;
        }

        public bool Update_DiaFeriado(ConfigScheduler_DiaFeriado request)
        {
            bool bandera_update_agenda = false;
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    bandera_update_agenda = controller.Update_DiaFeriado(request.mdlUser.vchUsuario, request.mdlDiaFeriado.datDia);
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en getAgenda: " + egU.Message, 3, "");
            }
            return bandera_update_agenda;
        }

        public bool Actualizar_Estatus_DiaFeriado(ConfigScheduler_DiaFeriado request)
        {
            bool bandera_update_agenda = false;
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    bandera_update_agenda = controller.Actualizar_Estatus_DiaFeriado(request.mdlUser.vchUsuario, request.mdlDiaFeriado.datDia, request.mdlDiaFeriado.bitActivo);
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en getAgenda: " + egU.Message, 3, "");
            }
            return bandera_update_agenda;
        }

        public bool Eliminar_DiaFeriado(ConfigScheduler_DiaFeriado request)
        {
            bool bandera_delete_DF = false;
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    bandera_delete_DF = controller.Eliminar_DiaFeriado(request.mdlUser.vchUsuario, request.mdlDiaFeriado.datDia);
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error Eliminar Dia Feriado: " + egU.Message, 3, "");
            }
            return bandera_delete_DF;
        }

        public bool Eliminar_Hora_Muerta(ConfigScheduler_HoraMuertaRequest request)
        {
            bool bandera_delete_HM = false;
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    bandera_delete_HM = controller.Eliminar_HoraMuerta(request.mdlUser.vchUsuario, request.mdlHMScheduler.tmeInicio, request.mdlHMScheduler.tmeFin);
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en Eliminar Hora Muerta: " + egU.Message, 3, "");
            }
            return bandera_delete_HM;
        }

        public bool Set_HoraMuerta_Sitio(ConfigScheduler_HoraMuertaRequest request)
        {
            bool bandera_update_agenda = false;
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    bandera_update_agenda = controller.Set_HoraMuerta_sitio(request.mdlUser.vchUsuario, request.mdlHMScheduler.tmeInicio, request.mdlHMScheduler.tmeFin, request.mdlHMScheduler.intSitioID);
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en Set_HoraMuerta: " + egU.Message, 3, "");
            }
            return bandera_update_agenda;
        }

        public bool Set_HoraMuerta(ConfigScheduler_HoraMuertaRequest request)
        {
            bool bandera_update_agenda = false;
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    bandera_update_agenda = controller.Set_HoraMuerta(request.mdlUser.vchUsuario, request.mdlHMScheduler.tmeInicio, request.mdlHMScheduler.tmeFin);
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en Set_HoraMuerta: " + egU.Message, 3, "");
            }
            return bandera_update_agenda;
        }


        #endregion scheduler

        #region EventoCita
        public List<clsEventoCita> getListEventoCita(CitasRequest request)
        {
            List<clsEventoCita> response = new List<clsEventoCita>();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    response = controller.getListEventoCita(request.mdlUser.vchUsuario);
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en getAgenda: " + egU.Message, 3, "");
            }
            return response;
        }
		
		public List<clsEventoCita> getListCitas(CitasRequest request)
        {
            List<clsEventoCita> response = new List<clsEventoCita>();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    response = controller.getListCitas(request.mdlUser.vchUsuario, request.mdlevento.intModalidadID);
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en getAgenda: " + egU.Message, 3, "");
            }
            return response;
        }

        public List<clsEventoCita> getListCitas_Sitio(CitasRequest request)
        {
            List<clsEventoCita> response = new List<clsEventoCita>();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    response = controller.getListCitas_Sitio(request.mdlUser.vchUsuario, request.mdlevento.intModalidadID, request.mdlevento.intSitioID);
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en getAgenda: " + egU.Message, 3, "");
            }
            return response;
        }

        public List<clsEquipo> getCitaEquipo_Sitio(CitaNumEquipos request)
        {
            List<clsEquipo> response = new List<clsEquipo>();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    response = controller.getCitaEquipos_Sitio(request.mdlUser.vchUsuario, request.mdlequipo.intModalidadID, request.mdlequipo.intSitioID);
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en getAgenda: " + egU.Message, 3, "");
            }
            return response;
        }

        public List<clsEquipo> getCitaEquipo(CitaNumEquipos request)
        {
            List<clsEquipo> response = new List<clsEquipo>();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    response = controller.getCitaEquipos(request.mdlUser.vchUsuario, request.mdlequipo.intModalidadID);
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en getAgenda: " + egU.Message, 3, "");
            }
            return response;
        }

        public List<clsEventoCita> getListCitas_en_agenda_Sitio(CitasRequest request)
        {
            List<clsEventoCita> response = new List<clsEventoCita>();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    response = controller.getListCitas_en_agenda_Sitio(request.mdlUser.vchUsuario, request.mdlevento.intModalidadID, request.mdlevento.Start, request.mdlevento.intSitioID);
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en getAgenda: " + egU.Message, 3, "");
            }
            return response;
        }

        public List<clsEventoCita> getListCitas_en_agenda(CitasRequest request)
        {
            List<clsEventoCita> response = new List<clsEventoCita>();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    response = controller.getListCitas_en_agenda(request.mdlUser.vchUsuario, request.mdlevento.intModalidadID, request.mdlevento.Start);
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en getAgenda: " + egU.Message, 3, "");
            }
            return response;
        }

        //public EstudioResponse getEstudioDetalle_citaNueva(EstudioRequest request)
        //{
        //    EstudioResponse response = new EstudioResponse();
        //    try
        //    {
        //        if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
        //        {
        //            RISLiteDataAccess controller = new RISLiteDataAccess();
        //            response.mdlEstudio = controller.getEstudioDetalle_citaNueva(request.mdlUser.vchUsuario);
        //        }
        //    }
        //    catch (Exception egU)
        //    {
        //        Log.EscribeLog("Existe un error en getEstudioDetalle: " + egU.Message, 3, "");
        //    }
        //    return response;
        //}


        #endregion EventoCita

        #region Equipo
        public List<tbl_CAT_Equipo> getListEquipo(EquipoRequest request)
        {
            List<tbl_CAT_Equipo> response = new List<tbl_CAT_Equipo>();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    response = controller.getListEquipo(request.intModalidadID, request.intSitioID, request.mdlUser.vchUsuario);
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en getListEquipo: " + egU.Message, 3, "");
            }
            return response;
        }

        public EquipoResponse setEquipo(EquipoRequest request)
        {
            EquipoResponse response = new EquipoResponse();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    string mensaje = "";
                    response.Success = controller.setEquipo(request.mdlEquipo, request.mdlUser.vchUsuario, ref mensaje);
                    response.Mensaje = mensaje;
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en setEquipo: " + egU.Message, 3, "");
            }
            return response;
        }

        public EquipoResponse setActualizaEquipo(EquipoRequest request)
        {
            EquipoResponse response = new EquipoResponse();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    string mensaje = "";
                    response.Success = controller.setActualizaEquipo(request.mdlEquipo, request.mdlUser.vchUsuario, ref mensaje);
                    response.Mensaje = mensaje;
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en setActualizaEquipo: " + egU.Message, 3, "");
            }
            return response;
        }

        public EquipoResponse setEstatusEquipo(EquipoRequest request)
        {
            EquipoResponse response = new EquipoResponse();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    string mensaje = "";
                    response.Success = controller.setEstatusEquipo(request.intEquipoID, request.mdlUser.vchUsuario, ref mensaje);
                    response.Mensaje = mensaje;
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en setEstatusEquipo: " + egU.Message, 3, "");
            }
            return response;
        }
        #endregion Equipo

        #region Paciente
        public List<tbl_CAT_Genero> getListaGenero(CatalogoRequest request)
        {
            List<tbl_CAT_Genero> response = new List<tbl_CAT_Genero>();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    response = controller.getListaGenero(request.mdlUser.vchUsuario);
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en getListaGenero: " + egU.Message, 3, "");
            }
            return response;
        }

        public DireccionResponse getDireccionPaciente(DireccionRequest request)
        {
            DireccionResponse response = new DireccionResponse();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    string mensaje = "";
                    response.lstDireccion = controller.getDireccionPaciente(request.vchCodigoPostal, request.mdlUser.vchUsuario, ref mensaje);
                    response.Mensaje = mensaje;
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en getDireccionPaciente: " + egU.Message, 3, "");
            }
            return response;
        }

        public PacienteResponse setPaciente(PacienteRequest request)
        {
            PacienteResponse response = new PacienteResponse();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    string mensaje = "";
                    int intPacienteID = 0;
                    response.Success = controller.setPaciente(request.mdlPaciente, request.mdlDireccion, request.lstIdent, request.lstVarAdic, request.mdlUser.vchUsuario, ref mensaje, ref intPacienteID);
                    response.Mensaje = mensaje;
                    response.intPacienteID = intPacienteID;
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en setPaciente: " + egU.Message, 3, "");
            }
            return response;
        }

        public PacienteResponse getPacienteDetalle(PacienteRequest request)
        {
            PacienteResponse response = new PacienteResponse();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    string mensaje = "";
                    clsPaciente mdlPaciente = new clsPaciente();
                    clsDireccion mdlDireccion = new clsDireccion();
                    List<tbl_REL_IdentificacionPaciente> lstIden = new List<tbl_REL_IdentificacionPaciente>();
                    List<clsVarAcicionales> lstVarAdi = new List<clsVarAcicionales>();
                    response.Success = controller.getPacienteDetalle(request.intPacienteID, request.mdlUser.vchUsuario, ref mdlPaciente, ref mdlDireccion, ref lstIden, ref lstVarAdi, ref mensaje);
                    response.Mensaje = mensaje;
                    response.mdlDireccion = mdlDireccion;
                    response.mdlPaciente = mdlPaciente;
                    response.lstIden = lstIden;
                    response.lstVarAdi = lstVarAdi;
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en getPacienteDetalle: " + egU.Message, 3, "");
            }
            return response;
        }

        public PacienteResponse getPacienteAdicionales(PacienteRequest request)
        {
            PacienteResponse response = new PacienteResponse();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    List<tbl_REL_IdentificacionPaciente> lstIden = new List<tbl_REL_IdentificacionPaciente>();
                    List<clsVarAcicionales> lstVarAdi = new List<clsVarAcicionales>();
                    response.Success = controller.getPacienteAdicionales(request.intPacienteID, ref lstIden, ref lstVarAdi, request.mdlUser.vchUsuario);
                    response.lstIden = lstIden;
                    response.lstVarAdi = lstVarAdi;
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en getPacienteDetalle: " + egU.Message, 3, "");
            }
            return response;
        }

        public CitaNuevaResponse getCitaAdicionales(CitaNuevaRequest request)
        {
            CitaNuevaResponse response = new CitaNuevaResponse();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    response.mdlDetCita = controller.getCitaAdicionales(request.intCitaID, request.mdlUser.vchUsuario);
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en getCitaAdicionales: " + egU.Message, 3, "");
            }
            return response;
        }

        public List<stp_getDetalleCita_Result> get_stpDetalleCita(CitaNuevaRequest request)
        {
            List<stp_getDetalleCita_Result> response = new List<stp_getDetalleCita_Result>();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    response = controller.get_stpDetalleCita(request.intCitaID, request.mdlUser.vchUsuario);
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en get_stpDetalleCita: " + egU.Message, 3, "");
            }
            return response;
        }

        public bool getListaDetalleCita(AgendaRequest request, int idcita)
        {
            bool boolbandera_citaDet = false;
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    boolbandera_citaDet = controller.getListaDetalleCita(request.mdlUser.vchUsuario, idcita);
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en getAgenda: " + egU.Message, 3, "");
            }
            return boolbandera_citaDet;
        }

        public PacienteResponse getBusquedaPacientes(PacienteRequest request)
        {
            PacienteResponse response = new PacienteResponse();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    response.lstCadenas = controller.getBusquedaPacientes(request.busqueda, request.intSitioID, request.mdlUser.vchUsuario);
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en getBusquedaPacientes: " + egU.Message, 3, "");
            }
            return response;
        }

        public PacienteResponse getBusquedaPacientesMod(PacienteRequest request)
        {
            PacienteResponse response = new PacienteResponse();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    response.lstPacientes = controller.getBusquedaPacientesMod(request.busqueda, request.intSitioID, request.mdlUser.vchUsuario);
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en getBusquedaPacientesMod: " + egU.Message, 3, "");
            }
            return response;
        }

        public PacienteResponse getBusquedaPacientesList(PacienteRequest request)
        {
            PacienteResponse response = new PacienteResponse();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    response.lstPacientes = controller.getBusquedaPacientesList(request.busqueda, request.intSitioID, request.mdlUser.vchUsuario);
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en getBusquedaPacientesList: " + egU.Message, 3, "");
            }
            return response;
        }

        public PacienteResponse getBusquedaEstudio(PacienteRequest request)
        {
            PacienteResponse response = new PacienteResponse();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    response.lstCadenas = controller.getBusquedaEstudio(request.busqueda, request.mdlUser.vchUsuario);
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en getBusquedaEstudio: " + egU.Message, 3, "");
            }
            return response;
        }

        public EstudioResponse getEstudioDetalle(EstudioRequest request)
        {
            EstudioResponse response = new EstudioResponse();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    response.mdlEstudio = controller.getEstudioDetalle(request.mdlEstudio.intRelModPres, request.mdlUser.vchUsuario);
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en getEstudioDetalle: " + egU.Message, 3, "");
            }
            return response;
        }

        public AsignacionModalidadNuevaCita_Response getEstudioDetalle_citaNueva(AsignacionModalidadNuevaCita_Request request, int id_tabla_modalidad)
        {
            AsignacionModalidadNuevaCita_Response response = new AsignacionModalidadNuevaCita_Response();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    response.mdlEstudio = controller.getEstudioDetalle_citaNueva(request.mdlEstudio.intRelModPres, request.mdlUser.vchUsuario, id_tabla_modalidad);
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en getEstudioDetalle: " + egU.Message, 3, "");
            }
            return response;
        }

        public AsignacionModalidadModificacionCita_Response getEstudioDetalle_ModificacionCIta(CitaNuevaRequest_Modif_Cita request, string id_estudio, int id_tabla_modalidad)
        {
            AsignacionModalidadModificacionCita_Response response = new AsignacionModalidadModificacionCita_Response();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    response.mdlEstudio = controller.getEstudioDetalle_ModificacionCIta(request.mdlUser.vchUsuario, id_estudio, id_tabla_modalidad);
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en getEstudioDetalle_ModificacionCIta: " + egU.Message, 3, "");
            }
            return response;
        }

        public PacienteResponse setActualizaPaciente(PacienteRequest request)
        {
            PacienteResponse response = new PacienteResponse();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    string mensaje = "";
                    int intPacienteID = 0;
                    response.Success = controller.setActualizaPaciente(request.mdlPaciente, request.mdlDireccion, request.lstIdent, request.lstVarAdic, request.mdlUser.vchUsuario, ref mensaje);
                    response.Mensaje = mensaje;
                    response.intPacienteID = intPacienteID;
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en setActualizaPaciente: " + egU.Message, 3, "");
            }
            return response;
        }
        #endregion Paciente

        #region Indicacion
        public List<tbl_DET_IndicacionPrestacion> getListIndicacion(IndicacionRequest request)
        {
            List<tbl_DET_IndicacionPrestacion> response = new List<tbl_DET_IndicacionPrestacion>();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    response = controller.getListIndicacion(request.intPrestacionID, request.mdlUser.vchUsuario);
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en getListIndicacion: " + egU.Message, 3, "");
            }
            return response;
        }

        public IndicacionResponse setIndicacion(IndicacionRequest request)
        {
            IndicacionResponse response = new IndicacionResponse();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    string mensaje = "";
                    response.Success = controller.setIndicacion(request.mdlIndicacion, request.mdlUser.vchUsuario, ref mensaje);
                    response.Mensaje = mensaje;
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en setIndicacion: " + egU.Message, 3, "");
            }
            return response;
        }

        public IndicacionResponse setActualizaIndicacion(IndicacionRequest request)
        {
            IndicacionResponse response = new IndicacionResponse();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    string mensaje = "";
                    response.Success = controller.setActualizaIndicacion(request.mdlIndicacion, request.mdlUser.vchUsuario, ref mensaje);
                    response.Mensaje = mensaje;
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en setActualizaIndicacion: " + egU.Message, 3, "");
            }
            return response;
        }

        public IndicacionResponse setEstatusIndicacion(IndicacionRequest request)
        {
            IndicacionResponse response = new IndicacionResponse();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    string mensaje = "";
                    response.Success = controller.setEstatusIndicacion(request.intIndicacionID, request.mdlUser.vchUsuario, ref mensaje);
                    response.Mensaje = mensaje;
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en setEstatusIndicacion: " + egU.Message, 3, "");
            }
            return response;
        }
        #endregion Indicacion

        #region Restriccion
        public List<tbl_DET_Restriccion> getListRestriccion(RestriccionRequest request)
        {
            List<tbl_DET_Restriccion> response = new List<tbl_DET_Restriccion>();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    response = controller.getListRestriccion(request.intPrestacionID, request.mdlUser.vchUsuario);
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en getListRestriccion: " + egU.Message, 3, "");
            }
            return response;
        }

        public RestriccionResponse setRestriccion(RestriccionRequest request)
        {
            RestriccionResponse response = new RestriccionResponse();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    string mensaje = "";
                    response.Success = controller.setRestriccion(request.mdlRestriccion, request.mdlUser.vchUsuario, ref mensaje);
                    response.Mensaje = mensaje;
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en setRestriccion: " + egU.Message, 3, "");
            }
            return response;
        }

        public RestriccionResponse setActualizaRestriccion(RestriccionRequest request)
        {
            RestriccionResponse response = new RestriccionResponse();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    string mensaje = "";
                    response.Success = controller.setActualizaRestriccion(request.mdlRestriccion, request.mdlUser.vchUsuario, ref mensaje);
                    response.Mensaje = mensaje;
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en setActualizaRestriccion: " + egU.Message, 3, "");
            }
            return response;
        }

        public RestriccionResponse setEstatusRestriccion(RestriccionRequest request)
        {
            RestriccionResponse response = new RestriccionResponse();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    string mensaje = "";
                    response.Success = controller.setEstatusRestriccion(request.intReestriccionID, request.mdlUser.vchUsuario, ref mensaje);
                    response.Mensaje = mensaje;
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en setEstatusRestriccion: " + egU.Message, 3, "");
            }
            return response;
        }
        #endregion Restriccion

        #region Cuestionario
        public List<tbl_DET_Cuestionario> getListCuestionario(CuestionarioRequest request)
        {
            List<tbl_DET_Cuestionario> response = new List<tbl_DET_Cuestionario>();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    response = controller.getListCuestionario(request.intPrestacionID, request.mdlUser.vchUsuario);
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en getListCuestionario: " + egU.Message, 3, "");
            }
            return response;
        }

        public CuestionarioResponse setCuestionario(CuestionarioRequest request)
        {
            CuestionarioResponse response = new CuestionarioResponse();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    string mensaje = "";
                    response.Success = controller.setCuestionario(request.mdlCuestionario, request.mdlUser.vchUsuario, ref mensaje);
                    response.Mensaje = mensaje;
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en setCuestionario: " + egU.Message, 3, "");
            }
            return response;
        }

        public CuestionarioResponse setActualizaCuestionario(CuestionarioRequest request)
        {
            CuestionarioResponse response = new CuestionarioResponse();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    string mensaje = "";
                    response.Success = controller.setActualizaCuestionario(request.mdlCuestionario, request.mdlUser.vchUsuario, ref mensaje);
                    response.Mensaje = mensaje;
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en setActualizaCuestionario: " + egU.Message, 3, "");
            }
            return response;
        }

        public CuestionarioResponse setEstatusCuestionario(CuestionarioRequest request)
        {
            CuestionarioResponse response = new CuestionarioResponse();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    string mensaje = "";
                    response.Success = controller.setEstatusCuestionario(request.intCuestionarioID, request.mdlUser.vchUsuario, ref mensaje);
                    response.Mensaje = mensaje;
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en setEstatusCuestionario: " + egU.Message, 3, "");
            }
            return response;
        }
        #endregion Cuestionario

        #region Estudios
        public List<clsEstudioCita> getEstudiosPaciente(EstudioRequest request)
        {
            List<clsEstudioCita> response = new List<clsEstudioCita>();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    response = controller.getEstudiosPaciente(request.intPacienteID, request.mdlUser.vchUsuario);
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en getEstudiosPaciente: " + egU.Message, 3, "");
            }
            return response;
        }
        #endregion Estudios

        #region Adicionales
        public List<clsAdicionales> getAdicionales(AdicionalesRequest request)
        {
            List<clsAdicionales> response = new List<clsAdicionales>();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    response = controller.getAdicionales(request.intTipoAdicional, request.intSitioID, request.mdlUser.vchUsuario);
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en getAdicionales: " + egU.Message, 3, "");
            }
            return response;
        }

        public List<clsAdicionales> getAdicionalesPac(AdicionalesRequest request)
        {
            List<clsAdicionales> response = new List<clsAdicionales>();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    response = controller.getAdicionalesPac(request.intSitioID, request.intMasculino, request.intFemenino, request.intMayor, request.intMenor, request.mdlUser.vchUsuario);
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en getAdicionalesPac: " + egU.Message, 3, "");
            }
            return response;
        }

        public AdicionalesResponse setAdicionales(AdicionalesRequest request)
        {
            AdicionalesResponse response = new AdicionalesResponse();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    string mensaje = "";
                    response.Success = controller.setAdicionales(request.mdlAdicional, request.mdlUser.vchUsuario, ref mensaje);
                    response.Mensaje = mensaje;
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en setAdicionales: " + egU.Message, 3, "");
            }
            return response;
        }

        public AdicionalesResponse setActualizarAdicionales(AdicionalesRequest request)
        {
            AdicionalesResponse response = new AdicionalesResponse();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    string mensaje = "";
                    response.Success = controller.setActualizarAdicionales(request.mdlAdicional, request.mdlUser.vchUsuario, ref mensaje);
                    response.Mensaje = mensaje;
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en setActualizarAdicionales: " + egU.Message, 3, "");
            }
            return response;
        }

        public List<tbl_CAT_TipoBoton> getCATTipoBoton(AdicionalesRequest request)
        {
            List<tbl_CAT_TipoBoton> response = new List<tbl_CAT_TipoBoton>();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    response = controller.getCATTipoBoton(request.mdlUser.vchUsuario);
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en getCATTipoBoton: " + egU.Message, 3, "");
            }
            return response;
        }

        public List<tbl_CAT_TipoAdicional> getCATTipoAdicional(AdicionalesRequest request)
        {
            List<tbl_CAT_TipoAdicional> response = new List<tbl_CAT_TipoAdicional>();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    response = controller.getCATTipoAdicional(request.mdlUser.vchUsuario);
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en getCATTipoAdicional: " + egU.Message, 3, "");
            }
            return response;
        }

        public AdicionalesResponse setEstatusAdicional(AdicionalesRequest request)
        {
            AdicionalesResponse response = new AdicionalesResponse();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    string mensaje = "";
                    response.Success = controller.setEstatusAdicional(request.intAdicionalesID, request.mdlUser.vchUsuario, ref mensaje);
                    response.Mensaje = mensaje;
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en setEstatusAdicional: " + egU.Message, 3, "");
            }
            return response;
        }

        public List<clsAdicionales> getAdicionalesREL(AdicionalesRequest request)
        {
            List<clsAdicionales> response = new List<clsAdicionales>();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    response = controller.getAdicionalesREL(request.intAdicionalesID, request.mdlUser.vchUsuario);
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en getAdicionalesREL: " + egU.Message, 3, "");
            }
            return response;
        }

        public AdicionalesResponse setAdicionalesREL(AdicionalesRequest request)
        {
            AdicionalesResponse response = new AdicionalesResponse();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    string mensaje = "";
                    response.Success = controller.setAdicionalesREL(request.mdlAdicional, request.mdlUser.vchUsuario, ref mensaje);
                    response.Mensaje = mensaje;
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en setAdicionalesREL: " + egU.Message, 3, "");
            }
            return response;
        }

        #endregion Adicionales

        #region SugerenciasCita
        public List<stp_getCitaDisponible_Result> getSugerenciasCita(SugerenciasRequest request)
        {
            List<stp_getCitaDisponible_Result> response = new List<stp_getCitaDisponible_Result>();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    response = controller.getSugerenciasCita(request.mdlSug, request.mdlUser.vchUsuario);
                }
            }
            catch(Exception egS)
            {
                Log.EscribeLog("Existe un error en getSugerenciasCita: " + egS.Message, 3, "");
            }
            return response;
        }
        #endregion SugerenciasCita

        #region Import
        public EquipoResponse Set_Equipo_Import(EquipoRequest request)
        {
            EquipoResponse response = new EquipoResponse();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    string mensaje = "";
                    response.Success = controller.Set_Equipo_Import(request.mdlEquipo, request.mdlUser.vchUsuario, ref mensaje);
                    response.Mensaje = mensaje;
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en setEquipo: " + egU.Message, 3, "");
            }
            return response;
        
        }
        
        public PrestacionResponse Set_Prestacion_Import(PrestacionImportRequest request)
        {
            PrestacionResponse response = new PrestacionResponse();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    string mensaje = "";
                    response.Success = controller.Set_Prestacion_Import(request.mdlPres, request.mdlDetCuest, request.mdlDetRest, request.mdlDetIndPres,
                        request.mdlUser.vchUsuario, ref mensaje);
                    response.Mensaje = mensaje;
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en Set_Prestacion_Import: " + egU.Message, 3, "");
            }
            return response;
        }

        #endregion

        #region InsertCita
        public CitaNuevaResponse setCitaNueva(CitaNuevaRequest request)
        {
            CitaNuevaResponse response = new CitaNuevaResponse();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    string mensaje = "";
                    tbl_MST_Cita cita = new tbl_MST_Cita();
                    response.Success = controller.setCitaNueva(request.mdlPaciente, request.lstAdicionales, request.lstEstudios, request.mdlUser.vchUsuario, ref mensaje, ref cita);
                    response.cita = cita;
                    response.Mensaje = mensaje;
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en setCitaNueva: " + egU.Message, 3, "");
            }
            return response;
        }
        #endregion InsertCita

        #region CitaReporte
        public List<stp_getCitaReporte_Result> getCitaReporte(CitaReporteRequest request)
        {
            List<stp_getCitaReporte_Result> response = new List<stp_getCitaReporte_Result>();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    response = controller.getCitaReporte(request.intCitaId, request.mdlUser.vchUsuario);
                }
            }
            catch (Exception egS)
            {
                Log.EscribeLog("Existe un error en getCitaReporte: " + egS.Message, 3, "");
            }
            return response;
        }

        public List<clsRepIndicacion> getIndicaciones(CitaReporteRequest request)
        {
            List<clsRepIndicacion> response = new List<clsRepIndicacion>();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    response = controller.getIndicaciones(request.intPrestacionID, request.mdlUser.vchUsuario);
                }
            }
            catch (Exception egS)
            {
                Log.EscribeLog("Existe un error en getIndicaciones: " + egS.Message, 3, "");
            }
            return response;
        }

        public List<clsRepRestriccion> getRestricciones(CitaReporteRequest request)
        {
            List<clsRepRestriccion> response = new List<clsRepRestriccion>();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    response = controller.getRestricciones(request.intPrestacionID, request.mdlUser.vchUsuario);
                }
            }
            catch (Exception egS)
            {
                Log.EscribeLog("Existe un error en getRestricciones: " + egS.Message, 3, "");
            }
            return response;
        }
        #endregion CitaReporte

        #region CitasGrid
        public List<stp_getCitas_Result> getCitas(CitaReporteRequest request)
        {
            List<stp_getCitas_Result> response = new List<stp_getCitas_Result>();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    response = controller.getCitas(request.mdlEstudio,request.mdlUser.intSitioID, request.mdlUser.vchUsuario);
                }
            }
            catch (Exception egS)
            {
                Log.EscribeLog("Existe un error en getCitas: " + egS.Message, 3, "");
            }
            return response;
        }

        public CitaReporteResponse setEstatusEstudio(CitaReporteRequest request)
        {
            CitaReporteResponse response = new CitaReporteResponse();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    string mensaje = "";
                    response.Success = controller.setEstatusEstudio(request.intEstudioID, request.intEstatusID, request.mdlUser.vchUsuario, ref mensaje);
                    response.Mensaje = mensaje;
                }
            }
            catch (Exception egS)
            {
                Log.EscribeLog("Existe un error en setEstatusEstudio: " + egS.Message, 3, "");
            }
            return response;
        }

        public void updateEstatusCitaAutomatica(string user)
        {
            try
            {
                RISLiteDataAccess controller = new RISLiteDataAccess();
                controller.updateEstatusCitaAutomatica(user);
            }
            catch (Exception egS)
            {
                Log.EscribeLog("Existe un error en updateEstatusCitaAutomatica: " + egS.Message, 3, "");
            }
        }
        #endregion CitasGrid

        #region ListadeTrabajo
        public List<clsListaDeTrabajo> getListaDeTrabajo(AgendaRequest request)
        {
            List<clsListaDeTrabajo> response = new List<clsListaDeTrabajo>();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    response = controller.getListadeTrabajo(request.mdlUser.vchUsuario, request.mdlagenda.intSitioID);
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en getAgenda: " + egU.Message, 3, "");
            }
            return response;
        }

        public bool UpdateEstatus_Cita(EstatusCita request, int idestatus, int idsitio)
        {
            bool bandera_actualizacion = false;
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    bandera_actualizacion = controller.UpdateEstatus_Cita(request.mdlUser, idsitio, idestatus);
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en getAgenda: " + egU.Message, 3, "");
            }
            return bandera_actualizacion;
        }

        public List<clsEventoCita> getListEventoCita_SoloSitio(CitasRequest request)
        {
            List<clsEventoCita> response = new List<clsEventoCita>();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    response = controller.getListEventoCita_SoloSitio(request.mdlUser.vchUsuario, request.mdlevento.intSitioID);
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en getAgenda: " + egU.Message, 3, "");
            }
            return response;
        }

        #endregion

        #region Perfil
        public PerfilResponse setPerfil(PerfilRequest request)
        {
            PerfilResponse response = new PerfilResponse();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    string mensaje = "";
                    response.success = controller.setPerfil(request.mdlPerfil, request.intVariableID, request.mdlUser.vchUsuario, ref mensaje);
                    response.mensaje = mensaje;
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en setPerfil: " + egU.Message, 3, "");
            }
            return response;
        }
        #endregion Perfil

        #region estadistica

        public List<clsConfAgenda> get_modalidades_estadistica(AgendaRequest request)
        {
            List<clsConfAgenda> response = new List<clsConfAgenda>();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    response = controller.get_modalidades_estadistica(request.mdlUser.vchUsuario, request.mdlagenda.intSitioID);
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en get_modalidades_estadistica: " + egU.Message, 3, "");
            }
            return response;
        }

        public List<clsUsuario> get_personal_Estadistica(AgendaRequest request)
        {
            List<clsUsuario> response = new List<clsUsuario>();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    response = controller.get_personal_Estadistica(request.mdlUser.vchUsuario, request.mdlagenda.intSitioID);
                }
            }
            catch (Exception egU)        
            {
                Log.EscribeLog("Existe un error en get_personal_Estadistica: " + egU.Message, 3, "");
            }
            return response;
        }

        public List<clsSitio> get_sitio_estadistica(AgendaRequest request)
        {
            List<clsSitio> response = new List<clsSitio>();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    response = controller.get_sitio_estadistica(request.mdlUser.vchUsuario);
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en getAgenda: " + egU.Message, 3, "");
            }
            return response;
        }

        public List<clsEstatusEstudio> get_sitio_estatus_estudio(AgendaRequest request)
        {
            List<clsEstatusEstudio> response = new List<clsEstatusEstudio>();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    response = controller.get_sitio_estatus_estudio(request.mdlUser.vchUsuario);               
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en getAgenda: " + egU.Message, 3, "");
            }
            return response;
        }



        public List<clsGraficaModalidad> stp_get_Datos_Modalidad_Estadistica(AgendaRequest request, int idsitio, int idmodalidad, string fechainicio, string fechafin, string estatusid)
        {
            List<clsGraficaModalidad> response = new List<clsGraficaModalidad>();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    response = controller.stp_get_Datos_Modalidad_Estadistica(request.mdlUser.vchUsuario, idsitio, idmodalidad, fechainicio, fechafin, estatusid);
                }
            }
            catch (Exception egU)
            {            
                Log.EscribeLog("Existe un error en stp_get_Datos_Modalidad_Estadistica: " + egU.Message, 3, "");
            }
            return response;
        }

        public List<clsGraficaUsuario> stp_get_Datos_Usuarios_Estadistica(AgendaRequest request, int idsitio, int idmodalidad, string fechainicio, string fechafin, string estatusid)
        {
            List<clsGraficaUsuario> response = new List<clsGraficaUsuario>();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    response = controller.stp_get_Datos_Usuarios_Estadistica(request.mdlUser.vchUsuario, idsitio, idmodalidad, fechainicio, fechafin, estatusid);
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en stp_get_Datos_Usuarios_Estadistica: " + egU.Message, 3, "");
            }
            return response;
        }
        #endregion

        public CitaNuevaResponse ModificacionCita(CitaNuevaRequest request, int _cita)
        {
            CitaNuevaResponse response = new CitaNuevaResponse();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    string mensaje = "";
                    tbl_MST_Cita cita = new tbl_MST_Cita();
                    response.Success = controller.ModificacionCita(request.mdlPaciente, request.lstAdicionales, request.lstEstudios, request.mdlUser.vchUsuario, ref mensaje, ref cita, _cita);
                    response.cita = cita;
                    response.Mensaje = mensaje;
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en setCitaNueva: " + egU.Message, 3, "");
            }
            return response;
        }

        #region arribo
        public ArriboResponse getDetalleCitaPaciente(ArriboRequest request)
        {
            ArriboResponse response = new ArriboResponse();
            try
            {
                RISLiteDataAccess controller = new RISLiteDataAccess();
                string mensaje = "";
                clsEstudioCita mdlCita = new clsEstudioCita();
                List<clsEstudio> lstEstudios = new List<clsEstudio>();
                response.Success = controller.getDetalleCitaPaciente(request.intCitaID, request.mdlUser.vchUsuario, ref mensaje, ref mdlCita, ref lstEstudios);
                response.mdlCita = mdlCita;
                response.lstEstudio = lstEstudios;
                response.mensaje = mensaje;
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en getDetalleCitaPaciente: " + egU.Message, 3, "");
            }
            return response;
        }

        public ArriboResponse setActualizaEstudioEstatus(ArriboRequest request)
        {
            ArriboResponse response = new ArriboResponse();
            try
            {
                RISLiteDataAccess controller = new RISLiteDataAccess();
                string mensaje = "";
                response.Success = controller.setActualizaEstudioEstatus(request.intEstudioID, request.intEstatusID, request.mdlUser.vchUsuario, ref mensaje);
                response.mensaje = mensaje;
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en setActualizaEstudioEstatus: " + egU.Message, 3, "");
            }
            return response;
        }
        #endregion arribo

    }
}
