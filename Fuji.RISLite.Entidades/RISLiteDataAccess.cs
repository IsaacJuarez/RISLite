using Fuji.RISLite.Entidades.DataBase;
using Fuji.RISLite.Entidades.Extensions;
using Fuji.RISLite.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fuji.RISLite.DataAccess
{


    public class RISLiteDataAccess
    {
        RISLiteEntities dbRisDA;

        public bool getUser(string user, ref clsUsuario Usuario, ref List<clsVistasUsuarios> lstVistas)
        {
            bool Success = false;
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    if (dbRisDA.tbl_CAT_Usuario.Any(x => (bool)x.bitActivo && x.vchUsuario.Trim().ToUpper() == user.ToUpper().Trim()))
                    {
                        var query = (from _user in dbRisDA.tbl_CAT_Usuario
                                     join catTipo in dbRisDA.tbl_CAT_TipoUsuario on _user.intTipoUsuario equals catTipo.intTipoUsuario
                                     where _user.vchUsuario.Trim().ToUpper() == user.Trim().ToUpper() && (bool)_user.bitActivo
                                     select new
                                     {
                                         intUsuarioID = _user.intUsuarioID,
                                         intTipoUsuario = _user.intTipoUsuario,
                                         vchNombre = _user.vchNombre,
                                         vchUsuario = _user.vchUsuario,
                                         vchTipoUsuario = catTipo.vchTipoUsuario,
                                         bitActivo = _user.bitActivo,
                                         datFecha = _user.datFecha,
                                         vchUserAdmin = _user.vchUserAdmin
                                     }
                           ).First();
                        if (query != null)
                        {
                            if (Success = query.intUsuarioID > 0 ? true : false)
                            {
                                Usuario.intUsuarioID = query.intUsuarioID;
                                Usuario.intTipoUsuario = (int)query.intTipoUsuario;
                                Usuario.vchTipoUsuario = query.vchTipoUsuario;
                                Usuario.vchNombre = query.vchNombre;
                                Usuario.vchUsuario = query.vchUsuario;
                                Usuario.bitActivo = (bool)query.bitActivo;
                                Usuario.datFecha = (DateTime)query.datFecha;
                                Usuario.vchUserAdmin = query.vchUserAdmin;
                                Usuario.Token = Security.Encrypt(query.intUsuarioID + "-" + query.vchUsuario);
                                lstVistas = getListVistas(Usuario.intTipoUsuario);
                            }
                        }
                    }
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en getUser: " + egU.Message, 3, user);
            }
            return Success;
        }

        public List<clsVistasUsuarios> getListVistas(int intTipoUsuario)
        {
            List<clsVistasUsuarios> lstReturn = new List<clsVistasUsuarios>();
            try
            {
                using(dbRisDA = new RISLiteEntities())
                {
                    if(dbRisDA.tbl_REL_TipoUsuarioBoton.Any(x=>x.intTipoUsuario == intTipoUsuario && (bool)x.bitActivo))
                    {
                        var query = dbRisDA.stp_getListaPaginas(intTipoUsuario).ToList();
                        if(query!= null)
                        {
                            if (query.Count > 0)
                            {
                                foreach(var item in query)
                                {
                                    clsVistasUsuarios mdl = new clsVistasUsuarios();
                                    mdl.bitActivo = (bool)item.bitActivo;
                                    mdl.intBotonID = (int)item.intBotonID;
                                    mdl.intTipoUsuario = (int)item.intTipoUsuario;
                                    mdl.intVistaID = (int)item.intVistaID;
                                    mdl.vchbtnImagenID = item.vchbtnImagenID;
                                    mdl.vchIconFontAwesome = item.vchIconFontAwesome;
                                    mdl.vchIdentificador = item.vchIdentificador;
                                    mdl.vchNombreBoton = item.vchNombreBoton;
                                    mdl.vchNombreVista = item.vchNombreVista;
                                    mdl.vchTipoUsuario = item.vchTipoUsuario;
                                    mdl.vchVistaIdentificador = item.vchVistaIdentificador;
                                    lstReturn.Add(mdl);
                                }
                            }
                        }
                    }
                }
            }
            catch(Exception egLV)
            {
                Log.EscribeLog("Existe un error en : " + egLV.Message, 3, "");
            }
            return lstReturn;
        }



        #region catalogos

        public List<tbl_CAT_Catalogo> getListCatalogos(string user)
        {
            List<tbl_CAT_Catalogo> lst = new List<tbl_CAT_Catalogo>();
            try
            {
                using(dbRisDA = new RISLiteEntities())
                {
                    if (dbRisDA.tbl_CAT_Catalogo.Any())
                    {
                        var query = dbRisDA.tbl_CAT_Catalogo.ToList();
                        if(query != null)
                        {
                            if(query.Count > 0)
                            {
                                lst = query;
                            }
                        }
                    }
                }
            }
           catch(Exception egLC)
            {
                Log.EscribeLog("Existe un error en getListCatalogos: " + egLC.Message, 3, user);
            }
            return lst;
        }

        public List<stp_getListCatalogo_Result> getListCatalogo(int intCatalogoId, string user)
        {
            List<stp_getListCatalogo_Result> list = new List<stp_getListCatalogo_Result>();
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                   var query =   dbRisDA.stp_getListCatalogo(intCatalogoId).ToList();
                    if(query != null)
                    {
                        if (query.Count > 0)
                        {
                            list = query;
                        }
                    }
                }
            }
            catch(Exception egLC)
            {
                Log.EscribeLog("Existe un error en getListCatalogos: " + egLC.Message, 3, user);
            }
            return list;
        }

        public stp_updateCatEstatus_Result updateCatalogoEstatus(int intCatalogoID,bool biActivo,int intPrimaryKey, string user)
        {
            stp_updateCatEstatus_Result result = new stp_updateCatEstatus_Result();
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    var query = dbRisDA.stp_updateCatEstatus(intCatalogoID, biActivo, intPrimaryKey);
                    if (query != null)
                    {
                        result = query.First();
                    }
                }
            }
            catch(Exception eUCE)
            {
                result = null;
                Log.EscribeLog("Existe un error en getListCatalogos: " + eUCE.Message, 3, user);
            }
            return result;
        }

        public stp_updateCatalogo_Result updateCatalogo(int intCatalogoID, int intPrimaryKey, string vchValor, string user)
        {
            stp_updateCatalogo_Result response = new stp_updateCatalogo_Result();
            try
            {
                using(dbRisDA = new RISLiteEntities())
                {
                    var query = dbRisDA.stp_updateCatalogo(intCatalogoID, intPrimaryKey, vchValor);
                    if(query != null)
                    {
                        response = query.First();
                    }
                }
            }
            catch(Exception eUC)
            {
                response = null;
                Log.EscribeLog("Existe un error en updateCatalogo: " + eUC.Message, 3, user);
            }
            return response;
        }

        public stp_setItemCatalogo_Result setItemCatalogo(int intCatalogoID, string vchValor, string user)
        {
            stp_setItemCatalogo_Result response = new stp_setItemCatalogo_Result();
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    var query = dbRisDA.stp_setItemCatalogo(intCatalogoID, vchValor, user);
                    if (query != null)
                    {
                        response = query.First();
                    }
                }
            }
            catch (Exception eUC)
            {
                response = null;
                Log.EscribeLog("Existe un error en setItemCatalogo: " + eUC.Message, 3, user);
            }
            return response;
        }

        public List<clsCatalogo> getTipoUsuario(string user)
        {
            List<clsCatalogo> lstResponse = new List<clsCatalogo>();
            try
            {
                using(dbRisDA = new RISLiteEntities())
                {
                    if (dbRisDA.tbl_CAT_TipoUsuario.Any(x=>(bool)x.bitActivo))
                    {
                        var query = dbRisDA.tbl_CAT_TipoUsuario.Where(x => (bool)x.bitActivo).ToList();
                        if(query!= null)
                        {
                            if(query.Count>0)
                            {
                                foreach(var item in query)
                                {
                                    clsCatalogo mdl = new clsCatalogo();
                                    mdl.intCatalogoID = item.intTipoUsuario;
                                    mdl.vchNombre = item.vchTipoUsuario;
                                    lstResponse.Add(mdl);
                                }
                            }
                        }
                    }
                }
            }
            catch(Exception egC)
            {
                Log.EscribeLog("Existe un error en getTipoUsuario: " + egC.Message, 2, user);
            }
            return lstResponse;
        }

        public List<clsCatalogo> getListaBoton(string user)
        {
            List<clsCatalogo> lstResponse = new List<clsCatalogo>();
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    if (dbRisDA.tbl_CAT_Botones.Any(x => (bool)x.bitActivo))
                    {
                        var query = dbRisDA.tbl_CAT_Botones.Where(x => (bool)x.bitActivo).ToList();
                        if (query != null)
                        {
                            if (query.Count > 0)
                            {
                                foreach (var item in query)
                                {
                                    clsCatalogo mdl = new clsCatalogo();
                                    mdl.intCatalogoID = item.intBotonID;
                                    mdl.vchNombre = item.vchNombreBoton;
                                    lstResponse.Add(mdl);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception egC)
            {
                Log.EscribeLog("Existe un error en getListaBoton: " + egC.Message, 2, user);
            }
            return lstResponse;
        }

        public List<clsCatalogo> getListaVista(string user)
        {
            List<clsCatalogo> lstResponse = new List<clsCatalogo>();
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    if (dbRisDA.tbl_CAT_Vistas.Any(x => (bool)x.bitActivo))
                    {
                        var query = dbRisDA.tbl_CAT_Vistas.Where(x => (bool)x.bitActivo).ToList();
                        if (query != null)
                        {
                            if (query.Count > 0)
                            {
                                foreach (var item in query)
                                {
                                    clsCatalogo mdl = new clsCatalogo();
                                    mdl.intCatalogoID = item.intVistaID;
                                    mdl.vchNombre = item.vchNombreVista;
                                    lstResponse.Add(mdl);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception egC)
            {
                Log.EscribeLog("Existe un error en getListaVista: " + egC.Message, 2, user);
            }
            return lstResponse;
        }

        public List<stp_getListaPaginas_Result> getListVistas(int intTipoUsuarioID,string user)
        {
            List<stp_getListaPaginas_Result> result = new List<stp_getListaPaginas_Result>();
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    var query = dbRisDA.stp_getListaPaginas(intTipoUsuarioID).ToList();
                    if(query!= null)
                    {
                        if (query.Count > 0)
                        {
                            result.AddRange(query);
                        }
                    }
                }
            }
            catch(Exception egLV)
            {
                Log.EscribeLog("Existe un error en getListVistas: " + egLV.Message, 3, user);
            }
            return result;
        }
        #endregion catalogos

        #region equipo
        public List<clsEquipo> getListaEquipos(string user)
        {
            List<clsEquipo> lst = new List<clsEquipo>();
            try
            {
                using(dbRisDA =  new RISLiteEntities())
                {
                    if (dbRisDA.tbl_CAT_Equipo.Any())
                    {


                        var query = from equipo in dbRisDA.tbl_CAT_Equipo
                                    join mod in dbRisDA.tbl_CAT_Modalidad on equipo.intModalidadID equals mod.intModalidadID
                                    select new
                                    {
                                        intEquipo = equipo.intEquipoID,
                                        intModalidadID = equipo.intModalidadID,
                                        vchModalidad = mod.vchModalidad,
                                        vchNombreEquipo = equipo.vchNombreEquipo,
                                        vchCodigoEquipo = equipo.vchCodigoEquipo,
                                        bitActivo = equipo.bitActivo,
                                        datFecha = equipo.datFecha,
                                        vchUserAdmin = equipo.vchUserAdmin
                                    };
                        if(query!= null)
                        {
                            if(query.Count() >0)
                            {
                                foreach(var item in query)
                                {
                                    clsEquipo mdl = new clsEquipo();
                                    mdl.intEquipoID = item.intEquipo;
                                    mdl.intModalidadID = (int)item.intModalidadID;
                                    mdl.vchModalidad = item.vchModalidad;
                                    mdl.vchNombreEquipo = item.vchNombreEquipo;
                                    mdl.vchCodigoEquipo = item.vchCodigoEquipo;
                                    mdl.bitActivo = (bool)item.bitActivo;
                                    mdl.datFecha = (DateTime)item.datFecha;
                                    mdl.vchUserAdmin = item.vchUserAdmin;
                                    lst.Add(mdl);
                                }
                            }
                        }
                    }
                                
                }
            }
            catch(Exception gLE)
            {
                Log.EscribeLog("Existe un error en getListaEquipos: " + gLE.Message, 3, user);
            }
            return lst;
        }

        #endregion equipo

        #region tecnicos
        public List<clsUsuario> getListTecnico(string user)
        {
            List<clsUsuario> lst = new List<clsUsuario>();
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    if (dbRisDA.tbl_CAT_Usuario.Any(x => x.intTipoUsuario == 3))
                    {
                        var query = dbRisDA.tbl_CAT_Usuario.Where(x => x.intTipoUsuario == 3).ToList();
                        if (query != null)
                        {
                            if (query.Count > 0)
                            {
                                foreach (var item in query)
                                {
                                    clsUsuario mdl = new clsUsuario();
                                    mdl.intTipoUsuario = (int)item.intTipoUsuario;
                                    mdl.intUsuarioID = item.intUsuarioID;
                                    mdl.bitActivo = (bool)item.bitActivo;
                                    mdl.datFecha = (DateTime)item.datFecha;
                                    mdl.vchNombre = item.vchNombre;
                                    mdl.vchUserAdmin = item.vchUserAdmin;
                                    mdl.vchUsuario = item.vchUsuario;
                                    lst.Add(mdl);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception eLT)
            {
                Log.EscribeLog("Existe un error en getListTecnico: " + eLT.Message, 3, user);
            }
            return lst;
        }
        #endregion tecnicos

        #region configSitio
        public bool getConfigSitio(string user, ref tbl_MST_ConfiguracionSistema mdl, ref string mensaje)
        {
            bool valido = false;
            tbl_MST_ConfiguracionSistema mdlConfig = new tbl_MST_ConfiguracionSistema();
            try
            {
                using(dbRisDA = new RISLiteEntities())
                {
                    if (dbRisDA.tbl_MST_ConfiguracionSistema.Any())
                    {
                        mdl = dbRisDA.tbl_MST_ConfiguracionSistema.First();
                        valido = true;
                    }
                    else
                    {
                        valido = false;
                        mensaje = "No existe una configuración para el sitio.";
                    }
                }
            }
            catch(Exception egCS)
            {
                valido = false;
                Log.EscribeLog("Existe un error en getConfigSitio: " + egCS.Message, 3, user);
            }
            return valido;
        }

        public bool setConfigSitio(tbl_MST_ConfiguracionSistema mdlConfigSistem, string user, ref string mensaje)
        {
            bool valido = false;
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    if (!dbRisDA.tbl_MST_ConfiguracionSistema.Any())
                    {
                        dbRisDA.tbl_MST_ConfiguracionSistema.Add(mdlConfigSistem);
                        dbRisDA.SaveChanges();
                        valido = true;
                    }
                }
            }
            catch (Exception esAC)
            {
                valido = false;
                mensaje = esAC.Message;
                Log.EscribeLog("Existe un error en setActualizarConfigSitio: " + esAC.Message, 3, user);
            }
            return valido;
        }

        public bool setActualizarConfigSitio(tbl_MST_ConfiguracionSistema mdlConfigSistem, string user, ref string mensaje)
        {
            bool valido = false;
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    if (dbRisDA.tbl_MST_ConfiguracionSistema.Any(x => x.intConfigID == mdlConfigSistem.intConfigID))
                    {
                        tbl_MST_ConfiguracionSistema mdl = new tbl_MST_ConfiguracionSistema();
                        mdl = dbRisDA.tbl_MST_ConfiguracionSistema.First(x => x.intConfigID == mdlConfigSistem.intConfigID);
                        if (mdl != null)
                        {
                            mdl.datFecha = DateTime.Now;
                            mdl.vbLogoSitio = mdlConfigSistem.vbLogoSitio;
                            mdl.vchDominio = mdlConfigSistem.vchDominio;
                            mdl.vchNombreSitio = mdlConfigSistem.vchNombreSitio;
                            mdl.vchPrefijo = mdlConfigSistem.vchPrefijo;
                            mdl.vchUserAdmin = user;
                            mdl.vchVersion = mdlConfigSistem.vchVersion;
                            dbRisDA.SaveChanges();
                            valido = true;
                        }
                    }
                }
            }
            catch (Exception esAC)
            {
                valido = false;
                mensaje = esAC.Message;
                Log.EscribeLog("Existe un error en setActualizarConfigSitio: " + esAC.Message, 3, user);
            }
            return valido;
        }
        #endregion configSitio

        #region ConfigEmail
        public bool getConfigEmail(string user, ref tbl_Conf_CorreoSitio mdl, ref string mensaje)
        {
            bool valido = false;
            tbl_Conf_CorreoSitio mdlConfig = new tbl_Conf_CorreoSitio();
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    if (dbRisDA.tbl_Conf_CorreoSitio.Any())
                    {
                        mdl = dbRisDA.tbl_Conf_CorreoSitio.First();
                        valido = true;
                    }
                    else
                    {
                        valido = false;
                        mensaje = "No existe una configuración para el correo del sitio.";
                    }
                }
            }
            catch (Exception egCS)
            {
                valido = false;
                Log.EscribeLog("Existe un error en getConfigEmail: " + egCS.Message, 3, user);
            }
            return valido;
        }

        public bool setConfigEmail(tbl_Conf_CorreoSitio mdlConfigSistem, string user, ref string mensaje)
        {
            bool valido = false;
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    if (!dbRisDA.tbl_Conf_CorreoSitio.Any())
                    {
                        dbRisDA.tbl_Conf_CorreoSitio.Add(mdlConfigSistem);
                        dbRisDA.SaveChanges();
                        valido = true;
                    }
                }
            }
            catch (Exception esAC)
            {
                valido = false;
                mensaje = esAC.Message;
                Log.EscribeLog("Existe un error en setConfigEmail: " + esAC.Message, 3, user);
            }
            return valido;
        }

        public bool setActualizarConfigEmail(tbl_Conf_CorreoSitio mdlConfigSistem, string user, ref string mensaje)
        {
            bool valido = false;
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    if (dbRisDA.tbl_Conf_CorreoSitio.Any(x => x.intConfigCorreoID == mdlConfigSistem.intConfigCorreoID))
                    {
                        tbl_Conf_CorreoSitio mdl = new tbl_Conf_CorreoSitio();
                        mdl = dbRisDA.tbl_Conf_CorreoSitio.First(x => x.intConfigCorreoID == mdlConfigSistem.intConfigCorreoID);
                        if (mdl != null)
                        {
                            mdl.bitActivo = mdlConfigSistem.bitActivo;
                            mdl.BitEnableSsl = mdlConfigSistem.BitEnableSsl;
                            mdl.datFecha = mdlConfigSistem.datFecha;
                            mdl.intPort = mdlConfigSistem.intPort;
                            mdl.vchCorreo = mdlConfigSistem.vchCorreo;
                            mdl.vchHost = mdlConfigSistem.vchHost;
                            mdl.vchPassword = mdlConfigSistem.vchPassword;
                            mdl.vchUserAdmin = mdlConfigSistem.vchUserAdmin;
                            mdl.vchUsuarioCorreo = mdlConfigSistem.vchUsuarioCorreo;
                            dbRisDA.SaveChanges();
                            valido = true;
                        }
                    }
                }
            }
            catch (Exception esAC)
            {
                valido = false;
                mensaje = esAC.Message;
                Log.EscribeLog("Existe un error en setActualizarConfigEmail: " + esAC.Message, 3, user);
            }
            return valido;
        }
        #endregion ConfigEmail

        #region AdminUsers
        public List<clsUsuario> getListaUsuarios(string user)
        {
            List<clsUsuario> lst = new List<clsUsuario>();
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    if (dbRisDA.tbl_CAT_Usuario.Any(x => (bool)x.bitActivo))
                    {
                        var query = (from _user in dbRisDA.tbl_CAT_Usuario
                                     join cat in dbRisDA.tbl_CAT_TipoUsuario on _user.intTipoUsuario equals cat.intTipoUsuario
                                     select new
                                     {
                                         intTipoUsuario = _user.intTipoUsuario,
                                         intUsuarioID = _user.intUsuarioID,
                                         bitActivo = _user.bitActivo,
                                         datFecha = _user.datFecha,
                                         vchNombre = _user.vchNombre,
                                         vchUserAdmin = _user.vchUserAdmin,
                                         vchUsuario = _user.vchUsuario,
                                         vchTipoUsuario = cat.vchTipoUsuario
                                     }).ToList();
                        if (query != null)
                        {
                            if (query.Count > 0)
                            {
                                foreach (var item in query)
                                {
                                    clsUsuario mdl = new clsUsuario();
                                    mdl.intTipoUsuario = (int)item.intTipoUsuario;
                                    mdl.intUsuarioID = item.intUsuarioID;
                                    mdl.bitActivo = (bool)item.bitActivo;
                                    mdl.datFecha = (DateTime)item.datFecha;
                                    mdl.vchNombre = item.vchNombre;
                                    mdl.vchUserAdmin = item.vchUserAdmin;
                                    mdl.vchUsuario = item.vchUsuario;
                                    mdl.vchTipoUsuario = item.vchTipoUsuario;
                                    lst.Add(mdl);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception eLT)
            {
                Log.EscribeLog("Existe un error en getListaUsuarios: " + eLT.Message, 3, user);
            }
            return lst;
        }

        public bool setUsuario(clsUsuario usuario, string user, ref string mensaje)
        {
            bool valido = false;
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    if (!dbRisDA.tbl_CAT_Usuario.Any(x => x.vchUsuario.ToUpper() == usuario.vchUsuario.ToUpper() && (bool)x.bitActivo))
                    {
                        tbl_CAT_Usuario mdlUser = new tbl_CAT_Usuario();
                        mdlUser.bitActivo = usuario.bitActivo;
                        mdlUser.datFecha = usuario.datFecha;
                        mdlUser.intTipoUsuario = usuario.intTipoUsuario;
                        mdlUser.vchNombre = usuario.vchNombre;
                        mdlUser.vchUserAdmin = user;
                        mdlUser.vchUsuario = usuario.vchUsuario;
                        dbRisDA.tbl_CAT_Usuario.Add(mdlUser);
                        dbRisDA.SaveChanges();
                        valido = true;
                    }
                    else
                    {
                        mensaje = "El usuario ya existe.";
                        valido = false;
                    }
                }
            }
            catch (Exception eSU)
            {
                valido = false;
                mensaje = eSU.Message;
                Log.EscribeLog("Existe un error en setUser: " + eSU.Message, 3, user);
            }
            return valido;
        }

        public bool setActualizaUsuario(clsUsuario usuario, string user, ref string mensaje)
        {
            bool valido = false;
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    if (dbRisDA.tbl_CAT_Usuario.Any(x => x.intUsuarioID == usuario.intUsuarioID))
                    {
                        tbl_CAT_Usuario mdlUser = new tbl_CAT_Usuario();
                        mdlUser = dbRisDA.tbl_CAT_Usuario.First(x => x.intUsuarioID == usuario.intUsuarioID);
                        mdlUser.bitActivo = usuario.bitActivo;
                        mdlUser.datFecha = usuario.datFecha;
                        mdlUser.vchNombre = usuario.vchNombre;
                        mdlUser.vchUserAdmin = user;
                        mdlUser.vchUsuario = usuario.vchUsuario;
                        dbRisDA.SaveChanges();
                        valido = true;
                    }
                    else
                    {
                        mensaje = "El usuario no existe.";
                        valido = false;
                    }
                }
            }
            catch (Exception eSU)
            {
                valido = false;
                mensaje = eSU.Message;
                Log.EscribeLog("Existe un error en setActualizaUsuario: " + eSU.Message, 3, user);
            }
            return valido;
        }

        public bool setEstatusUsuario(int intUsuarioID, string user, ref string mensaje)
        {
            bool valido = false;
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    if (dbRisDA.tbl_CAT_Usuario.Any(x => x.intUsuarioID == intUsuarioID))
                    {
                        tbl_CAT_Usuario mdlUser = new tbl_CAT_Usuario();
                        mdlUser = dbRisDA.tbl_CAT_Usuario.First(x => x.intUsuarioID == intUsuarioID);
                        mdlUser.bitActivo = !mdlUser.bitActivo;
                        mdlUser.datFecha = DateTime.Today;
                        mdlUser.vchUserAdmin = user;
                        dbRisDA.SaveChanges();
                        valido = true;
                    }
                    else
                    {
                        mensaje = "El usuario no existe.";
                        valido = false;
                    }
                }
            }
            catch (Exception eSU)
            {
                valido = false;
                mensaje = eSU.Message;
                Log.EscribeLog("Existe un error en setEstatusUsuario: " + eSU.Message, 3, user);
            }
            return valido;
        }
        #endregion AdminUsers

        #region varadicionales
        public List<clsVarAcicionales> getVariablesAdicionalPaciente(string user)
        {
            List<clsVarAcicionales> lstreturn = new List<clsVarAcicionales>();
            try
            {
                using(dbRisDA = new RISLiteEntities())
                {
                    if (dbRisDA.tbl_CONFIG_VariablesAdiPaciente.Any())
                    {
                        var query = dbRisDA.tbl_CONFIG_VariablesAdiPaciente.ToList();
                        if(query != null)
                        {
                            if(query.Count > 0)
                            {
                                foreach(var item in query)
                                {
                                    clsVarAcicionales mdl = new clsVarAcicionales();
                                    mdl.bitActivo = (bool)item.bitActivo;
                                    mdl.datFecha = (DateTime)item.datFecha;
                                    mdl.intVariableAdiID = item.intVarAdiPacienteID;
                                    mdl.vchNombreVarAdi = item.vchNombreVariable;
                                    mdl.vchUserAdmin = item.vchUserAdmin;
                                    lstreturn.Add(mdl);
                                }
                            }
                        }
                    }
                }
            }
            catch(Exception egV)
            {
                Log.EscribeLog("Existe un error en getVariablesAdicionalPaciente: " + egV.Message, 3, user);
            }
            return lstreturn;
        }

        public List<clsVarAcicionales> getVariablesAdicionalCita(string user)
        {
            List<clsVarAcicionales> lstreturn = new List<clsVarAcicionales>();
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    if (dbRisDA.tbl_CONFIG_VariablesAdiCita.Any())
                    {
                        var query = dbRisDA.tbl_CONFIG_VariablesAdiCita.ToList();
                        if (query != null)
                        {
                            if (query.Count > 0)
                            {
                                foreach (var item in query)
                                {
                                    clsVarAcicionales mdl = new clsVarAcicionales();
                                    mdl.bitActivo = (bool)item.bitActivo;
                                    mdl.datFecha = (DateTime)item.datFecha;
                                    mdl.intVariableAdiID = item.intVarAdiCitaID;
                                    mdl.vchNombreVarAdi = item.vchNombreVariable;
                                    mdl.vchUserAdmin = item.vchUserAdmin;
                                    lstreturn.Add(mdl);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception egV)
            {
                Log.EscribeLog("Existe un error en getVariablesAdicionalCita: " + egV.Message, 3, user);
            }
            return lstreturn;
        }

        public bool setAgregarVariable(int iTipoVariable, string vchVarible, string user, ref string mensaje)
        {
            bool valido = false;
            try
            {
                switch (iTipoVariable)
                {
                    case 1: //Variable adicional Paciente
                        using(dbRisDA = new RISLiteEntities())
                        {
                            if(!dbRisDA.tbl_CONFIG_VariablesAdiPaciente.Any(x=> x.vchNombreVariable.ToUpper() == vchVarible))
                            {
                                tbl_CONFIG_VariablesAdiPaciente mdl = new tbl_CONFIG_VariablesAdiPaciente();
                                mdl.vchNombreVariable = vchVarible;
                                mdl.datFecha = DateTime.Now;
                                mdl.bitActivo = true;
                                mdl.vchUserAdmin = user;
                                dbRisDA.tbl_CONFIG_VariablesAdiPaciente.Add(mdl);
                                dbRisDA.SaveChanges();
                                valido = true;
                            }
                            else
                            {
                                mensaje = "La variable ya existe. Favor de verificar.";
                            }
                        }
                        break;
                    case 2: //Variable adicional Cita
                        using (dbRisDA = new RISLiteEntities())
                        {
                            if (!dbRisDA.tbl_CONFIG_VariablesAdiCita.Any(x => x.vchNombreVariable.ToUpper() == vchVarible))
                            {
                                tbl_CONFIG_VariablesAdiCita mdl = new tbl_CONFIG_VariablesAdiCita();
                                mdl.vchNombreVariable = vchVarible;
                                mdl.datFecha = DateTime.Now;
                                mdl.bitActivo = true;
                                mdl.vchUserAdmin = user;
                                dbRisDA.tbl_CONFIG_VariablesAdiCita.Add(mdl);
                                dbRisDA.SaveChanges();
                                valido = true;
                            }
                            else
                            {
                                mensaje = "La variable ya existe. Favor de verificar.";
                            }
                        }
                        break;
                    case 3: //Variable adicional Estudio
                        break;
                }
            }
            catch(Exception esAV)
            {
                valido = false;
                mensaje = esAV.Message;
                Log.EscribeLog("Existe un error en setAgregarVariable: " + esAV.Message, 3, user);
            }
            return valido;
        }

        public bool setActualizarVariable(int iTipoVariable, int intVarAdiID, string vchVarible, string user, ref string mensaje)
        {
            bool valido = false;
            try
            {
                switch (iTipoVariable)
                {
                    case 1: //Variable adicional Paciente
                        using (dbRisDA = new RISLiteEntities())
                        {
                            if (dbRisDA.tbl_CONFIG_VariablesAdiPaciente.Any(x => x.intVarAdiPacienteID == intVarAdiID))
                            {
                                tbl_CONFIG_VariablesAdiPaciente mdl = new tbl_CONFIG_VariablesAdiPaciente();
                                mdl = dbRisDA.tbl_CONFIG_VariablesAdiPaciente.First(x => x.intVarAdiPacienteID == intVarAdiID);
                                mdl.vchNombreVariable = vchVarible;
                                mdl.vchUserAdmin = user;
                                mdl.datFecha = DateTime.Now;
                                dbRisDA.SaveChanges();
                                valido = true;
                            }
                            else
                            {
                                mensaje = "No existe la variable para actualizar.";
                            }
                        }
                        break;
                    case 2: //Variable adicional Cita
                        using (dbRisDA = new RISLiteEntities())
                        {
                            if (dbRisDA.tbl_CONFIG_VariablesAdiCita.Any(x => x.intVarAdiCitaID == intVarAdiID))
                            {
                                tbl_CONFIG_VariablesAdiCita mdl = new tbl_CONFIG_VariablesAdiCita();
                                mdl = dbRisDA.tbl_CONFIG_VariablesAdiCita.First(x => x.intVarAdiCitaID == intVarAdiID);
                                mdl.vchNombreVariable = vchVarible;
                                mdl.vchUserAdmin = user;
                                mdl.datFecha = DateTime.Now;
                                dbRisDA.SaveChanges();
                                valido = true;
                            }
                            else
                            {
                                mensaje = "No existe la variable para actualizar.";
                            }
                        }
                        break;
                    case 3: //Variable adicional Estudio
                        break;
                }
            }
            catch (Exception esAV)
            {
                valido = false;
                mensaje = esAV.Message;
                Log.EscribeLog("Existe un error en setAgregarVariable: " + esAV.Message, 3, user);
            }
            return valido;
        }

        public bool setEstatusVariable(int intTipoVariable, string user, ref string mensaje)
        {
            bool valido = false;
            try
            {
                switch (intTipoVariable)
                {
                    case 1: //Variable adicional Paciente
                        using (dbRisDA = new RISLiteEntities())
                        {
                            if (dbRisDA.tbl_CONFIG_VariablesAdiPaciente.Any(x => x.intVarAdiPacienteID == intTipoVariable))
                            {
                                tbl_CONFIG_VariablesAdiPaciente mdl = new tbl_CONFIG_VariablesAdiPaciente();
                                mdl = dbRisDA.tbl_CONFIG_VariablesAdiPaciente.First(x => x.intVarAdiPacienteID == intTipoVariable);
                                mdl.bitActivo = !mdl.bitActivo;
                                mdl.vchUserAdmin = user;
                                mdl.datFecha = DateTime.Now;
                                dbRisDA.SaveChanges();
                                valido = true;
                            }
                            else
                            {
                                mensaje = "No existe la variable para actualizar.";
                            }
                        }
                        break;
                    case 2: //Variable adicional Cita
                        using (dbRisDA = new RISLiteEntities())
                        {
                            if (dbRisDA.tbl_CONFIG_VariablesAdiCita.Any(x => x.intVarAdiCitaID == intTipoVariable))
                            {
                                tbl_CONFIG_VariablesAdiCita mdl = new tbl_CONFIG_VariablesAdiCita();
                                mdl = dbRisDA.tbl_CONFIG_VariablesAdiCita.First(x => x.intVarAdiCitaID == intTipoVariable);
                                mdl.bitActivo = !mdl.bitActivo;
                                mdl.vchUserAdmin = user;
                                mdl.datFecha = DateTime.Now;
                                dbRisDA.SaveChanges();
                                valido = true;
                            }
                            else
                            {
                                mensaje = "No existe la variable para actualizar.";
                            }
                        }
                        break;
                    case 3: //Variable adicional Estudio
                        break;
                }
            }
            catch (Exception esAV)
            {
                valido = false;
                mensaje = esAV.Message;
                Log.EscribeLog("Existe un error en setAgregarVariable: " + esAV.Message, 3, user);
            }
            return valido;
        }
        #endregion varadicionales

        #region Prestacion

        public List<tbl_CAT_Modalidad> getListModalidades(string user)
        {
            List<tbl_CAT_Modalidad> lst = new List<tbl_CAT_Modalidad>();
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    if (dbRisDA.tbl_CAT_Modalidad.Any(x=> (bool)x.bitActivo))
                    {
                        var query = dbRisDA.tbl_CAT_Modalidad.Where(x => (bool)x.bitActivo).ToList();
                        if (query != null)
                        {
                            if (query.Count > 0)
                            {
                                lst = query;
                            }
                        }
                    }
                }
            }
            catch (Exception egLC)
            {
                Log.EscribeLog("Existe un error en getListModalidades: " + egLC.Message, 3, user);
            }
            return lst;
        }

        public List<clsPrestacion> getListPrestacion(int intModalidadID, string user)
        {
            List<clsPrestacion> list = new List<clsPrestacion>();
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    var query = dbRisDA.stp_getPrestacionModalidad(intModalidadID).ToList();
                    if (query != null)
                    {
                        if (query.Count > 0)
                        {
                            foreach(var item in query)
                            {
                                clsPrestacion mdl = new clsPrestacion();
                                mdl.bitActivo = (bool)item.bitActivo;
                                mdl.intDuracionMin = (int)item.intDuracionMin;
                                mdl.intModalidadID = (int)item.intModalidadID;
                                mdl.intPrestacionID = (int)item.intPrestacionID;
                                mdl.intRELModPres = item.intRELModPres;
                                mdl.vchModalidad = item.vchModalidad;
                                mdl.vchPrestacion = item.vchPrestacion;
                                list.Add(mdl);
                            }
                        }
                    }
                }
            }
            catch (Exception egLC)
            {
                Log.EscribeLog("Existe un error en getListPrestacion: " + egLC.Message, 3, user);
            }
            return list;
        }

        public bool setPrestacion(clsPrestacion prestacion, string user, ref string mensaje)
        {
            bool valido = false;
            try
            {
                bool validoCat = false;
                tbl_CAT_Prestacion mdlCat = new tbl_CAT_Prestacion();
                using (dbRisDA = new RISLiteEntities())
                {   
                    //Primero en cat
                    if(!dbRisDA.tbl_CAT_Prestacion.Any(x=> x.vchPrestacion.ToUpper() == prestacion.vchPrestacion.ToUpper()))
                    {
                        validoCat = true;
                        mdlCat.bitActivo = prestacion.bitActivo;
                        mdlCat.datFecha = DateTime.Now;
                        mdlCat.intDuracionMin = prestacion.intDuracionMin;
                        mdlCat.vchPrestacion = prestacion.vchPrestacion;
                        mdlCat.vchUserAdmin = user;
                        dbRisDA.tbl_CAT_Prestacion.Add(mdlCat);
                        dbRisDA.SaveChanges();
                    }
                    else
                    {
                        validoCat = false;
                        mensaje += " Ya existe la prestación.";
                        valido = false;
                    }
                }

                if (validoCat && mdlCat.intPrestacionID > 0)
                {
                    using (dbRisDA = new RISLiteEntities())
                    {
                        //Luego en REL
                        if (!dbRisDA.tbl_REL_ModalidadPrestacion.Any(x => x.intModalidadID == prestacion.intModalidadID && x.intPrestacionID == mdlCat.intPrestacionID))
                        {
                            tbl_REL_ModalidadPrestacion mdlMod = new tbl_REL_ModalidadPrestacion();
                            mdlMod.bitActivo = prestacion.bitActivo;
                            mdlMod.datFecha = DateTime.Now;
                            mdlMod.intPrestacionID = mdlCat.intPrestacionID;
                            mdlMod.intModalidadID = prestacion.intModalidadID;
                            mdlMod.vchUserAdmin = user;
                            dbRisDA.tbl_REL_ModalidadPrestacion.Add(mdlMod);
                            dbRisDA.SaveChanges();
                            valido = true;
                        }
                        else
                        {
                            mensaje += " La relación ya existe.";
                            valido = false;
                        }
                    }
                }
            }
            catch (Exception eSU)
            {
                valido = false;
                mensaje += eSU.Message;
                Log.EscribeLog("Existe un error en setUser: " + eSU.Message, 3, user);
            }
            return valido;
        }

        public bool setActualizaPrestacion(clsPrestacion prestacion, string user, ref string mensaje)
        {
            bool valido = false;
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    if (dbRisDA.tbl_CAT_Prestacion.Any(x => x.intPrestacionID == prestacion.intPrestacionID))
                    {
                        if (!dbRisDA.tbl_CAT_Prestacion.Any(x => x.intPrestacionID == prestacion.intPrestacionID))
                        {
                            tbl_CAT_Prestacion mdlCat = new tbl_CAT_Prestacion();
                            mdlCat = dbRisDA.tbl_CAT_Prestacion.First(x => x.intPrestacionID == prestacion.intPrestacionID);
                            mdlCat.bitActivo = prestacion.bitActivo;
                            mdlCat.datFecha = DateTime.Now;
                            mdlCat.intDuracionMin = prestacion.intDuracionMin;
                            mdlCat.vchUserAdmin = user;
                            mdlCat.vchPrestacion = prestacion.vchPrestacion;
                            dbRisDA.SaveChanges();
                            valido = true;
                        }
                        else
                        {
                            mensaje = "El nombre de la prestación ya existe.";
                            valido = false;
                        }
                    }
                    else
                    {
                        mensaje = "No existe la prestación.";
                        valido = false;
                    }
                }
            }
            catch (Exception eSU)
            {
                valido = false;
                mensaje = eSU.Message;
                Log.EscribeLog("Existe un error en setActualizaUsuario: " + eSU.Message, 3, user);
            }
            return valido;
        }

        public bool setEstatusPrestacion(int intRELModPres, string user, ref string mensaje)
        {
            bool valido = false;
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    if (dbRisDA.tbl_REL_ModalidadPrestacion.Any(x => x.intRELModPres == intRELModPres))
                    {
                        tbl_REL_ModalidadPrestacion mdlUser = new tbl_REL_ModalidadPrestacion();
                        mdlUser = dbRisDA.tbl_REL_ModalidadPrestacion.First(x => x.intRELModPres == intRELModPres);
                        mdlUser.bitActivo = !mdlUser.bitActivo;
                        mdlUser.datFecha = DateTime.Today;
                        mdlUser.vchUserAdmin = user;
                        dbRisDA.SaveChanges();
                        valido = true;
                    }
                    else
                    {
                        mensaje = "El usuario no existe.";
                        valido = false;
                    }
                }
            }
            catch (Exception eSU)
            {
                valido = false;
                mensaje = eSU.Message;
                Log.EscribeLog("Existe un error en setEstatusUsuario: " + eSU.Message, 3, user);
            }
            return valido;
        }

        #endregion Prestacion


    }
}
