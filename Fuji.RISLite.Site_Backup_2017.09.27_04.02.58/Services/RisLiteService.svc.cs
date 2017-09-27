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
            catch(Exception egU)
            {
                Log.EscribeLog("Existe un error en getUser: " + egU.Message, 3, "");
            }
            return response;
        }

        #region catalogo
        public List<tbl_CAT_Catalogo> getListCatalogos(CatalogoRequest request)
        {
            List<tbl_CAT_Catalogo> response = new List<tbl_CAT_Catalogo>();
            try
            {
                if(Security.ValidateToken(request.mdlUser.Token,request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
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
                    response = controller.getListCatalogo(request.mdlCat.intCatalogoID, request.mdlUser.vchUsuario);
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
                    response = controller.updateCatalogoEstatus(request.mdlCat.intCatalogoID, request.mdlCat.bitActivo,request.mdlCat.intPrimaryKey, request.mdlUser.vchUsuario);
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
                    response = controller.setItemCatalogo(request.mdlCat.intCatalogoID, request.mdlCat.vchValor, request.mdlUser.vchUsuario);
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
                    response = controller.getListVistas(request.mdlUser.intTipoUsuario,request.mdlUser.vchUsuario);
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
                    response.Success = controller.getConfigSitio(request.mdlUser.vchUsuario,ref mdl,ref mensaje);
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
                    response.Success = controller.getConfigEmail(request.mdlUser.vchUsuario, ref mdl, ref mensaje);
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
                    response = controller.getVariablesAdicionalPaciente(request.mdlUser.vchUsuario);
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
                    response = controller.getVariablesAdicionalCita(request.mdlUser.vchUsuario);
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
                    response = controller.getVariablesAdicionalID(request.mdlUser.vchUsuario);
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
                    response.Success = controller.setAgregarVariable(request.intTipoVariable,request.mdlVariable.vchNombreVarAdi,request.mdlUser.vchUsuario,ref mensaje);
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
                    response.Success = controller.setActualizarVariable(request.intTipoVariable,request.mdlVariable.intVariableAdiID,request.mdlVariable.vchNombreVarAdi,request.mdlUser.vchUsuario, ref mensaje);
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
                    response.Success = controller.setUsuario(request.mdlAdminUser,request.mdlUser.vchUsuario, ref mensaje);
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
                    response = controller.getListPrestacion(request.intModalidad,request.mdlUser.vchUsuario);
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
                    response = controller.getListAgenda(request.mdlUser.vchUsuario);
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
                    bandera_update_agenda = controller.UpdateAgenda(request.mdlUser.vchUsuario, request.mdlagenda.intModalidadID, request.mdlagenda.vchCodigo, request.mdlagenda.vchModalidad, request.mdlagenda.vchColor);
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
                    bandera_update_agenda = controller.UpdateHR_Activo(request.mdlUser.vchUsuario, request.mdlgenconfagenda.tmeInicioDia, request.mdlgenconfagenda.tmeFinDia);
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
                    response = controller.getListEquipo(request.intModalidadID, request.mdlUser.vchUsuario);
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

        public PacienteResponse getBusquedaPacientes(PacienteRequest request)
        {
            PacienteResponse response = new PacienteResponse();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    response.lstCadenas = controller.getBusquedaPacientes(request.busqueda, request.mdlUser.vchUsuario);
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en getBusquedaPacientes: " + egU.Message, 3, "");
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
                    response.lstPacientes = controller.getBusquedaPacientesList(request.busqueda, request.mdlUser.vchUsuario);
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
                    response = controller.getAdicionales(request.intTipoAdicional, request.mdlUser.vchUsuario);
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en getAdicionales: " + egU.Message, 3, "");
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

        #endregion Adicionales

    }
}
