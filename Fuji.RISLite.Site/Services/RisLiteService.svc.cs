﻿using Fuji.RISLite.DataAccess;
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
    }
}