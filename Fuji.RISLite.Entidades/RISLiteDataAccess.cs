using Fuji.RISLite.Entidades.DataBase;
using Fuji.RISLite.Entidades.Extensions;
using Fuji.RISLite.Entities;
using QRCoder;
using Spire.Barcode;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Globalization;
using System.IO;
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
                Log.EscribeLog("Usuario de consulta: " + user, 3, "TEST");
                using (dbRisDA = new RISLiteEntities())
                {
                    if (dbRisDA.tbl_CAT_Usuario.Any(x => (bool)x.bitActivo && x.vchUsuario.Trim().ToUpper() == user.ToUpper().Trim()))
                    {
                        var query = (from _user in dbRisDA.tbl_CAT_Usuario
                                     join catTipo in dbRisDA.tbl_CAT_TipoUsuario on _user.intTipoUsuario equals catTipo.intTipoUsuario
                                     join sitio in dbRisDA.tbl_CAT_Sitio on _user.intSitioID equals sitio.intSitioID into sitioDef
                                     from x in sitioDef.DefaultIfEmpty()
                                     where _user.vchUsuario.Trim().ToUpper() == user.Trim().ToUpper() && (bool)_user.bitActivo
                                     select new
                                     {
                                         intUsuarioID = _user.intUsuarioID,
                                         intTipoUsuario = _user.intTipoUsuario,
                                         vchNombre = _user.vchNombre,
                                         vchUsuario = _user.vchUsuario,
                                         vchTipoUsuario = catTipo.vchTipoUsuario,
                                         bitActivo = _user.bitActivo,
                                         vchEmail = _user.vchEmail == null ? "" : _user.vchEmail,
                                         datFecha = _user.datFecha,
                                         vchUserAdmin = _user.vchUserAdmin,
                                         intSitioId = _user.intSitioID == null ? 0 : _user.intSitioID,
                                         vchSitio = x.vchNombreSitio == null ? "" : x.vchNombreSitio
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
                                Usuario.vchEmail = query.vchEmail;
                                Usuario.bitActivo = (bool)query.bitActivo;
                                Usuario.datFecha = (DateTime)query.datFecha;
                                Usuario.vchUserAdmin = query.vchUserAdmin;
                                Usuario.intSitioID = (int)query.intSitioId;
                                Usuario.vchSitio = query.vchSitio;
                                Usuario.Token = Security.Encrypt(query.intUsuarioID + "-" + query.vchUsuario);
                                lstVistas = getListVistas(Usuario.intTipoUsuario);
                            }
                        }
                    }
                }
            }
            catch (Exception egU)
            {
                Success = false;
                Log.EscribeLog("Existe un error en getUser: " + egU.Message, 3, user);
            }
            Log.EscribeLog("Usuario de consulta: " + Usuario.intUsuarioID, 3, "TEST");
            return Success;
        }

        public bool getLoginUser(string user, string pass, ref clsUsuario Usuario, ref List<clsVistasUsuarios> lstVistas, ref string mensaje)
        {
            bool Success = false;
            try
            {
                Log.EscribeLog("Usuario de consulta: " + user, 3, "TEST");
                using (dbRisDA = new RISLiteEntities())
                {
                    if (dbRisDA.tbl_CAT_Usuario.Any(x => (bool)x.bitActivo && x.vchUsuario.Trim().ToUpper() == user.ToUpper().Trim()))
                    {
                        string password = pass;
                        if (dbRisDA.tbl_CAT_Usuario.Any(x => (bool)x.bitActivo && x.vchUsuario.Trim().ToUpper() == user.ToUpper().Trim() && x.vchPassword == password))
                        {

                            var query = (from _user in dbRisDA.tbl_CAT_Usuario
                                         join catTipo in dbRisDA.tbl_CAT_TipoUsuario on _user.intTipoUsuario equals catTipo.intTipoUsuario
                                         join sitio in dbRisDA.tbl_CAT_Sitio on _user.intSitioID equals sitio.intSitioID into sitioDef
                                         from x in sitioDef.DefaultIfEmpty()
                                         where _user.vchUsuario.Trim().ToUpper() == user.Trim().ToUpper() && (bool)_user.bitActivo
                                         select new
                                         {
                                             intUsuarioID = _user.intUsuarioID,
                                             intTipoUsuario = _user.intTipoUsuario,
                                             vchNombre = _user.vchNombre,
                                             vchUsuario = _user.vchUsuario,
                                             vchTipoUsuario = catTipo.vchTipoUsuario,
                                             vchRutaIcono = _user.vchRutaIcono,
                                             bitActivo = _user.bitActivo,
                                             vchEmail = _user.vchEmail == null ? "" : _user.vchEmail,
                                             datFecha = _user.datFecha,
                                             vchUserAdmin = _user.vchUserAdmin,
                                             intSitioId = _user.intSitioID == null ? 0 : _user.intSitioID,
                                             vchSitio = x.vchNombreSitio == null ? "" : x.vchNombreSitio
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
                                    Usuario.vchRutaIcono = query.vchRutaIcono;
                                    Usuario.vchEmail = query.vchEmail;
                                    Usuario.bitActivo = (bool)query.bitActivo;
                                    Usuario.datFecha = (DateTime)query.datFecha;
                                    Usuario.vchUserAdmin = query.vchUserAdmin;
                                    Usuario.intSitioID = (int)query.intSitioId;
                                    Usuario.vchSitio = query.vchSitio;
                                    Usuario.Token = Security.Encrypt(query.intUsuarioID + "-" + query.vchUsuario);
                                    lstVistas = getListVistas(Usuario.intTipoUsuario);
                                }
                            }
                        }
                        else
                        {
                            Success = false;
                            mensaje = "Contraseña incorrecta.";
                        }
                    }
                    else
                    {
                        Success = false;
                        mensaje = "El usuario no existe";
                    }
                }
            }
            catch (Exception egU)
            {
                Success = false;
                mensaje = egU.Message;
                Log.EscribeLog("Existe un error en getUser: " + egU.Message, 3, user);
            }
            Log.EscribeLog("Usuario de consulta: " + Usuario.intUsuarioID, 3, "TEST");
            return Success;
        }

        public List<clsVistasUsuarios> getListVistas(int intTipoUsuario)
        {
            List<clsVistasUsuarios> lstReturn = new List<clsVistasUsuarios>();
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    if (dbRisDA.tbl_REL_TipoUsuarioBoton.Any(x => x.intTipoUsuario == intTipoUsuario && (bool)x.bitActivo))
                    {
                        var query = dbRisDA.stp_getListaPaginas(intTipoUsuario).ToList();
                        if (query != null)
                        {
                            if (query.Count > 0)
                            {
                                foreach (var item in query)
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
            catch (Exception egLV)
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
                using (dbRisDA = new RISLiteEntities())
                {
                    if (dbRisDA.tbl_CAT_Catalogo.Any())
                    {
                        var query = dbRisDA.tbl_CAT_Catalogo.ToList();
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
                Log.EscribeLog("Existe un error en getListCatalogos: " + egLC.Message, 3, user);
            }
            return lst;
        }

        public List<stp_getListCatalogo_Result> getListCatalogo(int intCatalogoId, int intSitioID, string user)
        {
            List<stp_getListCatalogo_Result> list = new List<stp_getListCatalogo_Result>();
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    var query = dbRisDA.stp_getListCatalogo(intCatalogoId, intSitioID).ToList();
                    if (query != null)
                    {
                        if (query.Count > 0)
                        {
                            list = query;
                        }
                    }
                }
            }
            catch (Exception egLC)
            {
                Log.EscribeLog("Existe un error en getListCatalogos: " + egLC.Message, 3, user);
            }
            return list;
        }

        public stp_updateCatEstatus_Result updateCatalogoEstatus(int intCatalogoID, bool biActivo, int intPrimaryKey, string user)
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
            catch (Exception eUCE)
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
                using (dbRisDA = new RISLiteEntities())
                {
                    var query = dbRisDA.stp_updateCatalogo(intCatalogoID, intPrimaryKey, vchValor);
                    if (query != null)
                    {
                        response = query.First();
                    }
                }
            }
            catch (Exception eUC)
            {
                response = null;
                Log.EscribeLog("Existe un error en updateCatalogo: " + eUC.Message, 3, user);
            }
            return response;
        }

        public stp_setItemCatalogo_Result setItemCatalogo(int intCatalogoID, int intSitioID, string vchValor, string user)
        {
            stp_setItemCatalogo_Result response = new stp_setItemCatalogo_Result();
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    var query = dbRisDA.stp_setItemCatalogo(intCatalogoID, intSitioID, vchValor, user);
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
                using (dbRisDA = new RISLiteEntities())
                {
                    if (dbRisDA.tbl_CAT_TipoUsuario.Any(x => (bool)x.bitActivo))
                    {
                        var query = dbRisDA.tbl_CAT_TipoUsuario.Where(x => (bool)x.bitActivo).ToList();
                        if (query != null)
                        {
                            if (query.Count > 0)
                            {
                                foreach (var item in query)
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
            catch (Exception egC)
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

        public List<stp_getListaPaginas_Result> getListVistas(int intTipoUsuarioID, string user)
        {
            List<stp_getListaPaginas_Result> result = new List<stp_getListaPaginas_Result>();
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    var query = dbRisDA.stp_getListaPaginas(intTipoUsuarioID).ToList();
                    if (query != null)
                    {
                        if (query.Count > 0)
                        {
                            result.AddRange(query);
                        }
                    }
                }
            }
            catch (Exception egLV)
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
                using (dbRisDA = new RISLiteEntities())
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
                        if (query != null)
                        {
                            if (query.Count() > 0)
                            {
                                foreach (var item in query)
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
            catch (Exception gLE)
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

        #region sitios
        public List<tbl_CAT_Sitio> getListSitios(string user)
        {
            List<tbl_CAT_Sitio> lst = new List<tbl_CAT_Sitio>();
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    if (dbRisDA.tbl_CAT_Sitio.Any())
                    {
                        var query = dbRisDA.tbl_CAT_Sitio.ToList();
                        if (query != null)
                        {
                            if (query.Count > 0)
                            {
                                foreach (var item in query)
                                {
                                    tbl_CAT_Sitio mdl = new tbl_CAT_Sitio();
                                    mdl.intSitioID = (int)item.intSitioID;
                                    mdl.vchNombreSitio = item.vchNombreSitio;
                                    mdl.vchUserAdmin = item.vchUserAdmin;
                                    mdl.datFecha = (DateTime)item.datFecha;
                                    mdl.bitActivo = item.bitActivo;
                                    lst.Add(mdl);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception eLT)
            {
                Log.EscribeLog("Existe un error en getListSitios: " + eLT.Message, 3, user);
            }
            return lst;
        }

        public bool setSitio(tbl_CAT_Sitio sitio, string user, ref string mensaje)
        {
            bool valido = false;
            try
            {
                tbl_CAT_Sitio mdlSit = new tbl_CAT_Sitio();
                using (dbRisDA = new RISLiteEntities())
                {
                    //Primero en cat
                    if (!dbRisDA.tbl_CAT_Sitio.Any(x => x.vchNombreSitio.ToUpper() == sitio.vchNombreSitio.ToUpper()))
                    {
                        mdlSit.bitActivo = sitio.bitActivo;
                        mdlSit.datFecha = DateTime.Now;
                        mdlSit.vchNombreSitio = sitio.vchNombreSitio;
                        mdlSit.vchUserAdmin = user;
                        dbRisDA.tbl_CAT_Sitio.Add(mdlSit);
                        dbRisDA.SaveChanges();
                        valido = true;
                    }
                    else
                    {
                        mensaje += " Ya existe el sitio.";
                        valido = false;
                    }
                }
            }
            catch (Exception eSU)
            {
                valido = false;
                mensaje += eSU.Message;
                Log.EscribeLog("Existe un error en setSitio: " + eSU.Message, 3, user);
            }
            return valido;
        }

        public bool setActualizaSitio(tbl_CAT_Sitio sitio, string user, ref string mensaje)
        {
            bool valido = false;
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    if (dbRisDA.tbl_CAT_Sitio.Any(x => x.intSitioID == sitio.intSitioID))
                    {
                        if (!dbRisDA.tbl_CAT_Sitio.Any(x => x.vchNombreSitio == sitio.vchNombreSitio))
                        {
                            tbl_CAT_Sitio mdlSit = new tbl_CAT_Sitio();
                            mdlSit = dbRisDA.tbl_CAT_Sitio.First(x => x.intSitioID == sitio.intSitioID);
                            mdlSit.bitActivo = sitio.bitActivo;
                            mdlSit.datFecha = DateTime.Now;
                            mdlSit.vchUserAdmin = user;
                            mdlSit.vchNombreSitio = sitio.vchNombreSitio;
                            dbRisDA.SaveChanges();
                            valido = true;
                        }
                        else
                        {
                            mensaje = "El nombre del sitio ya existe.";
                            valido = false;
                        }
                    }
                    else
                    {
                        mensaje = "No existe el sitio.";
                        valido = false;
                    }
                }
            }
            catch (Exception eSU)
            {
                valido = false;
                mensaje = eSU.Message;
                Log.EscribeLog("Existe un error en setActualizaSitio: " + eSU.Message, 3, user);
            }
            return valido;
        }

        public bool setEstatusSitio(int intSitioID, string user, ref string mensaje)
        {
            bool valido = false;
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    if (dbRisDA.tbl_CAT_Sitio.Any(x => x.intSitioID == intSitioID))
                    {
                        tbl_CAT_Sitio mdlUser = new tbl_CAT_Sitio();
                        mdlUser = dbRisDA.tbl_CAT_Sitio.First(x => x.intSitioID == intSitioID);
                        mdlUser.bitActivo = !mdlUser.bitActivo;
                        mdlUser.datFecha = DateTime.Today;
                        mdlUser.vchUserAdmin = user;
                        dbRisDA.SaveChanges();
                        valido = true;
                    }
                    else
                    {
                        mensaje = "El sitio no existe.";
                        valido = false;
                    }
                }
            }
            catch (Exception eSU)
            {
                valido = false;
                mensaje = eSU.Message;
                Log.EscribeLog("Existe un error en setEstatusSitio: " + eSU.Message, 3, user);
            }
            return valido;
        }

        #endregion sitios

        #region configSitio
        public bool getConfigSitio(int intSitioID, string user, ref tbl_MST_ConfiguracionSistema mdl, ref string mensaje)
        {
            bool valido = false;
            tbl_MST_ConfiguracionSistema mdlConfig = new tbl_MST_ConfiguracionSistema();
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    if (dbRisDA.tbl_CAT_Sitio.Any(x => x.intSitioID == intSitioID))
                    {
                        var mdlConsulta = (from sitio in dbRisDA.tbl_CAT_Sitio
                                           join config in dbRisDA.tbl_MST_ConfiguracionSistema on sitio.intSitioID equals config.intSitioID into sitioDef
                                           from x in sitioDef.DefaultIfEmpty()
                                           where sitio.intSitioID == intSitioID
                                           select new
                                           {
                                               intConfigID = x.intConfigID == null ? 0 : x.intConfigID,
                                               intSitioID = sitio.intSitioID,
                                               bitActivo = x.bitActivo == null ? false : true,
                                               datFecha = x.datFecha == null ? DateTime.MinValue : x.datFecha,
                                               vbLogoSitio = x.vbLogoSitio == null ? null : x.vbLogoSitio,
                                               intWidthImage = x.intWidthImage == null ? 0 : x.intWidthImage,
                                               intHeigthImage = x.intHeigthImage == null ? 0 : x.intHeigthImage,
                                               vchDominio = x.vchDominio == null ? "" : x.vchDominio,
                                               vchPrefijo = x.vchPrefijo == null ? "" : x.vchPrefijo,
                                               vchUserAdmin = x.vchUserAdmin == null ? "" : x.vchUserAdmin,
                                               vchVersion = x.vchVersion == null ? "" : x.vchVersion,
                                               vchNombreSitio = sitio.vchNombreSitio == null ? "" : sitio.vchNombreSitio
                                           }
                           ).First();
                        if (mdlConsulta != null)
                        {
                            mdl.bitActivo = mdlConsulta.bitActivo;
                            mdl.datFecha = mdlConsulta.datFecha;
                            mdl.intConfigID = mdlConsulta.intConfigID;
                            mdl.intSitioID = mdlConsulta.intSitioID;
                            mdl.vbLogoSitio = mdlConsulta.vbLogoSitio;
                            mdl.intHeigthImage = mdlConsulta.intHeigthImage;
                            mdl.intWidthImage = mdlConsulta.intWidthImage;
                            mdl.vchDominio = mdlConsulta.vchDominio;
                            mdl.vchPrefijo = mdlConsulta.vchPrefijo;
                            mdl.vchUserAdmin = mdlConsulta.vchUserAdmin;
                            mdl.vchVersion = mdlConsulta.vchVersion;
                        }
                        valido = true;
                    }
                    else
                    {
                        valido = false;
                        mensaje = "No existe una configuración para el sitio.";
                    }
                }
            }
            catch (Exception egCS)
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
                    tbl_MST_ConfiguracionSistema mdl = new tbl_MST_ConfiguracionSistema();
                    if (dbRisDA.tbl_MST_ConfiguracionSistema.Any(x => x.intConfigID == mdlConfigSistem.intConfigID))
                    {
                        mdl = dbRisDA.tbl_MST_ConfiguracionSistema.First(x => x.intConfigID == mdlConfigSistem.intConfigID);
                        if (mdl != null)
                        {
                            mdl.datFecha = DateTime.Now;
                            mdl.vbLogoSitio = mdlConfigSistem.vbLogoSitio;
                            mdl.intWidthImage = mdlConfigSistem.intWidthImage;
                            mdl.intHeigthImage = mdlConfigSistem.intHeigthImage;
                            mdl.vchDominio = mdlConfigSistem.vchDominio;
                            //mdl.vchNombreSitio = mdlConfigSistem.vchNombreSitio;
                            mdl.vchPrefijo = mdlConfigSistem.vchPrefijo;
                            mdl.vchUserAdmin = user;
                            mdl.vchVersion = mdlConfigSistem.vchVersion;
                            dbRisDA.SaveChanges();
                            valido = true;
                        }
                    }
                    else
                    {
                        mdl.bitActivo = true;
                        mdl.datFecha = DateTime.Now;
                        mdl.intSitioID = mdlConfigSistem.intSitioID;
                        mdl.vbLogoSitio = mdlConfigSistem.vbLogoSitio;
                        mdl.intWidthImage = mdlConfigSistem.intWidthImage;
                        mdl.intHeigthImage = mdlConfigSistem.intHeigthImage;
                        mdl.vchDominio = mdlConfigSistem.vchDominio;
                        //mdl.vchNombreSitio = mdlConfigSistem.vchNombreSitio;
                        mdl.vchPrefijo = mdlConfigSistem.vchPrefijo;
                        mdl.vchUserAdmin = user;
                        mdl.vchVersion = mdlConfigSistem.vchVersion;
                        dbRisDA.tbl_MST_ConfiguracionSistema.Add(mdl);
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
        #endregion configSitio

        #region ConfigEmail
        public bool getConfigEmail(int intSitioID, string user, ref tbl_Conf_CorreoSitio mdl, ref string mensaje)
        {
            bool valido = false;
            tbl_Conf_CorreoSitio mdlConfig = new tbl_Conf_CorreoSitio();
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    if (dbRisDA.tbl_Conf_CorreoSitio.Any(x => x.intSitioID == intSitioID))
                    {
                        mdl = dbRisDA.tbl_Conf_CorreoSitio.First();
                        Log.EscribeLog("Correo de sitio: " + mdl.vchCorreo, 3, user);
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
                    tbl_Conf_CorreoSitio mdl = new tbl_Conf_CorreoSitio();
                    if (dbRisDA.tbl_Conf_CorreoSitio.Any(x => x.intConfigCorreoID == mdlConfigSistem.intConfigCorreoID))
                    {
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
                    else
                    {
                        mdl.bitActivo = mdlConfigSistem.bitActivo;
                        mdl.BitEnableSsl = mdlConfigSistem.BitEnableSsl;
                        mdl.datFecha = mdlConfigSistem.datFecha;
                        //mdl.intConfigCorreoID = mdlConfigSistem.intConfigCorreoID;
                        mdl.intPort = mdlConfigSistem.intPort;
                        mdl.intSitioID = mdlConfigSistem.intSitioID;
                        mdl.vchCorreo = mdlConfigSistem.vchCorreo;
                        mdl.vchHost = mdlConfigSistem.vchHost;
                        mdl.vchPassword = mdlConfigSistem.vchPassword;
                        mdl.vchUserAdmin = user;
                        mdl.vchUsuarioCorreo = mdlConfigSistem.vchUsuarioCorreo;
                        dbRisDA.tbl_Conf_CorreoSitio.Add(mdl);
                        dbRisDA.SaveChanges();
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
                                     join sitio in dbRisDA.tbl_CAT_Sitio on _user.intSitioID equals sitio.intSitioID into sitioDef
                                     from x in sitioDef.DefaultIfEmpty()
                                     select new
                                     {
                                         intTipoUsuario = _user.intTipoUsuario,
                                         intUsuarioID = _user.intUsuarioID,
                                         bitActivo = _user.bitActivo,
                                         datFecha = _user.datFecha,
                                         vchNombre = _user.vchNombre,
                                         vchPassword = _user.vchPassword,
                                         vchUserAdmin = _user.vchUserAdmin,
                                         vchEmail = _user.vchEmail,
                                         vchUsuario = _user.vchUsuario,
                                         vchTipoUsuario = cat.vchTipoUsuario,
                                         intSitioId = _user.intSitioID == null ? 0 : _user.intSitioID,
                                         vchSitio = x.vchNombreSitio == null ? "" : x.vchNombreSitio
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
                                    mdl.vchPassword = item.vchPassword;
                                    mdl.vchNombre = item.vchNombre;
                                    mdl.vchEmail = item.vchEmail;
                                    mdl.vchUserAdmin = item.vchUserAdmin;
                                    mdl.vchUsuario = item.vchUsuario;
                                    mdl.vchTipoUsuario = item.vchTipoUsuario;
                                    mdl.intSitioID = (int)item.intSitioId;
                                    mdl.vchSitio = item.vchSitio;
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
                        mdlUser.vchEmail = usuario.vchEmail;
                        mdlUser.vchNombre = usuario.vchNombre;
                        mdlUser.vchRutaIcono = "user.png";
                        mdlUser.vchUserAdmin = user;
                        mdlUser.vchPassword = usuario.vchPassword;
                        mdlUser.vchUsuario = usuario.vchUsuario;
                        if (usuario.intSitioID > 0)
                            mdlUser.intSitioID = usuario.intSitioID;
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
                        mdlUser.vchPassword = usuario.vchPassword;
                        //mdlUser.intSitioID = usuario.intSitioID;
                        mdlUser.vchEmail = usuario.vchEmail;
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

        public List<tbl_CAT_Modalidad> getModalidadTecnico(int intSitioID, string user)
        {
            List<tbl_CAT_Modalidad> lstresult = new List<tbl_CAT_Modalidad>();
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    if (dbRisDA.tbl_CAT_Modalidad.Any(x => x.intSitioID == intSitioID))
                    {
                        var query = (dbRisDA.tbl_CAT_Modalidad.Where(x => x.intSitioID == intSitioID && (bool)x.bitActivo)).ToList();
                        if (query != null)
                        {
                            if (query.Count > 0)
                            {
                                lstresult.AddRange(query);
                            }
                        }
                    }
                }
            }
            catch (Exception egmT)
            {
                Log.EscribeLog("Existe un error en getModalidadTecnico : " + egmT.Message, 3, user);
            }
            return lstresult;
        }

        public List<stp_getRELModalidadTecnico_Result> getModalidadTecnicoList(int intUsuarioID, string user)
        {
            List<stp_getRELModalidadTecnico_Result> lstresult = new List<stp_getRELModalidadTecnico_Result>();
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    var query = (dbRisDA.stp_getRELModalidadTecnico(intUsuarioID)).ToList();
                    if (query != null)
                    {
                        if (query.Count > 0)
                        {
                            lstresult.AddRange(query);
                        }
                    }

                }
            }
            catch (Exception egmT)
            {
                Log.EscribeLog("Existe un error en getModalidadTecnicoList : " + egmT.Message, 3, user);
            }
            return lstresult;
        }

        public bool setModalidadTecnico(int intUsuarioID, int intModalidadID, string user, ref string mensaje)
        {
            bool valido = false;
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    tbl_REL_ModalidadesTecnico mdl = new tbl_REL_ModalidadesTecnico();
                    if (!dbRisDA.tbl_REL_ModalidadesTecnico.Any(x => x.intModalidadID == intModalidadID && x.intUsuarioID == intUsuarioID))
                    {
                        mdl.intUsuarioID = intUsuarioID;
                        mdl.intModalidadID = intModalidadID;
                        mdl.bitActivo = true;
                        mdl.datFecha = DateTime.Now;
                        mdl.vchUserAdmin = user;
                        dbRisDA.tbl_REL_ModalidadesTecnico.Add(mdl);
                        dbRisDA.SaveChanges();
                    }
                    else
                    {
                        mdl = dbRisDA.tbl_REL_ModalidadesTecnico.First(x => x.intModalidadID == intModalidadID && x.intUsuarioID == intUsuarioID);
                        mdl.bitActivo = true;
                        mdl.datFecha = DateTime.Now;
                        mdl.vchUserAdmin = user;
                        dbRisDA.SaveChanges();
                    }
                    valido = true;
                }
            }
            catch (Exception esMT)
            {
                valido = false;
                Log.EscribeLog("Existe un error en setModalidadTecnico: " + esMT.Message, 3, user);
            }
            return valido;
        }

        public bool setEstatusModalidadTecnico(int intRELModTecnicoID, string user, ref string mensaje)
        {
            bool valido = false;
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    tbl_REL_ModalidadesTecnico mdl = new tbl_REL_ModalidadesTecnico();
                    if (!dbRisDA.tbl_REL_ModalidadesTecnico.Any(x => x.intRELModTecnicoID == intRELModTecnicoID))
                    {
                        mdl.bitActivo = !mdl.bitActivo;
                        mdl.datFecha = DateTime.Now;
                        mdl.vchUserAdmin = user;
                        dbRisDA.tbl_REL_ModalidadesTecnico.Add(mdl);
                        dbRisDA.SaveChanges();
                        valido = true;
                    }
                    else
                    {
                        valido = false;
                        mensaje = "No existe la modalidad asociada al usuario.";
                    }

                }
            }
            catch (Exception esMT)
            {
                valido = false;
                Log.EscribeLog("Existe un error en setModalidadTecnico: " + esMT.Message, 3, user);
            }
            return valido;
        }


        #endregion AdminUsers

        #region varadicionales
        public List<clsVarAcicionales> getVariablesAdicionalPaciente(string user, int intSitioID)
        {
            List<clsVarAcicionales> lstreturn = new List<clsVarAcicionales>();
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    if (dbRisDA.tbl_CONFIG_VariablesAdiPaciente.Any(x => x.intSitioID == intSitioID))
                    {
                        var query = dbRisDA.tbl_CONFIG_VariablesAdiPaciente.Where(x => x.intSitioID == intSitioID).ToList();
                        if (query != null)
                        {
                            if (query.Count > 0)
                            {
                                foreach (var item in query)
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
            catch (Exception egV)
            {
                Log.EscribeLog("Existe un error en getVariablesAdicionalPaciente: " + egV.Message, 3, user);
            }
            return lstreturn;
        }

        public List<clsVarAcicionales> getVariablesAdicionalCita(string user, int intSitioID)
        {
            List<clsVarAcicionales> lstreturn = new List<clsVarAcicionales>();
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    if (dbRisDA.tbl_CONFIG_VariablesAdiCita.Any(x => x.intSitioID == intSitioID))
                    {
                        var query = dbRisDA.tbl_CONFIG_VariablesAdiCita.Where(x => x.intSitioID == intSitioID).ToList();
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

        public List<tbl_CAT_Identificacion> getVariablesAdicionalID(string user, int intSitioID)
        {
            List<tbl_CAT_Identificacion> lstreturn = new List<tbl_CAT_Identificacion>();
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    if (dbRisDA.tbl_CAT_Identificacion.Any(x => x.intSitioID == intSitioID))
                    {
                        var query = dbRisDA.tbl_CAT_Identificacion.Where(x => x.intSitioID == intSitioID).ToList();
                        if (query != null)
                        {
                            if (query.Count > 0)
                            {
                                lstreturn.AddRange(query);
                            }
                        }
                    }
                }
            }
            catch (Exception egV)
            {
                Log.EscribeLog("Existe un error en getVariablesAdicionalID: " + egV.Message, 3, user);
            }
            return lstreturn;
        }

        public bool setAgregarVariable(int iTipoVariable, string vchVarible, int intSitioID, string user, ref string mensaje)
        {
            bool valido = false;
            try
            {
                switch (iTipoVariable)
                {
                    case 1: //Variable adicional Paciente
                        using (dbRisDA = new RISLiteEntities())
                        {
                            if (!dbRisDA.tbl_CONFIG_VariablesAdiPaciente.Any(x => x.vchNombreVariable.ToUpper() == vchVarible && x.intSitioID == intSitioID))
                            {
                                tbl_CONFIG_VariablesAdiPaciente mdl = new tbl_CONFIG_VariablesAdiPaciente();
                                mdl.vchNombreVariable = vchVarible;
                                mdl.datFecha = DateTime.Now;
                                mdl.bitActivo = true;
                                mdl.vchUserAdmin = user;
                                mdl.intSitioID = intSitioID;
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
                            if (!dbRisDA.tbl_CONFIG_VariablesAdiCita.Any(x => x.vchNombreVariable.ToUpper() == vchVarible && x.intSitioID == intSitioID))
                            {
                                tbl_CONFIG_VariablesAdiCita mdl = new tbl_CONFIG_VariablesAdiCita();
                                mdl.vchNombreVariable = vchVarible;
                                mdl.datFecha = DateTime.Now;
                                mdl.bitActivo = true;
                                mdl.vchUserAdmin = user;
                                mdl.intSitioID = intSitioID;
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
                    case 3: //Variable adicional Identificaciones
                        using (dbRisDA = new RISLiteEntities())
                        {
                            if (!dbRisDA.tbl_CAT_Identificacion.Any(x => x.vchNombreId.ToUpper() == vchVarible && x.intSitioID == intSitioID))
                            {
                                tbl_CAT_Identificacion mdl = new tbl_CAT_Identificacion();
                                mdl.vchNombreId = vchVarible;
                                mdl.datFecha = DateTime.Now;
                                mdl.bitActivo = true;
                                mdl.vchUserAdmin = user;
                                mdl.intSitioID = intSitioID;
                                dbRisDA.tbl_CAT_Identificacion.Add(mdl);
                                dbRisDA.SaveChanges();
                                valido = true;
                            }
                            else
                            {
                                mensaje = "La identificación ya existe. Favor de verificar.";
                            }
                        }
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
                    case 3: //Variable adicional Identificaciones
                        using (dbRisDA = new RISLiteEntities())
                        {
                            if (dbRisDA.tbl_CAT_Identificacion.Any(x => x.intIdentificacionID == intVarAdiID))
                            {
                                tbl_CAT_Identificacion mdl = new tbl_CAT_Identificacion();
                                mdl = dbRisDA.tbl_CAT_Identificacion.First(x => x.intIdentificacionID == intVarAdiID);
                                mdl.vchNombreId = vchVarible;
                                mdl.vchUserAdmin = user;
                                mdl.datFecha = DateTime.Now;
                                dbRisDA.SaveChanges();
                                valido = true;
                            }
                            else
                            {
                                mensaje = "No existe la Identificación para actualizar.";
                            }
                        }
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
                        using (dbRisDA = new RISLiteEntities())
                        {
                            if (dbRisDA.tbl_CAT_Identificacion.Any(x => x.intIdentificacionID == intTipoVariable))
                            {
                                tbl_CAT_Identificacion mdl = new tbl_CAT_Identificacion();
                                mdl = dbRisDA.tbl_CAT_Identificacion.First(x => x.intIdentificacionID == intTipoVariable);
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
                    if (dbRisDA.tbl_CAT_Modalidad.Any(x => (bool)x.bitActivo))
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

        public List<clsPrestacion> getListPrestacion(int intModalidadID, int intSitioID, string user)
        {
            List<clsPrestacion> list = new List<clsPrestacion>();
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    var query = dbRisDA.stp_getPrestacionModalidad(intModalidadID, intSitioID).ToList();
                    if (query != null)
                    {
                        if (query.Count > 0)
                        {
                            foreach (var item in query)
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
                    if (!dbRisDA.tbl_CAT_Prestacion.Any(x => x.vchPrestacion.ToUpper() == prestacion.vchPrestacion.ToUpper()))
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
                        if (!dbRisDA.tbl_CAT_Prestacion.Any(x => x.vchPrestacion == prestacion.vchPrestacion && x.intSitioID == prestacion.intSitioId && x.intDuracionMin == prestacion.intDuracionMin))
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
                Log.EscribeLog("Existe un error en setActualizaPrestacion: " + eSU.Message, 3, user);
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

        #region Modalidadesagenda
        public List<clsConfAgenda> getListAgenda(string user, int idsitio)
        {
            List<clsConfAgenda> lst = new List<clsConfAgenda>();
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    if (dbRisDA.tbl_CAT_Modalidad.Any())
                    {
                        //var query = dbRisDA.tbl_CAT_Modalidad
                        //   //.Where(x => (bool)x.bitActivo).ToList()
                        //   .ToList();

                        var query = (from x in dbRisDA.tbl_CAT_Modalidad
                                     join dur_mod in dbRisDA.tbl_CAT_DuracionModalidad
                                     on x.intDuracionGen equals dur_mod.intDuracionModalidadID
                                     where x.intSitioID == idsitio
                                     select new { x.intModalidadID, x.vchModalidad, x.vchCodigo, x.vchColor, x.bitActivo, x.datFecha, x.vchUserAdmin, dur_mod.intDuracion }).ToList();

                        if (query != null)
                        {
                            if (query.Count > 0)
                            {
                                foreach (var item in query)
                                {
                                    clsConfAgenda mdl = new clsConfAgenda();
                                    mdl.intModalidadID = (int)item.intModalidadID;
                                    mdl.bitActivo = (bool)item.bitActivo;
                                    mdl.datFecha = (DateTime)item.datFecha;
                                    mdl.vchModalidad = item.vchModalidad;
                                    mdl.vchCodigo = item.vchCodigo;
                                    mdl.vchUserAdmin = item.vchUserAdmin;
                                    mdl.vchColor = item.vchColor;
                                    mdl.intDuracionGen = (int)item.intDuracion;
                                    lst.Add(mdl);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception eLT)
            {
                Log.EscribeLog("Existe un error en getModalidadAgenda: " + eLT.Message, 3, user);
            }
            return lst;
        }

        public bool UpdateAgenda(string user, int idmodalidad, string codigo, string modalidad, string color, int duracion, int idsitio)
        {
            bool bandera_Actualizar = false;
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {

                    var dbCstInfo = dbRisDA.tbl_CAT_Modalidad
                        .Where(w => w.intModalidadID == idmodalidad && w.intSitioID == idsitio)
                        .SingleOrDefault();

                    if (dbCstInfo != null)
                    {
                        dbCstInfo.vchCodigo = codigo;
                        dbCstInfo.vchModalidad = modalidad;
                        dbCstInfo.intDuracionGen = duracion;
                        dbCstInfo.vchColor = color;
                        dbRisDA.SaveChanges();
                        bandera_Actualizar = true;
                    }

                }
            }
            catch (Exception eLT)
            {
                Log.EscribeLog("Existe un error en getModalidadAgenda: " + eLT.Message, 3, user);
            }
            return bandera_Actualizar;
        }

        public bool setEstatusAgenda(string user, int idmodalidad, ref string mensaje)
        {
            bool valido = false;
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    if (dbRisDA.tbl_CAT_Modalidad.Any(x => x.intModalidadID == idmodalidad))
                    {
                        tbl_CAT_Modalidad mdlUser = new tbl_CAT_Modalidad();
                        mdlUser = dbRisDA.tbl_CAT_Modalidad.First(x => x.intModalidadID == idmodalidad);
                        mdlUser.bitActivo = !mdlUser.bitActivo;
                        mdlUser.datFecha = DateTime.Today;
                        mdlUser.vchUserAdmin = user;
                        dbRisDA.SaveChanges();
                        valido = true;
                    }
                    else
                    {
                        mensaje = "La modalidad no existe.";
                        valido = false;
                    }
                }
            }
            catch (Exception eSU)
            {
                valido = false;
                mensaje = eSU.Message;
                Log.EscribeLog("Existe un error en setEstatusAgenda: " + eSU.Message, 3, user);
            }
            return valido;
        }

        public bool setAgenda(clsConfAgenda agenda, string user, ref string mensaje)
        {
            bool valido = false;
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    if (!dbRisDA.tbl_CAT_Modalidad.Any(x => x.vchCodigo.ToUpper() == agenda.vchCodigo.ToUpper() && x.vchModalidad.ToUpper() == agenda.vchModalidad.ToUpper()
                    && (bool)x.bitActivo && x.intSitioID == agenda.intSitioID))
                    {
                        tbl_CAT_Modalidad mdlAgenda = new tbl_CAT_Modalidad();
                        mdlAgenda.bitActivo = agenda.bitActivo;
                        mdlAgenda.vchModalidad = agenda.vchModalidad;
                        mdlAgenda.vchCodigo = agenda.vchCodigo;
                        mdlAgenda.vchColor = agenda.vchColor;
                        mdlAgenda.datFecha = DateTime.Today;
                        mdlAgenda.vchUserAdmin = user;
                        mdlAgenda.intSitioID = agenda.intSitioID;
                        mdlAgenda.intDuracionGen = agenda.intDuracionGen;
                        dbRisDA.tbl_CAT_Modalidad.Add(mdlAgenda);
                        dbRisDA.SaveChanges();
                        valido = true;
                    }
                    else
                    {
                        mensaje = "La modalidad ya existe.";
                        valido = false;
                    }
                }
            }
            catch (Exception eSU)
            {
                valido = false;
                mensaje = eSU.Message;
                Log.EscribeLog("Existe un error en setAgenda: " + eSU.Message, 3, user);
            }
            return valido;
        }

        #endregion Modalidadesagenda

        #region AgendaDashboard

        public List<clsEventoCita> getListEventoCita(string user)
        {
            List<clsEventoCita> lst = new List<clsEventoCita>();
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    if (dbRisDA.tbl_CAT_Modalidad.Any())
                    {
                        var query = dbRisDA.tbl_CAT_Eventos.ToList();
                        if (query != null)
                        {
                            if (query.Count > 0)
                            {
                                foreach (var item in query)
                                {
                                    clsEventoCita mdl = new clsEventoCita();
                                    mdl.TaskID = (int)item.TaskID;
                                    mdl.Start = (DateTime)item.Start;
                                    mdl.End = (DateTime)item.End;
                                    mdl.Title = item.Title;
                                    mdl.Description = item.Description;
                                    mdl.OwnerID = (int)item.OwnerID;
                                    if (item.IsAllDay == null)
                                    {
                                        mdl.IsAllDay = false;
                                    }
                                    else
                                    {
                                        mdl.IsAllDay = (bool)item.IsAllDay;
                                    }
                                    //
                                    mdl.RecurrenceRule = item.RecurrenceRule;

                                    if (item.RecurrenceID == null)
                                    {
                                        mdl.RecurrenceID = 0;
                                    }
                                    else
                                    {
                                        mdl.RecurrenceID = (int)item.RecurrenceID;
                                    }

                                    mdl.RecurrenceException = item.RecurrenceException;
                                    mdl.intModalidadID = (int)item.intModalidadID;
                                    lst.Add(mdl);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception eLT)
            {
                Log.EscribeLog("Existe un error en getListEventoCita: " + eLT.Message, 3, user);
            }
            return lst;
        }

        public List<clsEventoCita> getListCitas_Sitio(string user, int idmodalidad, int idsitio)
        {
            List<clsEventoCita> lst = new List<clsEventoCita>();
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    if (dbRisDA.tbl_CAT_Modalidad.Any())
                    {

                        //var query = dbRisDA.tbl_CAT_Eventos
                        //            .Where(w => w.intModalidadID == idmodalidad).ToList();

                        var query = dbRisDA.stp_getBusquedaCita_Sitio(idmodalidad, idsitio).ToList();


                        if (query != null)
                        {
                            if (query.Count > 0)
                            {
                                foreach (var item in query)
                                {
                                    clsEventoCita mdl = new clsEventoCita();
                                    mdl.TaskID = (int)item.intEstudioID;
                                    mdl.Start = (DateTime)item.datFecha;
                                    mdl.End = (DateTime)item.datFechaFin;
                                    mdl.Title = item.vchTitulo;
                                    mdl.Description = item.vchDescripcion;
                                    mdl.intModalidadID = (int)item.intModalidadID;
                                    lst.Add(mdl);

                                }
                            }
                        }
                    }
                }
            }
            catch (Exception eLT)
            {
                Log.EscribeLog("Existe un error en getListCitas_Sitio: " + eLT.Message, 3, user);
            }
            return lst;
        }

        public List<clsEventoCita> getListCitas(string user, int idmodalidad)
        {
            List<clsEventoCita> lst = new List<clsEventoCita>();
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    if (dbRisDA.tbl_CAT_Modalidad.Any())
                    {
                        //var query = dbRisDA.tbl_CAT_Modalidad.Where(x => (bool)x.bitActivo).ToList();
                        //var query = dbRisDA.tbl_CAT_Eventos.ToList();

                        //int idmod = Convert.ToInt32(idmodalidad);

                        var query = dbRisDA.tbl_CAT_Eventos
                                    .Where(w => w.intModalidadID == idmodalidad).ToList();

                        if (query != null)
                        {
                            if (query.Count > 0)
                            {
                                foreach (var item in query)
                                {
                                    clsEventoCita mdl = new clsEventoCita();
                                    mdl.TaskID = (int)item.TaskID;
                                    mdl.Start = (DateTime)item.Start;
                                    mdl.End = (DateTime)item.End;
                                    mdl.Title = item.Title;
                                    mdl.Description = "";
                                    mdl.OwnerID = (int)item.OwnerID;
                                    if (item.IsAllDay == null)
                                    {
                                        mdl.IsAllDay = false;
                                    }
                                    else
                                    {
                                        mdl.IsAllDay = (bool)item.IsAllDay;
                                    }
                                    //
                                    mdl.RecurrenceRule = item.RecurrenceRule;

                                    if (item.RecurrenceID == null)
                                    {
                                        mdl.RecurrenceID = 0;
                                    }
                                    else
                                    {
                                        mdl.RecurrenceID = (int)item.RecurrenceID;
                                    }


                                    mdl.RecurrenceException = item.RecurrenceException;
                                    //mdl.StarTimezone = (DateTime)item.StarTimezone;
                                    //mdl.EndTimezone = (DateTime)item.EndTimezone;
                                    mdl.intModalidadID = (int)item.intModalidadID;
                                    lst.Add(mdl);

                                }
                            }
                        }
                    }
                }
            }
            catch (Exception eLT)
            {
                Log.EscribeLog("Existe un error en getModalidadAgenda: " + eLT.Message, 3, user);
            }
            return lst;
        }

        public List<clsEquipo> getCitaEquipos(string user, int idmodalidad)
        {
            List<clsEquipo> lst = new List<clsEquipo>();
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    if (dbRisDA.tbl_CAT_Equipo.Any())
                    {
                        var query = dbRisDA.tbl_CAT_Equipo
                                    .Where(w => w.intModalidadID == idmodalidad).ToList();

                        if (query != null)
                        {
                            if (query.Count > 0)
                            {
                                foreach (var item in query)
                                {
                                    clsEquipo mdl = new clsEquipo();
                                    mdl.intEquipoID = (int)item.intEquipoID;
                                    mdl.intModalidadID = (int)item.intModalidadID;
                                    mdl.vchNombreEquipo = item.vchNombreEquipo;
                                    mdl.vchCodigoEquipo = item.vchCodigoEquipo;
                                    lst.Add(mdl);

                                }
                            }
                        }
                    }
                }
            }
            catch (Exception eLT)
            {
                Log.EscribeLog("Existe un error en getCitaEquipos: " + eLT.Message, 3, user);
            }
            return lst;
        }

        public List<clsEquipo> getCitaEquipos_Sitio(string user, int idmodalidad, int idsitio)
        {
            List<clsEquipo> lst = new List<clsEquipo>();
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    if (dbRisDA.tbl_CAT_Equipo.Any())
                    {
                        var query = dbRisDA.tbl_CAT_Equipo
                                    .Where(w => w.intModalidadID == idmodalidad && w.intSitioID == idsitio).ToList();

                        if (query != null)
                        {
                            if (query.Count > 0)
                            {
                                foreach (var item in query)
                                {
                                    clsEquipo mdl = new clsEquipo();
                                    mdl.intEquipoID = (int)item.intEquipoID;
                                    mdl.intModalidadID = (int)item.intModalidadID;
                                    mdl.vchNombreEquipo = item.vchNombreEquipo;
                                    mdl.vchCodigoEquipo = item.vchCodigoEquipo;
                                    lst.Add(mdl);

                                }
                            }
                        }
                    }
                }
            }
            catch (Exception eLT)
            {
                Log.EscribeLog("Existe un error en getCitaEquipos: " + eLT.Message, 3, user);
            }
            return lst;
        }

        public string getListColorModalidad_Sitio(string user, string modalidad, int idsitio)
        {
            string color = "";
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    if (dbRisDA.tbl_CAT_Modalidad.Any())
                    {
                        //var query = dbRisDA.tbl_CAT_Modalidad.Where(x => x.vchCodigo.Contains(modalidad)).ToString();


                        var query = (from tblmod in dbRisDA.tbl_CAT_Modalidad
                                     where tblmod.vchCodigo == modalidad && tblmod.intSitioID == idsitio
                                     select tblmod.vchColor).First();

                        if (query != null)
                        {
                            color = query;
                        }
                    }
                }
            }
            catch (Exception eLT)
            {
                Log.EscribeLog("Existe un error en getModalidadAgenda: " + eLT.Message, 3, user);
            }
            return color;
        }

        public string getListColorModalidad(string user, string modalidad)
        {
            string color = "";
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    if (dbRisDA.tbl_CAT_Modalidad.Any())
                    {
                        //var query = dbRisDA.tbl_CAT_Modalidad.Where(x => x.vchCodigo.Contains(modalidad)).ToString();


                        var query = (from tblmod in dbRisDA.tbl_CAT_Modalidad
                                     where tblmod.vchCodigo == modalidad
                                     select tblmod.vchColor).First();

                        if (query != null)
                        {
                            color = query;
                        }
                    }
                }
            }
            catch (Exception eLT)
            {
                Log.EscribeLog("Existe un error en getModalidadAgenda: " + eLT.Message, 3, user);
            }
            return color;
        }

        public int getListDuracionGen_Sitio(string user, int modalidad, int idsitio)
        {
            int duracion = 0;
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    if (dbRisDA.tbl_CAT_Modalidad.Any())
                    {
                        //var query = dbRisDA.tbl_CAT_Modalidad.Where(x => x.vchCodigo.Contains(modalidad)).ToString();


                        var query = (from tblmod in dbRisDA.tbl_CAT_Modalidad
                                     where tblmod.intModalidadID == modalidad && tblmod.intSitioID == idsitio
                                     select tblmod.intDuracionGen).First();

                        if (query != null)
                        {
                            duracion = Convert.ToInt32(query);
                        }
                    }
                }
            }
            catch (Exception eLT)
            {
                Log.EscribeLog("Existe un error en getListDuracionGen: " + eLT.Message, 3, user);
            }
            return duracion;
        }

        public int getListDuracionGen(string user, int modalidad)
        {
            int duracion = 0;
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    if (dbRisDA.tbl_CAT_Modalidad.Any())
                    {
                        //var query = dbRisDA.tbl_CAT_Modalidad.Where(x => x.vchCodigo.Contains(modalidad)).ToString();


                        var query = (from tblmod in dbRisDA.tbl_CAT_Modalidad
                                     where tblmod.intModalidadID == modalidad
                                     select tblmod.intDuracionGen).First();

                        if (query != null)
                        {
                            duracion = Convert.ToInt32(query);
                        }
                    }
                }
            }
            catch (Exception eLT)
            {
                Log.EscribeLog("Existe un error en getListDuracionGen: " + eLT.Message, 3, user);
            }
            return duracion;
        }

        public string getDescripcionModalidad_Sitio(string user, int modalidad, int idsitio)
        {
            string modalidad_descripcion = "";
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    if (dbRisDA.tbl_CAT_Modalidad.Any())
                    {
                        //var query = dbRisDA.tbl_CAT_Modalidad.Where(x => x.vchCodigo.Contains(modalidad)).ToString();                        

                        var query = (from tblmod in dbRisDA.tbl_CAT_Modalidad
                                     where tblmod.intModalidadID == modalidad && tblmod.intSitioID == idsitio
                                     select tblmod.vchCodigo).First();

                        if (query != null)
                        {
                            modalidad_descripcion = query;
                        }
                    }
                }
            }
            catch (Exception eLT)
            {
                Log.EscribeLog("Existe un error en getModalidadAgenda: " + eLT.Message, 3, user);
            }
            return modalidad_descripcion;
        }

        public string getDescripcionModalidad(string user, int modalidad)
        {
            string modalidad_descripcion = "";
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    if (dbRisDA.tbl_CAT_Modalidad.Any())
                    {
                        //var query = dbRisDA.tbl_CAT_Modalidad.Where(x => x.vchCodigo.Contains(modalidad)).ToString();                        

                        var query = (from tblmod in dbRisDA.tbl_CAT_Modalidad
                                     where tblmod.intModalidadID == modalidad
                                     select tblmod.vchCodigo).First();

                        if (query != null)
                        {
                            modalidad_descripcion = query;
                        }
                    }
                }
            }
            catch (Exception eLT)
            {
                Log.EscribeLog("Existe un error en getModalidadAgenda: " + eLT.Message, 3, user);
            }
            return modalidad_descripcion;
        }

        #endregion AgendaDashboard

        #region Equipo
        public List<tbl_CAT_Equipo> getListEquipo(int intModalidadID, int intSitioID, string user)
        {
            List<tbl_CAT_Equipo> list = new List<tbl_CAT_Equipo>();
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    var query = dbRisDA.tbl_CAT_Equipo.Where(x => x.intModalidadID == intModalidadID && x.intSitioID == intSitioID).ToList();
                    if (query != null)
                    {
                        if (query.Count > 0)
                        {
                            list.AddRange(query);
                        }
                    }
                }
            }
            catch (Exception egLC)
            {
                Log.EscribeLog("Existe un error en getListEquipo: " + egLC.Message, 3, user);
            }
            return list;
        }

        public bool setEquipo(tbl_CAT_Equipo equipo, string user, ref string mensaje)
        {
            bool valido = false;
            try
            {
                tbl_CAT_Equipo mdlCat = new tbl_CAT_Equipo();
                using (dbRisDA = new RISLiteEntities())
                {
                    //Primero en cat
                    if (!dbRisDA.tbl_CAT_Equipo.Any(x => x.vchNombreEquipo.ToUpper() == equipo.vchNombreEquipo.ToUpper() && x.intSitioID == equipo.intSitioID))
                    {
                        mdlCat.bitActivo = equipo.bitActivo;
                        mdlCat.datFecha = DateTime.Now;
                        mdlCat.intSitioID = equipo.intSitioID;
                        mdlCat.intModalidadID = equipo.intModalidadID;
                        mdlCat.vchAETitle = equipo.vchAETitle;
                        mdlCat.vchCodigoEquipo = equipo.vchCodigoEquipo;
                        mdlCat.vchIPEquipo = equipo.vchIPEquipo;
                        mdlCat.vchNombreEquipo = equipo.vchNombreEquipo;
                        mdlCat.vchUserAdmin = user;
                        mdlCat.vchUserAdmin = user;
                        dbRisDA.tbl_CAT_Equipo.Add(mdlCat);
                        dbRisDA.SaveChanges();
                        valido = true;
                    }
                    else
                    {
                        mensaje += " Ya existe el equipo.";
                        valido = false;
                    }
                }
            }
            catch (Exception eSU)
            {
                valido = false;
                mensaje += eSU.Message;
                Log.EscribeLog("Existe un error en setEquipo: " + eSU.Message, 3, user);
            }
            return valido;
        }

        public bool setActualizaEquipo(tbl_CAT_Equipo equipo, string user, ref string mensaje)
        {
            bool valido = false;
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    if (dbRisDA.tbl_CAT_Equipo.Any(x => x.intEquipoID == equipo.intEquipoID))
                    {
                        if (!dbRisDA.tbl_CAT_Equipo.Any(x => x.vchNombreEquipo == equipo.vchNombreEquipo && x.intSitioID == equipo.intSitioID && x.vchAETitle == equipo.vchAETitle && x.vchCodigoEquipo == equipo.vchCodigoEquipo))
                        {
                            tbl_CAT_Equipo mdlCat = new tbl_CAT_Equipo();
                            mdlCat = dbRisDA.tbl_CAT_Equipo.First(x => x.intEquipoID == equipo.intEquipoID);
                            mdlCat.bitActivo = equipo.bitActivo;
                            mdlCat.datFecha = DateTime.Now;
                            mdlCat.intModalidadID = equipo.intModalidadID;
                            mdlCat.vchUserAdmin = user;
                            mdlCat.vchAETitle = equipo.vchAETitle;
                            mdlCat.vchCodigoEquipo = equipo.vchCodigoEquipo;
                            mdlCat.vchIPEquipo = equipo.vchIPEquipo;
                            mdlCat.vchNombreEquipo = equipo.vchNombreEquipo;
                            dbRisDA.SaveChanges();
                            valido = true;
                        }
                        else
                        {
                            mensaje = "El nombre del equipo ya existe.";
                            valido = false;
                        }
                    }
                    else
                    {
                        mensaje = "No existe el equipo.";
                        valido = false;
                    }
                }
            }
            catch (Exception eSU)
            {
                valido = false;
                mensaje = eSU.Message;
                Log.EscribeLog("Existe un error en setActualizaEquipo: " + eSU.Message, 3, user);
            }
            return valido;
        }

        public bool setEstatusEquipo(int intEquipoID, string user, ref string mensaje)
        {
            bool valido = false;
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    if (dbRisDA.tbl_CAT_Equipo.Any(x => x.intEquipoID == intEquipoID))
                    {
                        tbl_CAT_Equipo mdlUser = new tbl_CAT_Equipo();
                        mdlUser = dbRisDA.tbl_CAT_Equipo.First(x => x.intEquipoID == intEquipoID);
                        mdlUser.bitActivo = !mdlUser.bitActivo;
                        mdlUser.datFecha = DateTime.Today;
                        mdlUser.vchUserAdmin = user;
                        dbRisDA.SaveChanges();
                        valido = true;
                    }
                    else
                    {
                        mensaje = "El equipo no existe.";
                        valido = false;
                    }
                }
            }
            catch (Exception eSU)
            {
                valido = false;
                mensaje = eSU.Message;
                Log.EscribeLog("Existe un error en setEstatusEquipo: " + eSU.Message, 3, user);
            }
            return valido;
        }
        #endregion Equipo

        #region Paciente
        public List<tbl_CAT_Genero> getListaGenero(string user)
        {
            List<tbl_CAT_Genero> lst = new List<tbl_CAT_Genero>();
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    if (dbRisDA.tbl_CAT_Genero.Any(x => (bool)x.bitActivo))
                    {
                        var query = dbRisDA.tbl_CAT_Genero.Where(x => (bool)x.bitActivo).ToList();
                        if (query != null)
                        {
                            if (query.Count > 0)
                            {
                                lst.AddRange(query);
                            }
                        }
                    }
                }
            }
            catch (Exception glG)
            {
                Log.EscribeLog("Existe un error en getListaGenero: " + glG.Message, 3, user);
            }
            return lst;
        }

        public List<clsDireccion> getDireccionPaciente(string vchCodigoPostal, string user, ref string mensaje)
        {
            List<clsDireccion> lst = new List<clsDireccion>();
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    if (dbRisDA.tbl_CAT_CodigoPostal.Any(x => x.vchCodigoPostal == vchCodigoPostal))
                    {
                        var query = (from cp in dbRisDA.tbl_CAT_CodigoPostal
                                     join muni in dbRisDA.tbl_CAT_Municipio on cp.intMunicipioID equals muni.intMunicipioID
                                     join edo in dbRisDA.tbl_CAT_Estado on muni.intEstadoID equals edo.intEstadoID
                                     where cp.vchCodigoPostal.Trim() == vchCodigoPostal.Trim()
                                     select new
                                     {
                                         intCodigoPostalID = cp.intCodigoPostalID,
                                         vchCodigoPostal = cp.vchCodigoPostal,
                                         vchColonia = cp.vchColonia,
                                         intMunicipioID = cp.intMunicipioID,
                                         vchMunicipio = muni.vchMunicipio,
                                         intEstadoID = muni.intEstadoID,
                                         vchEstado = edo.vchEstado
                                     }).ToList();
                        if (query != null)
                        {
                            if (query.Count > 0)
                            {
                                foreach (var item in query)
                                {
                                    clsDireccion mdl = new clsDireccion();
                                    mdl.intCodigoPostalID = item.intCodigoPostalID;
                                    mdl.intEstadoID = (int)item.intEstadoID;
                                    mdl.intMunicipioID = (int)item.intMunicipioID;
                                    mdl.vchCodigoPostal = item.vchCodigoPostal;
                                    mdl.vchColonia = item.vchColonia;
                                    mdl.vchEstado = item.vchEstado;
                                    mdl.vchMunicipio = item.vchMunicipio;
                                    lst.Add(mdl);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception egDP)
            {
                Log.EscribeLog("Existe un error en getDireccionPaciente: " + egDP.Message, 3, user);
            }
            return lst;
        }

        public bool setPaciente(clsPaciente mdlPaciente, clsDireccion mdlDireccion, List<tbl_REL_IdentificacionPaciente> lstIdent, List<tbl_DET_PacienteDinamico> lstVarAdic, string user, ref string mensaje, ref int intPacienteID)
        {
            bool valido = false;
            bool validoPaciente = false;
            try
            {
                //master
                tbl_MST_Paciente _paciente = new tbl_MST_Paciente();
                using (dbRisDA = new RISLiteEntities())
                {
                    var query = (from item in dbRisDA.tbl_REL_SitioPaciente
                                 join pac in dbRisDA.tbl_MST_Paciente on item.intPacienteID equals pac.intPacienteID
                                 where item.intSitioID == mdlPaciente.intSitioID && (bool)pac.bitActivo
                                 select new
                                 {
                                     vchNombre = pac.vchNombre,
                                     vchApellidos = pac.vchApellidos,
                                     intGeneroID = pac.intGeneroID,
                                     datFechaNac = pac.datFechaNac
                                 }).ToList();
                    if (query != null)
                    {
                        if (!query.Any(x => x.vchNombre.ToUpper() == mdlPaciente.vchNombre.ToUpper() && x.vchApellidos.ToUpper() == mdlPaciente.vchApellidos.ToUpper()
                     && x.intGeneroID == mdlPaciente.intGeneroID && x.datFechaNac == mdlPaciente.datFechaNac))
                        {
                            _paciente.bitActivo = true;
                            _paciente.datFecha = DateTime.Now;
                            _paciente.datFechaNac = mdlPaciente.datFechaNac;
                            _paciente.intGeneroID = mdlPaciente.intGeneroID;
                            _paciente.vchApellidos = mdlPaciente.vchApellidos;
                            _paciente.vchNombre = mdlPaciente.vchNombre;
                            _paciente.vchUserAdmin = user;
                            dbRisDA.tbl_MST_Paciente.Add(_paciente);
                            dbRisDA.SaveChanges();
                            validoPaciente = true;
                        }
                        else
                        {
                            validoPaciente = false;
                            mensaje += " El paciente ya existe, favor de verificar.";
                            valido = false;
                        }
                    }
                    else
                    {
                        _paciente.bitActivo = true;
                        _paciente.datFecha = DateTime.Now;
                        _paciente.datFechaNac = mdlPaciente.datFechaNac;
                        _paciente.intGeneroID = mdlPaciente.intGeneroID;
                        _paciente.vchApellidos = mdlPaciente.vchApellidos;
                        _paciente.vchNombre = mdlPaciente.vchNombre;
                        _paciente.vchUserAdmin = user;
                        dbRisDA.tbl_MST_Paciente.Add(_paciente);
                        dbRisDA.SaveChanges();
                        validoPaciente = true;
                    }
                }

                if (validoPaciente && _paciente.intPacienteID > 0)
                {
                    //REl Sitio_Paciente
                    try
                    {
                        using (dbRisDA = new RISLiteEntities())
                        {
                            tbl_REL_SitioPaciente relSitio = new tbl_REL_SitioPaciente();
                            if (!dbRisDA.tbl_REL_SitioPaciente.Any(x => x.intSitioID == mdlPaciente.intSitioID && x.intPacienteID == _paciente.intPacienteID))
                            {
                                relSitio.bitActivo = true;
                                relSitio.datFecha = DateTime.Now;
                                relSitio.intPacienteID = _paciente.intPacienteID;
                                relSitio.intSitioID = mdlPaciente.intSitioID;
                                relSitio.vchUserAdmin = user;
                                dbRisDA.tbl_REL_SitioPaciente.Add(relSitio);
                                dbRisDA.SaveChanges();
                            }
                        }
                    }
                    catch (Exception eINsert)
                    {
                        Log.EscribeLog("Existe un error al insertar en tbl_REL_SitioPaciente: " + eINsert.Message, 3, user);
                    }


                    //Direccion
                    try
                    {
                        using (dbRisDA = new RISLiteEntities())
                        {
                            if (!dbRisDA.tbl_DET_DireccionPaciente.Any(x => x.intPacienteID == _paciente.intPacienteID))
                            {
                                tbl_DET_DireccionPaciente dir = new tbl_DET_DireccionPaciente();
                                dir.bitActivo = true;
                                dir.datFecha = DateTime.Now;
                                dir.intCodigoPostalID = mdlDireccion.intCodigoPostalID;
                                dir.intPacienteID = _paciente.intPacienteID;
                                dir.vchCalle = mdlDireccion.vchCalle;
                                dir.vchNumero = mdlDireccion.vchNumero;
                                dir.vchUserAdmin = user;
                                dbRisDA.tbl_DET_DireccionPaciente.Add(dir);
                                dbRisDA.SaveChanges();
                            }
                        }
                    }
                    catch (Exception eINsert)
                    {
                        Log.EscribeLog("Existe un error al insertar en tbl_DET_DireccionPaciente: " + eINsert.Message, 3, user);
                    }
                    //Detalle
                    try
                    {
                        using (dbRisDA = new RISLiteEntities())
                        {
                            if (!dbRisDA.tbl_DET_Paciente.Any(x => x.intPacienteID == _paciente.intPacienteID))
                            {
                                tbl_DET_Paciente pac = new tbl_DET_Paciente();
                                pac.bitActivo = true;
                                pac.datFecha = DateTime.Now;
                                pac.intPacienteID = _paciente.intPacienteID;
                                pac.vchContactoAcompaniante = "";
                                pac.vchEmail = mdlPaciente.vchEmail;
                                pac.vchNombreAcompaniante = "";
                                pac.vchNumeroContacto = mdlPaciente.vchNumeroContacto;
                                pac.vchParentesco = "";
                                pac.vchUserAdmin = user;
                                dbRisDA.tbl_DET_Paciente.Add(pac);
                                dbRisDA.SaveChanges();
                            }
                        }
                    }
                    catch (Exception eINsert)
                    {
                        Log.EscribeLog("Existe un error al insertar en tbl_DET_Paciente: " + eINsert.Message, 3, user);
                    }

                    //Identificaciones
                    try
                    {
                        foreach (tbl_REL_IdentificacionPaciente mdlIdent in lstIdent)
                        {
                            if (mdlIdent != null)
                            {
                                if (mdlIdent.intIdentificacionID != int.MinValue && mdlIdent.vchValor != "")
                                {
                                    using (dbRisDA = new RISLiteEntities())
                                    {
                                        if (!dbRisDA.tbl_REL_IdentificacionPaciente.Any(x => x.intIdentificacionID == (int)mdlIdent.intIdentificacionID && x.vchValor == mdlIdent.vchValor))
                                        {
                                            tbl_REL_IdentificacionPaciente mdl = new tbl_REL_IdentificacionPaciente();
                                            mdl.bitActivo = true;
                                            mdl.datFecha = DateTime.Now;
                                            mdl.intIdentificacionID = mdlIdent.intIdentificacionID;
                                            mdl.intPacienteID = _paciente.intPacienteID;
                                            mdl.vchValor = mdlIdent.vchValor;
                                            mdl.vchUserAdmin = user;
                                            dbRisDA.tbl_REL_IdentificacionPaciente.Add(mdl);
                                            dbRisDA.SaveChanges();
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception eINsert)
                    {
                        Log.EscribeLog("Existe un error al insertar en tbl_REL_IdentificacionPaciente: " + eINsert.Message, 3, user);
                    }

                    //Variables Adicionales
                    try
                    {
                        foreach (tbl_DET_PacienteDinamico mdlAdic in lstVarAdic)
                        {
                            if (mdlAdic != null)
                            {
                                if (mdlAdic.intVarAdiPacienteID != int.MinValue && mdlAdic.vchValorVar != "")
                                {
                                    using (dbRisDA = new RISLiteEntities())
                                    {
                                        if (!dbRisDA.tbl_DET_PacienteDinamico.Any(x => x.intVarAdiPacienteID == (int)mdlAdic.intVarAdiPacienteID && x.vchValorVar == mdlAdic.vchValorVar))
                                        {
                                            tbl_DET_PacienteDinamico mdl = new tbl_DET_PacienteDinamico();
                                            mdl.bitActivo = true;
                                            mdl.datFecha = DateTime.Now;
                                            mdl.intPacienteID = _paciente.intPacienteID;
                                            mdl.intVarAdiPacienteID = mdlAdic.intVarAdiPacienteID;
                                            mdl.vchValorVar = mdlAdic.vchValorVar;
                                            mdl.vchUserAdmin = user;
                                            dbRisDA.tbl_DET_PacienteDinamico.Add(mdl);
                                            dbRisDA.SaveChanges();
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception eINsert)
                    {
                        Log.EscribeLog("Existe un error al insertar en tbl_DET_PacienteDinamico: " + eINsert.Message, 3, user);
                    }

                    intPacienteID = Convert.ToInt32(_paciente.intPacienteID);
                    valido = true;
                }
            }
            catch (Exception esP)
            {
                mensaje += esP.Message;
                valido = false;
                Log.EscribeLog("Existe un error en setPaciente: " + esP.Message, 3, user);
            }
            return valido;
        }

        public bool getPacienteDetalle(int intPacienteID, string user, ref clsPaciente paciente, ref clsDireccion direccion, ref List<tbl_REL_IdentificacionPaciente> lstIden, ref List<clsVarAcicionales> lstVarAdi, ref string mensaje)
        {
            bool valido = false;
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    if (dbRisDA.tbl_MST_Paciente.Any(x => x.intPacienteID == intPacienteID))
                    {
                        var query = (from pac in dbRisDA.tbl_MST_Paciente
                                     where pac.intPacienteID == intPacienteID
                                     select new
                                     {
                                         PacienteID = pac.intPacienteID,
                                         datFecNac = pac.datFechaNac,
                                         intGeneroID = pac.intGeneroID,
                                         vchApellidos = pac.vchApellidos,
                                         vchNombre = pac.vchNombre
                                     }).ToList().First();
                        if (query != null)
                        {
                            paciente.datFechaNac = (DateTime)query.datFecNac;
                            paciente.intGeneroID = (int)query.intGeneroID;
                            paciente.intPacienteID = intPacienteID;
                            paciente.vchApellidos = query.vchApellidos;
                            paciente.vchNombre = query.vchNombre;
                        }
                    }
                }

                using (dbRisDA = new RISLiteEntities())
                {
                    if (dbRisDA.tbl_DET_Paciente.Any(x => x.intPacienteID == intPacienteID))
                    {
                        var query = (from pac in dbRisDA.tbl_DET_Paciente
                                     where pac.intPacienteID == intPacienteID
                                     select new
                                     {
                                         PacienteID = pac.intPacienteID,
                                         intDETPacienteID = pac.intDETPacienteID,
                                         vchEmail = pac.vchEmail,
                                         vchNumeroContacto = pac.vchNumeroContacto
                                     }).ToList().First();
                        if (query != null)
                        {
                            paciente.intDETPacienteID = (int)query.intDETPacienteID;
                            paciente.vchEmail = query.vchEmail;
                            paciente.vchNumeroContacto = query.vchNumeroContacto;
                        }
                    }
                }

                using (dbRisDA = new RISLiteEntities())
                {
                    if (dbRisDA.tbl_DET_Paciente.Any(x => x.intPacienteID == intPacienteID))
                    {
                        var query = (from pac in dbRisDA.tbl_DET_Paciente
                                     where pac.intPacienteID == intPacienteID
                                     select new
                                     {
                                         PacienteID = pac.intPacienteID,
                                         intDETPacienteID = pac.intDETPacienteID,
                                         vchEmail = pac.vchEmail,
                                         vchNumeroContacto = pac.vchNumeroContacto
                                     }).ToList().First();
                        if (query != null)
                        {
                            paciente.intPacienteID = (int)query.PacienteID;
                            paciente.intDETPacienteID = (int)query.intDETPacienteID;
                            paciente.vchEmail = query.vchEmail;
                            paciente.vchNumeroContacto = query.vchNumeroContacto;
                        }
                    }
                }

                using (dbRisDA = new RISLiteEntities())
                {
                    if (dbRisDA.tbl_DET_DireccionPaciente.Any(x => x.intPacienteID == intPacienteID))
                    {
                        var query = (from pac in dbRisDA.tbl_DET_DireccionPaciente
                                     join cp in dbRisDA.tbl_CAT_CodigoPostal on pac.intCodigoPostalID equals cp.intCodigoPostalID
                                     join muni in dbRisDA.tbl_CAT_Municipio on cp.intMunicipioID equals muni.intMunicipioID
                                     join edo in dbRisDA.tbl_CAT_Estado on muni.intEstadoID equals edo.intEstadoID
                                     where pac.intPacienteID == intPacienteID
                                     select new
                                     {
                                         PacienteID = pac.intPacienteID,
                                         intDireccioniD = pac.intDireccionID,
                                         intCodigoPostalID = pac.intCodigoPostalID,
                                         vchCalle = pac.vchCalle,
                                         vchNumero = pac.vchNumero,
                                         intMunicipioID = cp.intMunicipioID,
                                         vchCodigoPostal = cp.vchCodigoPostal,
                                         vchColonia = cp.vchColonia,
                                         intEstadoID = muni.intEstadoID,
                                         vchMunicipio = muni.vchMunicipio,
                                         vchEstado = edo.vchEstado
                                     }).ToList().First();
                        if (query != null)
                        {
                            direccion.intCodigoPostalID = (int)query.intCodigoPostalID;
                            direccion.intEstadoID = (int)query.intEstadoID;
                            direccion.intMunicipioID = (int)query.intMunicipioID;
                            direccion.vchCalle = query.vchCalle;
                            direccion.vchCodigoPostal = query.vchCodigoPostal;
                            direccion.vchColonia = query.vchColonia;
                            direccion.vchEstado = query.vchEstado;
                            direccion.vchMunicipio = query.vchMunicipio;
                            direccion.vchNumero = query.vchNumero;
                            direccion.intDireccionID = query.intDireccioniD;
                        }
                        valido = true;
                    }
                }

                //Identificaciones
                using (dbRisDA = new RISLiteEntities())
                {
                    if (dbRisDA.tbl_REL_IdentificacionPaciente.Any(x => x.intPacienteID == intPacienteID))
                    {
                        var query = dbRisDA.tbl_REL_IdentificacionPaciente.Where(x => x.intPacienteID == intPacienteID).ToList();
                        if (query != null)
                        {
                            if (query.Count > 0)
                            {
                                lstIden.AddRange(query);
                            }
                        }
                    }
                }

                //Variables Adicionales
                using (dbRisDA = new RISLiteEntities())
                {
                    if (dbRisDA.tbl_DET_PacienteDinamico.Any(x => x.intPacienteID == intPacienteID))
                    {
                        var query = dbRisDA.tbl_DET_PacienteDinamico.Where(x => x.intPacienteID == intPacienteID).ToList();
                        if (query != null)
                        {
                            if (query.Count > 0)
                            {
                                foreach (tbl_DET_PacienteDinamico item in query)
                                {
                                    clsVarAcicionales mdl = new clsVarAcicionales();
                                    mdl.intADIPacienteID = item.intADIPacienteID;
                                    mdl.bitActivo = (bool)item.bitActivo;
                                    mdl.datFecha = (DateTime)item.datFecha;
                                    mdl.intVariableAdiID = (int)item.intVarAdiPacienteID;
                                    mdl.vchValorAdicional = item.vchValorVar;
                                    mdl.vchUserAdmin = item.vchUserAdmin;
                                    lstVarAdi.Add(mdl);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception egPD)
            {
                valido = false;
                mensaje += egPD.Message;
                Log.EscribeLog("Existe un error en getPacienteDetalle: " + egPD.Message, 3, user);
            }
            return valido;
        }

        public bool getPacienteAdicionales(int intPacienteID, ref List<tbl_REL_IdentificacionPaciente> lstIden, ref List<clsVarAcicionales> lstVarAdi, string user)
        {
            bool valido = false;
            try
            {
                //Identificaciones
                using (dbRisDA = new RISLiteEntities())
                {
                    if (dbRisDA.tbl_REL_IdentificacionPaciente.Any(x => x.intPacienteID == intPacienteID))
                    {
                        var query = dbRisDA.tbl_REL_IdentificacionPaciente.Where(x => x.intPacienteID == intPacienteID).ToList();
                        if (query != null)
                        {
                            if (query.Count > 0)
                            {
                                lstIden.AddRange(query);
                            }
                        }
                    }
                }

                //Variables Adicionales
                using (dbRisDA = new RISLiteEntities())
                {
                    if (dbRisDA.tbl_DET_PacienteDinamico.Any(x => x.intPacienteID == intPacienteID))
                    {
                        var query = dbRisDA.tbl_DET_PacienteDinamico.Where(x => x.intPacienteID == intPacienteID).ToList();
                        if (query != null)
                        {
                            if (query.Count > 0)
                            {
                                foreach (tbl_DET_PacienteDinamico item in query)
                                {
                                    clsVarAcicionales mdl = new clsVarAcicionales();
                                    mdl.intADIPacienteID = item.intADIPacienteID;
                                    mdl.bitActivo = (bool)item.bitActivo;
                                    mdl.datFecha = (DateTime)item.datFecha;
                                    mdl.intVariableAdiID = (int)item.intVarAdiPacienteID;
                                    mdl.vchValorAdicional = item.vchValorVar;
                                    mdl.vchUserAdmin = item.vchUserAdmin;
                                    lstVarAdi.Add(mdl);
                                }
                            }
                        }
                    }
                }
                valido = true;
            }
            catch (Exception gpA)
            {
                valido = false;
                Log.EscribeLog("Existe un error en getPacienteAdicionales: " + gpA.Message, 3, user);
            }
            return valido;
        }

        public List<tbl_DET_Cita> getCitaAdicionales(int intCitaID, string user)
        {
            List<tbl_DET_Cita> lstCitaAdicionales = new List<tbl_DET_Cita>();
            try
            {
                //Variables Adicionales
                using (dbRisDA = new RISLiteEntities())
                {
                    if (dbRisDA.tbl_DET_Cita.Any(x => x.intCitaID == intCitaID))
                    {
                        var query = dbRisDA.tbl_DET_Cita.Where(x => x.intCitaID == intCitaID).ToList();
                        if (query != null)
                        {
                            if (query.Count > 0)
                            {
                                lstCitaAdicionales.AddRange(query);
                            }
                        }
                    }
                }
            }
            catch (Exception gpA)
            {
                Log.EscribeLog("Existe un error en getPacienteAdicionales: " + gpA.Message, 3, user);
            }
            return lstCitaAdicionales;
        }

        public List<stp_getDetalleCita_Result> get_stpDetalleCita(int intCitaID, string user)
        {
            List<stp_getDetalleCita_Result> lstCitaAdicionales = new List<stp_getDetalleCita_Result>();
            try
            {
                //Variables Adicionales
                using (dbRisDA = new RISLiteEntities())
                {
                    if (dbRisDA.tbl_DET_Cita.Any(x => x.intCitaID == intCitaID))
                    {
                        var query = dbRisDA.stp_getDetalleCita(intCitaID).ToList();
                        if (query != null)
                        {
                            if (query.Count > 0)
                            {
                                lstCitaAdicionales.AddRange(query);
                            }
                        }
                    }
                }
            }
            catch (Exception gpA)
            {
                Log.EscribeLog("Existe un error en get_stpDetalleCita: " + gpA.Message, 3, user);
            }
            return lstCitaAdicionales;
        }

        public bool getListaDetalleCita(string user, int idcita)
        {
            bool bandera_rev_detalle = false;
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    if (dbRisDA.tbl_MST_Cita.Any())
                    {
                        var query = dbRisDA.tbl_DET_Cita.Where(x => x.intCitaID == idcita).ToList();

                        if (query != null)
                        {
                            if (query.Count > 0)
                            {
                                bandera_rev_detalle = true;
                            }
                        }
                    }
                }
            }
            catch (Exception eLT)
            {
                Log.EscribeLog("Existe un error en getListaDetalleCita: " + eLT.Message, 3, user);
            }
            return bandera_rev_detalle;
        }

        public List<string> getBusquedaPacientes(string busqueda, int intSitioID, string user)
        {
            List<string> lst = new List<string>();
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    var query = dbRisDA.stp_getBusquedaPaciente(busqueda.ToUpper(), intSitioID).ToList();
                    if (query != null)
                    {
                        if (query.Count > 0)
                        {
                            foreach (var item in query)
                            {
                                string cadena = "";
                                cadena = item.CADENA;
                                lst.Add(cadena);
                            }
                        }
                    }
                }
            }
            catch (Exception egBP)
            {
                Log.EscribeLog("Existe un error en getBusquedaPacientes: " + egBP.Message, 3, user);
            }
            return lst;
        }

        public List<clsPaciente> getBusquedaPacientesMod(string busqueda, int intSitioID, string user)
        {
            List<clsPaciente> lst = new List<clsPaciente>();
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    var query = dbRisDA.stp_getBusquedaPacienteMod(busqueda.ToUpper(), intSitioID).ToList();
                    if (query != null)
                    {
                        if (query.Count > 0)
                        {
                            foreach (var item in query)
                            {
                                clsPaciente pac = new clsPaciente();
                                pac.intPacienteID = (int)item.intPacienteID;
                                pac.vchNombre = item.CADENA;
                                pac.intSitioID = intSitioID;
                                lst.Add(pac);
                            }
                        }
                    }
                }
            }
            catch (Exception egBP)
            {
                Log.EscribeLog("Existe un error en getBusquedaPacientes: " + egBP.Message, 3, user);
            }
            return lst;
        }

        public List<clsPaciente> getBusquedaPacientesList(string busqueda, int intSitioID, string user)
        {
            List<clsPaciente> lst = new List<clsPaciente>();
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    var query = dbRisDA.stp_getBusquedaPacienteList(busqueda.ToUpper(), intSitioID).ToList();
                    if (query != null)
                    {
                        if (query.Count > 0)
                        {
                            foreach (var item in query)
                            {
                                clsPaciente mdl = new clsPaciente();
                                mdl.intPacienteID = (int)item.intPacienteID;
                                mdl.vchNombre = item.vchNombre;
                                mdl.datFechaNac = (DateTime)item.datFechaNac;
                                mdl.vchApellidos = item.NSS;
                                mdl.intSitioID = (int)item.intSitioID;
                                mdl.vchNombreSitio = item.vchNombreSitio;
                                lst.Add(mdl);
                            }
                        }
                    }
                }
            }
            catch (Exception egBP)
            {
                Log.EscribeLog("Existe un error en getBusquedaPacientesList: " + egBP.Message, 3, user);
            }
            return lst;
        }

        public List<string> getBusquedaEstudio(string busqueda, string user)
        {
            List<string> lst = new List<string>();
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    var query = dbRisDA.stp_getBusquedaEstudio(busqueda.ToUpper()).ToList();
                    if (query != null)
                    {
                        if (query.Count > 0)
                        {
                            foreach (var item in query)
                            {
                                string cadena = "";
                                cadena = item.CADENA;
                                lst.Add(cadena);
                            }
                        }
                    }
                }
            }
            catch (Exception egBP)
            {
                Log.EscribeLog("Existe un error en getBusquedaEstudio: " + egBP.Message, 3, user);
            }
            return lst;
        }

        public clsEstudio getEstudioDetalle(int intRELModPres, string user)
        {
            clsEstudio estudio = new clsEstudio();
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    if (dbRisDA.tbl_REL_ModalidadPrestacion.Any(x => x.intRELModPres == intRELModPres && (bool)x.bitActivo))
                    {
                        var query = (from item in dbRisDA.tbl_REL_ModalidadPrestacion
                                     join mod in dbRisDA.tbl_CAT_Modalidad on item.intModalidadID equals mod.intModalidadID
                                     join pres in dbRisDA.tbl_CAT_Prestacion on item.intPrestacionID equals pres.intPrestacionID
                                     where item.intRELModPres == intRELModPres && (bool)item.bitActivo
                                     select new
                                     {
                                         intRELModPres = item.intRELModPres,
                                         intModalidadID = item.intModalidadID,
                                         intPrestacionID = item.intPrestacionID,
                                         vchCodigo = mod.vchCodigo,
                                         vchModalidad = mod.vchModalidad,
                                         intDuracionMin = pres.intDuracionMin,
                                         vchPrestacion = pres.vchPrestacion
                                     }).ToList().First();
                        if (query != null)
                        {
                            estudio.cadena = query.vchCodigo + " - " + query.vchPrestacion;
                            estudio.intRelModPres = query.intRELModPres;
                            estudio.intModalidadID = (int)query.intModalidadID;
                            estudio.intPrestacionID = (int)query.intPrestacionID;
                            estudio.vchCodigo = query.vchCodigo;
                            estudio.vchModalidad = query.vchModalidad;
                            estudio.intDuracionMin = (int)query.intDuracionMin;
                            estudio.vchPrestacion = query.vchPrestacion;
                        }
                    }
                }
            }
            catch (Exception egBP)
            {
                Log.EscribeLog("Existe un error en getEstudioDetalle: " + egBP.Message, 3, user);
            }
            return estudio;
        }

        public List<clsEstudioNuevaCita> getEstudioDetalle_ModificacionCIta(string user, string intCitaID, int idtablacita)
        {
            List <clsEstudioNuevaCita> Lista_estudio = new List<clsEstudioNuevaCita>();
            try
            {
                int id = Convert.ToInt32(intCitaID);

                using (dbRisDA = new RISLiteEntities())
                {
                    //var query = (from RCE in dbRisDA.tbl_REL_CitaEstudio
                    //             join MC in dbRisDA.tbl_MST_Cita on RCE.intCitaID equals MC.intCitaID
                    //             where RCE.intEstudioID == id
                    //             select new
                    //             {
                    //                 intEstudioID_ = RCE.intEstudioID,
                    //                 intCitaID_ = RCE.intCitaID,
                    //                 datFechaCita_ = MC.datFechaCita
                    //             }).ToList().First();

                    //if (query != null)
                    //{
                    //    estudio.intEstudioID = (int)query.intEstudioID_;
                    //    estudio.fechaInicio = (DateTime)query.datFechaCita_;

                    //}

                    //var query = (from RCE in dbRisDA.tbl_REL_CitaEstudio
                    //             join ME in dbRisDA.tbl_MST_Estudio on RCE.intEstudioID equals ME.intEstudioID
                    //             join RMP in dbRisDA.tbl_REL_ModalidadPrestacion on ME.intRELModPres equals RMP.intRELModPres
                    //             join mod in dbRisDA.tbl_CAT_Modalidad on RMP.intModalidadID equals mod.intModalidadID
                    //             join pres in dbRisDA.tbl_CAT_Prestacion on RMP.intPrestacionID equals pres.intPrestacionID
                    //             where RCE.intEstudioID == id
                    //             select new
                    //             {
                    //                 intRELModPres = RMP.intRELModPres,
                    //                 intModalidadID = RMP.intModalidadID,
                    //                 intPrestacionID = RMP.intPrestacionID,
                    //                 vchCodigo = mod.vchCodigo,
                    //                 vchModalidad = mod.vchModalidad,
                    //                 intDuracionMin = pres.intDuracionMin,
                    //                 vchPrestacion = pres.vchPrestacion,


                    //                 intEstudioID_ = ME.intEstudioID,
                    //                 //intCitaID_ = RCE.intCitaID,
                    //                 datFechaCita_ = ME.datFechaInicio
                    //             }).ToList().First();

                    //if (query != null)
                    //{
                    //    estudio.intEstudioID = (int)query.intEstudioID_;
                    //    estudio.fechaInicio = (DateTime)query.datFechaCita_;
                    //    estudio.intconsecutivo_Modalidad = idtablacita;
                    //    estudio.cadena = query.vchCodigo + " - " + query.vchPrestacion;
                    //    estudio.intRelModPres = query.intRELModPres;
                    //    estudio.intModalidadID = (int)query.intModalidadID;
                    //    estudio.intPrestacionID = (int)query.intPrestacionID;
                    //    estudio.vchCodigo = query.vchCodigo;
                    //    estudio.vchModalidad = query.vchModalidad;
                    //    estudio.intDuracionMin = (int)query.intDuracionMin;
                    //    estudio.vchPrestacion = query.vchPrestacion;
                    //}


                    var query = (from RCE in dbRisDA.tbl_REL_CitaEstudio
                                 join ME in dbRisDA.tbl_MST_Estudio on RCE.intEstudioID equals ME.intEstudioID
                                 join RMP in dbRisDA.tbl_REL_ModalidadPrestacion on ME.intRELModPres equals RMP.intRELModPres
                                 join mod in dbRisDA.tbl_CAT_Modalidad on RMP.intModalidadID equals mod.intModalidadID
                                 join pres in dbRisDA.tbl_CAT_Prestacion on RMP.intPrestacionID equals pres.intPrestacionID
                                 where RCE.intCitaID == id
                                 select new
                                 {
                                     intRELModPres = RMP.intRELModPres,
                                     intModalidadID = RMP.intModalidadID,
                                     intPrestacionID = RMP.intPrestacionID,
                                     vchCodigo = mod.vchCodigo,
                                     vchModalidad = mod.vchModalidad,
                                     intDuracionMin = pres.intDuracionMin,
                                     vchPrestacion = pres.vchPrestacion,


                                     intEstudioID_ = ME.intEstudioID,
                                     //intCitaID_ = RCE.intCitaID,
                                     datFechaCita_ = ME.datFechaInicio
                                 }).ToList();

                    if (query != null)
                    {
                        foreach (var elemento_cita in query)
                        {
                            clsEstudioNuevaCita estudio = new clsEstudioNuevaCita();
                            estudio.intEstudioID = (int)elemento_cita.intEstudioID_;
                            estudio.fechaInicio = (DateTime)elemento_cita.datFechaCita_;
                            estudio.intconsecutivo_Modalidad = idtablacita;
                            estudio.cadena = elemento_cita.vchCodigo + " - " + elemento_cita.vchPrestacion;
                            estudio.intRelModPres = elemento_cita.intRELModPres;
                            estudio.intModalidadID = (int)elemento_cita.intModalidadID;
                            estudio.intPrestacionID = (int)elemento_cita.intPrestacionID;
                            estudio.vchCodigo = elemento_cita.vchCodigo;
                            estudio.vchModalidad = elemento_cita.vchModalidad;
                            estudio.intDuracionMin = (int)elemento_cita.intDuracionMin;
                            estudio.vchPrestacion = elemento_cita.vchPrestacion;
                            Lista_estudio.Add(estudio);
                        }
                    }
                }
            }
            catch (Exception egBP)
            {
                Log.EscribeLog("Existe un error en getEstudioDetalle_ModificacionCIta: " + egBP.Message, 3, user);
            }
            return Lista_estudio;
        }

        public bool ModificacionCita(clsPaciente paciente, List<clsAdicionales> lstAdicionales, List<clsEstudioNuevaCita> lstEstudios, string user, ref string mensaje, ref tbl_MST_Cita cita, int _cita)
        {
            bool valido = false;
            try
            {
                //det_Cita
                try
                {
                    if (lstAdicionales.Count > 0)
                    {
                        foreach (clsAdicionales item in lstAdicionales)
                        {
                            tbl_DET_Cita detCita = new tbl_DET_Cita();
                            using (dbRisDA = new RISLiteEntities())
                            {
                                if (!dbRisDA.tbl_DET_Cita.Any(x => x.intAdicionalesID == item.intAdicionalesID && x.intCitaID == _cita))
                                {

                                    detCita.bitActivo = true;
                                    detCita.datFecha = DateTime.Now;
                                    detCita.intCitaID = _cita;
                                    detCita.intAdicionalesID = item.intAdicionalesID;
                                    if (item.vchObservaciones != "")
                                        detCita.vchObservaciones = item.vchObservaciones;
                                    detCita.vchUserAdmin = user;
                                    detCita.vchValor = item.vchValor;
                                    dbRisDA.tbl_DET_Cita.Add(detCita);
                                    dbRisDA.SaveChanges();
                                }
                                else
                                {

                                    detCita = dbRisDA.tbl_DET_Cita.First(x => x.intAdicionalesID == item.intAdicionalesID && x.intCitaID == _cita);
                                    detCita.vchValor = item.vchValor;
                                    detCita.datFecha = DateTime.Now;
                                    detCita.vchUserAdmin = user;
                                    if (item.vchObservaciones != "")
                                        detCita.vchObservaciones = item.vchObservaciones;
                                    dbRisDA.SaveChanges();
                                }
                                valido = true;
                            }
                        }
                    }
                    else
                    {
                        valido = true;
                    }
                }
                catch (Exception e)
                {
                    valido = false;
                    Log.EscribeLog("Existe un error en tbl_REL_PacienteCita: " + e.Message, 3, user);
                }

                //Estudios
                try
                {
                    if (lstEstudios.Count > 0)
                    {
                        foreach (clsEstudioNuevaCita item in lstEstudios)
                        {
                            tbl_MST_Estudio estudio = new tbl_MST_Estudio();
                            using (dbRisDA = new RISLiteEntities())
                            {

                                if (!dbRisDA.tbl_MST_Estudio.Any(x => x.intEstudioID == item.intEstudioID))
                                {
                                    estudio.bitActivo = true;
                                    estudio.datFecha = DateTime.Now;
                                    estudio.datFechaFin = item.fechaFin;
                                    estudio.datFechaInicio = item.fechaInicio;
                                    estudio.intEstatusEstudio = 1;
                                    estudio.intRELModPres = item.intRelModPres;
                                    estudio.vchDescripcion = paciente.vchNombre + " " + paciente.vchApellidos;
                                    estudio.vchTitulo = item.vchTitulo;
                                    estudio.vchUserAdmin = user;
                                    dbRisDA.tbl_MST_Estudio.Add(estudio);
                                    dbRisDA.SaveChanges();

                                    if (estudio.intEstudioID > 0)
                                    {
                                        using (dbRisDA = new RISLiteEntities())
                                        {
                                            if (!dbRisDA.tbl_REL_CitaEstudio.Any(x => x.intCitaID == _cita && x.intEstudioID == estudio.intEstudioID))
                                            {
                                                tbl_REL_CitaEstudio relCitaEst = new tbl_REL_CitaEstudio();
                                                relCitaEst.bitActivo = true;
                                                relCitaEst.datFecha = DateTime.Now;
                                                relCitaEst.intCitaID = _cita;
                                                relCitaEst.intEstudioID = estudio.intEstudioID;
                                                relCitaEst.vchUserAdmin = user;
                                                dbRisDA.tbl_REL_CitaEstudio.Add(relCitaEst);
                                                dbRisDA.SaveChanges();
                                            }
                                        }
                                    }
                                }

                                else
                                {

                                    if (!dbRisDA.tbl_MST_Estudio.Any(x => x.intEstudioID == item.intEstudioID && x.datFechaInicio == item.fechaInicio))
                                    {
                                        using (dbRisDA = new RISLiteEntities())
                                        {
                                            estudio = dbRisDA.tbl_MST_Estudio.First(x => x.intEstudioID == item.intEstudioID);
                                            //estudio.bitActivo = true;
                                            estudio.datFecha = DateTime.Now;
                                            estudio.datFechaFin = item.fechaInicio.AddHours(1);
                                            estudio.datFechaInicio = item.fechaInicio;
                                            //estudio.intEstatusEstudio = 1;
                                            //estudio.intRELModPres = item.intRelModPres;
                                            //estudio.vchDescripcion = paciente.vchNombre + " " + paciente.vchApellidos;
                                            //estudio.vchTitulo = item.vchTitulo;
                                            estudio.vchUserAdmin = user;
                                            dbRisDA.SaveChanges();
                                        }
                                    }
                                }
                            }

                            valido = true;
                        }
                    }
                }
                catch (Exception eEstudios)
                {
                    valido = false;
                    Log.EscribeLog("Existe un error al insertar los estudios: " + eEstudios.Message, 3, user);
                }
            }
            catch (Exception esCN)
            {
                valido = false;
                Log.EscribeLog("Existe un error en setCitaNueva: " + esCN.Message, 3, user);
            }
            return valido;
        }

        public clsEstudioNuevaCita getEstudioDetalle_citaNueva(int intRELModPres, string user, int idtablacita)
        {
            clsEstudioNuevaCita estudio = new clsEstudioNuevaCita();
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    if (dbRisDA.tbl_REL_ModalidadPrestacion.Any(x => x.intRELModPres == intRELModPres && (bool)x.bitActivo))
                    {
                        var query = (from item in dbRisDA.tbl_REL_ModalidadPrestacion
                                     join mod in dbRisDA.tbl_CAT_Modalidad on item.intModalidadID equals mod.intModalidadID
                                     join pres in dbRisDA.tbl_CAT_Prestacion on item.intPrestacionID equals pres.intPrestacionID
                                     where item.intRELModPres == intRELModPres && (bool)item.bitActivo
                                     select new
                                     {
                                         intRELModPres = item.intRELModPres,
                                         intModalidadID = item.intModalidadID,
                                         intPrestacionID = item.intPrestacionID,
                                         vchCodigo = mod.vchCodigo,
                                         vchModalidad = mod.vchModalidad,
                                         intDuracionMin = pres.intDuracionMin,
                                         vchPrestacion = pres.vchPrestacion
                                     }).ToList().First();
                        if (query != null)
                        {
                            estudio.intconsecutivo_Modalidad = idtablacita;
                            estudio.cadena = query.vchCodigo + " - " + query.vchPrestacion;
                            estudio.intRelModPres = query.intRELModPres;
                            estudio.intModalidadID = (int)query.intModalidadID;
                            estudio.intPrestacionID = (int)query.intPrestacionID;
                            estudio.vchCodigo = query.vchCodigo;
                            estudio.vchModalidad = query.vchModalidad;
                            estudio.intDuracionMin = (int)query.intDuracionMin;
                            estudio.vchPrestacion = query.vchPrestacion;
                        }
                    }
                }
            }
            catch (Exception egBP)
            {
                Log.EscribeLog("Existe un error en getEstudioDetalle_citaNueva: " + egBP.Message, 3, user);
            }
            return estudio;
        }

        public bool setActualizaPaciente(clsPaciente mdlPaciente, clsDireccion mdlDireccion, List<tbl_REL_IdentificacionPaciente> lstIdent, List<tbl_DET_PacienteDinamico> lstVarAdic, string user, ref string mensaje)
        {
            bool valido = false;
            try
            {
                //Master
                using (dbRisDA = new RISLiteEntities())
                {
                    if (dbRisDA.tbl_MST_Paciente.Any(x => x.intPacienteID == mdlPaciente.intPacienteID))
                    {
                        tbl_MST_Paciente mdl = dbRisDA.tbl_MST_Paciente.First(x => x.intPacienteID == mdlPaciente.intPacienteID);
                        if (mdl != null)
                        {
                            mdl.bitActivo = true;
                            mdl.datFecha = DateTime.Now;
                            mdl.datFechaNac = mdlPaciente.datFechaNac;
                            mdl.intGeneroID = mdlPaciente.intGeneroID;
                            mdl.vchApellidos = mdlPaciente.vchApellidos;
                            mdl.vchNombre = mdlPaciente.vchNombre;
                            mdl.vchUserAdmin = user;
                            dbRisDA.SaveChanges();
                        }
                    }
                }

                //Detalle
                using (dbRisDA = new RISLiteEntities())
                {
                    if (dbRisDA.tbl_DET_Paciente.Any(x => x.intDETPacienteID == mdlPaciente.intDETPacienteID))
                    {
                        tbl_DET_Paciente mdl = dbRisDA.tbl_DET_Paciente.First(x => x.intDETPacienteID == mdlPaciente.intDETPacienteID);
                        if (mdl != null)
                        {
                            mdl.bitActivo = true;
                            mdl.datFecha = DateTime.Now;
                            mdl.vchEmail = mdlPaciente.vchEmail;
                            mdl.vchNumeroContacto = mdlPaciente.vchNumeroContacto;
                            mdl.vchUserAdmin = user;
                            dbRisDA.SaveChanges();
                        }
                    }
                    else
                    {
                        tbl_DET_Paciente mdl = new tbl_DET_Paciente();
                        mdl.bitActivo = true;
                        mdl.datFecha = DateTime.Now;
                        mdl.vchEmail = mdlPaciente.vchEmail;
                        mdl.vchNumeroContacto = mdlPaciente.vchNumeroContacto;
                        mdl.vchUserAdmin = user;
                        mdl.intPacienteID = mdlPaciente.intPacienteID;
                        dbRisDA.tbl_DET_Paciente.Add(mdl);
                        dbRisDA.SaveChanges();
                    }
                }

                //Direccion
                using (dbRisDA = new RISLiteEntities())
                {
                    if (dbRisDA.tbl_DET_DireccionPaciente.Any(x => x.intDireccionID == mdlDireccion.intDireccionID))
                    {
                        tbl_DET_DireccionPaciente mdl = dbRisDA.tbl_DET_DireccionPaciente.First(x => x.intDireccionID == mdlDireccion.intDireccionID);
                        if (mdl != null)
                        {
                            mdl.bitActivo = true;
                            mdl.datFecha = DateTime.Now;
                            mdl.intCodigoPostalID = mdlDireccion.intCodigoPostalID;
                            mdl.vchCalle = mdlDireccion.vchCalle;
                            mdl.vchNumero = mdlDireccion.vchNumero;
                            mdl.vchUserAdmin = user;
                            dbRisDA.SaveChanges();
                        }
                    }
                    else
                    {
                        tbl_DET_DireccionPaciente mdl = new tbl_DET_DireccionPaciente();
                        mdl.bitActivo = true;
                        mdl.datFecha = DateTime.Now;
                        mdl.intCodigoPostalID = mdlDireccion.intCodigoPostalID;
                        mdl.vchCalle = mdlDireccion.vchCalle;
                        mdl.vchNumero = mdlDireccion.vchNumero;
                        mdl.vchUserAdmin = user;
                        mdl.intPacienteID = mdlPaciente.intPacienteID;
                        dbRisDA.tbl_DET_DireccionPaciente.Add(mdl);
                        dbRisDA.SaveChanges();
                    }
                }

                //Identificaciones
                foreach (tbl_REL_IdentificacionPaciente mdlIdent in lstIdent)
                {
                    if (mdlIdent != null)
                    {
                        if (mdlIdent.intRELIdenPacienteID != int.MinValue && mdlIdent.intRELIdenPacienteID > 0)
                        {
                            using (dbRisDA = new RISLiteEntities())
                            {
                                if (dbRisDA.tbl_REL_IdentificacionPaciente.Any(x => x.intRELIdenPacienteID == mdlIdent.intRELIdenPacienteID))
                                {
                                    tbl_REL_IdentificacionPaciente mdl = dbRisDA.tbl_REL_IdentificacionPaciente.First(x => x.intRELIdenPacienteID == mdlIdent.intRELIdenPacienteID);
                                    mdl.bitActivo = true;
                                    mdl.datFecha = DateTime.Now;
                                    mdl.intIdentificacionID = mdlIdent.intIdentificacionID;
                                    mdl.vchValor = mdlIdent.vchValor;
                                    mdl.vchUserAdmin = user;
                                    dbRisDA.SaveChanges();
                                }
                            }
                        }
                        else
                        {
                            using (dbRisDA = new RISLiteEntities())
                            {
                                tbl_REL_IdentificacionPaciente mdl = new tbl_REL_IdentificacionPaciente();
                                mdl.bitActivo = true;
                                mdl.datFecha = DateTime.Now;
                                mdl.intIdentificacionID = mdlIdent.intIdentificacionID;
                                mdl.vchValor = mdlIdent.vchValor;
                                mdl.vchUserAdmin = user;
                                mdl.intPacienteID = mdlPaciente.intPacienteID;
                                dbRisDA.tbl_REL_IdentificacionPaciente.Add(mdl);
                                dbRisDA.SaveChanges();
                            }
                        }
                    }
                }

                //Var Adicionales
                foreach (tbl_DET_PacienteDinamico mdlAdic in lstVarAdic)
                {
                    if (mdlAdic != null)
                    {
                        if (mdlAdic.intADIPacienteID != int.MinValue && mdlAdic.intADIPacienteID > 0)
                        {
                            using (dbRisDA = new RISLiteEntities())
                            {
                                if (dbRisDA.tbl_DET_PacienteDinamico.Any(x => x.intADIPacienteID == mdlAdic.intADIPacienteID))
                                {
                                    tbl_DET_PacienteDinamico mdl = dbRisDA.tbl_DET_PacienteDinamico.First(x => x.intADIPacienteID == mdlAdic.intADIPacienteID);
                                    mdl.bitActivo = true;
                                    mdl.datFecha = DateTime.Now;
                                    mdl.intVarAdiPacienteID = mdlAdic.intVarAdiPacienteID;
                                    mdl.vchValorVar = mdlAdic.vchValorVar;
                                    mdl.vchUserAdmin = user;
                                    dbRisDA.SaveChanges();
                                }
                            }
                        }
                        else
                        {
                            using (dbRisDA = new RISLiteEntities())
                            {
                                tbl_DET_PacienteDinamico mdl = new tbl_DET_PacienteDinamico();
                                mdl.bitActivo = true;
                                mdl.datFecha = DateTime.Now;
                                mdl.intVarAdiPacienteID = mdlAdic.intVarAdiPacienteID;
                                mdl.vchValorVar = mdlAdic.vchValorVar;
                                mdl.vchUserAdmin = user;
                                dbRisDA.tbl_DET_PacienteDinamico.Add(mdl);
                                dbRisDA.SaveChanges();
                            }
                        }
                    }
                }
                valido = true;
            }
            catch (Exception eSAP)
            {
                valido = false;
                mensaje = eSAP.Message;
                Log.EscribeLog("Existe un error en setActualizaPaciente: " + eSAP.Message, 3, user);
            }
            return valido;
        }
        #endregion Paciente

        #region NuevaCitaAgenda

        public List<clsEventoCita> getListCitas_en_agenda(string user, int idmodalidad, DateTime fecha_Revision)
        {
            DateTime dt_fecha_sumada = fecha_Revision.AddDays(+3);
            //DateTime dt_fecha_restada = fecha_Revision.AddDays(-3);

            List<clsEventoCita> lst = new List<clsEventoCita>();
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    if (dbRisDA.tbl_CAT_Modalidad.Any())
                    {
                        var query = dbRisDA.tbl_CAT_Eventos.OrderBy(x => x.Start).Where(x => (x.intModalidadID == idmodalidad && (x.Start >= fecha_Revision && x.Start < dt_fecha_sumada))).ToList();
                        if (query != null)
                        {
                            if (query.Count > 0)
                            {
                                foreach (var item in query)
                                {
                                    clsEventoCita mdl = new clsEventoCita();
                                    mdl.TaskID = (int)item.TaskID;
                                    mdl.Start = (DateTime)item.Start;
                                    mdl.End = (DateTime)item.End;
                                    mdl.Title = item.Title;
                                    mdl.Description = item.Description;
                                    mdl.OwnerID = (int)item.OwnerID;

                                    mdl.RecurrenceException = item.RecurrenceException;
                                    mdl.intModalidadID = (int)item.intModalidadID;
                                    lst.Add(mdl);

                                }
                            }
                        }
                    }
                }
            }
            catch (Exception eLT)
            {
                Log.EscribeLog("Existe un error en getListCitas_en_agenda: " + eLT.Message, 3, user);
            }
            return lst;
        }

        public List<clsEventoCita> getListCitas_en_agenda_Sitio(string user, int idmodalidad, DateTime fecha_Revision, int idsitio)
        {
            DateTime dt_fecha_sumada = fecha_Revision.AddDays(+3);
            //DateTime dt_fecha_restada = fecha_Revision.AddDays(-3);

            List<clsEventoCita> lst = new List<clsEventoCita>();
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    if (dbRisDA.tbl_CAT_Modalidad.Any())
                    {
                        //var query = dbRisDA.tbl_CAT_Eventos.OrderBy(x => x.Start).Where(x => (x.intModalidadID == idmodalidad && (x.Start >= fecha_Revision && x.Start < dt_fecha_sumada))).ToList();

                        //var query = dbRisDA.stp_getBusquedaCita_Sitio(idmodalidad, idsitio).OrderBy(x => x.datFechaInicio).Where(y => y.intModalidadID == idmodalidad &&
                        //                                                                                                         y.intEstudioID == idsitio && 
                        //                                                                                                         y.datFechaInicio >= fecha_Revision
                        //                                                                                                         && y.datFechaInicio < dt_fecha_sumada).ToList();

                        var query = dbRisDA.stp_getBusquedaCita_Sitio(idmodalidad, idsitio).OrderBy(x => x.datFechaInicio).Where(y => y.datFechaInicio >= fecha_Revision
                                                                                                                                   && y.datFechaInicio < dt_fecha_sumada).ToList();
                        if (query != null)
                        {
                            if (query.Count > 0)
                            {
                                foreach (var item in query)
                                {
                                    clsEventoCita mdl = new clsEventoCita();
                                    mdl.TaskID = (int)item.intEstudioID;
                                    mdl.Start = (DateTime)item.datFechaInicio;
                                    mdl.End = (DateTime)item.datFechaFin;
                                    mdl.Title = item.vchTitulo;
                                    mdl.Description = item.vchDescripcion;
                                    mdl.intModalidadID = (int)item.intModalidadID;
                                    lst.Add(mdl);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception eLT)
            {
                Log.EscribeLog("Existe un error en getListCitas_en_agenda: " + eLT.Message, 3, user);
            }
            return lst;
        }

        public List<clsEventoCita> getListCitaAgenda(string user)
        {
            List<clsEventoCita> lst = new List<clsEventoCita>();
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    if (dbRisDA.tbl_CAT_Modalidad.Any())
                    {
                        var query = dbRisDA.tbl_CAT_Eventos.Where(x => (x.intModalidadID == 0)).ToList();
                        //var query = dbRisDA.tbl_CAT_Eventos.ToList();
                        if (query != null)
                        {
                            if (query.Count > 0)
                            {
                                foreach (var item in query)
                                {
                                    clsEventoCita mdl = new clsEventoCita();
                                    mdl.TaskID = (int)item.TaskID;
                                    mdl.Start = (DateTime)item.Start;
                                    mdl.End = (DateTime)item.End;
                                    mdl.Title = item.Title;
                                    mdl.Description = item.Description;
                                    mdl.OwnerID = (int)item.OwnerID;
                                    if (item.IsAllDay == null)
                                    {
                                        mdl.IsAllDay = false;
                                    }
                                    else
                                    {
                                        mdl.IsAllDay = (bool)item.IsAllDay;
                                    }
                                    //
                                    mdl.RecurrenceRule = item.RecurrenceRule;

                                    if (item.RecurrenceID == null)
                                    {
                                        mdl.RecurrenceID = 0;
                                    }
                                    else
                                    {
                                        mdl.RecurrenceID = (int)item.RecurrenceID;
                                    }


                                    mdl.RecurrenceException = item.RecurrenceException;
                                    //mdl.StarTimezone = (DateTime)item.StarTimezone;
                                    //mdl.EndTimezone = (DateTime)item.EndTimezone;
                                    mdl.intModalidadID = (int)item.intModalidadID;
                                    lst.Add(mdl);

                                }
                            }
                        }
                    }
                }
            }
            catch (Exception eLT)
            {
                Log.EscribeLog("Existe un error en getModalidadAgenda: " + eLT.Message, 3, user);
            }
            return lst;
        }

        #endregion NuevaCitaAgenda

        #region ConfigAgenda

        public List<clsConfScheduler> getListConfigScheduler_Sito(string user, int idsitio)
        {
            List<clsConfScheduler> lst = new List<clsConfScheduler>();
            try
            {
                clsConfScheduler mdl = new clsConfScheduler();
                using (dbRisDA = new RISLiteEntities())
                {
                    if (dbRisDA.tbl_CONFIG_Agenda.Any())
                    {
                        var query = (from x in dbRisDA.tbl_CONFIG_Agenda
                                     where x.intSitioID == idsitio
                                     select new
                                     {
                                         x.intConfiguracionAgendaID,
                                         x.intSitioID,
                                         x.vchConfiguracionAgenda,
                                         x.tmeInicioDia,
                                         x.tmeFinDia,
                                         x.intIntervalo,
                                         x.datFecha,
                                         x.vchUserAdmin
                                     }).ToList();
                        if (query != null)
                        {
                            if (query.Count > 0)
                            {
                                foreach (var item in query)
                                {
                                    mdl.intConfiguracionAgendaID = (int)item.intConfiguracionAgendaID;
                                    mdl.tmeInicioDia = (TimeSpan)item.tmeInicioDia;
                                    mdl.tmeFinDia = (TimeSpan)item.tmeFinDia;
                                    mdl.intIntervalo = (int)item.intIntervalo;
                                    lst.Add(mdl);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception eLT)
            {
                Log.EscribeLog("Existe un error en getListConfigScheduler: " + eLT.Message, 3, user);
            }
            return lst;
        }

        public List<clsConfScheduler> getListConfigScheduler_Sitio(string user, int idsitio)
        {
            List<clsConfScheduler> lst = new List<clsConfScheduler>();
            try
            {
                clsConfScheduler mdl = new clsConfScheduler();
                using (dbRisDA = new RISLiteEntities())
                {
                    if (dbRisDA.tbl_CONFIG_Agenda.Any())
                    {
                        var query = dbRisDA.tbl_CONFIG_Agenda.Where(x => x.intSitioID == idsitio).ToList();
                        if (query != null)
                        {
                            if (query.Count > 0)
                            {
                                foreach (var item in query)
                                {
                                    mdl.intConfiguracionAgendaID = (int)item.intConfiguracionAgendaID;
                                    mdl.tmeInicioDia = (TimeSpan)item.tmeInicioDia;
                                    mdl.tmeFinDia = (TimeSpan)item.tmeFinDia;
                                    mdl.intIntervalo = (int)item.intIntervalo;

                                    lst.Add(mdl);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception eLT)
            {
                Log.EscribeLog("Existe un error en getListConfigScheduler: " + eLT.Message, 3, user);
            }
            return lst;
        }

        public List<clsConfScheduler> getListConfigScheduler(string user)
        {
            List<clsConfScheduler> lst = new List<clsConfScheduler>();
            try
            {
                clsConfScheduler mdl = new clsConfScheduler();
                using (dbRisDA = new RISLiteEntities())
                {
                    if (dbRisDA.tbl_CONFIG_Agenda.Any())
                    {
                        var query = dbRisDA.tbl_CONFIG_Agenda.ToList();
                        if (query != null)
                        {
                            if (query.Count > 0)
                            {
                                foreach (var item in query)
                                {
                                    mdl.intConfiguracionAgendaID = (int)item.intConfiguracionAgendaID;
                                    mdl.tmeInicioDia = (TimeSpan)item.tmeInicioDia;
                                    mdl.tmeFinDia = (TimeSpan)item.tmeFinDia;
                                    mdl.intIntervalo = (int)item.intIntervalo;

                                    lst.Add(mdl);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception eLT)
            {
                Log.EscribeLog("Existe un error en getListConfigScheduler: " + eLT.Message, 3, user);
            }
            return lst;
        }

        public List<clsDiaSemana> getListDiaSemana_sitio(string user, int idsitio)
        {
            List<clsDiaSemana> lst = new List<clsDiaSemana>();
            try
            {

                using (dbRisDA = new RISLiteEntities())
                {
                    if (dbRisDA.tbl_CAT_DiaSemana.Any())
                    {
                        var query = (from x in dbRisDA.tbl_REL_DiaSemana
                                     join semana in dbRisDA.tbl_CAT_DiaSemana
                                     on x.intDiaSemanaInt equals semana.intDiaSemanaInt
                                     where x.intSitioID == idsitio
                                     select new { x.intDiaSemanaInt, x.bitActivo, semana.datFecha, semana.vchUserAdmin, semana.vchDiaSemana }).ToList();


                        if (query != null)
                        {
                            if (query.Count > 0)
                            {
                                foreach (var item in query)
                                {
                                    clsDiaSemana mdl = new clsDiaSemana();
                                    mdl.intSemanaID = (int)item.intDiaSemanaInt;
                                    mdl.vchDiaSemana = item.vchDiaSemana;
                                    mdl.bitActivo = (bool)item.bitActivo;

                                    lst.Add(mdl);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception eLT)
            {
                Log.EscribeLog("Existe un error en getListDiaSemana: " + eLT.Message, 3, user);
            }
            return lst;
        }

        public List<clsDiaSemana> getListDiaSemana(string user)
        {
            List<clsDiaSemana> lst = new List<clsDiaSemana>();
            try
            {

                using (dbRisDA = new RISLiteEntities())
                {
                    if (dbRisDA.tbl_CAT_DiaSemana.Any())
                    {
                        //var query = dbRisDA.tbl_CAT_DiaSemana.Where(x => (x.bitActivo == true)).ToList();
                        var query = dbRisDA.tbl_CAT_DiaSemana.ToList();
                        if (query != null)
                        {
                            if (query.Count > 0)
                            {
                                foreach (var item in query)
                                {
                                    clsDiaSemana mdl = new clsDiaSemana();
                                    mdl.intSemanaID = (int)item.intDiaSemanaInt;
                                    mdl.vchDiaSemana = item.vchDiaSemana;
                                    mdl.bitActivo = (bool)item.bitActivo;

                                    lst.Add(mdl);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception eLT)
            {
                Log.EscribeLog("Existe un error en getListDiaSemana: " + eLT.Message, 3, user);
            }
            return lst;
        }

        public List<clsDiaFeriado> getListDiaFeriado(string user)
        {
            List<clsDiaFeriado> lst = new List<clsDiaFeriado>();
            try
            {

                using (dbRisDA = new RISLiteEntities())
                {
                    if (dbRisDA.tbl_CAT_DiaFeriado.Any())
                    {
                        var query = dbRisDA.tbl_CAT_DiaFeriado.Where(x => (x.bitActivo == true)).ToList();


                        if (query != null)
                        {
                            if (query.Count > 0)
                            {
                                foreach (var item in query)
                                {
                                    clsDiaFeriado mdl = new clsDiaFeriado();
                                    mdl.intDiaFeriadoID = (int)item.intDiaFeriadoID;
                                    mdl.datDia = (DateTime)item.datDia;


                                    lst.Add(mdl);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception eLT)
            {
                Log.EscribeLog("Existe un error en getListDiaFeriado: " + eLT.Message, 3, user);
            }
            return lst;
        }

        public List<clsDiaFeriado> getListDiaFeriado_Sitio(string user, int idsitio)
        {
            List<clsDiaFeriado> lst = new List<clsDiaFeriado>();
            try
            {

                using (dbRisDA = new RISLiteEntities())
                {
                    if (dbRisDA.tbl_CAT_DiaFeriado.Any())
                    {
                        var query = dbRisDA.tbl_CAT_DiaFeriado.Where(x => (x.bitActivo == true) && x.intSitioID == idsitio).ToList();


                        if (query != null)
                        {
                            if (query.Count > 0)
                            {
                                foreach (var item in query)
                                {
                                    clsDiaFeriado mdl = new clsDiaFeriado();
                                    mdl.intDiaFeriadoID = (int)item.intDiaFeriadoID;
                                    mdl.datDia = (DateTime)item.datDia;


                                    lst.Add(mdl);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception eLT)
            {
                Log.EscribeLog("Existe un error en getListDiaFeriado: " + eLT.Message, 3, user);
            }
            return lst;
        }

        public List<clsHoraMuerta> getListHorasMuertas_Sitio(string user, int idsitio)
        {
            List<clsHoraMuerta> lst = new List<clsHoraMuerta>();
            try
            {

                using (dbRisDA = new RISLiteEntities())
                {
                    if (dbRisDA.tbl_CAT_HoraMuerta.Any())
                    {
                        var query = dbRisDA.tbl_CAT_HoraMuerta.Where(x => (x.bitActivo == true) && x.intSitioID == idsitio).ToList();
                        if (query != null)
                        {
                            if (query.Count > 0)
                            {
                                foreach (var item in query)
                                {
                                    clsHoraMuerta mdl = new clsHoraMuerta();
                                    mdl.intHorasMuertasID = (int)item.intHorasMuertasID;
                                    mdl.tmeInicio = item.tmeInicio.ToString();
                                    mdl.tmeFin = item.tmeFin.ToString();
                                    //mdl.bitRepetir = item.bitRepetir;

                                    lst.Add(mdl);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception eLT)
            {
                Log.EscribeLog("Existe un error en getListHorasMuertas: " + eLT.Message, 3, user);
            }
            return lst;
        }

        public List<clsHoraMuerta> getListHorasMuertas(string user)
        {
            List<clsHoraMuerta> lst = new List<clsHoraMuerta>();
            try
            {

                using (dbRisDA = new RISLiteEntities())
                {
                    if (dbRisDA.tbl_CAT_HoraMuerta.Any())
                    {
                        var query = dbRisDA.tbl_CAT_HoraMuerta.Where(x => (x.bitActivo == true)).ToList();
                        if (query != null)
                        {
                            if (query.Count > 0)
                            {
                                foreach (var item in query)
                                {
                                    clsHoraMuerta mdl = new clsHoraMuerta();
                                    mdl.intHorasMuertasID = (int)item.intHorasMuertasID;
                                    mdl.tmeInicio = item.tmeInicio.ToString();
                                    mdl.tmeFin = item.tmeFin.ToString();
                                    //mdl.bitRepetir = item.bitRepetir;

                                    lst.Add(mdl);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception eLT)
            {
                Log.EscribeLog("Existe un error en getListHorasMuertas: " + eLT.Message, 3, user);
            }
            return lst;
        }

        public bool UpdateDiaSemana_Sitio(string user, int idsemana, bool estatus, int idsitio)
        {
            bool bandera_Actualizar = false;
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {

                    var dbCstInfo = dbRisDA.tbl_REL_DiaSemana
                        .Where(w => w.intDiaSemanaInt == idsemana && w.intSitioID == idsitio)
                        .SingleOrDefault();

                    if (dbCstInfo != null)
                    {
                        dbCstInfo.bitActivo = estatus;
                        dbCstInfo.vchUserAdmin = user;
                        dbCstInfo.datFecha = DateTime.Now;
                        dbRisDA.SaveChanges();
                        bandera_Actualizar = true;
                    }

                    else
                    {
                        tbl_REL_DiaSemana mdlsemana = new tbl_REL_DiaSemana();

                        mdlsemana.intDiaSemanaInt = idsemana;
                        mdlsemana.intSitioID = idsitio;
                        mdlsemana.bitActivo = estatus;
                        mdlsemana.datFecha = DateTime.Now;
                        mdlsemana.vchUserAdmin = user;

                        dbRisDA.tbl_REL_DiaSemana.Add(mdlsemana);
                        dbRisDA.SaveChanges();
                        bandera_Actualizar = true;
                    }

                }
            }
            catch (Exception eLT)
            {
                Log.EscribeLog("Existe un error en UpdateDiaSemana: " + eLT.Message, 3, user);
            }
            return bandera_Actualizar;
        }

        public bool UpdateDiaSemana(string user, int idsemana, bool estatus)
        {
            bool bandera_Actualizar = false;
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {

                    var dbCstInfo = dbRisDA.tbl_CAT_DiaSemana
                        .Where(w => w.intDiaSemanaInt == idsemana)
                        .SingleOrDefault();

                    if (dbCstInfo != null)
                    {
                        dbCstInfo.bitActivo = estatus;
                        dbCstInfo.vchUserAdmin = user;
                        dbCstInfo.datFecha = DateTime.Now;
                        dbRisDA.SaveChanges();
                        bandera_Actualizar = true;
                    }

                }
            }
            catch (Exception eLT)
            {
                Log.EscribeLog("Existe un error en UpdateDiaSemana: " + eLT.Message, 3, user);
            }
            return bandera_Actualizar;
        }

        public bool UpdateHR_Activo_Sitio(string user, TimeSpan HRInicio, TimeSpan HRFin, int idsitio)
        {
            bool bandera_Actualizar = false;
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    tbl_CONFIG_Agenda mdlCOnf = new tbl_CONFIG_Agenda();

                    mdlCOnf = dbRisDA.tbl_CONFIG_Agenda.First(x => x.intSitioID == idsitio);
                    mdlCOnf.tmeInicioDia = HRInicio;
                    mdlCOnf.tmeFinDia = HRFin;

                    //dbCstInfo.vchUserAdmin = user;
                    //dbCstInfo.datFecha = DateTime.Now;
                    dbRisDA.SaveChanges();
                    bandera_Actualizar = true;
                }
            }
            catch (Exception eLT)
            {
                Log.EscribeLog("Existe un error en UpdateHorarioActivo: " + eLT.Message, 3, user);
            }
            return bandera_Actualizar;
        }

        public bool UpdateHR_Activo(string user, TimeSpan HRInicio, TimeSpan HRFin)
        {
            bool bandera_Actualizar = false;
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    tbl_CONFIG_Agenda mdlCOnf = new tbl_CONFIG_Agenda();

                    mdlCOnf = dbRisDA.tbl_CONFIG_Agenda.First();
                    mdlCOnf.tmeInicioDia = HRInicio;
                    mdlCOnf.tmeFinDia = HRFin;

                    //dbCstInfo.vchUserAdmin = user;
                    //dbCstInfo.datFecha = DateTime.Now;
                    dbRisDA.SaveChanges();
                    bandera_Actualizar = true;
                }
            }
            catch (Exception eLT)
            {
                Log.EscribeLog("Existe un error en UpdateHorarioActivo: " + eLT.Message, 3, user);
            }
            return bandera_Actualizar;
        }

        public bool Update_Intervalo(string user, int intervalo)
        {
            bool bandera_Actualizar = false;
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    tbl_CONFIG_Agenda mdlCOnf = new tbl_CONFIG_Agenda();

                    mdlCOnf = dbRisDA.tbl_CONFIG_Agenda.First();
                    mdlCOnf.intIntervalo = intervalo;


                    //dbCstInfo.vchUserAdmin = user;
                    //dbCstInfo.datFecha = DateTime.Now;
                    dbRisDA.SaveChanges();
                    bandera_Actualizar = true;
                }
            }
            catch (Exception eLT)
            {
                Log.EscribeLog("Existe un error en Update_Intervalo: " + eLT.Message, 3, user);
            }
            return bandera_Actualizar;
        }

        public bool Set_DiaFeriado_Sitio(string user, DateTime dia, int idsitio)
        {
            bool bandera_insert = false;
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    tbl_CAT_DiaFeriado mdlDF = new tbl_CAT_DiaFeriado();
                    mdlDF.datDia = dia;
                    mdlDF.bitActivo = true;
                    mdlDF.datFecha = DateTime.Today;
                    mdlDF.vchUserAdmin = user;
                    mdlDF.intSitioID = idsitio;

                    dbRisDA.tbl_CAT_DiaFeriado.Add(mdlDF);
                    dbRisDA.SaveChanges();

                    bandera_insert = true;
                }
            }
            catch (Exception eLT)
            {
                Log.EscribeLog("Existe un error en InsertDiaFeriado: " + eLT.Message, 3, user);
            }
            return bandera_insert;
        }

        public bool Set_DiaFeriado(string user, DateTime dia)
        {
            bool bandera_insert = false;
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    tbl_CAT_DiaFeriado mdlDF = new tbl_CAT_DiaFeriado();
                    mdlDF.datDia = dia;
                    mdlDF.bitActivo = true;
                    mdlDF.datFecha = DateTime.Today;
                    mdlDF.vchUserAdmin = user;

                    dbRisDA.tbl_CAT_DiaFeriado.Add(mdlDF);
                    dbRisDA.SaveChanges();

                    bandera_insert = true;
                }
            }
            catch (Exception eLT)
            {
                Log.EscribeLog("Existe un error en InsertDiaFeriado: " + eLT.Message, 3, user);
            }
            return bandera_insert;
        }

        public bool Update_DiaFeriado(string user, DateTime dia)
        {
            bool bandera_Actualizar = false;
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    tbl_CAT_DiaFeriado mdlDF = new tbl_CAT_DiaFeriado();

                    mdlDF = dbRisDA.tbl_CAT_DiaFeriado.First(x => x.datDia == dia);
                    mdlDF.datFecha = DateTime.Today;
                    mdlDF.vchUserAdmin = user;


                    //mdlDF.bitActivo = mdlUser.bitActivo;             
                    dbRisDA.SaveChanges();

                    bandera_Actualizar = true;
                }
            }
            catch (Exception eLT)
            {
                Log.EscribeLog("Existe un error en UpdateDiaFeriado: " + eLT.Message, 3, user);
            }
            return bandera_Actualizar;
        }

        public bool Actualizar_Estatus_DiaFeriado(string user, DateTime dia, bool estatus)
        {
            bool bandera_Actualizar = false;
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    tbl_CAT_DiaFeriado mdlDF = new tbl_CAT_DiaFeriado();

                    mdlDF = dbRisDA.tbl_CAT_DiaFeriado.First(x => x.datDia == dia);
                    mdlDF.bitActivo = estatus;
                    mdlDF.datFecha = DateTime.Today;
                    mdlDF.vchUserAdmin = user;

                    dbRisDA.SaveChanges();
                    bandera_Actualizar = true;
                }
            }
            catch (Exception eLT)
            {
                Log.EscribeLog("Existe un error en EliminarDiaFeriado: " + eLT.Message, 3, user);
            }
            return bandera_Actualizar;
        }

        public bool Eliminar_DiaFeriado(string user, DateTime dia)
        {
            bool bandera_Actualizar = false;
            try
            {

                using (dbRisDA = new RISLiteEntities())
                {

                    var x = dbRisDA.tbl_CAT_DiaFeriado
                        .Where(w => w.datDia == dia)
                        .FirstOrDefault();

                    dbRisDA.tbl_CAT_DiaFeriado.Remove(x);
                    dbRisDA.SaveChanges();
                }

            }
            catch (Exception eLT)
            {
                Log.EscribeLog("Existe un error en EliminarDiaFeriado: " + eLT.Message, 3, user);
            }
            return bandera_Actualizar;
        }

        public bool Eliminar_HoraMuerta(string user, string HM1, string HM2)
        {
            bool bandera_Actualizar = false;

            //string pp =  HM1.Hours.ToString();
            //DateTime cc =  Convert.ToDateTime(HM1.SelectedTime.ToString()).ToShortTimeString();
            TimeSpan vv = TimeSpan.Parse(HM1);
            TimeSpan v2 = TimeSpan.Parse(HM2);
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    var x = dbRisDA.tbl_CAT_HoraMuerta
                        .Where(w => w.tmeInicio == vv && w.tmeFin == v2)
                        .FirstOrDefault();

                    dbRisDA.tbl_CAT_HoraMuerta.Remove(x);
                    dbRisDA.SaveChanges();
                }
            }
            catch (Exception eLT)
            {
                Log.EscribeLog("Existe un error en EliminarDiaFeriado: " + eLT.Message, 3, user);
            }
            return bandera_Actualizar;
        }

        public bool Set_HoraMuerta_sitio(string user, string HM1, string HM2, int idsitio)
        {
            bool bandera_insert = false;
            TimeSpan vv = TimeSpan.Parse(HM1);
            TimeSpan v2 = TimeSpan.Parse(HM2);


            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    tbl_CAT_HoraMuerta mdlDF = new tbl_CAT_HoraMuerta();
                    mdlDF.tmeInicio = vv;
                    mdlDF.tmeFin = v2;
                    mdlDF.datFecha = DateTime.Today;
                    mdlDF.vchUserAdmin = user;
                    mdlDF.bitActivo = true;
                    mdlDF.intSitioID = idsitio;

                    dbRisDA.tbl_CAT_HoraMuerta.Add(mdlDF);
                    dbRisDA.SaveChanges();

                    bandera_insert = true;
                }
            }
            catch (Exception eLT)
            {
                Log.EscribeLog("Existe un error en Set_HoraMuerta: " + eLT.Message, 3, user);
            }
            return bandera_insert;
        }

        public bool Set_HoraMuerta(string user, string HM1, string HM2)
        {
            bool bandera_insert = false;
            TimeSpan vv = TimeSpan.Parse(HM1);
            TimeSpan v2 = TimeSpan.Parse(HM2);


            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    tbl_CAT_HoraMuerta mdlDF = new tbl_CAT_HoraMuerta();
                    mdlDF.tmeInicio = vv;
                    mdlDF.tmeFin = v2;
                    mdlDF.datFecha = DateTime.Today;
                    mdlDF.vchUserAdmin = user;
                    mdlDF.bitActivo = true;

                    dbRisDA.tbl_CAT_HoraMuerta.Add(mdlDF);
                    dbRisDA.SaveChanges();

                    bandera_insert = true;
                }
            }
            catch (Exception eLT)
            {
                Log.EscribeLog("Existe un error en Set_HoraMuerta: " + eLT.Message, 3, user);
            }
            return bandera_insert;
        }

        #endregion ConfigAgenda

        #region Indicacion

        public List<tbl_DET_IndicacionPrestacion> getListIndicacion(int intPrestacionID, string user)
        {
            List<tbl_DET_IndicacionPrestacion> list = new List<tbl_DET_IndicacionPrestacion>();
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    var query = dbRisDA.tbl_DET_IndicacionPrestacion.Where(x => x.intPrestacionID == intPrestacionID).ToList();
                    if (query != null)
                    {
                        if (query.Count > 0)
                        {
                            list.AddRange(query);
                        }
                    }
                }
            }
            catch (Exception egLC)
            {
                Log.EscribeLog("Existe un error en getListIndicacion: " + egLC.Message, 3, user);
            }
            return list;
        }

        public bool setIndicacion(tbl_DET_IndicacionPrestacion indicacion, string user, ref string mensaje)
        {
            bool valido = false;
            try
            {
                tbl_DET_IndicacionPrestacion mdlInd = new tbl_DET_IndicacionPrestacion();
                using (dbRisDA = new RISLiteEntities())
                {
                    //Primero en cat
                    if (!dbRisDA.tbl_DET_IndicacionPrestacion.Any(x => x.vchIndicacion.ToUpper() == indicacion.vchIndicacion.ToUpper() && x.intPrestacionID == indicacion.intPrestacionID))
                    {
                        mdlInd.bitActivo = true;
                        mdlInd.datFecha = DateTime.Now;
                        mdlInd.intPrestacionID = indicacion.intPrestacionID;
                        mdlInd.vchIndicacion = indicacion.vchIndicacion;
                        mdlInd.vchUserAdmin = user;
                        dbRisDA.tbl_DET_IndicacionPrestacion.Add(mdlInd);
                        dbRisDA.SaveChanges();
                        valido = true;
                    }
                    else
                    {
                        mensaje += " Ya existe la indicación.";
                        valido = false;
                    }
                }
            }
            catch (Exception eSU)
            {
                valido = false;
                mensaje += eSU.Message;
                Log.EscribeLog("Existe un error en setIndicacion: " + eSU.Message, 3, user);
            }
            return valido;
        }

        public bool setActualizaIndicacion(tbl_DET_IndicacionPrestacion indicacion, string user, ref string mensaje)
        {
            bool valido = false;
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    if (dbRisDA.tbl_DET_IndicacionPrestacion.Any(x => x.intIndicacionID == indicacion.intIndicacionID))
                    {
                        if (!dbRisDA.tbl_DET_IndicacionPrestacion.Any(x => x.vchIndicacion == indicacion.vchIndicacion))
                        {
                            tbl_DET_IndicacionPrestacion mdlInd = new tbl_DET_IndicacionPrestacion();
                            mdlInd = dbRisDA.tbl_DET_IndicacionPrestacion.First(x => x.intIndicacionID == indicacion.intIndicacionID);
                            mdlInd.bitActivo = indicacion.bitActivo;
                            mdlInd.datFecha = DateTime.Now;
                            mdlInd.intPrestacionID = (int)indicacion.intPrestacionID;
                            mdlInd.vchUserAdmin = user;
                            mdlInd.vchIndicacion = indicacion.vchIndicacion;
                            dbRisDA.SaveChanges();
                            valido = true;
                        }
                        else
                        {
                            mensaje = "La indicación ya existe.";
                            valido = false;
                        }
                    }
                    else
                    {
                        mensaje = "No existe la indicación.";
                        valido = false;
                    }
                }
            }
            catch (Exception eSU)
            {
                valido = false;
                mensaje = eSU.Message;
                Log.EscribeLog("Existe un error en setActualizaIndicacion: " + eSU.Message, 3, user);
            }
            return valido;
        }

        public bool setEstatusIndicacion(int intIndicacionID, string user, ref string mensaje)
        {
            bool valido = false;
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    if (dbRisDA.tbl_DET_IndicacionPrestacion.Any(x => x.intIndicacionID == intIndicacionID))
                    {
                        tbl_DET_IndicacionPrestacion mdlUser = new tbl_DET_IndicacionPrestacion();
                        mdlUser = dbRisDA.tbl_DET_IndicacionPrestacion.First(x => x.intIndicacionID == intIndicacionID);
                        mdlUser.bitActivo = !mdlUser.bitActivo;
                        mdlUser.datFecha = DateTime.Today;
                        mdlUser.vchUserAdmin = user;
                        dbRisDA.SaveChanges();
                        valido = true;
                    }
                    else
                    {
                        mensaje = "La indicación no existe.";
                        valido = false;
                    }
                }
            }
            catch (Exception eSU)
            {
                valido = false;
                mensaje = eSU.Message;
                Log.EscribeLog("Existe un error en setEstatusIndicacion: " + eSU.Message, 3, user);
            }
            return valido;
        }

        #endregion Indicacion

        #region Restriccion

        public List<tbl_DET_Restriccion> getListRestriccion(int intPrestacionID, string user)
        {
            List<tbl_DET_Restriccion> list = new List<tbl_DET_Restriccion>();
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    var query = dbRisDA.tbl_DET_Restriccion.Where(x => x.intPrestacionID == intPrestacionID).ToList();
                    if (query != null)
                    {
                        if (query.Count > 0)
                        {
                            list.AddRange(query);
                        }
                    }
                }
            }
            catch (Exception egLC)
            {
                Log.EscribeLog("Existe un error en getListRestriccion: " + egLC.Message, 3, user);
            }
            return list;
        }

        public bool setRestriccion(tbl_DET_Restriccion indicacion, string user, ref string mensaje)
        {
            bool valido = false;
            try
            {
                tbl_DET_Restriccion mdlRes = new tbl_DET_Restriccion();
                using (dbRisDA = new RISLiteEntities())
                {
                    //Primero en cat
                    if (!dbRisDA.tbl_DET_Restriccion.Any(x => x.vchNombreReestriccion.ToUpper() == indicacion.vchNombreReestriccion.ToUpper() && x.intPrestacionID == indicacion.intPrestacionID))
                    {
                        mdlRes.bitActivo = true;
                        mdlRes.datFecha = DateTime.Now;
                        mdlRes.intPrestacionID = indicacion.intPrestacionID;
                        mdlRes.vchNombreReestriccion = indicacion.vchNombreReestriccion;
                        mdlRes.vchUserAdmin = user;
                        dbRisDA.tbl_DET_Restriccion.Add(mdlRes);
                        dbRisDA.SaveChanges();
                        valido = true;
                    }
                    else
                    {
                        mensaje += " Ya existe la restricción.";
                        valido = false;
                    }
                }
            }
            catch (Exception eSU)
            {
                valido = false;
                mensaje += eSU.Message;
                Log.EscribeLog("Existe un error en setRestriccion: " + eSU.Message, 3, user);
            }
            return valido;
        }

        public bool setActualizaRestriccion(tbl_DET_Restriccion restriccion, string user, ref string mensaje)
        {
            bool valido = false;
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    if (dbRisDA.tbl_DET_Restriccion.Any(x => x.intReestriccionID == restriccion.intReestriccionID))
                    {
                        if (!dbRisDA.tbl_DET_Restriccion.Any(x => x.vchNombreReestriccion == restriccion.vchNombreReestriccion))
                        {
                            tbl_DET_Restriccion mdlRes = new tbl_DET_Restriccion();
                            mdlRes = dbRisDA.tbl_DET_Restriccion.First(x => x.intReestriccionID == restriccion.intReestriccionID);
                            mdlRes.bitActivo = restriccion.bitActivo;
                            mdlRes.datFecha = DateTime.Now;
                            mdlRes.intPrestacionID = (int)restriccion.intPrestacionID;
                            mdlRes.vchUserAdmin = user;
                            mdlRes.vchNombreReestriccion = restriccion.vchNombreReestriccion;
                            dbRisDA.SaveChanges();
                            valido = true;
                        }
                        else
                        {
                            mensaje = "La restricción ya existe.";
                            valido = false;
                        }
                    }
                    else
                    {
                        mensaje = "No existe la restricción.";
                        valido = false;
                    }
                }
            }
            catch (Exception eSU)
            {
                valido = false;
                mensaje = eSU.Message;
                Log.EscribeLog("Existe un error en setActualizaRestriccion: " + eSU.Message, 3, user);
            }
            return valido;
        }

        public bool setEstatusRestriccion(int intReestriccionID, string user, ref string mensaje)
        {
            bool valido = false;
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    if (dbRisDA.tbl_DET_Restriccion.Any(x => x.intReestriccionID == intReestriccionID))
                    {
                        tbl_DET_Restriccion mdlUser = new tbl_DET_Restriccion();
                        mdlUser = dbRisDA.tbl_DET_Restriccion.First(x => x.intReestriccionID == intReestriccionID);
                        mdlUser.bitActivo = !mdlUser.bitActivo;
                        mdlUser.datFecha = DateTime.Today;
                        mdlUser.vchUserAdmin = user;
                        dbRisDA.SaveChanges();
                        valido = true;
                    }
                    else
                    {
                        mensaje = "La restricción no existe.";
                        valido = false;
                    }
                }
            }
            catch (Exception eSU)
            {
                valido = false;
                mensaje = eSU.Message;
                Log.EscribeLog("Existe un error en setEstatusRestriccion: " + eSU.Message, 3, user);
            }
            return valido;
        }

        #endregion Restriccion

        #region Cuestionario

        public List<tbl_DET_Cuestionario> getListCuestionario(int intPrestacionID, string user)
        {
            List<tbl_DET_Cuestionario> list = new List<tbl_DET_Cuestionario>();
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    var query = dbRisDA.tbl_DET_Cuestionario.Where(x => x.intPrestacionID == intPrestacionID).ToList();
                    if (query != null)
                    {
                        if (query.Count > 0)
                        {
                            list.AddRange(query);
                        }
                    }
                }
            }
            catch (Exception egLC)
            {
                Log.EscribeLog("Existe un error en getListCuestionario: " + egLC.Message, 3, user);
            }
            return list;
        }

        public bool setCuestionario(tbl_DET_Cuestionario cuestionario, string user, ref string mensaje)
        {
            bool valido = false;
            try
            {
                tbl_DET_Cuestionario mdlCues = new tbl_DET_Cuestionario();
                using (dbRisDA = new RISLiteEntities())
                {
                    //Primero en cat
                    if (!dbRisDA.tbl_DET_Cuestionario.Any(x => x.vchCuestionario.ToUpper() == cuestionario.vchCuestionario.ToUpper()))
                    {
                        mdlCues.bitActivo = true;
                        mdlCues.datFecha = DateTime.Now;
                        mdlCues.intPrestacionID = cuestionario.intPrestacionID;
                        mdlCues.vchCuestionario = cuestionario.vchCuestionario;
                        mdlCues.vchUserAdmin = user;
                        dbRisDA.tbl_DET_Cuestionario.Add(mdlCues);
                        dbRisDA.SaveChanges();
                        valido = true;
                    }
                    else
                    {
                        mensaje += " Ya existe el Cuestionario.";
                        valido = false;
                    }
                }
            }
            catch (Exception eSU)
            {
                valido = false;
                mensaje += eSU.Message;
                Log.EscribeLog("Existe un error en setCuestionario: " + eSU.Message, 3, user);
            }
            return valido;
        }

        public bool setActualizaCuestionario(tbl_DET_Cuestionario cuestionario, string user, ref string mensaje)
        {
            bool valido = false;
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    if (dbRisDA.tbl_DET_Cuestionario.Any(x => x.intDETCuestionarioID == cuestionario.intDETCuestionarioID))
                    {
                        if (!dbRisDA.tbl_DET_Cuestionario.Any(x => x.vchCuestionario == cuestionario.vchCuestionario))
                        {
                            tbl_DET_Cuestionario mdlRes = new tbl_DET_Cuestionario();
                            mdlRes = dbRisDA.tbl_DET_Cuestionario.First(x => x.intDETCuestionarioID == cuestionario.intDETCuestionarioID);
                            mdlRes.bitActivo = cuestionario.bitActivo;
                            mdlRes.datFecha = DateTime.Now;
                            mdlRes.intPrestacionID = (int)cuestionario.intPrestacionID;
                            mdlRes.vchUserAdmin = user;
                            mdlRes.vchCuestionario = cuestionario.vchCuestionario;
                            dbRisDA.SaveChanges();
                            valido = true;
                        }
                        else
                        {
                            mensaje = "El Cuestionario ya existe.";
                            valido = false;
                        }
                    }
                    else
                    {
                        mensaje = "No existe el Cuestionario.";
                        valido = false;
                    }
                }
            }
            catch (Exception eSU)
            {
                valido = false;
                mensaje = eSU.Message;
                Log.EscribeLog("Existe un error en setActualizaCuestionario: " + eSU.Message, 3, user);
            }
            return valido;
        }

        public bool setEstatusCuestionario(int intDETCuestionarioID, string user, ref string mensaje)
        {
            bool valido = false;
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    if (dbRisDA.tbl_DET_Cuestionario.Any(x => x.intDETCuestionarioID == intDETCuestionarioID))
                    {
                        tbl_DET_Cuestionario mdlUser = new tbl_DET_Cuestionario();
                        mdlUser = dbRisDA.tbl_DET_Cuestionario.First(x => x.intDETCuestionarioID == intDETCuestionarioID);
                        mdlUser.bitActivo = !mdlUser.bitActivo;
                        mdlUser.datFecha = DateTime.Today;
                        mdlUser.vchUserAdmin = user;
                        dbRisDA.SaveChanges();
                        valido = true;
                    }
                    else
                    {
                        mensaje = "El Cuestionario no existe.";
                        valido = false;
                    }
                }
            }
            catch (Exception eSU)
            {
                valido = false;
                mensaje = eSU.Message;
                Log.EscribeLog("Existe un error en setEstatusCuestionario: " + eSU.Message, 3, user);
            }
            return valido;
        }

        #endregion Cuestionario

        #region Estudios
        public List<clsEstudioCita> getEstudiosPaciente(int intPacienteID, string user)
        {
            List<clsEstudioCita> lst = new List<clsEstudioCita>();
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    var query = dbRisDA.stp_getEstudiosPaciente(intPacienteID).ToList();
                    if (query != null)
                    {
                        if (query.Count > 0)
                        {
                            foreach (var item in query)
                            {
                                clsEstudioCita mdl = new clsEstudioCita();
                                mdl.datFechaCita = (DateTime)item.datFechaCita;
                                mdl.intCitaID = (int)item.intCitaID;
                                mdl.intEstatusCita = item.intEstatusCita;
                                mdl.intPacienteID = (int)item.intPacienteID;
                                mdl.intPrestacionID = (int)item.intPrestacionID;
                                mdl.vchEstatusCita = item.vchEstatusCita;
                                mdl.vchNombrePaciente = item.vchNombre + " " + item.vchApellidos;
                                mdl.vchPrestacion = item.vchPrestacion;
                                lst.Add(mdl);
                            }
                        }
                    }
                }
            }
            catch (Exception egeP)
            {
                Log.EscribeLog("Existe un error en getEstudiosPaciente: " + egeP.Message, 3, user);
            }
            return lst;
        }
        #endregion Estudios

        #region Adicionales
        public List<clsAdicionales> getAdicionales(int intTipoAdicional, int intSitioID, string user)
        {
            List<clsAdicionales> lstreturn = new List<clsAdicionales>();
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    if (dbRisDA.tbl_MST_Adicionales.Any(x => x.intTipoAdicional == intTipoAdicional && x.intSitioID == intSitioID))
                    {
                        var query = (from adi in dbRisDA.tbl_MST_Adicionales
                                     join catBoton in dbRisDA.tbl_CAT_TipoBoton on adi.intTipoBotonID equals catBoton.intTipoBotonID
                                     join catAdi in dbRisDA.tbl_CAT_TipoAdicional on adi.intTipoAdicional equals catAdi.intTipoAdicional
                                     where adi.intTipoAdicional == intTipoAdicional && adi.intSitioID == intSitioID
                                     select new
                                     {
                                         bitActivo = adi.bitActivo,
                                         datFecha = adi.datFecha,
                                         bitObservaciones = adi.bitObservaciones,
                                         intAdicionalesID = adi.intAdicionalesID,
                                         intTipoAdicionalID = adi.intTipoAdicional,
                                         intTipoBotonID = adi.intTipoBotonID,
                                         vchNombreAdicional = adi.vchNombre,
                                         vchTipoAdicional = catAdi.vchNombre,
                                         vchTipoBoton = catBoton.vchTipoBoton,
                                         vchURLImagen = adi.vchURLImagen,
                                         vchUserAdmin = adi.vchUserAdmin
                                     }).ToList();
                        if (query != null)
                        {
                            if (query.Count > 0)
                            {
                                foreach (var item in query)
                                {
                                    clsAdicionales mdl = new clsAdicionales();
                                    mdl.bitActivo = (bool)item.bitActivo;
                                    mdl.datFecha = (DateTime)item.datFecha;
                                    mdl.bitObservaciones = (bool)item.bitObservaciones;
                                    mdl.intAdicionalesID = item.intAdicionalesID;
                                    mdl.intTipoAdicionalID = (int)item.intTipoAdicionalID;
                                    mdl.intTipoBotonID = (int)item.intTipoBotonID;
                                    mdl.vchNombreAdicional = item.vchNombreAdicional;
                                    mdl.vchUserAdmin = item.vchUserAdmin;
                                    mdl.vchTipoAdicional = item.vchTipoAdicional;
                                    mdl.vchTipoBoton = item.vchTipoBoton;
                                    mdl.vchURLImagen = item.vchURLImagen;
                                    lstreturn.Add(mdl);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception egV)
            {
                Log.EscribeLog("Existe un error en getAdicionales: " + egV.Message, 3, user);
            }
            return lstreturn;
        }

        public List<clsAdicionales> getAdicionalesPac(int intSitioID, int MASCULINO, int FEMENINO, int MAYOR, int MENOR, string user)
        {
            List<clsAdicionales> lstreturn = new List<clsAdicionales>();
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    var query = dbRisDA.stp_getAdicionalesPac(intSitioID, MASCULINO, FEMENINO, MAYOR, MENOR).ToList();
                    if (query != null)
                    {
                        if (query.Count > 0)
                        {
                            foreach (var item in query)
                            {
                                clsAdicionales mdl = new clsAdicionales();
                                mdl.bitActivo = (bool)item.bitActivo;
                                mdl.datFecha = (DateTime)item.datFecha;
                                mdl.bitObservaciones = (bool)item.bitObservaciones;
                                mdl.intAdicionalesID = item.intAdicionalesID;
                                mdl.intTipoAdicionalID = (int)item.intTipoAdicional;
                                mdl.intTipoBotonID = (int)item.intTipoBotonID;
                                mdl.vchNombreAdicional = item.vchNombreAdicional;
                                mdl.vchUserAdmin = item.vchUserAdmin;
                                mdl.vchTipoAdicional = item.vchTipoAdicional;
                                mdl.vchTipoBoton = item.vchTipoBoton;
                                mdl.vchURLImagen = item.vchURLImagen;
                                lstreturn.Add(mdl);
                            }
                        }
                    }
                }
            }
            catch (Exception egV)
            {
                Log.EscribeLog("Existe un error en getAdicionalesPac: " + egV.Message, 3, user);
            }
            return lstreturn;
        }

        public bool setAdicionales(clsAdicionales adicionales, string user, ref string mensaje)
        {
            bool valido = false;
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    tbl_MST_Adicionales mdl = new tbl_MST_Adicionales();
                    if (!dbRisDA.tbl_MST_Adicionales.Any(x => x.vchNombre.ToUpper() == adicionales.vchNombreAdicional && x.intSitioID == adicionales.intSitioID))
                    {
                        mdl.bitActivo = adicionales.bitActivo;
                        mdl.bitObservaciones = adicionales.bitObservaciones;
                        mdl.datFecha = adicionales.datFecha;
                        mdl.intTipoAdicional = adicionales.intTipoAdicionalID;
                        mdl.intTipoBotonID = adicionales.intTipoBotonID;
                        mdl.intSitioID = adicionales.intSitioID;
                        mdl.vchNombre = adicionales.vchNombreAdicional;
                        mdl.vchURLImagen = adicionales.vchURLImagen;
                        mdl.vchUserAdmin = user;
                        dbRisDA.tbl_MST_Adicionales.Add(mdl);
                        dbRisDA.SaveChanges();
                        valido = true;
                        if(mdl.intAdicionalesID > 0)
                        {
                            if(adicionales.intHombre == 1)
                            {
                                using(dbRisDA = new RISLiteEntities())
                                {
                                    tbl_REL_AdicionalEspecificaciones rel = new tbl_REL_AdicionalEspecificaciones();
                                    rel.intAdicionalesID = mdl.intAdicionalesID;
                                    rel.bitActivo = true;
                                    rel.datFecha = DateTime.Now;
                                    rel.intAdiEspecificoID = 1;
                                    rel.vchUserAdmin = user;
                                    dbRisDA.tbl_REL_AdicionalEspecificaciones.Add(rel);
                                    dbRisDA.SaveChanges();
                                }
                            }
                            if (adicionales.intHombre == 2)
                            {
                                using (dbRisDA = new RISLiteEntities())
                                {
                                    tbl_REL_AdicionalEspecificaciones rel = new tbl_REL_AdicionalEspecificaciones();
                                    rel.intAdicionalesID = mdl.intAdicionalesID;
                                    rel.bitActivo = true;
                                    rel.datFecha = DateTime.Now;
                                    rel.intAdiEspecificoID = 2;
                                    rel.vchUserAdmin = user;
                                    dbRisDA.tbl_REL_AdicionalEspecificaciones.Add(rel);
                                    dbRisDA.SaveChanges();
                                }
                            }
                            if (adicionales.intHombre == 3)
                            {
                                using (dbRisDA = new RISLiteEntities())
                                {
                                    tbl_REL_AdicionalEspecificaciones rel = new tbl_REL_AdicionalEspecificaciones();
                                    rel.intAdicionalesID = mdl.intAdicionalesID;
                                    rel.bitActivo = true;
                                    rel.datFecha = DateTime.Now;
                                    rel.intAdiEspecificoID = 3;
                                    rel.vchUserAdmin = user;
                                    dbRisDA.tbl_REL_AdicionalEspecificaciones.Add(rel);
                                    dbRisDA.SaveChanges();
                                }
                            }
                            if (adicionales.intHombre == 4)
                            {
                                using (dbRisDA = new RISLiteEntities())
                                {
                                    tbl_REL_AdicionalEspecificaciones rel = new tbl_REL_AdicionalEspecificaciones();
                                    rel.intAdicionalesID = mdl.intAdicionalesID;
                                    rel.bitActivo = true;
                                    rel.datFecha = DateTime.Now;
                                    rel.intAdiEspecificoID = 4;
                                    rel.vchUserAdmin = user;
                                    dbRisDA.tbl_REL_AdicionalEspecificaciones.Add(rel);
                                    dbRisDA.SaveChanges();
                                }
                            }
                        }
                    }
                    else
                    {
                        mensaje = "La variable ya existe. Favor de verificar.";
                    }
                }
            }
            catch (Exception esAV)
            {
                valido = false;
                mensaje = esAV.Message;
                Log.EscribeLog("Existe un error en setAdicionales: " + esAV.Message, 3, user);
            }
            return valido;
        }

        public bool setActualizarAdicionales(clsAdicionales adicional, string user, ref string mensaje)
        {
            bool valido = false;
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    if (dbRisDA.tbl_MST_Adicionales.Any(x => x.intAdicionalesID == adicional.intAdicionalesID))
                    {
                        if (!dbRisDA.tbl_MST_Adicionales.Any(x => x.vchNombre == adicional.vchNombreAdicional && x.intSitioID == adicional.intSitioID && x.vchURLImagen == adicional.vchURLImagen && x.intTipoAdicional == adicional.intTipoAdicionalID && x.intTipoBotonID == adicional.intTipoBotonID))
                        {
                            tbl_MST_Adicionales mdl = new tbl_MST_Adicionales();
                            mdl = dbRisDA.tbl_MST_Adicionales.First(x => x.intAdicionalesID == adicional.intAdicionalesID);
                            mdl.bitActivo = adicional.bitActivo;
                            mdl.bitObservaciones = adicional.bitObservaciones;
                            mdl.datFecha = DateTime.Now;
                            mdl.intTipoAdicional = adicional.intTipoAdicionalID;
                            mdl.intTipoBotonID = adicional.intTipoBotonID;
                            mdl.vchNombre = adicional.vchNombreAdicional;
                            mdl.vchURLImagen = adicional.vchURLImagen;
                            mdl.vchUserAdmin = user;
                            dbRisDA.SaveChanges();
                            valido = true;

                            saveRELAdicional(adicional.intAdicionalesID, 1, adicional.intHombre, user);
                            saveRELAdicional(adicional.intAdicionalesID, 2, adicional.intMujer, user);
                            saveRELAdicional(adicional.intAdicionalesID, 3, adicional.intMayor, user);
                            saveRELAdicional(adicional.intAdicionalesID, 4, adicional.intMenor, user);
                        }
                        else
                        {
                            mensaje = "La variable ya existe para el sitio";
                        }
                    }
                    else
                    {
                        mensaje = "No existe la variable para actualizar.";
                    }
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

        private void saveRELAdicional(int intAdicionalesID, int v, int intMenor, string user)
        {
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    tbl_REL_AdicionalEspecificaciones rel = new tbl_REL_AdicionalEspecificaciones();
                    if (dbRisDA.tbl_REL_AdicionalEspecificaciones.Any(x => x.intAdicionalesID == intAdicionalesID && x.intAdiEspecificoID == v))
                    {
                        if (intMenor > 0)
                        {
                            rel = dbRisDA.tbl_REL_AdicionalEspecificaciones.Where(x => x.intAdicionalesID == intAdicionalesID && x.intAdiEspecificoID == v).First();
                            rel.bitActivo = true;
                            rel.datFecha = DateTime.Now;
                            rel.vchUserAdmin = user;
                            dbRisDA.SaveChanges();
                        }
                        else
                        {
                            rel = dbRisDA.tbl_REL_AdicionalEspecificaciones.Where(x => x.intAdicionalesID == intAdicionalesID && x.intAdiEspecificoID == v).First();
                            rel.bitActivo = false;
                            rel.datFecha = DateTime.Now;
                            rel.vchUserAdmin = user;
                            dbRisDA.SaveChanges();
                        }
                    }
                    else
                    {
                        if (intMenor > 0)
                        {
                            rel.bitActivo = true;
                            rel.datFecha = DateTime.Now;
                            rel.intAdicionalesID = intAdicionalesID;
                            rel.intAdiEspecificoID = v;
                            rel.vchUserAdmin = user;
                            dbRisDA.tbl_REL_AdicionalEspecificaciones.Add(rel);
                            dbRisDA.SaveChanges();
                        }
                    }
                }
            }
            catch (Exception esRA)
            {
                Log.EscribeLog("Existe un error saveRELAdicional: " + esRA.Message, 3, user);
            }
        }

        public List<tbl_CAT_TipoBoton> getCATTipoBoton(string user)
        {
            List<tbl_CAT_TipoBoton> lstResponse = new List<tbl_CAT_TipoBoton>();
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    if (dbRisDA.tbl_CAT_TipoBoton.Any())
                    {
                        var query = dbRisDA.tbl_CAT_TipoBoton.ToList();
                        if (query != null)
                        {
                            if (query.Count > 0)
                            {
                                lstResponse.AddRange(query);
                            }
                        }
                    }
                }
            }
            catch (Exception egTC)
            {
                Log.EscribeLog("Existe un error en getCATTipoBoton: " + egTC.Message, 3, user);
            }
            return lstResponse;
        }

        public List<tbl_CAT_TipoAdicional> getCATTipoAdicional(string user)
        {
            List<tbl_CAT_TipoAdicional> lstResponse = new List<tbl_CAT_TipoAdicional>();
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    if (dbRisDA.tbl_CAT_TipoAdicional.Any())
                    {
                        var query = dbRisDA.tbl_CAT_TipoAdicional.ToList();
                        if (query != null)
                        {
                            if (query.Count > 0)
                            {
                                lstResponse.AddRange(query);
                            }
                        }
                    }
                }
            }
            catch (Exception egTC)
            {
                Log.EscribeLog("Existe un error en getCATTipoAdicional: " + egTC.Message, 3, user);
            }
            return lstResponse;
        }

        public bool setEstatusAdicional(int intAdicionalesID, string user, ref string mensaje)
        {
            bool valido = false;
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    if (dbRisDA.tbl_MST_Adicionales.Any(x => x.intAdicionalesID == intAdicionalesID))
                    {
                        tbl_MST_Adicionales mdl = new tbl_MST_Adicionales();
                        mdl = dbRisDA.tbl_MST_Adicionales.First(x => x.intAdicionalesID == intAdicionalesID);
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
            }
            catch (Exception esAV)
            {
                valido = false;
                mensaje = esAV.Message;
                Log.EscribeLog("Existe un error en setEstatusAdicional: " + esAV.Message, 3, user);
            }
            return valido;
        }

        public List<clsAdicionales> getAdicionalesREL(int intAdicionalID, string user)
        {
            List<clsAdicionales> lstResult = new List<clsAdicionales>();
            try
            {
                using(dbRisDA = new RISLiteEntities())
                {
                    if(dbRisDA.tbl_REL_AdicionalEspecificaciones.Any(x=> x.intAdicionalesID == intAdicionalID && (bool)x.bitActivo))
                    {
                        var query = (from adi in dbRisDA.tbl_REL_AdicionalEspecificaciones
                                     join cat in dbRisDA.tbl_CAT_AdicionalEspecifico on adi.intAdiEspecificoID equals cat.intAdiEspecificoID
                                     where (bool)adi.bitActivo && adi.intAdicionalesID == intAdicionalID
                                     select new
                                     {
                                         intAdicionalesID = adi.intAdicionalesID,
                                         intAdiEspecificoID = adi.intAdiEspecificoID,
                                         intRELAdiEspID = adi.intRELAdiEspID,
                                         vchEspecifico = cat.vchEspecifico
                                     }).ToList();
                        if (query != null)
                        {
                            if (query.Count > 0)
                            {
                                foreach(var item in query)
                                {
                                    clsAdicionales adi = new clsAdicionales();
                                    adi.intAdicionalesID = (int)item.intAdicionalesID;
                                    switch (item.intAdiEspecificoID)
                                    {
                                        case 1:
                                            adi.intHombre = 1;
                                            break;
                                        case 2:
                                            adi.intMujer = 2;
                                            break;
                                        case 3:
                                            adi.intMayor = 3;
                                            break;
                                        case 4:
                                            adi.intMenor = 4;
                                            break;
                                    }
                                    adi.intAdiEspecificoID = (int)item.intAdiEspecificoID;
                                    adi.intAdicionalesID = intAdicionalID;
                                    lstResult.Add(adi);
                                }
                            }
                        }
                    }
                }
            }
            catch(Exception egA)
            {
                Log.EscribeLog("Existe un error en getAdicionalesREL: " + egA.Message, 3, user);
            }
            return lstResult;
        }

        public bool setAdicionalesREL(clsAdicionales adicionales, string user, ref string mensaje)
        {
            bool valido = false;
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    if (adicionales.intHombre == 1)
                    {
                        using (dbRisDA = new RISLiteEntities())
                        {
                            tbl_REL_AdicionalEspecificaciones rel = new tbl_REL_AdicionalEspecificaciones();
                            if (!dbRisDA.tbl_REL_AdicionalEspecificaciones.Any(x => x.intAdicionalesID == adicionales.intAdicionalesID && x.intAdiEspecificoID == 1))
                            {
                                rel.intAdicionalesID = adicionales.intAdicionalesID;
                                rel.bitActivo = true;
                                rel.datFecha = DateTime.Now;
                                rel.intAdiEspecificoID = 1;
                                rel.vchUserAdmin = user;
                                dbRisDA.tbl_REL_AdicionalEspecificaciones.Add(rel);
                                dbRisDA.SaveChanges();
                            }
                            else
                            {
                                rel = dbRisDA.tbl_REL_AdicionalEspecificaciones.First(x => x.intAdicionalesID == adicionales.intAdicionalesID && x.intAdiEspecificoID == 2);
                                rel.intAdicionalesID = adicionales.intAdicionalesID;
                                rel.bitActivo = true;
                                rel.datFecha = DateTime.Now;
                                rel.intAdiEspecificoID = 1;
                                rel.vchUserAdmin = user;
                                dbRisDA.SaveChanges();
                            }
                        }
                        valido = true;
                    }
                    if (adicionales.intMujer == 2)
                    {
                        using (dbRisDA = new RISLiteEntities())
                        {
                            tbl_REL_AdicionalEspecificaciones rel = new tbl_REL_AdicionalEspecificaciones();
                            if (!dbRisDA.tbl_REL_AdicionalEspecificaciones.Any(x => x.intAdicionalesID == adicionales.intAdicionalesID && x.intAdiEspecificoID == 2))
                            {
                                rel.intAdicionalesID = adicionales.intAdicionalesID;
                                rel.bitActivo = true;
                                rel.datFecha = DateTime.Now;
                                rel.intAdiEspecificoID = 2;
                                rel.vchUserAdmin = user;
                                dbRisDA.tbl_REL_AdicionalEspecificaciones.Add(rel);
                                dbRisDA.SaveChanges();
                            }
                            else
                            {
                                rel = dbRisDA.tbl_REL_AdicionalEspecificaciones.First(x => x.intAdicionalesID == adicionales.intAdicionalesID && x.intAdiEspecificoID == 2);
                                rel.intAdicionalesID = adicionales.intAdicionalesID;
                                rel.bitActivo = true;
                                rel.datFecha = DateTime.Now;
                                rel.intAdiEspecificoID = 2;
                                rel.vchUserAdmin = user;
                                dbRisDA.SaveChanges();
                            }
                        }
                        valido = true;
                    }
                    if (adicionales.intMayor == 3)
                    {
                        using (dbRisDA = new RISLiteEntities())
                        {
                            tbl_REL_AdicionalEspecificaciones rel = new tbl_REL_AdicionalEspecificaciones();
                            if (!dbRisDA.tbl_REL_AdicionalEspecificaciones.Any(x => x.intAdicionalesID == adicionales.intAdicionalesID && x.intAdiEspecificoID == 3))
                            {
                                rel.intAdicionalesID = adicionales.intAdicionalesID;
                                rel.bitActivo = true;
                                rel.datFecha = DateTime.Now;
                                rel.intAdiEspecificoID = 3;
                                rel.vchUserAdmin = user;
                                dbRisDA.tbl_REL_AdicionalEspecificaciones.Add(rel);
                                dbRisDA.SaveChanges();
                            }
                            else
                            {
                                rel = dbRisDA.tbl_REL_AdicionalEspecificaciones.First(x => x.intAdicionalesID == adicionales.intAdicionalesID && x.intAdiEspecificoID == 3);
                                rel.intAdicionalesID = adicionales.intAdicionalesID;
                                rel.bitActivo = true;
                                rel.datFecha = DateTime.Now;
                                rel.intAdiEspecificoID = 3;
                                rel.vchUserAdmin = user;
                                dbRisDA.SaveChanges();
                            }
                        }
                        valido = true;
                    }
                    if (adicionales.intMenor == 4)
                    {
                        using (dbRisDA = new RISLiteEntities())
                        {
                            tbl_REL_AdicionalEspecificaciones rel = new tbl_REL_AdicionalEspecificaciones();
                            if (!dbRisDA.tbl_REL_AdicionalEspecificaciones.Any(x => x.intAdicionalesID == adicionales.intAdicionalesID && x.intAdiEspecificoID == 4))
                            {
                                rel.intAdicionalesID = adicionales.intAdicionalesID;
                                rel.bitActivo = true;
                                rel.datFecha = DateTime.Now;
                                rel.intAdiEspecificoID = 4;
                                rel.vchUserAdmin = user;
                                dbRisDA.tbl_REL_AdicionalEspecificaciones.Add(rel);
                                dbRisDA.SaveChanges();
                            }
                            else
                            {
                                rel = dbRisDA.tbl_REL_AdicionalEspecificaciones.First(x => x.intAdicionalesID == adicionales.intAdicionalesID && x.intAdiEspecificoID == 4);
                                rel.intAdicionalesID = adicionales.intAdicionalesID;
                                rel.bitActivo = true;
                                rel.datFecha = DateTime.Now;
                                rel.intAdiEspecificoID = 4;
                                rel.vchUserAdmin = user;
                                dbRisDA.SaveChanges();
                            }
                        }
                        valido = true;
                    }
                }
            }
            catch (Exception esAV)
            {
                valido = false;
                mensaje = esAV.Message;
                Log.EscribeLog("Existe un error en setAdicionalesREL: " + esAV.Message, 3, user);
            }
            return valido;
        }
        #endregion Adicionales


        #region Import

        public bool Set_Equipo_Import(tbl_CAT_Equipo equipo, string user, ref string mensaje)
        {
            bool valido = false;
            try
            {
                tbl_CAT_Equipo mdlCat = new tbl_CAT_Equipo();
                using (dbRisDA = new RISLiteEntities())
                {
                    //Primero en cat
                    if (!dbRisDA.tbl_CAT_Equipo.Any(x => x.vchNombreEquipo.ToUpper() == equipo.vchNombreEquipo.ToUpper()))
                    {
                        mdlCat.bitActivo = equipo.bitActivo;
                        mdlCat.intSitioID = equipo.intSitioID;
                        mdlCat.datFecha = DateTime.Now;
                        mdlCat.intModalidadID = equipo.intModalidadID;
                        mdlCat.vchAETitle = equipo.vchAETitle;
                        mdlCat.vchCodigoEquipo = equipo.vchCodigoEquipo;
                        mdlCat.vchIPEquipo = equipo.vchIPEquipo;
                        mdlCat.vchNombreEquipo = equipo.vchNombreEquipo;
                        mdlCat.vchUserAdmin = user;
                        dbRisDA.tbl_CAT_Equipo.Add(mdlCat);
                        dbRisDA.SaveChanges();
                        valido = true;
                    }
                    else
                    {
                        mensaje += " Ya existe el equipo Import.";
                        valido = false;
                    }
                }
            }
            catch (Exception eSU)
            {
                valido = false;
                mensaje += eSU.Message;
                Log.EscribeLog("Existe un error en setEquipo Import: " + eSU.Message, 3, user);
            }
            return valido;
        }

        public bool Set_Prestacion_Import(clsPrestacion prestacion, clsDetCuestionario detcuestionario, clsDetRestriccion detrestriccion, clsDetIndicacionPrestacion detindicacionprestacion,
            string user, ref string mensaje)
        {
            bool valido = false;
            try
            {
                bool validoCat = false;
                tbl_CAT_Prestacion mdlCat = new tbl_CAT_Prestacion();
                using (dbRisDA = new RISLiteEntities())
                {
                    //Primero en cat
                    if (!dbRisDA.tbl_CAT_Prestacion.Any(x => x.vchPrestacion.ToUpper() == prestacion.vchPrestacion.ToUpper()))
                    {
                        validoCat = true;
                        mdlCat.bitActivo = prestacion.bitActivo;
                        mdlCat.datFecha = DateTime.Now;
                        mdlCat.intDuracionMin = prestacion.intDuracionMin;
                        mdlCat.vchPrestacion = prestacion.vchPrestacion;
                        mdlCat.intSitioID = prestacion.intSitioId;
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

                if (validoCat && mdlCat.intPrestacionID > 0)
                {
                    using (dbRisDA = new RISLiteEntities())
                    {
                        tbl_DET_IndicacionPrestacion mdlDetIndPrestaion = new tbl_DET_IndicacionPrestacion();
                        mdlDetIndPrestaion.intPrestacionID = mdlCat.intPrestacionID;
                        mdlDetIndPrestaion.vchIndicacion = detindicacionprestacion.vchIndicacion;
                        mdlDetIndPrestaion.vchComentario = detindicacionprestacion.vchComentario;
                        mdlDetIndPrestaion.bitActivo = true;
                        mdlDetIndPrestaion.datFecha = DateTime.Now;
                        mdlDetIndPrestaion.vchUserAdmin = user;
                        dbRisDA.tbl_DET_IndicacionPrestacion.Add(mdlDetIndPrestaion);
                        dbRisDA.SaveChanges();
                    }
                }

                if (validoCat && mdlCat.intPrestacionID > 0)
                {
                    using (dbRisDA = new RISLiteEntities())
                    {
                        tbl_DET_Restriccion mdlrestriccion = new tbl_DET_Restriccion();

                        mdlrestriccion.intPrestacionID = mdlCat.intPrestacionID;
                        mdlrestriccion.vchNombreReestriccion = detrestriccion.vchNombreReestriccion;
                        mdlrestriccion.vchDetalle = detrestriccion.vchDetalle;
                        mdlrestriccion.bitActivo = true;
                        mdlrestriccion.datFecha = DateTime.Now;
                        mdlrestriccion.vchUserAdmin = user;
                        dbRisDA.tbl_DET_Restriccion.Add(mdlrestriccion);
                        dbRisDA.SaveChanges();
                    }
                }

                if (validoCat && mdlCat.intPrestacionID > 0)
                {
                    using (dbRisDA = new RISLiteEntities())
                    {
                        tbl_DET_Cuestionario mdlcuestionario = new tbl_DET_Cuestionario();

                        mdlcuestionario.intPrestacionID = mdlCat.intPrestacionID;
                        mdlcuestionario.vchCuestionario = detcuestionario.vchCuestionario;
                        mdlcuestionario.bitActivo = true;
                        mdlcuestionario.datFecha = DateTime.Now;
                        mdlcuestionario.vchUserAdmin = user;
                        dbRisDA.tbl_DET_Cuestionario.Add(mdlcuestionario);
                        dbRisDA.SaveChanges();
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

        #endregion

        #region SugerenciasCita
        public List<stp_getCitaDisponible_Result> getSugerenciasCita(clsSugerencia mdlSug, String user)
        {
            List<stp_getCitaDisponible_Result> result = new List<stp_getCitaDisponible_Result>();
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    var query = dbRisDA.stp_getCitaDisponible(mdlSug.datFechaInicio, mdlSug.datFechaFinal, mdlSug.intModalidad, mdlSug.vchDias, mdlSug.vchHoras, mdlSug.intSitioID).ToList();
                    if (query != null)
                    {
                        if (query.Count > 0)
                        {
                            result.AddRange(query);
                        }
                    }
                }
            }
            catch (Exception egS)
            {
                result = null;
                Log.EscribeLog("Existe un error en getSugerenciasCita: " + egS.Message, 3, user);
            }
            return result;
        }
        #endregion SugerenciasCita

        #region InsertCita
        public bool setCitaNueva(clsPaciente paciente, List<clsAdicionales> lstAdicionales, List<clsEstudioNuevaCita> lstEstudios, string user, ref string mensaje, ref tbl_MST_Cita cita)
        {
            bool valido = false;
            bool validCita = false;
            bool validRELPacienteCita = false;
            bool validoDetCita = false;
            bool validEstudioCita = false;
            try
            {
                tbl_MST_Cita _cita = new tbl_MST_Cita();
                //Cita
                try
                {
                    using (dbRisDA = new RISLiteEntities())
                    {
                        _cita.bitActivo = true;
                        _cita.datFecha = DateTime.Now;
                        _cita.datFechaCita = DateTime.Now;
                        _cita.intEstatusCita = 1;//Agendado
                        _cita.vchUserAdmin = user;
                        dbRisDA.tbl_MST_Cita.Add(_cita);
                        dbRisDA.SaveChanges();
                        validCita = true;
                    }
                }
                catch (Exception eMSTCita)
                {
                    validCita = false;
                    mensaje += eMSTCita.Message;
                    Log.EscribeLog("Existe un error al insertar en tbl_MST_Cita: " + eMSTCita.Message, 3, user);
                }

                if (_cita.intCitaID > 0)
                {
                    //Actualizar QR en el master de cita
                    try
                    {
                        using (dbRisDA = new RISLiteEntities())
                        {
                            if(dbRisDA.tbl_MST_Cita.Any(x=>x.intCitaID == _cita.intCitaID))
                            {
                                tbl_MST_Cita mdlCita = (dbRisDA.tbl_MST_Cita.First(x => x.intCitaID == _cita.intCitaID));
                                mdlCita.vbQRImage = crearQR(_cita.intCitaID, user);
                                dbRisDA.SaveChanges();
                            }
                        }
                    }
                    catch (Exception eQR)
                    {
                        Log.EscribeLog("Existe un error al crear el Codigo QR de la cita: " + eQR.Message, 3, user);
                    }

                    cita = _cita;
                    //REL_PacienteCita
                    try
                    {
                        using (dbRisDA = new RISLiteEntities())
                        {
                            if (!dbRisDA.tbl_REL_PacienteCita.Any(x => x.intCitaID == _cita.intCitaID && x.intPacienteID == paciente.intPacienteID))
                            {
                                tbl_REL_PacienteCita relCitaPac = new tbl_REL_PacienteCita();
                                relCitaPac.bitActivo = true;
                                relCitaPac.datFecha = DateTime.Now;
                                relCitaPac.intCitaID = _cita.intCitaID;
                                relCitaPac.intPacienteID = paciente.intPacienteID;
                                relCitaPac.vchUserAdmin = user;
                                dbRisDA.tbl_REL_PacienteCita.Add(relCitaPac);
                                dbRisDA.SaveChanges();
                                validRELPacienteCita = true;
                            }
                            else
                            {
                                validRELPacienteCita = false;
                                mensaje += "Ya existe la relación del paciente y la cita.";
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        validRELPacienteCita = false;
                        valido = false;
                        mensaje += " " + e.Message;
                        Log.EscribeLog("Existe un error en tbl_REL_PacienteCita: " + e.Message, 3, user);
                    }

                    //det_Cita
                    try
                    {
                        if (lstAdicionales.Count > 0)
                        {
                            foreach (clsAdicionales item in lstAdicionales)
                            {
                                tbl_DET_Cita detCita = new tbl_DET_Cita();
                                using (dbRisDA = new RISLiteEntities())
                                {
                                    if (!dbRisDA.tbl_DET_Cita.Any(x => x.intAdicionalesID == item.intAdicionalesID && x.intCitaID == _cita.intCitaID))
                                    {

                                        detCita.bitActivo = true;
                                        detCita.datFecha = DateTime.Now;
                                        detCita.intCitaID = _cita.intCitaID;
                                        detCita.intAdicionalesID = item.intAdicionalesID;
                                        if (item.vchObservaciones != "")
                                            detCita.vchObservaciones = item.vchObservaciones;
                                        detCita.vchUserAdmin = user;
                                        detCita.vchValor = item.vchValor;
                                        dbRisDA.tbl_DET_Cita.Add(detCita);
                                        dbRisDA.SaveChanges();
                                    }
                                    else
                                    {

                                        detCita = dbRisDA.tbl_DET_Cita.First(x => x.intAdicionalesID == item.intAdicionalesID && x.intCitaID == _cita.intCitaID);
                                        detCita.vchValor = item.vchValor;
                                        detCita.datFecha = DateTime.Now;
                                        detCita.vchUserAdmin = user;
                                        if (item.vchObservaciones != "")
                                            detCita.vchObservaciones = item.vchObservaciones;
                                        dbRisDA.SaveChanges();
                                    }
                                    validoDetCita = true;
                                }
                            }
                        }
                        else
                        {
                            validoDetCita = true;
                        }
                    }
                    catch (Exception e)
                    {
                        validoDetCita = false;
                        Log.EscribeLog("Existe un error en tbl_REL_PacienteCita: " + e.Message, 3, user);
                    }

                    //Estudios
                    try
                    {
                        if (lstEstudios.Count > 0)
                        {
                            foreach (clsEstudioNuevaCita item in lstEstudios)
                            {
                                tbl_MST_Estudio estudio = new tbl_MST_Estudio();
                                using (dbRisDA = new RISLiteEntities())
                                {
                                    estudio.bitActivo = true;
                                    estudio.datFecha = DateTime.Now;
                                    estudio.datFechaFin = item.fechaFin;
                                    estudio.datFechaInicio = item.fechaInicio;
                                    estudio.intEstatusEstudio = 1;
                                    estudio.intRELModPres = item.intRelModPres;
                                    estudio.vchDescripcion = paciente.vchNombre + " " + paciente.vchApellidos;
                                    estudio.vchTitulo = item.vchTitulo;
                                    estudio.vchUserAdmin = user;
                                    dbRisDA.tbl_MST_Estudio.Add(estudio);
                                    dbRisDA.SaveChanges();
                                }

                                if (estudio.intEstudioID > 0)
                                {
                                    using (dbRisDA = new RISLiteEntities())
                                    {
                                        if (!dbRisDA.tbl_REL_CitaEstudio.Any(x => x.intCitaID == _cita.intCitaID && x.intEstudioID == estudio.intEstudioID))
                                        {
                                            tbl_REL_CitaEstudio relCitaEst = new tbl_REL_CitaEstudio();
                                            relCitaEst.bitActivo = true;
                                            relCitaEst.datFecha = DateTime.Now;
                                            relCitaEst.intCitaID = _cita.intCitaID;
                                            relCitaEst.intEstudioID = estudio.intEstudioID;
                                            relCitaEst.vchUserAdmin = user;
                                            dbRisDA.tbl_REL_CitaEstudio.Add(relCitaEst);
                                            dbRisDA.SaveChanges();
                                        }
                                    }
                                }
                                validEstudioCita = true;
                            }
                        }
                        else
                        {
                            validEstudioCita = true;
                        }
                    }
                    catch (Exception eEstudios)
                    {
                        validEstudioCita = false;
                        Log.EscribeLog("Existe un error al insertar los estudios: " + eEstudios.Message, 3, user);
                    }
                }
                if (validEstudioCita && validoDetCita && validRELPacienteCita && validCita)
                    valido = true;
                else
                    valido = false;
            }
            catch (Exception esCN)
            {
                valido = false;
                Log.EscribeLog("Existe un error en setCitaNueva: " + esCN.Message, 3, user);
            }
            return valido;
        }

        private byte[] crearQR(long intCitaID, string user)
        {
            byte[] QRImage = null;
            try
            {
                string url = ConfigurationManager.AppSettings["URL"] + "\frmArribo.aspx?var=";
                string urlCompleta = url + Security.Encrypt(intCitaID.ToString());
                ///////---------------------------------------------------------
                QRCodeGenerator.ECCLevel eccLevel = (QRCodeGenerator.ECCLevel)1;
                Image img;
                using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
                {
                    using (QRCodeData qrCodeData = qrGenerator.CreateQrCode(url, eccLevel))
                    {
                        using (QRCode qrCode = new QRCode(qrCodeData))
                        {
                            img = qrCode.GetGraphic(20, Color.DarkGreen, Color.White, GetIconBitmap(), (int)0);
                        }
                    }
                }
                //img.Save("c:\\button2.jpeg", System.Drawing.Imaging.ImageFormat.Jpeg);
                QRImage = ImageToByteArray(img);
                ///////---------------------------------------------------------
                //BarcodeSettings.ApplyKey("WUUB05ZBIS3-CVLN7-USWPA-YFU04");//you need a key from e-iceblue, otherwise the watermark 'E-iceblue' will be shown in barcode
                //BarcodeSettings settings = new BarcodeSettings();
                //settings.Type = BarCodeType.QRCode;
                //settings.Unit = GraphicsUnit.Pixel;
                //settings.ShowText = false;
                //settings.ResolutionType = ResolutionType.UseDpi;
                //settings.Data = url;
                //settings.ForeColor = Color.DarkGreen;
                //settings.BackColor = Color.White;
                //settings.X = 4;
                //settings.QRCodeECL = QRCodeECL.L;
                //BarCodeGenerator generator = new BarCodeGenerator(settings);
                //Image QRbarcode = generator.GenerateImage();
                //QRbarcode.Save("c:\\button.jpeg", System.Drawing.Imaging.ImageFormat.Jpeg);
                //QRImage = ImageToByteArray(QRbarcode);
            }
            catch(Exception eCQR)
            {
                Log.EscribeLog("Existe un error en crearQR: " + eCQR.Message, 3, user);
            }
            return QRImage;
        }

        private Bitmap GetIconBitmap()
        {
            Bitmap img = null;
            //if (iconPath.Text.Length > 0)
            //{
            //    try
            //    {
            //        img = new Bitmap(iconPath.Text);
            //    }
            //    catch (Exception)
            //    {
            //    }
            //}
            return img;
        }

        public byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                return ms.ToArray();
            }
        }

        #endregion InsertCita

        #region citaReporte
        public List<stp_getCitaReporte_Result> getCitaReporte(int intCitaID, String user)
        {
            List<stp_getCitaReporte_Result> result = new List<stp_getCitaReporte_Result>();
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    var query = dbRisDA.stp_getCitaReporte(intCitaID).ToList();
                    if (query != null)
                    {
                        if (query.Count > 0)
                        {
                            result.AddRange(query);
                        }
                    }
                }
            }
            catch (Exception egS)
            {
                result = null;
                Log.EscribeLog("Existe un error en getCitaReporte: " + egS.Message, 3, user);
            }
            return result;
        }

        public List<clsRepIndicacion> getIndicaciones(int intPrestacionID, string user)
        {
            List<clsRepIndicacion> result = new List<clsRepIndicacion>();
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    if (dbRisDA.tbl_DET_IndicacionPrestacion.Any(x => x.intPrestacionID == intPrestacionID))
                    {
                        var query = (dbRisDA.tbl_DET_IndicacionPrestacion.Where(x => x.intPrestacionID == intPrestacionID)).ToList();
                        if (query != null)
                        {
                            if (query.Count > 0)
                            {
                                foreach (tbl_DET_IndicacionPrestacion indica in query)
                                {
                                    clsRepIndicacion rep = new clsRepIndicacion();
                                    rep.vchIndicacion = indica.vchIndicacion;
                                    result.Add(rep);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception egS)
            {
                result = null;
                Log.EscribeLog("Existe un error en getIndicaciones: " + egS.Message, 3, user);
            }
            return result;
        }

        public List<clsRepRestriccion> getRestricciones(int intPrestacionID, string user)
        {
            List<clsRepRestriccion> result = new List<clsRepRestriccion>();
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    if (dbRisDA.tbl_DET_Restriccion.Any(x => x.intPrestacionID == intPrestacionID))
                    {
                        var query = (dbRisDA.tbl_DET_Restriccion.Where(x => x.intPrestacionID == intPrestacionID)).ToList();
                        if (query != null)
                        {
                            if (query.Count > 0)
                            {
                                foreach (tbl_DET_Restriccion indica in query)
                                {
                                    clsRepRestriccion rep = new clsRepRestriccion();
                                    rep.vchRestriccion = indica.vchNombreReestriccion;
                                    result.Add(rep);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception egS)
            {
                result = null;
                Log.EscribeLog("Existe un error en getRestricciones: " + egS.Message, 3, user);
            }
            return result;
        }

        #endregion citaReporte


        #region CitasGrid
        public List<stp_getCitas_Result> getCitas(clsEstudioCita busqueda, int intSitioID, String user)
        {
            List<stp_getCitas_Result> result = new List<stp_getCitas_Result>();
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    var query = dbRisDA.stp_getCitas(busqueda.vchNombrePaciente, busqueda.intModalidadID, busqueda.datFechaCita, busqueda.datFechaCitaFin, intSitioID).ToList();
                    if (query != null)
                    {
                        if (query.Count > 0)
                        {
                            result.AddRange(query);
                        }
                    }
                }
            }
            catch (Exception egS)
            {
                result = null;
                Log.EscribeLog("Existe un error en getCitas: " + egS.Message, 3, user);
            }
            return result;
        }

        public bool setEstatusEstudio(int intEstudioID, int intEstatusID, string user, ref string mensaje)
        {
            bool valido = false;
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    if (dbRisDA.tbl_MST_Estudio.Any(x => x.intEstudioID == intEstudioID))
                    {
                        tbl_MST_Estudio mdlEstudio = new tbl_MST_Estudio();
                        mdlEstudio = dbRisDA.tbl_MST_Estudio.First(x => x.intEstudioID == intEstudioID);
                        mdlEstudio.datFecha = DateTime.Today;
                        mdlEstudio.vchUserAdmin = user;
                        mdlEstudio.intEstatusEstudio = intEstatusID;
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

        public void updateEstatusCitaAutomatica(string user)
        {
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    var query = dbRisDA.stp_updateCita();
                }
            }
            catch(Exception esEC)
            {
                Log.EscribeLog("Existe un error en updateEstatusCita: " + esEC.Message, 3, user);
            }
        }
        #endregion CitasGrid

        #region ListaDeTrabajo
        public List<clsListaDeTrabajo> getListadeTrabajo(string user, int idsitio)
        {
            List<clsListaDeTrabajo> lst = new List<clsListaDeTrabajo>();
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    //if (dbRisDA.tbl_MST_Estudio.Any())
                    //{
                        var query = dbRisDA.stp_getListaTrabajo_Sitio(idsitio).ToList();

                        if (query != null)
                        {
                            if (query.Count > 0)
                            {
                                foreach (var item in query)
                                {
                                    clsListaDeTrabajo mdl = new clsListaDeTrabajo();
                                    mdl.intEstudioID = (int)item.intEstudioID;
                                    mdl.vchNombre = item.NombreCom;
                                    mdl.vchtitulo = item.vchTitulo;
                                    mdl.vchModalidad = item.vchModalidad;
                                    mdl.datFechaInicio = (DateTime)item.datFechaInicio;
                                    mdl.vchEstatus = item.vchEstatus;
                                    mdl.vchPrestacion = item.vchPrestacion;
                                    mdl.intEstatusID = (int)item.intEstatusEstudio;
                                    mdl.datFecha = (DateTime)item.datFecha;
                                    mdl.intCitaID = (int)item.intCitaID;
                                    mdl.intModalidadID = (int)item.intModalidadID;
                                    lst.Add(mdl);
                                }
                            }
                        }
                    //}
                }
            }
            catch (Exception eLT)
            {
                Log.EscribeLog("Existe un error en getListadeTrabajo: " + eLT.Message, 3, user);
            }
            return lst;
        }

        public bool UpdateEstatus_Cita(clsUsuario user, int idestudio, int estatus)
        {
            bool bandera_Actualizar = false;
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    tbl_MST_Estudio mdlCOnf = new tbl_MST_Estudio();

                    mdlCOnf = dbRisDA.tbl_MST_Estudio.First(x => x.intEstudioID == idestudio);
                    mdlCOnf.intEstatusEstudio = estatus;
                    mdlCOnf.vchUserAdmin = user.vchUsuario;
                    dbRisDA.SaveChanges();
                    if (estatus == 3)//Tomar{
                    {
                        setEstudioTecnico((int)mdlCOnf.intEstudioID, user.intUsuarioID, user.vchUsuario);
                    }
                    bandera_Actualizar = true;
                }
            }
            catch (Exception eLT)
            {
                Log.EscribeLog("Existe un error en UpdateEstatus_Cita: " + eLT.Message, 3, user.vchUsuario);
            }
            return bandera_Actualizar;
        }

        public bool setEstudioTecnico(int intEstudioID, int intUsuarioID, string user)
        {
            bool bandera_Actualizar = false;
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    if (!dbRisDA.tbl_REL_EstudioTecnico.Any(x => x.intEstudioID == intEstudioID && x.intUsuarioID == intUsuarioID))
                    {
                        tbl_REL_EstudioTecnico mdlCOnf = new tbl_REL_EstudioTecnico();
                        mdlCOnf.intUsuarioID = intUsuarioID;
                        mdlCOnf.intEstudioID = intEstudioID;
                        mdlCOnf.vchUserAdmin = user;
                        mdlCOnf.datFecha = DateTime.Now;
                        mdlCOnf.bitActivo = true;
                        dbRisDA.tbl_REL_EstudioTecnico.Add(mdlCOnf);
                        dbRisDA.SaveChanges();
                        bandera_Actualizar = true;
                    }
                }
            }
            catch (Exception eLT)
            {
                Log.EscribeLog("Existe un error en setEstudioTecnico: " + eLT.Message, 3, user);
            }
            return bandera_Actualizar;
        }

        public List<clsEventoCita> getListEventoCita_SoloSitio(string user, int idsitio)
        {
            List<clsEventoCita> lst = new List<clsEventoCita>();
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    if (dbRisDA.tbl_CAT_Modalidad.Any())
                    {
                        var query = dbRisDA.stp_getBusquedaCita_SoloSitio(idsitio).ToList();
                        if (query != null)
                        {
                            if (query.Count > 0)
                            {
                                foreach (var item in query)
                                {
                                    clsEventoCita mdl = new clsEventoCita();
                                    mdl.TaskID = (int)item.intEstudioID;
                                    mdl.Start = (DateTime)item.datFechaInicio;
                                    mdl.End = (DateTime)item.datFechaFin;
                                    mdl.Title = item.vchTitulo;
                                    mdl.Description = item.vchDescripcion;
                                    mdl.intModalidadID = (int)item.intModalidadID;
                                    lst.Add(mdl);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception eLT)
            {
                Log.EscribeLog("Existe un error en getListEventoCita: " + eLT.Message, 3, user);
            }
            return lst;
        }
        #endregion

        #region Perfil

        public bool setPerfil(clsUsuario usuario, int Variable, string user, ref string mensaje)
        {
            bool valido = false;
            try
            {
                switch (Variable)
                {
                    case 1://Solo Contraseña o Nombre

                        using (dbRisDA = new RISLiteEntities())
                        {
                            if (dbRisDA.tbl_CAT_Usuario.Any(x => x.intUsuarioID == usuario.intUsuarioID))
                            {
                                tbl_CAT_Usuario mdluser = dbRisDA.tbl_CAT_Usuario.First(x => x.intUsuarioID == usuario.intUsuarioID);
                                if (mdluser != null)
                                {
                                    mdluser.vchNombre = usuario.vchNombre;
                                    mdluser.vchPassword = usuario.vchPassword;
                                    mdluser.datFecha = DateTime.Now;
                                    mdluser.bitActivo = true;
                                    mdluser.vchUserAdmin = user;
                                    dbRisDA.SaveChanges();
                                    valido = true;
                                }
                            }
                        }
                        break;
                    case 2: //Solo Icono
                        using (dbRisDA = new RISLiteEntities())
                        {
                            if (dbRisDA.tbl_CAT_Usuario.Any(x => x.intUsuarioID == usuario.intUsuarioID))
                            {
                                tbl_CAT_Usuario mdluser = dbRisDA.tbl_CAT_Usuario.First(x => x.intUsuarioID == usuario.intUsuarioID);
                                if (mdluser != null)
                                {
                                    mdluser.vchRutaIcono = usuario.vchRutaIcono;
                                    mdluser.datFecha = DateTime.Now;
                                    mdluser.bitActivo = true;
                                    mdluser.vchUserAdmin = user;
                                    dbRisDA.SaveChanges();
                                    valido = true;
                                }
                            }
                        }
                        break;
                    case 3://Icono y (contraseña o nombre)
                        using (dbRisDA = new RISLiteEntities())
                        {
                            if (dbRisDA.tbl_CAT_Usuario.Any(x => x.intUsuarioID == usuario.intUsuarioID))
                            {
                                tbl_CAT_Usuario mdluser = dbRisDA.tbl_CAT_Usuario.First(x => x.intUsuarioID == usuario.intUsuarioID);
                                if (mdluser != null)
                                {
                                    mdluser.vchNombre = usuario.vchNombre;
                                    mdluser.vchPassword = usuario.vchPassword;
                                    mdluser.vchRutaIcono = usuario.vchRutaIcono;
                                    mdluser.datFecha = DateTime.Now;
                                    mdluser.bitActivo = true;
                                    mdluser.vchUserAdmin = user;
                                    dbRisDA.SaveChanges();
                                    valido = true;
                                }
                            }
                        }
                        break;
                }
            }
            catch (Exception esP)
            {
                valido = false;
                mensaje = esP.Message;
                Log.EscribeLog("Existe un error en setPerfil: " + esP.Message, 3, user);
            }
            return valido;
        }
        #endregion Perfil

        #region Estadistica
        public List<clsConfAgenda> get_modalidades_estadistica(string user, int idsitio)
        {
            List<clsConfAgenda> lst = new List<clsConfAgenda>();
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    if (dbRisDA.tbl_CAT_Modalidad.Any())
                    {
                        var query = (from x in dbRisDA.tbl_CAT_Modalidad
                                     where x.intSitioID == idsitio
                                     select new { x.intModalidadID, x.vchModalidad }).ToList();
                        if (query != null)
                        {
                            if (query.Count > 0)
                            {
                                foreach (var item in query)
                                {
                                    clsConfAgenda mdl = new clsConfAgenda();
                                    mdl.intModalidadID = (int)item.intModalidadID;
                                    mdl.vchModalidad = item.vchModalidad;
                                    lst.Add(mdl);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception eLT)
            {
                Log.EscribeLog("Existe un error en get_modalidades_estadistica: " + eLT.Message, 3, user);
            }
            return lst;
        }

        public List<clsUsuario> get_personal_Estadistica(string user, int idsitio)
        {
            List<clsUsuario> lst = new List<clsUsuario>();
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    if (dbRisDA.tbl_CAT_Modalidad.Any())
                    {
                        var query = (from x in dbRisDA.tbl_CAT_Usuario
                                     where x.intSitioID == idsitio && x.intTipoUsuario == 3
                                     select new { x.intUsuarioID, x.vchUsuario }).ToList();
                        if (query != null)
                        {
                            if (query.Count > 0)
                            {
                                foreach (var item in query)
                                {
                                    clsUsuario mdl = new clsUsuario();
                                    mdl.intUsuarioID = (int)item.intUsuarioID;
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
                Log.EscribeLog("Existe un error en get_personal_Estadistica: " + eLT.Message, 3, user);
            }
            return lst;
        }

        public List<clsSitio> get_sitio_estadistica(string user)
        {
            List<clsSitio> lst = new List<clsSitio>();
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    if (dbRisDA.tbl_CAT_Modalidad.Any())
                    {
                        var query = (from x in dbRisDA.tbl_CAT_Sitio
                                     select new { x.intSitioID, x.vchNombreSitio }).ToList();
                        if (query != null)
                        {
                            if (query.Count > 0)
                            {
                                foreach (var item in query)
                                {
                                    clsSitio mdl = new clsSitio();
                                    mdl.intSitioID = (int)item.intSitioID;
                                    mdl.vchNombreSitio = item.vchNombreSitio;
                                    lst.Add(mdl);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception eLT)
            {
                Log.EscribeLog("Existe un error en get_sitio_estadistica: " + eLT.Message, 3, user);
            }
            return lst;
        }

        public List<clsEstatusEstudio> get_sitio_estatus_estudio(string user)
        {
            List<clsEstatusEstudio> lst = new List<clsEstatusEstudio>();
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    if (dbRisDA.tbl_CAT_Modalidad.Any())
                    {
                        var query = (from x in dbRisDA.tbl_CAT_EstatusEstudio
                                     where x.bitActivo == true
                                     select new { x.intEstatusEstudio, x.vchEstatus }).ToList();
                        if (query != null)
                        {
                            if (query.Count > 0)
                            {
                                foreach (var item in query)
                                {
                                    clsEstatusEstudio mdl = new clsEstatusEstudio();
                                    mdl.intEstatusEstudio = (int)item.intEstatusEstudio;
                                    mdl.vchEstatus = item.vchEstatus;
                                    lst.Add(mdl);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception eLT)
            {
                Log.EscribeLog("Existe un error en get_sitio_estatus_estudio: " + eLT.Message, 3, user);
            }
            return lst;
        }

        public List<clsGraficaModalidad> stp_get_Datos_Modalidad_Estadistica(string user, int idsitio, int idmodalidad, string fechaInicio, string fechaFin, string estatus)
        {
            List<clsGraficaModalidad> list = new List<clsGraficaModalidad>();
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    var query = dbRisDA.stp_getGraficaModalidad(idsitio, idmodalidad, Convert.ToDateTime(fechaInicio), Convert.ToDateTime(fechaFin), estatus);
                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            clsGraficaModalidad mdl = new clsGraficaModalidad();
                            mdl.intEstudioID = (int)item.intEstudioID;
                            mdl.vchEstatus = item.vchEstatus;
                            mdl.vchModalidad = item.vchModalidad;
                            mdl.vchTitulo = item.vchTitulo;
                            mdl.vchDescripcion = item.vchDescripcion;
                            mdl.fechaInicio = (DateTime)item.datFechaInicio;
                            mdl.fechaFin = (DateTime)item.datFechaFin;
                            list.Add(mdl);
                        }
                    }
                }
            }
            catch (Exception egLC)
            {
                Log.EscribeLog("Existe un error en stp_get_Datos_Modalidad_Estadistica: " + egLC.Message, 3, user);
            }
            return list;
        }

        public List<clsGraficaUsuario> stp_get_Datos_Usuarios_Estadistica(string user, int idsitio, int idusuario, string fechaInicio, string fechaFin, string estatus)
        {
            List<clsGraficaUsuario> list = new List<clsGraficaUsuario>();
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    var query = dbRisDA.stp_getGraficaUsuario(idsitio, idusuario, Convert.ToDateTime(fechaInicio), Convert.ToDateTime(fechaFin), estatus);
                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            clsGraficaUsuario mdl = new clsGraficaUsuario();
                            mdl.intEstudioID = (int)item.intEstudioID;
                            mdl.vchUsuario = item.vchUsuario;
                            mdl.vchEstatus = item.vchEstatus;
                            mdl.vchmodalidad = item.vchModalidad;
                            mdl.vchTitulo = item.vchTitulo;
                            mdl.vchDescripcion = item.vchDescripcion;
                            mdl.fechaInicio = (DateTime)item.datFechaInicio;
                            mdl.fechaFin = (DateTime)item.datFechaFin;
                            list.Add(mdl);
                        }
                    }
                }
            }
            catch (Exception egLC)
            {
                Log.EscribeLog("Existe un error en stp_get_Datos_Usuarios_Estadistica: " + egLC.Message, 3, user);
            }
            return list;
        }
        #endregion

        #region Arribo
        public bool getDetalleCitaPaciente(int intCitaID, string user, ref string mensaje, ref clsEstudioCita cita, ref List<clsEstudio> lstEstudios)
        {
            bool valido = false;
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    if(dbRisDA.tbl_MST_Cita.Any(x=> x.intCitaID == intCitaID))
                    {
                        var query = dbRisDA.stp_getDetalleCitaPaciente(intCitaID).ToList();
                        if(query != null)
                        {
                            if (query.Count > 0)
                            {
                                cita.datFechaCita = (DateTime)query.First().datFechaCita;
                                cita.intCitaID = intCitaID;
                                cita.intPacienteID = (int)query.First().intPacienteID;
                                cita.vchNombrePaciente = query.First().vchNombrePaciente;
                                foreach(stp_getDetalleCitaPaciente_Result item in query)
                                {
                                    clsEstudio mdlEstudio = new clsEstudio();
                                    mdlEstudio.fechaInicio = (DateTime)item.datFechaInicio;
                                    mdlEstudio.fechaFin = (DateTime)item.datFechaFin;
                                    mdlEstudio.intEstudioID = (int)item.intEstudioID;
                                    mdlEstudio.intRelModPres = (int)item.intRELModPres;
                                    mdlEstudio.vchModalidad = item.vchModalidad;
                                    mdlEstudio.vchPrestacion = item.vchPrestacion;
                                    mdlEstudio.vchEstatus = item.vchEstatus;
                                    lstEstudios.Add(mdlEstudio);
                                }
                            }
                        }
                    }
                    valido = true;
                }
            }
            catch(Exception egDC)
            {
                mensaje = egDC.Message;
                Log.EscribeLog("Existe un error en getDetalleCitaPaciente: " + egDC.Message, 3, user);
            }
            return valido;
        }

        public bool setActualizaEstudioEstatus(int intEstudioID, int intEstatusID, string  user, ref string mensaje)
        {
            bool valido = false;
            try
            {
                using(dbRisDA = new RISLiteEntities())
                {
                    if(dbRisDA.tbl_MST_Estudio.Any(x => x.intEstudioID == intEstudioID && (bool)x.bitActivo))
                    {
                        tbl_MST_Estudio mdlEstudio = new tbl_MST_Estudio();
                        mdlEstudio = dbRisDA.tbl_MST_Estudio.First(x => x.intEstudioID == intEstudioID && (bool)x.bitActivo);
                        mdlEstudio.intEstatusEstudio = intEstatusID;
                        mdlEstudio.vchUserAdmin = user;
                        mdlEstudio.datFecha = DateTime.Now;
                        dbRisDA.SaveChanges();
                        valido = true;
                    }
                }
            }
            catch(Exception esAE)
            {
                valido = false;
                mensaje = esAE.Message;
                Log.EscribeLog("Existe un error en setActualizaEstudioEstatus: " + esAE.Message, 3, user);
            }
            return valido;
        }
        #endregion Arribo
    }
}
